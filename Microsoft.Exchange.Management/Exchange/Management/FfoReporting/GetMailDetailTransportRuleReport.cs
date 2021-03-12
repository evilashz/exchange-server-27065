using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x0200039B RID: 923
	[Cmdlet("Get", "MailDetailTransportRuleReport")]
	[OutputType(new Type[]
	{
		typeof(MailDetailTransportRuleReport)
	})]
	public sealed class GetMailDetailTransportRuleReport : DetailTask<MailDetailTransportRuleReport>
	{
		// Token: 0x06002040 RID: 8256 RVA: 0x000889DE File Offset: 0x00086BDE
		public GetMailDetailTransportRuleReport() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyMessageDetail, Microsoft.Exchange.Hygiene.Data")
		{
			this.TransportRule = new MultiValuedProperty<string>();
			this.EventType = new MultiValuedProperty<string>();
			Schema.Utilities.AddRange<string>(this.EventType, GetMailDetailTransportRuleReport.EventTypeStrings);
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x00088A11 File Offset: 0x00086C11
		// (set) Token: 0x06002042 RID: 8258 RVA: 0x00088A19 File Offset: 0x00086C19
		[CmdletValidator("ValidateTransportRule", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidTransportRule, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		[QueryParameter("TransportRuleListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> TransportRule { get; set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00088A22 File Offset: 0x00086C22
		// (set) Token: 0x06002044 RID: 8260 RVA: 0x00088A2A File Offset: 0x00086C2A
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.EventTypes),
			Schema.EventTypes.TransportRuleActionHits | Schema.EventTypes.TransportRuleHits
		}, ErrorMessage = Strings.IDs.InvalidEventType)]
		[Parameter(Mandatory = false)]
		[QueryParameter("EventTypeListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x040019CB RID: 6603
		private const Schema.EventTypes SubsetEventTypes = Schema.EventTypes.TransportRuleActionHits | Schema.EventTypes.TransportRuleHits;

		// Token: 0x040019CC RID: 6604
		private static readonly string[] EventTypeStrings = Schema.Utilities.Split(Schema.EventTypes.TransportRuleActionHits | Schema.EventTypes.TransportRuleHits);
	}
}
