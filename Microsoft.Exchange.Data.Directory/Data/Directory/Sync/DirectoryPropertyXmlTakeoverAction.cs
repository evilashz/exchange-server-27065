using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000895 RID: 2197
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlTakeoverAction : DirectoryPropertyXml
	{
		// Token: 0x06006DAD RID: 28077 RVA: 0x00175BEA File Offset: 0x00173DEA
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006DAE RID: 28078 RVA: 0x00175C00 File Offset: 0x00173E00
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
			}
		}

		// Token: 0x17002715 RID: 10005
		// (get) Token: 0x06006DAF RID: 28079 RVA: 0x00175C11 File Offset: 0x00173E11
		// (set) Token: 0x06006DB0 RID: 28080 RVA: 0x00175C19 File Offset: 0x00173E19
		[XmlElement("Value", Order = 0)]
		public XmlValueTakeoverAction[] Value
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

		// Token: 0x04004787 RID: 18311
		private XmlValueTakeoverAction[] valueField;
	}
}
