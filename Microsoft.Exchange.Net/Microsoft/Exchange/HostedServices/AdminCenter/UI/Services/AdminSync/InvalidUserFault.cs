using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000878 RID: 2168
	[DataContract(Name = "InvalidUserFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class InvalidUserFault : AdminServiceFault
	{
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x00066508 File Offset: 0x00064708
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x00066510 File Offset: 0x00064710
		[DataMember]
		internal InvalidUserCode Code
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

		// Token: 0x04002842 RID: 10306
		[OptionalField]
		private InvalidUserCode CodeField;
	}
}
