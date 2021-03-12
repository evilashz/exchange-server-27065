using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000B6 RID: 182
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RootItemIdType : BaseItemIdType
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001FDB3 File Offset: 0x0001DFB3
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0001FDBB File Offset: 0x0001DFBB
		[XmlAttribute]
		public string RootItemId
		{
			get
			{
				return this.rootItemIdField;
			}
			set
			{
				this.rootItemIdField = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0001FDC4 File Offset: 0x0001DFC4
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x0001FDCC File Offset: 0x0001DFCC
		[XmlAttribute]
		public string RootItemChangeKey
		{
			get
			{
				return this.rootItemChangeKeyField;
			}
			set
			{
				this.rootItemChangeKeyField = value;
			}
		}

		// Token: 0x04000557 RID: 1367
		private string rootItemIdField;

		// Token: 0x04000558 RID: 1368
		private string rootItemChangeKeyField;
	}
}
