using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C2E RID: 3118
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "TenantQuery", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class TenantQuery : IExtensibleDataObject
	{
		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x000B6C56 File Offset: 0x000B4E56
		// (set) Token: 0x06004462 RID: 17506 RVA: 0x000B6C5E File Offset: 0x000B4E5E
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

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x000B6C67 File Offset: 0x000B4E67
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x000B6C6F File Offset: 0x000B4E6F
		[DataMember]
		public string[] PropertyNames
		{
			get
			{
				return this.PropertyNamesField;
			}
			set
			{
				this.PropertyNamesField = value;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x000B6C78 File Offset: 0x000B4E78
		// (set) Token: 0x06004466 RID: 17510 RVA: 0x000B6C80 File Offset: 0x000B4E80
		[DataMember(IsRequired = true)]
		public Guid TenantId
		{
			get
			{
				return this.TenantIdField;
			}
			set
			{
				this.TenantIdField = value;
			}
		}

		// Token: 0x040039F4 RID: 14836
		private ExtensionDataObject extensionDataField;

		// Token: 0x040039F5 RID: 14837
		private string[] PropertyNamesField;

		// Token: 0x040039F6 RID: 14838
		private Guid TenantIdField;
	}
}
