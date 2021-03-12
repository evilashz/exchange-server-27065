using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C23 RID: 3107
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DIFindTenantRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	public class DIFindTenantRequest : DIRequestBase
	{
		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06004420 RID: 17440 RVA: 0x000B6A33 File Offset: 0x000B4C33
		// (set) Token: 0x06004421 RID: 17441 RVA: 0x000B6A3B File Offset: 0x000B4C3B
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

		// Token: 0x040039D2 RID: 14802
		private Guid TenantIdField;
	}
}
