using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000328 RID: 808
	[DataContract(Name = "ListRolesForUserByUpnResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ListRolesForUserByUpnResponse : Response
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0008B7BD File Offset: 0x000899BD
		// (set) Token: 0x06001569 RID: 5481 RVA: 0x0008B7C5 File Offset: 0x000899C5
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

		// Token: 0x04000FBB RID: 4027
		private Role[] ReturnValueField;
	}
}
