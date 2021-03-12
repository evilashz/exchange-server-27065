using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A96 RID: 2710
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class IdentityCollection
	{
		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06004C92 RID: 19602 RVA: 0x001065C5 File Offset: 0x001047C5
		// (set) Token: 0x06004C93 RID: 19603 RVA: 0x001065CD File Offset: 0x001047CD
		[DataMember(EmitDefaultValue = false)]
		public Identity[] Identities { get; set; }

		// Token: 0x06004C94 RID: 19604 RVA: 0x001065E0 File Offset: 0x001047E0
		public override string ToString()
		{
			IEnumerable<string> values = from e in this.Identities
			select e.ToString();
			return string.Join(";", values);
		}
	}
}
