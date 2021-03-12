using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003CE RID: 974
	[DataContract(Name = "UserLicense", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class UserLicense : IExtensibleDataObject
	{
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0008CA76 File Offset: 0x0008AC76
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x0008CA7E File Offset: 0x0008AC7E
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0008CA87 File Offset: 0x0008AC87
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x0008CA8F File Offset: 0x0008AC8F
		[DataMember]
		public AccountSkuIdentifier AccountSku
		{
			get
			{
				return this.AccountSkuField;
			}
			set
			{
				this.AccountSkuField = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0008CA98 File Offset: 0x0008AC98
		// (set) Token: 0x060017AA RID: 6058 RVA: 0x0008CAA0 File Offset: 0x0008ACA0
		[DataMember]
		public string AccountSkuId
		{
			get
			{
				return this.AccountSkuIdField;
			}
			set
			{
				this.AccountSkuIdField = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0008CAA9 File Offset: 0x0008ACA9
		// (set) Token: 0x060017AC RID: 6060 RVA: 0x0008CAB1 File Offset: 0x0008ACB1
		[DataMember]
		public ServiceStatus[] ServiceStatus
		{
			get
			{
				return this.ServiceStatusField;
			}
			set
			{
				this.ServiceStatusField = value;
			}
		}

		// Token: 0x040010B9 RID: 4281
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010BA RID: 4282
		private AccountSkuIdentifier AccountSkuField;

		// Token: 0x040010BB RID: 4283
		private string AccountSkuIdField;

		// Token: 0x040010BC RID: 4284
		private ServiceStatus[] ServiceStatusField;
	}
}
