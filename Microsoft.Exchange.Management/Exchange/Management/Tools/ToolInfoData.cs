using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.Tools
{
	// Token: 0x02001247 RID: 4679
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[Serializable]
	public class ToolInfoData
	{
		// Token: 0x17003B7A RID: 15226
		// (get) Token: 0x0600BC51 RID: 48209 RVA: 0x002ACF3A File Offset: 0x002AB13A
		// (set) Token: 0x0600BC52 RID: 48210 RVA: 0x002ACF42 File Offset: 0x002AB142
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public SupportedVersion defaultSupportedVersion
		{
			get
			{
				return this.defaultSupportedVersionField;
			}
			set
			{
				this.defaultSupportedVersionField = value;
			}
		}

		// Token: 0x17003B7B RID: 15227
		// (get) Token: 0x0600BC53 RID: 48211 RVA: 0x002ACF4B File Offset: 0x002AB14B
		// (set) Token: 0x0600BC54 RID: 48212 RVA: 0x002ACF53 File Offset: 0x002AB153
		[XmlElement("customSupportedVersion", Form = XmlSchemaForm.Unqualified)]
		public CustomSupportedVersion[] customSupportedVersion
		{
			get
			{
				return this.customSupportedVersionField;
			}
			set
			{
				this.customSupportedVersionField = value;
			}
		}

		// Token: 0x17003B7C RID: 15228
		// (get) Token: 0x0600BC55 RID: 48213 RVA: 0x002ACF5C File Offset: 0x002AB15C
		// (set) Token: 0x0600BC56 RID: 48214 RVA: 0x002ACF64 File Offset: 0x002AB164
		[XmlAttribute]
		public ToolId id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x0400672D RID: 26413
		private SupportedVersion defaultSupportedVersionField;

		// Token: 0x0400672E RID: 26414
		private CustomSupportedVersion[] customSupportedVersionField;

		// Token: 0x0400672F RID: 26415
		private ToolId idField;
	}
}
