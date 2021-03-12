using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Web;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000058 RID: 88
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal sealed class AzureSasToken : IAzureSasTokenProvider
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0000B630 File Offset: 0x00009830
		public AzureSasToken(string resourceUri, string expirationInSeconds, string signature, string keyName, string targetResourceUri = null)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("resourceUri", resourceUri);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("expirationInSeconds", expirationInSeconds);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("signature", signature);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("keyName", keyName);
			ArgumentValidator.ThrowIfInvalidValue<string>("resourceUri", resourceUri, (string x) => Uri.IsWellFormedUriString(x, UriKind.Absolute));
			ArgumentValidator.ThrowIfInvalidValue<string>("expirationInSeconds", expirationInSeconds, (string x) => double.TryParse(x, out this.expirationValue));
			this.ResourceUri = resourceUri;
			this.ExpirationInSeconds = expirationInSeconds;
			this.Signature = signature;
			this.KeyName = keyName;
			this.targetResourceUri = targetResourceUri;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000B6DB File Offset: 0x000098DB
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000B6E3 File Offset: 0x000098E3
		[DataMember(Name = "sr", IsRequired = true)]
		public string ResourceUri { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000B6EC File Offset: 0x000098EC
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000B6F4 File Offset: 0x000098F4
		[DataMember(Name = "se", IsRequired = true)]
		public string ExpirationInSeconds { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000B6FD File Offset: 0x000098FD
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000B705 File Offset: 0x00009905
		[DataMember(Name = "sig", IsRequired = true)]
		public string Signature { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000B70E File Offset: 0x0000990E
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000B716 File Offset: 0x00009916
		[DataMember(Name = "skn", IsRequired = true)]
		public string KeyName { get; private set; }

		// Token: 0x06000356 RID: 854 RVA: 0x0000B720 File Offset: 0x00009920
		public bool IsExpired()
		{
			if (this.expirationValue == 0.0)
			{
				double.TryParse(this.ExpirationInSeconds, out this.expirationValue);
			}
			return Constants.EpochBaseTime.AddSeconds(this.expirationValue) < ExDateTime.UtcNow;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000B76D File Offset: 0x0000996D
		public bool IsValid()
		{
			return (this.targetResourceUri == null || this.targetResourceUri.IndexOf(this.ResourceUri) == 0) && !this.IsExpired();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B7A0 File Offset: 0x000099A0
		public AzureSasToken CreateSasToken(string resourceUri)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("resourceUri", resourceUri);
			ArgumentValidator.ThrowIfInvalidValue<string>("resourceUri", resourceUri, (string x) => Uri.IsWellFormedUriString(x, UriKind.Absolute));
			return new AzureSasToken(this.ResourceUri, this.ExpirationInSeconds, this.Signature, this.KeyName, resourceUri);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000B7FE File Offset: 0x000099FE
		public AzureSasToken CreateSasToken(string resourceUri, int expirationInSeconds)
		{
			throw new NotImplementedException("CreateSasToken(string, int) is not supported by the AzureSasToken.");
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B80A File Offset: 0x00009A0A
		public string ToJson()
		{
			return JsonConverter.Serialize<AzureSasToken>(this, null);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000B814 File Offset: 0x00009A14
		public string ToAzureAuthorizationString()
		{
			if (this.sasToken == null)
			{
				this.sasToken = string.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}", new object[]
				{
					HttpUtility.UrlEncode(this.ResourceUri),
					HttpUtility.UrlEncode(this.Signature),
					this.ExpirationInSeconds,
					this.KeyName
				});
			}
			return this.sasToken;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000B87C File Offset: 0x00009A7C
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("{{ sr:{0}; se:{1}; skn:{2}; str:{3}; exp:{4}; val:{5}}}", new object[]
				{
					this.ResourceUri,
					this.ExpirationInSeconds,
					this.KeyName,
					this.targetResourceUri,
					this.IsExpired(),
					this.IsValid()
				});
			}
			return this.toString;
		}

		// Token: 0x0400016C RID: 364
		private readonly string targetResourceUri;

		// Token: 0x0400016D RID: 365
		private string sasToken;

		// Token: 0x0400016E RID: 366
		private double expirationValue;

		// Token: 0x0400016F RID: 367
		private string toString;
	}
}
