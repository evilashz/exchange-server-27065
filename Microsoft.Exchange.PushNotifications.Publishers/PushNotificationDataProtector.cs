using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Exchange.Security.Dkm;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200000C RID: 12
	internal class PushNotificationDataProtector
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000029F9 File Offset: 0x00000BF9
		public PushNotificationDataProtector(IExchangeGroupKey dkm = null)
		{
			this.Dkm = (dkm ?? new ExchangeGroupKey(null, "Microsoft Exchange DKM"));
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002A17 File Offset: 0x00000C17
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002A1F File Offset: 0x00000C1F
		private IExchangeGroupKey Dkm { get; set; }

		// Token: 0x0600004C RID: 76 RVA: 0x00002A28 File Offset: 0x00000C28
		public SecureString Decrypt(string encryptedText)
		{
			if (string.IsNullOrEmpty(encryptedText))
			{
				return null;
			}
			SecureString result;
			try
			{
				result = this.Dkm.EncryptedStringToSecureString(encryptedText);
			}
			catch (Exception ex)
			{
				if (this.ShouldWrapException(ex))
				{
					throw new PushNotificationConfigurationException(Strings.DataProtectionDecryptingError(encryptedText, ex.Message), ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A80 File Offset: 0x00000C80
		public string Encrypt(string clearText)
		{
			if (string.IsNullOrEmpty(clearText))
			{
				return null;
			}
			string result;
			try
			{
				result = this.Dkm.ClearStringToEncryptedString(clearText);
			}
			catch (Exception ex)
			{
				if (this.ShouldWrapException(ex))
				{
					throw new PushNotificationConfigurationException(Strings.DataProtectionEncryptingError(ex.Message), ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AD8 File Offset: 0x00000CD8
		private bool ShouldWrapException(Exception ex)
		{
			return ex is CryptographicException || ex is InvalidDataException || ex is FormatException || this.Dkm.IsDkmException(ex);
		}

		// Token: 0x0400001A RID: 26
		public static readonly PushNotificationDataProtector Default = new PushNotificationDataProtector(new ExchangeGroupKey(null, "Microsoft Exchange DKM"));
	}
}
