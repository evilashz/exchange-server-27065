using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200038B RID: 907
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AdjacencyOrConflictInfo
	{
		// Token: 0x060027D3 RID: 10195 RVA: 0x0009F328 File Offset: 0x0009D528
		public AdjacencyOrConflictInfo(OccurrenceInfo occurrenceInfo, string subject, string location, BusyType freeBusyType, AdjacencyOrConflictType type, byte[] globalObjectId, Sensitivity sensitivity, bool isAllDayEvent)
		{
			EnumValidator.ThrowIfInvalid<BusyType>(freeBusyType, "freeBusyType");
			EnumValidator.ThrowIfInvalid<AdjacencyOrConflictType>(type, "type");
			EnumValidator.ThrowIfInvalid<Sensitivity>(sensitivity, "sensitivity");
			this.OccurrenceInfo = occurrenceInfo;
			this.Subject = subject;
			this.Location = location;
			this.FreeBusyStatus = freeBusyType;
			this.AdjacencyOrConflictType = type;
			this.GlobalObjectId = globalObjectId;
			this.Sensitivity = sensitivity;
			this.IsAllDayEvent = isAllDayEvent;
		}

		// Token: 0x0400178D RID: 6029
		public readonly OccurrenceInfo OccurrenceInfo;

		// Token: 0x0400178E RID: 6030
		public readonly string Subject;

		// Token: 0x0400178F RID: 6031
		public readonly string Location;

		// Token: 0x04001790 RID: 6032
		public readonly BusyType FreeBusyStatus;

		// Token: 0x04001791 RID: 6033
		public readonly AdjacencyOrConflictType AdjacencyOrConflictType;

		// Token: 0x04001792 RID: 6034
		public readonly byte[] GlobalObjectId;

		// Token: 0x04001793 RID: 6035
		public readonly Sensitivity Sensitivity;

		// Token: 0x04001794 RID: 6036
		public readonly bool IsAllDayEvent;
	}
}
