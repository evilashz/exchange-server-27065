using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x0200039F RID: 927
	[Cmdlet("Get", "MailTrafficPolicyReport")]
	[OutputType(new Type[]
	{
		typeof(MailTrafficPolicyReport)
	})]
	public sealed class GetMailTrafficPolicyReport : TrafficTask<MailTrafficPolicyReport>
	{
		// Token: 0x0600205E RID: 8286 RVA: 0x000893C4 File Offset: 0x000875C4
		public GetMailTrafficPolicyReport() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data")
		{
			this.DlpPolicy = new MultiValuedProperty<string>();
			this.TransportRule = new MultiValuedProperty<string>();
			this.Action = new MultiValuedProperty<string>();
			this.EventType = new MultiValuedProperty<string>();
			Schema.Utilities.AddRange<string>(this.EventType, GetMailTrafficPolicyReport.EventTypeStrings);
			this.SummarizeBy = new MultiValuedProperty<string>();
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00089423 File Offset: 0x00087623
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x0008942B File Offset: 0x0008762B
		[QueryParameter("EventTypeListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.EventTypes),
			Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits | Schema.EventTypes.TransportRuleActionHits | Schema.EventTypes.TransportRuleHits
		}, ErrorMessage = Strings.IDs.InvalidEventType)]
		[CmdletValidator("ScrubDlp", new object[]
		{
			Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits | Schema.EventTypes.TransportRuleActionHits | Schema.EventTypes.TransportRuleHits
		}, ErrorMessage = Strings.IDs.InvalidDlpRoleAccess)]
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x00089434 File Offset: 0x00087634
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x0008943C File Offset: 0x0008763C
		[QueryParameter("PolicyListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateDlpPolicy", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidDlpPolicyParameter, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		public MultiValuedProperty<string> DlpPolicy { get; set; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x00089445 File Offset: 0x00087645
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x0008944D File Offset: 0x0008764D
		[CmdletValidator("ValidateTransportRule", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidTransportRule, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		[Parameter(Mandatory = false)]
		[QueryParameter("RuleListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> TransportRule { get; set; }

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x00089456 File Offset: 0x00087656
		// (set) Token: 0x06002066 RID: 8294 RVA: 0x0008945E File Offset: 0x0008765E
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.Actions)
		}, ErrorMessage = Strings.IDs.InvalidActionParameter)]
		[Parameter(Mandatory = false)]
		[QueryParameter("ActionListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> Action { get; set; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x00089467 File Offset: 0x00087667
		// (set) Token: 0x06002068 RID: 8296 RVA: 0x0008946F File Offset: 0x0008766F
		[QueryParameter("SummarizeByQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.SummarizeByValues)
		}, ErrorMessage = Strings.IDs.InvalidSummmarizeBy)]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> SummarizeBy { get; set; }

		// Token: 0x040019DD RID: 6621
		private const Schema.EventTypes SubsetEventTypes = Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits | Schema.EventTypes.TransportRuleActionHits | Schema.EventTypes.TransportRuleHits;

		// Token: 0x040019DE RID: 6622
		private static readonly string[] EventTypeStrings = Schema.Utilities.Split(Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits | Schema.EventTypes.TransportRuleActionHits | Schema.EventTypes.TransportRuleHits);
	}
}
