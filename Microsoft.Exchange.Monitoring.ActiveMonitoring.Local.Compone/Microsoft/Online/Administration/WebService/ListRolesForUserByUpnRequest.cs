using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002DB RID: 731
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ListRolesForUserByUpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListRolesForUserByUpnRequest : Request
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0008ACC4 File Offset: 0x00088EC4
		// (set) Token: 0x0600141A RID: 5146 RVA: 0x0008ACCC File Offset: 0x00088ECC
		[DataMember]
		public string UserPrincipalName
		{
			get
			{
				return this.UserPrincipalNameField;
			}
			set
			{
				this.UserPrincipalNameField = value;
			}
		}

		// Token: 0x04000F3A RID: 3898
		private string UserPrincipalNameField;
	}
}
