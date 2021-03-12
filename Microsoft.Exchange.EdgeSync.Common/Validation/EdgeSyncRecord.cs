using System;
using System.Text;
using Microsoft.Exchange.EdgeSync.Common.Internal;
using Microsoft.Exchange.MessageSecurity.EdgeSync;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	public sealed class EdgeSyncRecord
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00008CF0 File Offset: 0x00006EF0
		private EdgeSyncRecord(string service, string context, ValidationStatus status, string detail, LeaseToken leaseToken, Cookie cookie, string additionalInfo)
		{
			this.status = status;
			this.detail = detail;
			this.alertTime = leaseToken.AlertTime;
			this.leaseHolder = leaseToken.Path;
			this.leaseType = leaseToken.Type;
			this.leaseExpiry = leaseToken.Expiry;
			this.lastSynchronized = leaseToken.LastSync;
			this.cookie = cookie;
			this.now = DateTime.UtcNow;
			this.additionalInfo = (string.IsNullOrEmpty(additionalInfo) ? "N/A" : additionalInfo);
			this.service = service;
			this.context = context;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008D8C File Offset: 0x00006F8C
		public string AdditionalInfo
		{
			get
			{
				return this.additionalInfo;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00008D94 File Offset: 0x00006F94
		public string Detail
		{
			get
			{
				return this.detail;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008D9C File Offset: 0x00006F9C
		public DateTime CurrentTime
		{
			get
			{
				return this.now;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00008DA4 File Offset: 0x00006FA4
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00008DAC File Offset: 0x00006FAC
		public ValidationStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public string StatusSummary
		{
			get
			{
				string result = string.Empty;
				switch (this.status)
				{
				case ValidationStatus.NoSyncConfigured:
					result = Strings.EdgeSyncNotConfigured(this.service);
					break;
				case ValidationStatus.Normal:
					result = Strings.EdgeSyncNormal(this.service);
					break;
				case ValidationStatus.Warning:
					result = Strings.EdgeSyncAbnormal(this.service, this.context);
					break;
				case ValidationStatus.Failed:
					result = Strings.EdgeSyncFailed(this.service, this.context);
					break;
				case ValidationStatus.Inconclusive:
					result = Strings.EdgeSyncInconclusive(this.service, this.context);
					break;
				case ValidationStatus.FailedUrgent:
					result = Strings.EdgeSyncFailedUrgent(this.service, this.context);
					break;
				}
				return result;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008E7E File Offset: 0x0000707E
		public string LeaseHolder
		{
			get
			{
				return this.leaseHolder;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00008E86 File Offset: 0x00007086
		public LeaseTokenType LeaseType
		{
			get
			{
				return this.leaseType;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00008E8E File Offset: 0x0000708E
		public DateTime LeaseExpiry
		{
			get
			{
				return this.leaseExpiry;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00008E96 File Offset: 0x00007096
		public DateTime AlertTime
		{
			get
			{
				return this.alertTime;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008E9E File Offset: 0x0000709E
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00008EA6 File Offset: 0x000070A6
		public DateTime LastSynchronized
		{
			get
			{
				return this.lastSynchronized;
			}
			set
			{
				this.lastSynchronized = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008EAF File Offset: 0x000070AF
		public string CookieDomainController
		{
			get
			{
				if (this.cookie == null)
				{
					return string.Empty;
				}
				return this.cookie.DomainController;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008ECA File Offset: 0x000070CA
		public DateTime CookieLastUpdated
		{
			get
			{
				if (this.cookie == null)
				{
					return DateTime.MinValue;
				}
				return this.cookie.LastUpdated;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00008EE5 File Offset: 0x000070E5
		public string CookieBaseDN
		{
			get
			{
				if (this.cookie == null)
				{
					return string.Empty;
				}
				return this.cookie.BaseDN;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008F00 File Offset: 0x00007100
		public int CookieLength
		{
			get
			{
				if (this.cookie == null || this.cookie.CookieValue == null)
				{
					return 0;
				}
				return this.cookie.CookieValue.Length;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008F26 File Offset: 0x00007126
		public static EdgeSyncRecord GetEdgeSyncConnectorNotConfiguredForEntireForestRecord(string service)
		{
			return new EdgeSyncRecord(service, null, ValidationStatus.NoSyncConfigured, "No EdgeSync connector has been configured or enabled for the entire forest", LeaseToken.Empty, null, null);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008F3C File Offset: 0x0000713C
		public static EdgeSyncRecord GetEdgeSyncConnectorNotConfiguredForCurrentSiteRecord(string service, string siteName)
		{
			return new EdgeSyncRecord(service, null, ValidationStatus.Normal, "No EdgeSync connector has been configured or enabled for the current site " + siteName, LeaseToken.Empty, null, null);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008F58 File Offset: 0x00007158
		public static EdgeSyncRecord GetEdgeSyncServiceNotConfiguredForCurrentSiteRecord(string service, string siteName)
		{
			return new EdgeSyncRecord(service, null, ValidationStatus.NoSyncConfigured, "No EdgeSync service config has been configured for the current site " + siteName, LeaseToken.Empty, null, null);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008F74 File Offset: 0x00007174
		public static EdgeSyncRecord GetNormalRecord(string service, string detail, LeaseToken leaseToken, Cookie cookie, string additionalInfo)
		{
			return new EdgeSyncRecord(service, null, ValidationStatus.Normal, detail, leaseToken, cookie, additionalInfo);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008F83 File Offset: 0x00007183
		public static EdgeSyncRecord GetFailedRecord(string service, string context, string detail, LeaseToken leaseToken, Cookie cookie, string additionalInfo)
		{
			return EdgeSyncRecord.GetFailedRecord(service, context, detail, leaseToken, cookie, additionalInfo, false);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008F93 File Offset: 0x00007193
		public static EdgeSyncRecord GetFailedRecord(string service, string context, string detail, LeaseToken leaseToken, Cookie cookie, string additionalInfo, bool isUrgent)
		{
			return new EdgeSyncRecord(service, context, isUrgent ? ValidationStatus.FailedUrgent : ValidationStatus.Failed, detail, leaseToken, cookie, additionalInfo);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008FAA File Offset: 0x000071AA
		public static EdgeSyncRecord GetInconclusiveRecord(string service, string context, string detail, LeaseToken leaseToken, Cookie cookie, string additionalInfo)
		{
			return new EdgeSyncRecord(service, context, ValidationStatus.Inconclusive, detail, leaseToken, cookie, additionalInfo);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008FBA File Offset: 0x000071BA
		public static EdgeSyncRecord GetWarningRecord(string service, string context, string detail, LeaseToken leaseToken, Cookie cookie)
		{
			return new EdgeSyncRecord(service, context, ValidationStatus.Warning, detail, leaseToken, cookie, null);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008FCC File Offset: 0x000071CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CurrentTime:");
			stringBuilder.AppendLine(this.now.ToString());
			stringBuilder.Append("Status:");
			stringBuilder.AppendLine(this.status.ToString());
			stringBuilder.Append("StatusSummary:");
			stringBuilder.AppendLine(this.StatusSummary);
			stringBuilder.Append("Detail:");
			stringBuilder.AppendLine(this.detail);
			stringBuilder.Append("LeaseHolder:");
			stringBuilder.AppendLine(this.leaseHolder);
			stringBuilder.Append("LeaseType:");
			stringBuilder.AppendLine(this.leaseType.ToString());
			stringBuilder.Append("LeaseExpiry:");
			stringBuilder.AppendLine(this.leaseExpiry.ToString());
			stringBuilder.Append("LastSynchronized:");
			stringBuilder.AppendLine(this.lastSynchronized.ToString());
			stringBuilder.Append("CookieBaseDN:");
			stringBuilder.AppendLine(this.CookieBaseDN);
			stringBuilder.Append("CookieDomainController:");
			stringBuilder.AppendLine(this.CookieDomainController);
			stringBuilder.Append("CookieLastUpdated:");
			stringBuilder.AppendLine(this.CookieLastUpdated.ToString());
			stringBuilder.Append("CookieLength:");
			stringBuilder.AppendLine(this.CookieLength.ToString());
			stringBuilder.Append("AdditionalInfo:");
			stringBuilder.AppendLine(this.additionalInfo.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x04000131 RID: 305
		private string additionalInfo;

		// Token: 0x04000132 RID: 306
		private string detail;

		// Token: 0x04000133 RID: 307
		private ValidationStatus status;

		// Token: 0x04000134 RID: 308
		private string leaseHolder;

		// Token: 0x04000135 RID: 309
		private DateTime leaseExpiry;

		// Token: 0x04000136 RID: 310
		private DateTime alertTime;

		// Token: 0x04000137 RID: 311
		private LeaseTokenType leaseType;

		// Token: 0x04000138 RID: 312
		private DateTime lastSynchronized;

		// Token: 0x04000139 RID: 313
		private DateTime now;

		// Token: 0x0400013A RID: 314
		private Cookie cookie;

		// Token: 0x0400013B RID: 315
		private string service;

		// Token: 0x0400013C RID: 316
		private string context;
	}
}
