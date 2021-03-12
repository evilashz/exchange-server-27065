using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002D4 RID: 724
	[DataContract]
	public class PopSubscription : PimSubscription
	{
		// Token: 0x06002C98 RID: 11416 RVA: 0x000897D8 File Offset: 0x000879D8
		public PopSubscription(PopSubscriptionProxy subscription) : base(subscription)
		{
			this.PopSubscriptionProxy = subscription;
		}

		// Token: 0x17001DEC RID: 7660
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x000897E8 File Offset: 0x000879E8
		// (set) Token: 0x06002C9A RID: 11418 RVA: 0x000897F0 File Offset: 0x000879F0
		public PopSubscriptionProxy PopSubscriptionProxy { get; private set; }

		// Token: 0x17001DED RID: 7661
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x000897F9 File Offset: 0x000879F9
		// (set) Token: 0x06002C9C RID: 11420 RVA: 0x00089806 File Offset: 0x00087A06
		[DataMember]
		public string IncomingUserName
		{
			get
			{
				return this.PopSubscriptionProxy.IncomingUserName;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DEE RID: 7662
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x0008980D File Offset: 0x00087A0D
		// (set) Token: 0x06002C9E RID: 11422 RVA: 0x0008982E File Offset: 0x00087A2E
		[DataMember]
		public string IncomingServer
		{
			get
			{
				if (this.PopSubscriptionProxy.IncomingServer == null)
				{
					return null;
				}
				return this.PopSubscriptionProxy.IncomingServer.Domain;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DEF RID: 7663
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x00089835 File Offset: 0x00087A35
		// (set) Token: 0x06002CA0 RID: 11424 RVA: 0x00089842 File Offset: 0x00087A42
		[DataMember]
		public int IncomingPort
		{
			get
			{
				return this.PopSubscriptionProxy.IncomingPort;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DF0 RID: 7664
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x00089849 File Offset: 0x00087A49
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x00089860 File Offset: 0x00087A60
		[DataMember]
		public string IncomingSecurity
		{
			get
			{
				return this.PopSubscriptionProxy.IncomingSecurity.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x00089867 File Offset: 0x00087A67
		// (set) Token: 0x06002CA4 RID: 11428 RVA: 0x0008987E File Offset: 0x00087A7E
		[DataMember]
		public string IncomingAuth
		{
			get
			{
				return this.PopSubscriptionProxy.IncomingAuthentication.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001DF2 RID: 7666
		// (get) Token: 0x06002CA5 RID: 11429 RVA: 0x00089885 File Offset: 0x00087A85
		// (set) Token: 0x06002CA6 RID: 11430 RVA: 0x00089892 File Offset: 0x00087A92
		[DataMember]
		public bool LeaveOnServer
		{
			get
			{
				return this.PopSubscriptionProxy.LeaveOnServer;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
