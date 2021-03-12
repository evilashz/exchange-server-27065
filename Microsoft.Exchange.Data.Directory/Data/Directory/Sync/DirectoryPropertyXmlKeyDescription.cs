using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000886 RID: 2182
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryPropertyXmlKeyDescription : DirectoryPropertyXml
	{
		// Token: 0x06006D66 RID: 28006 RVA: 0x0017574D File Offset: 0x0017394D
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D67 RID: 28007 RVA: 0x0017575E File Offset: 0x0017395E
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueKeyDescription[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002707 RID: 9991
		// (get) Token: 0x06006D68 RID: 28008 RVA: 0x0017578E File Offset: 0x0017398E
		// (set) Token: 0x06006D69 RID: 28009 RVA: 0x00175796 File Offset: 0x00173996
		[XmlElement("Value", Order = 0)]
		public XmlValueKeyDescription[] Value
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

		// Token: 0x04004779 RID: 18297
		private XmlValueKeyDescription[] valueField;
	}
}
