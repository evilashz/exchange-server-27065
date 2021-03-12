using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A97 RID: 2711
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class IdentityCollectionRequest : BaseJsonRequest
	{
		// Token: 0x06004C97 RID: 19607 RVA: 0x00106629 File Offset: 0x00104829
		public IdentityCollectionRequest()
		{
			this.IdentityCollection = new IdentityCollection();
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x0010663C File Offset: 0x0010483C
		// (set) Token: 0x06004C99 RID: 19609 RVA: 0x00106644 File Offset: 0x00104844
		[DataMember(IsRequired = true)]
		public IdentityCollection IdentityCollection { get; set; }

		// Token: 0x06004C9A RID: 19610 RVA: 0x0010664D File Offset: 0x0010484D
		public override string ToString()
		{
			return string.Format("IdentityCollectionRequest: {0}", this.IdentityCollection);
		}
	}
}
