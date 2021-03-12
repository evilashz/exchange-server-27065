using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200032A RID: 810
	[DataContract(Name = "NavigateRoleMemberResultsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class NavigateRoleMemberResultsResponse : Response
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x0008B7EF File Offset: 0x000899EF
		// (set) Token: 0x0600156F RID: 5487 RVA: 0x0008B7F7 File Offset: 0x000899F7
		[DataMember]
		public ListRoleMemberResults ReturnValue
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

		// Token: 0x04000FBD RID: 4029
		private ListRoleMemberResults ReturnValueField;
	}
}
