using School.Data.Models;

namespace School.Data.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        Student GetStudentById(Guid id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Guid id);
    }
}
