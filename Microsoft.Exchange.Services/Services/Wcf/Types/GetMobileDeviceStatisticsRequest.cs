using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AE8 RID: 2792
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMobileDeviceStatisticsRequest : BaseJsonRequest
	{
		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06004F66 RID: 20326 RVA: 0x00108994 File Offset: 0x00106B94
		// (set) Token: 0x06004F67 RID: 20327 RVA: 0x0010899C File Offset: 0x00106B9C
		[DataMember(IsRequired = true)]
		public MobileDeviceStatisticsOptions Options { get; set; }

		// Token: 0x06004F68 RID: 20328 RVA: 0x001089A5 File Offset: 0x00106BA5
		public override string ToString()
		{
			return string.Format("GetMobileDeviceStatisticsRequest: {0}", this.Options);
		}
	}
}
