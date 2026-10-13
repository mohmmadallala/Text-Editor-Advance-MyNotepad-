using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log = System.Console;

namespace EditorTextAdvance
{
    public class clsCyptography
    {
        static public void EncryptFile(string inputFile, string outputFile, string key)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                    using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                    using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                    using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
                    using (CryptoStream cryptoStream = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                    {
                        // Write the IV to the beginning of the file
                        fsOutput.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                        fsInput.CopyTo(cryptoStream);
                    }
                    System.Windows.Forms.MessageBox.Show("Successfully Encryption File");
                }

            }
            catch (CryptographicException ex)
            {
                System.Windows.Forms.MessageBox.Show("Croptography : " + ex.Message);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message);
            }
        }


        public static void DecryptFile(string inputFile, string outputFile, string key)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
                    aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                   
                    using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                    using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
                    using (CryptoStream cryptoStream = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
                    {
                        // Skip the IV at the beginning of the file
                        fsInput.Seek(aesAlg.IV.Length, SeekOrigin.Begin);
                        fsInput.CopyTo(cryptoStream);
                    }
                    
                }
                System.Windows.Forms.MessageBox.Show("Successfullly Decryption File");
                
            }
            catch (CryptographicException ex)
            {
                System.Windows.Forms.MessageBox.Show("Croptography : " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error : " + ex.Message);
                return;

            }
        }
    }
}
