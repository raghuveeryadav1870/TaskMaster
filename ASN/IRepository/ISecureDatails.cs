namespace ASN.IRepository
{
    public interface ISecureDatails
    {
        public string Encrypt(string? encryptString);
        public string Decrypt(string? cipherText);
    }
}
