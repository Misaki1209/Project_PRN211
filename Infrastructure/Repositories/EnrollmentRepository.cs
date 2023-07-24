using Domain.Models;
using Infrastructure.Dtos;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private ProjectPrn221Context _context;

    public EnrollmentRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<Enrollment> GetAllEnrollments()
    {
        try
        {
            return _context.Enrollments;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<Enrollment> GetEnrollmentByTeacherId(int teacherId)
    {
        try
        {
            return _context.Enrollments.Where(x => x.TeacherId == teacherId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<int> GetSubjectIdListByTeacher(int teacherId, int semesterId)
    {
        try
        {
            return _context.Enrollments
                .Where(x => x.TeacherId == teacherId 
                            && x.SemesterId == semesterId)
                .Select(x => x.SubjectId)
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<int> GetSubjectIdListByStudent(int studentId, int semesterId)
    {
        try
        {
            return _context.Enrollments
                .Where(x => x.StudentId == studentId 
                            && x.SemesterId == semesterId)
                .Select(x => x.SubjectId)
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<int> GetSubjctIdListByAdmin(int semesterId)
    {
        try
        {
            return _context.Enrollments
                .Where(x => x.SemesterId == semesterId)
                .Select(x => x.SubjectId)
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<int> GetClassIdListByTeacher(int teacherId, int semesterId, int subjectId)
    {
        try
        {
            return _context.Enrollments
                .Where(x => x.TeacherId == teacherId 
                            && x.SemesterId == semesterId 
                            && x.SubjectId == subjectId)
                .Select(x => x.ClassId)
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<int> GetClassIdListByAdmin(int semesterId, int subjectId)
    {
        try
        {
            return _context.Enrollments
                .Where(x => x.SemesterId == semesterId 
                            && x.SubjectId == subjectId)
                .Select(x => x.ClassId)
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<EnrollmentForClassList> GetClassDetail(int teacherId, int semesterId, int subjectId, int classId)
    {
        try
        {
            var enrollmentList = _context.Enrollments
                .Include(x => x.Student)
                .Where(x => x.TeacherId == teacherId
                            && x.SemesterId == semesterId && x.SubjectId == subjectId && x.ClassId == classId);
            var cc =  enrollmentList.Join(_context.StudentDetails,
                e => e.Student.AccountId,
                s => s.AccountId,
                (e, s) => new EnrollmentForClassList
                {
                    EnrollmentId = e.EnrollmentId,
                    TeacherId = e.TeacherId,
                    SubjectId = e.SubjectId,
                    ClassId = e.ClassId,
                    StudentId = e.StudentId,
                    SemesterId = e.SemesterId,
                    StudentCode = s.StudentCode,
                    StudentName = s.FullName
                }).ToList();
            return cc;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<EnrollmentForClassList> GetClassDetailByAdmin(int semesterId, int subjectId, int classId)
    {
        try
        {
            var enrollmentList = _context.Enrollments
                .Include(x => x.Student)
                .Where(x => x.SemesterId == semesterId && x.SubjectId == subjectId && x.ClassId == classId);
            var cc =  enrollmentList.Join(_context.StudentDetails,
                e => e.Student.AccountId,
                s => s.AccountId,
                (e, s) => new EnrollmentForClassList
                {
                    EnrollmentId = e.EnrollmentId,
                    TeacherId = e.TeacherId,
                    SubjectId = e.SubjectId,
                    ClassId = e.ClassId,
                    StudentId = e.StudentId,
                    SemesterId = e.SemesterId,
                    StudentCode = s.StudentCode,
                    StudentName = s.FullName
                }).ToList();
            return cc;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public EnrollmentDto GetEnrollmentDetail(int enrollmentId)
    {
        try
        {
            var enrollment =  _context.Enrollments
                .Include(x => x.Class)
                .Include(x => x.Semester)
                .Include(x => x.Student)
                .Include(x => x.Teacher)
                .Include(x => x.Subject)
                .ThenInclude(x => x.SubjectMarks).ThenInclude(x => x.Mark)
                .FirstOrDefault(x => x.EnrollmentId == enrollmentId);
            var studentDetail = _context.StudentDetails.FirstOrDefault(x => x.AccountId == enrollment.StudentId);
            var enrollmentDto = new EnrollmentDto
            {
                EnrollmentId = enrollment.EnrollmentId,
                TeacherId = enrollment.TeacherId,
                SubjectId = enrollment.SubjectId,
                ClassId = enrollment.ClassId,
                StudentId = enrollment.StudentId,
                SemesterId = enrollment.SemesterId,
                StudentCode = studentDetail.StudentCode,
                StudentName = studentDetail.FullName,
                Status = enrollment.Status,
                FinalMark = enrollment.FinalMark,
                Class = enrollment.Class,
                Semester = enrollment.Semester,
                Student = enrollment.Student,
                Teacher = enrollment.Teacher,
                Subject = enrollment.Subject
    };
    return enrollmentDto;
    /*var cc =  _context.Enrollments
        /*.Include(x => x.Class)
        .Include(x => x.Semester)
        .Include(x => x.Student)
        .Include(x => x.Teacher)#1#
        .Include(x => x.Subject)
        .ThenInclude(x => x.SubjectMarks)
        .Where(x => x.EnrollmentId == enrollmentId)
        .Join(_context.StudentDetails,
            e => e.StudentId,
            s => s.AccountId,
            (e, s) => new EnrollmentDto
            {
                EnrollmentId = e.EnrollmentId,
                TeacherId = e.TeacherId,
                SubjectId = e.SubjectId,
                ClassId = e.ClassId,
                StudentId = e.StudentId,
                SemesterId = e.SemesterId,
                StudentCode = s.StudentCode,
                StudentName = s.FullName,
                Status = e.Status,
                FinalMark = e.FinalMark,
                /*Class = e.Class,
                Semester = e.Semester,
                Student = e.Student,
                Teacher = e.Teacher,#1#
                Subject = e.Subject
            });
    return cc.First();*/
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public EnrollmentDto? GetEnrollmentDetailByStudent(int studentId, int semesterId, int subjectId)
    {
        try
        {
            var enrollment = _context.Enrollments
                .Include(x => x.Class)
                .Include(x => x.Semester)
                .Include(x => x.Student)
                .Include(x => x.Teacher)
                .Include(x => x.Subject)
                .ThenInclude(x => x.SubjectMarks).ThenInclude(x => x.Mark)
                .FirstOrDefault(x => x.StudentId == studentId && x.SemesterId == semesterId && x.SubjectId == subjectId);
            if (enrollment == null)
                return null;
            var studentDetail = _context.StudentDetails.FirstOrDefault(x => x.AccountId == enrollment.StudentId);
            var enrollmentDto = new EnrollmentDto
            {
                EnrollmentId = enrollment.EnrollmentId,
                TeacherId = enrollment.TeacherId,
                SubjectId = enrollment.SubjectId,
                ClassId = enrollment.ClassId,
                StudentId = enrollment.StudentId,
                SemesterId = enrollment.SemesterId,
                StudentCode = studentDetail.StudentCode,
                StudentName = studentDetail.FullName,
                Status = enrollment.Status,
                FinalMark = enrollment.FinalMark,
                Class = enrollment.Class,
                Semester = enrollment.Semester,
                Student = enrollment.Student,
                Teacher = enrollment.Teacher,
                Subject = enrollment.Subject
            };
            return enrollmentDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateFinalMark(int enrollmentId)
    {
        try
        {
            var enrollment = _context.Enrollments.FirstOrDefault(x => x.EnrollmentId == enrollmentId);
            if (enrollment == null)
                throw new Exception("Not found");
            var markList = _context.MarkReports.Include(x => x.Mark).Where(x => x.EnrollmentId == enrollmentId).ToList();
            var finalMark = markList.Sum(mark => mark.MarkValue * mark.Mark.Coefficient);
            foreach (var mark in markList)
            {
                if (mark.Mark.Resit != null)
                {
                    foreach (var markResit in markList)
                    {
                        if (markResit.MarkId == mark.Mark.Resit && markResit.MarkValue != 0)
                            finalMark -= mark.MarkValue * mark.Mark.Coefficient;
                    }
                }
            }
            enrollment.FinalMark = Math.Round(finalMark,2);
            if (enrollment.FinalMark < 5)
                enrollment.Status = false;
            else
            {
                var check = true;
                foreach (var mark in markList)
                {
                    if (mark.Mark.Resit == null) continue;
                    foreach (var markResit in markList.Where(markResit => markResit.MarkId == mark.Mark.Resit))
                    {
                        if (markResit.MarkValue == 0 && mark.MarkValue < 4) check = false;
                        if (markResit.MarkValue != 0 && markResit.MarkValue < 4) check = false;
                    }
                }

                enrollment.Status = check;
            }

            _context.Enrollments.Update(enrollment);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        try
        {
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public int GetNextId()
    {
        try
        {
            return _context.Enrollments.OrderByDescending(x => x.EnrollmentId).First().EnrollmentId + 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}