using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000857 RID: 2135
	[DataContract(Name = "DomainResponseInfoSet", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class DomainResponseInfoSet : ResponseInfoSet
	{
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x00065EF1 File Offset: 0x000640F1
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x00065EF9 File Offset: 0x000640F9
		[DataMember]
		internal DomainResponseInfo[] ResponseInfo
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

		// Token: 0x040027B2 RID: 10162
		[OptionalField]
		private DomainResponseInfo[] ResponseInfoField;
	}
}
