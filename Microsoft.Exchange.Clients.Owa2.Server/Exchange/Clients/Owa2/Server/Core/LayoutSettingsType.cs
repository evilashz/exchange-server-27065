using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003E2 RID: 994
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LayoutSettingsType
	{
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00078465 File Offset: 0x00076665
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0007846D File Offset: 0x0007666D
		[DataMember]
		public bool ShowInferenceUiElements
		{
			get
			{
				return this.showInferenceUiElements;
			}
			set
			{
				this.showInferenceUiElements = value;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00078476 File Offset: 0x00076676
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x0007847E File Offset: 0x0007667E
		[DataMember]
		public bool ShowSenderOnTopInListView
		{
			get
			{
				return this.showSenderOnTopInListView;
			}
			set
			{
				this.showSenderOnTopInListView = value;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x00078487 File Offset: 0x00076687
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x0007848F File Offset: 0x0007668F
		[DataMember]
		public bool ShowPreviewTextInListView
		{
			get
			{
				return this.showPreviewTextInListView;
			}
			set
			{
				this.showPreviewTextInListView = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x00078498 File Offset: 0x00076698
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x000784AA File Offset: 0x000766AA
		[DataMember(Name = "ConversationSortOrder")]
		public string ConversationSortOrderString
		{
			get
			{
				return this.ConversationSortOrder.ToString();
			}
			set
			{
				this.ConversationSortOrder = (ConversationSortOrder)Enum.Parse(typeof(ConversationSortOrder), value);
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x000784C7 File Offset: 0x000766C7
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x000784CF File Offset: 0x000766CF
		public ConversationSortOrder ConversationSortOrder
		{
			get
			{
				return this.conversationOrder;
			}
			set
			{
				this.conversationOrder = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x000784D8 File Offset: 0x000766D8
		// (set) Token: 0x06001FEE RID: 8174 RVA: 0x000784E0 File Offset: 0x000766E0
		[DataMember]
		public bool HideDeletedItems
		{
			get
			{
				return this.hideDeletedItems;
			}
			set
			{
				this.hideDeletedItems = value;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x000784E9 File Offset: 0x000766E9
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x000784F1 File Offset: 0x000766F1
		[DataMember]
		public int GlobalReadingPanePosition
		{
			get
			{
				return this.globalReadingPanePosition;
			}
			set
			{
				this.globalReadingPanePosition = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x000784FA File Offset: 0x000766FA
		// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x00078502 File Offset: 0x00076702
		[DataMember]
		public bool ShowFirstMessageOnSignIn
		{
			get
			{
				return this.showFirstMessageOnSignIn;
			}
			set
			{
				this.showFirstMessageOnSignIn = value;
			}
		}

		// Token: 0x04001211 RID: 4625
		private bool showInferenceUiElements;

		// Token: 0x04001212 RID: 4626
		private bool showSenderOnTopInListView;

		// Token: 0x04001213 RID: 4627
		private bool showPreviewTextInListView;

		// Token: 0x04001214 RID: 4628
		private ConversationSortOrder conversationOrder;

		// Token: 0x04001215 RID: 4629
		private bool hideDeletedItems;

		// Token: 0x04001216 RID: 4630
		private int globalReadingPanePosition;

		// Token: 0x04001217 RID: 4631
		private bool showFirstMessageOnSignIn;
	}
}
