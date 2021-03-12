using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E4 RID: 996
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListPartnerContractResults", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class ListPartnerContractResults : ListResults
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x0008CFE9 File Offset: 0x0008B1E9
		// (set) Token: 0x0600184B RID: 6219 RVA: 0x0008CFF1 File Offset: 0x0008B1F1
		[DataMember]
		public PartnerContract[] Results
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

		// Token: 0x04001123 RID: 4387
		private PartnerContract[] ResultsField;
	}
}
