using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x02000393 RID: 915
	[OutputType(new Type[]
	{
		typeof(DlpDetailReport)
	})]
	[Cmdlet("Get", "DlpDetailReport")]
	public sealed class GetDlpDetailReport : DetailTask<DlpDetailReport>
	{
		// Token: 0x06001FF8 RID: 8184 RVA: 0x00088518 File Offset: 0x00086718
		public GetDlpDetailReport() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.DLPUnifiedDetail, Microsoft.Exchange.Hygiene.Data")
		{
			this.DlpPolicy = new MultiValuedProperty<string>();
			this.TransportRule = new MultiValuedProperty<string>();
			this.EventType = new MultiValuedProperty<string>();
			this.Source = new MultiValuedProperty<string>();
			Schema.Utilities.AddRange<string>(this.EventType, GetDlpDetailReport.EventTypeStrings);
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x0008856C File Offset: 0x0008676C
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x00088574 File Offset: 0x00086774
		[CmdletValidator("ValidateDlpPolicy", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidDlpPolicyParameter, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		[Parameter(Mandatory = false)]
		[QueryParameter("PolicyListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> DlpPolicy { get; set; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0008857D File Offset: 0x0008677D
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x00088585 File Offset: 0x00086785
		[QueryParameter("TransportRuleListQueryDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateTransportRule", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidTransportRule, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> TransportRule { get; set; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x0008858E File Offset: 0x0008678E
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00088596 File Offset: 0x00086796
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.EventTypes),
			Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits
		}, ErrorMessage = Strings.IDs.InvalidEventType)]
		[QueryParameter("EventTypeListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0008859F File Offset: 0x0008679F
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x000885A7 File Offset: 0x000867A7
		[QueryParameter("SenderAddressListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> Actor { get; set; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x000885B0 File Offset: 0x000867B0
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x000885B8 File Offset: 0x000867B8
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.Source),
			Schema.Source.SPO | Schema.Source.ODB
		}, ErrorMessage = Strings.IDs.InvalidSource)]
		[Parameter(Mandatory = false)]
		[QueryParameter("DataSourceListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> Source { get; set; }

		// Token: 0x040019A6 RID: 6566
		private const Schema.EventTypes SubsetEventTypes = Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits;

		// Token: 0x040019A7 RID: 6567
		private const Schema.Source SubsetSources = Schema.Source.SPO | Schema.Source.ODB;

		// Token: 0x040019A8 RID: 6568
		private static readonly string[] EventTypeStrings = Schema.Utilities.Split(Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits);
	}
}
