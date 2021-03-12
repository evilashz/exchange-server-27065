using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000B3 RID: 179
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(RecurringMasterItemIdRangesType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ItemIdType : BaseItemIdType
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0001FD24 File Offset: 0x0001DF24
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0001FD2C File Offset: 0x0001DF2C
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

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0001FD35 File Offset: 0x0001DF35
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x0001FD3D File Offset: 0x0001DF3D
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

		// Token: 0x04000550 RID: 1360
		private string idField;

		// Token: 0x04000551 RID: 1361
		private string changeKeyField;
	}
}
