using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000883 RID: 2179
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[Serializable]
	public class DirectoryPropertyXmlEncryptedSecretKey : DirectoryPropertyXml
	{
		// Token: 0x06006D57 RID: 27991 RVA: 0x0017568C File Offset: 0x0017388C
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D58 RID: 27992 RVA: 0x0017569D File Offset: 0x0017389D
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new XmlValueEncryptedSecretKey[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x17002704 RID: 9988
		// (get) Token: 0x06006D59 RID: 27993 RVA: 0x001756CD File Offset: 0x001738CD
		// (set) Token: 0x06006D5A RID: 27994 RVA: 0x001756D5 File Offset: 0x001738D5
		[XmlElement("Value", Order = 0)]
		public XmlValueEncryptedSecretKey[] Value
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

		// Token: 0x04004776 RID: 18294
		private XmlValueEncryptedSecretKey[] valueField;
	}
}
