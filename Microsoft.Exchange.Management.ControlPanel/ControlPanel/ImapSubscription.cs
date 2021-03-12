using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002B2 RID: 690
	[DataContract]
	public class ImapSubscription : PimSubscription
	{
		// Token: 0x06002BD9 RID: 11225 RVA: 0x000885C8 File Offset: 0x000867C8
		public ImapSubscription(IMAPSubscriptionProxy subscription) : base(subscription)
		{
			this.ImapSubscriptionProxy = subscription;
		}

		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000885D8 File Offset: 0x000867D8
		// (set) Token: 0x06002BDB RID: 11227 RVA: 0x000885E0 File Offset: 0x000867E0
		public IMAPSubscriptionProxy ImapSubscriptionProxy { get; private set; }

		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x000885E9 File Offset: 0x000867E9
		// (set) Token: 0x06002BDD RID: 11229 RVA: 0x000885F6 File Offset: 0x000867F6
		[DataMember]
		public string IncomingUserName
		{
			get
			{
				return this.ImapSubscriptionProxy.IncomingUserName;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000885FD File Offset: 0x000867FD
		// (set) Token: 0x06002BDF RID: 11231 RVA: 0x0008861E File Offset: 0x0008681E
		[DataMember]
		public string IncomingServer
		{
			get
			{
				if (this.ImapSubscriptionProxy.IncomingServer == null)
				{
					return null;
				}
				return this.ImapSubscriptionProxy.IncomingServer.Domain;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001D9F RID: 7583
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x00088625 File Offset: 0x00086825
		// (set) Token: 0x06002BE1 RID: 11233 RVA: 0x00088632 File Offset: 0x00086832
		[DataMember]
		public int IncomingPort
		{
			get
			{
				return this.ImapSubscriptionProxy.IncomingPort;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DA0 RID: 7584
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x00088639 File Offset: 0x00086839
		// (set) Token: 0x06002BE3 RID: 11235 RVA: 0x00088650 File Offset: 0x00086850
		[DataMember]
		public string IncomingSecurity
		{
			get
			{
				return this.ImapSubscriptionProxy.IncomingSecurity.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DA1 RID: 7585
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x00088657 File Offset: 0x00086857
		// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x0008866E File Offset: 0x0008686E
		[DataMember]
		public string IncomingAuth
		{
			get
			{
				return this.ImapSubscriptionProxy.IncomingAuthentication.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
