using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200046F RID: 1135
	public class ReadDistributionList : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x06002ACD RID: 10957 RVA: 0x000F0D34 File Offset: 0x000EEF34
		protected override void OnLoad(EventArgs e)
		{
			ExTraceGlobals.ContactsCallTracer.TraceDebug((long)this.GetHashCode(), "ReadDistributionList.OnLoad");
			base.OnLoad(e);
			this.distributionList = base.Initialize<DistributionList>(ReadDistributionList.prefetchProperties);
			InfobarMessageBuilder.AddFlag(this.infobar, base.Item, base.UserContext);
			InfobarMessageBuilder.AddNoEditPermissionWarning(this.infobar, this.distributionList, base.IsPreviewForm);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000F0D9D File Offset: 0x000EEF9D
		protected void RenderDisplayName()
		{
			Utilities.HtmlEncode(this.distributionList.DisplayName, base.Response.Output);
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000F0DBA File Offset: 0x000EEFBA
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x000F0DC2 File Offset: 0x000EEFC2
		protected static int StoreObjectTypeDistributionList
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000F0DC8 File Offset: 0x000EEFC8
		protected void RenderMemberList()
		{
			foreach (DistributionListMember distributionListMember in this.distributionList)
			{
				if (!(distributionListMember.Participant == null))
				{
					Participant participant = distributionListMember.Participant;
					base.Response.Write("<tr><td class='adDlMmbrsLft'>");
					string itemType = null;
					string itemId = null;
					string emailAddress = participant.EmailAddress;
					string smtpAddress = Utilities.GetParticipantProperty<string>(participant, ParticipantSchema.SmtpAddress, null);
					AddressOrigin addressOrigin = AddressOrigin.Unknown;
					StoreParticipantOrigin storeParticipantOrigin = participant.Origin as StoreParticipantOrigin;
					bool flag = distributionListMember.MainEntryId is ADParticipantEntryId;
					if (storeParticipantOrigin != null)
					{
						if (string.CompareOrdinal(participant.RoutingType, "MAPIPDL") == 0)
						{
							base.UserContext.RenderThemeImage(base.Response.Output, ThemeFileId.DistributionListOther);
							itemType = "IPM.DistList";
						}
						else
						{
							base.UserContext.RenderThemeImage(base.Response.Output, ThemeFileId.Contact);
							itemType = "IPM.Contact";
						}
						itemId = storeParticipantOrigin.OriginItemId.ToBase64String();
						addressOrigin = AddressOrigin.Store;
					}
					else if (flag)
					{
						bool participantProperty = Utilities.GetParticipantProperty<bool>(participant, ParticipantSchema.IsDistributionList, false);
						if (participantProperty)
						{
							base.UserContext.RenderThemeImage(base.Response.Output, ThemeFileId.DistributionListOther);
							itemType = "ADDistList";
						}
						else
						{
							base.UserContext.RenderThemeImage(base.Response.Output, ThemeFileId.DistributionListUser);
							itemType = "AD.RecipientType.User";
						}
						addressOrigin = AddressOrigin.Directory;
					}
					else
					{
						base.UserContext.RenderThemeImage(base.Response.Output, ThemeFileId.DistributionListUser);
					}
					if (string.CompareOrdinal(participant.RoutingType, "EX") == 0)
					{
						ADRawEntry adrawEntry = this.FindRecipientInAD(participant.EmailAddress);
						if (adrawEntry != null)
						{
							smtpAddress = ((SmtpAddress)adrawEntry[ADRecipientSchema.PrimarySmtpAddress]).ToString();
							if (flag)
							{
								itemId = Utilities.GetBase64StringFromADObjectId((ADObjectId)adrawEntry[ADObjectSchema.Id]);
							}
						}
					}
					base.Response.Write("</td><td class=\"pdlNm adDlMmbrsRt\">");
					if (!string.IsNullOrEmpty(participant.DisplayName))
					{
						this.RenderMemberNameLink(itemType, itemId, participant.DisplayName);
					}
					base.Response.Write("</td><td class=adDlMmbrsRt>");
					if (!string.IsNullOrEmpty(emailAddress))
					{
						this.RenderMemberEmailLink(emailAddress, participant.DisplayName, smtpAddress, participant.RoutingType, addressOrigin, (storeParticipantOrigin != null) ? storeParticipantOrigin.OriginItemId : null, (storeParticipantOrigin != null) ? storeParticipantOrigin.EmailAddressIndex : EmailAddressIndex.None);
					}
					base.Response.Write("</td></tr>");
				}
			}
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000F1054 File Offset: 0x000EF254
		private void RenderMemberNameLink(string itemType, string itemId, string displayName)
		{
			if (itemType != null && itemId != null)
			{
				string handlerCode = string.Format("openItmRdFm(\"{0}\",\"{1}\");", Utilities.JavascriptEncode(itemType), Utilities.JavascriptEncode(itemId));
				base.Response.Write("<a class=lnk ");
				Utilities.RenderScriptHandler(base.Response.Output, "onclick", handlerCode);
				base.Response.Write(" title=\"");
				Utilities.HtmlEncode(displayName, base.Response.Output);
				base.Response.Write("\">");
				Utilities.HtmlEncode(displayName, base.Response.Output);
				base.Response.Write("</a>");
				return;
			}
			Utilities.HtmlEncode(displayName, base.Response.Output);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000F1110 File Offset: 0x000EF310
		private void RenderMemberEmailLink(string routingAddress, string displayName, string smtpAddress, string routingType, AddressOrigin addressOrigin, StoreObjectId storeObjectId, EmailAddressIndex emailAddressIndex)
		{
			StringBuilder stringBuilder = new StringBuilder("openItm(\"?ae=Item&t=IPM.Note&a=New&to=");
			stringBuilder.Append(Utilities.JavascriptEncode(Utilities.HtmlEncode(Utilities.UrlEncode(routingAddress))));
			stringBuilder.Append("&nm=");
			stringBuilder.Append(Utilities.JavascriptEncode(Utilities.HtmlEncode(Utilities.UrlEncode(displayName))));
			stringBuilder.Append("&rt=");
			stringBuilder.Append(Utilities.JavascriptEncode(Utilities.HtmlEncode(Utilities.UrlEncode(routingType))));
			if (addressOrigin != AddressOrigin.Unknown)
			{
				stringBuilder.Append("&ao=");
				stringBuilder.Append((int)addressOrigin);
			}
			if (storeObjectId != null)
			{
				stringBuilder.Append("&stId=");
				stringBuilder.Append(Utilities.JavascriptEncode(Utilities.HtmlEncode(Utilities.UrlEncode(storeObjectId.ToBase64String()))));
				if (!Utilities.IsMapiPDL(routingType))
				{
					stringBuilder.Append("&ei=");
					stringBuilder.Append((int)emailAddressIndex);
				}
			}
			string text = smtpAddress ?? routingAddress;
			if (Utilities.IsCustomRoutingType(text, routingType))
			{
				text = string.Concat(new string[]
				{
					"[",
					routingType,
					": ",
					text,
					"]"
				});
			}
			stringBuilder.Append("\");");
			base.Response.Write("<a class=lnk ");
			Utilities.RenderScriptHandler(base.Response.Output, "onclick", stringBuilder.ToString());
			base.Response.Write(" title=\"");
			Utilities.HtmlEncode(text, base.Response.Output);
			base.Response.Write("\">");
			Utilities.HtmlEncode(text, base.Response.Output);
			base.Response.Write("</a>");
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000F12B3 File Offset: 0x000EF4B3
		protected void RenderNotes()
		{
			BodyConversionUtilities.RenderMeetingPlainTextBody(base.Response.Output, base.Item, base.UserContext, false);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000F12D3 File Offset: 0x000EF4D3
		private ADRawEntry FindRecipientInAD(string distinguishedName)
		{
			if (this.adRecipientSession == null)
			{
				this.adRecipientSession = Utilities.CreateADRecipientSession(Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, true, base.UserContext);
			}
			return Utilities.GetAdRecipientByLegacyExchangeDN(this.adRecipientSession, distinguishedName);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000F1308 File Offset: 0x000EF508
		protected void RenderToolbar(TextWriter writer)
		{
			Toolbar toolbar = new ReadDistributionListToolbar(base.IsInDeleteItems, this.distributionList);
			toolbar.Render(writer);
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x000F132E File Offset: 0x000EF52E
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, base.Item);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x000F1341 File Offset: 0x000EF541
		protected void RenderCategories()
		{
			if (base.Item != null)
			{
				CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
			}
		}

		// Token: 0x04001CC0 RID: 7360
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

		// Token: 0x04001CC1 RID: 7361
		private DistributionList distributionList;

		// Token: 0x04001CC2 RID: 7362
		private IRecipientSession adRecipientSession;

		// Token: 0x04001CC3 RID: 7363
		private Infobar infobar = new Infobar();
	}
}
