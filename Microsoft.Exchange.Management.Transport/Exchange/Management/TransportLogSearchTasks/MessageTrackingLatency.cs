using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public sealed class MessageTrackingLatency
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000A2CB File Offset: 0x000084CB
		private MessageTrackingLatency(MessageTrackingEvent mte, LatencyComponent component)
		{
			this.mte = mte;
			this.component = component;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A2E1 File Offset: 0x000084E1
		public DateTime Timestamp
		{
			get
			{
				return this.mte.Timestamp;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000A2EE File Offset: 0x000084EE
		public string ClientIp
		{
			get
			{
				return this.mte.ClientIp;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000A2FB File Offset: 0x000084FB
		public string ClientHostname
		{
			get
			{
				return this.mte.ClientHostname;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000A308 File Offset: 0x00008508
		public string ServerIp
		{
			get
			{
				return this.mte.ServerIp;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A315 File Offset: 0x00008515
		public string ServerHostname
		{
			get
			{
				return this.mte.ServerHostname;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000A322 File Offset: 0x00008522
		public string SourceContext
		{
			get
			{
				return this.mte.SourceContext;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000A32F File Offset: 0x0000852F
		public string ConnectorId
		{
			get
			{
				return this.mte.ConnectorId;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000A33C File Offset: 0x0000853C
		public string Source
		{
			get
			{
				return this.mte.Source;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000A349 File Offset: 0x00008549
		public string EventId
		{
			get
			{
				return this.mte.EventId;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000A356 File Offset: 0x00008556
		public string InternalMessageId
		{
			get
			{
				return this.mte.InternalMessageId;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000A363 File Offset: 0x00008563
		public string MessageId
		{
			get
			{
				return this.mte.MessageId;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000A370 File Offset: 0x00008570
		public string[] Recipients
		{
			get
			{
				return this.mte.Recipients;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000A37D File Offset: 0x0000857D
		public string[] RecipientStatus
		{
			get
			{
				return this.mte.RecipientStatus;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000A38A File Offset: 0x0000858A
		public int? TotalBytes
		{
			get
			{
				return this.mte.TotalBytes;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000A397 File Offset: 0x00008597
		public int? RecipientCount
		{
			get
			{
				return this.mte.RecipientCount;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public string RelatedRecipientAddress
		{
			get
			{
				return this.mte.RelatedRecipientAddress;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000A3B1 File Offset: 0x000085B1
		public string[] Reference
		{
			get
			{
				return this.mte.Reference;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000A3BE File Offset: 0x000085BE
		public string MessageSubject
		{
			get
			{
				return this.mte.MessageSubject;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000A3CB File Offset: 0x000085CB
		public string Sender
		{
			get
			{
				return this.mte.Sender;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public string ReturnPath
		{
			get
			{
				return this.mte.ReturnPath;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000A3E5 File Offset: 0x000085E5
		public EnhancedTimeSpan? MessageLatency
		{
			get
			{
				return this.mte.MessageLatency;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000A3F2 File Offset: 0x000085F2
		public MessageLatencyType MessageLatencyType
		{
			get
			{
				return this.mte.MessageLatencyType;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000A3FF File Offset: 0x000085FF
		public string ComponentServerFqdn
		{
			get
			{
				return this.component.ServerFqdn;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000A40C File Offset: 0x0000860C
		public string ComponentCode
		{
			get
			{
				return this.component.Code;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000A419 File Offset: 0x00008619
		public LocalizedString ComponentName
		{
			get
			{
				return this.component.Name;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000A428 File Offset: 0x00008628
		public Unlimited<EnhancedTimeSpan> ComponentLatency
		{
			get
			{
				if ((double)this.component.Latency >= TransportAppConfig.LatencyTrackerConfig.MaxLatency.TotalSeconds)
				{
					return Unlimited<EnhancedTimeSpan>.UnlimitedValue;
				}
				return EnhancedTimeSpan.FromSeconds((double)this.component.Latency);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000A46C File Offset: 0x0000866C
		public int ComponentSequenceNumber
		{
			get
			{
				return this.component.SequenceNumber;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A640 File Offset: 0x00008840
		public static IEnumerable<MessageTrackingLatency> GetLatencies(MessageTrackingEvent mte)
		{
			if (mte != null)
			{
				ComponentLatencyParser parser = new ComponentLatencyParser();
				if (parser.TryParse(mte.MessageInfo))
				{
					foreach (LatencyComponent component in parser.Components)
					{
						yield return new MessageTrackingLatency(mte, component);
					}
				}
			}
			yield break;
		}

		// Token: 0x040000EC RID: 236
		private MessageTrackingEvent mte;

		// Token: 0x040000ED RID: 237
		private LatencyComponent component;
	}
}
