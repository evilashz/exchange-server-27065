using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000326 RID: 806
	[DataContract(Name = "ListRolesResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ListRolesResponse : Response
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0008B78B File Offset: 0x0008998B
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x0008B793 File Offset: 0x00089993
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

		// Token: 0x04000FB9 RID: 4025
		private Role[] ReturnValueField;
	}
}
