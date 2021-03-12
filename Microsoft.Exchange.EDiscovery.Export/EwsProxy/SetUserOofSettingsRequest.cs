using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000363 RID: 867
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetUserOofSettingsRequest : BaseRequestType
	{
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x000299C3 File Offset: 0x00027BC3
		// (set) Token: 0x06001BBE RID: 7102 RVA: 0x000299CB File Offset: 0x00027BCB
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public EmailAddress Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001BBF RID: 7103 RVA: 0x000299D4 File Offset: 0x00027BD4
		// (set) Token: 0x06001BC0 RID: 7104 RVA: 0x000299DC File Offset: 0x00027BDC
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public UserOofSettings UserOofSettings
		{
			get
			{
				return this.userOofSettingsField;
			}
			set
			{
				this.userOofSettingsField = value;
			}
		}

		// Token: 0x0400127A RID: 4730
		private EmailAddress mailboxField;

		// Token: 0x0400127B RID: 4731
		private UserOofSettings userOofSettingsField;
	}
}
