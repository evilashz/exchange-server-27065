using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200084D RID: 2125
	[DebuggerStepThrough]
	[DataContract(Name = "DomainConfigurationSettings", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[Serializable]
	internal class DomainConfigurationSettings : ConfigurationSettings
	{
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x00065DA8 File Offset: 0x00063FA8
		// (set) Token: 0x06002D70 RID: 11632 RVA: 0x00065DB0 File Offset: 0x00063FB0
		[DataMember]
		internal int[] ConnectorId
		{
			get
			{
				return this.ConnectorIdField;
			}
			set
			{
				this.ConnectorIdField = value;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x00065DB9 File Offset: 0x00063FB9
		// (set) Token: 0x06002D72 RID: 11634 RVA: 0x00065DC1 File Offset: 0x00063FC1
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

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x00065DCA File Offset: 0x00063FCA
		// (set) Token: 0x06002D74 RID: 11636 RVA: 0x00065DD2 File Offset: 0x00063FD2
		[DataMember]
		internal string DomainName
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

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x00065DDB File Offset: 0x00063FDB
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x00065DE3 File Offset: 0x00063FE3
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

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x00065DEC File Offset: 0x00063FEC
		// (set) Token: 0x06002D78 RID: 11640 RVA: 0x00065DF4 File Offset: 0x00063FF4
		[DataMember]
		internal DomainMailFlowType MailFlowType
		{
			get
			{
				return this.MailFlowTypeField;
			}
			set
			{
				this.MailFlowTypeField = value;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x00065DFD File Offset: 0x00063FFD
		// (set) Token: 0x06002D7A RID: 11642 RVA: 0x00065E05 File Offset: 0x00064005
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

		// Token: 0x0400278A RID: 10122
		[OptionalField]
		private int[] ConnectorIdField;

		// Token: 0x0400278B RID: 10123
		[OptionalField]
		private Guid? DomainGuidField;

		// Token: 0x0400278C RID: 10124
		[OptionalField]
		private string DomainNameField;

		// Token: 0x0400278D RID: 10125
		[OptionalField]
		private InheritanceSettings InheritFromCompanyField;

		// Token: 0x0400278E RID: 10126
		[OptionalField]
		private DomainMailFlowType MailFlowTypeField;

		// Token: 0x0400278F RID: 10127
		[OptionalField]
		private string SmtpProfileNameField;
	}
}
