using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002C9 RID: 713
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetServicePrincipalByAppPrincipalIdRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class GetServicePrincipalByAppPrincipalIdRequest : Request
	{
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0008AA8B File Offset: 0x00088C8B
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0008AA93 File Offset: 0x00088C93
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

		// Token: 0x04000F21 RID: 3873
		private Guid AppPrincipalIdField;
	}
}
