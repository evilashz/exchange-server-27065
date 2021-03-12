using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200087A RID: 2170
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlInclude(typeof(DirectoryPropertyXmlAnySingle))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlAny : DirectoryPropertyXml
	{
		// Token: 0x06006D2A RID: 27946 RVA: 0x0017533A File Offset: 0x0017353A
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D2B RID: 27947 RVA: 0x00175350 File Offset: 0x00173550
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlElement[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026FB RID: 9979
		// (get) Token: 0x06006D2C RID: 27948 RVA: 0x00175380 File Offset: 0x00173580
		// (set) Token: 0x06006D2D RID: 27949 RVA: 0x00175388 File Offset: 0x00173588
		[XmlElement("Value", Order = 0)]
		public XmlElement[] Value
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

		// Token: 0x0400476D RID: 18285
		private XmlElement[] valueField;
	}
}
