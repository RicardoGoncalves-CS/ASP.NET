﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalFilesManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notes.App.Data;
using Notes.App.Models;

namespace Notes.App.Controllers
{
    public class NotesController : Controller
    {
        private readonly NotesAppContext _context;

        public NotesController(NotesAppContext context)
        {
            _context = context;
        }

        // LOAD from local files
        [HttpPost]
        public async Task<IActionResult> Load()
        {
            string directoryPath = "C:\\Users\\ricar\\Desktop\\TextFiles";

            List<FileEntry> localFiles = FilesManager.ReadFilesFromDirectory(directoryPath);
            List<Note> notes = new List<Note>();

            foreach (var file in localFiles)
            {
                bool noteExists = await _context.Note.AnyAsync(n => n.Title == file.FileName && n.Content == file.Content);

                if (!noteExists)
                {
                    Note note = new Note
                    {
                        Title = file.FileName,
                        Content = file.Content,
                        CreationDate = file.CreationDate
                    };

                    notes.Add(note);
                }
            }

            _context.Note.AddRange(notes);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Save()
        {
            string directoryPath = "C:\\Users\\ricar\\Desktop\\TextFiles";

            List<Note> notes = await _context.Note.ToListAsync();
            List<FileEntry> files = new List<FileEntry>();

            foreach(var note in notes)
            {
                FileEntry file = new FileEntry
                {
                    FileName = note.Title,
                    Content = note.Content,
                    CreationDate = note.CreationDate
                };

                files.Add(file);
            }

            FilesManager.SaveFilesToDirectory(directoryPath, files);

            return RedirectToAction(nameof(Index));
        }

        // GET: Notes
        /*
        public async Task<IActionResult> Index()
        {
            return _context.Note != null ? 
                View(await _context.Note.ToListAsync()) :
                Problem("Entity set 'NotesAppContext.Note'  is null.");
        }
        */

        public IActionResult Index(string sortOrder)
        {
            ViewData["DateCreatedSortParam"] = string.IsNullOrEmpty(sortOrder) ? "date_created_desc" : "";
            List<Note> notes = _context.Note.ToList();

            switch (sortOrder)
            {
                case "date_created_desc":
                    notes = notes.OrderByDescending(n => n.CreationDate).ToList();
                    break;
                default:
                    notes = notes.OrderBy(n => n.CreationDate).ToList();
                    break;
            }

            return View(notes);
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content")] Note note)
        {
            if (ModelState.IsValid)
            {
                note.Id = Guid.NewGuid();
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Content")] Note updatedNote)
        {
            if (id != updatedNote.Id)
            {
                return NotFound();
            }

            Note existingNote = new Note();

            if (ModelState.IsValid)
            {
                try
                {
                    existingNote = await _context.Note.FindAsync(id);
                    if(existingNote == null)
                    {
                        return NotFound();
                    }

                    DateTime creationDate = existingNote.CreationDate;

                    existingNote.Title = updatedNote.Title;
                    existingNote.Content = updatedNote.Content;
                    existingNote.CreationDate = creationDate;

                    _context.Update(existingNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(updatedNote.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(existingNote);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Note == null)
            {
                return Problem("Entity set 'NotesAppContext.Note'  is null.");
            }
            var note = await _context.Note.FindAsync(id);
            if (note != null)
            {
                _context.Note.Remove(note);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(Guid id)
        {
          return (_context.Note?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
