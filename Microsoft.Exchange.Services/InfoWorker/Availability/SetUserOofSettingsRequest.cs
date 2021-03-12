using System;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x02000010 RID: 16
	[XmlType("SetUserOofSettingsRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetUserOofSettingsRequest : BaseAvailabilityRequest
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003974 File Offset: 0x00001B74
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000397C File Offset: 0x00001B7C
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public EmailAddress Mailbox
		{
			get
			{
				return this.mailbox;
			}
			set
			{
				this.mailbox = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003985 File Offset: 0x00001B85
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000398D File Offset: 0x00001B8D
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public UserOofSettings UserOofSettings
		{
			get
			{
				return this.userOofSettings;
			}
			set
			{
				this.userOofSettings = value;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003996 File Offset: 0x00001B96
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SetUserOofSettings(callContext, this);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000039A0 File Offset: 0x00001BA0
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			MailboxIdServerInfo result = null;
			if (this.Mailbox != null)
			{
				string address = this.Mailbox.Address;
				if (Util.IsValidSmtpAddress(address))
				{
					result = MailboxIdServerInfo.Create(address);
				}
			}
			return result;
		}

		// Token: 0x04000025 RID: 37
		internal const string ElementName = "SetUserOofSettingsRequest";

		// Token: 0x04000026 RID: 38
		internal const string MailboxElementName = "Mailbox";

		// Token: 0x04000027 RID: 39
		private EmailAddress mailbox;

		// Token: 0x04000028 RID: 40
		private UserOofSettings userOofSettings;
	}
}
