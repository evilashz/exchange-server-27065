using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000139 RID: 313
	[Serializable]
	public class UMAABusinessLocationPromptRpcRequest : UMAutoAttendantPromptRpcRequest
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x00026631 File Offset: 0x00024831
		public UMAABusinessLocationPromptRpcRequest(UMAutoAttendant aa) : base(aa)
		{
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002663A File Offset: 0x0002483A
		internal override string GetFriendlyName()
		{
			return Strings.AutoAttendantBusinessLocationPromptRequest;
		}
	}
}
