using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x02000399 RID: 921
	[OutputType(new Type[]
	{
		typeof(MailDetailReport)
	})]
	[Cmdlet("Get", "MailDetailReport")]
	public sealed class GetMailDetailReport : TrafficTask<MailDetailReport>
	{
		// Token: 0x06002035 RID: 8245 RVA: 0x00088917 File Offset: 0x00086B17
		public GetMailDetailReport() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.MessageDetailReport, Microsoft.Exchange.Hygiene.Data")
		{
			this.MessageId = new MultiValuedProperty<string>();
			this.MessageTraceId = new MultiValuedProperty<Guid>();
			this.EventType = new MultiValuedProperty<string>();
			Schema.Utilities.AddRange<string>(this.EventType, GetMailDetailReport.EventTypeStrings);
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x00088955 File Offset: 0x00086B55
		// (set) Token: 0x06002037 RID: 8247 RVA: 0x0008895D File Offset: 0x00086B5D
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.EventTypes),
			Schema.EventTypes.GoodMail
		}, ErrorMessage = Strings.IDs.InvalidEventType)]
		[QueryParameter("EventTypeListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x00088966 File Offset: 0x00086B66
		// (set) Token: 0x06002039 RID: 8249 RVA: 0x0008896E File Offset: 0x00086B6E
		[QueryParameter("MessageIdListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> MessageId { get; set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x00088977 File Offset: 0x00086B77
		// (set) Token: 0x0600203B RID: 8251 RVA: 0x0008897F File Offset: 0x00086B7F
		[QueryParameter("InternalMessageIdQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Guid> MessageTraceId { get; set; }

		// Token: 0x040019C4 RID: 6596
		private const Schema.EventTypes SubsetEventTypes = Schema.EventTypes.GoodMail;

		// Token: 0x040019C5 RID: 6597
		private static readonly string[] EventTypeStrings = Schema.Utilities.Split(Schema.EventTypes.GoodMail);
	}
}
