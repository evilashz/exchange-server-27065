using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AD4 RID: 2772
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendAddressCollection
	{
		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x00108381 File Offset: 0x00106581
		// (set) Token: 0x06004EEE RID: 20206 RVA: 0x00108389 File Offset: 0x00106589
		[DataMember(IsRequired = true)]
		public SendAddressData[] SendAddresses { get; set; }

		// Token: 0x06004EEF RID: 20207 RVA: 0x0010839C File Offset: 0x0010659C
		public override string ToString()
		{
			IEnumerable<string> values = from e in this.SendAddresses
			select e.ToString();
			return string.Join(";", values);
		}
	}
}
