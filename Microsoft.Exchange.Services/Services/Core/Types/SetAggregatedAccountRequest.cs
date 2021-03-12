using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000479 RID: 1145
	[XmlType("SetAggregatedAccountRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetAggregatedAccountRequest : BaseAggregatedAccountRequest
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x000A2A69 File Offset: 0x000A0C69
		// (set) Token: 0x060021DF RID: 8671 RVA: 0x000A2A71 File Offset: 0x000A0C71
		[XmlElement]
		public string Authentication { get; set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000A2A7A File Offset: 0x000A0C7A
		// (set) Token: 0x060021E1 RID: 8673 RVA: 0x000A2A82 File Offset: 0x000A0C82
		[XmlElement]
		public string EmailAddress { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000A2A8B File Offset: 0x000A0C8B
		// (set) Token: 0x060021E3 RID: 8675 RVA: 0x000A2A93 File Offset: 0x000A0C93
		[XmlElement]
		public string UserName { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x000A2A9C File Offset: 0x000A0C9C
		// (set) Token: 0x060021E5 RID: 8677 RVA: 0x000A2AA4 File Offset: 0x000A0CA4
		[XmlElement]
		public string Password { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x000A2AAD File Offset: 0x000A0CAD
		// (set) Token: 0x060021E7 RID: 8679 RVA: 0x000A2AB5 File Offset: 0x000A0CB5
		[XmlElement]
		public string IncomingServer { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000A2ABE File Offset: 0x000A0CBE
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x000A2AC6 File Offset: 0x000A0CC6
		[XmlElement]
		public string IncomingPort { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x000A2ACF File Offset: 0x000A0CCF
		// (set) Token: 0x060021EB RID: 8683 RVA: 0x000A2AD7 File Offset: 0x000A0CD7
		[XmlElement]
		public string IncrementalSyncInterval { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x000A2AE0 File Offset: 0x000A0CE0
		// (set) Token: 0x060021ED RID: 8685 RVA: 0x000A2AE8 File Offset: 0x000A0CE8
		[XmlElement]
		public string Security { get; set; }

		// Token: 0x060021EE RID: 8686 RVA: 0x000A2AF1 File Offset: 0x000A0CF1
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SetAggregatedAccount(callContext, this);
		}
	}
}
