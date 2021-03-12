using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x0200030A RID: 778
	[DebuggerStepThrough]
	[DataContract(Name = "SetDomainAuthenticationRequest", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class SetDomainAuthenticationRequest : Request
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0008B458 File Offset: 0x00089658
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x0008B460 File Offset: 0x00089660
		[DataMember]
		public DomainAuthenticationType Authentication
		{
			get
			{
				return this.AuthenticationField;
			}
			set
			{
				this.AuthenticationField = value;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0008B469 File Offset: 0x00089669
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x0008B471 File Offset: 0x00089671
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0008B47A File Offset: 0x0008967A
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0008B482 File Offset: 0x00089682
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

		// Token: 0x04000F96 RID: 3990
		private DomainAuthenticationType AuthenticationField;

		// Token: 0x04000F97 RID: 3991
		private string DomainNameField;

		// Token: 0x04000F98 RID: 3992
		private DomainFederationSettings FederationSettingsField;
	}
}
