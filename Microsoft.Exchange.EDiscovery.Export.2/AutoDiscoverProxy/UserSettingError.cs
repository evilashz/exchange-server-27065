using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200008D RID: 141
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class UserSettingError
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0001F7A7 File Offset: 0x0001D9A7
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0001F7AF File Offset: 0x0001D9AF
		public ErrorCode ErrorCode
		{
			get
			{
				return this.errorCodeField;
			}
			set
			{
				this.errorCodeField = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
		[XmlElement(IsNullable = true)]
		public string ErrorMessage
		{
			get
			{
				return this.errorMessageField;
			}
			set
			{
				this.errorMessageField = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0001F7C9 File Offset: 0x0001D9C9
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x0001F7D1 File Offset: 0x0001D9D1
		[XmlElement(IsNullable = true)]
		public string SettingName
		{
			get
			{
				return this.settingNameField;
			}
			set
			{
				this.settingNameField = value;
			}
		}

		// Token: 0x04000335 RID: 821
		private ErrorCode errorCodeField;

		// Token: 0x04000336 RID: 822
		private string errorMessageField;

		// Token: 0x04000337 RID: 823
		private string settingNameField;
	}
}
