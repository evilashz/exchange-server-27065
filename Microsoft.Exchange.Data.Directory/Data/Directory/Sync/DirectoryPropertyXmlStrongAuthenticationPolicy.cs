using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000893 RID: 2195
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlStrongAuthenticationPolicy : DirectoryPropertyXml
	{
		// Token: 0x06006DA3 RID: 28067 RVA: 0x00175B36 File Offset: 0x00173D36
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006DA4 RID: 28068 RVA: 0x00175B47 File Offset: 0x00173D47
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueStrongAuthenticationPolicy[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002713 RID: 10003
		// (get) Token: 0x06006DA5 RID: 28069 RVA: 0x00175B77 File Offset: 0x00173D77
		// (set) Token: 0x06006DA6 RID: 28070 RVA: 0x00175B7F File Offset: 0x00173D7F
		[XmlElement("Value", Order = 0)]
		public XmlValueStrongAuthenticationPolicy[] Value
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

		// Token: 0x04004785 RID: 18309
		private XmlValueStrongAuthenticationPolicy[] valueField;
	}
}
