using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x02000738 RID: 1848
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "TrustedDocDomain", Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04")]
	[DebuggerStepThrough]
	public class TrustedDocDomain : IExtensibleDataObject
	{
		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x0010C7A4 File Offset: 0x0010A9A4
		// (set) Token: 0x06004188 RID: 16776 RVA: 0x0010C7AC File Offset: 0x0010A9AC
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

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x0010C7B5 File Offset: 0x0010A9B5
		// (set) Token: 0x0600418A RID: 16778 RVA: 0x0010C7BD File Offset: 0x0010A9BD
		[DataMember]
		public KeyInformation m_ttdki
		{
			get
			{
				return this.m_ttdkiField;
			}
			set
			{
				this.m_ttdkiField = value;
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x0010C7C6 File Offset: 0x0010A9C6
		// (set) Token: 0x0600418C RID: 16780 RVA: 0x0010C7CE File Offset: 0x0010A9CE
		[DataMember(Order = 1)]
		public string[] m_strLicensorCertChain
		{
			get
			{
				return this.m_strLicensorCertChainField;
			}
			set
			{
				this.m_strLicensorCertChainField = value;
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x0010C7D7 File Offset: 0x0010A9D7
		// (set) Token: 0x0600418E RID: 16782 RVA: 0x0010C7DF File Offset: 0x0010A9DF
		[DataMember(Order = 2)]
		public string[] m_astrRightsTemplates
		{
			get
			{
				return this.m_astrRightsTemplatesField;
			}
			set
			{
				this.m_astrRightsTemplatesField = value;
			}
		}

		// Token: 0x04002956 RID: 10582
		private ExtensionDataObject extensionDataField;

		// Token: 0x04002957 RID: 10583
		private KeyInformation m_ttdkiField;

		// Token: 0x04002958 RID: 10584
		private string[] m_strLicensorCertChainField;

		// Token: 0x04002959 RID: 10585
		private string[] m_astrRightsTemplatesField;
	}
}
