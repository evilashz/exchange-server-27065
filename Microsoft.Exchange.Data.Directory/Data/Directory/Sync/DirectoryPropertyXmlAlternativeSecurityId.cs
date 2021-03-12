using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000876 RID: 2166
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlAlternativeSecurityId : DirectoryPropertyXml
	{
		// Token: 0x06006D16 RID: 27926 RVA: 0x00175206 File Offset: 0x00173406
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D17 RID: 27927 RVA: 0x00175217 File Offset: 0x00173417
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueAlternativeSecurityId[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026F7 RID: 9975
		// (get) Token: 0x06006D18 RID: 27928 RVA: 0x00175247 File Offset: 0x00173447
		// (set) Token: 0x06006D19 RID: 27929 RVA: 0x0017524F File Offset: 0x0017344F
		[XmlElement("Value", Order = 0)]
		public XmlValueAlternativeSecurityId[] Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x04004769 RID: 18281
		private XmlValueAlternativeSecurityId[] valueField;
	}
}
