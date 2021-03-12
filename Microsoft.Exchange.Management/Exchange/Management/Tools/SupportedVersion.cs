using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.Tools
{
	// Token: 0x02001248 RID: 4680
	[DesignerCategory("code")]
	[XmlInclude(typeof(CustomSupportedVersion))]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SupportedVersion
	{
		// Token: 0x17003B7D RID: 15229
		// (get) Token: 0x0600BC58 RID: 48216 RVA: 0x002ACF75 File Offset: 0x002AB175
		// (set) Token: 0x0600BC59 RID: 48217 RVA: 0x002ACF7D File Offset: 0x002AB17D
		[XmlAttribute]
		public string minSupportedVersion
		{
			get
			{
				return this.minSupportedVersionField;
			}
			set
			{
				this.minSupportedVersionField = value;
			}
		}

		// Token: 0x17003B7E RID: 15230
		// (get) Token: 0x0600BC5A RID: 48218 RVA: 0x002ACF86 File Offset: 0x002AB186
		// (set) Token: 0x0600BC5B RID: 48219 RVA: 0x002ACF8E File Offset: 0x002AB18E
		[XmlAttribute]
		public string latestVersion
		{
			get
			{
				return this.latestVersionField;
			}
			set
			{
				this.latestVersionField = value;
			}
		}

		// Token: 0x17003B7F RID: 15231
		// (get) Token: 0x0600BC5C RID: 48220 RVA: 0x002ACF97 File Offset: 0x002AB197
		// (set) Token: 0x0600BC5D RID: 48221 RVA: 0x002ACF9F File Offset: 0x002AB19F
		[XmlAttribute(DataType = "anyURI")]
		public string updateUrl
		{
			get
			{
				return this.updateUrlField;
			}
			set
			{
				this.updateUrlField = value;
			}
		}

		// Token: 0x04006730 RID: 26416
		private string minSupportedVersionField;

		// Token: 0x04006731 RID: 26417
		private string latestVersionField;

		// Token: 0x04006732 RID: 26418
		private string updateUrlField;
	}
}
