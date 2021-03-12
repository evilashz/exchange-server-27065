using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200088C RID: 2188
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyXmlRightsManagementUserKeySingle))]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class DirectoryPropertyXmlRightsManagementUserKey : DirectoryPropertyXml
	{
		// Token: 0x06006D80 RID: 28032 RVA: 0x00175921 File Offset: 0x00173B21
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D81 RID: 28033 RVA: 0x00175932 File Offset: 0x00173B32
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueRightsManagementUserKey[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x1700270C RID: 9996
		// (get) Token: 0x06006D82 RID: 28034 RVA: 0x00175962 File Offset: 0x00173B62
		// (set) Token: 0x06006D83 RID: 28035 RVA: 0x0017596A File Offset: 0x00173B6A
		[XmlElement("Value", Order = 0)]
		public XmlValueRightsManagementUserKey[] Value
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

		// Token: 0x0400477E RID: 18302
		private XmlValueRightsManagementUserKey[] valueField;
	}
}
