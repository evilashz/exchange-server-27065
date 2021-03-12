using System;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x0200000C RID: 12
	[XmlType("GetUserOofSettingsRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUserOofSettingsRequest : BaseAvailabilityRequest
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003369 File Offset: 0x00001569
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003371 File Offset: 0x00001571
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

		// Token: 0x06000067 RID: 103 RVA: 0x0000337A File Offset: 0x0000157A
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUserOofSettings(callContext, this);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003384 File Offset: 0x00001584
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

		// Token: 0x0400001F RID: 31
		private EmailAddress mailbox;
	}
}
