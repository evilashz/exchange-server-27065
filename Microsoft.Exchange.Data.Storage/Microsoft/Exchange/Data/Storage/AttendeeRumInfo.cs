using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000405 RID: 1029
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AttendeeRumInfo : RumInfo
	{
		// Token: 0x06002EDD RID: 11997 RVA: 0x000C1068 File Offset: 0x000BF268
		private AttendeeRumInfo() : base(RumType.None, null)
		{
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000C1085 File Offset: 0x000BF285
		protected AttendeeRumInfo(RumType type, ExDateTime? originalStartTime) : base(type, originalStartTime)
		{
		}
	}
}
