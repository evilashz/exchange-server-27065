using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200032A RID: 810
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class AddNewTelUriContactToGroupType : BaseRequestType
	{
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00028D4D File Offset: 0x00026F4D
		// (set) Token: 0x06001A44 RID: 6724 RVA: 0x00028D55 File Offset: 0x00026F55
		public string TelUriAddress
		{
			get
			{
				return this.telUriAddressField;
			}
			set
			{
				this.telUriAddressField = value;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00028D5E File Offset: 0x00026F5E
		// (set) Token: 0x06001A46 RID: 6726 RVA: 0x00028D66 File Offset: 0x00026F66
		public string ImContactSipUriAddress
		{
			get
			{
				return this.imContactSipUriAddressField;
			}
			set
			{
				this.imContactSipUriAddressField = value;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00028D6F File Offset: 0x00026F6F
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x00028D77 File Offset: 0x00026F77
		public string ImTelephoneNumber
		{
			get
			{
				return this.imTelephoneNumberField;
			}
			set
			{
				this.imTelephoneNumberField = value;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00028D80 File Offset: 0x00026F80
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x00028D88 File Offset: 0x00026F88
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

		// Token: 0x0400119C RID: 4508
		private string telUriAddressField;

		// Token: 0x0400119D RID: 4509
		private string imContactSipUriAddressField;

		// Token: 0x0400119E RID: 4510
		private string imTelephoneNumberField;

		// Token: 0x0400119F RID: 4511
		private ItemIdType groupIdField;
	}
}
