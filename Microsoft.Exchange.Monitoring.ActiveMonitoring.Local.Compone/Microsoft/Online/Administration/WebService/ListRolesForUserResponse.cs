using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000327 RID: 807
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListRolesForUserResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class ListRolesForUserResponse : Response
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0008B7A4 File Offset: 0x000899A4
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x0008B7AC File Offset: 0x000899AC
		[DataMember]
		public Role[] ReturnValue
		{
			get
			{
				return this.ReturnValueField;
			}
			set
			{
				this.ReturnValueField = value;
			}
		}

		// Token: 0x04000FBA RID: 4026
		private Role[] ReturnValueField;
	}
}
