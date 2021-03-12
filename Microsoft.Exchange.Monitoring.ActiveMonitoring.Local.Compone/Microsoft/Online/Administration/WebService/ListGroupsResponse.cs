using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200034C RID: 844
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListGroupsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class ListGroupsResponse : Response
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0008BBEB File Offset: 0x00089DEB
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0008BBF3 File Offset: 0x00089DF3
		[DataMember]
		public ListGroupResults ReturnValue
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

		// Token: 0x04000FE9 RID: 4073
		private ListGroupResults ReturnValueField;
	}
}
