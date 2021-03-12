using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000871 RID: 2161
	[DataContract(Name = "ErrorInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class ErrorInfo : AdminServiceFault
	{
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x00066459 File Offset: 0x00064659
		// (set) Token: 0x06002E44 RID: 11844 RVA: 0x00066461 File Offset: 0x00064661
		[DataMember]
		internal ErrorCode Code
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

		// Token: 0x0400283B RID: 10299
		[OptionalField]
		private ErrorCode CodeField;
	}
}
