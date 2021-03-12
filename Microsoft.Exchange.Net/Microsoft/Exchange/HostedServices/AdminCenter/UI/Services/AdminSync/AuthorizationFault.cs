using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000875 RID: 2165
	[DataContract(Name = "AuthorizationFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class AuthorizationFault : AdminServiceFault
	{
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x000664BD File Offset: 0x000646BD
		// (set) Token: 0x06002E50 RID: 11856 RVA: 0x000664C5 File Offset: 0x000646C5
		[DataMember]
		internal AuthorizationFaultCode Code
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

		// Token: 0x0400283F RID: 10303
		[OptionalField]
		private AuthorizationFaultCode CodeField;
	}
}
