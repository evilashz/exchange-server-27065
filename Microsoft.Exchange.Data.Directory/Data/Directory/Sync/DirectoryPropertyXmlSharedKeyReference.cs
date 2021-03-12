using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000890 RID: 2192
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryPropertyXmlSharedKeyReference : DirectoryPropertyXml
	{
		// Token: 0x06006D94 RID: 28052 RVA: 0x00175A5B File Offset: 0x00173C5B
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D95 RID: 28053 RVA: 0x00175A6C File Offset: 0x00173C6C
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueSharedKeyReference[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002710 RID: 10000
		// (get) Token: 0x06006D96 RID: 28054 RVA: 0x00175A9C File Offset: 0x00173C9C
		// (set) Token: 0x06006D97 RID: 28055 RVA: 0x00175AA4 File Offset: 0x00173CA4
		[XmlElement("Value", Order = 0)]
		public XmlValueSharedKeyReference[] Value
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

		// Token: 0x04004782 RID: 18306
		private XmlValueSharedKeyReference[] valueField;
	}
}
