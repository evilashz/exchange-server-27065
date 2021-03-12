using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000873 RID: 2163
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "InvalidCompanyFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[Serializable]
	internal class InvalidCompanyFault : AdminServiceFault
	{
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x0006648B File Offset: 0x0006468B
		// (set) Token: 0x06002E4A RID: 11850 RVA: 0x00066493 File Offset: 0x00064693
		[DataMember]
		internal InvalidCompanyCode Code
		{
			get
			{
				return this.CodeField;
			}
			set
			{
				this.CodeField = value;
			}
		}

		// Token: 0x0400283D RID: 10301
		[OptionalField]
		private InvalidCompanyCode CodeField;
	}
}
