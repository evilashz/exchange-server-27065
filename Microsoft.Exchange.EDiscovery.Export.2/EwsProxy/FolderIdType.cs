using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000F1 RID: 241
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FolderIdType : BaseFolderIdType
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00020E92 File Offset: 0x0001F092
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00020E9A File Offset: 0x0001F09A
		[XmlAttribute]
		public string Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00020EA3 File Offset: 0x0001F0A3
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x00020EAB File Offset: 0x0001F0AB
		[XmlAttribute]
		public string ChangeKey
		{
			get
			{
				return this.changeKeyField;
			}
			set
			{
				this.changeKeyField = value;
			}
		}

		// Token: 0x040007FF RID: 2047
		private string idField;

		// Token: 0x04000800 RID: 2048
		private string changeKeyField;
	}
}
