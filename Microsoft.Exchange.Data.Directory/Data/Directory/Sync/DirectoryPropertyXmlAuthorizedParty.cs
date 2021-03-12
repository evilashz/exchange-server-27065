using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200087E RID: 2174
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlAuthorizedParty : DirectoryPropertyXml
	{
		// Token: 0x06006D3E RID: 27966 RVA: 0x001754B1 File Offset: 0x001736B1
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D3F RID: 27967 RVA: 0x001754C7 File Offset: 0x001736C7
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueAuthorizedParty[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026FF RID: 9983
		// (get) Token: 0x06006D40 RID: 27968 RVA: 0x001754F7 File Offset: 0x001736F7
		// (set) Token: 0x06006D41 RID: 27969 RVA: 0x001754FF File Offset: 0x001736FF
		[XmlElement("Value", Order = 0)]
		public XmlValueAuthorizedParty[] Value
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

		// Token: 0x04004771 RID: 18289
		private XmlValueAuthorizedParty[] valueField;
	}
}
