using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200088A RID: 2186
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[XmlInclude(typeof(DirectoryPropertyXmlRightsManagementTenantConfigurationSingle))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlRightsManagementTenantConfiguration : DirectoryPropertyXml
	{
		// Token: 0x06006D76 RID: 28022 RVA: 0x0017586D File Offset: 0x00173A6D
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x0017587E File Offset: 0x00173A7E
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueRightsManagementTenantConfiguration[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x1700270A RID: 9994
		// (get) Token: 0x06006D78 RID: 28024 RVA: 0x001758AE File Offset: 0x00173AAE
		// (set) Token: 0x06006D79 RID: 28025 RVA: 0x001758B6 File Offset: 0x00173AB6
		[XmlElement("Value", Order = 0)]
		public XmlValueRightsManagementTenantConfiguration[] Value
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

		// Token: 0x0400477C RID: 18300
		private XmlValueRightsManagementTenantConfiguration[] valueField;
	}
}
