using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B11 RID: 2833
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SuiteStorageKeyType
	{
		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06005070 RID: 20592 RVA: 0x00109AD4 File Offset: 0x00107CD4
		// (set) Token: 0x06005071 RID: 20593 RVA: 0x00109ADC File Offset: 0x00107CDC
		[DataMember]
		public string Scope { get; set; }

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x00109AE5 File Offset: 0x00107CE5
		// (set) Token: 0x06005073 RID: 20595 RVA: 0x00109AED File Offset: 0x00107CED
		[DataMember]
		public string Namespace { get; set; }

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x00109AF6 File Offset: 0x00107CF6
		// (set) Token: 0x06005075 RID: 20597 RVA: 0x00109AFE File Offset: 0x00107CFE
		[DataMember]
		public string Name { get; set; }
	}
}
