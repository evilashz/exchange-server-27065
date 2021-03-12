using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000307 RID: 775
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "VerifyDomainRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class VerifyDomainRequest : Request
	{
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0008B3FC File Offset: 0x000895FC
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x0008B404 File Offset: 0x00089604
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

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0008B40D File Offset: 0x0008960D
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x0008B415 File Offset: 0x00089615
		[DataMember]
		public DomainFederationSettings FederationSettings
		{
			get
			{
				return this.FederationSettingsField;
			}
			set
			{
				this.FederationSettingsField = value;
			}
		}

		// Token: 0x04000F92 RID: 3986
		private string DomainNameField;

		// Token: 0x04000F93 RID: 3987
		private DomainFederationSettings FederationSettingsField;
	}
}
