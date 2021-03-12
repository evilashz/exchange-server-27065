using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CAB RID: 3243
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderFreeBusy
	{
		// Token: 0x17001E49 RID: 7753
		// (get) Token: 0x0600710F RID: 28943 RVA: 0x001F56CC File Offset: 0x001F38CC
		// (set) Token: 0x06007110 RID: 28944 RVA: 0x001F56D4 File Offset: 0x001F38D4
		public ExDateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.startDate = value;
			}
		}

		// Token: 0x17001E4A RID: 7754
		// (get) Token: 0x06007111 RID: 28945 RVA: 0x001F56DD File Offset: 0x001F38DD
		// (set) Token: 0x06007112 RID: 28946 RVA: 0x001F56E5 File Offset: 0x001F38E5
		public int NumberOfMonths
		{
			get
			{
				return this.numberOfMonths;
			}
			set
			{
				this.numberOfMonths = value;
			}
		}

		// Token: 0x17001E4B RID: 7755
		// (get) Token: 0x06007113 RID: 28947 RVA: 0x001F56EE File Offset: 0x001F38EE
		// (set) Token: 0x06007114 RID: 28948 RVA: 0x001F56F6 File Offset: 0x001F38F6
		public List<PublicFolderFreeBusyAppointment> Appointments
		{
			get
			{
				return this.appointments;
			}
			set
			{
				this.appointments = value;
			}
		}

		// Token: 0x04004E92 RID: 20114
		private int numberOfMonths;

		// Token: 0x04004E93 RID: 20115
		private ExDateTime startDate;

		// Token: 0x04004E94 RID: 20116
		private List<PublicFolderFreeBusyAppointment> appointments;
	}
}
