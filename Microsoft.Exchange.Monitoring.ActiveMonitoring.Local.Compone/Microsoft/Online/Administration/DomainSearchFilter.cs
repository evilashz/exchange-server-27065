using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003DA RID: 986
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainSearchFilter", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	public class DomainSearchFilter : IExtensibleDataObject
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0008CD67 File Offset: 0x0008AF67
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x0008CD6F File Offset: 0x0008AF6F
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x0008CD78 File Offset: 0x0008AF78
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x0008CD80 File Offset: 0x0008AF80
		[DataMember]
		public DomainAuthenticationType? Authentication
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

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0008CD89 File Offset: 0x0008AF89
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x0008CD91 File Offset: 0x0008AF91
		[DataMember]
		public DomainCapabilities? Capability
		{
			get
			{
				return this.CapabilityField;
			}
			set
			{
				this.CapabilityField = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0008CD9A File Offset: 0x0008AF9A
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x0008CDA2 File Offset: 0x0008AFA2
		[DataMember]
		public DomainStatus? Status
		{
			get
			{
				return this.StatusField;
			}
			set
			{
				this.StatusField = value;
			}
		}

		// Token: 0x040010F9 RID: 4345
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010FA RID: 4346
		private DomainAuthenticationType? AuthenticationField;

		// Token: 0x040010FB RID: 4347
		private DomainCapabilities? CapabilityField;

		// Token: 0x040010FC RID: 4348
		private DomainStatus? StatusField;
	}
}
