using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200082B RID: 2091
	internal class SyncGroup : SyncRecipient
	{
		// Token: 0x060067C4 RID: 26564 RVA: 0x0016E22D File Offset: 0x0016C42D
		public SyncGroup(SyncDirection syncDirection) : base(syncDirection)
		{
		}

		// Token: 0x170024B8 RID: 9400
		// (get) Token: 0x060067C5 RID: 26565 RVA: 0x0016E236 File Offset: 0x0016C436
		public override SyncObjectSchema Schema
		{
			get
			{
				return SyncGroup.schema;
			}
		}

		// Token: 0x170024B9 RID: 9401
		// (get) Token: 0x060067C6 RID: 26566 RVA: 0x0016E23D File Offset: 0x0016C43D
		internal override DirectoryObjectClass ObjectClass
		{
			get
			{
				return DirectoryObjectClass.Group;
			}
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x0016E240 File Offset: 0x0016C440
		protected override DirectoryObject CreateDirectoryObject()
		{
			return new Group();
		}

		// Token: 0x170024BA RID: 9402
		// (get) Token: 0x060067C8 RID: 26568 RVA: 0x0016E247 File Offset: 0x0016C447
		// (set) Token: 0x060067C9 RID: 26569 RVA: 0x0016E259 File Offset: 0x0016C459
		public SyncProperty<MultiValuedProperty<SyncLink>> CoManagedBy
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncGroupSchema.CoManagedBy];
			}
			set
			{
				base[SyncGroupSchema.CoManagedBy] = value;
			}
		}

		// Token: 0x170024BB RID: 9403
		// (get) Token: 0x060067CA RID: 26570 RVA: 0x0016E267 File Offset: 0x0016C467
		// (set) Token: 0x060067CB RID: 26571 RVA: 0x0016E279 File Offset: 0x0016C479
		public SyncProperty<bool> MailEnabled
		{
			get
			{
				return (SyncProperty<bool>)base[SyncGroupSchema.MailEnabled];
			}
			set
			{
				base[SyncGroupSchema.MailEnabled] = value;
			}
		}

		// Token: 0x170024BC RID: 9404
		// (get) Token: 0x060067CC RID: 26572 RVA: 0x0016E287 File Offset: 0x0016C487
		// (set) Token: 0x060067CD RID: 26573 RVA: 0x0016E299 File Offset: 0x0016C499
		public SyncProperty<MultiValuedProperty<SyncLink>> ManagedBy
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncGroupSchema.ManagedBy];
			}
			set
			{
				base[SyncGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x170024BD RID: 9405
		// (get) Token: 0x060067CE RID: 26574 RVA: 0x0016E2A7 File Offset: 0x0016C4A7
		// (set) Token: 0x060067CF RID: 26575 RVA: 0x0016E2B9 File Offset: 0x0016C4B9
		public SyncProperty<bool> ReportToOriginatorEnabled
		{
			get
			{
				return (SyncProperty<bool>)base[SyncGroupSchema.ReportToOriginatorEnabled];
			}
			set
			{
				base[SyncGroupSchema.ReportToOriginatorEnabled] = value;
			}
		}

		// Token: 0x170024BE RID: 9406
		// (get) Token: 0x060067D0 RID: 26576 RVA: 0x0016E2C7 File Offset: 0x0016C4C7
		// (set) Token: 0x060067D1 RID: 26577 RVA: 0x0016E2D9 File Offset: 0x0016C4D9
		public SyncProperty<MultiValuedProperty<SyncLink>> Members
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncGroupSchema.Members];
			}
			set
			{
				base[SyncGroupSchema.Members] = value;
			}
		}

		// Token: 0x170024BF RID: 9407
		// (get) Token: 0x060067D2 RID: 26578 RVA: 0x0016E2E7 File Offset: 0x0016C4E7
		// (set) Token: 0x060067D3 RID: 26579 RVA: 0x0016E2F9 File Offset: 0x0016C4F9
		public SyncProperty<bool> SecurityEnabled
		{
			get
			{
				return (SyncProperty<bool>)base[SyncGroupSchema.SecurityEnabled];
			}
			set
			{
				base[SyncGroupSchema.SecurityEnabled] = value;
			}
		}

		// Token: 0x170024C0 RID: 9408
		// (get) Token: 0x060067D4 RID: 26580 RVA: 0x0016E307 File Offset: 0x0016C507
		// (set) Token: 0x060067D5 RID: 26581 RVA: 0x0016E319 File Offset: 0x0016C519
		public SyncProperty<bool> SendOofMessageToOriginatorEnabled
		{
			get
			{
				return (SyncProperty<bool>)base[SyncGroupSchema.SendOofMessageToOriginatorEnabled];
			}
			set
			{
				base[SyncGroupSchema.SendOofMessageToOriginatorEnabled] = value;
			}
		}

		// Token: 0x170024C1 RID: 9409
		// (get) Token: 0x060067D6 RID: 26582 RVA: 0x0016E327 File Offset: 0x0016C527
		// (set) Token: 0x060067D7 RID: 26583 RVA: 0x0016E339 File Offset: 0x0016C539
		public SyncProperty<string> WellKnownObject
		{
			get
			{
				return (SyncProperty<string>)base[SyncGroupSchema.WellKnownObject];
			}
			set
			{
				base[SyncGroupSchema.WellKnownObject] = value;
			}
		}

		// Token: 0x170024C2 RID: 9410
		// (get) Token: 0x060067D8 RID: 26584 RVA: 0x0016E347 File Offset: 0x0016C547
		// (set) Token: 0x060067D9 RID: 26585 RVA: 0x0016E359 File Offset: 0x0016C559
		public override SyncProperty<RecipientTypeDetails> RecipientTypeDetailsValue
		{
			get
			{
				return (SyncProperty<RecipientTypeDetails>)base[SyncGroupSchema.RecipientTypeDetailsValue];
			}
			set
			{
				base[SyncGroupSchema.RecipientTypeDetailsValue] = value;
			}
		}

		// Token: 0x170024C3 RID: 9411
		// (get) Token: 0x060067DA RID: 26586 RVA: 0x0016E367 File Offset: 0x0016C567
		// (set) Token: 0x060067DB RID: 26587 RVA: 0x0016E379 File Offset: 0x0016C579
		public SyncProperty<string> Creator
		{
			get
			{
				return (SyncProperty<string>)base[SyncGroupSchema.Creator];
			}
			set
			{
				base[SyncGroupSchema.Creator] = value;
			}
		}

		// Token: 0x170024C4 RID: 9412
		// (get) Token: 0x060067DC RID: 26588 RVA: 0x0016E387 File Offset: 0x0016C587
		// (set) Token: 0x060067DD RID: 26589 RVA: 0x0016E399 File Offset: 0x0016C599
		public SyncProperty<MultiValuedProperty<string>> SharePointResources
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncGroupSchema.SharePointResources];
			}
			set
			{
				base[SyncGroupSchema.SharePointResources] = value;
			}
		}

		// Token: 0x170024C5 RID: 9413
		// (get) Token: 0x060067DE RID: 26590 RVA: 0x0016E3A7 File Offset: 0x0016C5A7
		// (set) Token: 0x060067DF RID: 26591 RVA: 0x0016E3B9 File Offset: 0x0016C5B9
		public SyncProperty<bool> IsPublic
		{
			get
			{
				return (SyncProperty<bool>)base[SyncGroupSchema.IsPublic];
			}
			set
			{
				base[SyncGroupSchema.IsPublic] = value;
			}
		}

		// Token: 0x170024C6 RID: 9414
		// (get) Token: 0x060067E0 RID: 26592 RVA: 0x0016E3C7 File Offset: 0x0016C5C7
		// (set) Token: 0x060067E1 RID: 26593 RVA: 0x0016E3D9 File Offset: 0x0016C5D9
		public SyncProperty<MultiValuedProperty<SyncLink>> Owners
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncGroupSchema.Owners];
			}
			set
			{
				base[SyncGroupSchema.Owners] = value;
			}
		}

		// Token: 0x170024C7 RID: 9415
		// (get) Token: 0x060067E2 RID: 26594 RVA: 0x0016E3E8 File Offset: 0x0016C5E8
		protected override SyncPropertyDefinition[] MinimumForwardSyncProperties
		{
			get
			{
				List<SyncPropertyDefinition> list = base.MinimumForwardSyncProperties.ToList<SyncPropertyDefinition>();
				list.AddRange(SyncGroup.minimumForwardSyncProperties);
				return list.ToArray();
			}
		}

		// Token: 0x060067E3 RID: 26595 RVA: 0x0016E414 File Offset: 0x0016C614
		public override SyncRecipient CreatePlaceHolder()
		{
			SyncGroup syncGroup = (SyncGroup)base.CreatePlaceHolder();
			syncGroup.CopyChangeFrom(this, SyncGroup.minimumForwardSyncProperties);
			return syncGroup;
		}

		// Token: 0x060067E4 RID: 26596 RVA: 0x0016E43C File Offset: 0x0016C63C
		internal static void SecurityEnabledSetter(object value, IPropertyBag propertyBag)
		{
			GroupTypeFlags groupTypeFlags = (GroupTypeFlags)propertyBag[SyncGroupSchema.GroupType];
			if ((bool)value)
			{
				propertyBag[SyncGroupSchema.GroupType] = (groupTypeFlags | GroupTypeFlags.SecurityEnabled);
				return;
			}
			propertyBag[SyncGroupSchema.GroupType] = (groupTypeFlags & (GroupTypeFlags)2147483647);
		}

		// Token: 0x0400445E RID: 17502
		private static readonly SyncGroupSchema schema = ObjectSchema.GetInstance<SyncGroupSchema>();

		// Token: 0x0400445F RID: 17503
		private static readonly SyncPropertyDefinition[] minimumForwardSyncProperties = new SyncPropertyDefinition[]
		{
			SyncGroupSchema.MailEnabled,
			SyncGroupSchema.WellKnownObject,
			SyncGroupSchema.RecipientTypeDetailsValue
		};
	}
}
