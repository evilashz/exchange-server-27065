using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200088F RID: 2191
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlServiceOriginatedResource : DirectoryPropertyXml
	{
		// Token: 0x06006D8F RID: 28047 RVA: 0x00175A01 File Offset: 0x00173C01
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D90 RID: 28048 RVA: 0x00175A12 File Offset: 0x00173C12
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueServiceOriginatedResource[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x1700270F RID: 9999
		// (get) Token: 0x06006D91 RID: 28049 RVA: 0x00175A42 File Offset: 0x00173C42
		// (set) Token: 0x06006D92 RID: 28050 RVA: 0x00175A4A File Offset: 0x00173C4A
		[XmlElement("Value", Order = 0)]
		public XmlValueServiceOriginatedResource[] Value
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

		// Token: 0x04004781 RID: 18305
		private XmlValueServiceOriginatedResource[] valueField;
	}
}
