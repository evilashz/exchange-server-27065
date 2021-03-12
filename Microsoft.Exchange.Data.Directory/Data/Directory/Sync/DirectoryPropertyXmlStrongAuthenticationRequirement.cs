using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000894 RID: 2196
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[Serializable]
	public class DirectoryPropertyXmlStrongAuthenticationRequirement : DirectoryPropertyXml
	{
		// Token: 0x06006DA8 RID: 28072 RVA: 0x00175B90 File Offset: 0x00173D90
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006DA9 RID: 28073 RVA: 0x00175BA1 File Offset: 0x00173DA1
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueStrongAuthenticationRequirement[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002714 RID: 10004
		// (get) Token: 0x06006DAA RID: 28074 RVA: 0x00175BD1 File Offset: 0x00173DD1
		// (set) Token: 0x06006DAB RID: 28075 RVA: 0x00175BD9 File Offset: 0x00173DD9
		[XmlElement("Value", Order = 0)]
		public XmlValueStrongAuthenticationRequirement[] Value
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

		// Token: 0x04004786 RID: 18310
		private XmlValueStrongAuthenticationRequirement[] valueField;
	}
}
