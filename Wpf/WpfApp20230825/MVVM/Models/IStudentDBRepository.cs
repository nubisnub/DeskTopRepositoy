using System.Collections.ObjectModel;

namespace WpfApp20230825.Models
{
    public interface IStudentDBRepository
    {
        ObservableCollection<StudentDB> GetOne();
        ObservableCollection<StudentDB> GetAll();

        
        public bool InsertOrUpdate(StudentDB studentDB);
        public bool Delete(string name, string day_of_the_class);

    }
}