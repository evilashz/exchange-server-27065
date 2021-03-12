using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000081 RID: 129
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DomainSettingError
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001F573 File Offset: 0x0001D773
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x0001F57B File Offset: 0x0001D77B
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

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0001F584 File Offset: 0x0001D784
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x0001F58C File Offset: 0x0001D78C
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

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001F595 File Offset: 0x0001D795
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0001F59D File Offset: 0x0001D79D
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

		// Token: 0x0400030D RID: 781
		private ErrorCode errorCodeField;

		// Token: 0x0400030E RID: 782
		private string errorMessageField;

		// Token: 0x0400030F RID: 783
		private string settingNameField;
	}
}
