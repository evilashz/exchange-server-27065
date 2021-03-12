using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E6 RID: 998
	[DataContract(Name = "ListGroupResults", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ListGroupResults : ListResults
	{
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x0008D01B File Offset: 0x0008B21B
		// (set) Token: 0x06001851 RID: 6225 RVA: 0x0008D023 File Offset: 0x0008B223
		[DataMember]
		public Group[] Results
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

		// Token: 0x04001125 RID: 4389
		private Group[] ResultsField;
	}
}
