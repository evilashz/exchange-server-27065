using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000406 RID: 1030
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ResponseRumInfo : AttendeeRumInfo
	{
		// Token: 0x06002EDF RID: 11999 RVA: 0x000C1090 File Offset: 0x000BF290
		private ResponseRumInfo() : this(null)
		{
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000C10AC File Offset: 0x000BF2AC
		private ResponseRumInfo(ExDateTime? originalStartTime) : base(RumType.Response, originalStartTime)
		{
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000C10B6 File Offset: 0x000BF2B6
		public static ResponseRumInfo CreateMasterInstance()
		{
			return new ResponseRumInfo();
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000C10BD File Offset: 0x000BF2BD
		public static ResponseRumInfo CreateOccurrenceInstance(ExDateTime originalStartTime)
		{
			return new ResponseRumInfo(new ExDateTime?(originalStartTime));
		}
	}
}
