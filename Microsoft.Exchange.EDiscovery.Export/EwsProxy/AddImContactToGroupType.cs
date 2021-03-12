using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000329 RID: 809
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AddImContactToGroupType : BaseRequestType
	{
		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00028D23 File Offset: 0x00026F23
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00028D2B File Offset: 0x00026F2B
		public ItemIdType ContactId
		{
			get
			{
				return this.contactIdField;
			}
			set
			{
				this.contactIdField = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00028D34 File Offset: 0x00026F34
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x00028D3C File Offset: 0x00026F3C
		public ItemIdType GroupId
		{
			get
			{
				return this.groupIdField;
			}
			set
			{
				this.groupIdField = value;
			}
		}

		// Token: 0x0400119A RID: 4506
		private ItemIdType contactIdField;

		// Token: 0x0400119B RID: 4507
		private ItemIdType groupIdField;
	}
}
