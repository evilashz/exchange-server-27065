using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000889 RID: 2185
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlProvisionedPlan : DirectoryPropertyXml
	{
		// Token: 0x06006D71 RID: 28017 RVA: 0x0017580E File Offset: 0x00173A0E
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D72 RID: 28018 RVA: 0x00175824 File Offset: 0x00173A24
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueProvisionedPlan[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002709 RID: 9993
		// (get) Token: 0x06006D73 RID: 28019 RVA: 0x00175854 File Offset: 0x00173A54
		// (set) Token: 0x06006D74 RID: 28020 RVA: 0x0017585C File Offset: 0x00173A5C
		[XmlElement("Value", Order = 0)]
		public XmlValueProvisionedPlan[] Value
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

		// Token: 0x0400477B RID: 18299
		private XmlValueProvisionedPlan[] valueField;
	}
}
