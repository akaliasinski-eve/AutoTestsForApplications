using AutoTestsForApplications.Utils.Enums;

namespace AutoTestsForApplications.Storages.ForUI.Models;

public class StudentRegistrationFormModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public GenderType Gender { get; set; }
    public string MobileNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<string> Subjects { get; set; }
    public List<HobbyType> Hobbies { get; set; }
    public string PicturePath { get; set; }
    public string CurrentAddress { get; set; }
    public string State { get; set; }
    public string City { get; set; }
}