using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200087D RID: 2173
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlAsymmetricKey : DirectoryPropertyXml
	{
		// Token: 0x06006D39 RID: 27961 RVA: 0x00175457 File Offset: 0x00173657
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D3A RID: 27962 RVA: 0x00175468 File Offset: 0x00173668
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueAsymmetricKey[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026FE RID: 9982
		// (get) Token: 0x06006D3B RID: 27963 RVA: 0x00175498 File Offset: 0x00173698
		// (set) Token: 0x06006D3C RID: 27964 RVA: 0x001754A0 File Offset: 0x001736A0
		[XmlElement("Value", Order = 0)]
		public XmlValueAsymmetricKey[] Value
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

		// Token: 0x04004770 RID: 18288
		private XmlValueAsymmetricKey[] valueField;
	}
}
