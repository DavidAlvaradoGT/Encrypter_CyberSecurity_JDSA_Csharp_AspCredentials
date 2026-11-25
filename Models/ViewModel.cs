using System.Collections.Generic;

namespace Encrypter_JDSA.Models
{
    public class UserCredentialRow
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string EncryptedPassword { get; set; }
        public string EncryptedUserName { get; set; }
        public string DecryptedPassword { get; set; }
        public string DecryptedUserName { get; set; }
        public string UserDateCreation { get; set; }
    }
    public class UserCredentialPageModel
    {
        public string UserNameInput {  get; set; }
        public string PasswordInput { get; set; }
        public string NetworkDateUsed { get; set; }
        public List<UserCredentialRow> Users { get; set; } = new List<UserCredentialRow>();

    }
}
