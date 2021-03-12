using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000CC RID: 204
	internal class MwiMessage
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x0001A188 File Offset: 0x00018388
		internal MwiMessage(Guid mailboxGuid, Guid dialPlanGuid, string userName, string userExtension, int unreadVoicemailCount, int totalVoicemailCount, TimeSpan timeToLive, ExDateTime eventTimeUtc, Guid tenantGuid)
		{
			this.mailboxGuid = mailboxGuid;
			this.dialPlanGuid = dialPlanGuid;
			this.userName = (string.IsNullOrEmpty(userName) ? string.Empty : userName);
			this.userExtension = (string.IsNullOrEmpty(userExtension) ? string.Empty : userExtension);
			this.unreadVoicemailCount = unreadVoicemailCount;
			this.totalVoicemailCount = totalVoicemailCount;
			this.expirationTimeUtc = ExDateTime.UtcNow.Add(timeToLive);
			this.eventTimeUtc = eventTimeUtc;
			this.sentTimeUtc = eventTimeUtc;
			this.deliveryErrors = new List<MwiDeliveryException>();
			this.tenantGuid = tenantGuid;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001A21F File Offset: 0x0001841F
		internal Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001A227 File Offset: 0x00018427
		internal Guid DialPlanGuid
		{
			get
			{
				return this.dialPlanGuid;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001A22F File Offset: 0x0001842F
		internal string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001A237 File Offset: 0x00018437
		internal string UserExtension
		{
			get
			{
				return this.userExtension;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001A23F File Offset: 0x0001843F
		internal int UnreadVoicemailCount
		{
			get
			{
				return this.unreadVoicemailCount;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001A247 File Offset: 0x00018447
		internal int TotalVoicemailCount
		{
			get
			{
				return this.totalVoicemailCount;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001A24F File Offset: 0x0001844F
		internal int NumberOfTargetsAttempted
		{
			get
			{
				return this.numberOfTargetsAttempted;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001A257 File Offset: 0x00018457
		internal bool Expired
		{
			get
			{
				return ExDateTime.Compare(ExDateTime.UtcNow, this.expirationTimeUtc) >= 0;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001A26F File Offset: 0x0001846F
		internal ExDateTime EventTimeUtc
		{
			get
			{
				return this.eventTimeUtc;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001A277 File Offset: 0x00018477
		internal ExDateTime SentTimeUtc
		{
			get
			{
				return this.sentTimeUtc;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001A280 File Offset: 0x00018480
		internal string MailboxDisplayName
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}({1})", new object[]
				{
					this.UserName,
					this.MailboxGuid
				});
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001A2BB File Offset: 0x000184BB
		internal List<MwiDeliveryException> DeliveryErrors
		{
			get
			{
				return this.deliveryErrors;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001A2C3 File Offset: 0x000184C3
		internal SendMessageCompletedDelegate CompletionCallback
		{
			get
			{
				return this.completionCallback;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001A2CB File Offset: 0x000184CB
		internal IMwiTarget CurrentTarget
		{
			get
			{
				return this.currentTarget;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001A2D3 File Offset: 0x000184D3
		internal Guid TenantGuid
		{
			get
			{
				return this.tenantGuid;
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001A2DC File Offset: 0x000184DC
		public override string ToString()
		{
			string text = "MbxGuid:{0}, DPGuid:{1}, Name:{2}, Ext:{3}, UnreadVM:{4}, ";
			text += "TotalVM:{5}, Target:{6}, Expires:{7}, Expired:{8}, EventTime:{9}, SentTime:{10}, TenantGuid: {11}";
			return string.Format(CultureInfo.InvariantCulture, text, new object[]
			{
				this.MailboxGuid,
				this.DialPlanGuid,
				this.UserName,
				this.UserExtension,
				this.UnreadVoicemailCount,
				this.TotalVoicemailCount,
				(this.CurrentTarget != null) ? this.CurrentTarget.Name : "null",
				this.expirationTimeUtc,
				this.Expired,
				this.EventTimeUtc,
				this.SentTimeUtc,
				this.TenantGuid
			});
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001A3C0 File Offset: 0x000185C0
		internal void SendAsync(IMwiTarget target, SendMessageCompletedDelegate completionCallback)
		{
			this.currentTarget = target;
			this.completionCallback = completionCallback;
			CallIdTracer.TraceDebug(ExTraceGlobals.MWITracer, this.GetHashCode(), "MwiMessage.SendAsync: Message={0}", new object[]
			{
				this
			});
			this.sentTimeUtc = ExDateTime.UtcNow;
			this.numberOfTargetsAttempted++;
			target.SendMessageAsync(this);
		}

		// Token: 0x040003E9 RID: 1001
		private readonly Guid tenantGuid;

		// Token: 0x040003EA RID: 1002
		private Guid mailboxGuid;

		// Token: 0x040003EB RID: 1003
		private Guid dialPlanGuid;

		// Token: 0x040003EC RID: 1004
		private string userName;

		// Token: 0x040003ED RID: 1005
		private string userExtension;

		// Token: 0x040003EE RID: 1006
		private int unreadVoicemailCount;

		// Token: 0x040003EF RID: 1007
		private int totalVoicemailCount;

		// Token: 0x040003F0 RID: 1008
		private int numberOfTargetsAttempted;

		// Token: 0x040003F1 RID: 1009
		private List<MwiDeliveryException> deliveryErrors;

		// Token: 0x040003F2 RID: 1010
		private SendMessageCompletedDelegate completionCallback;

		// Token: 0x040003F3 RID: 1011
		private IMwiTarget currentTarget;

		// Token: 0x040003F4 RID: 1012
		private ExDateTime expirationTimeUtc;

		// Token: 0x040003F5 RID: 1013
		private ExDateTime eventTimeUtc;

		// Token: 0x040003F6 RID: 1014
		private ExDateTime sentTimeUtc;
	}
}
