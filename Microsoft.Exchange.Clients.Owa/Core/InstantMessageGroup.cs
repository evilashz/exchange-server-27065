using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000138 RID: 312
	internal class InstantMessageGroup : IComparable<InstantMessageGroup>
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x00045F8C File Offset: 0x0004418C
		private InstantMessageGroup()
		{
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00045F94 File Offset: 0x00044194
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x00045F9C File Offset: 0x0004419C
		internal string Name { get; private set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00045FA5 File Offset: 0x000441A5
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x00045FAD File Offset: 0x000441AD
		internal string Id { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00045FB6 File Offset: 0x000441B6
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00045FBE File Offset: 0x000441BE
		internal InstantMessageGroupType Type { get; private set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00045FC7 File Offset: 0x000441C7
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x00045FCF File Offset: 0x000441CF
		internal bool Expanded { get; private set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00045FD8 File Offset: 0x000441D8
		internal bool VisibleOnClient
		{
			get
			{
				return this.Type != InstantMessageGroupType.Tagged && this.Type != InstantMessageGroupType.Pinned;
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00045FF1 File Offset: 0x000441F1
		internal static InstantMessageGroup Create(string id, string name)
		{
			return InstantMessageGroup.Create(id, name, InstantMessageGroupType.Standard);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00045FFC File Offset: 0x000441FC
		internal static InstantMessageGroup Create(string id, string name, InstantMessageGroupType type)
		{
			InstantMessageGroup instantMessageGroup = new InstantMessageGroup();
			instantMessageGroup.Id = id;
			instantMessageGroup.Name = name;
			instantMessageGroup.Type = type;
			instantMessageGroup.Expanded = false;
			if (name == "~")
			{
				instantMessageGroup.Name = LocalizedStrings.GetNonEncoded(-1499962683);
				instantMessageGroup.Type = InstantMessageGroupType.OtherContacts;
			}
			return instantMessageGroup;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00046050 File Offset: 0x00044250
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
			if (this.Type == InstantMessageGroupType.Favorites)
			{
				return -1;
			}
			if (otherGroup.Type == InstantMessageGroupType.Favorites)
			{
				return 1;
			}
			return this.Name.CompareTo(otherGroup.Name);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00046084 File Offset: 0x00044284
		internal void SetExpandedState(HashSet<string> expandedGroupIds)
		{
			this.Expanded = (expandedGroupIds != null && expandedGroupIds.Contains(InstantMessageUtilities.ToGroupFormat(this.Id)));
			if (this.Type == InstantMessageGroupType.OtherContacts && expandedGroupIds == null)
			{
				this.Expanded = true;
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000460B8 File Offset: 0x000442B8
		internal string SerializeToJavascript()
		{
			return string.Format("['{0}','{1}',{2},{3}]", new object[]
			{
				Utilities.JavascriptEncode(this.Name ?? string.Empty),
				Utilities.JavascriptEncode(InstantMessageUtilities.ToGroupFormat(this.Id)),
				(int)this.Type,
				this.Expanded ? "1" : "0"
			});
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00046126 File Offset: 0x00044326
		internal string SerializeIdToJavascript()
		{
			return string.Format("'{0}'", Utilities.JavascriptEncode(InstantMessageUtilities.ToGroupFormat(this.Id)));
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00046142 File Offset: 0x00044342
		internal string SerializeIdAndNameToJavascript()
		{
			return string.Format("'{0}','{1}'", Utilities.JavascriptEncode(InstantMessageUtilities.ToGroupFormat(this.Id)), Utilities.JavascriptEncode(this.Name ?? string.Empty));
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00046172 File Offset: 0x00044372
		internal string SerializeIdAndNameToJavascriptArray()
		{
			return string.Format("[{0}]", this.SerializeIdAndNameToJavascript());
		}
	}
}
