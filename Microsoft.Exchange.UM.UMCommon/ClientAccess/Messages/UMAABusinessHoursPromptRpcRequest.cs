using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x0200013A RID: 314
	[Serializable]
	public class UMAABusinessHoursPromptRpcRequest : UMAutoAttendantPromptRpcRequest
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x00026646 File Offset: 0x00024846
		public UMAABusinessHoursPromptRpcRequest(UMAutoAttendant aa) : base(aa)
		{
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002664F File Offset: 0x0002484F
		internal override string GetFriendlyName()
		{
			return Strings.AutoAttendantBusinessHoursPromptRequest;
		}
	}
}
