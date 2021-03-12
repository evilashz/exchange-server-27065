using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C2D RID: 3117
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "FindTenantRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindTenantRequest : IExtensibleDataObject
	{
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x000B6C1B File Offset: 0x000B4E1B
		// (set) Token: 0x0600445B RID: 17499 RVA: 0x000B6C23 File Offset: 0x000B4E23
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

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x0600445C RID: 17500 RVA: 0x000B6C2C File Offset: 0x000B4E2C
		// (set) Token: 0x0600445D RID: 17501 RVA: 0x000B6C34 File Offset: 0x000B4E34
		[DataMember]
		public int ReadFlag
		{
			get
			{
				return this.ReadFlagField;
			}
			set
			{
				this.ReadFlagField = value;
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x0600445E RID: 17502 RVA: 0x000B6C3D File Offset: 0x000B4E3D
		// (set) Token: 0x0600445F RID: 17503 RVA: 0x000B6C45 File Offset: 0x000B4E45
		[DataMember(IsRequired = true)]
		public TenantQuery Tenant
		{
			get
			{
				return this.TenantField;
			}
			set
			{
				this.TenantField = value;
			}
		}

		// Token: 0x040039F1 RID: 14833
		private ExtensionDataObject extensionDataField;

		// Token: 0x040039F2 RID: 14834
		private int ReadFlagField;

		// Token: 0x040039F3 RID: 14835
		private TenantQuery TenantField;
	}
}
