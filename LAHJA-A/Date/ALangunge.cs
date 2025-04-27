//using Date.Repository;

//using System.ComponentModel.DataAnnotations;
//using System.Diagnostics;
//using System.Linq;
//using System.Linq;
//using System.Reflection.Metadata.Ecma335;
//using Data.Modelues;

//public interface IInfo
//{
//    void Info();
//}

//public interface ISchoolRepository : IRepsitory<HeaderItem>, IInfo
//{
//    void AddRow(string studentid, string schoolId);
//    //void AddRow(RowModel row);
//    //void AddTeacher(string teacherId, string schoolId);
//    //void AddStudent(StudentModel student, string rowId);
//    //void AddStudent(string studentid, string schoolId);
//    //void AddStudent(string teacherId, string schoolId, string rowId);
//    //void UpdateSchoolName(string schoolId, string newName);
//    //void RemoveStudent(string studentId, string schoolId);
//    //void RemoveTeacher(string teacherId, string schoolId);
//    //void AddModul(ModulModel modul, string rowId);
//    //void AddTeacherToModul(TeacherModel teacher, string modulId);
//    //void ShowAll();
//    //void SerachStudent(StudentModel name);
//    void SearchStudentByRowName(string rowName);
//}








//public class SchoolRepository : Repository<HeaderItem>, ISchoolRepository
//{

   
  
//}


////public void ShowAll()
////{
////    Console.WriteLine("===== School Info =====");

////    _teacherRepository.Info();
////    _studentRepository.Info();
////    _modulRepository.Info();
////    _rowRepository.Info();
////}






//public abstract class Printer
//{
//    public static void PrintAll(params IEnumerable<IInfo>[] collections)
//    {
//        foreach (var collection in collections)
//        {
//            foreach (var item in collection)
//            {
//                item.Info();
//            }

//        }
//    }

//}

//public abstract class Validator
//{
//    public static bool Validate<T>(ICollection<T> items, T item) where T : class
//    {

//        foreach (var ob in items)
//        {
//            if (ob == item)
//            {
//                Console.WriteLine("Error: Null item found in the list.");
//                return false;
//            }

//        }

//        return true;

//    }
//}
