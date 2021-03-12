using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007EB RID: 2027
	[Serializable]
	public class FailedMSOSyncObjectPresentationObject : ConfigurableObject
	{
		// Token: 0x06006455 RID: 25685 RVA: 0x0015C343 File Offset: 0x0015A543
		public FailedMSOSyncObjectPresentationObject(FailedMSOSyncObject divergence) : base(new SimpleProviderPropertyBag())
		{
			if (divergence == null)
			{
				throw new ArgumentNullException("divergence");
			}
			this.propertyBag = divergence.propertyBag;
			this.divergence = divergence;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x0015C371 File Offset: 0x0015A571
		public FailedMSOSyncObjectPresentationObject() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x1700238B RID: 9099
		// (get) Token: 0x06006457 RID: 25687 RVA: 0x0015C37E File Offset: 0x0015A57E
		public SyncObjectId ObjectId
		{
			get
			{
				return (SyncObjectId)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.SyncObjectId];
			}
		}

		// Token: 0x1700238C RID: 9100
		// (get) Token: 0x06006458 RID: 25688 RVA: 0x0015C395 File Offset: 0x0015A595
		public DateTime? DivergenceTimestamp
		{
			get
			{
				return (DateTime?)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.DivergenceTimestamp];
			}
		}

		// Token: 0x1700238D RID: 9101
		// (get) Token: 0x06006459 RID: 25689 RVA: 0x0015C3AC File Offset: 0x0015A5AC
		public int DivergenceCount
		{
			get
			{
				return (int)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.DivergenceCount];
			}
		}

		// Token: 0x1700238E RID: 9102
		// (get) Token: 0x0600645A RID: 25690 RVA: 0x0015C3C3 File Offset: 0x0015A5C3
		public bool IsTemporary
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.IsTemporary];
			}
		}

		// Token: 0x1700238F RID: 9103
		// (get) Token: 0x0600645B RID: 25691 RVA: 0x0015C3DA File Offset: 0x0015A5DA
		public bool IsIncrementalOnly
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.IsIncrementalOnly];
			}
		}

		// Token: 0x17002390 RID: 9104
		// (get) Token: 0x0600645C RID: 25692 RVA: 0x0015C3F1 File Offset: 0x0015A5F1
		public bool IsLinkRelated
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.IsLinkRelated];
			}
		}

		// Token: 0x17002391 RID: 9105
		// (get) Token: 0x0600645D RID: 25693 RVA: 0x0015C408 File Offset: 0x0015A608
		// (set) Token: 0x0600645E RID: 25694 RVA: 0x0015C41F File Offset: 0x0015A61F
		[Parameter]
		public bool IsIgnoredInHaltCondition
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.IsIgnoredInHaltCondition];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.IsIgnoredInHaltCondition] = value;
			}
		}

		// Token: 0x17002392 RID: 9106
		// (get) Token: 0x0600645F RID: 25695 RVA: 0x0015C437 File Offset: 0x0015A637
		public bool IsTenantWideDivergence
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.IsTenantWideDivergence];
			}
		}

		// Token: 0x17002393 RID: 9107
		// (get) Token: 0x06006460 RID: 25696 RVA: 0x0015C44E File Offset: 0x0015A64E
		public bool IsValidationDivergence
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.IsValidationDivergence];
			}
		}

		// Token: 0x17002394 RID: 9108
		// (get) Token: 0x06006461 RID: 25697 RVA: 0x0015C465 File Offset: 0x0015A665
		// (set) Token: 0x06006462 RID: 25698 RVA: 0x0015C47C File Offset: 0x0015A67C
		[Parameter]
		public bool IsRetriable
		{
			get
			{
				return (bool)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.IsRetriable];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.IsRetriable] = value;
			}
		}

		// Token: 0x17002395 RID: 9109
		// (get) Token: 0x06006463 RID: 25699 RVA: 0x0015C494 File Offset: 0x0015A694
		public string BuildNumber
		{
			get
			{
				return this.divergence.BuildNumber;
			}
		}

		// Token: 0x17002396 RID: 9110
		// (get) Token: 0x06006464 RID: 25700 RVA: 0x0015C4A1 File Offset: 0x0015A6A1
		public string TargetBuildNumber
		{
			get
			{
				return this.divergence.TargetBuildNumber;
			}
		}

		// Token: 0x17002397 RID: 9111
		// (get) Token: 0x06006465 RID: 25701 RVA: 0x0015C4AE File Offset: 0x0015A6AE
		public string CmdletName
		{
			get
			{
				return this.divergence.CmdletName;
			}
		}

		// Token: 0x17002398 RID: 9112
		// (get) Token: 0x06006466 RID: 25702 RVA: 0x0015C4BB File Offset: 0x0015A6BB
		public string CmdletParameters
		{
			get
			{
				return this.divergence.CmdletParameters;
			}
		}

		// Token: 0x17002399 RID: 9113
		// (get) Token: 0x06006467 RID: 25703 RVA: 0x0015C4C8 File Offset: 0x0015A6C8
		public string ErrorMessage
		{
			get
			{
				return this.divergence.ErrorMessage;
			}
		}

		// Token: 0x1700239A RID: 9114
		// (get) Token: 0x06006468 RID: 25704 RVA: 0x0015C4D5 File Offset: 0x0015A6D5
		public string ErrorSymbolicName
		{
			get
			{
				return this.divergence.ErrorSymbolicName;
			}
		}

		// Token: 0x1700239B RID: 9115
		// (get) Token: 0x06006469 RID: 25705 RVA: 0x0015C4E2 File Offset: 0x0015A6E2
		public string ErrorStringId
		{
			get
			{
				return this.divergence.ErrorStringId;
			}
		}

		// Token: 0x1700239C RID: 9116
		// (get) Token: 0x0600646A RID: 25706 RVA: 0x0015C4EF File Offset: 0x0015A6EF
		public string ErrorCategory
		{
			get
			{
				return this.divergence.ErrorCategory;
			}
		}

		// Token: 0x1700239D RID: 9117
		// (get) Token: 0x0600646B RID: 25707 RVA: 0x0015C4FC File Offset: 0x0015A6FC
		public string StreamName
		{
			get
			{
				return this.divergence.StreamName;
			}
		}

		// Token: 0x1700239E RID: 9118
		// (get) Token: 0x0600646C RID: 25708 RVA: 0x0015C509 File Offset: 0x0015A709
		public string MinDivergenceRetryDatetime
		{
			get
			{
				return this.divergence.MinDivergenceRetryDatetime;
			}
		}

		// Token: 0x1700239F RID: 9119
		// (get) Token: 0x0600646D RID: 25709 RVA: 0x0015C516 File Offset: 0x0015A716
		public MultiValuedProperty<string> Errors
		{
			get
			{
				return (MultiValuedProperty<string>)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.Errors];
			}
		}

		// Token: 0x170023A0 RID: 9120
		// (get) Token: 0x0600646E RID: 25710 RVA: 0x0015C52D File Offset: 0x0015A72D
		// (set) Token: 0x0600646F RID: 25711 RVA: 0x0015C544 File Offset: 0x0015A744
		[Parameter]
		public string Comment
		{
			get
			{
				return (string)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.Comment];
			}
			set
			{
				this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.Comment] = value;
			}
		}

		// Token: 0x170023A1 RID: 9121
		// (get) Token: 0x06006470 RID: 25712 RVA: 0x0015C557 File Offset: 0x0015A757
		public string ServiceInstanceId
		{
			get
			{
				return this.divergence.ServiceInstanceId;
			}
		}

		// Token: 0x170023A2 RID: 9122
		// (get) Token: 0x06006471 RID: 25713 RVA: 0x0015C564 File Offset: 0x0015A764
		public override ObjectId Identity
		{
			get
			{
				return new CompoundSyncObjectId(this.ObjectId, new ServiceInstanceId(this.ServiceInstanceId));
			}
		}

		// Token: 0x170023A3 RID: 9123
		// (get) Token: 0x06006472 RID: 25714 RVA: 0x0015C57C File Offset: 0x0015A77C
		public string ExternalDirectoryOrganizationId
		{
			get
			{
				return (string)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.ContextId];
			}
		}

		// Token: 0x170023A4 RID: 9124
		// (get) Token: 0x06006473 RID: 25715 RVA: 0x0015C593 File Offset: 0x0015A793
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.ObjectId];
			}
		}

		// Token: 0x170023A5 RID: 9125
		// (get) Token: 0x06006474 RID: 25716 RVA: 0x0015C5AA File Offset: 0x0015A7AA
		public DirectoryObjectClass ExternalDirectoryObjectClass
		{
			get
			{
				return (DirectoryObjectClass)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.ExternalDirectoryObjectClass];
			}
		}

		// Token: 0x170023A6 RID: 9126
		// (get) Token: 0x06006475 RID: 25717 RVA: 0x0015C5C1 File Offset: 0x0015A7C1
		public DateTime? WhenChanged
		{
			get
			{
				return (DateTime?)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.WhenChanged];
			}
		}

		// Token: 0x170023A7 RID: 9127
		// (get) Token: 0x06006476 RID: 25718 RVA: 0x0015C5D8 File Offset: 0x0015A7D8
		public DateTime? WhenChangedUTC
		{
			get
			{
				return (DateTime?)this.propertyBag[FailedMSOSyncObjectPresentationObjectSchema.WhenChangedUTC];
			}
		}

		// Token: 0x170023A8 RID: 9128
		// (get) Token: 0x06006477 RID: 25719 RVA: 0x0015C5EF File Offset: 0x0015A7EF
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return FailedMSOSyncObjectPresentationObject.SchemaObject;
			}
		}

		// Token: 0x170023A9 RID: 9129
		// (get) Token: 0x06006478 RID: 25720 RVA: 0x0015C5F6 File Offset: 0x0015A7F6
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x040042CA RID: 17098
		private readonly FailedMSOSyncObject divergence;

		// Token: 0x040042CB RID: 17099
		private static readonly FailedMSOSyncObjectPresentationObjectSchema SchemaObject = ObjectSchema.GetInstance<FailedMSOSyncObjectPresentationObjectSchema>();
	}
}
