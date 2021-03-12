using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200003C RID: 60
	internal class ThrottlingConfig : IThrottlingConfig
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000CBBA File Offset: 0x0000ADBA
		public int RecipientThreadLimit
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.RecipientThreadLimit;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000CBCB File Offset: 0x0000ADCB
		public int MaxConcurrentMailboxDeliveries
		{
			get
			{
				return Components.Configuration.LocalServer.MaxConcurrentMailboxDeliveries;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		public int MailboxServerThreadLimit
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MailboxServerThreadLimit;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000CBED File Offset: 0x0000ADED
		public int MaxMailboxDeliveryPerMdbConnections
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MaxMailboxDeliveryPerMdbConnections;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000CBFE File Offset: 0x0000ADFE
		public bool MailboxDeliveryThrottlingEnabled
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MailboxDeliveryThrottlingEnabled;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000CC0F File Offset: 0x0000AE0F
		public int MdbHealthMediumToHighThreshold
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MdbHealthMediumToHighThreshold;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000CC20 File Offset: 0x0000AE20
		public int MaxMailboxDeliveryPerMdbConnectionsHighHealthPercent
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MaxMailboxDeliveryPerMdbConnectionsHighHealthPercent;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000CC31 File Offset: 0x0000AE31
		public int MdbHealthLowToMediumThreshold
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MdbHealthLowToMediumThreshold;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000CC42 File Offset: 0x0000AE42
		public int MaxMailboxDeliveryPerMdbConnectionsMediumHealthPercent
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MaxMailboxDeliveryPerMdbConnectionsMediumHealthPercent;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000CC53 File Offset: 0x0000AE53
		public int MaxMailboxDeliveryPerMdbConnectionsLowHealthPercent
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MaxMailboxDeliveryPerMdbConnectionsLowHealthPercent;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000CC64 File Offset: 0x0000AE64
		public int MaxMailboxDeliveryPerMdbConnectionsLowestHealthPercent
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MaxMailboxDeliveryPerMdbConnectionsLowestHealthPercent;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000CC75 File Offset: 0x0000AE75
		public bool DynamicMailboxDatabaseThrottlingEnabled
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.DynamicMailboxDatabaseThrottlingEnabled;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000CC88 File Offset: 0x0000AE88
		public TimeSpan AcquireConnectionTimeout
		{
			get
			{
				return TimeSpan.FromMilliseconds(Math.Round(Components.TransportAppConfig.ConnectionCacheConfig.ConnectionInactivityTimeout.TotalMilliseconds * 0.8));
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		public ulong MaxConcurrentMessageSizeLimit
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MaxMailboxDeliveryConcurrentMessageSizeLimit.ToBytes();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000CCE4 File Offset: 0x0000AEE4
		public bool ThrottlingLogEnabled
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.MailboxDeliveryThrottlingLogEnabled;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000CCFA File Offset: 0x0000AEFA
		public EnhancedTimeSpan ThrottlingLogMaxAge
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.MailboxDeliveryThrottlingLogMaxAge;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000CD10 File Offset: 0x0000AF10
		public Unlimited<ByteQuantifiedSize> ThrottlingLogMaxDirectorySize
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.MailboxDeliveryThrottlingLogMaxDirectorySize;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000CD26 File Offset: 0x0000AF26
		public Unlimited<ByteQuantifiedSize> ThrottlingLogMaxFileSize
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.MailboxDeliveryThrottlingLogMaxFileSize;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		public LocalLongFullPath ThrottlingLogPath
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.MailboxDeliveryThrottlingLogPath;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000CD52 File Offset: 0x0000AF52
		public int ThrottlingLogBufferSize
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MailboxDeliveryThrottlingLogBufferSize;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000CD63 File Offset: 0x0000AF63
		public TimeSpan ThrottlingLogFlushInterval
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MailboxDeliveryThrottlingLogFlushInterval;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public TimeSpan AsyncLogInterval
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MailboxDeliveryThrottlingLogAsyncLogInterval;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000CD85 File Offset: 0x0000AF85
		public TimeSpan ThrottlingSummaryLoggingInterval
		{
			get
			{
				return Components.TransportAppConfig.RemoteDelivery.MailboxDeliveryThrottlingLogSummaryLoggingInterval;
			}
		}
	}
}
