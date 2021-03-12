using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200030D RID: 781
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetDomainRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class GetDomainRequest : Request
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0008B4D6 File Offset: 0x000896D6
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x0008B4DE File Offset: 0x000896DE
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

		// Token: 0x04000F9C RID: 3996
		private string DomainNameField;
	}
}
