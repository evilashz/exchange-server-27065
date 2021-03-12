using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000896 RID: 2198
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryPropertyXmlValidationError : DirectoryPropertyXml
	{
		// Token: 0x06006DB2 RID: 28082 RVA: 0x00175C2A File Offset: 0x00173E2A
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006DB3 RID: 28083 RVA: 0x00175C40 File Offset: 0x00173E40
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueValidationError[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002716 RID: 10006
		// (get) Token: 0x06006DB4 RID: 28084 RVA: 0x00175C70 File Offset: 0x00173E70
		// (set) Token: 0x06006DB5 RID: 28085 RVA: 0x00175C78 File Offset: 0x00173E78
		[XmlElement("Value", Order = 0)]
		public XmlValueValidationError[] Value
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

		// Token: 0x04004788 RID: 18312
		private XmlValueValidationError[] valueField;
	}
}
