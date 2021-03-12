using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000131 RID: 305
	internal class InstantMessageBuddy : IComparable<InstantMessageBuddy>
	{
		// Token: 0x060009FC RID: 2556 RVA: 0x000457B0 File Offset: 0x000439B0
		private InstantMessageBuddy()
		{
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x000457B8 File Offset: 0x000439B8
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x000457C0 File Offset: 0x000439C0
		internal string Guid { get; private set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x000457C9 File Offset: 0x000439C9
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x000457D1 File Offset: 0x000439D1
		internal string SipUri { get; private set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000457DA File Offset: 0x000439DA
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x000457E2 File Offset: 0x000439E2
		internal string DisplayName { get; private set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x000457EB File Offset: 0x000439EB
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x000457F3 File Offset: 0x000439F3
		internal string RequestMessage { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x000457FC File Offset: 0x000439FC
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00045804 File Offset: 0x00043A04
		internal int AddressType { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0004580D File Offset: 0x00043A0D
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00045815 File Offset: 0x00043A15
		internal bool Tagged { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00045826 File Offset: 0x00043A26
		internal string[] GroupIds
		{
			get
			{
				return (from g in this.groups.Values
				select g.Id).ToArray<string>();
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0004585C File Offset: 0x00043A5C
		internal static InstantMessageBuddy Create(string guid, string sipUri, string displayName)
		{
			return new InstantMessageBuddy
			{
				Guid = guid,
				SipUri = InstantMessageUtilities.ToSipFormat(sipUri),
				DisplayName = displayName,
				RequestMessage = string.Empty,
				AddressType = 0,
				groups = new Dictionary<string, InstantMessageGroup>(),
				Tagged = false
			};
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000458AE File Offset: 0x00043AAE
		public int CompareTo(InstantMessageBuddy otherBuddy)
		{
			if (this == otherBuddy)
			{
				return 0;
			}
			if (otherBuddy == null)
			{
				return 1;
			}
			return this.DisplayName.CompareTo(otherBuddy.DisplayName);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000458CC File Offset: 0x00043ACC
		internal void AddGroup(InstantMessageGroup group)
		{
			if (group.Type == InstantMessageGroupType.Tagged)
			{
				this.Tagged = true;
			}
			this.groups[group.Id] = group;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000458F0 File Offset: 0x00043AF0
		internal void AddGroups(string[] groupIds)
		{
			if (groupIds != null)
			{
				foreach (string id in groupIds)
				{
					this.AddGroup(InstantMessageGroup.Create(id, string.Empty));
				}
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00045928 File Offset: 0x00043B28
		internal string SerializeToJavascript()
		{
			return string.Format("['{0}','{1}','{2}','{3}',{4},{5}]", new object[]
			{
				Utilities.JavascriptEncode(this.SipUri),
				Utilities.JavascriptEncode(this.Guid),
				Utilities.JavascriptEncode(this.DisplayName),
				this.AddressType,
				this.SerializeGroupsToJavascript(),
				this.Tagged ? "1" : "0"
			});
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0004599F File Offset: 0x00043B9F
		internal string SerializeSipToJavascript()
		{
			return string.Format("'{0}'", Utilities.JavascriptEncode(this.SipUri));
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000459DC File Offset: 0x00043BDC
		private string SerializeGroupsToJavascript()
		{
			return string.Format("[{0}]", string.Join(",", (from g in this.groups.Values
			where g.VisibleOnClient
			select string.Format("'{0}'", Utilities.JavascriptEncode(InstantMessageUtilities.ToGroupFormat(g.Id)))).ToArray<string>()));
		}

		// Token: 0x0400077A RID: 1914
		private Dictionary<string, InstantMessageGroup> groups;
	}
}
