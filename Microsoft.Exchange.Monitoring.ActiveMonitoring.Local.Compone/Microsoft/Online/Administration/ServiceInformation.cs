using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003ED RID: 1005
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ServiceInformation", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	public class ServiceInformation : IExtensibleDataObject
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0008D42E File Offset: 0x0008B62E
		// (set) Token: 0x060018CC RID: 6348 RVA: 0x0008D436 File Offset: 0x0008B636
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

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0008D43F File Offset: 0x0008B63F
		// (set) Token: 0x060018CE RID: 6350 RVA: 0x0008D447 File Offset: 0x0008B647
		[DataMember]
		public ArrayOfXElement ServiceElements
		{
			get
			{
				return this.ServiceElementsField;
			}
			set
			{
				this.ServiceElementsField = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0008D450 File Offset: 0x0008B650
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x0008D458 File Offset: 0x0008B658
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

		// Token: 0x04001169 RID: 4457
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400116A RID: 4458
		private ArrayOfXElement ServiceElementsField;

		// Token: 0x0400116B RID: 4459
		private string ServiceInstanceField;
	}
}
