using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C3F RID: 3135
	[DataContract(Name = "SaveTenantRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class SaveTenantRequest : IExtensibleDataObject
	{
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x000B6F0F File Offset: 0x000B510F
		// (set) Token: 0x060044B5 RID: 17589 RVA: 0x000B6F17 File Offset: 0x000B5117
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

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x000B6F20 File Offset: 0x000B5120
		// (set) Token: 0x060044B7 RID: 17591 RVA: 0x000B6F28 File Offset: 0x000B5128
		[DataMember]
		public string CustomTag
		{
			get
			{
				return this.CustomTagField;
			}
			set
			{
				this.CustomTagField = value;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x000B6F31 File Offset: 0x000B5131
		// (set) Token: 0x060044B9 RID: 17593 RVA: 0x000B6F39 File Offset: 0x000B5139
		[DataMember(IsRequired = true)]
		public TenantInfo TenantInfo
		{
			get
			{
				return this.TenantInfoField;
			}
			set
			{
				this.TenantInfoField = value;
			}
		}

		// Token: 0x04003A15 RID: 14869
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A16 RID: 14870
		private string CustomTagField;

		// Token: 0x04003A17 RID: 14871
		private TenantInfo TenantInfoField;
	}
}
