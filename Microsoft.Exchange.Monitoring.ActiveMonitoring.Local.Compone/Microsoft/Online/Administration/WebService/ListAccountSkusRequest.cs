using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x020002FB RID: 763
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ListAccountSkusRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class ListAccountSkusRequest : Request
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x0008B29D File Offset: 0x0008949D
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x0008B2A5 File Offset: 0x000894A5
		[DataMember]
		public Guid? AccountId
		{
			get
			{
				return this.AccountIdField;
			}
			set
			{
				this.AccountIdField = value;
			}
		}

		// Token: 0x04000F83 RID: 3971
		private Guid? AccountIdField;
	}
}
