using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LinQwithJoin.Models;

namespace LinQwithJoin.Controllers
{
    public class SchoolStudentsController : Controller
    {
        private readonly PraveenContext _context;

        public SchoolStudentsController(PraveenContext context)
        {
            _context = context;
        }

        // GET: SchoolStudents
        public async Task<IActionResult> Index()
        {
            //var praveenContext = _context.SchoolStudents.Include(s => s.Student);

            //return View(await praveenContext.ToListAsync());
                        //var praveenContext = _context.SchoolStudents.Include(s => s.Student);
            var query = from student in _context.SchoolStudents
                        join person in _context.People on student.StudentId equals person.PersonId
                        select new
                        {
                            personId = person.PersonId,
                            personName = person.PersonName,
                            personGender = person.PersonGender,
                            personAge = person.PersonAge,
                            personCity = person.PersonCity,
                            schoolName = student.SchoolName,
                            studentMarks = student.StudentMark,
                            RollNumber= student.RollNumber,

                        };
            //Console.WriteLine(query);
            //Log.Information("Query==>{@query}", query);
            return View(query);
        }

        // GET: SchoolStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolStudent = await _context.SchoolStudents
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.RollNumber == id);
            if (schoolStudent == null)
            {
                return NotFound();
            }

            return View(schoolStudent);
        }

        // GET: SchoolStudents/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.People, "PersonId", "PersonId");
            return View();
        }

        // POST: SchoolStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,SchoolName,StudentMark,RollNumber")] SchoolStudent schoolStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.People, "PersonId", "PersonId", schoolStudent.StudentId);
            return View(schoolStudent);
        }

        // GET: SchoolStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolStudent = await _context.SchoolStudents.FindAsync(id);
            if (schoolStudent == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.People, "PersonId", "PersonId", schoolStudent.StudentId);
            return View(schoolStudent);
        }

        // POST: SchoolStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,SchoolName,StudentMark,RollNumber")] SchoolStudent schoolStudent)
        {
            if (id != schoolStudent.RollNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolStudentExists(schoolStudent.RollNumber))
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
            ViewData["StudentId"] = new SelectList(_context.People, "PersonId", "PersonId", schoolStudent.StudentId);
            return View(schoolStudent);
        }

        // GET: SchoolStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolStudent = await _context.SchoolStudents
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.RollNumber == id);
            if (schoolStudent == null)
            {
                return NotFound();
            }

            return View(schoolStudent);
        }

        // POST: SchoolStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolStudent = await _context.SchoolStudents.FindAsync(id);
            if (schoolStudent != null)
            {
                _context.SchoolStudents.Remove(schoolStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolStudentExists(int id)
        {
            return _context.SchoolStudents.Any(e => e.RollNumber == id);
        }
    }
}
