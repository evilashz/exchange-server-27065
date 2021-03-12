using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000376 RID: 886
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PlayOnPhoneType : BaseRequestType
	{
		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x00029CFB File Offset: 0x00027EFB
		// (set) Token: 0x06001C20 RID: 7200 RVA: 0x00029D03 File Offset: 0x00027F03
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00029D0C File Offset: 0x00027F0C
		// (set) Token: 0x06001C22 RID: 7202 RVA: 0x00029D14 File Offset: 0x00027F14
		public string DialString
		{
			get
			{
				return this.dialStringField;
			}
			set
			{
				this.dialStringField = value;
			}
		}

		// Token: 0x040012A5 RID: 4773
		private ItemIdType itemIdField;

		// Token: 0x040012A6 RID: 4774
		private string dialStringField;
	}
}
