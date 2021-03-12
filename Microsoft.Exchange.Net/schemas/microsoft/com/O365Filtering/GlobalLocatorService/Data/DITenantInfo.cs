using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C28 RID: 3112
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "DITenantInfo", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DITenantInfo : DITimeStamp
	{
		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06004439 RID: 17465 RVA: 0x000B6B05 File Offset: 0x000B4D05
		// (set) Token: 0x0600443A RID: 17466 RVA: 0x000B6B0D File Offset: 0x000B4D0D
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

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x0600443B RID: 17467 RVA: 0x000B6B16 File Offset: 0x000B4D16
		// (set) Token: 0x0600443C RID: 17468 RVA: 0x000B6B1E File Offset: 0x000B4D1E
		[DataMember]
		public GLSProperty[] TenantProperties
		{
			get
			{
				return this.TenantPropertiesField;
			}
			set
			{
				this.TenantPropertiesField = value;
			}
		}

		// Token: 0x040039DC RID: 14812
		private Guid TenantIdField;

		// Token: 0x040039DD RID: 14813
		private GLSProperty[] TenantPropertiesField;
	}
}
