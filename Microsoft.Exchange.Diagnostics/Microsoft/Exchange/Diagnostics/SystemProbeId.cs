using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001E2 RID: 482
	internal static class SystemProbeId
	{
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00038E6E File Offset: 0x0003706E
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x00038E75 File Offset: 0x00037075
		internal static X509Certificate2 EncryptionCertificate
		{
			get
			{
				return SystemProbeId.testEncryptionCertificate;
			}
			set
			{
				SystemProbeId.testEncryptionCertificate = value;
			}
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00038E80 File Offset: 0x00037080
		public static string EncryptProbeGuid(Guid guid, DateTime timestamp)
		{
			if (guid == Guid.Empty)
			{
				throw new ArgumentException("Guid to encrypt must not be Guid.Empty");
			}
			X509Certificate2 publicCertificate = SystemProbeId.GetPublicCertificate();
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(string.Format("{0}|{1}", guid.ToString(), timestamp.Ticks.ToString()));
			RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)publicCertificate.PublicKey.Key;
			return Convert.ToBase64String(rsacryptoServiceProvider.Encrypt(bytes, false));
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00038F00 File Offset: 0x00037100
		public static void DecryptProbeGuid(string cyphertext, out Guid guid, out DateTime timestamp)
		{
			X509Certificate2 privateCertificate = SystemProbeId.GetPrivateCertificate();
			RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)privateCertificate.PrivateKey;
			byte[] rgb;
			try
			{
				rgb = Convert.FromBase64String(cyphertext);
			}
			catch (ArgumentNullException innerException)
			{
				throw new SystemProbeException(SystemProbeStrings.NullEncryptedData, innerException);
			}
			catch (FormatException innerException2)
			{
				throw new SystemProbeException(SystemProbeStrings.EncryptedDataNotValidBase64String, innerException2);
			}
			string[] array = null;
			try
			{
				byte[] array2 = rsacryptoServiceProvider.Decrypt(rgb, false);
				if (array2 != null)
				{
					UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
					string @string = unicodeEncoding.GetString(array2);
					if (!string.IsNullOrEmpty(@string))
					{
						array = @string.Split("|".ToCharArray());
					}
				}
			}
			catch (CryptographicException innerException3)
			{
				throw new SystemProbeException(SystemProbeStrings.EncryptedDataCannotBeDecrypted, innerException3);
			}
			if (array == null || array.Length != 2)
			{
				throw new SystemProbeException(SystemProbeStrings.InvalidGuidInDecryptedText);
			}
			guid = Guid.Empty;
			timestamp = DateTime.MinValue;
			if (!Guid.TryParse(array[0], out guid))
			{
				throw new SystemProbeException(SystemProbeStrings.InvalidGuidInDecryptedText);
			}
			long ticks = 0L;
			if (!long.TryParse(array[1], out ticks))
			{
				throw new SystemProbeException(SystemProbeStrings.InvalidTimeInDecryptedText);
			}
			timestamp = new DateTime(ticks);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00039028 File Offset: 0x00037228
		internal static X509Certificate2 GetPublicCertificate()
		{
			return SystemProbeId.GetCertificate(StoreName.CertificateAuthority, StoreLocation.LocalMachine);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00039031 File Offset: 0x00037231
		internal static X509Certificate2 GetPrivateCertificate()
		{
			return SystemProbeId.GetCertificate(StoreName.My, StoreLocation.LocalMachine);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0003903C File Offset: 0x0003723C
		private static X509Certificate2 GetCertificate(StoreName storeName, StoreLocation storeLocation)
		{
			X509Certificate2 x509Certificate = SystemProbeId.testEncryptionCertificate;
			if (x509Certificate == null)
			{
				X509Store x509Store = new X509Store(storeName, storeLocation);
				x509Store.Open(OpenFlags.ReadOnly);
				foreach (X509Certificate2 x509Certificate2 in x509Store.Certificates)
				{
					if (x509Certificate2.Subject == "CN=FfoSystemProbe")
					{
						x509Certificate = x509Certificate2;
						break;
					}
				}
				x509Store.Close();
			}
			if (x509Certificate == null)
			{
				throw new SystemProbeException(SystemProbeStrings.CertificateNotFound);
			}
			if (DateTime.UtcNow < x509Certificate.NotBefore || DateTime.UtcNow > x509Certificate.NotAfter)
			{
				throw new SystemProbeException(SystemProbeStrings.CertificateTimeNotValid(x509Certificate.GetEffectiveDateString(), x509Certificate.GetExpirationDateString()));
			}
			if (!SystemProbeId.IsSelfSigned(x509Certificate) && !x509Certificate.Verify())
			{
				throw new SystemProbeException(SystemProbeStrings.CertificateNotSigned);
			}
			return x509Certificate;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000390FF File Offset: 0x000372FF
		private static bool IsSelfSigned(X509Certificate cert)
		{
			return cert.Issuer == cert.Subject;
		}

		// Token: 0x040009FC RID: 2556
		private static X509Certificate2 testEncryptionCertificate;
	}
}
