using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Security.Compliance;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000056 RID: 86
	internal sealed class AzureSasKey : IAzureSasTokenProvider
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000B3C8 File Offset: 0x000095C8
		public AzureSasKey(string keyName, SecureString keyValue, AzureSasKey.ClaimType? claims = null)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("keyName", keyName);
			ArgumentValidator.ThrowIfNull("keyValue", keyValue);
			ArgumentValidator.ThrowIfZeroOrNegative("keyValue.Length", keyValue.Length);
			this.KeyName = keyName;
			this.KeyValue = keyValue;
			this.Claims = ((claims != null) ? claims.Value : AzureSasKey.ClaimType.None);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000B428 File Offset: 0x00009628
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000B430 File Offset: 0x00009630
		public string KeyName { get; private set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000B439 File Offset: 0x00009639
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000B441 File Offset: 0x00009641
		public SecureString KeyValue { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000B44A File Offset: 0x0000964A
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000B452 File Offset: 0x00009652
		public AzureSasKey.ClaimType Claims { get; private set; }

		// Token: 0x06000347 RID: 839 RVA: 0x0000B45C File Offset: 0x0000965C
		public static AzureSasKey GenerateRandomKey(AzureSasKey.ClaimType claims, string keyName = null)
		{
			byte[] array = new byte[32];
			AzureSasKey.RandomGenerator.GetBytes(array);
			return new AzureSasKey(keyName ?? "TenantSasKey", Convert.ToBase64String(array).ConvertToSecureString(), new AzureSasKey.ClaimType?(claims));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000B49C File Offset: 0x0000969C
		public AzureSasToken CreateSasToken(string resourceUri)
		{
			return this.CreateSasToken(resourceUri, 300);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public AzureSasToken CreateSasToken(string resourceUri, int expirationInSeconds)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("resourceUri", resourceUri);
			ArgumentValidator.ThrowIfInvalidValue<string>("resourceUri", resourceUri, (string x) => Uri.IsWellFormedUriString(resourceUri, UriKind.Absolute));
			ArgumentValidator.ThrowIfZeroOrNegative("expirationInSeconds", expirationInSeconds);
			string text = Convert.ToString((int)(ExDateTime.UtcNow - Constants.EpochBaseTime).TotalSeconds + expirationInSeconds);
			string arg = HttpUtility.UrlEncode(resourceUri);
			string s = string.Format("{0}\n{1}", arg, text);
			AzureSasToken result;
			using (HMACSHA256Cng hmacsha256Cng = new HMACSHA256Cng(Encoding.UTF8.GetBytes(this.KeyValue.AsUnsecureString())))
			{
				string signature = Convert.ToBase64String(hmacsha256Cng.ComputeHash(Encoding.UTF8.GetBytes(s)));
				result = new AzureSasToken(resourceUri, text, signature, this.KeyName, null);
			}
			return result;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public AzureSasKey ChangeClaims(AzureSasKey.ClaimType claims)
		{
			return new AzureSasKey(this.KeyName, this.KeyValue, new AzureSasKey.ClaimType?(claims));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000B5D9 File Offset: 0x000097D9
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("{0} [{1}]", this.KeyName, this.Claims);
			}
			return this.toString;
		}

		// Token: 0x0400015F RID: 351
		public const string RandomSasKeyDefaultName = "TenantSasKey";

		// Token: 0x04000160 RID: 352
		public const int DefaultTokenExpirationInSeconds = 300;

		// Token: 0x04000161 RID: 353
		private const int SasKeySizeInBytes = 32;

		// Token: 0x04000162 RID: 354
		private static readonly RandomNumberGenerator RandomGenerator = RandomNumberGenerator.Create();

		// Token: 0x04000163 RID: 355
		private string toString;

		// Token: 0x02000057 RID: 87
		[Flags]
		public enum ClaimType
		{
			// Token: 0x04000168 RID: 360
			None = 0,
			// Token: 0x04000169 RID: 361
			Send = 1,
			// Token: 0x0400016A RID: 362
			Listen = 2,
			// Token: 0x0400016B RID: 363
			Manage = 4
		}
	}
}
