using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002CA RID: 714
	[DataContract(Name = "GetServicePrincipalBySpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class GetServicePrincipalBySpnRequest : Request
	{
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0008AAA4 File Offset: 0x00088CA4
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x0008AAAC File Offset: 0x00088CAC
		[DataMember]
		public string ServicePrincipalName
		{
			get
			{
				return this.ServicePrincipalNameField;
			}
			set
			{
				this.ServicePrincipalNameField = value;
			}
		}

		// Token: 0x04000F22 RID: 3874
		private string ServicePrincipalNameField;
	}
}
