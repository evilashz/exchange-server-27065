using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003A4 RID: 932
	[Cmdlet("Get", "MailTrafficTopReport")]
	[OutputType(new Type[]
	{
		typeof(MailTrafficTopReport)
	})]
	public sealed class GetMailTrafficTopReport : TrafficTask<MailTrafficTopReport>
	{
		// Token: 0x060020BE RID: 8382 RVA: 0x0008AEC7 File Offset: 0x000890C7
		public GetMailTrafficTopReport() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.TopTrafficReport, Microsoft.Exchange.Hygiene.Data")
		{
			this.EventType = new MultiValuedProperty<string>();
			Schema.Utilities.AddRange<string>(this.EventType, GetMailTrafficTopReport.EventTypeStrings);
			this.SummarizeBy = new MultiValuedProperty<string>();
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0008AEFA File Offset: 0x000890FA
		// (set) Token: 0x060020C0 RID: 8384 RVA: 0x0008AF02 File Offset: 0x00089102
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.EventTypes),
			Schema.EventTypes.TopMailUser | Schema.EventTypes.TopMalware | Schema.EventTypes.TopMalwareUser | Schema.EventTypes.TopSpamUser
		}, ErrorMessage = Strings.IDs.InvalidEventType)]
		[QueryParameter("EventTypeListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> EventType { get; set; }

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0008AF0B File Offset: 0x0008910B
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x0008AF13 File Offset: 0x00089113
		[QueryParameter("SummarizeByQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false)]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.SummarizeByValues),
			Schema.SummarizeByValues.Domain | Schema.SummarizeByValues.EventType
		}, ErrorMessage = Strings.IDs.InvalidSummmarizeBy)]
		public MultiValuedProperty<string> SummarizeBy { get; set; }

		// Token: 0x04001A23 RID: 6691
		private const Schema.EventTypes SubsetEventTypes = Schema.EventTypes.TopMailUser | Schema.EventTypes.TopMalware | Schema.EventTypes.TopMalwareUser | Schema.EventTypes.TopSpamUser;

		// Token: 0x04001A24 RID: 6692
		private static readonly string[] EventTypeStrings = Schema.Utilities.Split(Schema.EventTypes.TopMailUser | Schema.EventTypes.TopMalware | Schema.EventTypes.TopMalwareUser | Schema.EventTypes.TopSpamUser);
	}
}
