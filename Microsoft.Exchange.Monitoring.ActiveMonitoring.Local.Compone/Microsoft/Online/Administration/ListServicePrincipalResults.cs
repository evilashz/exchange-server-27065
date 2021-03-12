using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003E0 RID: 992
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "ListServicePrincipalResults", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class ListServicePrincipalResults : ListResults
	{
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0008CF85 File Offset: 0x0008B185
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x0008CF8D File Offset: 0x0008B18D
		[DataMember]
		public ServicePrincipal[] Results
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

		// Token: 0x0400111F RID: 4383
		private ServicePrincipal[] ResultsField;
	}
}
