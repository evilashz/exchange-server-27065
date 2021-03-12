using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200030C RID: 780
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SetDomainFederationSettingsRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[DebuggerStepThrough]
	public class SetDomainFederationSettingsRequest : Request
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0008B4AC File Offset: 0x000896AC
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x0008B4B4 File Offset: 0x000896B4
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

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0008B4BD File Offset: 0x000896BD
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x0008B4C5 File Offset: 0x000896C5
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

		// Token: 0x04000F9A RID: 3994
		private string DomainNameField;

		// Token: 0x04000F9B RID: 3995
		private DomainFederationSettings FederationSettingsField;
	}
}
