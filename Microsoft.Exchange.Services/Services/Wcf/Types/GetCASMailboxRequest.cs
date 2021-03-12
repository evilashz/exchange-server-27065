using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AA2 RID: 2722
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCASMailboxRequest : BaseJsonRequest
	{
		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x00106918 File Offset: 0x00104B18
		// (set) Token: 0x06004CDC RID: 19676 RVA: 0x00106920 File Offset: 0x00104B20
		[DataMember(IsRequired = true)]
		public GetCASMailboxOptions Options { get; set; }

		// Token: 0x06004CDD RID: 19677 RVA: 0x00106929 File Offset: 0x00104B29
		public override string ToString()
		{
			return string.Format("GetCASMailboxOptions: {0}", this.Options);
		}
	}
}
