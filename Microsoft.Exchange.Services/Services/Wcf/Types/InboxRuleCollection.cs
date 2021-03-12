using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000ABB RID: 2747
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InboxRuleCollection
	{
		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06004DED RID: 19949 RVA: 0x00107642 File Offset: 0x00105842
		// (set) Token: 0x06004DEE RID: 19950 RVA: 0x0010764A File Offset: 0x0010584A
		[DataMember(IsRequired = true)]
		public InboxRule[] InboxRules { get; set; }

		// Token: 0x06004DEF RID: 19951 RVA: 0x0010765C File Offset: 0x0010585C
		public override string ToString()
		{
			IEnumerable<string> values = from e in this.InboxRules
			select e.ToString();
			return string.Join(";", values);
		}
	}
}
