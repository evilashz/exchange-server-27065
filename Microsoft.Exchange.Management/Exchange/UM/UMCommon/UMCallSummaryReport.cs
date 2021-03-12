using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000D5C RID: 3420
	[Serializable]
	public class UMCallSummaryReport : UMCallReportBase
	{
		// Token: 0x0600831C RID: 33564 RVA: 0x00217EDE File Offset: 0x002160DE
		public UMCallSummaryReport(ObjectId identity) : base(identity)
		{
		}

		// Token: 0x170028C9 RID: 10441
		// (get) Token: 0x0600831D RID: 33565 RVA: 0x00217EE7 File Offset: 0x002160E7
		// (set) Token: 0x0600831E RID: 33566 RVA: 0x00217EF9 File Offset: 0x002160F9
		public ulong AutoAttendant
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.AutoAttendant];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.AutoAttendant] = value;
			}
		}

		// Token: 0x170028CA RID: 10442
		// (get) Token: 0x0600831F RID: 33567 RVA: 0x00217F0C File Offset: 0x0021610C
		// (set) Token: 0x06008320 RID: 33568 RVA: 0x00217F1E File Offset: 0x0021611E
		public ulong FailedOrRejectedCalls
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.FailedOrRejectedCalls];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.FailedOrRejectedCalls] = value;
			}
		}

		// Token: 0x170028CB RID: 10443
		// (get) Token: 0x06008321 RID: 33569 RVA: 0x00217F31 File Offset: 0x00216131
		// (set) Token: 0x06008322 RID: 33570 RVA: 0x00217F43 File Offset: 0x00216143
		public ulong Fax
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.Fax];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.Fax] = value;
			}
		}

		// Token: 0x170028CC RID: 10444
		// (get) Token: 0x06008323 RID: 33571 RVA: 0x00217F56 File Offset: 0x00216156
		// (set) Token: 0x06008324 RID: 33572 RVA: 0x00217F68 File Offset: 0x00216168
		public ulong MissedCalls
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.MissedCalls];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.MissedCalls] = value;
			}
		}

		// Token: 0x170028CD RID: 10445
		// (get) Token: 0x06008325 RID: 33573 RVA: 0x00217F7B File Offset: 0x0021617B
		// (set) Token: 0x06008326 RID: 33574 RVA: 0x00217F8D File Offset: 0x0021618D
		public ulong OtherCalls
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.OtherCalls];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.OtherCalls] = value;
			}
		}

		// Token: 0x170028CE RID: 10446
		// (get) Token: 0x06008327 RID: 33575 RVA: 0x00217FA0 File Offset: 0x002161A0
		// (set) Token: 0x06008328 RID: 33576 RVA: 0x00217FB2 File Offset: 0x002161B2
		public ulong Outbound
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.Outbound];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.Outbound] = value;
			}
		}

		// Token: 0x170028CF RID: 10447
		// (get) Token: 0x06008329 RID: 33577 RVA: 0x00217FC5 File Offset: 0x002161C5
		// (set) Token: 0x0600832A RID: 33578 RVA: 0x00217FD7 File Offset: 0x002161D7
		public ulong SubscriberAccess
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.SubscriberAccess];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.SubscriberAccess] = value;
			}
		}

		// Token: 0x170028D0 RID: 10448
		// (get) Token: 0x0600832B RID: 33579 RVA: 0x00217FEA File Offset: 0x002161EA
		// (set) Token: 0x0600832C RID: 33580 RVA: 0x00217FFC File Offset: 0x002161FC
		public ulong VoiceMessages
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.VoiceMessages];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.VoiceMessages] = value;
			}
		}

		// Token: 0x170028D1 RID: 10449
		// (get) Token: 0x0600832D RID: 33581 RVA: 0x0021800F File Offset: 0x0021620F
		// (set) Token: 0x0600832E RID: 33582 RVA: 0x00218021 File Offset: 0x00216221
		public ulong TotalCalls
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.TotalCalls];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.TotalCalls] = value;
			}
		}

		// Token: 0x170028D2 RID: 10450
		// (get) Token: 0x0600832F RID: 33583 RVA: 0x00218034 File Offset: 0x00216234
		// (set) Token: 0x06008330 RID: 33584 RVA: 0x00218046 File Offset: 0x00216246
		public string Date
		{
			get
			{
				return (string)this[UMCallSummaryReportSchema.Date];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.Date] = value;
			}
		}

		// Token: 0x170028D3 RID: 10451
		// (get) Token: 0x06008331 RID: 33585 RVA: 0x00218054 File Offset: 0x00216254
		// (set) Token: 0x06008332 RID: 33586 RVA: 0x00218066 File Offset: 0x00216266
		public ulong TotalAudioQualityCallsSampled
		{
			get
			{
				return (ulong)this[UMCallSummaryReportSchema.TotalAudioQualityCallsSampled];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.TotalAudioQualityCallsSampled] = value;
			}
		}

		// Token: 0x170028D4 RID: 10452
		// (get) Token: 0x06008333 RID: 33587 RVA: 0x00218079 File Offset: 0x00216279
		// (set) Token: 0x06008334 RID: 33588 RVA: 0x0021808B File Offset: 0x0021628B
		public string UMDialPlanName
		{
			get
			{
				return (string)this[UMCallSummaryReportSchema.UMDialPlanName];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.UMDialPlanName] = value;
			}
		}

		// Token: 0x170028D5 RID: 10453
		// (get) Token: 0x06008335 RID: 33589 RVA: 0x00218099 File Offset: 0x00216299
		// (set) Token: 0x06008336 RID: 33590 RVA: 0x002180AB File Offset: 0x002162AB
		public string UMIPGatewayName
		{
			get
			{
				return (string)this[UMCallSummaryReportSchema.UMIPGatewayName];
			}
			internal set
			{
				this[UMCallSummaryReportSchema.UMIPGatewayName] = value;
			}
		}

		// Token: 0x170028D6 RID: 10454
		// (get) Token: 0x06008337 RID: 33591 RVA: 0x002180B9 File Offset: 0x002162B9
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return UMCallSummaryReport.schema;
			}
		}

		// Token: 0x04003FAB RID: 16299
		private static UMCallSummaryReportSchema schema = ObjectSchema.GetInstance<UMCallSummaryReportSchema>();
	}
}
