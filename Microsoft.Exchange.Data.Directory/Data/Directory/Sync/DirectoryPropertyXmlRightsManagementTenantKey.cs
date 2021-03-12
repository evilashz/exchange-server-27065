using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200088B RID: 2187
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlRightsManagementTenantKey : DirectoryPropertyXml
	{
		// Token: 0x06006D7B RID: 28027 RVA: 0x001758C7 File Offset: 0x00173AC7
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D7C RID: 28028 RVA: 0x001758D8 File Offset: 0x00173AD8
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueRightsManagementTenantKey[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x1700270B RID: 9995
		// (get) Token: 0x06006D7D RID: 28029 RVA: 0x00175908 File Offset: 0x00173B08
		// (set) Token: 0x06006D7E RID: 28030 RVA: 0x00175910 File Offset: 0x00173B10
		[XmlElement("Value", Order = 0)]
		public XmlValueRightsManagementTenantKey[] Value
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

		// Token: 0x0400477D RID: 18301
		private XmlValueRightsManagementTenantKey[] valueField;
	}
}
