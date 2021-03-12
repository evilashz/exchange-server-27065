using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E3 RID: 995
	[DebuggerStepThrough]
	[DataContract(Name = "ListUserResults", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ListUserResults : ListResults
	{
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0008CFD0 File Offset: 0x0008B1D0
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x0008CFD8 File Offset: 0x0008B1D8
		[DataMember]
		public User[] Results
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

		// Token: 0x04001122 RID: 4386
		private User[] ResultsField;
	}
}
