using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x020000AD RID: 173
	[Serializable]
	public class InitUMMailboxRequest : UpdateUMMailboxRequest
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x00018F66 File Offset: 0x00017166
		public InitUMMailboxRequest()
		{
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00018F6E File Offset: 0x0001716E
		internal InitUMMailboxRequest(ADUser user) : base(user)
		{
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00018F77 File Offset: 0x00017177
		internal override UMRpcResponse Execute()
		{
			Utils.InitUMMailbox(base.User);
			return null;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00018F85 File Offset: 0x00017185
		internal override string GetFriendlyName()
		{
			return Strings.InitializeUMMailboxRequest;
		}
	}
}
