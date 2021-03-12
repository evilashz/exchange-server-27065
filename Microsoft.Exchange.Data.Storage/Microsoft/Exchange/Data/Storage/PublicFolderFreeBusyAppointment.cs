using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CAC RID: 3244
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderFreeBusyAppointment
	{
		// Token: 0x06007115 RID: 28949 RVA: 0x001F56FF File Offset: 0x001F38FF
		public PublicFolderFreeBusyAppointment(ExDateTime startTime, ExDateTime endTime, BusyType busyType)
		{
			this.StartTime = startTime;
			this.EndTime = endTime;
			if (EnumValidator.IsValidValue<BusyType>(busyType))
			{
				this.BusyType = busyType;
				return;
			}
			this.BusyType = BusyType.Free;
		}

		// Token: 0x17001E4C RID: 7756
		// (get) Token: 0x06007116 RID: 28950 RVA: 0x001F572C File Offset: 0x001F392C
		// (set) Token: 0x06007117 RID: 28951 RVA: 0x001F5734 File Offset: 0x001F3934
		public ExDateTime StartTime { get; private set; }

		// Token: 0x17001E4D RID: 7757
		// (get) Token: 0x06007118 RID: 28952 RVA: 0x001F573D File Offset: 0x001F393D
		// (set) Token: 0x06007119 RID: 28953 RVA: 0x001F5745 File Offset: 0x001F3945
		public ExDateTime EndTime { get; private set; }

		// Token: 0x17001E4E RID: 7758
		// (get) Token: 0x0600711A RID: 28954 RVA: 0x001F574E File Offset: 0x001F394E
		// (set) Token: 0x0600711B RID: 28955 RVA: 0x001F5756 File Offset: 0x001F3956
		public BusyType BusyType { get; private set; }
	}
}
