using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001FE RID: 510
	[Serializable]
	public sealed class MoveRequestStatistics : RequestStatisticsBase
	{
		// Token: 0x060018C0 RID: 6336 RVA: 0x000339FF File Offset: 0x00031BFF
		public MoveRequestStatistics()
		{
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00033A07 File Offset: 0x00031C07
		internal MoveRequestStatistics(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00033A10 File Offset: 0x00031C10
		internal MoveRequestStatistics(TransactionalRequestJob requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00033A2A File Offset: 0x00031C2A
		internal MoveRequestStatistics(RequestJobXML requestJob) : this((SimpleProviderPropertyBag)requestJob.propertyBag)
		{
			base.CopyNonSchematizedPropertiesFrom(requestJob);
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00033A44 File Offset: 0x00031C44
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x00033A4C File Offset: 0x00031C4C
		public ADObjectId MailboxIdentity
		{
			get
			{
				return base.UserId;
			}
			internal set
			{
				base.UserId = value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00033A55 File Offset: 0x00031C55
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x00033A5D File Offset: 0x00031C5D
		public new string DistinguishedName
		{
			get
			{
				return base.DistinguishedName;
			}
			internal set
			{
				base.DistinguishedName = value;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00033A66 File Offset: 0x00031C66
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x00033A6E File Offset: 0x00031C6E
		public new string DisplayName
		{
			get
			{
				return base.DisplayName;
			}
			internal set
			{
				base.DisplayName = value;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00033A77 File Offset: 0x00031C77
		// (set) Token: 0x060018CB RID: 6347 RVA: 0x00033A7F File Offset: 0x00031C7F
		public new string Alias
		{
			get
			{
				return base.Alias;
			}
			internal set
			{
				base.Alias = value;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00033A88 File Offset: 0x00031C88
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x00033A90 File Offset: 0x00031C90
		public new Guid ExchangeGuid
		{
			get
			{
				return base.ExchangeGuid;
			}
			internal set
			{
				base.ExchangeGuid = value;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00033A99 File Offset: 0x00031C99
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x00033AA1 File Offset: 0x00031CA1
		public new Guid? ArchiveGuid
		{
			get
			{
				return base.ArchiveGuid;
			}
			internal set
			{
				base.ArchiveGuid = value;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00033AAA File Offset: 0x00031CAA
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x00033AB2 File Offset: 0x00031CB2
		public new RequestStatus Status
		{
			get
			{
				return base.Status;
			}
			internal set
			{
				base.Status = value;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00033ABB File Offset: 0x00031CBB
		public new RequestState StatusDetail
		{
			get
			{
				return base.StatusDetail;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x00033AC3 File Offset: 0x00031CC3
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x00033ACB File Offset: 0x00031CCB
		public new SyncStage SyncStage
		{
			get
			{
				return base.SyncStage;
			}
			internal set
			{
				base.SyncStage = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x00033AD4 File Offset: 0x00031CD4
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x00033ADC File Offset: 0x00031CDC
		public new RequestFlags Flags
		{
			get
			{
				return base.Flags;
			}
			internal set
			{
				base.Flags = value;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00033AE5 File Offset: 0x00031CE5
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x00033AED File Offset: 0x00031CED
		public new RequestStyle RequestStyle
		{
			get
			{
				return base.RequestStyle;
			}
			internal set
			{
				base.RequestStyle = value;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00033AF6 File Offset: 0x00031CF6
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x00033AFE File Offset: 0x00031CFE
		public new RequestDirection Direction
		{
			get
			{
				return base.Direction;
			}
			internal set
			{
				base.Direction = value;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00033B07 File Offset: 0x00031D07
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x00033B0F File Offset: 0x00031D0F
		public new bool IsOffline
		{
			get
			{
				return base.IsOffline;
			}
			internal set
			{
				base.IsOffline = value;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00033B18 File Offset: 0x00031D18
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x00033B20 File Offset: 0x00031D20
		public new bool Protect
		{
			get
			{
				return base.Protect;
			}
			internal set
			{
				base.Protect = value;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00033B29 File Offset: 0x00031D29
		public bool DoNotPreserveMailboxSignature
		{
			get
			{
				return !base.PreserveMailboxSignature;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x00033B34 File Offset: 0x00031D34
		// (set) Token: 0x060018E1 RID: 6369 RVA: 0x00033B3C File Offset: 0x00031D3C
		public new RequestPriority Priority
		{
			get
			{
				return base.Priority;
			}
			internal set
			{
				base.Priority = value;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00033B45 File Offset: 0x00031D45
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x00033B4D File Offset: 0x00031D4D
		public new RequestWorkloadType WorkloadType
		{
			get
			{
				return base.WorkloadType;
			}
			internal set
			{
				base.WorkloadType = value;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00033B56 File Offset: 0x00031D56
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x00033B5E File Offset: 0x00031D5E
		public new bool Suspend
		{
			get
			{
				return base.Suspend;
			}
			internal set
			{
				base.Suspend = value;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x00033B67 File Offset: 0x00031D67
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x00033B6F File Offset: 0x00031D6F
		public new bool SuspendWhenReadyToComplete
		{
			get
			{
				return base.SuspendWhenReadyToComplete;
			}
			internal set
			{
				base.SuspendWhenReadyToComplete = value;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00033B78 File Offset: 0x00031D78
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x00033B80 File Offset: 0x00031D80
		public new bool IgnoreRuleLimitErrors
		{
			get
			{
				return base.IgnoreRuleLimitErrors;
			}
			internal set
			{
				base.IgnoreRuleLimitErrors = value;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00033B89 File Offset: 0x00031D89
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x00033B91 File Offset: 0x00031D91
		public new RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return base.RecipientTypeDetails;
			}
			internal set
			{
				base.RecipientTypeDetails = value;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00033B9A File Offset: 0x00031D9A
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x00033BB1 File Offset: 0x00031DB1
		public new ServerVersion SourceVersion
		{
			get
			{
				if (base.SourceVersion == 0)
				{
					return null;
				}
				return new ServerVersion(base.SourceVersion);
			}
			internal set
			{
				base.SourceVersion = ((value != null) ? value.ToInt() : 0);
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00033BCB File Offset: 0x00031DCB
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x00033BD3 File Offset: 0x00031DD3
		public new ADObjectId SourceDatabase
		{
			get
			{
				return base.SourceDatabase;
			}
			internal set
			{
				base.SourceDatabase = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x00033BDC File Offset: 0x00031DDC
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x00033BE4 File Offset: 0x00031DE4
		public new string SourceServer
		{
			get
			{
				return base.SourceServer;
			}
			internal set
			{
				base.SourceServer = value;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x00033BED File Offset: 0x00031DED
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x00033C04 File Offset: 0x00031E04
		public new ServerVersion TargetVersion
		{
			get
			{
				if (base.TargetVersion == 0)
				{
					return null;
				}
				return new ServerVersion(base.TargetVersion);
			}
			set
			{
				base.TargetVersion = ((value != null) ? value.ToInt() : 0);
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x00033C1E File Offset: 0x00031E1E
		// (set) Token: 0x060018F5 RID: 6389 RVA: 0x00033C26 File Offset: 0x00031E26
		public new ADObjectId TargetDatabase
		{
			get
			{
				return base.TargetDatabase;
			}
			internal set
			{
				base.TargetDatabase = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060018F6 RID: 6390 RVA: 0x00033C2F File Offset: 0x00031E2F
		// (set) Token: 0x060018F7 RID: 6391 RVA: 0x00033C37 File Offset: 0x00031E37
		public new string TargetServer
		{
			get
			{
				return base.TargetServer;
			}
			internal set
			{
				base.TargetServer = value;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x00033C40 File Offset: 0x00031E40
		// (set) Token: 0x060018F9 RID: 6393 RVA: 0x00033C48 File Offset: 0x00031E48
		public new ADObjectId SourceArchiveDatabase
		{
			get
			{
				return base.SourceArchiveDatabase;
			}
			internal set
			{
				base.SourceArchiveDatabase = value;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x00033C51 File Offset: 0x00031E51
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x00033C68 File Offset: 0x00031E68
		public new ServerVersion SourceArchiveVersion
		{
			get
			{
				if (base.SourceArchiveVersion == 0)
				{
					return null;
				}
				return new ServerVersion(base.SourceArchiveVersion);
			}
			internal set
			{
				base.SourceArchiveVersion = ((value != null) ? value.ToInt() : 0);
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x00033C82 File Offset: 0x00031E82
		// (set) Token: 0x060018FD RID: 6397 RVA: 0x00033C8A File Offset: 0x00031E8A
		public new string SourceArchiveServer
		{
			get
			{
				return base.SourceArchiveServer;
			}
			internal set
			{
				base.SourceArchiveServer = value;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x00033C93 File Offset: 0x00031E93
		// (set) Token: 0x060018FF RID: 6399 RVA: 0x00033C9B File Offset: 0x00031E9B
		public new ADObjectId TargetArchiveDatabase
		{
			get
			{
				return base.TargetArchiveDatabase;
			}
			internal set
			{
				base.TargetArchiveDatabase = value;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x00033CA4 File Offset: 0x00031EA4
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x00033CBB File Offset: 0x00031EBB
		public new ServerVersion TargetArchiveVersion
		{
			get
			{
				if (base.TargetArchiveVersion == 0)
				{
					return null;
				}
				return new ServerVersion(base.TargetArchiveVersion);
			}
			internal set
			{
				base.TargetArchiveVersion = ((value != null) ? value.ToInt() : 0);
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x00033CD5 File Offset: 0x00031ED5
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x00033CDD File Offset: 0x00031EDD
		public new string TargetArchiveServer
		{
			get
			{
				return base.TargetArchiveServer;
			}
			internal set
			{
				base.TargetArchiveServer = value;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x00033CE6 File Offset: 0x00031EE6
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x00033CEE File Offset: 0x00031EEE
		public new string RemoteHostName
		{
			get
			{
				return base.RemoteHostName;
			}
			internal set
			{
				base.RemoteHostName = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x00033CF7 File Offset: 0x00031EF7
		public new string RemoteGlobalCatalog
		{
			get
			{
				return base.RemoteGlobalCatalog;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x00033CFF File Offset: 0x00031EFF
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x00033D07 File Offset: 0x00031F07
		public new string BatchName
		{
			get
			{
				return base.BatchName;
			}
			internal set
			{
				base.BatchName = value;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x00033D10 File Offset: 0x00031F10
		public DateTime? StartAfter
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.StartAfter);
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x00033D1F File Offset: 0x00031F1F
		public DateTime? CompleteAfter
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.CompleteAfter);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x00033D2E File Offset: 0x00031F2E
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x00033D36 File Offset: 0x00031F36
		public new string RemoteCredentialUsername
		{
			get
			{
				return base.RemoteCredentialUsername;
			}
			internal set
			{
				base.RemoteCredentialUsername = value;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x00033D3F File Offset: 0x00031F3F
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x00033D47 File Offset: 0x00031F47
		public new string RemoteDatabaseName
		{
			get
			{
				return base.RemoteDatabaseName;
			}
			internal set
			{
				base.RemoteDatabaseName = value;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x00033D50 File Offset: 0x00031F50
		// (set) Token: 0x06001910 RID: 6416 RVA: 0x00033D58 File Offset: 0x00031F58
		public new Guid? RemoteDatabaseGuid
		{
			get
			{
				return base.RemoteDatabaseGuid;
			}
			internal set
			{
				base.RemoteDatabaseGuid = value;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x00033D61 File Offset: 0x00031F61
		// (set) Token: 0x06001912 RID: 6418 RVA: 0x00033D69 File Offset: 0x00031F69
		public new string RemoteArchiveDatabaseName
		{
			get
			{
				return base.RemoteArchiveDatabaseName;
			}
			internal set
			{
				base.RemoteArchiveDatabaseName = value;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x00033D72 File Offset: 0x00031F72
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x00033D7A File Offset: 0x00031F7A
		public new Guid? RemoteArchiveDatabaseGuid
		{
			get
			{
				return base.RemoteArchiveDatabaseGuid;
			}
			internal set
			{
				base.RemoteArchiveDatabaseGuid = value;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x00033D83 File Offset: 0x00031F83
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x00033D8B File Offset: 0x00031F8B
		public new string TargetDeliveryDomain
		{
			get
			{
				return base.TargetDeliveryDomain;
			}
			internal set
			{
				base.TargetDeliveryDomain = value;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x00033D94 File Offset: 0x00031F94
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x00033D9C File Offset: 0x00031F9C
		public new string ArchiveDomain
		{
			get
			{
				return base.ArchiveDomain;
			}
			internal set
			{
				base.ArchiveDomain = value;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x00033DA5 File Offset: 0x00031FA5
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x00033DAD File Offset: 0x00031FAD
		public new Unlimited<int> BadItemLimit
		{
			get
			{
				return base.BadItemLimit;
			}
			internal set
			{
				base.BadItemLimit = value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x00033DB6 File Offset: 0x00031FB6
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x00033DBE File Offset: 0x00031FBE
		public new int BadItemsEncountered
		{
			get
			{
				return base.BadItemsEncountered;
			}
			internal set
			{
				base.BadItemsEncountered = value;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x00033DC7 File Offset: 0x00031FC7
		// (set) Token: 0x0600191E RID: 6430 RVA: 0x00033DCF File Offset: 0x00031FCF
		public new Unlimited<int> LargeItemLimit
		{
			get
			{
				return base.LargeItemLimit;
			}
			internal set
			{
				base.LargeItemLimit = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x00033DD8 File Offset: 0x00031FD8
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x00033DE0 File Offset: 0x00031FE0
		public new int LargeItemsEncountered
		{
			get
			{
				return base.LargeItemsEncountered;
			}
			internal set
			{
				base.LargeItemsEncountered = value;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x00033DE9 File Offset: 0x00031FE9
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x00033DF1 File Offset: 0x00031FF1
		public new bool AllowLargeItems
		{
			get
			{
				return base.AllowLargeItems;
			}
			internal set
			{
				base.AllowLargeItems = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x00033DFA File Offset: 0x00031FFA
		public DateTime? QueuedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Creation);
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x00033E08 File Offset: 0x00032008
		public DateTime? StartTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Start);
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x00033E16 File Offset: 0x00032016
		public DateTime? LastUpdateTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastUpdate);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x00033E24 File Offset: 0x00032024
		public DateTime? InitialSeedingCompletedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.InitialSeedingCompleted);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x00033E32 File Offset: 0x00032032
		public DateTime? FinalSyncTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.FinalSync);
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00033E40 File Offset: 0x00032040
		public DateTime? CompletionTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Completion);
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x00033E4E File Offset: 0x0003204E
		public DateTime? SuspendedTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Suspended);
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00033E5C File Offset: 0x0003205C
		public EnhancedTimeSpan? OverallDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.OverallMove).Duration);
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x00033E79 File Offset: 0x00032079
		public EnhancedTimeSpan? TotalFinalizationDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Finalization).Duration);
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x00033E97 File Offset: 0x00032097
		public EnhancedTimeSpan? TotalDataReplicationWaitDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.DataReplicationWait).Duration);
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x00033EB5 File Offset: 0x000320B5
		public EnhancedTimeSpan? TotalSuspendedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Suspended).Duration);
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x00033ED3 File Offset: 0x000320D3
		public EnhancedTimeSpan? TotalFailedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Failed).Duration);
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x00033EF1 File Offset: 0x000320F1
		public EnhancedTimeSpan? TotalQueuedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Queued).Duration);
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00033F0E File Offset: 0x0003210E
		public EnhancedTimeSpan? TotalInProgressDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.InProgress).Duration);
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x00033F2B File Offset: 0x0003212B
		public EnhancedTimeSpan? TotalStalledDueToCIDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToCI).Duration);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x00033F49 File Offset: 0x00032149
		public EnhancedTimeSpan? TotalStalledDueToHADuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToHA).Duration);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x00033F67 File Offset: 0x00032167
		public EnhancedTimeSpan? TotalStalledDueToMailboxLockedDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToMailboxLock).Duration);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x00033F85 File Offset: 0x00032185
		public EnhancedTimeSpan? TotalStalledDueToReadThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadThrottle).Duration);
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x00033FA3 File Offset: 0x000321A3
		public EnhancedTimeSpan? TotalStalledDueToWriteThrottle
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteThrottle).Duration);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x00033FC1 File Offset: 0x000321C1
		public EnhancedTimeSpan? TotalStalledDueToReadCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadCpu).Duration);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x00033FDF File Offset: 0x000321DF
		public EnhancedTimeSpan? TotalStalledDueToWriteCpu
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteCpu).Duration);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x00033FFD File Offset: 0x000321FD
		public EnhancedTimeSpan? TotalStalledDueToReadUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToReadUnknown).Duration);
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x0003401B File Offset: 0x0003221B
		public EnhancedTimeSpan? TotalStalledDueToWriteUnknown
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.StalledDueToWriteUnknown).Duration);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x00034039 File Offset: 0x00032239
		public EnhancedTimeSpan? TotalTransientFailureDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.TransientFailure).Duration);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x00034057 File Offset: 0x00032257
		public EnhancedTimeSpan? TotalProxyBackoffDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.ProxyBackoff).Duration);
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x00034075 File Offset: 0x00032275
		public EnhancedTimeSpan? TotalIdleDuration
		{
			get
			{
				return new EnhancedTimeSpan?(base.TimeTracker.GetDisplayDuration(RequestState.Idle).Duration);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x00034093 File Offset: 0x00032293
		// (set) Token: 0x0600193E RID: 6462 RVA: 0x0003409B File Offset: 0x0003229B
		public new string MRSServerName
		{
			get
			{
				return base.MRSServerName;
			}
			internal set
			{
				base.MRSServerName = value;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x000340A4 File Offset: 0x000322A4
		// (set) Token: 0x06001940 RID: 6464 RVA: 0x000340B1 File Offset: 0x000322B1
		public new ByteQuantifiedSize TotalMailboxSize
		{
			get
			{
				return new ByteQuantifiedSize(base.TotalMailboxSize);
			}
			internal set
			{
				base.TotalMailboxSize = value.ToBytes();
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x000340C0 File Offset: 0x000322C0
		// (set) Token: 0x06001942 RID: 6466 RVA: 0x000340C8 File Offset: 0x000322C8
		public new ulong TotalMailboxItemCount
		{
			get
			{
				return base.TotalMailboxItemCount;
			}
			internal set
			{
				base.TotalMailboxItemCount = value;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x000340D4 File Offset: 0x000322D4
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x00034114 File Offset: 0x00032314
		public new ByteQuantifiedSize? TotalArchiveSize
		{
			get
			{
				if (base.TotalArchiveSize != null)
				{
					return new ByteQuantifiedSize?(new ByteQuantifiedSize(base.TotalArchiveSize.Value));
				}
				return null;
			}
			internal set
			{
				if (value != null)
				{
					base.TotalArchiveSize = new ulong?(value.Value.ToBytes());
					return;
				}
				base.TotalArchiveSize = null;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00034154 File Offset: 0x00032354
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x0003415C File Offset: 0x0003235C
		public new ulong? TotalArchiveItemCount
		{
			get
			{
				return base.TotalArchiveItemCount;
			}
			internal set
			{
				base.TotalArchiveItemCount = value;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00034165 File Offset: 0x00032365
		public override ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				return base.BytesTransferred;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x0003416D File Offset: 0x0003236D
		public override ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				return base.BytesTransferredPerMinute;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x00034175 File Offset: 0x00032375
		public override ulong? ItemsTransferred
		{
			get
			{
				return base.ItemsTransferred;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0003417D File Offset: 0x0003237D
		// (set) Token: 0x0600194B RID: 6475 RVA: 0x00034185 File Offset: 0x00032385
		public new int PercentComplete
		{
			get
			{
				return base.PercentComplete;
			}
			internal set
			{
				base.PercentComplete = value;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0003418E File Offset: 0x0003238E
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x00034196 File Offset: 0x00032396
		public new Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
		{
			get
			{
				return base.CompletedRequestAgeLimit;
			}
			internal set
			{
				base.CompletedRequestAgeLimit = value;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0003419F File Offset: 0x0003239F
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x000341A7 File Offset: 0x000323A7
		public override LocalizedString PositionInQueue
		{
			get
			{
				return base.PositionInQueue;
			}
			internal set
			{
				base.PositionInQueue = value;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x000341B0 File Offset: 0x000323B0
		// (set) Token: 0x06001951 RID: 6481 RVA: 0x000341B8 File Offset: 0x000323B8
		public RequestJobInternalFlags InternalFlags
		{
			get
			{
				return base.RequestJobInternalFlags;
			}
			internal set
			{
				base.RequestJobInternalFlags = value;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x000341C1 File Offset: 0x000323C1
		// (set) Token: 0x06001953 RID: 6483 RVA: 0x000341C9 File Offset: 0x000323C9
		public new int? FailureCode
		{
			get
			{
				return base.FailureCode;
			}
			internal set
			{
				base.FailureCode = value;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x000341D2 File Offset: 0x000323D2
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x000341DA File Offset: 0x000323DA
		public new string FailureType
		{
			get
			{
				return base.FailureType;
			}
			internal set
			{
				base.FailureType = value;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x000341E3 File Offset: 0x000323E3
		// (set) Token: 0x06001957 RID: 6487 RVA: 0x000341EB File Offset: 0x000323EB
		public new ExceptionSide? FailureSide
		{
			get
			{
				return base.FailureSide;
			}
			internal set
			{
				base.FailureSide = value;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x000341F4 File Offset: 0x000323F4
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x000341FC File Offset: 0x000323FC
		public new LocalizedString Message
		{
			get
			{
				return base.Message;
			}
			internal set
			{
				base.Message = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x00034205 File Offset: 0x00032405
		public DateTime? FailureTimestamp
		{
			get
			{
				return base.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.Failure);
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00034214 File Offset: 0x00032414
		public override bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0003421C File Offset: 0x0003241C
		// (set) Token: 0x0600195D RID: 6493 RVA: 0x00034224 File Offset: 0x00032424
		public new LocalizedString ValidationMessage
		{
			get
			{
				return base.ValidationMessage;
			}
			internal set
			{
				base.ValidationMessage = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x0003422D File Offset: 0x0003242D
		// (set) Token: 0x0600195F RID: 6495 RVA: 0x00034235 File Offset: 0x00032435
		public new Guid RequestGuid
		{
			get
			{
				return base.RequestGuid;
			}
			internal set
			{
				base.RequestGuid = value;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0003423E File Offset: 0x0003243E
		// (set) Token: 0x06001961 RID: 6497 RVA: 0x00034246 File Offset: 0x00032446
		public new ADObjectId RequestQueue
		{
			get
			{
				return base.RequestQueue;
			}
			internal set
			{
				base.RequestQueue = value;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0003424F File Offset: 0x0003244F
		public new ObjectId Identity
		{
			get
			{
				return base.UserId;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00034257 File Offset: 0x00032457
		public new string DiagnosticInfo
		{
			get
			{
				return base.DiagnosticInfo;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x0003425F File Offset: 0x0003245F
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x00034267 File Offset: 0x00032467
		public override Report Report
		{
			get
			{
				return base.Report;
			}
			internal set
			{
				base.Report = value;
			}
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00034270 File Offset: 0x00032470
		public override string ToString()
		{
			string result;
			if ((result = this.Alias) == null)
			{
				if (this.MailboxIdentity != null)
				{
					return this.MailboxIdentity.ToString();
				}
				result = base.ToString();
			}
			return result;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00034298 File Offset: 0x00032498
		internal static void ValidateRequestJob(RequestJobBase requestJob)
		{
			if (requestJob.IsFake || requestJob.WorkItemQueueMdb == null)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMissing);
				requestJob.ValidationMessage = MrsStrings.ValidationMoveRequestNotDeserialized;
				return;
			}
			if (requestJob.OriginatingMDBGuid != Guid.Empty && requestJob.OriginatingMDBGuid != requestJob.WorkItemQueueMdb.ObjectGuid)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Orphaned);
				requestJob.ValidationMessage = MrsStrings.ValidationMoveRequestInWrongMDB(requestJob.OriginatingMDBGuid, requestJob.WorkItemQueueMdb.ObjectGuid);
				return;
			}
			if (requestJob.OriginatingMDBGuid == Guid.Empty)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
				return;
			}
			MoveRequestStatistics.LoadAdditionalPropertiesFromUser(requestJob);
			if (requestJob.CancelRequest || requestJob.Status == RequestStatus.Completed || requestJob.Status == RequestStatus.CompletedWithWarning)
			{
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
				requestJob.ValidationMessage = LocalizedString.Empty;
				return;
			}
			if (!requestJob.ValidateUser(requestJob.User, requestJob.UserId))
			{
				return;
			}
			Guid guid;
			Guid guid2;
			RequestIndexEntryProvider.GetMoveGuids(requestJob.User, out guid, out guid2);
			if (guid != requestJob.ExchangeGuid)
			{
				MrsTracer.Common.Error("Orphaned RequestJob: mailbox guid does not match between AD {0} and workitem queue {1}.", new object[]
				{
					requestJob.User.ExchangeGuid,
					requestJob.ExchangeGuid
				});
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
				requestJob.ValidationMessage = MrsStrings.ValidationMailboxGuidsDontMatch(guid, requestJob.ExchangeGuid);
				return;
			}
			if (requestJob.User.MailboxMoveStatus == RequestStatus.None)
			{
				MrsTracer.Common.Warning("Orphaned RequestJob: AD user {0} is not being moved.", new object[]
				{
					requestJob.User.ToString()
				});
				requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Orphaned);
				requestJob.ValidationMessage = MrsStrings.ValidationADUserIsNotBeingMoved;
				return;
			}
			if (requestJob.Flags != requestJob.User.MailboxMoveFlags)
			{
				if ((requestJob.Flags & RequestJobBase.StaticFlags) != (requestJob.User.MailboxMoveFlags & RequestJobBase.StaticFlags))
				{
					MrsTracer.Common.Error("Mismatched RequestJob: flags don't match: AD [{0}], workitem queue [{1}]", new object[]
					{
						requestJob.User.MailboxMoveFlags,
						requestJob.Flags
					});
					requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
					requestJob.ValidationMessage = MrsStrings.ValidationFlagsMismatch(requestJob.User.MailboxMoveFlags.ToString(), requestJob.Flags.ToString());
					return;
				}
				MrsTracer.Common.Debug("Possibly mismatched RequestJob: flags don't match: AD [{0}], workitem queue [{1}]", new object[]
				{
					requestJob.User.MailboxMoveFlags,
					requestJob.Flags
				});
			}
			if (requestJob.PrimaryIsMoving)
			{
				if (requestJob.SourceIsLocal && (requestJob.SourceDatabase == null || !requestJob.SourceDatabase.Equals(requestJob.User.MailboxMoveSourceMDB)))
				{
					MrsTracer.Common.Error("Mismatched RequestJob: Source database does not match between AD ({0}) and RequestJob ({1})", new object[]
					{
						requestJob.User.MailboxMoveSourceMDB,
						requestJob.SourceDatabase
					});
					requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
					requestJob.ValidationMessage = MrsStrings.ValidationSourceMDBMismatch((requestJob.User.MailboxMoveSourceMDB != null) ? requestJob.User.MailboxMoveSourceMDB.ToString() : "(null)", (requestJob.SourceDatabase != null) ? requestJob.SourceDatabase.ToString() : "(null)");
					return;
				}
				if (requestJob.TargetIsLocal && (requestJob.TargetDatabase == null || (!requestJob.RehomeRequest && !requestJob.TargetDatabase.Equals(requestJob.User.MailboxMoveTargetMDB))))
				{
					MrsTracer.Common.Error("Target database does not match between AD ({0}) and RequestJob ({1})", new object[]
					{
						requestJob.User.MailboxMoveTargetMDB,
						requestJob.TargetDatabase
					});
					requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
					requestJob.ValidationMessage = MrsStrings.ValidationTargetMDBMismatch((requestJob.User.MailboxMoveTargetMDB != null) ? requestJob.User.MailboxMoveTargetMDB.ToString() : "(null)", (requestJob.TargetDatabase != null) ? requestJob.TargetDatabase.ToString() : "(null)");
					return;
				}
			}
			if (requestJob.JobType >= MRSJobType.RequestJobE14R5_PrimaryOrArchiveExclusiveMoves && requestJob.ArchiveIsMoving)
			{
				if (requestJob.SourceIsLocal && (requestJob.SourceArchiveDatabase == null || !requestJob.SourceArchiveDatabase.Equals(requestJob.User.MailboxMoveSourceArchiveMDB)))
				{
					MrsTracer.Common.Error("Mismatched RequestJob: Source archive database does not match between AD ({0}) and RequestJob ({1})", new object[]
					{
						requestJob.User.MailboxMoveSourceArchiveMDB,
						requestJob.SourceArchiveDatabase
					});
					requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
					requestJob.ValidationMessage = MrsStrings.ValidationSourceArchiveMDBMismatch((requestJob.User.MailboxMoveSourceArchiveMDB != null) ? requestJob.User.MailboxMoveSourceArchiveMDB.ToString() : "(null)", (requestJob.SourceArchiveDatabase != null) ? requestJob.SourceArchiveDatabase.ToString() : "(null)");
					return;
				}
				if (requestJob.TargetIsLocal && (requestJob.TargetArchiveDatabase == null || (!requestJob.RehomeRequest && !requestJob.TargetArchiveDatabase.Equals(requestJob.User.MailboxMoveTargetArchiveMDB))))
				{
					MrsTracer.Common.Error("Target archive database does not match between AD ({0}) and RequestJob ({1})", new object[]
					{
						requestJob.User.MailboxMoveTargetArchiveMDB,
						requestJob.TargetArchiveDatabase
					});
					requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMismatch);
					requestJob.ValidationMessage = MrsStrings.ValidationTargetArchiveMDBMismatch((requestJob.User.MailboxMoveTargetArchiveMDB != null) ? requestJob.User.MailboxMoveTargetArchiveMDB.ToString() : "(null)", (requestJob.TargetArchiveDatabase != null) ? requestJob.TargetArchiveDatabase.ToString() : "(null)");
					return;
				}
			}
			requestJob.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
			requestJob.ValidationMessage = LocalizedString.Empty;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00034868 File Offset: 0x00032A68
		private static void LoadAdditionalPropertiesFromUser(RequestJobBase requestJob)
		{
			if (requestJob.User != null)
			{
				requestJob.Alias = requestJob.User.Alias;
				requestJob.DisplayName = requestJob.User.DisplayName;
				requestJob.RecipientTypeDetails = requestJob.User.RecipientTypeDetails;
				requestJob.UserId = requestJob.User.Id;
			}
		}
	}
}
