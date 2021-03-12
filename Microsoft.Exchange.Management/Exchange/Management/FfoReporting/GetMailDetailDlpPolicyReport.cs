using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x02000397 RID: 919
	[Cmdlet("Get", "MailDetailDlpPolicyReport")]
	[OutputType(new Type[]
	{
		typeof(MailDetailDlpPolicyReport)
	})]
	public sealed class GetMailDetailDlpPolicyReport : DetailTask<MailDetailDlpPolicyReport>
	{
		// Token: 0x06002028 RID: 8232 RVA: 0x00088834 File Offset: 0x00086A34
		public GetMailDetailDlpPolicyReport() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.DLPMessageDetail, Microsoft.Exchange.Hygiene.Data")
		{
			this.DlpPolicy = new MultiValuedProperty<string>();
			this.TransportRule = new MultiValuedProperty<string>();
			this.EventType = new MultiValuedProperty<string>();
			Schema.Utilities.AddRange<string>(this.EventType, GetMailDetailDlpPolicyReport.EventTypeStrings);
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x00088872 File Offset: 0x00086A72
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x0008887A File Offset: 0x00086A7A
		[QueryParameter("PolicyListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateDlpPolicy", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidDlpPolicyParameter, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		public MultiValuedProperty<string> DlpPolicy { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x00088883 File Offset: 0x00086A83
		// (set) Token: 0x0600202C RID: 8236 RVA: 0x0008888B File Offset: 0x00086A8B
		[CmdletValidator("ValidateTransportRule", new object[]
		{

		}, ErrorMessage = Strings.IDs.InvalidTransportRule, ValidatorType = CmdletValidator.ValidatorTypes.PostprocessingWithConfigSession)]
		[Parameter(Mandatory = false)]
		[QueryParameter("TransportRuleListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> TransportRule { get; set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x00088894 File Offset: 0x00086A94
		// (set) Token: 0x0600202E RID: 8238 RVA: 0x0008889C File Offset: 0x00086A9C
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.EventTypes),
			Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits
		}, ErrorMessage = Strings.IDs.InvalidEventType)]
		[Parameter(Mandatory = false)]
		[QueryParameter("EventTypeListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x040019BD RID: 6589
		private const Schema.EventTypes SubsetEventTypes = Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits;

		// Token: 0x040019BE RID: 6590
		private static readonly string[] EventTypeStrings = Schema.Utilities.Split(Schema.EventTypes.DLPActionHits | Schema.EventTypes.DLPPolicyFalsePositive | Schema.EventTypes.DLPPolicyHits | Schema.EventTypes.DLPPolicyOverride | Schema.EventTypes.DLPRuleHits);
	}
}
