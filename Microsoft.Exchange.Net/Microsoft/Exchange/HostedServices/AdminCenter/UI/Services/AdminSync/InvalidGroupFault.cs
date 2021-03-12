using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000876 RID: 2166
	[DebuggerStepThrough]
	[DataContract(Name = "InvalidGroupFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class InvalidGroupFault : AdminServiceFault
	{
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x000664D6 File Offset: 0x000646D6
		// (set) Token: 0x06002E53 RID: 11859 RVA: 0x000664DE File Offset: 0x000646DE
		[DataMember]
		internal InvalidGroupCode Code
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

		// Token: 0x04002840 RID: 10304
		[OptionalField]
		private InvalidGroupCode CodeField;
	}
}
