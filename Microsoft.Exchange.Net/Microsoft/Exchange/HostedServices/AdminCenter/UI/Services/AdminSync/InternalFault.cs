using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000874 RID: 2164
	[DebuggerStepThrough]
	[DataContract(Name = "InternalFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class InternalFault : AdminServiceFault
	{
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x000664A4 File Offset: 0x000646A4
		// (set) Token: 0x06002E4D RID: 11853 RVA: 0x000664AC File Offset: 0x000646AC
		[DataMember]
		internal InternalFaultCode Code
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

		// Token: 0x0400283E RID: 10302
		[OptionalField]
		private InternalFaultCode CodeField;
	}
}
