using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x02000861 RID: 2145
	[DataContract(Name = "Domain", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class Domain : IExtensibleDataObject
	{
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x00066031 File Offset: 0x00064231
		// (set) Token: 0x06002DBD RID: 11709 RVA: 0x00066039 File Offset: 0x00064239
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

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x00066042 File Offset: 0x00064242
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x0006604A File Offset: 0x0006424A
		[DataMember]
		internal bool CatchAll
		{
			get
			{
				return this.CatchAllField;
			}
			set
			{
				this.CatchAllField = value;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x00066053 File Offset: 0x00064253
		// (set) Token: 0x06002DC1 RID: 11713 RVA: 0x0006605B File Offset: 0x0006425B
		[DataMember]
		internal int CompanyId
		{
			get
			{
				return this.CompanyIdField;
			}
			set
			{
				this.CompanyIdField = value;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x00066064 File Offset: 0x00064264
		// (set) Token: 0x06002DC3 RID: 11715 RVA: 0x0006606C File Offset: 0x0006426C
		[DataMember]
		internal DateTime DateCreated
		{
			get
			{
				return this.DateCreatedField;
			}
			set
			{
				this.DateCreatedField = value;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x00066075 File Offset: 0x00064275
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x0006607D File Offset: 0x0006427D
		[DataMember]
		internal Guid? DomainGuid
		{
			get
			{
				return this.DomainGuidField;
			}
			set
			{
				this.DomainGuidField = value;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x00066086 File Offset: 0x00064286
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x0006608E File Offset: 0x0006428E
		[DataMember]
		internal int DomainId
		{
			get
			{
				return this.DomainIdField;
			}
			set
			{
				this.DomainIdField = value;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x00066097 File Offset: 0x00064297
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x0006609F File Offset: 0x0006429F
		[DataMember]
		internal InheritanceSettings InheritFromCompany
		{
			get
			{
				return this.InheritFromCompanyField;
			}
			set
			{
				this.InheritFromCompanyField = value;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000660A8 File Offset: 0x000642A8
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x000660B0 File Offset: 0x000642B0
		[DataMember]
		internal bool IsEnabled
		{
			get
			{
				return this.IsEnabledField;
			}
			set
			{
				this.IsEnabledField = value;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x000660B9 File Offset: 0x000642B9
		// (set) Token: 0x06002DCD RID: 11725 RVA: 0x000660C1 File Offset: 0x000642C1
		[DataMember]
		internal bool IsValid
		{
			get
			{
				return this.IsValidField;
			}
			set
			{
				this.IsValidField = value;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000660CA File Offset: 0x000642CA
		// (set) Token: 0x06002DCF RID: 11727 RVA: 0x000660D2 File Offset: 0x000642D2
		[DataMember]
		internal string MailServer
		{
			get
			{
				return this.MailServerField;
			}
			set
			{
				this.MailServerField = value;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06002DD0 RID: 11728 RVA: 0x000660DB File Offset: 0x000642DB
		// (set) Token: 0x06002DD1 RID: 11729 RVA: 0x000660E3 File Offset: 0x000642E3
		[DataMember]
		internal MailServerType MailServerType
		{
			get
			{
				return this.MailServerTypeField;
			}
			set
			{
				this.MailServerTypeField = value;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06002DD2 RID: 11730 RVA: 0x000660EC File Offset: 0x000642EC
		// (set) Token: 0x06002DD3 RID: 11731 RVA: 0x000660F4 File Offset: 0x000642F4
		[DataMember]
		internal string Name
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

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06002DD4 RID: 11732 RVA: 0x000660FD File Offset: 0x000642FD
		// (set) Token: 0x06002DD5 RID: 11733 RVA: 0x00066105 File Offset: 0x00064305
		[DataMember]
		internal int RetentionPeriod
		{
			get
			{
				return this.RetentionPeriodField;
			}
			set
			{
				this.RetentionPeriodField = value;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06002DD6 RID: 11734 RVA: 0x0006610E File Offset: 0x0006430E
		// (set) Token: 0x06002DD7 RID: 11735 RVA: 0x00066116 File Offset: 0x00064316
		[DataMember]
		internal DomainConfigurationSettings Settings
		{
			get
			{
				return this.SettingsField;
			}
			set
			{
				this.SettingsField = value;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06002DD8 RID: 11736 RVA: 0x0006611F File Offset: 0x0006431F
		// (set) Token: 0x06002DD9 RID: 11737 RVA: 0x00066127 File Offset: 0x00064327
		[DataMember]
		internal string SmtpProfileName
		{
			get
			{
				return this.SmtpProfileNameField;
			}
			set
			{
				this.SmtpProfileNameField = value;
			}
		}

		// Token: 0x04002811 RID: 10257
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002812 RID: 10258
		[OptionalField]
		private bool CatchAllField;

		// Token: 0x04002813 RID: 10259
		[OptionalField]
		private int CompanyIdField;

		// Token: 0x04002814 RID: 10260
		[OptionalField]
		private DateTime DateCreatedField;

		// Token: 0x04002815 RID: 10261
		[OptionalField]
		private Guid? DomainGuidField;

		// Token: 0x04002816 RID: 10262
		[OptionalField]
		private int DomainIdField;

		// Token: 0x04002817 RID: 10263
		[OptionalField]
		private InheritanceSettings InheritFromCompanyField;

		// Token: 0x04002818 RID: 10264
		[OptionalField]
		private bool IsEnabledField;

		// Token: 0x04002819 RID: 10265
		[OptionalField]
		private bool IsValidField;

		// Token: 0x0400281A RID: 10266
		[OptionalField]
		private string MailServerField;

		// Token: 0x0400281B RID: 10267
		[OptionalField]
		private MailServerType MailServerTypeField;

		// Token: 0x0400281C RID: 10268
		[OptionalField]
		private string NameField;

		// Token: 0x0400281D RID: 10269
		[OptionalField]
		private int RetentionPeriodField;

		// Token: 0x0400281E RID: 10270
		[OptionalField]
		private DomainConfigurationSettings SettingsField;

		// Token: 0x0400281F RID: 10271
		[OptionalField]
		private string SmtpProfileNameField;
	}
}
