using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x0200007C RID: 124
	public class ReadADRecipient : OwaPage, IRegistryOnlyForm
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0001F556 File Offset: 0x0001D756
		protected override bool UseStrictMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0001F559 File Offset: 0x0001D759
		protected ADRecipient ADRecipient
		{
			get
			{
				return this.adRecipient;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0001F561 File Offset: 0x0001D761
		internal IRecipientSession ADRecipientSession
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0001F569 File Offset: 0x0001D769
		protected string MessageId
		{
			get
			{
				return this.messageIdString ?? string.Empty;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0001F57A File Offset: 0x0001D77A
		protected string ChangeKey
		{
			get
			{
				return this.changeKey;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0001F582 File Offset: 0x0001D782
		protected AddressBook.Mode AddressBookMode
		{
			get
			{
				return this.viewMode;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0001F58A File Offset: 0x0001D78A
		protected string RecipientId
		{
			get
			{
				return this.recipientId;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0001F592 File Offset: 0x0001D792
		protected int ReadItemType
		{
			get
			{
				if (!(this.adRecipient is IADDistributionList))
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0001F5A4 File Offset: 0x0001D7A4
		protected int RecipientWell
		{
			get
			{
				return (int)this.recipientWell;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0001F5AC File Offset: 0x0001D7AC
		protected string DisplayName
		{
			get
			{
				return this.adRecipient.DisplayName;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001F5B9 File Offset: 0x0001D7B9
		protected static void RenderSecondaryNavigation(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table class=\"snt\"><tr><td class=\"absn\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-454140714));
			writer.Write("</td></tr></table>");
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001F5EF File Offset: 0x0001D7EF
		protected static void RenderDetailHeader(TextWriter writer, Strings.IDs name)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<tr><td colspan=2 class=\"hd lp\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(name));
			writer.Write("</td></tr>");
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001F621 File Offset: 0x0001D821
		protected static void RenderAddressHeader(TextWriter writer, Strings.IDs name)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<tr><td colspan=2 class=\"hd\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(name));
			writer.Write("</td></tr>");
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001F654 File Offset: 0x0001D854
		protected static void RenderADRecipient(TextWriter writer, int readItemType, ADObjectId adObjectId, string displayName)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<a href=\"#\" id=\"");
			writer.Write(Utilities.GetBase64StringFromADObjectId(adObjectId));
			writer.Write("\" class=\"peer\"");
			writer.Write(" onClick=\"return loadOrgP(this,");
			writer.Write(readItemType.ToString());
			writer.Write(")\">");
			Utilities.HtmlEncode(displayName, writer);
			writer.Write("</a>");
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.recipientId = Utilities.GetQueryStringParameter(base.Request, "id");
			this.hasOwner = !string.IsNullOrEmpty(Utilities.GetQueryStringParameter(base.Request, "oT", false));
			if (this.hasOwner)
			{
				this.viewMode = AddressBookHelper.TryReadAddressBookMode(base.Request, AddressBook.Mode.None);
				if (AddressBook.IsEditingMode(this.viewMode))
				{
					this.messageIdString = Utilities.GetQueryStringParameter(base.Request, "oId", false);
					this.changeKey = Utilities.GetQueryStringParameter(base.Request, "oCk", false);
				}
			}
			else
			{
				AddressBookViewState addressBookViewState = base.UserContext.LastClientViewState as AddressBookViewState;
				if (addressBookViewState != null)
				{
					this.viewMode = addressBookViewState.Mode;
					this.recipientWell = addressBookViewState.RecipientWell;
					if (addressBookViewState.ItemId != null)
					{
						this.messageIdString = addressBookViewState.ItemId.ToBase64String();
						this.changeKey = addressBookViewState.ItemChangeKey;
					}
				}
			}
			this.session = Utilities.CreateADRecipientSession(Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().LCID, true, ConsistencyMode.FullyConsistent, true, base.UserContext);
			ADObjectId adobjectId = DirectoryAssistance.ParseADObjectId(this.recipientId);
			if (adobjectId == null)
			{
				throw new OwaADObjectNotFoundException();
			}
			this.adRecipient = this.session.Read(adobjectId);
			if (this.adRecipient == null)
			{
				throw new OwaADObjectNotFoundException();
			}
			this.session = Utilities.CreateADRecipientSession(Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, true, base.UserContext);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001F830 File Offset: 0x0001DA30
		protected void RenderNavigation()
		{
			NavigationModule navigationModule = NavigationModule.AddressBook;
			switch (this.viewMode)
			{
			case AddressBook.Mode.EditMessage:
			case AddressBook.Mode.EditMeetingResponse:
				navigationModule = NavigationModule.Mail;
				break;
			case AddressBook.Mode.EditCalendar:
				navigationModule = NavigationModule.Calendar;
				break;
			}
			Navigation navigation = new Navigation(navigationModule, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001F880 File Offset: 0x0001DA80
		protected void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar;
			if (AddressBook.IsEditingMode(this.viewMode))
			{
				string searchUrlSuffix = OptionsBar.BuildPeoplePickerSearchUrlSuffix(this.viewMode, this.MessageId, this.recipientWell);
				optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.PeoplePicker, OptionsBar.RenderingFlags.None, searchUrlSuffix);
			}
			else
			{
				optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.None);
			}
			optionsBar.Render(helpFile);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001F8F0 File Offset: 0x0001DAF0
		protected void RenderHeaderToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			if (this.viewMode == AddressBook.Mode.Lookup || this.viewMode == AddressBook.Mode.None)
			{
				toolbar.RenderButton(ToolbarButtons.SendEmail);
				if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
				{
					toolbar.RenderButton(ToolbarButtons.SendMeetingRequest);
					toolbar.RenderDivider();
				}
				if (base.UserContext.IsFeatureEnabled(Feature.Contacts))
				{
					toolbar.RenderButton(ToolbarButtons.AddToContacts);
					toolbar.RenderDivider();
				}
			}
			toolbar.RenderButton(ToolbarButtons.CloseText);
			toolbar.RenderFill();
			toolbar.RenderButton(ToolbarButtons.CloseImage);
			toolbar.RenderEnd();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001F994 File Offset: 0x0001DB94
		protected void RenderFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001F9C5 File Offset: 0x0001DBC5
		protected void RenderDetailsLabel(TextWriter writer, Strings.IDs name, string value, ThemeFileId? themeFileId)
		{
			this.RenderDetailsLabel(writer, LocalizedStrings.GetHtmlEncoded(name), value, themeFileId);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001F9D8 File Offset: 0x0001DBD8
		protected void RenderDetailsLabel(TextWriter writer, string name, string value, ThemeFileId? themeFileId)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<tr><td class=\"lbl lp\" nowrap>");
			writer.Write(name);
			writer.Write("</td><td class=\"txvl");
			if (themeFileId != null)
			{
				writer.Write(" phtdpd\"><img ");
				Utilities.RenderImageAltAttribute(writer, base.UserContext, themeFileId.Value);
				writer.Write(" src=\"");
				base.UserContext.RenderThemeFileUrl(writer, themeFileId.Value);
			}
			writer.Write("\">");
			Utilities.HtmlEncode(value, writer);
			writer.Write("</td></tr>");
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001FA74 File Offset: 0x0001DC74
		protected void RenderCustomJavascriptVariables()
		{
			if (this.hasOwner)
			{
				RenderingUtilities.RenderJavascriptVariable(base.Response.Output, "a_fOw", true);
				RenderingUtilities.RenderOwnerItemInformationFromQueryString(base.Request, base.Response.Output);
				return;
			}
			RenderingUtilities.RenderJavascriptVariable(base.Response.Output, "a_fOw", false);
			RenderingUtilities.RenderJavascriptVariable(base.Response.Output, "a_sBkUrl", base.UserContext.LastClientViewState.ToQueryString());
		}

		// Token: 0x040002AE RID: 686
		private IRecipientSession session;

		// Token: 0x040002AF RID: 687
		private ADRecipient adRecipient;

		// Token: 0x040002B0 RID: 688
		private string changeKey = string.Empty;

		// Token: 0x040002B1 RID: 689
		private string messageIdString;

		// Token: 0x040002B2 RID: 690
		private string recipientId;

		// Token: 0x040002B3 RID: 691
		private AddressBook.Mode viewMode;

		// Token: 0x040002B4 RID: 692
		private RecipientItemType recipientWell = RecipientItemType.To;

		// Token: 0x040002B5 RID: 693
		private bool hasOwner;
	}
}
