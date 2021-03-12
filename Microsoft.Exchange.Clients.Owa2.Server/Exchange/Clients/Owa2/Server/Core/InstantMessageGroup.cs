using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000133 RID: 307
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InstantMessageGroup : IComparable<InstantMessageGroup>
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00023A5A File Offset: 0x00021C5A
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00023A62 File Offset: 0x00021C62
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string Name { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00023A6B File Offset: 0x00021C6B
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00023A73 File Offset: 0x00021C73
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string Id { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00023A7C File Offset: 0x00021C7C
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00023A84 File Offset: 0x00021C84
		[IgnoreDataMember]
		public InstantMessageGroupType GroupType { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00023A8D File Offset: 0x00021C8D
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x00023A9F File Offset: 0x00021C9F
		[DataMember(Name = "GroupType", Order = 3)]
		public string GroupTypeString
		{
			get
			{
				return this.GroupType.ToString();
			}
			set
			{
				this.GroupType = InstantMessageUtilities.ParseEnumValue<InstantMessageGroupType>(value, InstantMessageGroupType.Standard);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00023AAE File Offset: 0x00021CAE
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00023AB6 File Offset: 0x00021CB6
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public bool Expanded { get; private set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00023ABF File Offset: 0x00021CBF
		[IgnoreDataMember]
		internal bool VisibleOnClient
		{
			get
			{
				return this.GroupType != InstantMessageGroupType.Tagged && this.GroupType != InstantMessageGroupType.Pinned;
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00023AD8 File Offset: 0x00021CD8
		public int CompareTo(InstantMessageGroup otherGroup)
		{
			if (this == otherGroup)
			{
				return 0;
			}
			if (otherGroup == null)
			{
				return 1;
			}
			if (this.GroupType == InstantMessageGroupType.Favorites)
			{
				return -1;
			}
			if (otherGroup.GroupType == InstantMessageGroupType.Favorites)
			{
				return 1;
			}
			return this.Name.CompareTo(otherGroup.Name);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00023B0C File Offset: 0x00021D0C
		internal static InstantMessageGroup Create(string groupId, string groupName)
		{
			return InstantMessageGroup.Create(groupId, groupName, InstantMessageGroupType.Standard);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00023B18 File Offset: 0x00021D18
		internal static InstantMessageGroup Create(string groupId, string groupName, InstantMessageGroupType groupType)
		{
			InstantMessageGroup instantMessageGroup = new InstantMessageGroup
			{
				Id = groupId,
				Name = groupName,
				GroupType = groupType,
				Expanded = false
			};
			if (groupName.Equals("~"))
			{
				instantMessageGroup.Name = Strings.GetLocalizedString(-1499962683);
				instantMessageGroup.GroupType = InstantMessageGroupType.OtherContacts;
			}
			return instantMessageGroup;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00023B6E File Offset: 0x00021D6E
		internal void SetExpandedState(HashSet<string> expandedGroupIds)
		{
			this.Expanded = (expandedGroupIds != null && expandedGroupIds.Contains(InstantMessageUtilities.ToGroupFormat(this.Id)));
			if (this.GroupType == InstantMessageGroupType.OtherContacts && expandedGroupIds == null)
			{
				this.Expanded = true;
			}
		}

		// Token: 0x040006CB RID: 1739
		public const string OtherContactsGroupName = "~";
	}
}
