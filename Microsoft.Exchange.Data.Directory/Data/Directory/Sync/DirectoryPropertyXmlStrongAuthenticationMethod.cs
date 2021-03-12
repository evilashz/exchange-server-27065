using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000892 RID: 2194
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlStrongAuthenticationMethod : DirectoryPropertyXml
	{
		// Token: 0x06006D9E RID: 28062 RVA: 0x00175ADC File Offset: 0x00173CDC
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D9F RID: 28063 RVA: 0x00175AED File Offset: 0x00173CED
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueStrongAuthenticationMethod[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002712 RID: 10002
		// (get) Token: 0x06006DA0 RID: 28064 RVA: 0x00175B1D File Offset: 0x00173D1D
		// (set) Token: 0x06006DA1 RID: 28065 RVA: 0x00175B25 File Offset: 0x00173D25
		[XmlElement("Value", Order = 0)]
		public XmlValueStrongAuthenticationMethod[] Value
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

		// Token: 0x04004784 RID: 18308
		private XmlValueStrongAuthenticationMethod[] valueField;
	}
}
