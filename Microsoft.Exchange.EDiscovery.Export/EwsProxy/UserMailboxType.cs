using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001AB RID: 427
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class UserMailboxType
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00024873 File Offset: 0x00022A73
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x0002487B File Offset: 0x00022A7B
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

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00024884 File Offset: 0x00022A84
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x0002488C File Offset: 0x00022A8C
		[XmlAttribute]
		public bool IsArchive
		{
			get
			{
				return this.isArchiveField;
			}
			set
			{
				this.isArchiveField = value;
			}
		}

		// Token: 0x04000C52 RID: 3154
		private string idField;

		// Token: 0x04000C53 RID: 3155
		private bool isArchiveField;
	}
}
