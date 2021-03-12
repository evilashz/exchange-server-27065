using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200030F RID: 783
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetDomainVerificationDnsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class GetDomainVerificationDnsRequest : Request
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x0008B508 File Offset: 0x00089708
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x0008B510 File Offset: 0x00089710
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

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x0008B519 File Offset: 0x00089719
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x0008B521 File Offset: 0x00089721
		[DataMember]
		public DomainVerificationMode? Mode
		{
			get
			{
				return this.ModeField;
			}
			set
			{
				this.ModeField = value;
			}
		}

		// Token: 0x04000F9E RID: 3998
		private string DomainNameField;

		// Token: 0x04000F9F RID: 3999
		private DomainVerificationMode? ModeField;
	}
}
