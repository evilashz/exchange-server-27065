using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200030B RID: 779
	[DebuggerStepThrough]
	[DataContract(Name = "GetDomainFederationSettingsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GetDomainFederationSettingsRequest : Request
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0008B493 File Offset: 0x00089693
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x0008B49B File Offset: 0x0008969B
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

		// Token: 0x04000F99 RID: 3993
		private string DomainNameField;
	}
}
