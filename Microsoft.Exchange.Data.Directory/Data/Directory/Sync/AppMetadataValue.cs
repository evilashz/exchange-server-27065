using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000907 RID: 2311
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class AppMetadataValue
	{
		// Token: 0x1700276A RID: 10090
		// (get) Token: 0x06006EF8 RID: 28408 RVA: 0x001767E0 File Offset: 0x001749E0
		// (set) Token: 0x06006EF9 RID: 28409 RVA: 0x001767E8 File Offset: 0x001749E8
		[XmlElement(DataType = "base64Binary", Order = 0)]
		public byte[] Data
		{
			get
			{
				return this.dataField;
			}
			set
			{
				this.dataField = value;
			}
		}

		// Token: 0x1700276B RID: 10091
		// (get) Token: 0x06006EFA RID: 28410 RVA: 0x001767F1 File Offset: 0x001749F1
		// (set) Token: 0x06006EFB RID: 28411 RVA: 0x001767F9 File Offset: 0x001749F9
		[XmlAttribute]
		public int Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x04004816 RID: 18454
		private byte[] dataField;

		// Token: 0x04004817 RID: 18455
		private int versionField;
	}
}
