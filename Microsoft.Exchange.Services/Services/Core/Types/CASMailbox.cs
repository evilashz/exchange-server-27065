using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000A9A RID: 2714
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CASMailbox : SetCASMailbox
	{
		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06004CAA RID: 19626 RVA: 0x0010671E File Offset: 0x0010491E
		// (set) Token: 0x06004CAB RID: 19627 RVA: 0x00106726 File Offset: 0x00104926
		[DataMember]
		public bool ActiveSyncEnabled { get; set; }

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06004CAC RID: 19628 RVA: 0x0010672F File Offset: 0x0010492F
		// (set) Token: 0x06004CAD RID: 19629 RVA: 0x00106737 File Offset: 0x00104937
		[DataMember]
		public string ExternalImapSettings { get; set; }

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06004CAE RID: 19630 RVA: 0x00106740 File Offset: 0x00104940
		// (set) Token: 0x06004CAF RID: 19631 RVA: 0x00106748 File Offset: 0x00104948
		[DataMember]
		public string ExternalPopSettings { get; set; }

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06004CB0 RID: 19632 RVA: 0x00106751 File Offset: 0x00104951
		// (set) Token: 0x06004CB1 RID: 19633 RVA: 0x00106759 File Offset: 0x00104959
		[DataMember]
		public string ExternalSmtpSettings { get; set; }

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06004CB2 RID: 19634 RVA: 0x00106762 File Offset: 0x00104962
		// (set) Token: 0x06004CB3 RID: 19635 RVA: 0x0010676A File Offset: 0x0010496A
		[DataMember]
		public bool ImapEnabled { get; set; }

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x00106773 File Offset: 0x00104973
		// (set) Token: 0x06004CB5 RID: 19637 RVA: 0x0010677B File Offset: 0x0010497B
		[DataMember]
		public string InternalImapSettings { get; set; }

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x00106784 File Offset: 0x00104984
		// (set) Token: 0x06004CB7 RID: 19639 RVA: 0x0010678C File Offset: 0x0010498C
		[DataMember]
		public string InternalPopSettings { get; set; }

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06004CB8 RID: 19640 RVA: 0x00106795 File Offset: 0x00104995
		// (set) Token: 0x06004CB9 RID: 19641 RVA: 0x0010679D File Offset: 0x0010499D
		[DataMember]
		public string InternalSmtpSettings { get; set; }

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06004CBA RID: 19642 RVA: 0x001067A6 File Offset: 0x001049A6
		// (set) Token: 0x06004CBB RID: 19643 RVA: 0x001067AE File Offset: 0x001049AE
		[DataMember]
		public bool MAPIEnabled { get; set; }

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06004CBC RID: 19644 RVA: 0x001067B7 File Offset: 0x001049B7
		// (set) Token: 0x06004CBD RID: 19645 RVA: 0x001067BF File Offset: 0x001049BF
		[DataMember]
		public bool OWAEnabled { get; set; }

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06004CBE RID: 19646 RVA: 0x001067C8 File Offset: 0x001049C8
		// (set) Token: 0x06004CBF RID: 19647 RVA: 0x001067D0 File Offset: 0x001049D0
		[DataMember]
		public bool PopEnabled { get; set; }
	}
}
