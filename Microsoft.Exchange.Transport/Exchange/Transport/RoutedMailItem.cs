using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020003B8 RID: 952
	internal class RoutedMailItem : ILockableItem, IQueueItem, IReadOnlyMailItem, ISystemProbeTraceable
	{
		// Token: 0x06002AE6 RID: 10982 RVA: 0x000AB690 File Offset: 0x000A9890
		public RoutedMailItem(TransportMailItem mailItem, NextHopSolutionKey key)
		{
			this.mailItem = mailItem;
			if (!mailItem.NextHopSolutions.TryGetValue(key, out this.solution))
			{
				throw new ArgumentException("Invalid solution key used to create routed mail item", "key");
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000AB6E4 File Offset: 0x000A98E4
		public int MailItemRecipientCount
		{
			get
			{
				return this.mailItem.Recipients.Count;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x000AB6F8 File Offset: 0x000A98F8
		public DeliveryType DeliveryType
		{
			get
			{
				return this.solution.NextHopSolutionKey.NextHopType.DeliveryType;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x000AB720 File Offset: 0x000A9920
		public DateTime RoutingTimeStamp
		{
			get
			{
				return this.mailItem.RoutingTimeStamp;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x000AB730 File Offset: 0x000A9930
		public UnreachableReason UnreachableReasons
		{
			get
			{
				UnreachableSolution unreachableSolution = this.solution as UnreachableSolution;
				if (unreachableSolution == null)
				{
					return UnreachableReason.None;
				}
				return unreachableSolution.Reasons;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x000AB754 File Offset: 0x000A9954
		public ADRecipientCache<TransportMiniRecipient> ADRecipientCache
		{
			get
			{
				return this.mailItem.ADRecipientCache;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000AB761 File Offset: 0x000A9961
		public string Auth
		{
			get
			{
				return this.mailItem.Auth;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000AB76E File Offset: 0x000A996E
		public MultilevelAuthMechanism AuthMethod
		{
			get
			{
				return this.mailItem.AuthMethod;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x000AB77B File Offset: 0x000A997B
		public BodyType BodyType
		{
			get
			{
				return this.mailItem.BodyType;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000AB788 File Offset: 0x000A9988
		public DateTime DateReceived
		{
			get
			{
				return this.mailItem.DateReceived;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000AB795 File Offset: 0x000A9995
		// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x000AB79D File Offset: 0x000A999D
		public DeferReason DeferReason
		{
			get
			{
				return this.deferReason;
			}
			set
			{
				this.deferReason = value;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000AB7A6 File Offset: 0x000A99A6
		public bool IsInactive
		{
			get
			{
				return this.solution.IsInactive;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000AB7B3 File Offset: 0x000A99B3
		public MailDirectionality Directionality
		{
			get
			{
				return this.mailItem.Directionality;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000AB7C0 File Offset: 0x000A99C0
		public DsnFormat DsnFormat
		{
			get
			{
				if (this.suppressBodyInDsn)
				{
					return DsnFormat.Headers;
				}
				return this.mailItem.DsnFormat;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000AB7D7 File Offset: 0x000A99D7
		public DsnParameters DsnParameters
		{
			get
			{
				return this.dsnParameters;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000AB7DF File Offset: 0x000A99DF
		public string EnvId
		{
			get
			{
				return this.mailItem.EnvId;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000AB7EC File Offset: 0x000A99EC
		public IReadOnlyExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				if (this.deliveryQueueMailboxSubComponent != LatencyComponent.None)
				{
					if (this.extendedPropertyDictionaryCopy == null)
					{
						this.extendedPropertyDictionaryCopy = new ExtendedPropertyDictionary();
						this.extendedPropertyDictionaryCopy.CloneFrom((ExtendedPropertyDictionary)this.mailItem.ExtendedProperties);
					}
					this.extendedPropertyDictionaryCopy.SetValue<string>("Microsoft.Exchange.Transport.DeliveryQueueMailboxSubComponent", this.deliveryQueueMailboxSubComponent.ToString());
					this.extendedPropertyDictionaryCopy.SetValue<long>("Microsoft.Exchange.Transport.DeliveryQueueMailboxSubComponentLatency", (long)this.deliveryQueueMailboxSubComponentLatency.TotalSeconds);
					return this.extendedPropertyDictionaryCopy;
				}
				return this.mailItem.ExtendedProperties;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000AB87D File Offset: 0x000A9A7D
		public TimeSpan ExtensionToExpiryDuration
		{
			get
			{
				return this.mailItem.ExtensionToExpiryDuration;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x000AB88A File Offset: 0x000A9A8A
		public RoutingAddress From
		{
			get
			{
				return this.mailItem.From;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000AB897 File Offset: 0x000A9A97
		public string HeloDomain
		{
			get
			{
				return this.mailItem.HeloDomain;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x000AB8A4 File Offset: 0x000A9AA4
		public string InternetMessageId
		{
			get
			{
				return this.mailItem.InternetMessageId;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000AB8B1 File Offset: 0x000A9AB1
		public Guid NetworkMessageId
		{
			get
			{
				return this.mailItem.NetworkMessageId;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x000AB8BE File Offset: 0x000A9ABE
		public Guid SystemProbeId
		{
			get
			{
				return this.mailItem.SystemProbeId;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000AB8CB File Offset: 0x000A9ACB
		public bool IsProbe
		{
			get
			{
				return this.mailItem.IsProbe;
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06002AFF RID: 11007 RVA: 0x000AB8D8 File Offset: 0x000A9AD8
		public string ProbeName
		{
			get
			{
				return this.mailItem.ProbeName;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000AB8E5 File Offset: 0x000A9AE5
		public bool PersistProbeTrace
		{
			get
			{
				return this.mailItem.PersistProbeTrace;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x000AB8F2 File Offset: 0x000A9AF2
		public bool IsHeartbeat
		{
			get
			{
				return this.mailItem.IsHeartbeat;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000AB8FF File Offset: 0x000A9AFF
		public bool IsActive
		{
			get
			{
				return this.mailItem.IsActive;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x000AB90C File Offset: 0x000A9B0C
		public LatencyTracker LatencyTracker
		{
			get
			{
				if (this.latencyTracker == null)
				{
					this.latencyTracker = LatencyTracker.Clone(this.mailItem.LatencyTracker);
				}
				return this.latencyTracker;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000AB932 File Offset: 0x000A9B32
		public byte[] LegacyXexch50Blob
		{
			get
			{
				return this.mailItem.LegacyXexch50Blob;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x000AB93F File Offset: 0x000A9B3F
		public EmailMessage Message
		{
			get
			{
				return this.mailItem.Message;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x000AB94C File Offset: 0x000A9B4C
		public string Oorg
		{
			get
			{
				return this.mailItem.Oorg;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x000AB959 File Offset: 0x000A9B59
		public string ExoAccountForest
		{
			get
			{
				return this.mailItem.ExoAccountForest;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x000AB966 File Offset: 0x000A9B66
		public string ExoTenantContainer
		{
			get
			{
				return this.mailItem.ExoTenantContainer;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06002B09 RID: 11017 RVA: 0x000AB973 File Offset: 0x000A9B73
		public Guid ExternalOrganizationId
		{
			get
			{
				return this.mailItem.ExternalOrganizationId;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000AB980 File Offset: 0x000A9B80
		public string PrioritizationReason
		{
			get
			{
				return this.mailItem.PrioritizationReason;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x000AB98D File Offset: 0x000A9B8D
		public MimeDocument MimeDocument
		{
			get
			{
				return this.mailItem.MimeDocument;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x000AB99A File Offset: 0x000A9B9A
		public string MimeFrom
		{
			get
			{
				return this.mailItem.MimeFrom;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x000AB9A7 File Offset: 0x000A9BA7
		public RoutingAddress MimeSender
		{
			get
			{
				return this.mailItem.MimeSender;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x000AB9B4 File Offset: 0x000A9BB4
		public long MimeSize
		{
			get
			{
				return this.mailItem.MimeSize;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000AB9C1 File Offset: 0x000A9BC1
		public OrganizationId OrganizationId
		{
			get
			{
				return this.mailItem.OrganizationId;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x000AB9CE File Offset: 0x000A9BCE
		public RoutingAddress OriginalFrom
		{
			get
			{
				return this.mailItem.OriginalFrom;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06002B11 RID: 11025 RVA: 0x000AB9DB File Offset: 0x000A9BDB
		public int PoisonCount
		{
			get
			{
				return this.mailItem.PoisonCount;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x000AB9E8 File Offset: 0x000A9BE8
		public int PoisonForRemoteCount
		{
			get
			{
				return this.poisonForRemoteCount;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x000AB9F0 File Offset: 0x000A9BF0
		public bool IsPoison
		{
			get
			{
				return this.mailItem.IsPoison;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x000AB9FD File Offset: 0x000A9BFD
		public string ReceiveConnectorName
		{
			get
			{
				return this.mailItem.ReceiveConnectorName;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x000ABA0A File Offset: 0x000A9C0A
		public IReadOnlyMailRecipientCollection Recipients
		{
			get
			{
				return this.solution;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x000ABA12 File Offset: 0x000A9C12
		// (set) Token: 0x06002B17 RID: 11031 RVA: 0x000ABA1A File Offset: 0x000A9C1A
		public bool DelayDeferLogged
		{
			get
			{
				return this.delayDeferLogged;
			}
			private set
			{
				this.delayDeferLogged = value;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x000ABA23 File Offset: 0x000A9C23
		// (set) Token: 0x06002B19 RID: 11033 RVA: 0x000ABA2B File Offset: 0x000A9C2B
		public bool RetryDeferLogged
		{
			get
			{
				return this.retryDeferLogged;
			}
			private set
			{
				this.retryDeferLogged = value;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000ABA34 File Offset: 0x000A9C34
		public long RecordId
		{
			get
			{
				return this.mailItem.RecordId;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x000ABA41 File Offset: 0x000A9C41
		public bool RetryDeliveryIfRejected
		{
			get
			{
				return this.mailItem.RetryDeliveryIfRejected;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x000ABA4E File Offset: 0x000A9C4E
		public MimePart RootPart
		{
			get
			{
				return this.mailItem.RootPart;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x000ABA5B File Offset: 0x000A9C5B
		public int Scl
		{
			get
			{
				return this.mailItem.Scl;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x000ABA68 File Offset: 0x000A9C68
		public Guid ShadowMessageId
		{
			get
			{
				return this.mailItem.ShadowMessageId;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000ABA75 File Offset: 0x000A9C75
		public string ShadowServerContext
		{
			get
			{
				return this.mailItem.ShadowServerContext;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x000ABA82 File Offset: 0x000A9C82
		public string ShadowServerDiscardId
		{
			get
			{
				return this.mailItem.ShadowServerDiscardId;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x000ABA8F File Offset: 0x000A9C8F
		public IPAddress SourceIPAddress
		{
			get
			{
				return this.mailItem.SourceIPAddress;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000ABA9C File Offset: 0x000A9C9C
		public string Subject
		{
			get
			{
				return this.mailItem.Subject;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000ABAA9 File Offset: 0x000A9CA9
		// (set) Token: 0x06002B24 RID: 11044 RVA: 0x000ABAB1 File Offset: 0x000A9CB1
		public bool SuppressBodyInDsn
		{
			get
			{
				return this.suppressBodyInDsn;
			}
			set
			{
				this.suppressBodyInDsn = value;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000ABABA File Offset: 0x000A9CBA
		public PerTenantTransportSettings TransportSettings
		{
			get
			{
				return this.mailItem.TransportSettings;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x000ABAC7 File Offset: 0x000A9CC7
		// (set) Token: 0x06002B27 RID: 11047 RVA: 0x000ABAD4 File Offset: 0x000A9CD4
		public DateTime DeferUntil
		{
			get
			{
				return this.solution.DeferUntil;
			}
			set
			{
				this.solution.DeferUntil = value;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x000ABAE4 File Offset: 0x000A9CE4
		DateTime IQueueItem.Expiry
		{
			get
			{
				AdminActionStatus adminActionStatus = this.solution.AdminActionStatus;
				if (adminActionStatus == AdminActionStatus.PendingDeleteWithNDR || adminActionStatus == AdminActionStatus.PendingDeleteWithOutNDR)
				{
					return DateTime.MinValue;
				}
				return this.mailItem.Expiry;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x000ABB16 File Offset: 0x000A9D16
		// (set) Token: 0x06002B2A RID: 11050 RVA: 0x000ABB23 File Offset: 0x000A9D23
		public DeliveryPriority Priority
		{
			get
			{
				return this.mailItem.Priority;
			}
			set
			{
				throw new NotImplementedException("Priority is NOT settable on RMI.");
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000ABB2F File Offset: 0x000A9D2F
		public DateTime OriginalEnqueuedTime
		{
			get
			{
				return this.originalEnqueuedTime;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000ABB37 File Offset: 0x000A9D37
		// (set) Token: 0x06002B2D RID: 11053 RVA: 0x000ABB3F File Offset: 0x000A9D3F
		public DateTime EnqueuedTime
		{
			get
			{
				return this.enqueuedTime;
			}
			set
			{
				this.enqueuedTime = value;
				if (this.originalEnqueuedTime == DateTime.MinValue)
				{
					this.originalEnqueuedTime = value;
				}
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x000ABB61 File Offset: 0x000A9D61
		// (set) Token: 0x06002B2F RID: 11055 RVA: 0x000ABB6E File Offset: 0x000A9D6E
		public bool Deferred
		{
			get
			{
				return this.solution.Deferred;
			}
			set
			{
				this.solution.Deferred = value;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000ABB7C File Offset: 0x000A9D7C
		public RiskLevel RiskLevel
		{
			get
			{
				return this.solution.NextHopSolutionKey.RiskLevel;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x000ABB9C File Offset: 0x000A9D9C
		public bool IsDeletedByAdmin
		{
			get
			{
				return !this.mailItem.IsActive || this.solution.AdminActionStatus == AdminActionStatus.PendingDeleteWithNDR || this.solution.AdminActionStatus == AdminActionStatus.PendingDeleteWithOutNDR;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x000ABBC9 File Offset: 0x000A9DC9
		public bool IsSuspendedByAdmin
		{
			get
			{
				return this.solution.AdminActionStatus == AdminActionStatus.Suspended || this.solution.AdminActionStatus == AdminActionStatus.SuspendedInSubmissionQueue;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000ABBE9 File Offset: 0x000A9DE9
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x000ABBF6 File Offset: 0x000A9DF6
		public AccessToken AccessToken
		{
			get
			{
				return this.solution.AccessToken;
			}
			set
			{
				this.solution.AccessToken = value;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000ABC04 File Offset: 0x000A9E04
		// (set) Token: 0x06002B36 RID: 11062 RVA: 0x000ABC0C File Offset: 0x000A9E0C
		public ThrottlingContext ThrottlingContext
		{
			get
			{
				return this.throttlingContext;
			}
			set
			{
				this.throttlingContext = value;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000ABC15 File Offset: 0x000A9E15
		// (set) Token: 0x06002B38 RID: 11064 RVA: 0x000ABC22 File Offset: 0x000A9E22
		public WaitCondition CurrentCondition
		{
			get
			{
				return this.solution.CurrentCondition;
			}
			set
			{
				this.solution.CurrentCondition = value;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x000ABC30 File Offset: 0x000A9E30
		// (set) Token: 0x06002B3A RID: 11066 RVA: 0x000ABC38 File Offset: 0x000A9E38
		public QueuedRecipientsByAgeToken QueuedRecipientsByAgeToken
		{
			get
			{
				return this.queuedRecipientsByAgeToken;
			}
			set
			{
				this.queuedRecipientsByAgeToken = value;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000ABC41 File Offset: 0x000A9E41
		// (set) Token: 0x06002B3C RID: 11068 RVA: 0x000ABC4E File Offset: 0x000A9E4E
		public DateTimeOffset LockExpirationTime
		{
			get
			{
				return this.solution.LockExpirationTime;
			}
			set
			{
				this.solution.LockExpirationTime = value;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000ABC5C File Offset: 0x000A9E5C
		// (set) Token: 0x06002B3E RID: 11070 RVA: 0x000ABC69 File Offset: 0x000A9E69
		public string LockReason
		{
			get
			{
				return this.solution.LockReason;
			}
			set
			{
				this.solution.LockReason = value;
				if (!string.IsNullOrEmpty(value))
				{
					if (this.lockReasonHistory == null)
					{
						this.lockReasonHistory = new List<string>();
					}
					this.lockReasonHistory.Add(value);
				}
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x000ABC9E File Offset: 0x000A9E9E
		public IEnumerable<string> LockReasonHistory
		{
			get
			{
				if (this.lockReasonHistory == null)
				{
					return this.mailItem.LockReasonHistory;
				}
				if (this.mailItem.LockReasonHistory != null)
				{
					return this.mailItem.LockReasonHistory.Concat(this.lockReasonHistory);
				}
				return this.lockReasonHistory;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000ABCDE File Offset: 0x000A9EDE
		public LazyBytes FastIndexBlob
		{
			get
			{
				return this.mailItem.FastIndexBlob;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x000ABCEB File Offset: 0x000A9EEB
		// (set) Token: 0x06002B42 RID: 11074 RVA: 0x000ABCF3 File Offset: 0x000A9EF3
		public LastError LastQueueLevelError
		{
			get
			{
				return this.lastQueueLevelError;
			}
			set
			{
				this.lastQueueLevelError = value;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x000ABCFC File Offset: 0x000A9EFC
		public DateTime LatencyStartTime
		{
			get
			{
				return this.mailItem.LatencyStartTime;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x000ABD09 File Offset: 0x000A9F09
		// (set) Token: 0x06002B45 RID: 11077 RVA: 0x000ABD11 File Offset: 0x000A9F11
		internal bool RetriedDueToDeliverySourceLimit
		{
			get
			{
				return this.retriedDueToDeliverySourceLimit;
			}
			set
			{
				this.retriedDueToDeliverySourceLimit = value;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000ABD1C File Offset: 0x000A9F1C
		private DateTime DelayNotificationTime
		{
			get
			{
				TimeSpan t;
				if (Components.TransportAppConfig.RemoteDelivery.PriorityQueuingEnabled)
				{
					t = Components.TransportAppConfig.RemoteDelivery.DelayNotificationTimeout(((IQueueItem)this.mailItem).Priority);
				}
				else
				{
					t = Components.Configuration.LocalServer.TransportServer.DelayNotificationTimeout;
				}
				return this.mailItem.DateReceived + t;
			}
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000ABD82 File Offset: 0x000A9F82
		public void CacheTransportSettings()
		{
			this.mailItem.CacheTransportSettings();
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000ABD8F File Offset: 0x000A9F8F
		public bool IsJournalReport()
		{
			return this.mailItem.IsJournalReport();
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000ABD9C File Offset: 0x000A9F9C
		public bool IsPfReplica()
		{
			return this.mailItem.IsPfReplica();
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000ABDA9 File Offset: 0x000A9FA9
		public bool IsShadowed()
		{
			return this.mailItem.IsShadowed();
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000ABDB6 File Offset: 0x000A9FB6
		public bool IsDelayedAck()
		{
			return this.mailItem.IsDelayedAck();
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000ABDC4 File Offset: 0x000A9FC4
		public TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache)
		{
			TransportMailItem result;
			lock (this.mailItem)
			{
				result = this.mailItem.NewCloneWithoutRecipients(shareRecipientCache, this.latencyTracker ?? this.mailItem.LatencyTracker, this.solution);
			}
			return result;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000ABE28 File Offset: 0x000AA028
		public Stream OpenMimeReadStream()
		{
			return this.mailItem.OpenMimeReadStream();
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000ABE38 File Offset: 0x000AA038
		public bool IsWorkNeeded(DateTime now)
		{
			if (!Monitor.TryEnter(this.mailItem))
			{
				return false;
			}
			try
			{
				if (this.IsDeletedByAdmin || this.DelayNotificationTime >= now)
				{
					return false;
				}
				if (now - this.mailItem.DateReceived >= TimeSpan.FromMinutes(30.0) && this.mailItem.PendingDatabaseUpdates)
				{
					return true;
				}
				foreach (MailRecipient mailRecipient in this.solution.Recipients)
				{
					if (mailRecipient.IsDelayDsnNeeded)
					{
						return true;
					}
				}
			}
			finally
			{
				Monitor.Exit(this.mailItem);
			}
			return false;
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000ABF18 File Offset: 0x000AA118
		MessageContext IQueueItem.GetMessageContext(MessageProcessingSource source)
		{
			return new MessageContext(this.RecordId, this.InternetMessageId, source);
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000ABF2C File Offset: 0x000AA12C
		void IQueueItem.Update()
		{
			if (Monitor.TryEnter(this.mailItem))
			{
				try
				{
					this.InternalProcessDeferral();
				}
				finally
				{
					Monitor.Exit(this.mailItem);
				}
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000ABF80 File Offset: 0x000AA180
		public void Ack(AckStatus ackStatus, SmtpResponse smtpResponse, Queue<AckStatusAndResponse> recipientResponses, IEnumerable<MailRecipient> recipientsToTrack, MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, AckDetails ackDetails, bool reportEndToEndLatencies, DeferReason resubmitDeferReason, TimeSpan? resubmitDeferInterval, TimeSpan? retryInterval, string remoteMta, bool shadowed, string primaryServer, bool resetTimeForOrar, out bool shouldEnqueue)
		{
			shouldEnqueue = false;
			lock (this.mailItem)
			{
				this.UpdateE2ELatencyBucketsOnAck(recipientResponses);
				bool flag2 = RoutedMailItem.AllRecipientLimitRetries(recipientResponses);
				Destination deliveredDestination = null;
				if (this.solution.NextHopSolutionKey.NextHopType.DeliveryType == DeliveryType.SmtpDeliveryToMailbox)
				{
					deliveredDestination = new Destination(Destination.DestinationType.Mdb, this.solution.NextHopSolutionKey.NextHopConnector);
				}
				if (ackDetails != null)
				{
					ackDetails.LastRetryTime = new DateTime?(this.EnqueuedTime);
				}
				List<MailRecipient> list = new List<MailRecipient>();
				List<MailRecipient> list2 = new List<MailRecipient>();
				bool flag3;
				bool flag4;
				this.mailItem.Ack(ackStatus, smtpResponse, ackDetails, this.solution.Recipients, this.solution.AdminActionStatus, recipientResponses, deliveredDestination, list2, out flag3, out flag4);
				shouldEnqueue = (flag3 || flag4);
				TimeSpan timeSpan = TimeSpan.Zero;
				string str = null;
				bool flag5 = flag4 && !flag3;
				if (flag5)
				{
					this.numConsecutiveRetries++;
					if (retryInterval != null)
					{
						timeSpan = retryInterval.Value;
						if (string.IsNullOrEmpty(messageTrackingSourceContext))
						{
							str = string.Format("Deferring message with provided retry time of: {0}", timeSpan);
						}
					}
					else if (Components.TransportAppConfig.RemoteDelivery.MessageRetryIntervalProgressiveBackoffEnabled && (this.Priority == DeliveryPriority.High || this.Priority == DeliveryPriority.Normal))
					{
						timeSpan = Components.TransportAppConfig.RemoteDelivery.GetMessageRetryInterval(this.numConsecutiveRetries, TransportDeliveryTypes.internalDeliveryTypes.Contains(this.DeliveryType));
						str = string.Format("Progressive backoff retry time of: {0}", timeSpan);
					}
					else
					{
						timeSpan = Components.Configuration.LocalServer.TransportServer.MessageRetryInterval;
						str = string.Format("Using server config retry time of {0}", timeSpan);
					}
					this.DeferUntil = DateTime.UtcNow + timeSpan;
					this.UpdateDeliveryQueueMailboxLatency(timeSpan, smtpResponse, flag2);
					if (!flag2)
					{
						goto IL_259;
					}
					this.deferReason = DeferReason.RecipientThreadLimitExceeded;
					if (this.numConsecutiveRetries < Components.TransportAppConfig.RemoteDelivery.DeprioritizeOnRecipientThreadLimitExceededCount || this.Priority != DeliveryPriority.Normal)
					{
						goto IL_259;
					}
					shouldEnqueue = false;
					flag5 = false;
					using (List<MailRecipient>.Enumerator enumerator = this.solution.Recipients.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MailRecipient mailRecipient = enumerator.Current;
							if (mailRecipient.AckStatus == AckStatus.Retry)
							{
								mailRecipient.Ack(AckStatus.Resubmit, AckReason.RecipientThreadLimitExceeded);
								list.Add(mailRecipient);
							}
						}
						goto IL_259;
					}
				}
				this.numConsecutiveRetries = 0;
				IL_259:
				if (shouldEnqueue)
				{
					this.solution.DeliveryStatus = DeliveryStatus.Enqueued;
				}
				else if (list2.Count > 0 || list.Count > 0)
				{
					this.solution.DeliveryStatus = DeliveryStatus.PendingResubmit;
				}
				else
				{
					this.solution.DeliveryStatus = DeliveryStatus.Complete;
				}
				if (this.solution.NextHopSolutionKey.NextHopType != NextHopType.Heartbeat)
				{
					Components.OrarGenerator.GenerateOrarMessage(this, resetTimeForOrar);
					Components.DsnGenerator.GenerateDSNs(this, remoteMta, DsnGenerator.CallerComponent.Delivery);
					string messageTrackingSourceContext2 = messageTrackingSourceContext + str;
					this.Track(ackStatus, smtpResponse, recipientsToTrack, messageTrackingSource, messageTrackingSourceContext2, ackDetails, reportEndToEndLatencies, flag5);
				}
				if (this.solution.DeliveryStatus == DeliveryStatus.Complete)
				{
					Components.ShadowRedundancyComponent.ShadowRedundancyManager.EnqueueShadowMailItem(this.mailItem, this.solution, primaryServer, shadowed, ackStatus);
				}
				this.Commit();
				if (list2.Count > 0)
				{
					this.Resubmit(list2, resubmitDeferReason, resubmitDeferInterval, false, null);
				}
				if (list.Count > 0)
				{
					this.Resubmit(list, DeferReason.RecipientThreadLimitExceeded, new TimeSpan?(timeSpan), false, delegate(TransportMailItem mailItemToSubmit)
					{
						mailItemToSubmit.Priority = DeliveryPriority.Low;
						mailItemToSubmit.PrioritizationReason = "RecipientThreadLimitExceeded";
					});
				}
			}
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000AC34C File Offset: 0x000AA54C
		public void Ack(AckStatus ackStatus, SmtpResponse smtpResponse, MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, AckDetails ackDetails, bool resetTimeForOrar, out bool deletedByAdmin, out RiskLevel riskLevel, out DeliveryPriority priority)
		{
			if (ackStatus == AckStatus.Success)
			{
				throw new ArgumentOutOfRangeException("ackStatus", "This overload does not support AckStatus.Success");
			}
			lock (this.mailItem)
			{
				if (this.IsDeletedByAdmin)
				{
					deletedByAdmin = true;
					riskLevel = RiskLevel.Normal;
					priority = DeliveryPriority.Normal;
				}
				else
				{
					deletedByAdmin = false;
					riskLevel = this.RiskLevel;
					priority = this.Priority;
					bool flag2;
					this.Ack(ackStatus, smtpResponse, null, this.Recipients, messageTrackingSource, messageTrackingSourceContext, ackDetails, ackStatus == AckStatus.Fail, DeferReason.None, null, null, null, false, null, resetTimeForOrar, out flag2);
				}
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000AC3FC File Offset: 0x000AA5FC
		public void AbortHeartbeat()
		{
			lock (this.mailItem)
			{
				this.mailItem.Recipients[0].Ack(AckStatus.SuccessNoDsn, SmtpResponse.Empty);
				this.mailItem.Ack(AckStatus.Fail, SmtpResponse.Empty, this.solution.Recipients, null);
				this.Commit();
			}
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000AC478 File Offset: 0x000AA678
		public bool Defer()
		{
			bool result;
			lock (this.mailItem)
			{
				if (this.InternalProcessDeferral())
				{
					this.Deferred = true;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000AC4C8 File Offset: 0x000AA6C8
		public void Lock(string lockReason)
		{
			lock (this.mailItem)
			{
				this.LockReason = lockReason;
				this.solution.DeliveryStatus = DeliveryStatus.Enqueued;
				foreach (MailRecipient mailRecipient in this.solution.Recipients)
				{
					if (mailRecipient.Status == Status.Ready)
					{
						mailRecipient.Status = Status.Locked;
					}
				}
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000AC564 File Offset: 0x000AA764
		public void Dehydrate(Breadcrumb breadcrumb)
		{
			lock (this.mailItem)
			{
				this.mailItem.CommitLazyAndDehydrateMessageIfPossible(breadcrumb);
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000AC5AC File Offset: 0x000AA7AC
		public bool Activate()
		{
			lock (this.mailItem)
			{
				if (this.IsDeletedByAdmin)
				{
					return false;
				}
				foreach (MailRecipient mailRecipient in this.solution.Recipients)
				{
					if (mailRecipient.Status == Status.Retry)
					{
						mailRecipient.Status = Status.Ready;
					}
				}
				this.solution.DeliveryStatus = DeliveryStatus.Enqueued;
				if (this.mailItem.DeferReason != DeferReason.None)
				{
					LatencyTracker.EndTrackLatency(TransportMailItem.GetDeferLatencyComponent(this.mailItem.DeferReason), this.mailItem.LatencyTracker);
				}
				this.deferReason = DeferReason.None;
				this.Deferred = false;
			}
			return true;
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000AC694 File Offset: 0x000AA894
		public RoutedMailItem.PrepareForDeliveryResult PrepareForDelivery()
		{
			RoutedMailItem.PrepareForDeliveryResult result;
			lock (this.mailItem)
			{
				if (this.IsDeletedByAdmin)
				{
					result = RoutedMailItem.PrepareForDeliveryResult.IgnoreDeleted;
				}
				else if (this.solution.AdminActionStatus == AdminActionStatus.Suspended)
				{
					this.PrepareForSuspension();
					result = RoutedMailItem.PrepareForDeliveryResult.Requeue;
				}
				else if (this.AccessToken != null && !this.AccessToken.Validate(this.AccessToken.Condition))
				{
					this.AccessToken = null;
					result = RoutedMailItem.PrepareForDeliveryResult.Requeue;
				}
				else
				{
					this.solution.DeliveryStatus = DeliveryStatus.InDelivery;
					result = RoutedMailItem.PrepareForDeliveryResult.Deliver;
				}
			}
			return result;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000AC730 File Offset: 0x000AA930
		public bool PrepareForSuspension()
		{
			lock (this.mailItem)
			{
				if (this.solution.AdminActionStatus == AdminActionStatus.Suspended)
				{
					this.solution.DeliveryStatus = DeliveryStatus.DequeuedAndDeferred;
					((IQueueItem)this).DeferUntil = DateTime.MaxValue;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000AC798 File Offset: 0x000AA998
		public void InitializeDeliveryLatencyTracking()
		{
			LatencyTracker.EndAndBeginTrackLatency(LatencyTracker.GetDeliveryQueueLatencyComponent(this.DeliveryType), LatencyComponent.Delivery, this.latencyTracker);
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000AC7B3 File Offset: 0x000AA9B3
		public void FinalizeDeliveryLatencyTracking(LatencyComponent deliveryComponent)
		{
			LatencyTracker.EndTrackLatency(LatencyComponent.Delivery, deliveryComponent, this.latencyTracker);
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000AC7C4 File Offset: 0x000AA9C4
		public void TrackSuccessfulConnectLatency(LatencyComponent connectComponent)
		{
			LatencyTracker.EndTrackLatency(LatencyComponent.Delivery, connectComponent, this.latencyTracker);
			LatencyTracker.BeginTrackLatency(LatencyComponent.Delivery, this.latencyTracker);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000AC7E2 File Offset: 0x000AA9E2
		public void AddDsnParameters(string key, object value)
		{
			if (this.dsnParameters == null)
			{
				this.dsnParameters = new DsnParameters();
			}
			this.dsnParameters[key] = value;
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000AC804 File Offset: 0x000AAA04
		public void IncrementPoisonForRemoteCount()
		{
			this.poisonForRemoteCount++;
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000AC814 File Offset: 0x000AAA14
		public bool ShouldDequeueForResubmit()
		{
			lock (this.mailItem)
			{
				if (this.IsDeletedByAdmin)
				{
					return true;
				}
				if (this.solution.AdminActionStatus == AdminActionStatus.Suspended)
				{
					return false;
				}
				this.solution.DeliveryStatus = DeliveryStatus.PendingResubmit;
			}
			return true;
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000AC87C File Offset: 0x000AAA7C
		public bool Resubmit(DeferReason resubmitDeferReason, TimeSpan? resubmitDeferInterval, bool routeForHighAvailability, Action<TransportMailItem> updateBeforeResubmit = null)
		{
			return this.Resubmit(this.solution.Recipients.ToArray(), resubmitDeferReason, resubmitDeferInterval, routeForHighAvailability, updateBeforeResubmit);
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000AC89C File Offset: 0x000AAA9C
		private bool Resubmit(IList<MailRecipient> recipientsToResubmit, DeferReason resubmitDeferReason, TimeSpan? resubmitDeferInterval, bool routeForHighAvailability, Action<TransportMailItem> updateBeforeResubmit)
		{
			if (recipientsToResubmit == null || recipientsToResubmit.Count == 0)
			{
				throw new ArgumentException("recipientsToResubmit");
			}
			foreach (MailRecipient mailRecipient in recipientsToResubmit)
			{
				if (this.solution.Recipients.IndexOf(mailRecipient) == -1)
				{
					throw new ArgumentException(string.Format("recipient <{0}> is not in the solution recipient list", mailRecipient.Email.ToString()));
				}
			}
			TransportMailItem transportMailItem = null;
			lock (this.mailItem)
			{
				if (!this.CanBeResubmitted())
				{
					return false;
				}
				if (recipientsToResubmit.Count != this.solution.Recipients.Count || this.NeedForkBeforeResubmit())
				{
					transportMailItem = this.CreateForkedMailItem(recipientsToResubmit);
					MessageTrackingLog.TrackTransfer(MessageTrackingSource.QUEUE, transportMailItem, this.RecordId, "Forking for resubmit");
				}
				else
				{
					Dictionary<NextHopSolutionKey, NextHopSolution> nextHopSolutions = new Dictionary<NextHopSolutionKey, NextHopSolution>();
					this.mailItem.NextHopSolutions = nextHopSolutions;
					Components.RemoteDeliveryComponent.ReleaseMailItem(this.mailItem);
					transportMailItem = this.mailItem;
				}
				transportMailItem.RouteForHighAvailability = routeForHighAvailability;
				SystemProbeHelper.SmtpSendTracer.TracePass(this.mailItem, 0L, "message resubmitting in Delivery");
				MessageTrackingLog.TrackResubmit(MessageTrackingSource.QUEUE, transportMailItem, this.mailItem, (resubmitDeferReason != DeferReason.None) ? resubmitDeferReason.ToString() : "resubmitting in Delivery");
				this.PrepareRecipientsForResubmit(recipientsToResubmit);
				Resolver.ClearResolverAndTransportSettings(transportMailItem);
				this.solution.DeliveryStatus = DeliveryStatus.Complete;
				if (updateBeforeResubmit != null)
				{
					updateBeforeResubmit(transportMailItem);
				}
			}
			this.TransferLatencyAndLockInformation(transportMailItem);
			if (resubmitDeferInterval != null && resubmitDeferInterval.Value > TimeSpan.Zero)
			{
				transportMailItem.DeferReason = resubmitDeferReason;
				transportMailItem.DeferUntil = DateTime.UtcNow.Add(resubmitDeferInterval.Value);
				LatencyTracker.BeginTrackLatency(TransportMailItem.GetDeferLatencyComponent(resubmitDeferReason), transportMailItem.LatencyTracker);
			}
			Components.CategorizerComponent.EnqueueSubmittedMessage(transportMailItem);
			return true;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000ACAA4 File Offset: 0x000AACA4
		private bool CanBeResubmitted()
		{
			lock (this.mailItem)
			{
				if (this.IsDeletedByAdmin)
				{
					return false;
				}
				if (this.IsHeartbeat)
				{
					this.AbortHeartbeat();
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000ACB00 File Offset: 0x000AAD00
		public void Poison()
		{
			TransportMailItem transportMailItem = null;
			lock (this.mailItem)
			{
				foreach (MailRecipient mailRecipient in this.Recipients)
				{
					mailRecipient.UpdateForPoisonMessage();
				}
				if (this.NeedForkBeforePoisoning())
				{
					transportMailItem = this.CreateForkedMailItem(this.solution.Recipients.ToArray());
					MessageTrackingLog.TrackTransfer(MessageTrackingSource.QUEUE, transportMailItem, this.RecordId, "Forking for poison");
				}
				else
				{
					transportMailItem = this.mailItem;
					Dictionary<NextHopSolutionKey, NextHopSolution> nextHopSolutions = new Dictionary<NextHopSolutionKey, NextHopSolution>();
					this.mailItem.NextHopSolutions = nextHopSolutions;
					Components.RemoteDeliveryComponent.ReleaseMailItem(this.mailItem);
				}
				this.solution.DeliveryStatus = DeliveryStatus.Complete;
			}
			this.TransferLatencyAndLockInformation(transportMailItem);
			transportMailItem.PoisonCount = int.MaxValue;
			if (Components.MessageDepotComponent.Enabled)
			{
				MessageDepotMailItem item = new MessageDepotMailItem(transportMailItem);
				Components.MessageDepotComponent.MessageDepot.Add(item);
			}
			else
			{
				Components.QueueManager.PoisonMessageQueue.Enqueue(transportMailItem);
			}
			MessageTrackingLog.TrackPoisonMessage(MessageTrackingSource.QUEUE, transportMailItem);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000ACC38 File Offset: 0x000AAE38
		public Stream OpenMimeReadStream(bool downConvert)
		{
			return this.mailItem.OpenMimeReadStream(downConvert);
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000ACC46 File Offset: 0x000AAE46
		private static void MarkForDelayDsn(MailRecipient recipient)
		{
			if (recipient.IsDelayDsnNeeded)
			{
				recipient.DsnNeeded = DsnFlags.Delay;
				recipient.SmtpResponse = SmtpResponse.MessageDelayed;
			}
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000ACC62 File Offset: 0x000AAE62
		internal void UpdateE2ELatencyBucketsOnEnqueue()
		{
			this.UpdateE2ELatencyBuckets(null);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000ACC6B File Offset: 0x000AAE6B
		private void UpdateE2ELatencyBucketsOnAck(Queue<AckStatusAndResponse> recipientResponses)
		{
			this.UpdateE2ELatencyBuckets(recipientResponses);
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000ACC8C File Offset: 0x000AAE8C
		private void UpdateE2ELatencyBuckets(Queue<AckStatusAndResponse> recipientResponses)
		{
			if (this.externalDeliveryEnqueueTime != DateTime.MinValue || this.Recipients.Count == 0)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan timeSpan = utcNow - this.LatencyStartTime;
			if (timeSpan < TimeSpan.Zero)
			{
				return;
			}
			if (recipientResponses == null)
			{
				if (TransportDeliveryTypes.externalDeliveryTypes.Contains(this.DeliveryType))
				{
					Components.RemoteDeliveryComponent.GetEndToEndLatencyBuckets().RecordExternalSendLatency(this.Priority, timeSpan, this.Recipients.Count);
					this.externalDeliveryEnqueueTime = utcNow;
					return;
				}
			}
			else if (this.DeliveryType == DeliveryType.SmtpDeliveryToMailbox)
			{
				int num = (from r in recipientResponses
				where r.AckStatus == AckStatus.Success || r.AckStatus == AckStatus.Fail
				select r).Count<AckStatusAndResponse>();
				if (num > 0)
				{
					Components.RemoteDeliveryComponent.GetEndToEndLatencyBuckets().RecordMailboxDeliveryLatency(this.Priority, timeSpan, num);
				}
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000ACDD0 File Offset: 0x000AAFD0
		private static bool AllRecipientLimitRetries(Queue<AckStatusAndResponse> recipientResponses)
		{
			bool result = false;
			if (recipientResponses != null)
			{
				if (recipientResponses.Any((AckStatusAndResponse r) => r.AckStatus == AckStatus.Retry))
				{
					result = (from r in recipientResponses
					where r.AckStatus == AckStatus.Retry
					select r).All((AckStatusAndResponse r) => r.SmtpResponse.StatusText != null && r.SmtpResponse.StatusText.Length > 0 && r.SmtpResponse.StatusText[0].Equals(AckReason.RecipientThreadLimitExceeded.StatusText[0]));
				}
			}
			return result;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000ACE50 File Offset: 0x000AB050
		private void UpdateDeliveryQueueMailboxLatency(TimeSpan latency, SmtpResponse smtpResponse, bool allRecipientLimitRetries)
		{
			LatencyComponent latencyComponent = LatencyComponent.None;
			if (allRecipientLimitRetries)
			{
				latencyComponent = LatencyComponent.DeliveryQueueMailboxRecipientThreadLimitExceeded;
			}
			else
			{
				string text = smtpResponse.ToString();
				foreach (KeyValuePair<string, LatencyComponent> keyValuePair in RoutedMailItem.smtpResponseToLatencyComponentMap)
				{
					if (text.StartsWith(keyValuePair.Key, StringComparison.InvariantCultureIgnoreCase))
					{
						latencyComponent = keyValuePair.Value;
						break;
					}
				}
			}
			if (latencyComponent != LatencyComponent.None)
			{
				this.deliveryQueueMailboxSubComponent = latencyComponent;
				this.deliveryQueueMailboxSubComponentLatency += latency;
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000ACEF8 File Offset: 0x000AB0F8
		private void Track(AckStatus ackStatus, SmtpResponse smtpResponse, IEnumerable<MailRecipient> recipientsToTrack, MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, AckDetails ackDetails, bool reportEndToEndLatencies, bool willRetry)
		{
			LatencyFormatter latencyFormatter = null;
			if (!willRetry)
			{
				latencyFormatter = new LatencyFormatter(this, Components.Configuration.LocalServer.TransportServer.Fqdn, reportEndToEndLatencies, this.externalDeliveryEnqueueTime, this.LatencyStartTime);
			}
			MessageTrackingLog.TrackRelayedAndFailed(messageTrackingSource, messageTrackingSourceContext, this, recipientsToTrack, ackDetails, smtpResponse, latencyFormatter);
			if (willRetry)
			{
				IEnumerable<MailRecipient> enumerable = (from r in recipientsToTrack
				where r.AckStatus == AckStatus.Retry
				select r).ToArray<MailRecipient>();
				SystemProbeHelper.SmtpSendTracer.TracePass<SmtpResponse>(this, 0L, "going into retry with smtp response {0}", smtpResponse);
				MessageTrackingLog.TrackDefer(messageTrackingSource, messageTrackingSourceContext, this, enumerable, ackDetails);
				if (messageTrackingSource == MessageTrackingSource.SMTP)
				{
					foreach (MailRecipient mailRecipient in enumerable)
					{
						mailRecipient.SetSmtpDeferLogged();
					}
				}
				this.RetryDeferLogged = true;
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000ACFD8 File Offset: 0x000AB1D8
		public void TrackDeferRetryOrSuspended(SmtpResponse smtpResponse, AckDetails ackDetails)
		{
			this.RetryDeferLogged = true;
			this.InternalTrackDefer(smtpResponse, null, ackDetails, null);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000ACFEB File Offset: 0x000AB1EB
		public void TrackDeferDelay(SmtpResponse smtpResponse, AckDetails ackDetails, string sourceContext)
		{
			this.DelayDeferLogged = true;
			this.InternalTrackDefer(smtpResponse, sourceContext, ackDetails, null);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000AD000 File Offset: 0x000AB200
		public bool TrackDeferIfRecipientsInRetry(SmtpResponse smtpResponse, AckDetails ackDetails)
		{
			MailRecipient mailRecipient = null;
			foreach (MailRecipient mailRecipient2 in this.Recipients)
			{
				if (mailRecipient2.Status == Status.Retry)
				{
					mailRecipient = mailRecipient2;
					break;
				}
			}
			if (mailRecipient == null)
			{
				return false;
			}
			List<MailRecipient> list = new List<MailRecipient>();
			foreach (MailRecipient mailRecipient3 in this.Recipients)
			{
				if (!mailRecipient3.IsProcessed)
				{
					list.Add(mailRecipient3);
				}
			}
			this.InternalTrackDefer(mailRecipient.SmtpResponse.Equals(SmtpResponse.Empty) ? smtpResponse : mailRecipient.SmtpResponse, null, ackDetails, list);
			this.RetryDeferLogged = true;
			return true;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000AD0E0 File Offset: 0x000AB2E0
		public WaitCondition GetCondition()
		{
			if (this.CurrentCondition == null)
			{
				if (Components.TransportAppConfig.ThrottlingConfig.DeliveryTenantThrottlingEnabled)
				{
					Guid tenantId = TransportMailItemWrapper.GetTenantId(this);
					this.CurrentCondition = new TenantBasedCondition(tenantId);
				}
				else if (Components.TransportAppConfig.ThrottlingConfig.DeliverySenderThrottlingEnabled)
				{
					this.CurrentCondition = new SenderBasedCondition(TransportMailItem.GetSourceID(this.mailItem));
				}
			}
			return this.CurrentCondition;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000AD148 File Offset: 0x000AB348
		public long GetCurrentMimeSize()
		{
			return this.mailItem.GetCurrrentMimeSize();
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000AD158 File Offset: 0x000AB358
		private void InternalTrackDefer(SmtpResponse smtpResponse, string sourceContext, AckDetails ackDetails, List<MailRecipient> retryRecipients)
		{
			if (retryRecipients != null)
			{
				using (List<MailRecipient>.Enumerator enumerator = retryRecipients.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MailRecipient mailRecipient = enumerator.Current;
						mailRecipient.SmtpResponse = smtpResponse;
					}
					goto IL_6E;
				}
			}
			foreach (MailRecipient mailRecipient2 in this.Recipients)
			{
				if (!mailRecipient2.IsProcessed)
				{
					mailRecipient2.SmtpResponse = smtpResponse;
				}
			}
			IL_6E:
			SystemProbeHelper.SmtpSendTracer.TracePass<SmtpResponse>(this, 0L, "going into retry with smtp response {0}", smtpResponse);
			MessageTrackingLog.TrackDefer(MessageTrackingSource.QUEUE, sourceContext, this, retryRecipients, ackDetails);
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000AD210 File Offset: 0x000AB410
		private void Commit()
		{
			if (this.mailItem.Status == Status.Complete)
			{
				this.mailItem.ReleaseFromRemoteDelivery();
				Components.RemoteDeliveryComponent.ReleaseMailItem(this.mailItem);
				return;
			}
			this.solution.CheckAdminAction();
			this.mailItem.CommitLazy(this.solution);
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000AD263 File Offset: 0x000AB463
		private bool InternalProcessDeferral()
		{
			if (this.IsDeletedByAdmin)
			{
				return false;
			}
			this.GenerateDelayDsnIfNeeded();
			this.mailItem.CommitLazyAndDehydrateMessageIfPossible(Breadcrumb.DehydrateOnRoutedMailItemDeferral);
			return true;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000AD288 File Offset: 0x000AB488
		private void GenerateDelayDsnIfNeeded()
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.Transport.DelayDsn.Enabled)
			{
				return;
			}
			if (this.DelayNotificationTime < DateTime.UtcNow && this.solution.DeliveryStatus == DeliveryStatus.Enqueued)
			{
				this.solution.Recipients.ForEach(new Action<MailRecipient>(RoutedMailItem.MarkForDelayDsn));
				LastError lastError = new LastError((this.lastQueueLevelError != null) ? this.lastQueueLevelError.LastAttemptFqdn : string.Empty, (this.lastQueueLevelError != null) ? this.lastQueueLevelError.LastAttemptEndpoint : null, null, SmtpResponse.MessageDelayed, this.lastQueueLevelError);
				Components.DsnGenerator.GenerateDSNs(this.mailItem, this.solution.Recipients, lastError);
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000AD358 File Offset: 0x000AB558
		private void PrepareRecipientsForResubmit(IEnumerable<MailRecipient> recipients)
		{
			foreach (MailRecipient mailRecipient in recipients)
			{
				Resolver.ClearResolverProperties(mailRecipient);
				mailRecipient.SmtpResponse = SmtpResponse.Empty;
				mailRecipient.NextHop = NextHopSolutionKey.Empty;
				mailRecipient.UnreachableReason = UnreachableReason.None;
				mailRecipient.Type = MailRecipientType.Unknown;
				mailRecipient.FinalDestination = string.Empty;
				mailRecipient.RetryCount = 0;
				mailRecipient.ResetRoutingOverride();
				mailRecipient.ResetTlsDomain();
				if (mailRecipient.Status == Status.Retry)
				{
					mailRecipient.Status = Status.Ready;
				}
			}
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000AD3F4 File Offset: 0x000AB5F4
		private bool NeedForkBeforeResubmit()
		{
			Dictionary<NextHopSolutionKey, NextHopSolution> nextHopSolutions = this.mailItem.NextHopSolutions;
			if (nextHopSolutions.Count > 1)
			{
				foreach (NextHopSolution nextHopSolution in nextHopSolutions.Values)
				{
					if (nextHopSolution != this.solution && nextHopSolution.DeliveryStatus != DeliveryStatus.Complete)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000AD470 File Offset: 0x000AB670
		private bool NeedForkBeforePoisoning()
		{
			return this.NeedForkBeforeResubmit();
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000AD478 File Offset: 0x000AB678
		private TransportMailItem CreateForkedMailItem(IList<MailRecipient> recipientsToFork)
		{
			LatencyTracker latencyTrackerToClone = (this.latencyTracker == null) ? this.mailItem.LatencyTracker : null;
			TransportMailItem transportMailItem = this.mailItem.NewCloneWithoutRecipients(false, latencyTrackerToClone, this.solution);
			foreach (MailRecipient mailRecipient in recipientsToFork)
			{
				mailRecipient.MoveTo(transportMailItem);
				this.solution.Recipients.Remove(mailRecipient);
			}
			transportMailItem.CommitLazy();
			return transportMailItem;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000AD504 File Offset: 0x000AB704
		private void TransferLatencyAndLockInformation(TransportMailItem mailItem)
		{
			if (this.latencyTracker != null)
			{
				mailItem.LatencyTracker = LatencyTracker.Clone(this.latencyTracker);
			}
			if (this.LockReasonHistory != null)
			{
				mailItem.LockReasonHistory = this.LockReasonHistory;
			}
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000AD5A4 File Offset: 0x000AB7A4
		internal bool UpdateProperties(ExtensibleMessageInfo properties, bool resubmit)
		{
			if (resubmit && properties.OutboundIPPool > 0 && properties.OutboundIPPool != this.solution.GetOutboundIPPool())
			{
				this.Resubmit(this.solution.Recipients.ToArray(), DeferReason.None, null, false, delegate(TransportMailItem mailItemToSubmit)
				{
					foreach (MailRecipient mailRecipient in mailItemToSubmit.Recipients)
					{
						mailRecipient.OutboundIPPool = properties.OutboundIPPool;
					}
					this.solution.AdminActionStatus = AdminActionStatus.None;
				});
				return true;
			}
			return false;
		}

		// Token: 0x040015C2 RID: 5570
		private static readonly Dictionary<string, LatencyComponent> smtpResponseToLatencyComponentMap = new Dictionary<string, LatencyComponent>
		{
			{
				AckReason.DeliverAgentTransientFailure.ToString(),
				LatencyComponent.DeliveryQueueMailboxDeliverAgentTransientFailure
			},
			{
				AckReason.DynamicMailboxDatabaseThrottlingLimitExceeded.ToString(),
				LatencyComponent.DeliveryQueueMailboxDynamicMailboxDatabaseThrottlingLimitExceeded
			},
			{
				SmtpResponse.InsufficientResource.ToString(),
				LatencyComponent.DeliveryQueueMailboxInsufficientResources
			},
			{
				AckReason.MDBOffline.ToString(),
				LatencyComponent.DeliveryQueueMailboxMailboxDatabaseOffline
			},
			{
				AckReason.MailboxServerOffline.ToString(),
				LatencyComponent.DeliveryQueueMailboxMailboxServerOffline
			},
			{
				"432 4.2.0 STOREDRV.Deliver.Exception:StorageTransientException.MapiExceptionLockViolation",
				LatencyComponent.DeliveryQueueMailboxMapiExceptionLockViolation
			},
			{
				"432 4.2.0 STOREDRV.Deliver.Exception:StorageTransientException.MapiExceptionTimeout",
				LatencyComponent.DeliveryQueueMailboxMapiExceptionTimeout
			},
			{
				AckReason.MaxConcurrentMessageSizeLimitExceeded.ToString(),
				LatencyComponent.DeliveryQueueMailboxMaxConcurrentMessageSizeLimitExceeded
			}
		};

		// Token: 0x040015C3 RID: 5571
		private TransportMailItem mailItem;

		// Token: 0x040015C4 RID: 5572
		private NextHopSolution solution;

		// Token: 0x040015C5 RID: 5573
		private LatencyTracker latencyTracker;

		// Token: 0x040015C6 RID: 5574
		private DsnParameters dsnParameters;

		// Token: 0x040015C7 RID: 5575
		private DateTime enqueuedTime;

		// Token: 0x040015C8 RID: 5576
		private DateTime originalEnqueuedTime;

		// Token: 0x040015C9 RID: 5577
		private DeferReason deferReason;

		// Token: 0x040015CA RID: 5578
		private bool delayDeferLogged;

		// Token: 0x040015CB RID: 5579
		private bool retryDeferLogged;

		// Token: 0x040015CC RID: 5580
		private bool retriedDueToDeliverySourceLimit;

		// Token: 0x040015CD RID: 5581
		private bool suppressBodyInDsn;

		// Token: 0x040015CE RID: 5582
		private List<string> lockReasonHistory;

		// Token: 0x040015CF RID: 5583
		private ThrottlingContext throttlingContext;

		// Token: 0x040015D0 RID: 5584
		private int poisonForRemoteCount;

		// Token: 0x040015D1 RID: 5585
		private LastError lastQueueLevelError;

		// Token: 0x040015D2 RID: 5586
		private QueuedRecipientsByAgeToken queuedRecipientsByAgeToken;

		// Token: 0x040015D3 RID: 5587
		private DateTime externalDeliveryEnqueueTime = DateTime.MinValue;

		// Token: 0x040015D4 RID: 5588
		private int numConsecutiveRetries;

		// Token: 0x040015D5 RID: 5589
		private LatencyComponent deliveryQueueMailboxSubComponent;

		// Token: 0x040015D6 RID: 5590
		private TimeSpan deliveryQueueMailboxSubComponentLatency = TimeSpan.Zero;

		// Token: 0x040015D7 RID: 5591
		private ExtendedPropertyDictionary extendedPropertyDictionaryCopy;

		// Token: 0x020003B9 RID: 953
		public enum PrepareForDeliveryResult
		{
			// Token: 0x040015DF RID: 5599
			Deliver,
			// Token: 0x040015E0 RID: 5600
			IgnoreDeleted,
			// Token: 0x040015E1 RID: 5601
			Requeue
		}
	}
}
