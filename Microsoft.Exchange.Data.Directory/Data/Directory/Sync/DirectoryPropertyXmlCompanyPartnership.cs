using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200087F RID: 2175
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyXmlCompanyPartnershipSingle))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlCompanyPartnership : DirectoryPropertyXml
	{
		// Token: 0x06006D43 RID: 27971 RVA: 0x00175510 File Offset: 0x00173710
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D44 RID: 27972 RVA: 0x00175526 File Offset: 0x00173726
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueCompanyPartnership[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002700 RID: 9984
		// (get) Token: 0x06006D45 RID: 27973 RVA: 0x00175556 File Offset: 0x00173756
		// (set) Token: 0x06006D46 RID: 27974 RVA: 0x0017555E File Offset: 0x0017375E
		[XmlElement("Value", Order = 0)]
		public XmlValueCompanyPartnership[] Value
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

		// Token: 0x04004772 RID: 18290
		private XmlValueCompanyPartnership[] valueField;
	}
}
