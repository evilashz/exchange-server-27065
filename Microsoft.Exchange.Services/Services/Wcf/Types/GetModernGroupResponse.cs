using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009E7 RID: 2535
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernGroupResponse
	{
		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06004792 RID: 18322 RVA: 0x00100622 File Offset: 0x000FE822
		// (set) Token: 0x06004793 RID: 18323 RVA: 0x0010062A File Offset: 0x000FE82A
		[DataMember]
		public ModernGroupGeneralInfoResponse GeneralInfo { get; set; }

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06004794 RID: 18324 RVA: 0x00100633 File Offset: 0x000FE833
		// (set) Token: 0x06004795 RID: 18325 RVA: 0x0010063B File Offset: 0x000FE83B
		[DataMember]
		public ModernGroupMembersResponse MembersInfo { get; set; }

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06004796 RID: 18326 RVA: 0x00100644 File Offset: 0x000FE844
		// (set) Token: 0x06004797 RID: 18327 RVA: 0x0010064C File Offset: 0x000FE84C
		[DataMember]
		public ModernGroupMembersResponse OwnerList { get; set; }

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06004798 RID: 18328 RVA: 0x00100655 File Offset: 0x000FE855
		// (set) Token: 0x06004799 RID: 18329 RVA: 0x0010065D File Offset: 0x000FE85D
		[DataMember]
		public ModernGroupExternalResources ExternalResources { get; set; }

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x00100666 File Offset: 0x000FE866
		// (set) Token: 0x0600479B RID: 18331 RVA: 0x0010066E File Offset: 0x000FE86E
		[DataMember]
		public GroupMailboxProperties MailboxProperties { get; set; }
	}
}
