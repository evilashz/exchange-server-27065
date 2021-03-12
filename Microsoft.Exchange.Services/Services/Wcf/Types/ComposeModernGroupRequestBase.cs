using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B42 RID: 2882
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class ComposeModernGroupRequestBase : BaseRequest
	{
		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x0600519E RID: 20894 RVA: 0x0010A9CC File Offset: 0x00108BCC
		// (set) Token: 0x0600519F RID: 20895 RVA: 0x0010A9D4 File Offset: 0x00108BD4
		[DataMember(Name = "Name", IsRequired = false)]
		public string Name { get; set; }

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x060051A0 RID: 20896 RVA: 0x0010A9DD File Offset: 0x00108BDD
		// (set) Token: 0x060051A1 RID: 20897 RVA: 0x0010A9E5 File Offset: 0x00108BE5
		[DataMember(Name = "Description", IsRequired = false)]
		public string Description { get; set; }

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x060051A2 RID: 20898 RVA: 0x0010A9EE File Offset: 0x00108BEE
		// (set) Token: 0x060051A3 RID: 20899 RVA: 0x0010A9F6 File Offset: 0x00108BF6
		[DataMember(Name = "AddedMembers", IsRequired = false)]
		public string[] AddedMembers { get; set; }

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x060051A4 RID: 20900 RVA: 0x0010A9FF File Offset: 0x00108BFF
		// (set) Token: 0x060051A5 RID: 20901 RVA: 0x0010AA07 File Offset: 0x00108C07
		[DataMember(Name = "AddedOwners", IsRequired = false)]
		public string[] AddedOwners { get; set; }
	}
}
