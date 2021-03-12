using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC3 RID: 2755
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MessageClassificationCollection
	{
		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06004E59 RID: 20057 RVA: 0x00107C5C File Offset: 0x00105E5C
		// (set) Token: 0x06004E5A RID: 20058 RVA: 0x00107C64 File Offset: 0x00105E64
		[DataMember(IsRequired = true)]
		public MessageClassification[] MessageClassifications { get; set; }

		// Token: 0x06004E5B RID: 20059 RVA: 0x00107C78 File Offset: 0x00105E78
		public override string ToString()
		{
			IEnumerable<string> values = from e in this.MessageClassifications
			select e.ToString();
			return string.Join(";", values);
		}
	}
}
