using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class GroupBlackoutDisplay : ConfigurableObject
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000DF44 File Offset: 0x0000C144
		public GroupBlackoutDisplay() : base(new SimplePropertyBag(GroupBlackoutDisplay.GroupBlackoutDisplaySchema.GroupName, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
			base.ResetChangeTracking();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000DF71 File Offset: 0x0000C171
		public GroupBlackoutDisplay(string groupName, BlackoutInterval blackout) : this()
		{
			this.GroupName = groupName;
			this.StartDate = new DateTime?(blackout.StartDate);
			this.EndDate = new DateTime?(blackout.EndDate);
			this.Reason = blackout.Reason;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000DFAE File Offset: 0x0000C1AE
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		public string GroupName
		{
			get
			{
				return (string)this[GroupBlackoutDisplay.GroupBlackoutDisplaySchema.GroupName];
			}
			internal set
			{
				this[GroupBlackoutDisplay.GroupBlackoutDisplaySchema.GroupName] = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000DFCE File Offset: 0x0000C1CE
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		public DateTime? StartDate
		{
			get
			{
				return (DateTime?)this[GroupBlackoutDisplay.GroupBlackoutDisplaySchema.StartDate];
			}
			internal set
			{
				this[GroupBlackoutDisplay.GroupBlackoutDisplaySchema.StartDate] = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000DFF3 File Offset: 0x0000C1F3
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000E005 File Offset: 0x0000C205
		public DateTime? EndDate
		{
			get
			{
				return (DateTime?)this[GroupBlackoutDisplay.GroupBlackoutDisplaySchema.EndDate];
			}
			internal set
			{
				this[GroupBlackoutDisplay.GroupBlackoutDisplaySchema.EndDate] = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000E018 File Offset: 0x0000C218
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000E02A File Offset: 0x0000C22A
		public string Reason
		{
			get
			{
				return (string)this[GroupBlackoutDisplay.GroupBlackoutDisplaySchema.Reason];
			}
			internal set
			{
				this[GroupBlackoutDisplay.GroupBlackoutDisplaySchema.Reason] = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000E038 File Offset: 0x0000C238
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return GroupBlackoutDisplay.schema;
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000E040 File Offset: 0x0000C240
		public override bool Equals(object obj)
		{
			GroupBlackoutDisplay groupBlackoutDisplay = obj as GroupBlackoutDisplay;
			return groupBlackoutDisplay != null && (string.Equals(this.GroupName, groupBlackoutDisplay.GroupName, StringComparison.OrdinalIgnoreCase) && object.Equals(this.StartDate, groupBlackoutDisplay.StartDate)) && object.Equals(this.EndDate, groupBlackoutDisplay.EndDate);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000E0A7 File Offset: 0x0000C2A7
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000112 RID: 274
		private static GroupBlackoutDisplay.GroupBlackoutDisplaySchema schema = ObjectSchema.GetInstance<GroupBlackoutDisplay.GroupBlackoutDisplaySchema>();

		// Token: 0x0200003E RID: 62
		internal class GroupBlackoutDisplaySchema : SimpleProviderObjectSchema
		{
			// Token: 0x04000113 RID: 275
			public static readonly ProviderPropertyDefinition GroupName = new SimpleProviderPropertyDefinition("GroupName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000114 RID: 276
			public static readonly ProviderPropertyDefinition StartDate = new SimpleProviderPropertyDefinition("StartDate", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000115 RID: 277
			public static readonly ProviderPropertyDefinition EndDate = new SimpleProviderPropertyDefinition("EndDate", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x04000116 RID: 278
			public static readonly ProviderPropertyDefinition Reason = new SimpleProviderPropertyDefinition("Reason", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
