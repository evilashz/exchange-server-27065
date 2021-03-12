using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ProcessManager;

namespace Microsoft.Exchange.Transport.MessageThrottling
{
	// Token: 0x02000130 RID: 304
	internal sealed class MessageThrottlingManager : IMessageThrottlingManager
	{
		// Token: 0x06000D75 RID: 3445 RVA: 0x00030EF2 File Offset: 0x0002F0F2
		public MessageThrottlingManager() : this(null)
		{
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00030EFB File Offset: 0x0002F0FB
		public MessageThrottlingManager(IMessageThrottlingManagerConfig config)
		{
			this.config = (config ?? MessageThrottlingManager.CreateDefaultMessageThrottlingManagerConfig());
			this.ipAddressRateLimiter = new RateLimiter<IPAddress>();
			this.userRateLimiter = new RateLimiter<Guid>();
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00030F29 File Offset: 0x0002F129
		public bool Enabled
		{
			get
			{
				return this.config.Enabled;
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00030F36 File Offset: 0x0002F136
		public MessageThrottlingReason ShouldThrottleMessage(Guid mailboxGuid, int messageRateLimit)
		{
			return this.ShouldThrottleMessage(mailboxGuid, messageRateLimit, IPAddress.Any, 0, MessageRateSourceFlags.User);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00030F47 File Offset: 0x0002F147
		public MessageThrottlingReason ShouldThrottleMessage(IPAddress ipAddress, int receiveConnectorLimit, MessageRateSourceFlags messageRateSource)
		{
			return this.ShouldThrottleMessage(Guid.Empty, 0, ipAddress, receiveConnectorLimit, messageRateSource & MessageRateSourceFlags.IPAddress);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00030F5C File Offset: 0x0002F15C
		public MessageThrottlingReason ShouldThrottleMessage(Guid mailboxGuid, int userMessageRateLimit, IPAddress ipAddress, int receiveConnectorLimit, MessageRateSourceFlags messageRateSource)
		{
			MessageThrottlingReason result = MessageThrottlingReason.NotThrottled;
			if (this.Enabled && messageRateSource != MessageRateSourceFlags.None)
			{
				if ((messageRateSource & MessageRateSourceFlags.User) == MessageRateSourceFlags.User && (userMessageRateLimit == 0 || !this.userRateLimiter.TryFetchToken(mailboxGuid, userMessageRateLimit)))
				{
					result = MessageThrottlingReason.UserLimitExceeded;
				}
				else if ((messageRateSource & MessageRateSourceFlags.IPAddress) == MessageRateSourceFlags.IPAddress && (receiveConnectorLimit == 0 || !this.ipAddressRateLimiter.TryFetchToken(ipAddress, receiveConnectorLimit)))
				{
					if ((messageRateSource & MessageRateSourceFlags.User) == MessageRateSourceFlags.User)
					{
						this.userRateLimiter.ReleaseUnusedToken(mailboxGuid);
					}
					result = MessageThrottlingReason.IPAddressLimitExceeded;
				}
			}
			return result;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00030FC8 File Offset: 0x0002F1C8
		public void CleanupIdleEntries()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.ipAddressRateLimiter.CleanupIdleEntries(utcNow);
			this.userRateLimiter.CleanupIdleEntries(utcNow);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00030FF3 File Offset: 0x0002F1F3
		private static IMessageThrottlingManagerConfig CreateDefaultMessageThrottlingManagerConfig()
		{
			return new MessageThrottlingManagerConfig();
		}

		// Token: 0x040005CB RID: 1483
		private IMessageThrottlingManagerConfig config;

		// Token: 0x040005CC RID: 1484
		private IRateLimiter<IPAddress> ipAddressRateLimiter;

		// Token: 0x040005CD RID: 1485
		private IRateLimiter<Guid> userRateLimiter;
	}
}
