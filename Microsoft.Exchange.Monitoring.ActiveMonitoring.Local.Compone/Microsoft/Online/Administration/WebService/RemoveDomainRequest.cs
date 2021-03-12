using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000308 RID: 776
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "RemoveDomainRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class RemoveDomainRequest : Request
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0008B426 File Offset: 0x00089626
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0008B42E File Offset: 0x0008962E
		[DataMember]
		public string DomainName
		{
			get
			{
				return this.DomainNameField;
			}
			set
			{
				this.DomainNameField = value;
			}
		}

		// Token: 0x04000F94 RID: 3988
		private string DomainNameField;
	}
}
