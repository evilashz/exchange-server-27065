using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x020000AC RID: 172
	[Serializable]
	public abstract class UpdateUMMailboxRequest : UMRpcRequest
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00018EA1 File Offset: 0x000170A1
		public UpdateUMMailboxRequest()
		{
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00018EA9 File Offset: 0x000170A9
		internal UpdateUMMailboxRequest(ADUser user) : base(user)
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00018EB4 File Offset: 0x000170B4
		protected override void PopulateUserFields(ADUser adUser)
		{
			base.PopulateUserFields(adUser);
			adUser.UMRecipientDialPlanId = base.DialPlanId;
			adUser.UMMailboxPolicy = base.PolicyId;
			adUser.EmailAddresses = new ProxyAddressCollection();
			foreach (string text in base.AddressList)
			{
				ProxyAddress proxyAddress = ProxyAddress.Parse(text);
				InvalidProxyAddress invalidProxyAddress = proxyAddress as InvalidProxyAddress;
				if (invalidProxyAddress != null)
				{
					CallIdTracer.TraceWarning(ExTraceGlobals.UtilTracer, 0, "{0}.PopulateUserFields: failed to parse proxy address '{1}'. Skipping it. Error: {2}.", new object[]
					{
						this.GetFriendlyName(),
						text,
						invalidProxyAddress.ParseException
					});
				}
				else
				{
					adUser.EmailAddresses.Add(proxyAddress);
				}
			}
		}
	}
}
