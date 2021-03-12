using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003EF RID: 1007
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("AddAggregatedAccountRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class AddAggregatedAccountRequest : BaseAggregatedAccountRequest
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0009E152 File Offset: 0x0009C352
		// (set) Token: 0x06001C31 RID: 7217 RVA: 0x0009E15A File Offset: 0x0009C35A
		[XmlElement]
		[DataMember(IsRequired = false)]
		public string Authentication { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x0009E163 File Offset: 0x0009C363
		// (set) Token: 0x06001C33 RID: 7219 RVA: 0x0009E16B File Offset: 0x0009C36B
		[DataMember(IsRequired = true)]
		[XmlElement]
		public string EmailAddress { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x0009E174 File Offset: 0x0009C374
		// (set) Token: 0x06001C35 RID: 7221 RVA: 0x0009E17C File Offset: 0x0009C37C
		[DataMember(IsRequired = false)]
		[XmlElement]
		public string UserName { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0009E185 File Offset: 0x0009C385
		// (set) Token: 0x06001C37 RID: 7223 RVA: 0x0009E18D File Offset: 0x0009C38D
		[XmlElement]
		[DataMember(IsRequired = false)]
		public string Password { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0009E196 File Offset: 0x0009C396
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x0009E19E File Offset: 0x0009C39E
		[DataMember(IsRequired = false)]
		[XmlElement]
		public string IncomingServer { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x0009E1A7 File Offset: 0x0009C3A7
		// (set) Token: 0x06001C3B RID: 7227 RVA: 0x0009E1AF File Offset: 0x0009C3AF
		[DataMember(IsRequired = false)]
		[XmlElement]
		public string IncomingPort { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x0009E1B8 File Offset: 0x0009C3B8
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x0009E1C0 File Offset: 0x0009C3C0
		[DataMember(IsRequired = false)]
		[XmlElement]
		public string IncrementalSyncInterval { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x0009E1C9 File Offset: 0x0009C3C9
		// (set) Token: 0x06001C3F RID: 7231 RVA: 0x0009E1D1 File Offset: 0x0009C3D1
		[XmlElement]
		[DataMember(IsRequired = false)]
		public string IncomingProtocol { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0009E1DA File Offset: 0x0009C3DA
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x0009E1E2 File Offset: 0x0009C3E2
		[DataMember(IsRequired = false)]
		[XmlElement]
		public string Security { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0009E1EB File Offset: 0x0009C3EB
		// (set) Token: 0x06001C43 RID: 7235 RVA: 0x0009E1F3 File Offset: 0x0009C3F3
		[XmlElement]
		[DataMember(IsRequired = false)]
		public string OutgoingServer { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0009E1FC File Offset: 0x0009C3FC
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x0009E204 File Offset: 0x0009C404
		[DataMember(IsRequired = false)]
		[XmlElement]
		public string OutgoingPort { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0009E20D File Offset: 0x0009C40D
		// (set) Token: 0x06001C47 RID: 7239 RVA: 0x0009E215 File Offset: 0x0009C415
		[XmlElement]
		[DataMember(IsRequired = false)]
		public string OutgoingProtocol { get; set; }

		// Token: 0x06001C49 RID: 7241 RVA: 0x0009E226 File Offset: 0x0009C426
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new AddAggregatedAccount(callContext, this);
		}
	}
}
