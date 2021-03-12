using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200087B RID: 2171
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlAppAddress : DirectoryPropertyXml
	{
		// Token: 0x06006D2F RID: 27951 RVA: 0x00175399 File Offset: 0x00173599
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D30 RID: 27952 RVA: 0x001753AF File Offset: 0x001735AF
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueAppAddress[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026FC RID: 9980
		// (get) Token: 0x06006D31 RID: 27953 RVA: 0x001753DF File Offset: 0x001735DF
		// (set) Token: 0x06006D32 RID: 27954 RVA: 0x001753E7 File Offset: 0x001735E7
		[XmlElement("Value", Order = 0)]
		public XmlValueAppAddress[] Value
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

		// Token: 0x0400476E RID: 18286
		private XmlValueAppAddress[] valueField;
	}
}
