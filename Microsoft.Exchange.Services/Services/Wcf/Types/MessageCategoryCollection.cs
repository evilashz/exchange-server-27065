using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AC1 RID: 2753
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MessageCategoryCollection
	{
		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06004E4E RID: 20046 RVA: 0x00107BB0 File Offset: 0x00105DB0
		// (set) Token: 0x06004E4F RID: 20047 RVA: 0x00107BB8 File Offset: 0x00105DB8
		[DataMember(IsRequired = true)]
		public MessageCategory[] MessageCategories { get; set; }

		// Token: 0x06004E50 RID: 20048 RVA: 0x00107BCC File Offset: 0x00105DCC
		public override string ToString()
		{
			IEnumerable<string> values = from e in this.MessageCategories
			select e.ToString();
			return string.Join(";", values);
		}
	}
}
