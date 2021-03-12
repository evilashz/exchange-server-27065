using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	public class RequestJobBase : ConfigurableObject, ISettingsContextProvider
	{
		// Token: 0x0600110B RID: 4363 RVA: 0x00027D98 File Offset: 0x00025F98
		public RequestJobBase() : this(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00027DB0 File Offset: 0x00025FB0
		internal RequestJobBase(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
			this.isFake = false;
			this.isRetired = false;
			this.timeTracker = new RequestJobTimeTracker();
			this.ProgressTracker = new TransferProgressTracker();
			this.indexEntries = new List<IRequestIndexEntry>();
			this.indexIds = new List<RequestIndexId>();
			this.folderToMailboxMapping = new List<FolderToMailboxMapping>();
			this.folderList = new List<MoveFolderInfo>();
			this.jobConfigContext = new Lazy<SettingsContextBase>(() => this.CreateConfigContext());
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00027E34 File Offset: 0x00026034
		public override bool IsValid
		{
			get
			{
				return base.IsValid && this.ValidationResult == RequestJobBase.ValidationResultEnum.Valid;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00027E64 File Offset: 0x00026064
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x00027E6C File Offset: 0x0002606C
		protected internal string DiagnosticInfo { get; protected set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00027E75 File Offset: 0x00026075
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x00027E7D File Offset: 0x0002607D
		internal ADUser User
		{
			get
			{
				return this.adUser;
			}
			set
			{
				this.adUser = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00027E86 File Offset: 0x00026086
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x00027E8E File Offset: 0x0002608E
		internal ADUser TargetUser
		{
			get
			{
				return this.targetUser;
			}
			set
			{
				this.targetUser = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00027E97 File Offset: 0x00026097
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00027E9F File Offset: 0x0002609F
		internal ADUser SourceUser
		{
			get
			{
				return this.sourceUser;
			}
			set
			{
				this.sourceUser = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00027EA8 File Offset: 0x000260A8
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00027EB0 File Offset: 0x000260B0
		internal List<IRequestIndexEntry> IndexEntries
		{
			get
			{
				return this.indexEntries;
			}
			set
			{
				this.indexEntries = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00027EB9 File Offset: 0x000260B9
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00027EC1 File Offset: 0x000260C1
		internal RequestJobBase.ValidationResultEnum? ValidationResult
		{
			get
			{
				return this.validationResult;
			}
			set
			{
				this.validationResult = value;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x00027ECA File Offset: 0x000260CA
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x00027ED2 File Offset: 0x000260D2
		internal LocalizedString ValidationMessage
		{
			get
			{
				return this.validationMessage;
			}
			set
			{
				this.validationMessage = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00027EDB File Offset: 0x000260DB
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00027EE3 File Offset: 0x000260E3
		internal Guid OriginatingMDBGuid
		{
			get
			{
				return this.originatingMDBGuid;
			}
			set
			{
				this.originatingMDBGuid = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x00027EEC File Offset: 0x000260EC
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x00027EF4 File Offset: 0x000260F4
		internal bool IsFake
		{
			get
			{
				return this.isFake;
			}
			set
			{
				this.isFake = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00027EFD File Offset: 0x000260FD
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x00027F05 File Offset: 0x00026105
		internal List<RequestIndexId> IndexIds
		{
			get
			{
				return this.indexIds;
			}
			set
			{
				this.indexIds = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00027F0E File Offset: 0x0002610E
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x00027F16 File Offset: 0x00026116
		internal RequestJobTimeTracker TimeTracker
		{
			get
			{
				return this.timeTracker;
			}
			set
			{
				this.timeTracker = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00027F1F File Offset: 0x0002611F
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x00027F27 File Offset: 0x00026127
		internal TransferProgressTracker ProgressTracker { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00027F30 File Offset: 0x00026130
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x00027F38 File Offset: 0x00026138
		internal SkippedItemCounts SkippedItemCounts
		{
			get
			{
				return this.skippedItemCounts;
			}
			set
			{
				this.skippedItemCounts = value;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00027F41 File Offset: 0x00026141
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x00027F49 File Offset: 0x00026149
		internal FailureHistory FailureHistory
		{
			get
			{
				return this.failureHistory;
			}
			set
			{
				this.failureHistory = value;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00027F52 File Offset: 0x00026152
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x00027F64 File Offset: 0x00026164
		internal new RequestJobObjectId Identity
		{
			get
			{
				return (RequestJobObjectId)this[SimpleProviderObjectSchema.Identity];
			}
			set
			{
				this[SimpleProviderObjectSchema.Identity] = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00027F72 File Offset: 0x00026172
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00027F84 File Offset: 0x00026184
		internal ADObjectId UserId
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.UserId];
			}
			set
			{
				this[RequestJobSchema.UserId] = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00027F94 File Offset: 0x00026194
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00027FFC File Offset: 0x000261FC
		internal string DistinguishedName
		{
			get
			{
				if (this.UserId == null)
				{
					return null;
				}
				if (!string.IsNullOrEmpty(this.UserId.DistinguishedName))
				{
					return this.UserId.DistinguishedName;
				}
				if (this.UserId.ObjectGuid != Guid.Empty)
				{
					return string.Format("<GUID={0}>", this.UserId.ObjectGuid);
				}
				return null;
			}
			set
			{
				if (this.UserId == null)
				{
					this.UserId = new ADObjectId(value);
					return;
				}
				this.UserId = new ADObjectId(value, this.UserId.ObjectGuid);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0002802A File Offset: 0x0002622A
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x0002803C File Offset: 0x0002623C
		internal string DisplayName
		{
			get
			{
				return (string)this[RequestJobSchema.DisplayName];
			}
			set
			{
				this[RequestJobSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0002804A File Offset: 0x0002624A
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x0002805C File Offset: 0x0002625C
		internal string Alias
		{
			get
			{
				return (string)this[RequestJobSchema.Alias];
			}
			set
			{
				this[RequestJobSchema.Alias] = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0002806A File Offset: 0x0002626A
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x0002807C File Offset: 0x0002627C
		internal string SourceAlias
		{
			get
			{
				return (string)this[RequestJobSchema.SourceAlias];
			}
			set
			{
				this[RequestJobSchema.SourceAlias] = value;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0002808A File Offset: 0x0002628A
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x0002809C File Offset: 0x0002629C
		internal string TargetAlias
		{
			get
			{
				return (string)this[RequestJobSchema.TargetAlias];
			}
			set
			{
				this[RequestJobSchema.TargetAlias] = value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x000280AA File Offset: 0x000262AA
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x000280BC File Offset: 0x000262BC
		internal Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[RequestJobSchema.ExchangeGuid];
			}
			set
			{
				this[RequestJobSchema.ExchangeGuid] = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x000280CF File Offset: 0x000262CF
		// (set) Token: 0x0600113B RID: 4411 RVA: 0x000280E1 File Offset: 0x000262E1
		internal Guid SourceExchangeGuid
		{
			get
			{
				return (Guid)this[RequestJobSchema.SourceExchangeGuid];
			}
			set
			{
				this[RequestJobSchema.SourceExchangeGuid] = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x000280F4 File Offset: 0x000262F4
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x00028106 File Offset: 0x00026306
		internal Guid TargetExchangeGuid
		{
			get
			{
				return (Guid)this[RequestJobSchema.TargetExchangeGuid];
			}
			set
			{
				this[RequestJobSchema.TargetExchangeGuid] = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x00028119 File Offset: 0x00026319
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x0002812B File Offset: 0x0002632B
		internal Guid? ArchiveGuid
		{
			get
			{
				return (Guid?)this[RequestJobSchema.ArchiveGuid];
			}
			set
			{
				this[RequestJobSchema.ArchiveGuid] = value;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0002813E File Offset: 0x0002633E
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x00028150 File Offset: 0x00026350
		internal string SourceRootFolder
		{
			get
			{
				return (string)this[RequestJobSchema.SourceRootFolder];
			}
			set
			{
				this[RequestJobSchema.SourceRootFolder] = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0002815E File Offset: 0x0002635E
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x00028170 File Offset: 0x00026370
		internal string TargetRootFolder
		{
			get
			{
				return (string)this[RequestJobSchema.TargetRootFolder];
			}
			set
			{
				this[RequestJobSchema.TargetRootFolder] = value;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0002817E File Offset: 0x0002637E
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x00028190 File Offset: 0x00026390
		internal bool SourceIsArchive
		{
			get
			{
				return (bool)this[RequestJobSchema.SourceIsArchive];
			}
			set
			{
				this[RequestJobSchema.SourceIsArchive] = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x000281A3 File Offset: 0x000263A3
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x000281B5 File Offset: 0x000263B5
		internal bool TargetIsArchive
		{
			get
			{
				return (bool)this[RequestJobSchema.TargetIsArchive];
			}
			set
			{
				this[RequestJobSchema.TargetIsArchive] = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x000281C8 File Offset: 0x000263C8
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x000281DF File Offset: 0x000263DF
		internal string[] IncludeFolders
		{
			get
			{
				return ((MultiValuedProperty<string>)this[RequestJobSchema.IncludeFolders]).ToArray();
			}
			set
			{
				this[RequestJobSchema.IncludeFolders] = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x000281F2 File Offset: 0x000263F2
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x00028209 File Offset: 0x00026409
		internal string[] ExcludeFolders
		{
			get
			{
				return ((MultiValuedProperty<string>)this[RequestJobSchema.ExcludeFolders]).ToArray();
			}
			set
			{
				this[RequestJobSchema.ExcludeFolders] = value;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0002821C File Offset: 0x0002641C
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x0002822E File Offset: 0x0002642E
		internal bool ExcludeDumpster
		{
			get
			{
				return (bool)this[RequestJobSchema.ExcludeDumpster];
			}
			set
			{
				this[RequestJobSchema.ExcludeDumpster] = value;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00028241 File Offset: 0x00026441
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x00028253 File Offset: 0x00026453
		internal Guid RequestGuid
		{
			get
			{
				return (Guid)this[RequestJobSchema.RequestGuid];
			}
			set
			{
				this[RequestJobSchema.RequestGuid] = value;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00028266 File Offset: 0x00026466
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x00028278 File Offset: 0x00026478
		internal RequestStatus Status
		{
			get
			{
				return (RequestStatus)this[RequestJobSchema.Status];
			}
			set
			{
				this[RequestJobSchema.Status] = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x0002828B File Offset: 0x0002648B
		internal RequestState StatusDetail
		{
			get
			{
				return this.TimeTracker.CurrentState;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00028298 File Offset: 0x00026498
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x000282C9 File Offset: 0x000264C9
		internal RequestFlags Flags
		{
			get
			{
				RequestFlags requestFlags = (RequestFlags)this[RequestJobSchema.Flags];
				if (this.Priority > RequestPriority.Normal)
				{
					requestFlags |= RequestFlags.HighPriority;
				}
				return requestFlags;
			}
			set
			{
				value &= ~RequestFlags.HighPriority;
				this[RequestJobSchema.Flags] = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x000282E5 File Offset: 0x000264E5
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x000282F7 File Offset: 0x000264F7
		internal RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[RequestJobSchema.RecipientTypeDetails];
			}
			set
			{
				this[RequestJobSchema.RecipientTypeDetails] = (long)value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0002830A File Offset: 0x0002650A
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0002831C File Offset: 0x0002651C
		internal int SourceVersion
		{
			get
			{
				return (int)this[RequestJobSchema.SourceVersion];
			}
			set
			{
				this[RequestJobSchema.SourceVersion] = value;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0002832F File Offset: 0x0002652F
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00028341 File Offset: 0x00026541
		internal ADObjectId SourceDatabase
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.SourceDatabase];
			}
			set
			{
				this[RequestJobSchema.SourceDatabase] = value;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0002834F File Offset: 0x0002654F
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x00028361 File Offset: 0x00026561
		internal string SourceServer
		{
			get
			{
				return (string)this[RequestJobSchema.SourceServer];
			}
			set
			{
				this[RequestJobSchema.SourceServer] = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0002836F File Offset: 0x0002656F
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x00028381 File Offset: 0x00026581
		internal int TargetVersion
		{
			get
			{
				return (int)this[RequestJobSchema.TargetVersion];
			}
			set
			{
				this[RequestJobSchema.TargetVersion] = value;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x00028394 File Offset: 0x00026594
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x000283A6 File Offset: 0x000265A6
		internal ADObjectId TargetDatabase
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.TargetDatabase];
			}
			set
			{
				this[RequestJobSchema.TargetDatabase] = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000283B4 File Offset: 0x000265B4
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x000283C6 File Offset: 0x000265C6
		internal string TargetServer
		{
			get
			{
				return (string)this[RequestJobSchema.TargetServer];
			}
			set
			{
				this[RequestJobSchema.TargetServer] = value;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x000283D4 File Offset: 0x000265D4
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x000283E6 File Offset: 0x000265E6
		internal Guid? TargetContainerGuid
		{
			get
			{
				return (Guid?)this[RequestJobSchema.TargetContainerGuid];
			}
			set
			{
				this[RequestJobSchema.TargetContainerGuid] = value;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x000283F9 File Offset: 0x000265F9
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x0002840B File Offset: 0x0002660B
		internal CrossTenantObjectId TargetUnifiedMailboxId
		{
			get
			{
				return (CrossTenantObjectId)this[RequestJobSchema.TargetUnifiedMailboxId];
			}
			set
			{
				this[RequestJobSchema.TargetUnifiedMailboxId] = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00028419 File Offset: 0x00026619
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x0002842B File Offset: 0x0002662B
		internal ADObjectId RequestQueue
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.RequestQueue];
			}
			set
			{
				this[RequestJobSchema.RequestQueue] = value;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00028439 File Offset: 0x00026639
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x00028455 File Offset: 0x00026655
		internal bool RehomeRequest
		{
			get
			{
				return (bool)(this[RequestJobSchema.RehomeRequest] ?? false);
			}
			set
			{
				this[RequestJobSchema.RehomeRequest] = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x00028468 File Offset: 0x00026668
		internal ADObjectId OptimalRequestQueue
		{
			get
			{
				MRSRequestType requestType = this.RequestType;
				if (requestType != MRSRequestType.Move)
				{
					switch (requestType)
					{
					case MRSRequestType.PublicFolderMove:
					case MRSRequestType.PublicFolderMigration:
					case MRSRequestType.PublicFolderMailboxMigration:
						return this.RequestQueue;
					case MRSRequestType.FolderMove:
						return this.TargetDatabase ?? this.RequestQueue;
					}
					if (this.Direction == RequestDirection.Push)
					{
						return this.SourceDatabase ?? this.RequestQueue;
					}
					return this.TargetDatabase ?? this.RequestQueue;
				}
				else
				{
					if (this.ArchiveOnly)
					{
						return this.TargetArchiveDatabase ?? this.RequestQueue;
					}
					return this.TargetDatabase ?? this.RequestQueue;
				}
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0002850E File Offset: 0x0002670E
		internal bool ShouldSuspendRequest
		{
			get
			{
				return this.Suspend && this.Status != RequestStatus.Suspended && this.Status != RequestStatus.AutoSuspended && this.Status != RequestStatus.Failed && this.Status != RequestStatus.Completed && this.Status != RequestStatus.CompletedWithWarning;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x00028550 File Offset: 0x00026750
		internal bool ShouldRehomeRequest
		{
			get
			{
				return this.RehomeRequest && this.RequestQueue != null && !this.RequestQueue.Equals(this.OptimalRequestQueue) && ((this.RequestType != MRSRequestType.Move && this.RequestType != MRSRequestType.MailboxRelocation) || RequestJobStateNode.RequestStateIs(this.StatusDetail, RequestState.Queued));
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600116E RID: 4462 RVA: 0x000285A4 File Offset: 0x000267A4
		internal bool ShouldClearRehomeRequest
		{
			get
			{
				return this.RehomeRequest && (this.RequestQueue == null || this.RequestQueue.Equals(this.OptimalRequestQueue) || ((this.RequestType == MRSRequestType.Move || this.RequestType == MRSRequestType.MailboxRelocation) && !RequestJobStateNode.RequestStateIs(this.StatusDetail, RequestState.Queued)));
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x000285FC File Offset: 0x000267FC
		internal DateTime? NextPickupTime
		{
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				DateTime dateTime = DateTime.MaxValue;
				DateTime? timestamp = new DateTime?(this.RequestCanceledTimestamp + ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("CanceledRequestAge"));
				if (timestamp < dateTime && timestamp > utcNow)
				{
					dateTime = timestamp.Value;
				}
				timestamp = new DateTime?(this.ServerBusyBackoffUntilTimestamp);
				if (timestamp < dateTime && timestamp > utcNow)
				{
					dateTime = timestamp.Value;
				}
				timestamp = this.TimeTracker.GetTimestamp(RequestJobTimestamp.DoNotPickUntil);
				if (timestamp != null && timestamp.Value < dateTime && timestamp.Value > utcNow)
				{
					dateTime = timestamp.Value;
				}
				if (!(dateTime == DateTime.MaxValue))
				{
					return new DateTime?(dateTime);
				}
				return null;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0002872F File Offset: 0x0002692F
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x00028741 File Offset: 0x00026941
		internal int SourceArchiveVersion
		{
			get
			{
				return (int)this[RequestJobSchema.SourceArchiveVersion];
			}
			set
			{
				this[RequestJobSchema.SourceArchiveVersion] = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00028754 File Offset: 0x00026954
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x00028766 File Offset: 0x00026966
		internal ADObjectId SourceArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.SourceArchiveDatabase];
			}
			set
			{
				this[RequestJobSchema.SourceArchiveDatabase] = value;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00028774 File Offset: 0x00026974
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x00028786 File Offset: 0x00026986
		internal string SourceArchiveServer
		{
			get
			{
				return (string)this[RequestJobSchema.SourceArchiveServer];
			}
			set
			{
				this[RequestJobSchema.SourceArchiveServer] = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00028794 File Offset: 0x00026994
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x000287A6 File Offset: 0x000269A6
		internal int TargetArchiveVersion
		{
			get
			{
				return (int)this[RequestJobSchema.TargetArchiveVersion];
			}
			set
			{
				this[RequestJobSchema.TargetArchiveVersion] = value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x000287B9 File Offset: 0x000269B9
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x000287CB File Offset: 0x000269CB
		internal ADObjectId TargetArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.TargetArchiveDatabase];
			}
			set
			{
				this[RequestJobSchema.TargetArchiveDatabase] = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x000287D9 File Offset: 0x000269D9
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x000287EB File Offset: 0x000269EB
		internal string TargetArchiveServer
		{
			get
			{
				return (string)this[RequestJobSchema.TargetArchiveServer];
			}
			set
			{
				this[RequestJobSchema.TargetArchiveServer] = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x000287F9 File Offset: 0x000269F9
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x0002880B File Offset: 0x00026A0B
		internal string ArchiveDomain
		{
			get
			{
				return (string)this[RequestJobSchema.ArchiveDomain];
			}
			set
			{
				this[RequestJobSchema.ArchiveDomain] = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00028819 File Offset: 0x00026A19
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x0002882B File Offset: 0x00026A2B
		internal string RemoteHostName
		{
			get
			{
				return (string)this[RequestJobSchema.RemoteHostName];
			}
			set
			{
				this[RequestJobSchema.RemoteHostName] = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00028839 File Offset: 0x00026A39
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x0002884B File Offset: 0x00026A4B
		internal int RemoteHostPort
		{
			get
			{
				return (int)this[RequestJobSchema.RemoteHostPort];
			}
			set
			{
				this[RequestJobSchema.RemoteHostPort] = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0002885E File Offset: 0x00026A5E
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x00028870 File Offset: 0x00026A70
		internal string SmtpServerName
		{
			get
			{
				return (string)this[RequestJobSchema.SmtpServerName];
			}
			set
			{
				this[RequestJobSchema.SmtpServerName] = value;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0002887E File Offset: 0x00026A7E
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x00028890 File Offset: 0x00026A90
		internal int SmtpServerPort
		{
			get
			{
				return (int)this[RequestJobSchema.SmtpServerPort];
			}
			set
			{
				this[RequestJobSchema.SmtpServerPort] = value;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x000288A3 File Offset: 0x00026AA3
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x000288B5 File Offset: 0x00026AB5
		internal IMAPSecurityMechanism SecurityMechanism
		{
			get
			{
				return (IMAPSecurityMechanism)this[RequestJobSchema.SecurityMechanism];
			}
			set
			{
				this[RequestJobSchema.SecurityMechanism] = value;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x000288C8 File Offset: 0x00026AC8
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x000288DA File Offset: 0x00026ADA
		internal SyncProtocol SyncProtocol
		{
			get
			{
				return (SyncProtocol)this[RequestJobSchema.SyncProtocol];
			}
			set
			{
				this[RequestJobSchema.SyncProtocol] = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x000288ED File Offset: 0x00026AED
		// (set) Token: 0x0600118B RID: 4491 RVA: 0x000288FF File Offset: 0x00026AFF
		internal SmtpAddress EmailAddress
		{
			get
			{
				return (SmtpAddress)this[RequestJobSchema.EmailAddress];
			}
			set
			{
				this[RequestJobSchema.EmailAddress] = value;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00028912 File Offset: 0x00026B12
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x00028924 File Offset: 0x00026B24
		internal TimeSpan IncrementalSyncInterval
		{
			get
			{
				return (TimeSpan)this[RequestJobSchema.IncrementalSyncInterval];
			}
			set
			{
				this[RequestJobSchema.IncrementalSyncInterval] = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00028938 File Offset: 0x00026B38
		internal string RemoteGlobalCatalog
		{
			get
			{
				if (this.RequestStyle != RequestStyle.CrossOrg)
				{
					return null;
				}
				if (this.Flags.HasFlag(RequestFlags.RemoteLegacy))
				{
					if (this.Direction == RequestDirection.Push)
					{
						return this.TargetDCName;
					}
					return this.SourceDCName;
				}
				else
				{
					if (!string.IsNullOrEmpty(this.RemoteDomainControllerToUpdate))
					{
						return this.RemoteDomainControllerToUpdate;
					}
					return null;
				}
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00028995 File Offset: 0x00026B95
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x000289A7 File Offset: 0x00026BA7
		internal string BatchName
		{
			get
			{
				return (string)this[RequestJobSchema.BatchName];
			}
			set
			{
				this[RequestJobSchema.BatchName] = value;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x000289B5 File Offset: 0x00026BB5
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x000289C7 File Offset: 0x00026BC7
		internal string RemoteDatabaseName
		{
			get
			{
				return (string)this[RequestJobSchema.RemoteDatabaseName];
			}
			set
			{
				this[RequestJobSchema.RemoteDatabaseName] = value;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x000289D5 File Offset: 0x00026BD5
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x000289E7 File Offset: 0x00026BE7
		internal Guid? RemoteDatabaseGuid
		{
			get
			{
				return (Guid?)this[RequestJobSchema.RemoteDatabaseGuid];
			}
			set
			{
				this[RequestJobSchema.RemoteDatabaseGuid] = value;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x000289FA File Offset: 0x00026BFA
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x00028A0C File Offset: 0x00026C0C
		internal string RemoteArchiveDatabaseName
		{
			get
			{
				return (string)this[RequestJobSchema.RemoteArchiveDatabaseName];
			}
			set
			{
				this[RequestJobSchema.RemoteArchiveDatabaseName] = value;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00028A1A File Offset: 0x00026C1A
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x00028A2C File Offset: 0x00026C2C
		internal Guid? RemoteArchiveDatabaseGuid
		{
			get
			{
				return (Guid?)this[RequestJobSchema.RemoteArchiveDatabaseGuid];
			}
			set
			{
				this[RequestJobSchema.RemoteArchiveDatabaseGuid] = value;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00028A3F File Offset: 0x00026C3F
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x00028A51 File Offset: 0x00026C51
		internal Unlimited<int> BadItemLimit
		{
			get
			{
				return (Unlimited<int>)this[RequestJobSchema.BadItemLimit];
			}
			set
			{
				this[RequestJobSchema.BadItemLimit] = value;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00028A64 File Offset: 0x00026C64
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x00028A76 File Offset: 0x00026C76
		internal int BadItemsEncountered
		{
			get
			{
				return (int)this[RequestJobSchema.BadItemsEncountered];
			}
			set
			{
				this[RequestJobSchema.BadItemsEncountered] = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00028A89 File Offset: 0x00026C89
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x00028A9B File Offset: 0x00026C9B
		internal Unlimited<int> LargeItemLimit
		{
			get
			{
				return (Unlimited<int>)this[RequestJobSchema.LargeItemLimit];
			}
			set
			{
				this[RequestJobSchema.LargeItemLimit] = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x00028AAE File Offset: 0x00026CAE
		// (set) Token: 0x060011A0 RID: 4512 RVA: 0x00028AC0 File Offset: 0x00026CC0
		internal int LargeItemsEncountered
		{
			get
			{
				return (int)this[RequestJobSchema.LargeItemsEncountered];
			}
			set
			{
				this[RequestJobSchema.LargeItemsEncountered] = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060011A1 RID: 4513 RVA: 0x00028AD3 File Offset: 0x00026CD3
		// (set) Token: 0x060011A2 RID: 4514 RVA: 0x00028AE5 File Offset: 0x00026CE5
		internal bool AllowLargeItems
		{
			get
			{
				return (bool)this[RequestJobSchema.AllowLargeItems];
			}
			set
			{
				this[RequestJobSchema.AllowLargeItems] = value;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x00028AF8 File Offset: 0x00026CF8
		// (set) Token: 0x060011A4 RID: 4516 RVA: 0x00028B0A File Offset: 0x00026D0A
		internal int MissingItemsEncountered
		{
			get
			{
				return (int)this[RequestJobSchema.MissingItemsEncountered];
			}
			set
			{
				this[RequestJobSchema.MissingItemsEncountered] = value;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060011A5 RID: 4517 RVA: 0x00028B1D File Offset: 0x00026D1D
		// (set) Token: 0x060011A6 RID: 4518 RVA: 0x00028B2F File Offset: 0x00026D2F
		internal string MRSServerName
		{
			get
			{
				return (string)this[RequestJobSchema.MRSServerName];
			}
			set
			{
				this[RequestJobSchema.MRSServerName] = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00028B3D File Offset: 0x00026D3D
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x00028B4F File Offset: 0x00026D4F
		internal ulong TotalMailboxSize
		{
			get
			{
				return (ulong)this[RequestJobSchema.TotalMailboxSize];
			}
			set
			{
				this[RequestJobSchema.TotalMailboxSize] = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00028B62 File Offset: 0x00026D62
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00028B74 File Offset: 0x00026D74
		internal ulong TotalMailboxItemCount
		{
			get
			{
				return (ulong)this[RequestJobSchema.TotalMailboxItemCount];
			}
			set
			{
				this[RequestJobSchema.TotalMailboxItemCount] = value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00028B87 File Offset: 0x00026D87
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x00028B99 File Offset: 0x00026D99
		internal ulong? TotalArchiveSize
		{
			get
			{
				return (ulong?)this[RequestJobSchema.TotalArchiveSize];
			}
			set
			{
				this[RequestJobSchema.TotalArchiveSize] = value;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00028BAC File Offset: 0x00026DAC
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x00028BBE File Offset: 0x00026DBE
		internal ulong? TotalArchiveItemCount
		{
			get
			{
				return (ulong?)this[RequestJobSchema.TotalArchiveItemCount];
			}
			set
			{
				this[RequestJobSchema.TotalArchiveItemCount] = value;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00028BD1 File Offset: 0x00026DD1
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00028BE3 File Offset: 0x00026DE3
		internal int PercentComplete
		{
			get
			{
				return (int)this[RequestJobSchema.PercentComplete];
			}
			set
			{
				this[RequestJobSchema.PercentComplete] = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00028BF6 File Offset: 0x00026DF6
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x00028C08 File Offset: 0x00026E08
		internal int? FailureCode
		{
			get
			{
				return (int?)this[RequestJobSchema.FailureCode];
			}
			set
			{
				this[RequestJobSchema.FailureCode] = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00028C1B File Offset: 0x00026E1B
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x00028C2D File Offset: 0x00026E2D
		internal string FailureType
		{
			get
			{
				return (string)this[RequestJobSchema.FailureType];
			}
			set
			{
				this[RequestJobSchema.FailureType] = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00028C3B File Offset: 0x00026E3B
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x00028C4D File Offset: 0x00026E4D
		internal ExceptionSide? FailureSide
		{
			get
			{
				return (ExceptionSide?)this[RequestJobSchema.FailureSide];
			}
			set
			{
				this[RequestJobSchema.FailureSide] = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00028C60 File Offset: 0x00026E60
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x00028C72 File Offset: 0x00026E72
		internal LocalizedString Message
		{
			get
			{
				return (LocalizedString)this[RequestJobSchema.Message];
			}
			set
			{
				this[RequestJobSchema.Message] = value;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00028C85 File Offset: 0x00026E85
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x00028C97 File Offset: 0x00026E97
		internal string RemoteOrgName
		{
			get
			{
				return (string)this[RequestJobSchema.RemoteOrgName];
			}
			set
			{
				this[RequestJobSchema.RemoteOrgName] = value;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00028CA5 File Offset: 0x00026EA5
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x00028CB0 File Offset: 0x00026EB0
		internal NetworkCredential RemoteCredential
		{
			get
			{
				return this.remoteCredential;
			}
			set
			{
				this.remoteCredential = value;
				if (value == null)
				{
					this.RemoteCredentialUsername = null;
					return;
				}
				if (string.IsNullOrEmpty(value.Domain))
				{
					this.RemoteCredentialUsername = value.UserName;
					return;
				}
				this.RemoteCredentialUsername = value.Domain + "\\" + value.UserName;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00028D05 File Offset: 0x00026F05
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x00028D17 File Offset: 0x00026F17
		internal string RemoteCredentialUsername
		{
			get
			{
				return (string)this[RequestJobSchema.RemoteCredentialUsername];
			}
			set
			{
				this[RequestJobSchema.RemoteCredentialUsername] = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00028D28 File Offset: 0x00026F28
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00028DB8 File Offset: 0x00026FB8
		internal string DomainControllerToUpdate
		{
			get
			{
				string text = (string)this[RequestJobSchema.DomainControllerToUpdate];
				if (text == null)
				{
					return text;
				}
				using (this.jobConfigContext.Value.Activate())
				{
					TimeSpan config = ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("DCNameValidityInterval");
					if (config != TimeSpan.Zero && DateTime.UtcNow - this.DomainControllerUpdateTimestamp > config)
					{
						this[RequestJobSchema.DomainControllerToUpdate] = null;
						text = null;
					}
				}
				return text;
			}
			set
			{
				this[RequestJobSchema.DomainControllerToUpdate] = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00028DC6 File Offset: 0x00026FC6
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x00028DD8 File Offset: 0x00026FD8
		internal string RemoteDomainControllerToUpdate
		{
			get
			{
				return (string)this[RequestJobSchema.RemoteDomainControllerToUpdate];
			}
			set
			{
				this[RequestJobSchema.RemoteDomainControllerToUpdate] = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00028DE6 File Offset: 0x00026FE6
		internal string SourceDomainControllerToUpdate
		{
			get
			{
				if (this.Direction != RequestDirection.Pull)
				{
					return this.DomainControllerToUpdate;
				}
				return this.RemoteDomainControllerToUpdate;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00028DFE File Offset: 0x00026FFE
		internal string DestDomainControllerToUpdate
		{
			get
			{
				if (this.Direction != RequestDirection.Pull)
				{
					return this.RemoteDomainControllerToUpdate;
				}
				return this.DomainControllerToUpdate;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00028E16 File Offset: 0x00027016
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00028E28 File Offset: 0x00027028
		internal bool AllowedToFinishMove
		{
			get
			{
				return (bool)this[RequestJobSchema.AllowedToFinishMove];
			}
			set
			{
				this[RequestJobSchema.AllowedToFinishMove] = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00028E3B File Offset: 0x0002703B
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00028E4D File Offset: 0x0002704D
		internal bool PreserveMailboxSignature
		{
			get
			{
				return (bool)this[RequestJobSchema.PreserveMailboxSignature];
			}
			set
			{
				this[RequestJobSchema.PreserveMailboxSignature] = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00028E60 File Offset: 0x00027060
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x00028E72 File Offset: 0x00027072
		internal bool RestartingAfterSignatureChange
		{
			get
			{
				return (bool)this[RequestJobSchema.RestartingAfterSignatureChange];
			}
			set
			{
				this[RequestJobSchema.RestartingAfterSignatureChange] = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00028E85 File Offset: 0x00027085
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x00028E97 File Offset: 0x00027097
		internal int? IsIntegData
		{
			get
			{
				return (int?)this[RequestJobSchema.IsIntegData];
			}
			set
			{
				this[RequestJobSchema.IsIntegData] = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00028EAA File Offset: 0x000270AA
		// (set) Token: 0x060011CE RID: 4558 RVA: 0x00028EBC File Offset: 0x000270BC
		internal long? UserPuid
		{
			get
			{
				return (long?)this[RequestJobSchema.UserPuid];
			}
			set
			{
				this[RequestJobSchema.UserPuid] = value;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00028ECF File Offset: 0x000270CF
		// (set) Token: 0x060011D0 RID: 4560 RVA: 0x00028EE1 File Offset: 0x000270E1
		internal int? OlcDGroup
		{
			get
			{
				return (int?)this[RequestJobSchema.OlcDGroup];
			}
			set
			{
				this[RequestJobSchema.OlcDGroup] = value;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00028EF4 File Offset: 0x000270F4
		// (set) Token: 0x060011D2 RID: 4562 RVA: 0x00028F06 File Offset: 0x00027106
		internal bool CancelRequest
		{
			get
			{
				return (bool)this[RequestJobSchema.CancelRequest];
			}
			set
			{
				this[RequestJobSchema.CancelRequest] = value;
				if (value)
				{
					this.AllowedToFinishMove = false;
				}
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00028F24 File Offset: 0x00027124
		internal DateTime RequestCanceledTimestamp
		{
			get
			{
				DateTime? timestamp = this.TimeTracker.GetTimestamp(RequestJobTimestamp.RequestCanceled);
				if (timestamp == null)
				{
					return DateTime.MinValue;
				}
				return timestamp.GetValueOrDefault();
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00028F58 File Offset: 0x00027158
		internal DateTime LastServerBusyBackoffTimestamp
		{
			get
			{
				DateTime? timestamp = this.TimeTracker.GetTimestamp(RequestJobTimestamp.LastServerBusyBackoff);
				if (timestamp == null)
				{
					return DateTime.MinValue;
				}
				return timestamp.GetValueOrDefault();
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00028F8C File Offset: 0x0002718C
		internal DateTime ServerBusyBackoffUntilTimestamp
		{
			get
			{
				DateTime? timestamp = this.TimeTracker.GetTimestamp(RequestJobTimestamp.ServerBusyBackoffUntil);
				if (timestamp == null)
				{
					return DateTime.MinValue;
				}
				return timestamp.GetValueOrDefault();
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00028FBD File Offset: 0x000271BD
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x00028FCF File Offset: 0x000271CF
		internal JobProcessingState RequestJobState
		{
			get
			{
				return (JobProcessingState)this[RequestJobSchema.RequestJobState];
			}
			set
			{
				this[RequestJobSchema.RequestJobState] = value;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00028FE2 File Offset: 0x000271E2
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x00028FF4 File Offset: 0x000271F4
		internal string UserOrgName
		{
			get
			{
				return (string)this[RequestJobSchema.UserOrgName];
			}
			set
			{
				this[RequestJobSchema.UserOrgName] = value;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x00029004 File Offset: 0x00027204
		internal TimeSpan IdleTime
		{
			get
			{
				DateTime d = this.TimeTracker.GetTimestamp(RequestJobTimestamp.LastUpdate) ?? DateTime.MinValue;
				return DateTime.UtcNow - d;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00029041 File Offset: 0x00027241
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x00029053 File Offset: 0x00027253
		internal SyncStage SyncStage
		{
			get
			{
				return (SyncStage)this[RequestJobSchema.SyncStage];
			}
			set
			{
				this[RequestJobSchema.SyncStage] = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00029066 File Offset: 0x00027266
		// (set) Token: 0x060011DE RID: 4574 RVA: 0x00029078 File Offset: 0x00027278
		internal string SourceDCName
		{
			get
			{
				return (string)this[RequestJobSchema.SourceDCName];
			}
			set
			{
				this[RequestJobSchema.SourceDCName] = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00029086 File Offset: 0x00027286
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x0002908E File Offset: 0x0002728E
		internal NetworkCredential SourceCredential
		{
			get
			{
				return this.sourceCredential;
			}
			set
			{
				this.sourceCredential = value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00029097 File Offset: 0x00027297
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x000290A9 File Offset: 0x000272A9
		internal string TargetDCName
		{
			get
			{
				return (string)this[RequestJobSchema.TargetDCName];
			}
			set
			{
				this[RequestJobSchema.TargetDCName] = value;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x000290B7 File Offset: 0x000272B7
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x000290BF File Offset: 0x000272BF
		internal NetworkCredential TargetCredential
		{
			get
			{
				return this.targetCredential;
			}
			set
			{
				this.targetCredential = value;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x000290C8 File Offset: 0x000272C8
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x000290DA File Offset: 0x000272DA
		internal int RetryCount
		{
			get
			{
				return (int)this[RequestJobSchema.RetryCount];
			}
			set
			{
				this[RequestJobSchema.RetryCount] = value;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x000290ED File Offset: 0x000272ED
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x000290FF File Offset: 0x000272FF
		internal int TotalRetryCount
		{
			get
			{
				return (int)this[RequestJobSchema.TotalRetryCount];
			}
			set
			{
				this[RequestJobSchema.TotalRetryCount] = value;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00029112 File Offset: 0x00027312
		// (set) Token: 0x060011EA RID: 4586 RVA: 0x00029124 File Offset: 0x00027324
		internal string TargetDeliveryDomain
		{
			get
			{
				return (string)this[RequestJobSchema.TargetDeliveryDomain];
			}
			set
			{
				this[RequestJobSchema.TargetDeliveryDomain] = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00029132 File Offset: 0x00027332
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x00029144 File Offset: 0x00027344
		internal bool IgnoreRuleLimitErrors
		{
			get
			{
				return (bool)this[RequestJobSchema.IgnoreRuleLimitErrors];
			}
			set
			{
				this[RequestJobSchema.IgnoreRuleLimitErrors] = value;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00029157 File Offset: 0x00027357
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x00029169 File Offset: 0x00027369
		internal MRSJobType JobType
		{
			get
			{
				return (MRSJobType)this[RequestJobSchema.JobType];
			}
			set
			{
				this[RequestJobSchema.JobType] = value;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0002917C File Offset: 0x0002737C
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x0002918E File Offset: 0x0002738E
		internal string Name
		{
			get
			{
				return (string)this[RequestJobSchema.Name];
			}
			set
			{
				this[RequestJobSchema.Name] = value;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0002919C File Offset: 0x0002739C
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x000291AE File Offset: 0x000273AE
		internal MRSRequestType RequestType
		{
			get
			{
				return (MRSRequestType)this[RequestJobSchema.RequestType];
			}
			set
			{
				this[RequestJobSchema.RequestType] = value;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000291C1 File Offset: 0x000273C1
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x000291D3 File Offset: 0x000273D3
		internal string FilePath
		{
			get
			{
				return (string)this[RequestJobSchema.FilePath];
			}
			set
			{
				this[RequestJobSchema.FilePath] = value;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x000291E1 File Offset: 0x000273E1
		// (set) Token: 0x060011F6 RID: 4598 RVA: 0x000291F3 File Offset: 0x000273F3
		internal MailboxRestoreType? MailboxRestoreFlags
		{
			get
			{
				return (MailboxRestoreType?)this[RequestJobSchema.MailboxRestoreFlags];
			}
			set
			{
				this[RequestJobSchema.MailboxRestoreFlags] = value;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00029206 File Offset: 0x00027406
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00029218 File Offset: 0x00027418
		internal ADObjectId TargetUserId
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.TargetUserId];
			}
			set
			{
				this[RequestJobSchema.TargetUserId] = value;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00029226 File Offset: 0x00027426
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x00029238 File Offset: 0x00027438
		internal ADObjectId SourceUserId
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.SourceUserId];
			}
			set
			{
				this[RequestJobSchema.SourceUserId] = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00029246 File Offset: 0x00027446
		// (set) Token: 0x060011FC RID: 4604 RVA: 0x00029258 File Offset: 0x00027458
		internal string RemoteMailboxLegacyDN
		{
			get
			{
				return (string)this[RequestJobSchema.RemoteMailboxLegacyDN];
			}
			set
			{
				this[RequestJobSchema.RemoteMailboxLegacyDN] = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00029266 File Offset: 0x00027466
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x00029278 File Offset: 0x00027478
		internal string RemoteUserLegacyDN
		{
			get
			{
				return (string)this[RequestJobSchema.RemoteUserLegacyDN];
			}
			set
			{
				this[RequestJobSchema.RemoteUserLegacyDN] = value;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00029286 File Offset: 0x00027486
		// (set) Token: 0x06001200 RID: 4608 RVA: 0x00029298 File Offset: 0x00027498
		internal string RemoteMailboxServerLegacyDN
		{
			get
			{
				return (string)this[RequestJobSchema.RemoteMailboxServerLegacyDN];
			}
			set
			{
				this[RequestJobSchema.RemoteMailboxServerLegacyDN] = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000292A6 File Offset: 0x000274A6
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x000292B8 File Offset: 0x000274B8
		internal string OutlookAnywhereHostName
		{
			get
			{
				return (string)this[RequestJobSchema.OutlookAnywhereHostName];
			}
			set
			{
				this[RequestJobSchema.OutlookAnywhereHostName] = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x000292C6 File Offset: 0x000274C6
		// (set) Token: 0x06001204 RID: 4612 RVA: 0x000292D8 File Offset: 0x000274D8
		internal AuthenticationMethod? AuthenticationMethod
		{
			get
			{
				return (AuthenticationMethod?)this[RequestJobSchema.AuthMethod];
			}
			set
			{
				this[RequestJobSchema.AuthMethod] = value;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x000292EB File Offset: 0x000274EB
		// (set) Token: 0x06001206 RID: 4614 RVA: 0x000292FD File Offset: 0x000274FD
		internal bool? IsAdministrativeCredential
		{
			get
			{
				return (bool?)this[RequestJobSchema.IsAdministrativeCredential];
			}
			set
			{
				this[RequestJobSchema.IsAdministrativeCredential] = value;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x00029310 File Offset: 0x00027510
		// (set) Token: 0x06001208 RID: 4616 RVA: 0x00029322 File Offset: 0x00027522
		internal ConflictResolutionOption? ConflictResolutionOption
		{
			get
			{
				return (ConflictResolutionOption?)this[RequestJobSchema.ConflictResolutionOption];
			}
			set
			{
				this[RequestJobSchema.ConflictResolutionOption] = value;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00029335 File Offset: 0x00027535
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x00029347 File Offset: 0x00027547
		internal FAICopyOption? AssociatedMessagesCopyOption
		{
			get
			{
				return (FAICopyOption?)this[RequestJobSchema.AssociatedMessagesCopyOption];
			}
			set
			{
				this[RequestJobSchema.AssociatedMessagesCopyOption] = value;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0002935A File Offset: 0x0002755A
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x0002936C File Offset: 0x0002756C
		internal ADObjectId OrganizationalUnitRoot
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.OrganizationalUnitRoot];
			}
			set
			{
				this[RequestJobSchema.OrganizationalUnitRoot] = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0002937A File Offset: 0x0002757A
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x0002938C File Offset: 0x0002758C
		internal ADObjectId ConfigurationUnit
		{
			get
			{
				return (ADObjectId)this[RequestJobSchema.ConfigurationUnit];
			}
			set
			{
				this[RequestJobSchema.ConfigurationUnit] = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0002939A File Offset: 0x0002759A
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x000293A2 File Offset: 0x000275A2
		internal Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x000293AB File Offset: 0x000275AB
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x000293B3 File Offset: 0x000275B3
		internal TenantPartitionHint PartitionHint { get; set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000293BC File Offset: 0x000275BC
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x000293CE File Offset: 0x000275CE
		internal OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)this[RequestJobSchema.OrganizationId];
			}
			set
			{
				this[RequestJobSchema.OrganizationId] = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x000293DC File Offset: 0x000275DC
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x000293EE File Offset: 0x000275EE
		internal string ContentFilter
		{
			get
			{
				return (string)this[RequestJobSchema.ContentFilter];
			}
			set
			{
				this[RequestJobSchema.ContentFilter] = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x000293FC File Offset: 0x000275FC
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x0002940E File Offset: 0x0002760E
		internal int ContentFilterLCID
		{
			get
			{
				return (int)this[RequestJobSchema.ContentFilterLCID];
			}
			set
			{
				this[RequestJobSchema.ContentFilterLCID] = value;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00029421 File Offset: 0x00027621
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x00029433 File Offset: 0x00027633
		internal RequestPriority Priority
		{
			get
			{
				return (RequestPriority)this[RequestJobSchema.Priority];
			}
			set
			{
				this[RequestJobSchema.Priority] = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00029446 File Offset: 0x00027646
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x00029458 File Offset: 0x00027658
		internal RequestWorkloadType WorkloadType
		{
			get
			{
				return (RequestWorkloadType)this[RequestJobSchema.WorkloadType];
			}
			set
			{
				this[RequestJobSchema.WorkloadType] = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0002946B File Offset: 0x0002766B
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0002947D File Offset: 0x0002767D
		internal RequestJobInternalFlags RequestJobInternalFlags
		{
			get
			{
				return (RequestJobInternalFlags)this[RequestJobSchema.JobInternalFlags];
			}
			set
			{
				this[RequestJobSchema.JobInternalFlags] = value;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00029490 File Offset: 0x00027690
		// (set) Token: 0x06001220 RID: 4640 RVA: 0x000294A2 File Offset: 0x000276A2
		internal Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[RequestJobSchema.CompletedRequestAgeLimit];
			}
			set
			{
				this[RequestJobSchema.CompletedRequestAgeLimit] = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x000294B5 File Offset: 0x000276B5
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x000294C7 File Offset: 0x000276C7
		internal string RequestCreator
		{
			get
			{
				return (string)this[RequestJobSchema.RequestCreator];
			}
			set
			{
				this[RequestJobSchema.RequestCreator] = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x000294D5 File Offset: 0x000276D5
		// (set) Token: 0x06001224 RID: 4644 RVA: 0x000294E7 File Offset: 0x000276E7
		internal int PoisonCount
		{
			get
			{
				return (int)this[RequestJobSchema.PoisonCount];
			}
			set
			{
				this[RequestJobSchema.PoisonCount] = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x000294FA File Offset: 0x000276FA
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x0002950C File Offset: 0x0002770C
		internal DateTime? LastPickupTime
		{
			get
			{
				return (DateTime?)this[RequestJobSchema.LastPickupTime];
			}
			set
			{
				this[RequestJobSchema.LastPickupTime] = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x0002951F File Offset: 0x0002771F
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x00029531 File Offset: 0x00027731
		internal int? ContentCodePage
		{
			get
			{
				return (int?)this[RequestJobSchema.ContentCodePage];
			}
			set
			{
				this[RequestJobSchema.ContentCodePage] = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0002955C File Offset: 0x0002775C
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x000295BA File Offset: 0x000277BA
		internal List<FolderToMailboxMapping> FolderToMailboxMap
		{
			get
			{
				if (SuppressingPiiContext.NeedPiiSuppression && this.folderToMailboxMapping != null && this.folderToMailboxMapping.Count > 0)
				{
					return (from map in this.folderToMailboxMapping
					select new FolderToMailboxMapping(SuppressingPiiData.Redact(map.FolderName), map.MailboxGuid)).ToList<FolderToMailboxMapping>();
				}
				return this.folderToMailboxMapping;
			}
			set
			{
				this.folderToMailboxMapping = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x000295C3 File Offset: 0x000277C3
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x000295CB File Offset: 0x000277CB
		internal List<MoveFolderInfo> FolderList
		{
			get
			{
				return this.folderList;
			}
			set
			{
				this.folderList = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x000295D4 File Offset: 0x000277D4
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return RequestJobBase.Schema;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x000295DB File Offset: 0x000277DB
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x000295E2 File Offset: 0x000277E2
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x000295FF File Offset: 0x000277FF
		internal RequestStyle RequestStyle
		{
			get
			{
				if (!this.Flags.HasFlag(RequestFlags.CrossOrg))
				{
					return RequestStyle.IntraOrg;
				}
				return RequestStyle.CrossOrg;
			}
			set
			{
				this.UpdateFlags((value == RequestStyle.CrossOrg) ? RequestFlags.CrossOrg : RequestFlags.IntraOrg, RequestFlags.CrossOrg | RequestFlags.IntraOrg);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00029610 File Offset: 0x00027810
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x0002962D File Offset: 0x0002782D
		internal RequestDirection Direction
		{
			get
			{
				if (!this.Flags.HasFlag(RequestFlags.Push))
				{
					return RequestDirection.Pull;
				}
				return RequestDirection.Push;
			}
			set
			{
				this.UpdateFlags((value == RequestDirection.Push) ? RequestFlags.Push : RequestFlags.Pull, RequestFlags.Push | RequestFlags.Pull);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0002963F File Offset: 0x0002783F
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x00029658 File Offset: 0x00027858
		internal bool IsOffline
		{
			get
			{
				return this.Flags.HasFlag(RequestFlags.Offline);
			}
			set
			{
				this.UpdateFlags(value ? RequestFlags.Offline : RequestFlags.None, RequestFlags.Offline);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0002966A File Offset: 0x0002786A
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x00029683 File Offset: 0x00027883
		internal bool Protect
		{
			get
			{
				return this.Flags.HasFlag(RequestFlags.Protected);
			}
			set
			{
				this.UpdateFlags(value ? RequestFlags.Protected : RequestFlags.None, RequestFlags.Protected);
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00029695 File Offset: 0x00027895
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x000296B1 File Offset: 0x000278B1
		internal bool Suspend
		{
			get
			{
				return this.Flags.HasFlag(RequestFlags.Suspend);
			}
			set
			{
				this.UpdateFlags(value ? RequestFlags.Suspend : RequestFlags.None, RequestFlags.Suspend);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x000296C9 File Offset: 0x000278C9
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x000296E5 File Offset: 0x000278E5
		internal bool SuspendWhenReadyToComplete
		{
			get
			{
				return this.Flags.HasFlag(RequestFlags.SuspendWhenReadyToComplete);
			}
			set
			{
				this.UpdateFlags(value ? RequestFlags.SuspendWhenReadyToComplete : RequestFlags.None, RequestFlags.SuspendWhenReadyToComplete);
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00029700 File Offset: 0x00027900
		internal ADObjectId WorkItemQueueMdb
		{
			get
			{
				if (this.RequestQueue != null)
				{
					return this.RequestQueue;
				}
				if (this.ArchiveOnly)
				{
					if (this.Direction != RequestDirection.Push)
					{
						return this.TargetArchiveDatabase;
					}
					return this.SourceArchiveDatabase;
				}
				else
				{
					if (this.Direction != RequestDirection.Push)
					{
						return this.TargetDatabase;
					}
					return this.SourceDatabase;
				}
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x00029751 File Offset: 0x00027951
		internal Guid IdentifyingGuid
		{
			get
			{
				if (this.RequestType != MRSRequestType.Move)
				{
					return this.RequestGuid;
				}
				return this.ExchangeGuid;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x00029768 File Offset: 0x00027968
		internal string SourceMDBName
		{
			get
			{
				if (this.SourceIsLocal)
				{
					ADObjectId adobjectId = this.ArchiveOnly ? this.SourceArchiveDatabase : this.SourceDatabase;
					if (adobjectId == null)
					{
						return null;
					}
					return adobjectId.ToString();
				}
				else
				{
					if (!this.ArchiveOnly)
					{
						return this.RemoteDatabaseName;
					}
					return this.RemoteArchiveDatabaseName;
				}
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x000297B8 File Offset: 0x000279B8
		internal Guid SourceMDBGuid
		{
			get
			{
				if (!this.PrimaryIsMoving)
				{
					return Guid.Empty;
				}
				if (this.SourceIsLocal)
				{
					if (this.SourceDatabase == null)
					{
						return Guid.Empty;
					}
					return this.SourceDatabase.ObjectGuid;
				}
				else
				{
					Guid? remoteDatabaseGuid = this.RemoteDatabaseGuid;
					if (remoteDatabaseGuid == null)
					{
						return Guid.Empty;
					}
					return remoteDatabaseGuid.GetValueOrDefault();
				}
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00029814 File Offset: 0x00027A14
		internal string TargetMDBName
		{
			get
			{
				if (this.TargetIsLocal)
				{
					ADObjectId adobjectId = this.ArchiveOnly ? this.TargetArchiveDatabase : this.TargetDatabase;
					if (adobjectId == null)
					{
						return null;
					}
					return adobjectId.ToString();
				}
				else
				{
					if (!this.ArchiveOnly)
					{
						return this.RemoteDatabaseName;
					}
					return this.RemoteArchiveDatabaseName;
				}
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00029864 File Offset: 0x00027A64
		internal Guid TargetMDBGuid
		{
			get
			{
				if (!this.PrimaryIsMoving)
				{
					return Guid.Empty;
				}
				if (this.TargetIsLocal)
				{
					if (this.TargetDatabase == null)
					{
						return Guid.Empty;
					}
					return this.TargetDatabase.ObjectGuid;
				}
				else
				{
					Guid? remoteDatabaseGuid = this.RemoteDatabaseGuid;
					if (remoteDatabaseGuid == null)
					{
						return Guid.Empty;
					}
					return remoteDatabaseGuid.GetValueOrDefault();
				}
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x000298C0 File Offset: 0x00027AC0
		internal Guid SourceArchiveMDBGuid
		{
			get
			{
				if (!this.ArchiveIsMoving)
				{
					return Guid.Empty;
				}
				if (this.SourceIsLocal)
				{
					ADObjectId adobjectId = this.SourceArchiveDatabase ?? this.SourceDatabase;
					if (adobjectId == null)
					{
						return Guid.Empty;
					}
					return adobjectId.ObjectGuid;
				}
				else
				{
					Guid? remoteArchiveDatabaseGuid = this.RemoteArchiveDatabaseGuid;
					if (remoteArchiveDatabaseGuid == null)
					{
						return Guid.Empty;
					}
					return remoteArchiveDatabaseGuid.GetValueOrDefault();
				}
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00029924 File Offset: 0x00027B24
		internal Guid TargetArchiveMDBGuid
		{
			get
			{
				if (!this.ArchiveIsMoving)
				{
					return Guid.Empty;
				}
				if (this.TargetIsLocal)
				{
					ADObjectId adobjectId = this.TargetArchiveDatabase ?? this.TargetDatabase;
					if (adobjectId == null)
					{
						return Guid.Empty;
					}
					return adobjectId.ObjectGuid;
				}
				else
				{
					Guid? remoteArchiveDatabaseGuid = this.RemoteArchiveDatabaseGuid;
					if (remoteArchiveDatabaseGuid == null)
					{
						return Guid.Empty;
					}
					return remoteArchiveDatabaseGuid.GetValueOrDefault();
				}
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x00029988 File Offset: 0x00027B88
		internal string WorkItemQueueMdbName
		{
			get
			{
				if (this.RequestQueue != null)
				{
					return this.RequestQueue.ToString();
				}
				if (this.ArchiveOnly)
				{
					if (this.Direction != RequestDirection.Push)
					{
						return this.TargetArchiveDatabase.Name;
					}
					return this.SourceArchiveDatabase.Name;
				}
				else
				{
					if (this.Direction != RequestDirection.Push)
					{
						return this.TargetMDBName;
					}
					return this.SourceMDBName;
				}
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x000299E8 File Offset: 0x00027BE8
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x000299FF File Offset: 0x00027BFF
		internal byte[] MessageId
		{
			get
			{
				if (this.Identity != null)
				{
					return this.Identity.MessageId;
				}
				return null;
			}
			set
			{
				if (this.Identity == null)
				{
					this.Identity = new RequestJobObjectId(this.IdentifyingGuid, this.WorkItemQueueMdb.ObjectGuid, value);
					return;
				}
				this.Identity.MessageId = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00029A34 File Offset: 0x00027C34
		internal DateTime DomainControllerUpdateTimestamp
		{
			get
			{
				DateTime? timestamp = this.TimeTracker.GetTimestamp(RequestJobTimestamp.DomainControllerUpdate);
				if (timestamp == null)
				{
					timestamp = new DateTime?(this.TimeTracker.GetTimestamp(RequestJobTimestamp.Creation) ?? DateTime.UtcNow);
				}
				return timestamp.Value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00029A8A File Offset: 0x00027C8A
		internal bool PrimaryOnly
		{
			get
			{
				return this.Flags.HasFlag(RequestFlags.MoveOnlyPrimaryMailbox);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00029AA6 File Offset: 0x00027CA6
		internal bool ArchiveOnly
		{
			get
			{
				return this.Flags.HasFlag(RequestFlags.MoveOnlyArchiveMailbox);
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00029AC2 File Offset: 0x00027CC2
		internal bool PrimaryIsMoving
		{
			get
			{
				return !this.ArchiveOnly;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00029AD0 File Offset: 0x00027CD0
		internal bool ArchiveIsMoving
		{
			get
			{
				return !this.PrimaryOnly && this.ArchiveGuid != null;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x00029AF5 File Offset: 0x00027CF5
		internal bool SourceIsLocal
		{
			get
			{
				return this.RequestStyle == RequestStyle.IntraOrg || this.Direction == RequestDirection.Push;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00029B0B File Offset: 0x00027D0B
		internal bool TargetIsLocal
		{
			get
			{
				return this.RequestStyle == RequestStyle.IntraOrg || this.Direction == RequestDirection.Pull;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00029B24 File Offset: 0x00027D24
		internal bool IsSplitPrimaryAndArchive
		{
			get
			{
				return this.PrimaryOnly || this.ArchiveOnly || (this.ArchiveGuid != null && (this.SourceMDBGuid != this.SourceArchiveMDBGuid || this.TargetMDBGuid != this.TargetArchiveMDBGuid));
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x00029B80 File Offset: 0x00027D80
		internal ReportVersion ReportVersion
		{
			get
			{
				if (this.JobType < MRSJobType.RequestJobE14R6_CompressedReports)
				{
					return ReportVersion.ReportE14R4;
				}
				return ReportVersion.ReportE14R6Compression;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x00029B8E File Offset: 0x00027D8E
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x00029BA6 File Offset: 0x00027DA6
		internal bool ForceOfflineMove
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.ForceOfflineMove);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.ForceOfflineMove, value);
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00029BB0 File Offset: 0x00027DB0
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x00029BCC File Offset: 0x00027DCC
		internal bool PreventCompletion
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.PreventCompletion);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.PreventCompletion, value);
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00029BDA File Offset: 0x00027DDA
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x00029BF6 File Offset: 0x00027DF6
		internal bool SkipMailboxReleaseCheck
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipMailboxReleaseCheck);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipMailboxReleaseCheck, value);
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00029C04 File Offset: 0x00027E04
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x00029C1C File Offset: 0x00027E1C
		internal bool RestartFromScratch
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.RestartFromScratch);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.RestartFromScratch, value);
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00029C26 File Offset: 0x00027E26
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00029C3E File Offset: 0x00027E3E
		internal bool SkipFolderACLs
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipFolderACLs);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipFolderACLs, value);
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00029C48 File Offset: 0x00027E48
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x00029C61 File Offset: 0x00027E61
		internal bool SkipFolderPromotedProperties
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipFolderPromotedProperties);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipFolderPromotedProperties, value);
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00029C6C File Offset: 0x00027E6C
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x00029C85 File Offset: 0x00027E85
		internal bool SkipFolderRestrictions
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipFolderRestrictions);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipFolderRestrictions, value);
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00029C90 File Offset: 0x00027E90
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x00029CA8 File Offset: 0x00027EA8
		internal bool SkipFolderRules
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipFolderRules);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipFolderRules, value);
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00029CB2 File Offset: 0x00027EB2
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x00029CCB File Offset: 0x00027ECB
		internal bool SkipFolderViews
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipFolderViews);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipFolderViews, value);
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00029CD6 File Offset: 0x00027ED6
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x00029CF2 File Offset: 0x00027EF2
		internal bool SkipInitialConnectionValidation
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipInitialConnectionValidation);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipInitialConnectionValidation, value);
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00029D00 File Offset: 0x00027F00
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x00029D14 File Offset: 0x00027F14
		internal bool SkipContentVerification
		{
			get
			{
				return (this.RequestJobInternalFlags & RequestJobInternalFlags.SkipContentVerification) != RequestJobInternalFlags.None;
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipContentVerification, value);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00029D22 File Offset: 0x00027F22
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x00029D3E File Offset: 0x00027F3E
		internal bool SkipKnownCorruptions
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipKnownCorruptions);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipKnownCorruptions, value);
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00029D4C File Offset: 0x00027F4C
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x00029D68 File Offset: 0x00027F68
		internal bool FailOnCorruptSyncState
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.FailOnCorruptSyncState);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.FailOnCorruptSyncState, value);
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00029D76 File Offset: 0x00027F76
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x00029D92 File Offset: 0x00027F92
		internal bool IncrementallyUpdateGlobalCounterRanges
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.IncrementallyUpdateGlobalCounterRanges);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.IncrementallyUpdateGlobalCounterRanges, value);
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00029DA0 File Offset: 0x00027FA0
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x00029DBC File Offset: 0x00027FBC
		internal bool ExecutedByTransportSync
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.ExecutedByTransportSync);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.ExecutedByTransportSync, value);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00029DCA File Offset: 0x00027FCA
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x00029DDE File Offset: 0x00027FDE
		internal bool BlockFinalization
		{
			get
			{
				return (this.RequestJobInternalFlags & RequestJobInternalFlags.BlockFinalization) != RequestJobInternalFlags.None;
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.BlockFinalization, value);
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x00029DEC File Offset: 0x00027FEC
		// (set) Token: 0x06001270 RID: 4720 RVA: 0x00029E08 File Offset: 0x00028008
		internal bool SkipStorageProviderForSource
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipStorageProviderForSource);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipStorageProviderForSource, value);
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00029E16 File Offset: 0x00028016
		// (set) Token: 0x06001272 RID: 4722 RVA: 0x00029E32 File Offset: 0x00028032
		internal bool FailOnFirstBadItem
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.FailOnFirstBadItem);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.FailOnFirstBadItem, value);
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00029E40 File Offset: 0x00028040
		internal bool IsPublicFolderMailboxRestore
		{
			get
			{
				return this.MailboxRestoreFlags != null && this.MailboxRestoreFlags.Value.HasFlag(MailboxRestoreType.PublicFolderMailbox);
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00029E80 File Offset: 0x00028080
		internal bool IsLivePublicFolderMailboxRestore
		{
			get
			{
				return this.MailboxRestoreFlags != null && this.MailboxRestoreFlags.Value == MailboxRestoreType.PublicFolderMailbox;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x00029EB1 File Offset: 0x000280B1
		// (set) Token: 0x06001276 RID: 4726 RVA: 0x00029ECD File Offset: 0x000280CD
		internal bool SkipPreFinalSyncDataProcessing
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipPreFinalSyncDataProcessing);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipPreFinalSyncDataProcessing, value);
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00029EDB File Offset: 0x000280DB
		// (set) Token: 0x06001278 RID: 4728 RVA: 0x00029EF7 File Offset: 0x000280F7
		internal bool SkipWordBreaking
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipWordBreaking);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipWordBreaking, value);
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00029F05 File Offset: 0x00028105
		// (set) Token: 0x0600127A RID: 4730 RVA: 0x00029F21 File Offset: 0x00028121
		internal bool InvalidateContentIndexAnnotations
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.InvalidateContentIndexAnnotations);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.InvalidateContentIndexAnnotations, value);
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00029F2F File Offset: 0x0002812F
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x00029F4B File Offset: 0x0002814B
		internal bool SkipProvisioningCheck
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipProvisioningCheck);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipProvisioningCheck, value);
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00029F59 File Offset: 0x00028159
		internal bool UseAsyncNotificationAPI
		{
			get
			{
				return this.RequestType == MRSRequestType.MailboxExport || this.RequestType == MRSRequestType.MailboxImport || this.RequestType == MRSRequestType.MailboxRestore;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00029F78 File Offset: 0x00028178
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x00029F94 File Offset: 0x00028194
		internal bool SkipConvertingSourceToMeu
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.SkipConvertingSourceToMeu);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.SkipConvertingSourceToMeu, value);
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00029FA2 File Offset: 0x000281A2
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x00029FBE File Offset: 0x000281BE
		internal bool ResolveServer
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.ResolveServer);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.ResolveServer, value);
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00029FCC File Offset: 0x000281CC
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00029FE8 File Offset: 0x000281E8
		internal bool UseTcp
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.UseTcp);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.UseTcp, value);
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00029FF6 File Offset: 0x000281F6
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x0002A012 File Offset: 0x00028212
		internal bool CrossResourceForest
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.CrossResourceForest);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.CrossResourceForest, value);
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0002A020 File Offset: 0x00028220
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x0002A03C File Offset: 0x0002823C
		internal bool UseCertificateAuthentication
		{
			get
			{
				return this.RequestJobInternalFlags.HasFlag(RequestJobInternalFlags.UseCertificateAuthentication);
			}
			set
			{
				this.UpdateInternalFlags(RequestJobInternalFlags.UseCertificateAuthentication, value);
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0002A04C File Offset: 0x0002824C
		internal AsyncOperationType AsyncOperationType
		{
			get
			{
				switch (this.RequestType)
				{
				case MRSRequestType.MailboxImport:
					return AsyncOperationType.ImportPST;
				case MRSRequestType.MailboxExport:
					return AsyncOperationType.ExportPST;
				case MRSRequestType.MailboxRestore:
					return AsyncOperationType.MailboxRestore;
				default:
					return AsyncOperationType.Unknown;
				}
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0002A080 File Offset: 0x00028280
		internal AsyncOperationStatus AsyncOperationStatus
		{
			get
			{
				RequestStatus status = this.Status;
				switch (status)
				{
				case RequestStatus.None:
				case RequestStatus.Queued:
					return AsyncOperationStatus.Queued;
				case RequestStatus.InProgress:
				case RequestStatus.CompletionInProgress:
					return AsyncOperationStatus.InProgress;
				case RequestStatus.AutoSuspended:
					break;
				case RequestStatus.Synced:
				case (RequestStatus)6:
				case (RequestStatus)7:
				case (RequestStatus)8:
				case (RequestStatus)9:
					return AsyncOperationStatus.Failed;
				case RequestStatus.Completed:
				case RequestStatus.CompletedWithWarning:
					return AsyncOperationStatus.Completed;
				default:
					switch (status)
					{
					case RequestStatus.Suspended:
						break;
					case RequestStatus.Failed:
						return AsyncOperationStatus.Failed;
					default:
						return AsyncOperationStatus.Failed;
					}
					break;
				}
				return AsyncOperationStatus.Suspended;
			}
		}

		// Token: 0x17000632 RID: 1586
		internal override object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				if (this.isRetired)
				{
					MrsTracer.Common.Warning("Reading data from a retired request...", new object[0]);
				}
				return base[propertyDefinition];
			}
			set
			{
				if (this.isRetired)
				{
					MrsTracer.Common.Warning("Modifying retired request...", new object[0]);
				}
				base[propertyDefinition] = value;
			}
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0002A135 File Offset: 0x00028335
		ISettingsContext ISettingsContextProvider.GetSettingsContext()
		{
			return this.jobConfigContext.Value;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0002A142 File Offset: 0x00028342
		public override string ToString()
		{
			if (this.UserId != null)
			{
				return this.UserId.ToString();
			}
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return base.ToString();
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0002A174 File Offset: 0x00028374
		internal static object OrganizationIdGetter(IPropertyBag propertyBag)
		{
			OrganizationId result = OrganizationId.ForestWideOrgId;
			ADObjectId adobjectId = (ADObjectId)propertyBag[RequestJobSchema.OrganizationalUnitRoot];
			ADObjectId adobjectId2 = (ADObjectId)propertyBag[RequestJobSchema.ConfigurationUnit];
			if (adobjectId != null && adobjectId2 != null)
			{
				result = new OrganizationId(adobjectId, adobjectId2);
			}
			return result;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0002A1B8 File Offset: 0x000283B8
		internal static void OrganizationIdSetter(object value, IPropertyBag propertyBag)
		{
			OrganizationId organizationId = value as OrganizationId;
			if (organizationId != null)
			{
				propertyBag[RequestJobSchema.OrganizationalUnitRoot] = organizationId.OrganizationalUnit;
				propertyBag[RequestJobSchema.ConfigurationUnit] = organizationId.ConfigurationUnit;
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0002A1F7 File Offset: 0x000283F7
		internal static T CreateDummyObject<T>() where T : RequestJobBase, new()
		{
			return RequestJobBase.CreateDummyObject<T>(MRSRequestType.Move);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0002A200 File Offset: 0x00028400
		internal static T CreateDummyObject<T>(MRSRequestType type) where T : RequestJobBase, new()
		{
			T t = Activator.CreateInstance<T>();
			t.RequestType = type;
			t.isFake = true;
			return t;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0002A230 File Offset: 0x00028430
		internal static RequestStatus GetVersionAppropriateStatus(RequestStatus status, ExchangeObjectVersion version)
		{
			if (!version.IsOlderThan(ExchangeObjectVersion.Exchange2012))
			{
				return status;
			}
			switch (status)
			{
			case RequestStatus.None:
			case RequestStatus.Queued:
			case RequestStatus.InProgress:
			case RequestStatus.AutoSuspended:
			case RequestStatus.CompletionInProgress:
			case RequestStatus.Completed:
			case RequestStatus.CompletedWithWarning:
				break;
			case RequestStatus.Synced:
				return RequestStatus.InProgress;
			case (RequestStatus)6:
			case (RequestStatus)7:
			case (RequestStatus)8:
			case (RequestStatus)9:
				return status;
			default:
				switch (status)
				{
				case RequestStatus.Suspended:
				case RequestStatus.Failed:
					break;
				default:
					return status;
				}
				break;
			}
			return status;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0002A29C File Offset: 0x0002849C
		internal static RequestFlags GetVersionAppropriateFlags(RequestFlags flags, ExchangeObjectVersion version)
		{
			if (!version.IsOlderThan(ExchangeObjectVersion.Exchange2012))
			{
				return flags;
			}
			return flags;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0002A2B0 File Offset: 0x000284B0
		internal bool ValidateUser(ADUser user, ADObjectId userId)
		{
			string mrUserId = (userId != null) ? userId.ToString() : string.Empty;
			if (user == null)
			{
				this.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Orphaned);
				this.ValidationMessage = MrsStrings.ValidationUserIsNotInAD(mrUserId);
				return false;
			}
			return true;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0002A2EC File Offset: 0x000284EC
		internal bool ValidateMailbox(ADUser user, bool archive)
		{
			string jobUser = (user.Id != null) ? user.Id.ToString() : string.Empty;
			if ((user.RecipientType != RecipientType.UserMailbox && (!archive || user.RecipientType != RecipientType.MailUser)) || (archive && (user.ArchiveGuid == Guid.Empty || user.ArchiveDatabase == null)) || (!archive && (user.ExchangeGuid == Guid.Empty || user.Database == null)))
			{
				this.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMissing);
				this.ValidationMessage = MrsStrings.ValidationUserLacksMailbox(jobUser);
				return false;
			}
			return true;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0002A380 File Offset: 0x00028580
		internal bool ValidateOutlookAnywhereParams()
		{
			if (string.IsNullOrEmpty(this.RemoteMailboxLegacyDN))
			{
				this.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMissing);
				this.ValidationMessage = MrsStrings.ValidationValueIsMissing("RemoteMailboxLegacyDN");
				return false;
			}
			if (string.IsNullOrEmpty(this.RemoteMailboxServerLegacyDN))
			{
				this.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMissing);
				this.ValidationMessage = MrsStrings.ValidationValueIsMissing("RemoteMailboxServerLegacyDN");
				return false;
			}
			if (this.RemoteCredential == null)
			{
				this.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.DataMissing);
				this.ValidationMessage = MrsStrings.ValidationValueIsMissing("RemoteCredential");
				return false;
			}
			return true;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0002A40C File Offset: 0x0002860C
		internal bool ValidateRequestIndexEntries()
		{
			RequestJobBase.ValidationResultEnum validationResultEnum = RequestJobBase.ValidationResultEnum.Orphaned;
			LocalizedString localizedString = MrsStrings.ValidationNoIndexEntryForRequest(this.ToString());
			if (this.IndexEntries != null)
			{
				foreach (IRequestIndexEntry requestIndexEntry in this.IndexEntries)
				{
					if (requestIndexEntry.RequestGuid.Equals(this.RequestGuid))
					{
						if (this.RequestQueue != null && !this.RequestQueue.Equals(requestIndexEntry.StorageMDB))
						{
							if (validationResultEnum == RequestJobBase.ValidationResultEnum.Orphaned)
							{
								localizedString = MrsStrings.ValidationStorageMDBMismatch((requestIndexEntry.StorageMDB == null) ? "(null)" : requestIndexEntry.StorageMDB.ToString(), (this.RequestQueue == null) ? "(null)" : this.RequestQueue.ToString());
							}
						}
						else if (!this.OrganizationId.Equals(requestIndexEntry.OrganizationId))
						{
							validationResultEnum = RequestJobBase.ValidationResultEnum.DataMismatch;
							localizedString = MrsStrings.ValidationOrganizationMismatch(this.OrganizationId.ToString(), (requestIndexEntry.OrganizationId == null) ? "(null)" : requestIndexEntry.OrganizationId.ToString());
						}
						else
						{
							if (this.Flags != requestIndexEntry.Flags)
							{
								if ((this.Flags & RequestJobBase.StaticFlags) != (requestIndexEntry.Flags & RequestJobBase.StaticFlags))
								{
									MrsTracer.Common.Error("Mismatched RequestJob: flags don't match: Index Entry [{0}], Request Job [{1}]", new object[]
									{
										requestIndexEntry.Flags,
										this.Flags
									});
									validationResultEnum = RequestJobBase.ValidationResultEnum.DataMismatch;
									localizedString = MrsStrings.ValidationFlagsMismatch2(this.Flags.ToString(), requestIndexEntry.Flags.ToString());
									continue;
								}
								MrsTracer.Common.Debug("Possibly mismatched RequestJob: flags don't match: Index Entry [{0}], Request Job [{1}]", new object[]
								{
									requestIndexEntry.Flags,
									this.Flags
								});
							}
							if (this.RequestType != requestIndexEntry.Type)
							{
								validationResultEnum = RequestJobBase.ValidationResultEnum.DataMismatch;
								localizedString = MrsStrings.ValidationRequestTypeMismatch(this.RequestType.ToString(), requestIndexEntry.Type.ToString());
							}
							else if ((string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(requestIndexEntry.Name)) || (!string.IsNullOrEmpty(this.Name) && string.IsNullOrEmpty(requestIndexEntry.Name)) || !this.Name.Equals(requestIndexEntry.Name))
							{
								validationResultEnum = RequestJobBase.ValidationResultEnum.DataMismatch;
								localizedString = MrsStrings.ValidationNameMismatch(this.Name, requestIndexEntry.Name);
							}
							else if (this.SourceUserId != null && !this.SourceUserId.Equals(requestIndexEntry.SourceUserId))
							{
								if (requestIndexEntry.SourceUserId == null)
								{
									validationResultEnum = RequestJobBase.ValidationResultEnum.DataMissing;
									localizedString = MrsStrings.ValidationValueIsMissing("SourceUserId");
								}
								else
								{
									validationResultEnum = RequestJobBase.ValidationResultEnum.DataMismatch;
									localizedString = MrsStrings.ValidationSourceUserMismatch((this.SourceUserId == null) ? "(null)" : this.SourceUserId.ToString(), (requestIndexEntry.SourceUserId == null) ? "(null)" : requestIndexEntry.SourceUserId.ToString());
								}
							}
							else
							{
								if (this.TargetUserId == null || this.TargetUserId.Equals(requestIndexEntry.TargetUserId))
								{
									this.ValidationResult = new RequestJobBase.ValidationResultEnum?(RequestJobBase.ValidationResultEnum.Valid);
									this.ValidationMessage = LocalizedString.Empty;
									return true;
								}
								if (requestIndexEntry.TargetUserId == null)
								{
									validationResultEnum = RequestJobBase.ValidationResultEnum.DataMissing;
									localizedString = MrsStrings.ValidationValueIsMissing("TargetUserId");
								}
								else
								{
									validationResultEnum = RequestJobBase.ValidationResultEnum.DataMismatch;
									localizedString = MrsStrings.ValidationTargetUserMismatch((this.TargetUserId == null) ? "(null)" : this.TargetUserId.ToString(), (requestIndexEntry.TargetUserId == null) ? "(null)" : requestIndexEntry.TargetUserId.ToString());
								}
							}
						}
					}
				}
			}
			this.ValidationResult = new RequestJobBase.ValidationResultEnum?(validationResultEnum);
			this.ValidationMessage = localizedString;
			return false;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0002A7C4 File Offset: 0x000289C4
		internal void UpdateAsyncNotification(ReportData report)
		{
			if (!this.UseAsyncNotificationAPI)
			{
				return;
			}
			RequestStatus status = this.Status;
			switch (status)
			{
			case RequestStatus.Completed:
			case RequestStatus.CompletedWithWarning:
				break;
			default:
				if (status != RequestStatus.Failed)
				{
					AsyncOperationNotificationDataProvider.UpdateNotification(this.OrganizationId, this.RequestGuid.ToString(), new AsyncOperationStatus?(this.AsyncOperationStatus), new int?(this.PercentComplete), new LocalizedString?(this.Message), false, null);
					return;
				}
				break;
			}
			List<LocalizedString> list = new List<LocalizedString>();
			foreach (ReportEntry reportEntry in report.Entries)
			{
				list.Add(((ILocalizedString)reportEntry).LocalizedString);
			}
			AsyncOperationNotificationDataProvider.CompleteNotification(this.OrganizationId, this.RequestGuid.ToString(), new LocalizedString?(this.Message), list, this.Status != RequestStatus.Failed, new int?(this.PercentComplete), false);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0002A8D4 File Offset: 0x00028AD4
		internal void CreateAsyncNotification(ADRecipientOrAddress requestCreator, params KeyValuePair<string, LocalizedString>[] extendedAttributes)
		{
			if (!this.UseAsyncNotificationAPI)
			{
				return;
			}
			AsyncOperationNotificationDataProvider.CreateNotification(this.OrganizationId, this.RequestGuid.ToString(), this.AsyncOperationType, new LocalizedString(this.Name), requestCreator, extendedAttributes, false);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0002A920 File Offset: 0x00028B20
		internal void RemoveAsyncNotification()
		{
			if (!this.UseAsyncNotificationAPI)
			{
				return;
			}
			AsyncOperationNotificationDataProvider.RemoveNotification(this.OrganizationId, this.RequestGuid.ToString(), false);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0002A958 File Offset: 0x00028B58
		internal void ValidateRequestJob()
		{
			if (this.RequestType == MRSRequestType.Move)
			{
				MoveRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.Merge)
			{
				MergeRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.MailboxImport)
			{
				MailboxImportRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.MailboxExport)
			{
				MailboxExportRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.MailboxRestore)
			{
				MailboxRestoreRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.PublicFolderMove)
			{
				PublicFolderMoveRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.PublicFolderMigration)
			{
				PublicFolderMigrationRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.PublicFolderMailboxMigration)
			{
				PublicFolderMailboxMigrationRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.Sync)
			{
				SyncRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.MailboxRelocation)
			{
				MailboxRelocationRequestStatistics.ValidateRequestJob(this);
			}
			else if (this.RequestType == MRSRequestType.FolderMove)
			{
				FolderMoveRequestStatistics.ValidateRequestJob(this);
			}
			if (string.IsNullOrEmpty(this.UserOrgName))
			{
				if (this.OrganizationId != null && this.OrganizationId.OrganizationalUnit != null)
				{
					this.UserOrgName = this.OrganizationId.OrganizationalUnit.Name;
					return;
				}
				if (this.User != null)
				{
					this.UserOrgName = this.User.Id.DomainId.ToString();
				}
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0002AA8B File Offset: 0x00028C8B
		internal void Retire()
		{
			this.isRetired = true;
			this.propertyBag = new SimpleProviderPropertyBag();
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0002AAA0 File Offset: 0x00028CA0
		internal bool ShouldProcessJob()
		{
			bool config;
			using (this.jobConfigContext.Value.Activate())
			{
				config = ConfigBase<MRSConfigSchema>.GetConfig<bool>("IsJobPickupEnabled");
			}
			return config;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0002AAE8 File Offset: 0x00028CE8
		internal bool IsSupported()
		{
			MRSRequestType requestType = this.RequestType;
			return requestType != MRSRequestType.Sync || this.IsSupportedSyncJob();
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0002AB08 File Offset: 0x00028D08
		internal ProxyControlFlags GetProxyControlFlags()
		{
			ProxyControlFlags proxyControlFlags = ProxyControlFlags.None;
			if (this.ResolveServer)
			{
				proxyControlFlags |= ProxyControlFlags.ResolveServerName;
			}
			if (this.UseTcp)
			{
				proxyControlFlags |= ProxyControlFlags.UseTcp;
			}
			if (this.UseCertificateAuthentication)
			{
				proxyControlFlags |= ProxyControlFlags.UseCertificateToAuthenticate;
			}
			if (this.SyncProtocol == SyncProtocol.Olc)
			{
				proxyControlFlags |= ProxyControlFlags.Olc;
				proxyControlFlags |= ProxyControlFlags.UseCertificateToAuthenticate;
			}
			return proxyControlFlags;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0002AB60 File Offset: 0x00028D60
		internal void CopyNonSchematizedPropertiesFrom(RequestJobBase requestJob)
		{
			this.User = requestJob.User;
			this.SourceUser = requestJob.SourceUser;
			this.TargetUser = requestJob.TargetUser;
			this.IndexEntries = requestJob.IndexEntries;
			this.ValidationResult = requestJob.ValidationResult;
			this.ValidationMessage = requestJob.ValidationMessage;
			this.OriginatingMDBGuid = requestJob.OriginatingMDBGuid;
			this.ExternalDirectoryOrganizationId = requestJob.ExternalDirectoryOrganizationId;
			this.PartitionHint = requestJob.PartitionHint;
			this.RemoteCredential = requestJob.RemoteCredential;
			this.SourceCredential = requestJob.SourceCredential;
			this.TargetCredential = requestJob.TargetCredential;
			this.IsFake = requestJob.IsFake;
			this.TimeTracker = requestJob.TimeTracker;
			this.ProgressTracker = requestJob.ProgressTracker;
			this.IndexIds = requestJob.IndexIds;
			this.FolderToMailboxMap = requestJob.FolderToMailboxMap;
			this.FolderList = requestJob.FolderList;
			this.SkippedItemCounts = requestJob.SkippedItemCounts;
			this.FailureHistory = requestJob.FailureHistory;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0002AC60 File Offset: 0x00028E60
		internal MailboxReplicationServiceClient CreateMRSClient(IConfigurationSession session, Guid mdbGuid, List<string> unreachableMrsServers)
		{
			string serverNameToConnect = this.GetServerNameToConnect();
			MailboxReplicationServiceClient result;
			if (!string.IsNullOrEmpty(serverNameToConnect))
			{
				result = MailboxReplicationServiceClient.Create(serverNameToConnect);
			}
			else
			{
				result = MailboxReplicationServiceClient.Create(session, this.JobType, mdbGuid, unreachableMrsServers);
			}
			return result;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0002AC97 File Offset: 0x00028E97
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.isFake)
			{
				return;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0002ACA9 File Offset: 0x00028EA9
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			if (this.isFake)
			{
				return;
			}
			base.ValidateWrite(errors);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0002ACBB File Offset: 0x00028EBB
		private bool IsSupportedSyncJob()
		{
			return this.SyncProtocol == SyncProtocol.Imap || this.SyncProtocol == SyncProtocol.Olc || this.SyncProtocol == SyncProtocol.Eas || this.SyncProtocol == SyncProtocol.Pop;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0002ACE3 File Offset: 0x00028EE3
		private void UpdateFlags(RequestFlags setFlag, RequestFlags mask)
		{
			this.Flags = ((this.Flags & ~mask) | setFlag);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0002ACF6 File Offset: 0x00028EF6
		private void UpdateInternalFlags(RequestJobInternalFlags flag, bool value)
		{
			if (value)
			{
				this.RequestJobInternalFlags |= flag;
				return;
			}
			this.RequestJobInternalFlags &= ~flag;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0002AD1C File Offset: 0x00028F1C
		private string GetServerNameToConnect()
		{
			string result = null;
			if (this.RequestJobState == JobProcessingState.InProgress && this.IdleTime < TimeSpan.FromMinutes(60.0) && !string.IsNullOrEmpty(this.MRSServerName))
			{
				result = this.MRSServerName;
			}
			else if (this.WorkItemQueueMdb != null)
			{
				DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(this.WorkItemQueueMdb.ObjectGuid, null, null, FindServerFlags.None);
				if (!string.IsNullOrEmpty(databaseInformation.ServerFqdn))
				{
					result = databaseInformation.ServerFqdn;
				}
			}
			return result;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0002AD98 File Offset: 0x00028F98
		private SettingsContextBase CreateConfigContext()
		{
			return CommonUtils.CreateConfigContext(this.ExchangeGuid, (this.WorkItemQueueMdb == null) ? Guid.Empty : this.WorkItemQueueMdb.ObjectGuid, this.OrganizationId, this.WorkloadType, this.RequestType, this.SyncProtocol);
		}

		// Token: 0x04000992 RID: 2450
		internal static readonly RequestFlags StaticFlags = RequestFlags.CrossOrg | RequestFlags.IntraOrg | RequestFlags.Push | RequestFlags.Pull | RequestFlags.RemoteLegacy;

		// Token: 0x04000993 RID: 2451
		internal static readonly ObjectSchema Schema = ObjectSchema.GetInstance<RequestJobSchema>();

		// Token: 0x04000994 RID: 2452
		private List<RequestIndexId> indexIds;

		// Token: 0x04000995 RID: 2453
		private RequestJobTimeTracker timeTracker;

		// Token: 0x04000996 RID: 2454
		private bool isFake;

		// Token: 0x04000997 RID: 2455
		private bool isRetired;

		// Token: 0x04000998 RID: 2456
		private List<FolderToMailboxMapping> folderToMailboxMapping;

		// Token: 0x04000999 RID: 2457
		private List<MoveFolderInfo> folderList;

		// Token: 0x0400099A RID: 2458
		[NonSerialized]
		private NetworkCredential remoteCredential;

		// Token: 0x0400099B RID: 2459
		[NonSerialized]
		private NetworkCredential sourceCredential;

		// Token: 0x0400099C RID: 2460
		[NonSerialized]
		private NetworkCredential targetCredential;

		// Token: 0x0400099D RID: 2461
		[NonSerialized]
		private ADUser adUser;

		// Token: 0x0400099E RID: 2462
		[NonSerialized]
		private ADUser sourceUser;

		// Token: 0x0400099F RID: 2463
		[NonSerialized]
		private ADUser targetUser;

		// Token: 0x040009A0 RID: 2464
		[NonSerialized]
		private List<IRequestIndexEntry> indexEntries;

		// Token: 0x040009A1 RID: 2465
		private RequestJobBase.ValidationResultEnum? validationResult;

		// Token: 0x040009A2 RID: 2466
		private LocalizedString validationMessage;

		// Token: 0x040009A3 RID: 2467
		private Guid originatingMDBGuid;

		// Token: 0x040009A4 RID: 2468
		[NonSerialized]
		private SkippedItemCounts skippedItemCounts;

		// Token: 0x040009A5 RID: 2469
		[NonSerialized]
		private FailureHistory failureHistory;

		// Token: 0x040009A6 RID: 2470
		[NonSerialized]
		private Lazy<SettingsContextBase> jobConfigContext;

		// Token: 0x020001C7 RID: 455
		internal enum ValidationResultEnum
		{
			// Token: 0x040009AD RID: 2477
			Valid,
			// Token: 0x040009AE RID: 2478
			Orphaned,
			// Token: 0x040009AF RID: 2479
			DataMismatch,
			// Token: 0x040009B0 RID: 2480
			DataMissing
		}
	}
}
