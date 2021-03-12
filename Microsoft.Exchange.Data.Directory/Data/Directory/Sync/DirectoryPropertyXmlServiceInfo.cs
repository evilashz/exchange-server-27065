using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200088D RID: 2189
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlServiceInfo : DirectoryPropertyXml
	{
		// Token: 0x06006D85 RID: 28037 RVA: 0x0017597B File Offset: 0x00173B7B
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D86 RID: 28038 RVA: 0x00175991 File Offset: 0x00173B91
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueServiceInfo[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x1700270D RID: 9997
		// (get) Token: 0x06006D87 RID: 28039 RVA: 0x001759C1 File Offset: 0x00173BC1
		// (set) Token: 0x06006D88 RID: 28040 RVA: 0x001759C9 File Offset: 0x00173BC9
		[XmlElement("Value", Order = 0)]
		public XmlValueServiceInfo[] Value
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

		// Token: 0x0400477F RID: 18303
		private XmlValueServiceInfo[] valueField;
	}
}
