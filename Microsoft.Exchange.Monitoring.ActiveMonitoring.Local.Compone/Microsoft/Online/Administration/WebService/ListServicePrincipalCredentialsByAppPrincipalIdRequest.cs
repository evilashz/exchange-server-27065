using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D5 RID: 725
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ListServicePrincipalCredentialsByAppPrincipalIdRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListServicePrincipalCredentialsByAppPrincipalIdRequest : Request
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0008AC1D File Offset: 0x00088E1D
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x0008AC25 File Offset: 0x00088E25
		[DataMember]
		public Guid AppPrincipalId
		{
			get
			{
				return this.AppPrincipalIdField;
			}
			set
			{
				this.AppPrincipalIdField = value;
			}
		}

		// Token: 0x04000F33 RID: 3891
		private Guid AppPrincipalIdField;
	}
}
