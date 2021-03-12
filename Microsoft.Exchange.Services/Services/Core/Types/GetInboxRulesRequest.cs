using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200043A RID: 1082
	[XmlType("GetInboxRulesRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetInboxRulesRequest : InboxRulesRequest
	{
		// Token: 0x06001FBA RID: 8122 RVA: 0x000A1167 File Offset: 0x0009F367
		public GetInboxRulesRequest() : base(ExTraceGlobals.GetInboxRulesCallTracer, false)
		{
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000A1175 File Offset: 0x0009F375
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetInboxRules(callContext, this);
		}
	}
}
