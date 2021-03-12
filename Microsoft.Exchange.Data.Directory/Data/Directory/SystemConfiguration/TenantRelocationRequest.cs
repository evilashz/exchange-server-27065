using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005DA RID: 1498
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class TenantRelocationRequest : ADLegacyVersionableObject
	{
		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x0600450C RID: 17676 RVA: 0x001012BA File Offset: 0x000FF4BA
		internal override ADObjectSchema Schema
		{
			get
			{
				return TenantRelocationRequest.schema;
			}
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x001012C1 File Offset: 0x000FF4C1
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeConfigurationUnit.MostDerivedClass;
			}
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x0600450E RID: 17678 RVA: 0x001012C8 File Offset: 0x000FF4C8
		// (set) Token: 0x0600450F RID: 17679 RVA: 0x001012DA File Offset: 0x000FF4DA
		public bool RelocationInProgress
		{
			get
			{
				return (bool)this[TenantRelocationRequestSchema.RelocationInProgress];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.RelocationInProgress] = value;
			}
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x001012ED File Offset: 0x000FF4ED
		public TenantRelocationStatus RelocationStatus
		{
			get
			{
				return (TenantRelocationStatus)((byte)this[TenantRelocationRequestSchema.RelocationStatus]);
			}
		}

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06004511 RID: 17681 RVA: 0x001012FF File Offset: 0x000FF4FF
		public RelocationStatusDetailsSource RelocationStatusDetailsSource
		{
			get
			{
				return (RelocationStatusDetailsSource)((byte)this[TenantRelocationRequestSchema.RelocationStatusDetailsSource]);
			}
		}

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x00101311 File Offset: 0x000FF511
		// (set) Token: 0x06004513 RID: 17683 RVA: 0x00101323 File Offset: 0x000FF523
		public RelocationStatusDetailsDestination RelocationStatusDetailsDestination
		{
			get
			{
				return (RelocationStatusDetailsDestination)((byte)this[TenantRelocationRequestSchema.RelocationStatusDetailsDestination]);
			}
			private set
			{
				this[TenantRelocationRequestSchema.RelocationStatusDetailsDestination] = value;
			}
		}

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x00101336 File Offset: 0x000FF536
		// (set) Token: 0x06004515 RID: 17685 RVA: 0x00101348 File Offset: 0x000FF548
		internal RelocationStatusDetails RelocationStatusDetailsRaw
		{
			get
			{
				return (RelocationStatusDetails)((byte)this[TenantRelocationRequestSchema.RelocationStatusDetailsRaw]);
			}
			set
			{
				this[TenantRelocationRequestSchema.RelocationStatusDetailsRaw] = value;
			}
		}

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x0010135B File Offset: 0x000FF55B
		// (set) Token: 0x06004517 RID: 17687 RVA: 0x0010136D File Offset: 0x000FF56D
		public bool Suspended
		{
			get
			{
				return (bool)this[TenantRelocationRequestSchema.Suspended];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.Suspended] = value;
			}
		}

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06004518 RID: 17688 RVA: 0x00101380 File Offset: 0x000FF580
		// (set) Token: 0x06004519 RID: 17689 RVA: 0x00101393 File Offset: 0x000FF593
		public RelocationStateRequested RelocationStateRequested
		{
			get
			{
				return (RelocationStateRequested)((int)this[TenantRelocationRequestSchema.RelocationStateRequested]);
			}
			internal set
			{
				this[TenantRelocationRequestSchema.RelocationStateRequested] = (int)value;
			}
		}

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x0600451A RID: 17690 RVA: 0x001013A6 File Offset: 0x000FF5A6
		// (set) Token: 0x0600451B RID: 17691 RVA: 0x001013B9 File Offset: 0x000FF5B9
		public RelocationError RelocationLastError
		{
			get
			{
				return (RelocationError)((int)this[TenantRelocationRequestSchema.RelocationLastError]);
			}
			internal set
			{
				this[TenantRelocationRequestSchema.RelocationLastError] = (int)value;
			}
		}

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x0600451C RID: 17692 RVA: 0x001013CC File Offset: 0x000FF5CC
		// (set) Token: 0x0600451D RID: 17693 RVA: 0x001013DE File Offset: 0x000FF5DE
		public string TargetForest
		{
			get
			{
				return (string)this[TenantRelocationRequestSchema.TargetForest];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.TargetForest] = value;
			}
		}

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x001013EC File Offset: 0x000FF5EC
		// (set) Token: 0x0600451F RID: 17695 RVA: 0x001013FE File Offset: 0x000FF5FE
		public string SourceForest
		{
			get
			{
				return (string)this[TenantRelocationRequestSchema.SourceForest];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.SourceForest] = value;
			}
		}

		// Token: 0x170016D2 RID: 5842
		// (get) Token: 0x06004520 RID: 17696 RVA: 0x0010140C File Offset: 0x000FF60C
		// (set) Token: 0x06004521 RID: 17697 RVA: 0x0010141E File Offset: 0x000FF61E
		internal string RelocationSourceForestRaw
		{
			get
			{
				return (string)this[TenantRelocationRequestSchema.RelocationSourceForestRaw];
			}
			set
			{
				this[TenantRelocationRequestSchema.RelocationSourceForestRaw] = value;
			}
		}

		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06004522 RID: 17698 RVA: 0x0010142C File Offset: 0x000FF62C
		// (set) Token: 0x06004523 RID: 17699 RVA: 0x0010143E File Offset: 0x000FF63E
		public string GLSResolvedForest
		{
			get
			{
				return (string)this[TenantRelocationRequestSchema.GLSResolvedForest];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.GLSResolvedForest] = value;
			}
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x0010144C File Offset: 0x000FF64C
		// (set) Token: 0x06004525 RID: 17701 RVA: 0x0010145E File Offset: 0x000FF65E
		public string SourceForestRIDMaster
		{
			get
			{
				return (string)this[TenantRelocationRequestSchema.SourceForestRIDMaster];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.SourceForestRIDMaster] = value;
			}
		}

		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06004526 RID: 17702 RVA: 0x0010146C File Offset: 0x000FF66C
		// (set) Token: 0x06004527 RID: 17703 RVA: 0x0010147E File Offset: 0x000FF67E
		public string TargetForestRIDMaster
		{
			get
			{
				return (string)this[TenantRelocationRequestSchema.TargetForestRIDMaster];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.TargetForestRIDMaster] = value;
			}
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x0010148C File Offset: 0x000FF68C
		// (set) Token: 0x06004529 RID: 17705 RVA: 0x0010149E File Offset: 0x000FF69E
		public OrganizationId TargetOrganizationId
		{
			get
			{
				return (OrganizationId)this[TenantRelocationRequestSchema.TargetOrganizationId];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.TargetOrganizationId] = value;
			}
		}

		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x001014AC File Offset: 0x000FF6AC
		// (set) Token: 0x0600452B RID: 17707 RVA: 0x001014BE File Offset: 0x000FF6BE
		public string TargetOriginatingServer
		{
			get
			{
				return (string)this[TenantRelocationRequestSchema.TargetOriginatingServer];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.TargetOriginatingServer] = value;
			}
		}

		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x001014CC File Offset: 0x000FF6CC
		// (set) Token: 0x0600452D RID: 17709 RVA: 0x001014DE File Offset: 0x000FF6DE
		public bool AutoCompletionEnabled
		{
			get
			{
				return (bool)this[TenantRelocationRequestSchema.AutoCompletionEnabled];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.AutoCompletionEnabled] = value;
			}
		}

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x001014F1 File Offset: 0x000FF6F1
		// (set) Token: 0x0600452F RID: 17711 RVA: 0x00101503 File Offset: 0x000FF703
		public bool LargeTenantModeEnabled
		{
			get
			{
				return (bool)this[TenantRelocationRequestSchema.LargeTenantModeEnabled];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.LargeTenantModeEnabled] = value;
			}
		}

		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x06004530 RID: 17712 RVA: 0x00101516 File Offset: 0x000FF716
		// (set) Token: 0x06004531 RID: 17713 RVA: 0x00101528 File Offset: 0x000FF728
		public DateTime? LockdownStartTime
		{
			get
			{
				return (DateTime?)this[TenantRelocationRequestSchema.LockdownStartTime];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.LockdownStartTime] = value;
			}
		}

		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x0010153B File Offset: 0x000FF73B
		// (set) Token: 0x06004533 RID: 17715 RVA: 0x0010154D File Offset: 0x000FF74D
		public DateTime? RelocationSyncStartTime
		{
			get
			{
				return (DateTime?)this[TenantRelocationRequestSchema.RelocationSyncStartTime];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.RelocationSyncStartTime] = value;
			}
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x06004534 RID: 17716 RVA: 0x00101560 File Offset: 0x000FF760
		// (set) Token: 0x06004535 RID: 17717 RVA: 0x00101572 File Offset: 0x000FF772
		public DateTime? RetiredStartTime
		{
			get
			{
				return (DateTime?)this[TenantRelocationRequestSchema.RetiredStartTime];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.RetiredStartTime] = value;
			}
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x00101585 File Offset: 0x000FF785
		// (set) Token: 0x06004537 RID: 17719 RVA: 0x00101597 File Offset: 0x000FF797
		public MultiValuedProperty<TransitionCount> TransitionCounter
		{
			get
			{
				return (MultiValuedProperty<TransitionCount>)this[TenantRelocationRequestSchema.TransitionCounter];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.TransitionCounter] = value;
			}
		}

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x001015A5 File Offset: 0x000FF7A5
		// (set) Token: 0x06004539 RID: 17721 RVA: 0x001015B7 File Offset: 0x000FF7B7
		public Schedule SafeLockdownSchedule
		{
			get
			{
				return (Schedule)this[TenantRelocationRequestSchema.SafeLockdownSchedule];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.SafeLockdownSchedule] = value;
			}
		}

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x001015C5 File Offset: 0x000FF7C5
		// (set) Token: 0x0600453B RID: 17723 RVA: 0x001015D7 File Offset: 0x000FF7D7
		internal byte[] TenantSyncCookie
		{
			get
			{
				return (byte[])this[TenantRelocationRequestSchema.TenantSyncCookie];
			}
			set
			{
				this[TenantRelocationRequestSchema.TenantSyncCookie] = value;
			}
		}

		// Token: 0x170016E0 RID: 5856
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x001015E5 File Offset: 0x000FF7E5
		// (set) Token: 0x0600453D RID: 17725 RVA: 0x001015F7 File Offset: 0x000FF7F7
		public DateTime? LastSuccessfulRelocationSyncStart
		{
			get
			{
				return (DateTime?)this[TenantRelocationRequestSchema.LastSuccessfulRelocationSyncStart];
			}
			internal set
			{
				this[TenantRelocationRequestSchema.LastSuccessfulRelocationSyncStart] = value;
			}
		}

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x0600453E RID: 17726 RVA: 0x0010160A File Offset: 0x000FF80A
		// (set) Token: 0x0600453F RID: 17727 RVA: 0x0010161C File Offset: 0x000FF81C
		internal byte[] TenantRelocationCompletionTargetVector
		{
			get
			{
				return (byte[])this[TenantRelocationRequestSchema.TenantRelocationCompletionTargetVector];
			}
			set
			{
				this[TenantRelocationRequestSchema.TenantRelocationCompletionTargetVector] = value;
			}
		}

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x06004540 RID: 17728 RVA: 0x0010162A File Offset: 0x000FF82A
		public new string Name
		{
			get
			{
				return base.OrganizationId.OrganizationalUnit.Name;
			}
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x06004541 RID: 17729 RVA: 0x0010163C File Offset: 0x000FF83C
		public string ExternalDirectoryOrganizationId
		{
			get
			{
				return (string)this[TenantRelocationRequestSchema.ExternalDirectoryOrganizationId];
			}
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x06004542 RID: 17730 RVA: 0x0010164E File Offset: 0x000FF84E
		public string ServicePlan
		{
			get
			{
				return (string)this[TenantRelocationRequestSchema.ServicePlan];
			}
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x06004543 RID: 17731 RVA: 0x00101660 File Offset: 0x000FF860
		public ADObjectId ExchangeUpgradeBucket
		{
			get
			{
				return (ADObjectId)this[TenantRelocationRequestSchema.ExchangeUpgradeBucket];
			}
		}

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x06004544 RID: 17732 RVA: 0x00101672 File Offset: 0x000FF872
		// (set) Token: 0x06004545 RID: 17733 RVA: 0x00101684 File Offset: 0x000FF884
		public OrganizationStatus OrganizationStatus
		{
			get
			{
				return (OrganizationStatus)((int)this[TenantRelocationRequestSchema.OrganizationStatus]);
			}
			internal set
			{
				this[TenantRelocationRequestSchema.OrganizationStatus] = (int)value;
			}
		}

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x06004546 RID: 17734 RVA: 0x00101697 File Offset: 0x000FF897
		// (set) Token: 0x06004547 RID: 17735 RVA: 0x001016A9 File Offset: 0x000FF8A9
		public OrganizationStatus TargetOrganizationStatus
		{
			get
			{
				return (OrganizationStatus)((int)this[TenantRelocationRequestSchema.TargetOrganizationStatus]);
			}
			internal set
			{
				this[TenantRelocationRequestSchema.TargetOrganizationStatus] = value;
			}
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06004548 RID: 17736 RVA: 0x001016BC File Offset: 0x000FF8BC
		public ExchangeObjectVersion AdminDisplayVersion
		{
			get
			{
				return (ExchangeObjectVersion)this[TenantRelocationRequestSchema.AdminDisplayVersion];
			}
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x06004549 RID: 17737 RVA: 0x001016CE File Offset: 0x000FF8CE
		internal new string AdminDisplayName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x001016D4 File Offset: 0x000FF8D4
		static TenantRelocationRequest()
		{
			TenantRelocationRequest.InFlightRelocationRequestsFilter = new AndFilter(new QueryFilter[]
			{
				TenantRelocationRequest.TenantRelocationRequestFilter,
				new ExistsFilter(TenantRelocationRequestSchema.RelocationSyncStartTime)
			});
			TenantRelocationRequest.JustStartedRelocationRequestsFilter = new AndFilter(new QueryFilter[]
			{
				TenantRelocationRequest.TenantRelocationRequestFilter,
				new NotFilter(new ExistsFilter(TenantRelocationRequestSchema.RelocationSyncStartTime))
			});
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x00101814 File Offset: 0x000FFA14
		internal static object GetRelocationStatus(IPropertyBag propertyBag)
		{
			RelocationStatusDetails r = (RelocationStatusDetails)propertyBag[TenantRelocationRequestSchema.RelocationStatusDetailsRaw];
			return TenantRelocationRequest.GetRelocationStatusFromStatusDetails(r);
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x00101840 File Offset: 0x000FFA40
		internal static TenantRelocationStatus GetRelocationStatusFromStatusDetails(RelocationStatusDetails r)
		{
			if (r <= RelocationStatusDetails.SynchronizationFinishedDeltaSync)
			{
				if (r > RelocationStatusDetails.InitializationFinished)
				{
					if (r <= RelocationStatusDetails.SynchronizationFinishedFullSync)
					{
						if (r != RelocationStatusDetails.SynchronizationStartedFullSync && r != RelocationStatusDetails.SynchronizationFinishedFullSync)
						{
							goto IL_9E;
						}
					}
					else if (r != RelocationStatusDetails.SynchronizationStartedDeltaSync && r != RelocationStatusDetails.SynchronizationFinishedDeltaSync)
					{
						goto IL_9E;
					}
					return TenantRelocationStatus.Synchronization;
				}
				if (r == RelocationStatusDetails.NotStarted || r == RelocationStatusDetails.InitializationStarted || r == RelocationStatusDetails.InitializationFinished)
				{
					return TenantRelocationStatus.NotStarted;
				}
			}
			else
			{
				if (r <= RelocationStatusDetails.LockdownSwitchedGLS)
				{
					if (r <= RelocationStatusDetails.LockdownStartedFinalDeltaSync)
					{
						if (r != RelocationStatusDetails.LockdownStarted && r != RelocationStatusDetails.LockdownStartedFinalDeltaSync)
						{
							goto IL_9E;
						}
					}
					else if (r != RelocationStatusDetails.LockdownFinishedFinalDeltaSync && r != RelocationStatusDetails.LockdownSwitchedGLS)
					{
						goto IL_9E;
					}
					return TenantRelocationStatus.Lockdown;
				}
				if (r <= RelocationStatusDetails.RetiredUpdatedTargetForest)
				{
					if (r == RelocationStatusDetails.RetiredUpdatedSourceForest || r == RelocationStatusDetails.RetiredUpdatedTargetForest)
					{
						return TenantRelocationStatus.Retired;
					}
				}
				else
				{
					if (r == RelocationStatusDetails.Arriving)
					{
						return TenantRelocationStatus.Arriving;
					}
					if (r == RelocationStatusDetails.Active)
					{
						return TenantRelocationStatus.Active;
					}
				}
			}
			IL_9E:
			throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("RelocationStatus", new ArgumentOutOfRangeException(r.ToString()).Message), TenantRelocationRequestSchema.RelocationStatus, r));
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x00101920 File Offset: 0x000FFB20
		internal static object GetRelocationStatusDetailsSource(IPropertyBag propertyBag)
		{
			object obj = propertyBag[TenantRelocationRequestSchema.RelocationStatusDetailsRaw];
			if (obj == null)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("RelocationStatusDetailsOnSource", new ArgumentNullException("RelocationStatusDetailsRaw").Message), TenantRelocationRequestSchema.RelocationStatusDetailsSource, propertyBag));
			}
			return (RelocationStatusDetailsSource)obj;
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x00101974 File Offset: 0x000FFB74
		internal static object GetTransitionCounter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[TenantRelocationRequestSchema.TransitionCounterRaw];
			if (obj == null)
			{
				return null;
			}
			MultiValuedProperty<int> multiValuedProperty = (MultiValuedProperty<int>)obj;
			MultiValuedProperty<TransitionCount> multiValuedProperty2 = new MultiValuedProperty<TransitionCount>();
			for (int i = 0; i < multiValuedProperty.Count; i++)
			{
				int num = multiValuedProperty[i];
				TenantRelocationTransition transition = (TenantRelocationTransition)((num & TenantRelocationRequest.TransitionCounterTypeBitmask) >> 16);
				ushort count = (ushort)(num & TenantRelocationRequest.TransitionCounterCountBitmask);
				multiValuedProperty2.Add(new TransitionCount(transition, count));
			}
			multiValuedProperty2.Sort();
			return multiValuedProperty2;
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x001019E8 File Offset: 0x000FFBE8
		internal static void SetTransitionCounter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<int> multiValuedProperty = new MultiValuedProperty<int>();
			foreach (TransitionCount transitionCount in ((MultiValuedProperty<TransitionCount>)value))
			{
				int item = (int)((ushort)((int)transitionCount.Transition << 16) | transitionCount.Count);
				multiValuedProperty.Add(item);
			}
			propertyBag[TenantRelocationRequestSchema.TransitionCounterRaw] = multiValuedProperty;
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x00101A60 File Offset: 0x000FFC60
		internal static QueryFilter GetStaleLockedRelocationRequestsFilter(ExDateTime olderThan, bool excludeSuspended)
		{
			ComparisonFilter comparisonFilter = excludeSuspended ? new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.Suspended, false) : null;
			string propertyValue = olderThan.ToString("yyyyMMddHHmmss'.0Z'", CultureInfo.InvariantCulture);
			return QueryFilter.AndTogether(new QueryFilter[]
			{
				TenantRelocationRequest.LockedRelocationRequestsFilter,
				new ComparisonFilter(ComparisonOperator.LessThanOrEqual, TenantRelocationRequestSchema.LockdownStartTime, propertyValue),
				comparisonFilter
			});
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x00101AC0 File Offset: 0x000FFCC0
		internal static void PopulatePresentationObject(TenantRelocationRequest presentationObject, string targetForestDomainController, out Exception ex)
		{
			TenantRelocationRequest targetForestObject;
			TenantRelocationRequest.LoadTargetForestObject(presentationObject, targetForestDomainController, out targetForestObject, out ex);
			if (ex == null)
			{
				TenantRelocationRequest.PopulatePresentationObject(presentationObject, targetForestObject);
			}
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x00101AE4 File Offset: 0x000FFCE4
		internal static void SetRelocationCompletedOnOU(ITenantConfigurationSession session, OrganizationId organizationId)
		{
			bool useConfigNC = session.UseConfigNC;
			try
			{
				session.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = session.Read<ADOrganizationalUnit>(organizationId.OrganizationalUnit);
				if (adorganizationalUnit == null)
				{
					throw new ArgumentException("Cannot read target tenant OU: " + organizationId.OrganizationalUnit.ToString());
				}
				adorganizationalUnit.RelocationInProgress = true;
				session.Save(adorganizationalUnit);
				adorganizationalUnit.RelocationInProgress = false;
				session.Save(adorganizationalUnit);
			}
			finally
			{
				session.UseConfigNC = useConfigNC;
			}
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x00101B60 File Offset: 0x000FFD60
		private static void LoadTargetForestObject(TenantRelocationRequest presentationObject, string targetForestDomainController, out TenantRelocationRequest targetForestObject, out Exception ex)
		{
			if (presentationObject == null)
			{
				throw new ArgumentNullException("presentationObject");
			}
			if (presentationObject.TargetForest == null)
			{
				throw new ADTransientException(DirectoryStrings.ErrorReplicationLatency);
			}
			TenantRelocationRequest.LoadOtherForestObjectInternal(targetForestDomainController, presentationObject.TargetForest, presentationObject.DistinguishedName, presentationObject.ExternalDirectoryOrganizationId, true, out targetForestObject, out ex);
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x00101BA0 File Offset: 0x000FFDA0
		internal static void LoadOtherForestObjectInternal(string dc, string searchForest, string originalObjectDN, string externalDirectoryOrganizationId, bool needTargetTenant, out TenantRelocationRequest otherForestObject, out Exception ex)
		{
			if (string.IsNullOrEmpty(searchForest))
			{
				throw new ArgumentNullException("searchForest");
			}
			if (string.IsNullOrEmpty(originalObjectDN))
			{
				throw new ArgumentNullException("originalObjectDN");
			}
			if (string.IsNullOrEmpty(externalDirectoryOrganizationId))
			{
				throw new ArgumentNullException("externalDirectoryOrganizationId");
			}
			ex = null;
			otherForestObject = null;
			PartitionId partitionId = new PartitionId(searchForest);
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.NonCacheSessionFactory.CreateTenantConfigurationSession(dc, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAllTenantsPartitionId(partitionId), 1539, "LoadOtherForestObjectInternal", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\TenantRelocationRequest.cs");
			tenantConfigurationSession.SessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
			QueryFilter queryFilter;
			if (needTargetTenant)
			{
				queryFilter = TenantRelocationRequest.TenantRelocationLandingFilter;
			}
			else
			{
				queryFilter = TenantRelocationRequest.TenantRelocationRequestFilter;
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId, externalDirectoryOrganizationId),
				queryFilter
			});
			try
			{
				TenantRelocationRequest[] array = tenantConfigurationSession.Find<TenantRelocationRequest>(null, QueryScope.SubTree, filter, null, 2);
				if (array.Length > 1)
				{
					ex = new ADOperationException(DirectoryStrings.ErrorTargetPartitionHas2TenantsWithSameId(originalObjectDN, searchForest, externalDirectoryOrganizationId));
				}
				else if (array.Length > 0)
				{
					otherForestObject = array[0];
				}
				else
				{
					ex = new CannotFindTargetTenantException(originalObjectDN, searchForest, externalDirectoryOrganizationId);
				}
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x00101CBC File Offset: 0x000FFEBC
		internal static void PopulatePresentationObject(TenantRelocationRequest presentationObject, TenantRelocationRequest targetForestObject)
		{
			if (presentationObject == null)
			{
				throw new ArgumentNullException("presentationObject");
			}
			if (targetForestObject == null)
			{
				throw new ArgumentNullException("targetForestObject");
			}
			presentationObject.RelocationStatusDetailsDestination = (RelocationStatusDetailsDestination)targetForestObject.RelocationStatusDetailsRaw;
			presentationObject.SourceForest = targetForestObject.RelocationSourceForestRaw;
			presentationObject.TargetOriginatingServer = targetForestObject.OriginatingServer;
			presentationObject.TargetOrganizationStatus = targetForestObject.OrganizationStatus;
			if (targetForestObject.OrganizationId != OrganizationId.ForestWideOrgId)
			{
				presentationObject.TargetOrganizationId = targetForestObject.OrganizationId;
			}
			presentationObject.RelocationInProgress = presentationObject.IsRelocationInProgress();
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x00101D3F File Offset: 0x000FFF3F
		internal bool HasPermanentError()
		{
			return this.RelocationLastError > RelocationError.LastTransientError;
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x00101D4C File Offset: 0x000FFF4C
		internal bool IsLockdownAllowed()
		{
			if (TenantRelocationStateCache.IgnoreRelocationTimeConstraints())
			{
				return true;
			}
			DateTime utcNow = DateTime.UtcNow;
			if (this.RelocationSyncStartTime != null && this.RelocationSyncStartTime.Value.ToUniversalTime() + TimeSpan.FromHours((double)TenantRelocationRequest.MinTimeBetweenRelocationStartAndLockdownHours) > utcNow)
			{
				return false;
			}
			if (this.SafeLockdownSchedule == null || this.SafeLockdownSchedule == Schedule.Always)
			{
				return true;
			}
			int num = 0;
			uint num2;
			if (TenantRelocationSyncCoordinator.GetInt32ValueFromRegistryValue("TimeZoneOffset", out num2))
			{
				num = (int)num2;
			}
			return this.SafeLockdownSchedule.Contains(utcNow.AddHours((double)num));
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x00101DE8 File Offset: 0x000FFFE8
		internal bool IsLockdownTimedOut()
		{
			return !this.Suspended && this.InLockdown() && this.LockdownStartTime != null && this.LockdownStartTime.Value.ToUniversalTime() + TimeSpan.FromMinutes((double)TenantRelocationRequest.MaxLockdownTimeMinutes) < DateTime.UtcNow && !TenantRelocationStateCache.IgnoreRelocationTimeConstraints();
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x00101E54 File Offset: 0x00100054
		internal bool IsRetiredSourceHoldTimedOut()
		{
			return TenantRelocationStateCache.IgnoreRelocationTimeConstraints() || (this.RetiredStartTime != null && this.RetiredStartTime.Value.ToUniversalTime() + TimeSpan.FromDays((double)TenantRelocationRequest.WaitTimeBeforeRemoveSourceReplicaDays) < DateTime.UtcNow);
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x00101EAC File Offset: 0x001000AC
		internal bool IsRelocationInProgress()
		{
			return !this.HasPermanentError() && !this.Suspended && RelocationStatusDetailsSource.RetiredUpdatedTargetForest != this.RelocationStatusDetailsSource && (this.AutoCompletionEnabled || this.RelocationStatusDetailsSource < (RelocationStatusDetailsSource)this.RelocationStateRequested);
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x00101EE8 File Offset: 0x001000E8
		internal bool InLockdown()
		{
			RelocationStatusDetailsSource relocationStatusDetailsSource = this.RelocationStatusDetailsSource;
			if (relocationStatusDetailsSource <= RelocationStatusDetailsSource.LockdownStartedFinalDeltaSync)
			{
				if (relocationStatusDetailsSource != RelocationStatusDetailsSource.LockdownStarted && relocationStatusDetailsSource != RelocationStatusDetailsSource.LockdownStartedFinalDeltaSync)
				{
					return false;
				}
			}
			else if (relocationStatusDetailsSource != RelocationStatusDetailsSource.LockdownFinishedFinalDeltaSync && relocationStatusDetailsSource != RelocationStatusDetailsSource.LockdownSwitchedGLS && relocationStatusDetailsSource != RelocationStatusDetailsSource.RetiredUpdatedSourceForest)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x00101F20 File Offset: 0x00100120
		internal bool InLockdownBeforeGLSSwitchState()
		{
			RelocationStatusDetailsSource relocationStatusDetailsSource = this.RelocationStatusDetailsSource;
			return relocationStatusDetailsSource == RelocationStatusDetailsSource.LockdownStarted || relocationStatusDetailsSource == RelocationStatusDetailsSource.LockdownStartedFinalDeltaSync || relocationStatusDetailsSource == RelocationStatusDetailsSource.LockdownFinishedFinalDeltaSync;
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x00101F48 File Offset: 0x00100148
		internal bool InPostGLSSwitchState()
		{
			RelocationStatusDetailsSource relocationStatusDetailsSource = this.RelocationStatusDetailsSource;
			return relocationStatusDetailsSource == RelocationStatusDetailsSource.LockdownSwitchedGLS || relocationStatusDetailsSource == RelocationStatusDetailsSource.RetiredUpdatedSourceForest || relocationStatusDetailsSource == RelocationStatusDetailsSource.RetiredUpdatedTargetForest;
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x00101F6E File Offset: 0x0010016E
		internal bool IsOrchestrated()
		{
			return !this.Suspended && !this.AutoCompletionEnabled && !this.HasPermanentError() && this.ExchangeUpgradeBucket != null && this.ExchangeUpgradeBucket.Name.StartsWith("Relocation");
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x00101FA8 File Offset: 0x001001A8
		internal bool HasTooManyTransitions(out TransitionCount transitionCount)
		{
			transitionCount = null;
			if (this.TransitionCounter != null)
			{
				int config = TenantRelocationConfigImpl.GetConfig<int>("MaxNumberOfTransitions");
				foreach (TransitionCount transitionCount2 in this.TransitionCounter)
				{
					if ((int)transitionCount2.Count > config)
					{
						transitionCount = transitionCount2;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x00102020 File Offset: 0x00100220
		internal void IncrementTransitionCounter(TenantRelocationTransition transition)
		{
			MultiValuedProperty<TransitionCount> multiValuedProperty = new MultiValuedProperty<TransitionCount>();
			bool flag = false;
			foreach (TransitionCount transitionCount in this.TransitionCounter)
			{
				if (transitionCount.Transition == transition)
				{
					TransitionCount transitionCount2 = transitionCount;
					transitionCount2.Count += 1;
					flag = true;
				}
				multiValuedProperty.Add(transitionCount);
			}
			if (!flag)
			{
				multiValuedProperty.Add(new TransitionCount(transition, 1));
			}
			this.TransitionCounter = multiValuedProperty;
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x001020AC File Offset: 0x001002AC
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (!string.IsNullOrEmpty(this.RelocationSourceForestRaw) && !string.IsNullOrEmpty(this.TargetForest))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorBothTargetAndSourceForestPopulated(this.RelocationSourceForestRaw, this.TargetForest), this.Identity, string.Empty));
			}
			if ((!string.IsNullOrEmpty(this.RelocationSourceForestRaw) || !string.IsNullOrEmpty(this.TargetForest)) && this.RelocationStatusDetailsRaw == RelocationStatusDetails.NotStarted)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorTargetOrSourceForestPopulatedStatusNotStarted(this.RelocationSourceForestRaw, this.TargetForest), this.Identity, string.Empty));
			}
			if (this.TransitionCounter != null)
			{
				HashSet<TenantRelocationTransition> hashSet = new HashSet<TenantRelocationTransition>();
				foreach (TransitionCount transitionCount in this.TransitionCounter)
				{
					if (!hashSet.Contains(transitionCount.Transition))
					{
						hashSet.Add(transitionCount.Transition);
					}
					else
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.ErrorTransitionCounterHasDuplicateEntry(transitionCount.Transition.ToString()), this.Identity, string.Empty));
					}
				}
			}
			base.ValidateRead(errors);
		}

		// Token: 0x04002F97 RID: 12183
		internal const string Noun = "TenantRelocationRequest";

		// Token: 0x04002F98 RID: 12184
		internal const string RelocationBucketNamePrefix = "Relocation";

		// Token: 0x04002F99 RID: 12185
		private static readonly int TransitionCounterTypeBitmask = Convert.ToInt32("00000000111111110000000000000000", 2);

		// Token: 0x04002F9A RID: 12186
		private static readonly int TransitionCounterCountBitmask = Convert.ToInt32("00000000000000001111111111111111", 2);

		// Token: 0x04002F9B RID: 12187
		private static readonly TenantRelocationRequestSchema schema = ObjectSchema.GetInstance<TenantRelocationRequestSchema>();

		// Token: 0x04002F9C RID: 12188
		internal static readonly int MinTimeBetweenRelocationStartAndLockdownHours = 24;

		// Token: 0x04002F9D RID: 12189
		internal static readonly int WaitTimeBeforeRemoveSourceReplicaDays = 30;

		// Token: 0x04002F9E RID: 12190
		internal static readonly int MaxLockdownTimeMinutes = 30;

		// Token: 0x04002F9F RID: 12191
		internal static readonly QueryFilter TenantRelocationRequestFilter = new ExistsFilter(TenantRelocationRequestSchema.TargetForest);

		// Token: 0x04002FA0 RID: 12192
		internal static readonly QueryFilter LockedRelocationRequestsFilter = new OrFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.RelocationStatusDetailsRaw, RelocationStatusDetails.LockdownStarted),
			new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.RelocationStatusDetailsRaw, RelocationStatusDetails.LockdownStartedFinalDeltaSync),
			new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.RelocationStatusDetailsRaw, RelocationStatusDetails.LockdownFinishedFinalDeltaSync),
			new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.RelocationStatusDetailsRaw, RelocationStatusDetails.LockdownSwitchedGLS),
			new ComparisonFilter(ComparisonOperator.Equal, TenantRelocationRequestSchema.RelocationStatusDetailsRaw, RelocationStatusDetails.RetiredUpdatedSourceForest)
		});

		// Token: 0x04002FA1 RID: 12193
		internal static readonly QueryFilter InFlightRelocationRequestsFilter;

		// Token: 0x04002FA2 RID: 12194
		internal static readonly QueryFilter JustStartedRelocationRequestsFilter;

		// Token: 0x04002FA3 RID: 12195
		internal static readonly QueryFilter TenantRelocationLandingFilter = new ExistsFilter(TenantRelocationRequestSchema.RelocationSourceForestRaw);
	}
}
