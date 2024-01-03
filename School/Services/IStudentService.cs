using School.Data.Models;

namespace School.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentById(Guid id);
        void AddStudent(Student student);
        void UpdateStudent(Guid id, Student updatedStudent);
        void DeleteStudent(Guid id);
    }
}
