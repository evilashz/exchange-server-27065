using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003EE RID: 1006
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ServiceInstanceInformation", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	public class ServiceInstanceInformation : IExtensibleDataObject
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0008D469 File Offset: 0x0008B669
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x0008D471 File Offset: 0x0008B671
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

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0008D47A File Offset: 0x0008B67A
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x0008D482 File Offset: 0x0008B682
		[DataMember]
		public string ServiceInstance
		{
			get
			{
				return this.ServiceInstanceField;
			}
			set
			{
				this.ServiceInstanceField = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0008D48B File Offset: 0x0008B68B
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x0008D493 File Offset: 0x0008B693
		[DataMember]
		public ServiceEndpoint[] ServiceInstanceEndpoints
		{
			get
			{
				return this.ServiceInstanceEndpointsField;
			}
			set
			{
				this.ServiceInstanceEndpointsField = value;
			}
		}

		// Token: 0x0400116C RID: 4460
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400116D RID: 4461
		private string ServiceInstanceField;

		// Token: 0x0400116E RID: 4462
		private ServiceEndpoint[] ServiceInstanceEndpointsField;
	}
}
