using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D6 RID: 726
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListServicePrincipalCredentialsBySpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListServicePrincipalCredentialsBySpnRequest : Request
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0008AC36 File Offset: 0x00088E36
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x0008AC3E File Offset: 0x00088E3E
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

		// Token: 0x04000F34 RID: 3892
		private string ServicePrincipalNameField;
	}
}
