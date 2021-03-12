using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002E4 RID: 740
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RemoveUserByUpnRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class RemoveUserByUpnRequest : Request
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0008AE3E File Offset: 0x0008903E
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x0008AE46 File Offset: 0x00089046
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

		// Token: 0x04000F4C RID: 3916
		private string UserPrincipalNameField;
	}
}
