using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.Tools
{
	// Token: 0x02001246 RID: 4678
	[XmlRoot(Namespace = "", IsNullable = false)]
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[Serializable]
	public class supportedTools
	{
		// Token: 0x17003B79 RID: 15225
		// (get) Token: 0x0600BC4E RID: 48206 RVA: 0x002ACF21 File Offset: 0x002AB121
		// (set) Token: 0x0600BC4F RID: 48207 RVA: 0x002ACF29 File Offset: 0x002AB129
		[XmlElement("toolInformation", Form = XmlSchemaForm.Unqualified)]
		public ToolInfoData[] toolInformation
		{
			get
			{
				return this.toolInformationField;
			}
			set
			{
				this.toolInformationField = value;
			}
		}

		// Token: 0x0400672C RID: 26412
		private ToolInfoData[] toolInformationField;
	}
}
