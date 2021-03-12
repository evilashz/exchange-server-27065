using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C39 RID: 3129
	[DebuggerStepThrough]
	[DataContract(Name = "FindTenantResponse", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class FindTenantResponse : ResponseBase
	{
		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x000B6D8B File Offset: 0x000B4F8B
		// (set) Token: 0x06004487 RID: 17543 RVA: 0x000B6D93 File Offset: 0x000B4F93
		[DataMember]
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

		// Token: 0x04003A01 RID: 14849
		private TenantInfo TenantInfoField;
	}
}
