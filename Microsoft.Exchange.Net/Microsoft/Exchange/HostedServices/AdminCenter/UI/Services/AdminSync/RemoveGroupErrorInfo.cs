using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000877 RID: 2167
	[DataContract(Name = "RemoveGroupErrorInfo", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class RemoveGroupErrorInfo : AdminServiceFault
	{
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000664EF File Offset: 0x000646EF
		// (set) Token: 0x06002E56 RID: 11862 RVA: 0x000664F7 File Offset: 0x000646F7
		[DataMember]
		internal RemoveGroupErrorCode Code
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

		// Token: 0x04002841 RID: 10305
		[OptionalField]
		private RemoveGroupErrorCode CodeField;
	}
}
