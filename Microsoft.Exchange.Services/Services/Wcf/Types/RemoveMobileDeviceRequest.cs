using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AED RID: 2797
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveMobileDeviceRequest : BaseJsonRequest
	{
		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06004FC3 RID: 20419 RVA: 0x00108ED5 File Offset: 0x001070D5
		// (set) Token: 0x06004FC4 RID: 20420 RVA: 0x00108EDD File Offset: 0x001070DD
		[DataMember(IsRequired = true)]
		public Identity Identity { get; set; }

		// Token: 0x06004FC5 RID: 20421 RVA: 0x00108EE6 File Offset: 0x001070E6
		public override string ToString()
		{
			return string.Format("RemoveMobileDeviceRequest: {0}", this.Identity);
		}
	}
}
