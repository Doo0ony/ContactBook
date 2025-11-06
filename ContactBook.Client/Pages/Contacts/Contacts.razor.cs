using ContactBook.Client.Services;
using ContactBook.Shared.DTOs.Phone;
using ContactBook.Shared.DTOs.User;
using Microsoft.AspNetCore.Components;

namespace ContactBook.Client.Pages.Contacts
{
    public class UsersPhonesBase : ComponentBase
    {
        [Inject] protected UserService UserService { get; set; } = default!;
        [Inject] protected PhoneService PhoneService { get; set; } = default!;

        protected List<UserDto>? users;

        // Store phones per user
        protected Dictionary<int, List<PhoneDto>> userPhones = new();
        protected HashSet<int> visiblePhoneUsers = new();

        protected bool showUserModal = false;
        protected bool showPhoneModal = false;
        protected bool showConfirmModal = false;

        protected UserDto editUser = new();
        protected PhoneDto? editPhone = null;
        protected string phoneNumberInput = string.Empty;
        protected UserDto? currentPhoneUser = null;

        protected string confirmMessage = string.Empty;
        protected Func<Task>? confirmAction;

        protected override async Task OnInitializedAsync() => await LoadUsers();

        protected async Task LoadUsers()
        {
            users = await UserService.GetUsersAsync();
            StateHasChanged();
        }

        // USER CRUD
        protected void ShowAddUserModal()
        {
            editUser = new();
            showUserModal = true;
        }

        protected void ShowEditUserModal(UserDto user)
        {
            editUser = new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
            showUserModal = true;
        }

        protected async Task SaveUser()
        {
            if (editUser.Id == 0)
            {
                await UserService.CreateUserAsync(new CreateUserDto
                {
                    Name = editUser.Name,
                    Email = editUser.Email
                });
            }
            else
            {
                await UserService.UpdateUserAsync(new UpdateUserDto
                {
                    Id = editUser.Id,
                    Name = editUser.Name,
                    Email = editUser.Email
                });
            }

            await LoadUsers();
            showUserModal = false;
        }

        protected void CloseUserModal() => showUserModal = false;
    
        protected void ConfirmDeleteUser(UserDto user)
        {
            confirmMessage = $"Are you sure you want to delete user '{user.Name}'?";
            confirmAction = async () =>
            {
                await UserService.DeleteUserAsync(user.Id);
                await LoadUsers();
                visiblePhoneUsers.Remove(user.Id);
                userPhones.Remove(user.Id);
            };
            showConfirmModal = true;
        }

        // PHONE CRUD
        protected async Task LoadPhones(UserDto user)
        {
            if (!userPhones.ContainsKey(user.Id))
            {
                var phonesList = await PhoneService.GetPhonesByUserIdAsync(user.Id);
                userPhones[user.Id] = phonesList;
            }
        }

        protected async Task TogglePhones(UserDto user)
        {
            if (visiblePhoneUsers.Contains(user.Id))
                visiblePhoneUsers.Remove(user.Id);
            else
            {
                visiblePhoneUsers.Add(user.Id);
                await LoadPhones(user);
            }
        }

        protected bool IsUserPhonesVisible(UserDto user) => visiblePhoneUsers.Contains(user.Id);

        protected void ShowAddPhoneModal(UserDto user)
        {
            currentPhoneUser = user;
            editPhone = new PhoneDto();
            phoneNumberInput = string.Empty;
            showPhoneModal = true;
        }

        protected void ShowEditPhoneModal(PhoneDto phone, UserDto user)
        {
            currentPhoneUser = user;
            editPhone = phone;
            phoneNumberInput = phone.PhoneNumber;
            showPhoneModal = true;
        }

        protected void ClosePhoneModal()
        {
            showPhoneModal = false;
            editPhone = null;
            currentPhoneUser = null;
        }

        protected async Task SavePhone()
        {
            if (currentPhoneUser == null || string.IsNullOrWhiteSpace(phoneNumberInput))
                return;

            if (editPhone != null && editPhone.Id == 0)
            {
                await PhoneService.CreatePhoneAsync(new CreatePhoneDto
                {
                    UserId = currentPhoneUser.Id,
                    PhoneNumber = phoneNumberInput
                });
            }
            else if (editPhone != null)
            {
                await PhoneService.UpdatePhoneAsync(editPhone.Id, new UpdatePhoneDto
                {
                    Id = editPhone.Id,
                    UserId = currentPhoneUser.Id,
                    PhoneNumber = phoneNumberInput
                });
            }

            userPhones[currentPhoneUser.Id] = await PhoneService.GetPhonesByUserIdAsync(currentPhoneUser.Id);
            showPhoneModal = false;
        }

        protected void ConfirmDeletePhone(PhoneDto phone, UserDto user)
        {
            confirmMessage = $"Delete phone '{phone.PhoneNumber}'?";
            confirmAction = async () =>
            {
                await PhoneService.DeletePhoneAsync(phone.Id);
                userPhones[user.Id] = await PhoneService.GetPhonesByUserIdAsync(user.Id);
            };
            showConfirmModal = true;
        }

        // CONFIRM MODAL
        protected async Task ConfirmDeleteAction()
        {
            if (confirmAction != null)
                await confirmAction.Invoke();
            showConfirmModal = false;
        }

        protected void CloseConfirmModal() => showConfirmModal = false;
    }
}
