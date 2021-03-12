using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000873 RID: 2163
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(DirectoryPropertyReferenceUserAndServicePrincipalSingle))]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryPropertyReferenceUserAndServicePrincipal : DirectoryPropertyReference
	{
		// Token: 0x06006D0B RID: 27915 RVA: 0x00175145 File Offset: 0x00173345
		public override IList GetValues()
		{
			return this.Value ?? DirectoryProperty.EmptyValues;
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x00175156 File Offset: 0x00173356
		public sealed override void SetValues(IList values)
		{
			if (values == DirectoryProperty.EmptyValues)
			{
				this.Value = null;
				return;
			}
			this.Value = new DirectoryReferenceUserAndServicePrincipal[values.Count];
			values.CopyTo(this.Value, 0);
		}

		// Token: 0x170026F5 RID: 9973
		// (get) Token: 0x06006D0D RID: 27917 RVA: 0x00175186 File Offset: 0x00173386
		// (set) Token: 0x06006D0E RID: 27918 RVA: 0x0017518E File Offset: 0x0017338E
		[XmlElement("Value", Order = 0)]
		public DirectoryReferenceUserAndServicePrincipal[] Value
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

		// Token: 0x04004767 RID: 18279
		private DirectoryReferenceUserAndServicePrincipal[] valueField;
	}
}
