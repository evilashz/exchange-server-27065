using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AE4 RID: 2788
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscriptionCollection
	{
		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x001088BF File Offset: 0x00106ABF
		// (set) Token: 0x06004F58 RID: 20312 RVA: 0x001088C7 File Offset: 0x00106AC7
		[DataMember(IsRequired = true)]
		public Subscription[] Subscriptions { get; set; }

		// Token: 0x06004F59 RID: 20313 RVA: 0x001088D8 File Offset: 0x00106AD8
		public override string ToString()
		{
			IEnumerable<string> values = from e in this.Subscriptions
			select e.ToString();
			return string.Join(";", values);
		}
	}
}
