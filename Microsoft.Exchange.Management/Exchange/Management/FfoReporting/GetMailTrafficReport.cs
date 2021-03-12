using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003A0 RID: 928
	[Cmdlet("Get", "MailTrafficReport")]
	[OutputType(new Type[]
	{
		typeof(MailTrafficReport)
	})]
	public sealed class GetMailTrafficReport : TrafficTask<MailTrafficReport>
	{
		// Token: 0x0600206A RID: 8298 RVA: 0x0008948E File Offset: 0x0008768E
		public GetMailTrafficReport() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.TrafficReport, Microsoft.Exchange.Hygiene.Data")
		{
			this.Action = new MultiValuedProperty<string>();
			this.EventType = new MultiValuedProperty<string>();
			Schema.Utilities.AddRange<string>(this.EventType, GetMailTrafficReport.EventTypeStrings);
			this.SummarizeBy = new MultiValuedProperty<string>();
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600206B RID: 8299 RVA: 0x000894CC File Offset: 0x000876CC
		// (set) Token: 0x0600206C RID: 8300 RVA: 0x000894D4 File Offset: 0x000876D4
		[Parameter(Mandatory = false)]
		[QueryParameter("EventTypeListQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.EventTypes),
			Schema.EventTypes.DLPMessages | Schema.EventTypes.GoodMail | Schema.EventTypes.Malware | Schema.EventTypes.SpamContentFiltered | Schema.EventTypes.SpamEnvelopeBlock | Schema.EventTypes.SpamIPBlock | Schema.EventTypes.TransportRuleMessages | Schema.EventTypes.SpamDBEBFilter
		}, ErrorMessage = Strings.IDs.InvalidEventType)]
		[CmdletValidator("ScrubDlp", new object[]
		{
			Schema.EventTypes.DLPMessages | Schema.EventTypes.GoodMail | Schema.EventTypes.Malware | Schema.EventTypes.SpamContentFiltered | Schema.EventTypes.SpamEnvelopeBlock | Schema.EventTypes.SpamIPBlock | Schema.EventTypes.TransportRuleMessages | Schema.EventTypes.SpamDBEBFilter
		}, ErrorMessage = Strings.IDs.InvalidDlpRoleAccess)]
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600206D RID: 8301 RVA: 0x000894DD File Offset: 0x000876DD
		// (set) Token: 0x0600206E RID: 8302 RVA: 0x000894E5 File Offset: 0x000876E5
		[QueryParameter("ActionListQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.Actions)
		}, ErrorMessage = Strings.IDs.InvalidActionParameter)]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> Action { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x000894EE File Offset: 0x000876EE
		// (set) Token: 0x06002070 RID: 8304 RVA: 0x000894F6 File Offset: 0x000876F6
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.SummarizeByValues),
			Schema.SummarizeByValues.Action | Schema.SummarizeByValues.Domain | Schema.SummarizeByValues.EventType
		}, ErrorMessage = Strings.IDs.InvalidSummmarizeBy)]
		[QueryParameter("SummarizeByQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> SummarizeBy { get; set; }

		// Token: 0x040019E4 RID: 6628
		private const Schema.EventTypes SubsetEventTypes = Schema.EventTypes.DLPMessages | Schema.EventTypes.GoodMail | Schema.EventTypes.Malware | Schema.EventTypes.SpamContentFiltered | Schema.EventTypes.SpamEnvelopeBlock | Schema.EventTypes.SpamIPBlock | Schema.EventTypes.TransportRuleMessages | Schema.EventTypes.SpamDBEBFilter;

		// Token: 0x040019E5 RID: 6629
		private static readonly string[] EventTypeStrings = Schema.Utilities.Split(Schema.EventTypes.DLPMessages | Schema.EventTypes.GoodMail | Schema.EventTypes.Malware | Schema.EventTypes.SpamContentFiltered | Schema.EventTypes.SpamEnvelopeBlock | Schema.EventTypes.SpamIPBlock | Schema.EventTypes.TransportRuleMessages | Schema.EventTypes.SpamDBEBFilter);
	}
}
