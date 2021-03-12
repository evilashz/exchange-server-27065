using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AE9 RID: 2793
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMobileDeviceStatisticsResponse : OptionsResponseBase
	{
		// Token: 0x06004F6A RID: 20330 RVA: 0x001089BF File Offset: 0x00106BBF
		public GetMobileDeviceStatisticsResponse()
		{
			this.MobileDeviceStatisticsCollection = new MobileDeviceStatisticsCollection();
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06004F6B RID: 20331 RVA: 0x001089D2 File Offset: 0x00106BD2
		// (set) Token: 0x06004F6C RID: 20332 RVA: 0x001089DA File Offset: 0x00106BDA
		[DataMember(IsRequired = true)]
		public MobileDeviceStatisticsCollection MobileDeviceStatisticsCollection { get; set; }

		// Token: 0x06004F6D RID: 20333 RVA: 0x001089E3 File Offset: 0x00106BE3
		public override string ToString()
		{
			return string.Format("GetMobileDeviceStatisticsResponse: {0}", this.MobileDeviceStatisticsCollection);
		}
	}
}
