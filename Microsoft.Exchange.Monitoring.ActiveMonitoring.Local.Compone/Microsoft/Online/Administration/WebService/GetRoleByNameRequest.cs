using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002D9 RID: 729
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetRoleByNameRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class GetRoleByNameRequest : Request
	{
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0008AC92 File Offset: 0x00088E92
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x0008AC9A File Offset: 0x00088E9A
		[DataMember]
		public string RoleName
		{
			get
			{
				return this.RoleNameField;
			}
			set
			{
				this.RoleNameField = value;
			}
		}

		// Token: 0x04000F38 RID: 3896
		private string RoleNameField;
	}
}
