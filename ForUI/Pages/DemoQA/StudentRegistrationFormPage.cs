using System.Globalization;
using AutoTestsForApplications.Storages.ForUI.Models;
using AutoTestsForApplications.Utils.Enums;
using Microsoft.Playwright;

namespace AutoTestsForApplications.ForUI.Pages.DemoQA;

public class StudentRegistrationFormPage
{
    private IPage Page;
    private ILocator FirstName => Page.Locator("#firstName");
    private ILocator LastName => Page.Locator("#lastName");
    private ILocator Email => Page.Locator("#userEmail");
    private ILocator Gender(GenderType gender) => Page.Locator($"//label[text()='{gender}']");
    private ILocator Mobile => Page.Locator("#userNumber");
    private ILocator DateOfBirth => Page.Locator("#dateOfBirthInput");
    private ILocator SubjectsField => Page.Locator("#subjectsInput");
    private ILocator HobbiesCheckbox(HobbyType hobby) => Page.Locator($"//label[text()='{hobby}']");
    private ILocator PictureUpload => Page.Locator("//input[@label='Select picture']");
    private ILocator CurrentAddressField => Page.Locator("#currentAddress");
    private ILocator SelectStateField => Page.Locator("#react-select-3-input");
    private ILocator SelectCityField => Page.Locator("#city");
    private ILocator DropdownOption(string optionName) => Page.Locator($"//div[text()='{optionName}']");
    private ILocator SubmitButton => Page.GetByRole(AriaRole.Button, new() { Name = "Submit" });
    private ILocator ModalTitle => Page.Locator("#example-modal-sizes-title-lg");

    public StudentRegistrationFormPage(IPage page)
    {
        Page = page;
    }

    public async Task OpenStudentRegistrationFormAsync()
    {
        await Page.GotoAsync("https://demoqa.com/automation-practice-form");
    }

    public async Task FillFormAsync(StudentRegistrationFormModel studentData)
    {
        await FirstName.FillAsync(studentData.FirstName);
        await LastName.FillAsync(studentData.LastName);
        await Email.FillAsync(studentData.Email);
        await Gender(studentData.Gender).CheckAsync();
        await Mobile.FillAsync(studentData.MobileNumber);
        await EnterDateOfBirthAsync(studentData.DateOfBirth.ToString("dd MMM yyyy", CultureInfo.InvariantCulture));
        await AddSubjectsAsync(studentData.Subjects);
        await AddJobbiesAsync(studentData.Hobbies);
        await PictureUpload.SetInputFilesAsync(studentData.PicturePath);
        await CurrentAddressField.FillAsync(studentData.CurrentAddress);
        await SelectStateAndCityAsync(studentData.State, studentData.City);
    }

    public async Task AddSubjectsAsync(List<string> subjects)
    {
        foreach (var subject in subjects)
        {
            await SubjectsField.FillAsync(subject);
            await SubjectsField.PressAsync("Enter");
        }
    }

    public async Task AddJobbiesAsync(List<HobbyType> hobbies)
    {
        foreach (var hobby in hobbies)
        {
            await HobbiesCheckbox(hobby).ClickAsync();
        }
    }

    public async Task SelectStateAndCityAsync(string state, string city)
    {
        await SelectStateField.ClickAsync();
        await DropdownOption(state).ClickAsync();
        await SelectCityField.ClickAsync();
        await DropdownOption(city).ClickAsync();
    }

    public async Task EnterDateOfBirthAsync(string dateOfBirth)
    {
        await DateOfBirth.FillAsync(dateOfBirth);
        await DateOfBirth.PressAsync("Enter");
    }

    public async Task ClickSubmitAsync()
    {
        await SubmitButton.ClickAsync();
    }

    public async Task<string> GetModalTitleAsync()
    {
        var modalTitle = await ModalTitle.InnerTextAsync();
        return modalTitle;
    }
}