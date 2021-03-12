using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200044F RID: 1103
	public class EditDistributionList : EditItemForm, IRegistryOnlyForm
	{
		// Token: 0x06002808 RID: 10248 RVA: 0x000E2B68 File Offset: 0x000E0D68
		protected override void OnLoad(EventArgs e)
		{
			ExTraceGlobals.ContactsCallTracer.TraceDebug((long)this.GetHashCode(), "EditDistributionList.OnLoad");
			base.OnLoad(e);
			this.distributionList = base.Initialize<DistributionList>(false, EditDistributionList.prefetchProperties);
			this.listView = new DistributionListMemberListView(base.UserContext, this.distributionList, ColumnId.MemberDisplayName, SortOrder.Ascending);
			if (this.distributionList != null)
			{
				InfobarMessageBuilder.AddFlag(this.infobar, base.Item, base.UserContext);
			}
			this.toolbar = new EditDistributionListToolbar(this.distributionList);
			this.recipientWell = new MessageRecipientWell();
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x000E2BF9 File Offset: 0x000E0DF9
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x000E2C01 File Offset: 0x000E0E01
		protected bool IsDLNull
		{
			get
			{
				return this.distributionList == null;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x000E2C0C File Offset: 0x000E0E0C
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000E2C14 File Offset: 0x000E0E14
		protected void RenderMemberListView()
		{
			this.listView.Render(base.Response.Output, ListView.RenderFlags.Behavior | ListView.RenderFlags.Contents | ListView.RenderFlags.Headers);
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000E2C2D File Offset: 0x000E0E2D
		protected void RenderNotes()
		{
			BodyConversionUtilities.RenderMeetingPlainTextBody(base.Response.Output, base.Item, base.UserContext, false);
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000E2C4D File Offset: 0x000E0E4D
		protected void RenderAddMemberToListButton()
		{
			RenderingUtilities.RenderButton(base.Response.Output, "btnAddToLst", string.Empty, string.Empty, "&nbsp;&nbsp;" + LocalizedStrings.GetHtmlEncoded(-100912444) + "&nbsp;&nbsp;");
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000E2C87 File Offset: 0x000E0E87
		protected void RenderRemoveButton()
		{
			RenderingUtilities.RenderButton(base.Response.Output, "btnRmv", string.Empty, "onBtnRmv()", LocalizedStrings.GetHtmlEncoded(1967506178));
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000E2CB2 File Offset: 0x000E0EB2
		protected void RenderMembersButton()
		{
			RenderingUtilities.RenderButton(base.Response.Output, "btnMmb", string.Empty, string.Empty, LocalizedStrings.GetHtmlEncoded(-878744874));
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x000E2CDD File Offset: 0x000E0EDD
		protected string DistributionListName
		{
			get
			{
				if (!this.IsDLNull)
				{
					return Utilities.HtmlEncode(this.distributionList.DisplayName);
				}
				return string.Empty;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x000E2CFD File Offset: 0x000E0EFD
		protected Toolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000E2D05 File Offset: 0x000E0F05
		protected void RenderCategories()
		{
			if (base.Item != null)
			{
				CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000E2D26 File Offset: 0x000E0F26
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, base.Item);
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000E2D3C File Offset: 0x000E0F3C
		protected void RenderTitle()
		{
			if (this.distributionList == null)
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1576800949));
				return;
			}
			string displayName = this.distributionList.DisplayName;
			if (string.IsNullOrEmpty(displayName))
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1576800949));
				return;
			}
			Utilities.HtmlEncode(displayName, base.Response.Output);
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x000E2DA2 File Offset: 0x000E0FA2
		protected static int StoreObjectTypeDistributionList
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x04001BE2 RID: 7138
		private static readonly PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
		{
			ItemSchema.Categories,
			ItemSchema.FlagStatus,
			ItemSchema.FlagCompleteTime,
			MessageItemSchema.ReplyTime,
			ItemSchema.UtcDueDate,
			ItemSchema.UtcStartDate,
			ItemSchema.ReminderDueBy,
			ItemSchema.ReminderIsSet,
			StoreObjectSchema.EffectiveRights
		};

		// Token: 0x04001BE3 RID: 7139
		private Infobar infobar = new Infobar();

		// Token: 0x04001BE4 RID: 7140
		private MessageRecipientWell recipientWell;

		// Token: 0x04001BE5 RID: 7141
		private Toolbar toolbar;

		// Token: 0x04001BE6 RID: 7142
		private DistributionList distributionList;

		// Token: 0x04001BE7 RID: 7143
		private ListView listView;
	}
}
