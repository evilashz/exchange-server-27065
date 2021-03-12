using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000497 RID: 1175
	[XmlType("UpdateInboxRulesRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UpdateInboxRulesRequest : InboxRulesRequest
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x000A38C4 File Offset: 0x000A1AC4
		// (set) Token: 0x06002319 RID: 8985 RVA: 0x000A38CC File Offset: 0x000A1ACC
		[XmlElement(Order = 1)]
		public bool RemoveOutlookRuleBlob { get; set; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x000A38D5 File Offset: 0x000A1AD5
		// (set) Token: 0x0600231B RID: 8987 RVA: 0x000A38DD File Offset: 0x000A1ADD
		[XmlIgnore]
		public bool RemoveOutlookRuleBlobSpecified { get; set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x000A38E6 File Offset: 0x000A1AE6
		// (set) Token: 0x0600231D RID: 8989 RVA: 0x000A38EE File Offset: 0x000A1AEE
		[XmlArray(Order = 2)]
		[XmlArrayItem("SetRuleOperation", Type = typeof(SetRuleOperation), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem("DeleteRuleOperation", Type = typeof(DeleteRuleOperation), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem("CreateRuleOperation", Type = typeof(CreateRuleOperation), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public RuleOperation[] Operations { get; set; }

		// Token: 0x0600231E RID: 8990 RVA: 0x000A38F7 File Offset: 0x000A1AF7
		public UpdateInboxRulesRequest() : base(ExTraceGlobals.UpdateInboxRulesCallTracer, true)
		{
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000A3905 File Offset: 0x000A1B05
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new UpdateInboxRules(callContext, this);
		}
	}
}
