using SchoolDesktop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolDesktop
{
    public partial class MainFrom : Form
    {
        private readonly ApiClient apiClient;

        public MainFrom(string token)
        {
            InitializeComponent();
            apiClient = new ApiClient("https://localhost:7253", token);
            LoadStudents();
        }

        private async void LoadStudents()
        {
            var students = await apiClient.GetStudentsAsync();
            dataGridViewStudents.DataSource = students;
        }

        private async void buttonCreate_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxFirstName.Text) || string.IsNullOrWhiteSpace(textBoxLastName.Text))
                {
                    MessageBox.Show("Please enter both first name and last name.");
                    return;
                }

                var newStudent = new Student
                {
                    FirstName = textBoxFirstName.Text,
                    LastName = textBoxLastName.Text
                };

                var createdStudent = await apiClient.CreateStudentAsync(newStudent);

                MessageBox.Show($"Student {createdStudent.FirstName} {createdStudent.LastName} created successfully.");

                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating student: {ex.Message}");
            }
        }

        private async void buttonDelete_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewStudents.SelectedRows.Count > 0)
                {
                    Guid selectedStudentId = Guid.Parse(dataGridViewStudents.SelectedRows[0].Cells["Id"].Value.ToString());

                    await apiClient.DeleteStudentAsync(selectedStudentId);

                    MessageBox.Show("Student deleted successfully.");

                    LoadStudents();
                }
                else
                {
                    MessageBox.Show("Please select a student to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting student: {ex.Message}");
            }
        }

        private async void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewStudents.SelectedRows.Count > 0)
                {
                    Guid selectedStudentId = Guid.Parse(dataGridViewStudents.SelectedRows[0].Cells["Id"].Value.ToString());

                    var updatedStudent = new Student
                    {
                        Id = selectedStudentId,
                        FirstName = textBoxFirstName.Text,
                        LastName = textBoxLastName.Text
                    };

                    await apiClient.UpdateStudentAsync(selectedStudentId, updatedStudent);

                    MessageBox.Show($"Student {updatedStudent.FirstName} {updatedStudent.LastName} updated successfully.");

                    LoadStudents();
                }
                else
                {
                    MessageBox.Show("Please select a student to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student: {ex.Message}");
            }
        }
    }
}
