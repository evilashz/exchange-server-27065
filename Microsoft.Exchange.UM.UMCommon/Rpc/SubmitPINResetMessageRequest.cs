using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000166 RID: 358
	[Serializable]
	public class SubmitPINResetMessageRequest : SubmitMessageRequest
	{
		// Token: 0x06000B63 RID: 2915 RVA: 0x0002A26B File Offset: 0x0002846B
		public SubmitPINResetMessageRequest()
		{
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002A273 File Offset: 0x00028473
		internal SubmitPINResetMessageRequest(ADUser user) : base(user)
		{
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002A27C File Offset: 0x0002847C
		internal override UMRpcResponse Execute()
		{
			Utils.SendPasswordResetMail(base.User, base.AccessNumbers, base.Extension, base.PIN, base.To);
			return null;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002A2A2 File Offset: 0x000284A2
		internal override string GetFriendlyName()
		{
			return Strings.SubmitPINResetMessageRequest;
		}
	}
}
