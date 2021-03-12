using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000879 RID: 2169
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlStsAddress : DirectoryPropertyXml
	{
		// Token: 0x06006D25 RID: 27941 RVA: 0x001752E0 File Offset: 0x001734E0
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D26 RID: 27942 RVA: 0x001752F1 File Offset: 0x001734F1
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueStsAddress[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026FA RID: 9978
		// (get) Token: 0x06006D27 RID: 27943 RVA: 0x00175321 File Offset: 0x00173521
		// (set) Token: 0x06006D28 RID: 27944 RVA: 0x00175329 File Offset: 0x00173529
		[XmlElement("Value", Order = 0)]
		public XmlValueStsAddress[] Value
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

		// Token: 0x0400476C RID: 18284
		private XmlValueStsAddress[] valueField;
	}
}
