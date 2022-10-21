using DataAccessLayer;
using Model3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Logic
    {
        IRepository<Student> repository { set; get; }
        public Logic(IRepository<Student> repository1)
        {
            repository = repository1;
        }


        /// <summary>
        /// Добавить нового студента.
        /// </summary>
        public void AddStudent(string name, string speciality, string group)
        {
            if (!string.IsNullOrEmpty(name) | !string.IsNullOrEmpty(speciality) | !string.IsNullOrEmpty(group))
            {
                repository.Create(new Student { Name = name, Speciality = speciality, Group = group });
                repository.Save();
            }
        }

        /// <summary>
        /// Удалить студента по идентификатору.
        /// </summary>
        public void DeleteStudent(int id)
        {
            repository.Delete(id);
            repository.Save();
        }

        public void DeleteStudent()
        {
            repository.DeleteAll();//смотри описание метода
            repository.Save();
        }

        /// <summary>
        /// Вывести весь список студентов.
        /// </summary>
        public List<Student> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public List<(string Name, string Speciality, string Group, int Id)> GetAllStudents()
        {
            var temp = new List<(string Name, string Speciality, string Group, int Id)>();
            foreach (var student in repository.GetAll())
            {
                temp.Add((student.Name, student.Speciality, student.Group, student.Id));
            }
            return temp;
        }


        public Dictionary<string, int> DistributionOfSpecialties()
        {
            Dictionary<string, int> specialtiesDistribution = new Dictionary<string, int>();

            foreach (Student student in (List<Student>)repository.GetAll())
            {
                if (specialtiesDistribution.ContainsKey(student.Speciality))
                    specialtiesDistribution[student.Speciality] += 1;

                else
                    specialtiesDistribution[student.Speciality] = 1;
            }
            return specialtiesDistribution;
        }
    }
}
