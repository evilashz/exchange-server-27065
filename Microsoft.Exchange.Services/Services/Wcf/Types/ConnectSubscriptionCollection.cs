using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AF5 RID: 2805
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ConnectSubscriptionCollection
	{
		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06004FD5 RID: 20437 RVA: 0x00108FE5 File Offset: 0x001071E5
		// (set) Token: 0x06004FD6 RID: 20438 RVA: 0x00108FED File Offset: 0x001071ED
		[DataMember(IsRequired = true)]
		public ConnectSubscription[] ConnectSubscriptions { get; set; }

		// Token: 0x06004FD7 RID: 20439 RVA: 0x00109000 File Offset: 0x00107200
		public override string ToString()
		{
			IEnumerable<string> values = from e in this.ConnectSubscriptions
			select e.ToString();
			return string.Join(";", values);
		}
	}
}
