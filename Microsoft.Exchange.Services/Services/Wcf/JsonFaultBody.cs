using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C8C RID: 3212
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class JsonFaultBody
	{
		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x0600570F RID: 22287 RVA: 0x0011198A File Offset: 0x0010FB8A
		// (set) Token: 0x06005710 RID: 22288 RVA: 0x00111992 File Offset: 0x0010FB92
		[DataMember]
		public string FaultMessage { get; set; }

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06005711 RID: 22289 RVA: 0x0011199B File Offset: 0x0010FB9B
		// (set) Token: 0x06005712 RID: 22290 RVA: 0x001119A3 File Offset: 0x0010FBA3
		[DataMember]
		public int ErrorCode { get; set; }

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06005713 RID: 22291 RVA: 0x001119AC File Offset: 0x0010FBAC
		// (set) Token: 0x06005714 RID: 22292 RVA: 0x001119B4 File Offset: 0x0010FBB4
		[DataMember(EmitDefaultValue = false)]
		public string StackTrace { get; set; }
	}
}
