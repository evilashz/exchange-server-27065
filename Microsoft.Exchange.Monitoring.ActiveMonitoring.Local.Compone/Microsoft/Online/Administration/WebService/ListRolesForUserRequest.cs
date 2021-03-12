using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002DA RID: 730
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListRolesForUserRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListRolesForUserRequest : Request
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0008ACAB File Offset: 0x00088EAB
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x0008ACB3 File Offset: 0x00088EB3
		[DataMember]
		public Guid ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x04000F39 RID: 3897
		private Guid ObjectIdField;
	}
}
