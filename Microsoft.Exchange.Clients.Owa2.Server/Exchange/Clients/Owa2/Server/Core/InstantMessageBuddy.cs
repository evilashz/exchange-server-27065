using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000130 RID: 304
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InstantMessageBuddy : IComparable<InstantMessageBuddy>
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0002344A File Offset: 0x0002164A
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x00023452 File Offset: 0x00021652
		[DataMember(Order = 1)]
		public string SipUri { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002345B File Offset: 0x0002165B
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00023463 File Offset: 0x00021663
		[DataMember(Order = 2)]
		public string Guid { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0002346C File Offset: 0x0002166C
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x00023474 File Offset: 0x00021674
		[DataMember(Order = 3)]
		public string DisplayName { get; set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0002347D File Offset: 0x0002167D
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x00023485 File Offset: 0x00021685
		[DataMember(Order = 5)]
		public int AddressType { get; set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002348E File Offset: 0x0002168E
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x00023496 File Offset: 0x00021696
		[DataMember(Order = 6)]
		public bool Tagged { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x000234A7 File Offset: 0x000216A7
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x000234DB File Offset: 0x000216DB
		[DataMember(Order = 7)]
		public string[] GroupIds
		{
			get
			{
				return (from g in this.groups.Values
				select g.Id).ToArray<string>();
			}
			set
			{
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x000234DD File Offset: 0x000216DD
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x000234E5 File Offset: 0x000216E5
		[DataMember(Order = 8)]
		public string FirstName { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x000234EE File Offset: 0x000216EE
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x000234F6 File Offset: 0x000216F6
		[DataMember(Order = 9)]
		public string LastName { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000234FF File Offset: 0x000216FF
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x00023507 File Offset: 0x00021707
		[DataMember(Order = 10)]
		public EmailAddressWrapper EmailAddress { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00023510 File Offset: 0x00021710
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00023518 File Offset: 0x00021718
		[DataMember(Order = 11)]
		public ItemId PersonaId { get; set; }

		// Token: 0x06000A3B RID: 2619 RVA: 0x00023521 File Offset: 0x00021721
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

		// Token: 0x06000A3C RID: 2620 RVA: 0x00023540 File Offset: 0x00021740
		internal static InstantMessageBuddy Create(string guid, string sipUri, string displayName, EmailAddressWrapper emailAddress)
		{
			return new InstantMessageBuddy
			{
				Guid = guid,
				SipUri = InstantMessageUtilities.ToSipFormat(sipUri),
				DisplayName = displayName,
				AddressType = 0,
				groups = new Dictionary<string, InstantMessageGroup>(),
				Tagged = false,
				FirstName = null,
				LastName = null,
				EmailAddress = emailAddress
			};
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002359C File Offset: 0x0002179C
		internal void AddGroup(InstantMessageGroup group)
		{
			if (group.GroupType == InstantMessageGroupType.Tagged)
			{
				this.Tagged = true;
			}
			this.groups[group.Id] = group;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000235C0 File Offset: 0x000217C0
		internal void AddGroups(string[] groupIds)
		{
			if (groupIds != null)
			{
				foreach (string groupId in groupIds)
				{
					this.AddGroup(InstantMessageGroup.Create(groupId, string.Empty));
				}
			}
		}

		// Token: 0x040006BB RID: 1723
		private Dictionary<string, InstantMessageGroup> groups;
	}
}
