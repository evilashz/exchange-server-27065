using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000357 RID: 855
	public class DistributionListMemberListView : ListView
	{
		// Token: 0x06002056 RID: 8278 RVA: 0x000BB42F File Offset: 0x000B962F
		internal DistributionListMemberListView(UserContext userContext, DistributionList distributionList, ColumnId sortedColumn, SortOrder order) : this(userContext, DistributionListMemberListView.GetRecipientsFromDistributionList(distributionList), sortedColumn, order)
		{
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000BB441 File Offset: 0x000B9641
		internal DistributionListMemberListView(UserContext userContext, RecipientInfo[] recipients, int count, ColumnId sortedColumn, SortOrder order) : this(userContext, DistributionListMemberListView.GetRecipientsFromRecipientInfo(recipients, count), sortedColumn, order)
		{
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000BB455 File Offset: 0x000B9655
		private DistributionListMemberListView(UserContext userContext, List<Participant> participants, ColumnId sortedColumn, SortOrder order) : base(userContext, sortedColumn, order, false)
		{
			this.participants = participants;
			base.Initialize();
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x000BB46F File Offset: 0x000B966F
		protected override string EventNamespace
		{
			get
			{
				return "EditPDL";
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600205A RID: 8282 RVA: 0x000BB476 File Offset: 0x000B9676
		protected override ViewType ViewTypeId
		{
			get
			{
				return ViewType.ContactModule;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x000BB479 File Offset: 0x000B9679
		protected override bool IsSortable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000BB47C File Offset: 0x000B967C
		private static List<Participant> GetRecipientsFromDistributionList(DistributionList distributionList)
		{
			List<Participant> list = new List<Participant>();
			if (distributionList == null)
			{
				return list;
			}
			foreach (DistributionListMember distributionListMember in distributionList)
			{
				if (!(distributionListMember.Participant == null))
				{
					if (distributionListMember.Participant.Origin is OneOffParticipantOrigin && distributionListMember.MainEntryId is ADParticipantEntryId)
					{
						list.Add(distributionListMember.Participant.ChangeOrigin(new DirectoryParticipantOrigin()));
					}
					else
					{
						list.Add(distributionListMember.Participant);
					}
				}
			}
			return list;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000BB51C File Offset: 0x000B971C
		private static List<Participant> GetRecipientsFromRecipientInfo(RecipientInfo[] recipients, int count)
		{
			List<Participant> list = new List<Participant>();
			for (int i = 0; i < Math.Min(recipients.Length, count); i++)
			{
				Participant item;
				recipients[i].ToParticipant(out item);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000BB556 File Offset: 0x000B9756
		public override SortBy[] GetSortByProperties()
		{
			return null;
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000BB55C File Offset: 0x000B975C
		protected override void InitializeListViewContents()
		{
			base.ViewDescriptor = new ViewDescriptor(ColumnId.MemberDisplayName, false, new ColumnId[]
			{
				ColumnId.MemberIcon,
				ColumnId.MemberDisplayName,
				ColumnId.MemberEmail
			});
			base.Contents = new DistributionListContents(base.UserContext, base.ViewDescriptor);
			base.Contents.DataSource = new DistributionListDataSource(base.UserContext, base.Contents.Properties, this.participants, base.SortedColumn, base.SortOrder);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000BB5D8 File Offset: 0x000B97D8
		protected override bool IsValidDataSource(IListViewDataSource dataSource)
		{
			return dataSource is DistributionListDataSource;
		}

		// Token: 0x0400175C RID: 5980
		private List<Participant> participants;
	}
}
