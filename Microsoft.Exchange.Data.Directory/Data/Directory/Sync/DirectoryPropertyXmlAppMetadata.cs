using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000877 RID: 2167
	[XmlInclude(typeof(DirectoryPropertyXmlAppMetadataSingle))]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlAppMetadata : DirectoryPropertyXml
	{
		// Token: 0x06006D1B RID: 27931 RVA: 0x00175260 File Offset: 0x00173460
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x00175276 File Offset: 0x00173476
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
			}
		}

		// Token: 0x170026F8 RID: 9976
		// (get) Token: 0x06006D1D RID: 27933 RVA: 0x00175287 File Offset: 0x00173487
		// (set) Token: 0x06006D1E RID: 27934 RVA: 0x0017528F File Offset: 0x0017348F
		[XmlElement("Value", Order = 0)]
		public XmlValueAppMetadata[] Value
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

		// Token: 0x0400476A RID: 18282
		private XmlValueAppMetadata[] valueField;
	}
}
