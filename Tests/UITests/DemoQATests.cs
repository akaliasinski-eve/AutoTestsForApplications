using AutoTestsForApplications.ForUI.Pages.DemoQA;
using AutoTestsForApplications.Storages.ForUI.Builders;
using AutoTestsForApplications.Storages.ForUI.Models;
using AutoTestsForApplications.Utils.Enums;
using FluentAssertions;

namespace AutoTestsForApplications.UITests;

[TestFixture]
public class DemoQATests : BaseTest
{
    [Test]
    public async Task SelectItemInDropdownAndCheckIt()
    {
        SelectMenuPage selectMenuPage = new SelectMenuPage(Page);
        await selectMenuPage.OpenSelectMenuPageAsync();
        await selectMenuPage.SelectItemInDropdownAsync("Prof.");
        await selectMenuPage.CheckSelectedItemIsDisplayedAsync("Prof.");
    }

    [Test]
    public async Task FillStudentRegistrationForm()
    {
        StudentRegistrationFormPage studentRegistrationFormPage = new StudentRegistrationFormPage(Page);
        await studentRegistrationFormPage.OpenStudentRegistrationFormAsync();
        StudentRegistrationBuilder builder = new StudentRegistrationBuilder();
        var studentData = builder.WithFirstName("Rajesh")
            .WithLastName("Kutrapalli")
            .WithEmail("rajesh@gmail.com")
            .WithGender(GenderType.Male)
            .WithMobile("375291122334")
            .WithDateOfBirth(new DateTime(1985, 03, 14))
            .WithSubjects("Maths", "English", "Physics")
            .WithHobbies(HobbyType.Sports, HobbyType.Music)
            .WithPicture(Path.Combine(AppContext.BaseDirectory, @"Resources\test_picture.png"))
            .WithAddress("123 Main St.")
            .WithLocation("Haryana", "Karnal")
            .Build();

        await studentRegistrationFormPage.FillFormAsync(studentData);
        await studentRegistrationFormPage.ClickSubmitAsync();
        var modalTitle = await studentRegistrationFormPage.GetModalTitleAsync();
        modalTitle.Should().Be("Thanks for submitting the form");
    }
}