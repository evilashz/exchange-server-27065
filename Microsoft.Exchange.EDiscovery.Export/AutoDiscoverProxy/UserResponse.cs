using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000093 RID: 147
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UserResponse : AutodiscoverResponse
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0001F8C5 File Offset: 0x0001DAC5
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0001F8CD File Offset: 0x0001DACD
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

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0001F8D6 File Offset: 0x0001DAD6
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0001F8DE File Offset: 0x0001DADE
		[XmlArray(IsNullable = true)]
		public UserSettingError[] UserSettingErrors
		{
			get
			{
				return this.userSettingErrorsField;
			}
			set
			{
				this.userSettingErrorsField = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0001F8E7 File Offset: 0x0001DAE7
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x0001F8EF File Offset: 0x0001DAEF
		[XmlArray(IsNullable = true)]
		public UserSetting[] UserSettings
		{
			get
			{
				return this.userSettingsField;
			}
			set
			{
				this.userSettingsField = value;
			}
		}

		// Token: 0x04000343 RID: 835
		private string redirectTargetField;

		// Token: 0x04000344 RID: 836
		private UserSettingError[] userSettingErrorsField;

		// Token: 0x04000345 RID: 837
		private UserSetting[] userSettingsField;
	}
}
