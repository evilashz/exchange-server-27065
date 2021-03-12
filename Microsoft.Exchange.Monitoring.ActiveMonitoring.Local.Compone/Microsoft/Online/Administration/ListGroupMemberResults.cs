using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E1 RID: 993
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListGroupMemberResults", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class ListGroupMemberResults : ListResults
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0008CF9E File Offset: 0x0008B19E
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x0008CFA6 File Offset: 0x0008B1A6
		[DataMember]
		public GroupMember[] Results
		{
			get
			{
				return this.ResultsField;
			}
			set
			{
				this.ResultsField = value;
			}
		}

		// Token: 0x04001120 RID: 4384
		private GroupMember[] ResultsField;
	}
}
