using System;
using Microsoft.Exchange.Data.Directory.ABProviderFramework;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000A8 RID: 168
	internal sealed class ADABGroup : ABGroup
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x00036B66 File Offset: 0x00034D66
		public ADABGroup(ADABSession ownerSession, ADGroup activeDirectoryGroup) : base(ownerSession)
		{
			if (activeDirectoryGroup == null)
			{
				throw new ArgumentNullException("activeDirectoryGroup");
			}
			if (activeDirectoryGroup.Id == null)
			{
				throw new ArgumentException("activeDirectoryGroup.Id can't be null.", "activeDirectoryGroup.Id");
			}
			this.recipient = activeDirectoryGroup;
			this.activeDirectoryGroup = activeDirectoryGroup;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00036BA3 File Offset: 0x00034DA3
		public ADABGroup(ADABSession ownerSession, ADDynamicGroup dynamicGroup) : base(ownerSession)
		{
			if (dynamicGroup == null)
			{
				throw new ArgumentNullException("dynamicGroup");
			}
			if (dynamicGroup.Id == null)
			{
				throw new ArgumentException("dynamicGroup.Id can't be null.", "dynamicGroup.Id");
			}
			this.recipient = dynamicGroup;
			this.dynamicGroup = dynamicGroup;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00036BE0 File Offset: 0x00034DE0
		protected override string GetAlias()
		{
			return this.recipient.Alias;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00036BED File Offset: 0x00034DED
		protected override bool GetCanEmail()
		{
			return ADABUtils.CanEmailRecipientType(this.recipient.RecipientType);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00036BFF File Offset: 0x00034DFF
		protected override string GetDisplayName()
		{
			return this.recipient.DisplayName;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00036C0C File Offset: 0x00034E0C
		protected override string GetLegacyExchangeDN()
		{
			return this.recipient.LegacyExchangeDN;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00036C1C File Offset: 0x00034E1C
		protected override string GetEmailAddress()
		{
			return this.recipient.PrimarySmtpAddress.ToString();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00036C42 File Offset: 0x00034E42
		protected override ABObjectId GetId()
		{
			if (this.id == null)
			{
				this.id = new ADABObjectId(this.recipient.Id);
			}
			return this.id;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00036C68 File Offset: 0x00034E68
		protected override ABObjectId GetOwnerId()
		{
			if (this.ownerId != null)
			{
				return this.ownerId;
			}
			if (ADABUtils.GetOwnerId(this.recipient, out this.ownerId))
			{
				return this.ownerId;
			}
			return null;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00036C94 File Offset: 0x00034E94
		protected override bool? GetHiddenMembership()
		{
			if (this.activeDirectoryGroup != null)
			{
				return new bool?(this.activeDirectoryGroup.HiddenGroupMembershipEnabled);
			}
			if (this.dynamicGroup != null)
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x040005D8 RID: 1496
		private ADRecipient recipient;

		// Token: 0x040005D9 RID: 1497
		private ADGroup activeDirectoryGroup;

		// Token: 0x040005DA RID: 1498
		private ADDynamicGroup dynamicGroup;

		// Token: 0x040005DB RID: 1499
		private ADABObjectId id;

		// Token: 0x040005DC RID: 1500
		private ADABObjectId ownerId;
	}
}
