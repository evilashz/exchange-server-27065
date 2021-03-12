using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x020000A5 RID: 165
	public class ReadDistributionList : ReadContactBase
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x0002FCAB File Offset: 0x0002DEAB
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.distributionList = base.Initialize<DistributionList>(new PropertyDefinition[0]);
			base.Module = Navigation.GetNavigationModuleFromFolder(base.UserContext, base.FolderStoreObjectId);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0002FCDD File Offset: 0x0002DEDD
		protected string DisplayName
		{
			get
			{
				if (Utilities.WhiteSpaceOnlyOrNullEmpty(this.distributionList.DisplayName))
				{
					return LocalizedStrings.GetNonEncoded(-808148510);
				}
				return this.distributionList.DisplayName;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0002FD07 File Offset: 0x0002DF07
		protected string ItemIdString
		{
			get
			{
				return base.ItemId.ToBase64String();
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0002FD14 File Offset: 0x0002DF14
		protected void RenderDetailsBucket(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"pDtls\">");
			writer.Write("<tr><td colspan=2 class=\"hd lp\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-905993889));
			writer.Write("</td></tr>");
			bool flag = false;
			foreach (DistributionListMember distributionListMember in this.distributionList)
			{
				if (!(distributionListMember.Participant == null))
				{
					Participant participant = distributionListMember.Participant;
					writer.Write("<tr><td class=\"lbl lp\" nowrap>");
					if (!flag)
					{
						writer.Write(LocalizedStrings.GetHtmlEncoded(1099536643));
						flag = true;
					}
					writer.Write("</td>");
					string routingType;
					if ((routingType = participant.RoutingType) == null)
					{
						goto IL_E7;
					}
					if (!(routingType == "MAPIPDL"))
					{
						if (!(routingType == "EX"))
						{
							if (!(routingType == "SMTP"))
							{
								goto IL_E7;
							}
							goto IL_E7;
						}
						else
						{
							this.RenderADParticipant(writer, participant);
						}
					}
					else
					{
						this.RenderPDLParticipant(writer, participant);
					}
					IL_EF:
					writer.Write("</tr>");
					continue;
					IL_E7:
					this.RenderSmtpParticipant(writer, participant);
					goto IL_EF;
				}
			}
			writer.Write("</table>");
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0002FE50 File Offset: 0x0002E050
		private void RenderADParticipant(TextWriter writer, Participant participant)
		{
			if (this.adRecipientSession == null)
			{
				this.adRecipientSession = Utilities.CreateADRecipientSession(Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, true, base.UserContext);
			}
			ADRecipient recipientByLegacyExchangeDN = Utilities.GetRecipientByLegacyExchangeDN(this.adRecipientSession, participant.EmailAddress);
			bool flag = recipientByLegacyExchangeDN is IADDistributionList;
			if (recipientByLegacyExchangeDN != null && (flag || recipientByLegacyExchangeDN is IADOrgPerson))
			{
				this.RenderMemberWithIcon(writer, participant.DisplayName, Utilities.GetBase64StringFromADObjectId(recipientByLegacyExchangeDN.Id), flag ? ListViewContents.ReadItemType.AdDistributionList : ListViewContents.ReadItemType.AdOrgPerson, flag ? ThemeFileId.DistributionListOther : ThemeFileId.AddressBook, flag);
				return;
			}
			writer.Write("<td class=\"txvl pdlncn\">");
			Utilities.HtmlEncode(participant.DisplayName, writer);
			writer.Write("</td>");
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0002FEFC File Offset: 0x0002E0FC
		private void RenderPDLParticipant(TextWriter writer, Participant participant)
		{
			StoreParticipantOrigin storeParticipantOrigin = participant.Origin as StoreParticipantOrigin;
			if (storeParticipantOrigin != null)
			{
				StoreObjectId originItemId = storeParticipantOrigin.OriginItemId;
				this.RenderMemberWithIcon(writer, participant.DisplayName, originItemId.ToBase64String(), ListViewContents.ReadItemType.ContactDistributionList, ThemeFileId.DistributionListOther, true);
				return;
			}
			writer.Write("<td class=\"txvl pdlncn\"><img src=\"");
			base.UserContext.RenderThemeFileUrl(writer, ThemeFileId.DistributionListOther);
			writer.Write("\"><b>");
			Utilities.HtmlEncode(participant.DisplayName, writer);
			writer.Write("</b></td>");
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0002FF78 File Offset: 0x0002E178
		private void RenderSmtpParticipant(TextWriter writer, Participant participant)
		{
			StoreParticipantOrigin storeParticipantOrigin = participant.Origin as StoreParticipantOrigin;
			if (storeParticipantOrigin != null)
			{
				StoreObjectId originItemId = storeParticipantOrigin.OriginItemId;
				this.RenderMemberWithIcon(writer, participant.DisplayName, originItemId.ToBase64String(), ListViewContents.ReadItemType.Contact, ThemeFileId.Contact, false);
				return;
			}
			writer.Write("<td class=\"txvl pdlncn\">");
			base.RenderEmail(writer, participant.DisplayName, participant.EmailAddress, participant.RoutingType, null, EmailAddressIndex.None);
			writer.Write("</td>");
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0002FFE4 File Offset: 0x0002E1E4
		private void RenderMemberWithIcon(TextWriter writer, string name, string id, ListViewContents.ReadItemType type, ThemeFileId themeFileId, bool isBold)
		{
			writer.Write("<td class=\"txvl phtdpd\"><a href=\"#\" id=\"");
			Utilities.HtmlEncode(id, writer);
			writer.Write("\" onClick=\"return onClkRcpt(this,");
			int num = (int)type;
			writer.Write(num.ToString());
			writer.Write(");\" class=\"map\"><img src=\"");
			base.UserContext.RenderThemeFileUrl(writer, themeFileId);
			writer.Write("\">");
			if (isBold)
			{
				writer.Write("<b>");
			}
			Utilities.HtmlEncode(name, writer);
			if (isBold)
			{
				writer.Write("</b>");
			}
			writer.Write("</a></td>");
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00030072 File Offset: 0x0002E272
		protected void RenderProfileBucket(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"pAddr\">");
			base.RenderNotes(writer);
			writer.Write("</table>");
		}

		// Token: 0x0400045F RID: 1119
		private DistributionList distributionList;

		// Token: 0x04000460 RID: 1120
		private IRecipientSession adRecipientSession;
	}
}
