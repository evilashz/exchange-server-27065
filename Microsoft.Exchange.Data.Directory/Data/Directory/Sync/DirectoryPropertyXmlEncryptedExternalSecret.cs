using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000884 RID: 2180
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DirectoryPropertyXmlEncryptedExternalSecretSingle))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyXmlEncryptedExternalSecret : DirectoryPropertyXml
	{
		// Token: 0x06006D5C RID: 27996 RVA: 0x001756E6 File Offset: 0x001738E6
		public override IList GetValues()
		{
			if (this.Value != null)
			{
				return this.Value;
			}
			return DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D5D RID: 27997 RVA: 0x001756FC File Offset: 0x001738FC
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
			}
		}

		// Token: 0x17002705 RID: 9989
		// (get) Token: 0x06006D5E RID: 27998 RVA: 0x0017570D File Offset: 0x0017390D
		// (set) Token: 0x06006D5F RID: 27999 RVA: 0x00175715 File Offset: 0x00173915
		[XmlElement("Value", Order = 0)]
		public XmlValueEncryptedExternalSecret[] Value
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

		// Token: 0x04004777 RID: 18295
		private XmlValueEncryptedExternalSecret[] valueField;
	}
}
