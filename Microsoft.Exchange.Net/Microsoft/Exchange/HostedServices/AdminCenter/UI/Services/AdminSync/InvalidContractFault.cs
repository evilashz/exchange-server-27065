using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000872 RID: 2162
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "InvalidContractFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[Serializable]
	internal class InvalidContractFault : AdminServiceFault
	{
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x00066472 File Offset: 0x00064672
		// (set) Token: 0x06002E47 RID: 11847 RVA: 0x0006647A File Offset: 0x0006467A
		[DataMember]
		internal InvalidContractCode Code
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

		// Token: 0x0400283C RID: 10300
		[OptionalField]
		private InvalidContractCode CodeField;
	}
}
