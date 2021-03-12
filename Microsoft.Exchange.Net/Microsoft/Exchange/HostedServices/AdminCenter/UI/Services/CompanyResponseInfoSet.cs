using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000858 RID: 2136
	[DebuggerStepThrough]
	[DataContract(Name = "CompanyResponseInfoSet", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class CompanyResponseInfoSet : ResponseInfoSet
	{
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x00065F0A File Offset: 0x0006410A
		// (set) Token: 0x06002D9A RID: 11674 RVA: 0x00065F12 File Offset: 0x00064112
		[DataMember]
		internal CompanyResponseInfo[] ResponseInfo
		{
			get
			{
				return this.ResponseInfoField;
			}
			set
			{
				this.ResponseInfoField = value;
			}
		}

		// Token: 0x040027B3 RID: 10163
		[OptionalField]
		private CompanyResponseInfo[] ResponseInfoField;
	}
}
