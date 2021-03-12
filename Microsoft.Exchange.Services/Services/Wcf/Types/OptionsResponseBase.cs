using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A6A RID: 2666
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class OptionsResponseBase : BaseJsonResponse
	{
		// Token: 0x06004BA2 RID: 19362 RVA: 0x00105B7E File Offset: 0x00103D7E
		public OptionsResponseBase(OptionsActionError errorCode)
		{
			this.WasSuccessful = false;
			this.ErrorCode = errorCode;
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x00105B94 File Offset: 0x00103D94
		public OptionsResponseBase()
		{
			this.WasSuccessful = true;
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06004BA4 RID: 19364 RVA: 0x00105BA3 File Offset: 0x00103DA3
		// (set) Token: 0x06004BA5 RID: 19365 RVA: 0x00105BAB File Offset: 0x00103DAB
		[DataMember]
		public bool WasSuccessful { get; set; }

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06004BA6 RID: 19366 RVA: 0x00105BB4 File Offset: 0x00103DB4
		// (set) Token: 0x06004BA7 RID: 19367 RVA: 0x00105BBC File Offset: 0x00103DBC
		[DataMember]
		public OptionsActionError ErrorCode { get; set; }

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06004BA8 RID: 19368 RVA: 0x00105BC5 File Offset: 0x00103DC5
		// (set) Token: 0x06004BA9 RID: 19369 RVA: 0x00105BCD File Offset: 0x00103DCD
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06004BAA RID: 19370 RVA: 0x00105BD6 File Offset: 0x00103DD6
		// (set) Token: 0x06004BAB RID: 19371 RVA: 0x00105BDE File Offset: 0x00103DDE
		[DataMember]
		public string UserPrompt { get; set; }
	}
}
