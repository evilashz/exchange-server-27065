using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x02000395 RID: 917
	[Cmdlet("Get", "DlpDetectionsReport")]
	[OutputType(new Type[]
	{
		typeof(DlpReport)
	})]
	public sealed class GetDlpDetectionsReport : TrafficTask<DlpReport>
	{
		// Token: 0x06002014 RID: 8212 RVA: 0x0008871C File Offset: 0x0008691C
		public GetDlpDetectionsReport() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.PolicyTrafficReport, Microsoft.Exchange.Hygiene.Data")
		{
			this.DlpPolicy = new MultiValuedProperty<string>();
			this.TransportRule = new MultiValuedProperty<string>();
			this.Action = new MultiValuedProperty<string>();
			this.EventType = new MultiValuedProperty<string>();
			this.Source = new MultiValuedProperty<string>();
			Schema.Utilities.AddRange<string>(this.EventType, GetDlpDetectionsReport.EventTypeStrings);
			this.SummarizeBy = new MultiValuedProperty<string>();
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x00088786 File Offset: 0x00086986
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x0008878E File Offset: 0x0008698E
		[QueryParameter("EventTypeListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.EventTypes),
			Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits
		}, ErrorMessage = Strings.IDs.InvalidEventType)]
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x00088797 File Offset: 0x00086997
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x0008879F File Offset: 0x0008699F
		[Parameter(Mandatory = false)]
		[QueryParameter("PolicyListQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateDlpPolicy", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidDlpPolicyParameter, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		public MultiValuedProperty<string> DlpPolicy { get; set; }

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x000887A8 File Offset: 0x000869A8
		// (set) Token: 0x0600201A RID: 8218 RVA: 0x000887B0 File Offset: 0x000869B0
		[Parameter(Mandatory = false)]
		[QueryParameter("RuleListQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateTransportRule", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidTransportRule, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		public MultiValuedProperty<string> TransportRule { get; set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x000887B9 File Offset: 0x000869B9
		// (set) Token: 0x0600201C RID: 8220 RVA: 0x000887C1 File Offset: 0x000869C1
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.Actions)
		}, ErrorMessage = Strings.IDs.InvalidActionParameter)]
		[QueryParameter("ActionListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> Action { get; set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x000887CA File Offset: 0x000869CA
		// (set) Token: 0x0600201E RID: 8222 RVA: 0x000887D2 File Offset: 0x000869D2
		[QueryParameter("SummarizeByQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.SummarizeByValues)
		}, ErrorMessage = Strings.IDs.InvalidSummmarizeBy)]
		public MultiValuedProperty<string> SummarizeBy { get; set; }

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000887DB File Offset: 0x000869DB
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x000887E3 File Offset: 0x000869E3
		[QueryParameter("DataSourceListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.Source),
			Schema.Source.SPO | Schema.Source.ODB
		}, ErrorMessage = Strings.IDs.InvalidSource)]
		public MultiValuedProperty<string> Source { get; set; }

		// Token: 0x040019B4 RID: 6580
		private const Schema.EventTypes SubsetEventTypes = Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits;

		// Token: 0x040019B5 RID: 6581
		private const Schema.Source SubsetSources = Schema.Source.SPO | Schema.Source.ODB;

		// Token: 0x040019B6 RID: 6582
		private static readonly string[] EventTypeStrings = Schema.Utilities.Split(Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits);
	}
}
