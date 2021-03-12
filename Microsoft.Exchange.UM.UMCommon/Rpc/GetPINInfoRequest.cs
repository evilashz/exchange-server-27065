using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000081 RID: 129
	[Serializable]
	public class GetPINInfoRequest : UMRpcRequest
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x0000F305 File Offset: 0x0000D505
		public GetPINInfoRequest()
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000F30D File Offset: 0x0000D50D
		internal GetPINInfoRequest(ADUser user) : base(user)
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000F316 File Offset: 0x0000D516
		internal override UMRpcResponse Execute()
		{
			return Utils.GetPINInfo(base.User);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000F323 File Offset: 0x0000D523
		internal override string GetFriendlyName()
		{
			return Strings.GetPINInfoRequest;
		}
	}
}
