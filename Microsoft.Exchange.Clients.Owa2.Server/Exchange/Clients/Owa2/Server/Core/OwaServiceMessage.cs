using System;
using System.ServiceModel.Channels;
using System.Web;
using System.Xml;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200024E RID: 590
	public class OwaServiceMessage : Message
	{
		// Token: 0x06001639 RID: 5689 RVA: 0x0005119F File Offset: 0x0004F39F
		public OwaServiceMessage(HttpRequest httpRequest, object request)
		{
			this.HttpRequest = httpRequest;
			this.Request = request;
			this.messageProperties = new MessageProperties();
			this.messageHeaders = new MessageHeaders(MessageVersion.None);
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x000511D0 File Offset: 0x0004F3D0
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x000511D8 File Offset: 0x0004F3D8
		public HttpRequest HttpRequest { get; private set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x000511E1 File Offset: 0x0004F3E1
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x000511E9 File Offset: 0x0004F3E9
		public object Request { get; private set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x000511F2 File Offset: 0x0004F3F2
		public override MessageHeaders Headers
		{
			get
			{
				return this.messageHeaders;
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000511FA File Offset: 0x0004F3FA
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x000511FC File Offset: 0x0004F3FC
		public override MessageProperties Properties
		{
			get
			{
				return this.messageProperties;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00051204 File Offset: 0x0004F404
		public override MessageVersion Version
		{
			get
			{
				return MessageVersion.None;
			}
		}

		// Token: 0x04000C51 RID: 3153
		private MessageProperties messageProperties;

		// Token: 0x04000C52 RID: 3154
		private MessageHeaders messageHeaders;
	}
}
