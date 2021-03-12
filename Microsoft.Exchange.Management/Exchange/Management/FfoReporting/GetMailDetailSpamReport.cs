using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x0200039A RID: 922
	[Cmdlet("Get", "MailDetailSpamReport")]
	[OutputType(new Type[]
	{
		typeof(MailDetailSpamReport)
	})]
	public sealed class GetMailDetailSpamReport : DetailTask<MailDetailSpamReport>
	{
		// Token: 0x0600203D RID: 8253 RVA: 0x0008899B File Offset: 0x00086B9B
		public GetMailDetailSpamReport() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.SpamMessageDetail, Microsoft.Exchange.Hygiene.Data")
		{
			this.EventType = new MultiValuedProperty<string>();
			this.EventType.Add(Schema.EventTypes.SpamContentFiltered.ToString());
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x000889CD File Offset: 0x00086BCD
		// (set) Token: 0x0600203F RID: 8255 RVA: 0x000889D5 File Offset: 0x00086BD5
		[QueryParameter("EventTypeListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.EventTypes),
			Schema.EventTypes.SpamContentFiltered | Schema.EventTypes.SpamEnvelopeBlock | Schema.EventTypes.SpamIPBlock | Schema.EventTypes.SpamDBEBFilter
		}, ErrorMessage = Strings.IDs.InvalidEventType)]
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x040019C9 RID: 6601
		private const Schema.EventTypes SubsetEventTypes = Schema.EventTypes.SpamContentFiltered | Schema.EventTypes.SpamEnvelopeBlock | Schema.EventTypes.SpamIPBlock | Schema.EventTypes.SpamDBEBFilter;
	}
}
