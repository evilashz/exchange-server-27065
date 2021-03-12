using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	public class SubmitWelcomeMessageRequest : SubmitMessageRequest
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x0002A2AE File Offset: 0x000284AE
		public SubmitWelcomeMessageRequest()
		{
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002A2B6 File Offset: 0x000284B6
		internal SubmitWelcomeMessageRequest(ADUser user) : base(user)
		{
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002A2BF File Offset: 0x000284BF
		internal override UMRpcResponse Execute()
		{
			Utils.SendWelcomeMail(base.User, base.AccessNumbers, base.Extension, base.PIN, base.To, null);
			return null;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002A2E6 File Offset: 0x000284E6
		internal override string GetFriendlyName()
		{
			return Strings.SubmitWelcomeMessageRequest;
		}
	}
}
