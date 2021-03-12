using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002C1 RID: 705
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class GetClientExtensionUserParametersType
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00027AF1 File Offset: 0x00025CF1
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x00027AF9 File Offset: 0x00025CF9
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UserEnabledExtensions
		{
			get
			{
				return this.userEnabledExtensionsField;
			}
			set
			{
				this.userEnabledExtensionsField = value;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00027B02 File Offset: 0x00025D02
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x00027B0A File Offset: 0x00025D0A
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UserDisabledExtensions
		{
			get
			{
				return this.userDisabledExtensionsField;
			}
			set
			{
				this.userDisabledExtensionsField = value;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00027B13 File Offset: 0x00025D13
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x00027B1B File Offset: 0x00025D1B
		[XmlAttribute]
		public string UserId
		{
			get
			{
				return this.userIdField;
			}
			set
			{
				this.userIdField = value;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00027B24 File Offset: 0x00025D24
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x00027B2C File Offset: 0x00025D2C
		[XmlAttribute]
		public bool EnabledOnly
		{
			get
			{
				return this.enabledOnlyField;
			}
			set
			{
				this.enabledOnlyField = value;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x00027B35 File Offset: 0x00025D35
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x00027B3D File Offset: 0x00025D3D
		[XmlIgnore]
		public bool EnabledOnlySpecified
		{
			get
			{
				return this.enabledOnlyFieldSpecified;
			}
			set
			{
				this.enabledOnlyFieldSpecified = value;
			}
		}

		// Token: 0x0400105B RID: 4187
		private string[] userEnabledExtensionsField;

		// Token: 0x0400105C RID: 4188
		private string[] userDisabledExtensionsField;

		// Token: 0x0400105D RID: 4189
		private string userIdField;

		// Token: 0x0400105E RID: 4190
		private bool enabledOnlyField;

		// Token: 0x0400105F RID: 4191
		private bool enabledOnlyFieldSpecified;
	}
}
