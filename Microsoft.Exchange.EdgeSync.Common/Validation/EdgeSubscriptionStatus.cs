using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.MessageSecurity.EdgeSync;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000044 RID: 68
	[Serializable]
	public class EdgeSubscriptionStatus
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00008968 File Offset: 0x00006B68
		public EdgeSubscriptionStatus(string name)
		{
			this.syncStatus = ValidationStatus.Inconclusive;
			this.utcNow = DateTime.UtcNow;
			this.name = name;
			this.leaseType = LeaseTokenType.None;
			this.TransportServerStatus = new EdgeConfigStatus();
			this.TransportConfigStatus = new EdgeConfigStatus();
			this.AcceptedDomainStatus = new EdgeConfigStatus();
			this.RemoteDomainStatus = new EdgeConfigStatus();
			this.SendConnectorStatus = new EdgeConfigStatus();
			this.MessageClassificationStatus = new EdgeConfigStatus();
			this.RecipientStatus = new EdgeConfigStatus();
			this.credentialRecords = new CredentialRecords();
			this.cookieRecords = new CookieRecords();
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000089FE File Offset: 0x00006BFE
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00008A06 File Offset: 0x00006C06
		public ValidationStatus SyncStatus
		{
			get
			{
				return this.syncStatus;
			}
			set
			{
				this.syncStatus = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008A0F File Offset: 0x00006C0F
		public DateTime UtcNow
		{
			get
			{
				return this.utcNow;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008A17 File Offset: 0x00006C17
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008A1F File Offset: 0x00006C1F
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00008A27 File Offset: 0x00006C27
		public string LeaseHolder
		{
			get
			{
				return this.leaseHolder;
			}
			set
			{
				this.leaseHolder = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008A30 File Offset: 0x00006C30
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00008A38 File Offset: 0x00006C38
		public LeaseTokenType LeaseType
		{
			get
			{
				return this.leaseType;
			}
			set
			{
				this.leaseType = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008A41 File Offset: 0x00006C41
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00008A49 File Offset: 0x00006C49
		public string FailureDetail
		{
			get
			{
				return this.failureDetail;
			}
			set
			{
				this.failureDetail = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00008A52 File Offset: 0x00006C52
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00008A5A File Offset: 0x00006C5A
		public DateTime LeaseExpiryUtc
		{
			get
			{
				return this.leaseExpiryUtc;
			}
			set
			{
				this.leaseExpiryUtc = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00008A63 File Offset: 0x00006C63
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00008A6B File Offset: 0x00006C6B
		public DateTime LastSynchronizedUtc
		{
			get
			{
				return this.lastSynchronizedUtc;
			}
			set
			{
				this.lastSynchronizedUtc = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00008A74 File Offset: 0x00006C74
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00008A7C File Offset: 0x00006C7C
		public EdgeConfigStatus TransportServerStatus
		{
			get
			{
				return this.transportServerStatus;
			}
			set
			{
				this.transportServerStatus = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008A85 File Offset: 0x00006C85
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00008A8D File Offset: 0x00006C8D
		public EdgeConfigStatus TransportConfigStatus
		{
			get
			{
				return this.transportConfigStatus;
			}
			set
			{
				this.transportConfigStatus = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008A96 File Offset: 0x00006C96
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00008A9E File Offset: 0x00006C9E
		public EdgeConfigStatus AcceptedDomainStatus
		{
			get
			{
				return this.acceptedDomainStatus;
			}
			set
			{
				this.acceptedDomainStatus = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008AA7 File Offset: 0x00006CA7
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00008AAF File Offset: 0x00006CAF
		public EdgeConfigStatus RemoteDomainStatus
		{
			get
			{
				return this.remoteDomainStatus;
			}
			set
			{
				this.remoteDomainStatus = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008AB8 File Offset: 0x00006CB8
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public EdgeConfigStatus SendConnectorStatus
		{
			get
			{
				return this.sendConnectorStatus;
			}
			set
			{
				this.sendConnectorStatus = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008AC9 File Offset: 0x00006CC9
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00008AD1 File Offset: 0x00006CD1
		public EdgeConfigStatus MessageClassificationStatus
		{
			get
			{
				return this.messageClassificationStatus;
			}
			set
			{
				this.messageClassificationStatus = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008ADA File Offset: 0x00006CDA
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00008AE2 File Offset: 0x00006CE2
		public EdgeConfigStatus RecipientStatus
		{
			get
			{
				return this.recipientStatus;
			}
			set
			{
				this.recipientStatus = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00008AEB File Offset: 0x00006CEB
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00008AF3 File Offset: 0x00006CF3
		public CredentialRecords CredentialRecords
		{
			get
			{
				return this.credentialRecords;
			}
			set
			{
				this.credentialRecords = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00008AFC File Offset: 0x00006CFC
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00008B04 File Offset: 0x00006D04
		public CookieRecords CookieRecords
		{
			get
			{
				return this.cookieRecords;
			}
			set
			{
				this.cookieRecords = value;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008B10 File Offset: 0x00006D10
		public string ToStringForm()
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			stringBuilder.AppendLine("******************************");
			stringBuilder.Append("CurrentTime (UTC):");
			stringBuilder.AppendLine(DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append("Name:");
			stringBuilder.AppendLine(this.name);
			stringBuilder.Append("SyncStatus:");
			stringBuilder.AppendLine(this.syncStatus.ToString());
			stringBuilder.Append("LeaseHolder:");
			stringBuilder.AppendLine(this.leaseHolder);
			stringBuilder.Append("LeaseType:");
			stringBuilder.AppendLine(this.leaseType.ToString());
			stringBuilder.Append("LeaseExpiry (UTC):");
			stringBuilder.AppendLine(this.leaseExpiryUtc.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append("LastSynchronized (UTC):");
			stringBuilder.AppendLine(this.lastSynchronizedUtc.ToString(CultureInfo.InvariantCulture));
			stringBuilder.AppendLine("Cookie Records (" + this.cookieRecords.Records.Count + "):");
			foreach (CookieRecord cookieRecord in this.cookieRecords.Records)
			{
				string value = string.Format(CultureInfo.InvariantCulture, "Domain:{0}; LastUpdated (UTC):{1}; DC:{2}", new object[]
				{
					cookieRecord.BaseDN,
					cookieRecord.LastUpdated.ToString(CultureInfo.InvariantCulture),
					cookieRecord.DomainController
				});
				stringBuilder.AppendLine(value);
			}
			stringBuilder.Append("Failure Details:");
			stringBuilder.AppendLine(this.failureDetail);
			return stringBuilder.ToString();
		}

		// Token: 0x04000120 RID: 288
		private readonly DateTime utcNow;

		// Token: 0x04000121 RID: 289
		private ValidationStatus syncStatus;

		// Token: 0x04000122 RID: 290
		private string name;

		// Token: 0x04000123 RID: 291
		private string leaseHolder;

		// Token: 0x04000124 RID: 292
		private DateTime leaseExpiryUtc;

		// Token: 0x04000125 RID: 293
		private LeaseTokenType leaseType;

		// Token: 0x04000126 RID: 294
		private DateTime lastSynchronizedUtc;

		// Token: 0x04000127 RID: 295
		private string failureDetail;

		// Token: 0x04000128 RID: 296
		private EdgeConfigStatus transportServerStatus;

		// Token: 0x04000129 RID: 297
		private EdgeConfigStatus transportConfigStatus;

		// Token: 0x0400012A RID: 298
		private EdgeConfigStatus acceptedDomainStatus;

		// Token: 0x0400012B RID: 299
		private EdgeConfigStatus remoteDomainStatus;

		// Token: 0x0400012C RID: 300
		private EdgeConfigStatus sendConnectorStatus;

		// Token: 0x0400012D RID: 301
		private EdgeConfigStatus messageClassificationStatus;

		// Token: 0x0400012E RID: 302
		private EdgeConfigStatus recipientStatus;

		// Token: 0x0400012F RID: 303
		private CredentialRecords credentialRecords;

		// Token: 0x04000130 RID: 304
		private CookieRecords cookieRecords;
	}
}
