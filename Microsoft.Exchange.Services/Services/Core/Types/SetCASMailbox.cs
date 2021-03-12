using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000A99 RID: 2713
	[KnownType(typeof(CASMailbox))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCASMailbox : OptionsPropertyChangeTracker
	{
		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x0010668A File Offset: 0x0010488A
		// (set) Token: 0x06004CA0 RID: 19616 RVA: 0x00106692 File Offset: 0x00104892
		[DataMember]
		public bool ActiveSyncDebugLogging
		{
			get
			{
				return this.activeSyncDebugLogging;
			}
			set
			{
				this.activeSyncDebugLogging = value;
				base.TrackPropertyChanged("ActiveSyncDebugLogging");
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x001066A6 File Offset: 0x001048A6
		// (set) Token: 0x06004CA2 RID: 19618 RVA: 0x001066AE File Offset: 0x001048AE
		[DataMember]
		public bool ImapForceICalForCalendarRetrievalOption
		{
			get
			{
				return this.imapForceICalForCalendarRetrievalOption;
			}
			set
			{
				this.imapForceICalForCalendarRetrievalOption = value;
				base.TrackPropertyChanged("ImapForceICalForCalendarRetrievalOption");
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x001066C2 File Offset: 0x001048C2
		// (set) Token: 0x06004CA4 RID: 19620 RVA: 0x001066CA File Offset: 0x001048CA
		[DataMember]
		public bool ImapSuppressReadReceipt
		{
			get
			{
				return this.imapSuppressReadReceipt;
			}
			set
			{
				this.imapSuppressReadReceipt = value;
				base.TrackPropertyChanged("ImapSuppressReadReceipt");
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x001066DE File Offset: 0x001048DE
		// (set) Token: 0x06004CA6 RID: 19622 RVA: 0x001066E6 File Offset: 0x001048E6
		[DataMember]
		public bool PopForceICalForCalendarRetrievalOption
		{
			get
			{
				return this.popForceICalForCalendarRetrievalOption;
			}
			set
			{
				this.popForceICalForCalendarRetrievalOption = value;
				base.TrackPropertyChanged("PopForceICalForCalendarRetrievalOption");
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06004CA7 RID: 19623 RVA: 0x001066FA File Offset: 0x001048FA
		// (set) Token: 0x06004CA8 RID: 19624 RVA: 0x00106702 File Offset: 0x00104902
		[DataMember]
		public bool PopSuppressReadReceipt
		{
			get
			{
				return this.popSuppressReadReceipt;
			}
			set
			{
				this.popSuppressReadReceipt = value;
				base.TrackPropertyChanged("PopSuppressReadReceipt");
			}
		}

		// Token: 0x04002B55 RID: 11093
		private bool activeSyncDebugLogging;

		// Token: 0x04002B56 RID: 11094
		private bool imapForceICalForCalendarRetrievalOption;

		// Token: 0x04002B57 RID: 11095
		private bool imapSuppressReadReceipt;

		// Token: 0x04002B58 RID: 11096
		private bool popForceICalForCalendarRetrievalOption;

		// Token: 0x04002B59 RID: 11097
		private bool popSuppressReadReceipt;
	}
}
