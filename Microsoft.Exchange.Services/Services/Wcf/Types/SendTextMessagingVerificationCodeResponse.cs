using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A8C RID: 2700
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendTextMessagingVerificationCodeResponse : OptionsResponseBase
	{
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06004C61 RID: 19553 RVA: 0x001063B5 File Offset: 0x001045B5
		// (set) Token: 0x06004C62 RID: 19554 RVA: 0x001063BD File Offset: 0x001045BD
		[DataMember]
		public string[] WarningMessages { get; internal set; }
	}
}
