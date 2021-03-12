using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D4 RID: 980
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Domain", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class Domain : IExtensibleDataObject
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0008CC25 File Offset: 0x0008AE25
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x0008CC2D File Offset: 0x0008AE2D
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

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0008CC36 File Offset: 0x0008AE36
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x0008CC3E File Offset: 0x0008AE3E
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

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0008CC47 File Offset: 0x0008AE47
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0008CC4F File Offset: 0x0008AE4F
		[DataMember]
		public DomainCapabilities? Capabilities
		{
			get
			{
				return this.CapabilitiesField;
			}
			set
			{
				this.CapabilitiesField = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0008CC58 File Offset: 0x0008AE58
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x0008CC60 File Offset: 0x0008AE60
		[DataMember]
		public bool? IsDefault
		{
			get
			{
				return this.IsDefaultField;
			}
			set
			{
				this.IsDefaultField = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0008CC69 File Offset: 0x0008AE69
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x0008CC71 File Offset: 0x0008AE71
		[DataMember]
		public bool? IsInitial
		{
			get
			{
				return this.IsInitialField;
			}
			set
			{
				this.IsInitialField = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0008CC7A File Offset: 0x0008AE7A
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x0008CC82 File Offset: 0x0008AE82
		[DataMember]
		public string Name
		{
			get
			{
				return this.NameField;
			}
			set
			{
				this.NameField = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0008CC8B File Offset: 0x0008AE8B
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x0008CC93 File Offset: 0x0008AE93
		[DataMember]
		public string RootDomain
		{
			get
			{
				return this.RootDomainField;
			}
			set
			{
				this.RootDomainField = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0008CC9C File Offset: 0x0008AE9C
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x0008CCA4 File Offset: 0x0008AEA4
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

		// Token: 0x040010D5 RID: 4309
		private ExtensionDataObject extensionDataField;

		// Token: 0x040010D6 RID: 4310
		private DomainAuthenticationType? AuthenticationField;

		// Token: 0x040010D7 RID: 4311
		private DomainCapabilities? CapabilitiesField;

		// Token: 0x040010D8 RID: 4312
		private bool? IsDefaultField;

		// Token: 0x040010D9 RID: 4313
		private bool? IsInitialField;

		// Token: 0x040010DA RID: 4314
		private string NameField;

		// Token: 0x040010DB RID: 4315
		private string RootDomainField;

		// Token: 0x040010DC RID: 4316
		private DomainStatus? StatusField;
	}
}
