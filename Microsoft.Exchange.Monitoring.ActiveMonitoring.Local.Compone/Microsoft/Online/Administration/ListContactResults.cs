using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E5 RID: 997
	[DataContract(Name = "ListContactResults", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ListContactResults : ListResults
	{
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0008D002 File Offset: 0x0008B202
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x0008D00A File Offset: 0x0008B20A
		[DataMember]
		public Contact[] Results
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

		// Token: 0x04001124 RID: 4388
		private Contact[] ResultsField;
	}
}
