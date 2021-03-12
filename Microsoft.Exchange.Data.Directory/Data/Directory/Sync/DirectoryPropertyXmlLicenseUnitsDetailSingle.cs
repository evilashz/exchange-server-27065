using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000888 RID: 2184
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryPropertyXmlLicenseUnitsDetailSingle : DirectoryPropertyXmlLicenseUnitsDetail
	{
		// Token: 0x06006D6E RID: 28014 RVA: 0x001757C0 File Offset: 0x001739C0
		public override IList GetValues()
		{
			if (base.Value != null)
			{
				return base.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D6F RID: 28015 RVA: 0x001757D6 File Offset: 0x001739D6
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				base.Value = null;
				return;
			}
			base.Value = new XmlValueLicenseUnitsDetail[values.Count];
			values.CopyTo(base.Value, 0);
		}
	}
}
