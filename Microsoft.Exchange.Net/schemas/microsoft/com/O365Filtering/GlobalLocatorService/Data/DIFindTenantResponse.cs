using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C26 RID: 3110
	[DataContract(Name = "DIFindTenantResponse", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class DIFindTenantResponse : DIResponseBase
	{
		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x0600442D RID: 17453 RVA: 0x000B6AA0 File Offset: 0x000B4CA0
		// (set) Token: 0x0600442E RID: 17454 RVA: 0x000B6AA8 File Offset: 0x000B4CA8
		[DataMember]
		public DITenantInfo DITenantInformation
		{
			get
			{
				return this.DITenantInformationField;
			}
			set
			{
				this.DITenantInformationField = value;
			}
		}

		// Token: 0x040039D7 RID: 14807
		private DITenantInfo DITenantInformationField;
	}
}
