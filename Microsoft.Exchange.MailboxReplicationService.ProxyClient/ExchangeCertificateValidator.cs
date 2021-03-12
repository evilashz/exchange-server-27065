using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000005 RID: 5
	internal static class ExchangeCertificateValidator
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002684 File Offset: 0x00000884
		public static void Initialize()
		{
			if (ExchangeCertificateValidator.isInitialized)
			{
				return;
			}
			lock (ExchangeCertificateValidator.locker)
			{
				if (!ExchangeCertificateValidator.isInitialized)
				{
					using (RegistryKey registryKey = ExchangeCertificateValidator.SafeOpenKey())
					{
						if (registryKey != null)
						{
							object value = registryKey.GetValue("AllowInternalUntrustedCerts");
							if (value is int)
							{
								ExchangeCertificateValidator.allowInternalUntrustedCerts = ((int)value != 0);
							}
							value = registryKey.GetValue("AllowCertificateNameMismatchForMRS");
							if (value is string[])
							{
								string[] values = (string[])value;
								ExchangeCertificateValidator.AddToList(ExchangeCertificateValidator.TrustedSubjects, values);
							}
						}
					}
					string config = ConfigBase<MRSConfigSchema>.GetConfig<string>("ProxyClientTrustedCertificateThumbprints");
					if (!string.IsNullOrWhiteSpace(config))
					{
						ExchangeCertificateValidator.AddToList(ExchangeCertificateValidator.TrustedThumbprints, config.Split(new char[]
						{
							','
						}));
					}
					if (ExchangeCertificateValidator.allowInternalUntrustedCerts || ExchangeCertificateValidator.TrustedSubjects.Count > 0 || ExchangeCertificateValidator.TrustedThumbprints.Count > 0)
					{
						ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ExchangeCertificateValidator.CertificateValidatorCallback);
					}
					else
					{
						ServicePointManager.ServerCertificateValidationCallback = null;
					}
					ExchangeCertificateValidator.isInitialized = true;
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000027B4 File Offset: 0x000009B4
		public static bool CertificateValidatorCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if (ExchangeCertificateValidator.allowInternalUntrustedCerts)
			{
				return true;
			}
			if (certificate != null)
			{
				string item = certificate.Subject.ToLower(CultureInfo.InvariantCulture);
				if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch && ExchangeCertificateValidator.TrustedSubjects.Contains(item))
				{
					return true;
				}
				string certHashString = certificate.GetCertHashString();
				if (ExchangeCertificateValidator.TrustedThumbprints.Contains(certHashString.ToLower()))
				{
					return true;
				}
			}
			MrsTracer.ProxyClient.Debug("ExchangeCertificateValidator: PolicyErrors={0}, Subject={1}, Thumbprint={2}", new object[]
			{
				sslPolicyErrors,
				(certificate != null) ? certificate.Subject : "null",
				(certificate != null) ? certificate.GetCertHashString() : "null"
			});
			return false;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002858 File Offset: 0x00000A58
		private static RegistryKey SafeOpenKey()
		{
			RegistryKey result = null;
			try
			{
				result = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA", false);
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000028A0 File Offset: 0x00000AA0
		private static void AddToList(HashSet<string> list, string[] values)
		{
			foreach (string text in values)
			{
				string text2 = text.ToLower(CultureInfo.InvariantCulture).Trim();
				if (!string.IsNullOrEmpty(text2) && !list.Contains(text2))
				{
					list.Add(text2);
				}
			}
		}

		// Token: 0x0400000D RID: 13
		private const string ContainerPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA";

		// Token: 0x0400000E RID: 14
		private const string KeyName = "AllowInternalUntrustedCerts";

		// Token: 0x0400000F RID: 15
		private const string AllowCertificateNameMismatchKeyName = "AllowCertificateNameMismatchForMRS";

		// Token: 0x04000010 RID: 16
		private static readonly object locker = new object();

		// Token: 0x04000011 RID: 17
		private static readonly HashSet<string> TrustedSubjects = new HashSet<string>();

		// Token: 0x04000012 RID: 18
		private static readonly HashSet<string> TrustedThumbprints = new HashSet<string>();

		// Token: 0x04000013 RID: 19
		private static bool isInitialized = false;

		// Token: 0x04000014 RID: 20
		private static bool allowInternalUntrustedCerts = false;
	}
}
