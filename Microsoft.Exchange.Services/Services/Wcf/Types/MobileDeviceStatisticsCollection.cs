using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AEB RID: 2795
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MobileDeviceStatisticsCollection
	{
		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06004FB7 RID: 20407 RVA: 0x00108E14 File Offset: 0x00107014
		// (set) Token: 0x06004FB8 RID: 20408 RVA: 0x00108E1C File Offset: 0x0010701C
		[DataMember(IsRequired = true)]
		public MobileDeviceStatistics[] MobileDevices { get; set; }

		// Token: 0x06004FB9 RID: 20409 RVA: 0x00108E30 File Offset: 0x00107030
		public override string ToString()
		{
			IEnumerable<string> values = from e in this.MobileDevices
			select e.ToString();
			return string.Join(";", values);
		}
	}
}
