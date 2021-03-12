using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000334 RID: 820
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "NavigateUserResultsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class NavigateUserResultsResponse : Response
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0008B8E9 File Offset: 0x00089AE9
		// (set) Token: 0x0600158D RID: 5517 RVA: 0x0008B8F1 File Offset: 0x00089AF1
		[DataMember]
		public ListUserResults ReturnValue
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

		// Token: 0x04000FC7 RID: 4039
		private ListUserResults ReturnValueField;
	}
}
