using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003EF RID: 1007
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ServiceEndpoint", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class ServiceEndpoint : IExtensibleDataObject
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0008D4A4 File Offset: 0x0008B6A4
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x0008D4AC File Offset: 0x0008B6AC
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

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0008D4B5 File Offset: 0x0008B6B5
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x0008D4BD File Offset: 0x0008B6BD
		[DataMember]
		public string Address
		{
			get
			{
				return this.AddressField;
			}
			set
			{
				this.AddressField = value;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0008D4C6 File Offset: 0x0008B6C6
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x0008D4CE File Offset: 0x0008B6CE
		[DataMember]
		public string Name
		{
			get
			{
				return this.NameField;
			}
			set
			{
				this.NameField = value;
			}
		}

		// Token: 0x0400116F RID: 4463
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001170 RID: 4464
		private string AddressField;

		// Token: 0x04001171 RID: 4465
		private string NameField;
	}
}
