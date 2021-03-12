using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000091 RID: 145
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class DomainResponse : AutodiscoverResponse
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0001F871 File Offset: 0x0001DA71
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0001F879 File Offset: 0x0001DA79
		[XmlArray(IsNullable = true)]
		public DomainSettingError[] DomainSettingErrors
		{
			get
			{
				return this.domainSettingErrorsField;
			}
			set
			{
				this.domainSettingErrorsField = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001F882 File Offset: 0x0001DA82
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x0001F88A File Offset: 0x0001DA8A
		[XmlArray(IsNullable = true)]
		public DomainSetting[] DomainSettings
		{
			get
			{
				return this.domainSettingsField;
			}
			set
			{
				this.domainSettingsField = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x0001F893 File Offset: 0x0001DA93
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0001F89B File Offset: 0x0001DA9B
		[XmlElement(IsNullable = true)]
		public string RedirectTarget
		{
			get
			{
				return this.redirectTargetField;
			}
			set
			{
				this.redirectTargetField = value;
			}
		}

		// Token: 0x0400033F RID: 831
		private DomainSettingError[] domainSettingErrorsField;

		// Token: 0x04000340 RID: 832
		private DomainSetting[] domainSettingsField;

		// Token: 0x04000341 RID: 833
		private string redirectTargetField;
	}
}
