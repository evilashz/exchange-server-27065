using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class GroupCapacityDisplay : ConfigurableObject
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000E179 File Offset: 0x0000C379
		public GroupCapacityDisplay() : base(new SimplePropertyBag(GroupCapacityDisplay.GroupCapacityDisplaySchema.GroupName, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
			base.ResetChangeTracking();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000E1A6 File Offset: 0x0000C3A6
		public GroupCapacityDisplay(string groupName, CapacityBlock capacity) : this()
		{
			this.GroupName = groupName;
			this.StartDate = new DateTime?(capacity.StartDate);
			this.UpgradeUnits = new int?(capacity.UpgradeUnits);
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000E1D7 File Offset: 0x0000C3D7
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000E1E9 File Offset: 0x0000C3E9
		public string GroupName
		{
			get
			{
				return (string)this[GroupCapacityDisplay.GroupCapacityDisplaySchema.GroupName];
			}
			internal set
			{
				this[GroupCapacityDisplay.GroupCapacityDisplaySchema.GroupName] = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000E1F7 File Offset: 0x0000C3F7
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000E209 File Offset: 0x0000C409
		public DateTime? StartDate
		{
			get
			{
				return (DateTime?)this[GroupCapacityDisplay.GroupCapacityDisplaySchema.StartDate];
			}
			internal set
			{
				this[GroupCapacityDisplay.GroupCapacityDisplaySchema.StartDate] = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000E21C File Offset: 0x0000C41C
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000E22E File Offset: 0x0000C42E
		public int? UpgradeUnits
		{
			get
			{
				return (int?)this[GroupCapacityDisplay.GroupCapacityDisplaySchema.UpgradeUnits];
			}
			internal set
			{
				this[GroupCapacityDisplay.GroupCapacityDisplaySchema.UpgradeUnits] = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000E241 File Offset: 0x0000C441
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return GroupCapacityDisplay.schema;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000E248 File Offset: 0x0000C448
		public override bool Equals(object obj)
		{
			GroupCapacityDisplay groupCapacityDisplay = obj as GroupCapacityDisplay;
			return groupCapacityDisplay != null && (string.Equals(this.GroupName, groupCapacityDisplay.GroupName, StringComparison.OrdinalIgnoreCase) && object.Equals(this.StartDate, groupCapacityDisplay.StartDate)) && this.UpgradeUnits == groupCapacityDisplay.UpgradeUnits;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000117 RID: 279
		private static GroupCapacityDisplay.GroupCapacityDisplaySchema schema = ObjectSchema.GetInstance<GroupCapacityDisplay.GroupCapacityDisplaySchema>();

		// Token: 0x02000040 RID: 64
		internal class GroupCapacityDisplaySchema : SimpleProviderObjectSchema
		{
			// Token: 0x04000118 RID: 280
			public static readonly ProviderPropertyDefinition GroupName = new SimpleProviderPropertyDefinition("GroupName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000119 RID: 281
			public static readonly ProviderPropertyDefinition StartDate = new SimpleProviderPropertyDefinition("StartDate", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x0400011A RID: 282
			public static readonly ProviderPropertyDefinition UpgradeUnits = new SimpleProviderPropertyDefinition("UpgradeUnits", ExchangeObjectVersion.Exchange2010, typeof(int?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
