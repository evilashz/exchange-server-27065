using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AE6 RID: 2790
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ClearMobileDeviceRequest : BaseJsonRequest
	{
		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06004F61 RID: 20321 RVA: 0x00108961 File Offset: 0x00106B61
		// (set) Token: 0x06004F62 RID: 20322 RVA: 0x00108969 File Offset: 0x00106B69
		[DataMember(IsRequired = true)]
		public ClearMobileDeviceOptions Options { get; set; }

		// Token: 0x06004F63 RID: 20323 RVA: 0x00108972 File Offset: 0x00106B72
		public override string ToString()
		{
			return string.Format("ClearMobileDeviceRequest: {0}", this.Options);
		}
	}
}
