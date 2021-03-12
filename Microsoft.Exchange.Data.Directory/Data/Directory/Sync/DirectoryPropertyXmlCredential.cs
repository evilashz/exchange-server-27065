using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000881 RID: 2177
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlCredential : DirectoryPropertyXml
	{
		// Token: 0x06006D4D RID: 27981 RVA: 0x001755CE File Offset: 0x001737CE
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D4E RID: 27982 RVA: 0x001755E4 File Offset: 0x001737E4
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueCredential[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002702 RID: 9986
		// (get) Token: 0x06006D4F RID: 27983 RVA: 0x00175614 File Offset: 0x00173814
		// (set) Token: 0x06006D50 RID: 27984 RVA: 0x0017561C File Offset: 0x0017381C
		[XmlElement("Value", Order = 0)]
		public XmlValueCredential[] Value
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

		// Token: 0x04004774 RID: 18292
		private XmlValueCredential[] valueField;
	}
}
