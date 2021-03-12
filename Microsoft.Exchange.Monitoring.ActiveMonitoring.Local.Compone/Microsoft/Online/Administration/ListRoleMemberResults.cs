using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E2 RID: 994
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ListRoleMemberResults", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class ListRoleMemberResults : ListResults
	{
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0008CFB7 File Offset: 0x0008B1B7
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x0008CFBF File Offset: 0x0008B1BF
		[DataMember]
		public RoleMember[] Results
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

		// Token: 0x04001121 RID: 4385
		private RoleMember[] ResultsField;
	}
}
