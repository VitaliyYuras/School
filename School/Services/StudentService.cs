using School.Data.Models;
using School.Data.Repository;

namespace School.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _studentRepository.GetStudents();
        }

        public Student GetStudentById(Guid id)
        {
            return _studentRepository.GetStudentById(id);
        }

        public void AddStudent(Student student)
        {
            _studentRepository.AddStudent(student);
        }

        public void UpdateStudent(Guid id, Student updatedStudent)
        {
            var existingStudent = _studentRepository.GetStudentById(id);

            if (existingStudent != null)
            {
                existingStudent.FirstName = updatedStudent.FirstName;
                existingStudent.LastName = updatedStudent.LastName;
                _studentRepository.UpdateStudent(existingStudent);
            }
        }

        public void DeleteStudent(Guid id)
        {
            _studentRepository.DeleteStudent(id);
        }
    }
}
