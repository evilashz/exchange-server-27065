using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AD6 RID: 2774
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCASMailboxRequest : BaseJsonRequest
	{
		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x0010840F File Offset: 0x0010660F
		// (set) Token: 0x06004EF8 RID: 20216 RVA: 0x00108417 File Offset: 0x00106617
		[DataMember(IsRequired = true)]
		public SetCASMailbox Options { get; set; }
	}
}
