using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000413 RID: 1043
	public class SharedCalendarItemInfobar
	{
		// Token: 0x0600258F RID: 9615 RVA: 0x000D943C File Offset: 0x000D763C
		internal SharedCalendarItemInfobar(UserContext userContext, CalendarFolder folder, int colorIndex, bool renderNotifyForOtherUser)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			this.userContext = userContext;
			this.folder = folder;
			this.colorIndex = colorIndex;
			this.renderNotifyForOtherUser = renderNotifyForOtherUser;
			this.isSharedOutFolder = Utilities.IsFolderSharedOut(folder);
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromStoreObject(folder);
			if (owaStoreObjectId.GetSession(userContext) is MailboxSession)
			{
				this.folderEncodedDisplayName = Utilities.SanitizeHtmlEncode(string.Format(LocalizedStrings.GetNonEncoded(-83764036), folder.DisplayName, Utilities.GetMailboxOwnerDisplayName((MailboxSession)owaStoreObjectId.GetSession(userContext))));
			}
			else
			{
				this.folderEncodedDisplayName = Utilities.SanitizeHtmlEncode(folder.DisplayName);
			}
			this.isSharedFolder = (owaStoreObjectId != null && owaStoreObjectId.IsOtherMailbox);
			this.isPublicFolder = owaStoreObjectId.IsPublic;
			if (this.isSharedFolder)
			{
				this.folderOwnerEncodedName = Utilities.SanitizeHtmlEncode(Utilities.GetFolderOwnerExchangePrincipal(owaStoreObjectId, userContext).MailboxInfo.DisplayName);
			}
			this.containerClass = folder.GetValueOrDefault<string>(StoreObjectSchema.ContainerClass, "IPF.Appointment");
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000D9548 File Offset: 0x000D7748
		internal void Build(Infobar infobar)
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<div id=divSFIB ");
			if (this.colorIndex != -2)
			{
				sanitizingStringBuilder.Append("class=bcal");
				sanitizingStringBuilder.Append<int>(CalendarColorManager.GetClientColorIndex(this.colorIndex));
			}
			else
			{
				sanitizingStringBuilder.Append("class=calNoClr");
			}
			sanitizingStringBuilder.Append(">");
			string str = this.userContext.IsRtl ? "<div class=\"fltRight\"" : "<div class=\"fltLeft\"";
			sanitizingStringBuilder.Append(str);
			if (this.isSharedFolder && this.renderNotifyForOtherUser)
			{
				sanitizingStringBuilder.Append("><input type=\"checkbox\" id=\"chkNtfy\"></div>");
				sanitizingStringBuilder.Append(str);
				sanitizingStringBuilder.Append(" id=\"divNtfy\"><label for=\"chkNtfy\">");
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(816646619));
				sanitizingStringBuilder.Append(" ");
				sanitizingStringBuilder.Append<SanitizedHtmlString>(this.folderOwnerEncodedName);
				sanitizingStringBuilder.Append("</label></div>");
				str = (this.userContext.IsRtl ? "<div class=\"fltLeft\"" : "<div class=\"fltRight\"");
				sanitizingStringBuilder.Append(str);
			}
			if (!this.isSharedFolder || !this.renderNotifyForOtherUser)
			{
				sanitizingStringBuilder.Append(" id=\"divShrType\">");
				this.BuildFolderType(sanitizingStringBuilder);
			}
			else
			{
				sanitizingStringBuilder.Append(" id=\"divShrName\">");
				this.BuildFolderName(sanitizingStringBuilder);
			}
			sanitizingStringBuilder.Append(this.userContext.DirectionMark);
			sanitizingStringBuilder.Append("</div>");
			sanitizingStringBuilder.Append(str);
			sanitizingStringBuilder.Append(" id=\"divShrImg\">");
			this.BuildIcon(sanitizingStringBuilder);
			sanitizingStringBuilder.Append(this.userContext.DirectionMark);
			sanitizingStringBuilder.Append("</div>");
			sanitizingStringBuilder.Append(str);
			if (!this.isSharedFolder || !this.renderNotifyForOtherUser)
			{
				sanitizingStringBuilder.Append(" id=\"divShrName\">");
				this.BuildFolderName(sanitizingStringBuilder);
			}
			else
			{
				sanitizingStringBuilder.Append(" id=\"divShrType\">");
				this.BuildFolderType(sanitizingStringBuilder);
			}
			sanitizingStringBuilder.Append("</div></div>");
			infobar.AddMessage(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>(), InfobarMessageType.Informational, "divCalendarInfobarMessage");
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000D9728 File Offset: 0x000D7928
		private void BuildFolderType(SanitizingStringBuilder<OwaHtml> stringBuilder)
		{
			if (this.isSharedFolder || this.isSharedOutFolder)
			{
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(1379417169));
				return;
			}
			if (this.isPublicFolder)
			{
				stringBuilder.Append(LocalizedStrings.GetNonEncoded(16167073));
				return;
			}
			stringBuilder.Append(LocalizedStrings.GetNonEncoded(185425884));
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000D9780 File Offset: 0x000D7980
		private void BuildIcon(SanitizingStringBuilder<OwaHtml> stringBuilder)
		{
			FolderSharingFlag sharingFlag = FolderSharingFlag.None;
			if (this.isSharedFolder || this.isPublicFolder)
			{
				sharingFlag = FolderSharingFlag.SharedIn;
			}
			else if (this.isSharedOutFolder)
			{
				sharingFlag = FolderSharingFlag.SharedOut;
			}
			using (SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>())
			{
				SmallIconManager.RenderFolderIcon(sanitizingStringWriter, this.userContext, this.containerClass, sharingFlag, false, new string[0]);
				stringBuilder.Append<SanitizedHtmlString>(sanitizingStringWriter.ToSanitizedString<SanitizedHtmlString>());
			}
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x000D97F8 File Offset: 0x000D79F8
		private void BuildFolderName(SanitizingStringBuilder<OwaHtml> stringBuilder)
		{
			if (!this.isSharedFolder)
			{
				stringBuilder.Append<SanitizedHtmlString>(this.folderEncodedDisplayName);
				return;
			}
			if (Utilities.IsDefaultFolder(this.folder, DefaultFolderType.Calendar))
			{
				stringBuilder.Append<SanitizedHtmlString>(this.folderOwnerEncodedName);
				return;
			}
			string htmlEncoded = LocalizedStrings.GetHtmlEncoded(-881877772);
			stringBuilder.Append<SanitizedHtmlString>(SanitizedHtmlString.Format(this.userContext.UserCulture, htmlEncoded, new object[]
			{
				this.folderEncodedDisplayName,
				this.folderOwnerEncodedName
			}));
		}

		// Token: 0x040019FA RID: 6650
		private CalendarFolder folder;

		// Token: 0x040019FB RID: 6651
		private UserContext userContext;

		// Token: 0x040019FC RID: 6652
		private int colorIndex;

		// Token: 0x040019FD RID: 6653
		private bool renderNotifyForOtherUser;

		// Token: 0x040019FE RID: 6654
		private SanitizedHtmlString folderEncodedDisplayName;

		// Token: 0x040019FF RID: 6655
		private bool isSharedOutFolder;

		// Token: 0x04001A00 RID: 6656
		private bool isSharedFolder;

		// Token: 0x04001A01 RID: 6657
		private bool isPublicFolder;

		// Token: 0x04001A02 RID: 6658
		private SanitizedHtmlString folderOwnerEncodedName;

		// Token: 0x04001A03 RID: 6659
		private string containerClass;
	}
}
