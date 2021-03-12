using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000417 RID: 1047
	public class SharingMessageWriter
	{
		// Token: 0x0600259B RID: 9627 RVA: 0x000D99BA File Offset: 0x000D7BBA
		internal SharingMessageWriter(SharingMessageItem sharingMessage, UserContext userContext)
		{
			if (sharingMessage == null)
			{
				throw new ArgumentNullException("sharingMessage");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.sharingMessage = sharingMessage;
			this.userContext = userContext;
			this.Initialize();
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000D99F4 File Offset: 0x000D7BF4
		public void RenderSharingSetting(TextWriter writer, bool isReadonly)
		{
			if (this.sharingMessage.IsDraft && (this.sharingMessage.SharingMessageType == SharingMessageType.AcceptOfRequest || this.sharingMessage.SharingMessageType.IsInvitationOrRequest))
			{
				writer.Write("<div>");
				this.RenderSettingsStart(writer, isReadonly, "divShareSetting", 14478983, ThemeFileId.ExchangeCalendar);
				bool isReadOnly = isReadonly || this.sharingMessage.SharingMessageType == SharingMessageType.Request;
				if (this.sharingMessage.IsSharedFolderPrimary)
				{
					this.RenderSharingOptionItem(writer, isReadOnly, "optFb", SharingLevel.FreeBusy, 1482647983);
					this.RenderSharingOptionItem(writer, isReadOnly, "optLimited", SharingLevel.Limited, 535805963);
					this.RenderSharingOptionItem(writer, isReadOnly, "optDetailsReviewer", SharingLevel.FullDetailsReviewer, 1827193016);
				}
				else
				{
					this.RenderSharingOptionItem(writer, isReadOnly, "optDetailsReviewer", SharingLevel.FullDetailsReviewer, 1827193016);
					this.RenderSharingOptionItem(writer, isReadOnly, "optDetailsEditor", SharingLevel.FullDetailsEditor, -2146599836);
					string value = (this.shareLevel == SharingLevel.FullDetailsEditor && !isReadonly) ? "shareEditorNote" : "shareEditorNote grayTxt";
					writer.Write("<div id=\"divShareEditorNote\" class=\"");
					writer.Write(value);
					writer.Write("\">");
					writer.Write(SanitizedHtmlString.FromStringId(75400189));
					writer.Write("</div>");
				}
				if (this.sharingMessage.IsSharedFolderPrimary && this.sharingMessage.SharingMessageType.IsInvitationOrRequest)
				{
					writer.Write("<div class=\"shareRequestOption\">");
					writer.Write("<input type=\"checkbox\" id=\"chkReqPerm\"");
					if (this.sharingMessage.SharingMessageType.IsRequest)
					{
						writer.Write(" checked");
					}
					if (isReadonly)
					{
						writer.Write(" disabled");
					}
					writer.Write("><label");
					if (isReadonly)
					{
						writer.Write(" class=\"grayTxt\"");
					}
					writer.Write(" for=\"chkReqPerm\">");
					writer.Write(SanitizedHtmlString.FromStringId(1703875658));
					writer.Write("</label>");
					writer.Write("</div>");
				}
				writer.Write("</div>");
				this.RenderSettingsEnd(writer);
			}
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000D9BF4 File Offset: 0x000D7DF4
		public void RenderPublishLinks(TextWriter writer, bool isReadonly)
		{
			if (this.sharingMessage.IsDraft)
			{
				this.RenderSettingsStart(writer, isReadonly, "divPublishSetting", -836489933, ThemeFileId.WebCalendarBig);
				writer.Write("<div class=\"shareOptionItem\">");
				if (this.sharingMessage.IsSharedFolderPrimary)
				{
					writer.Write(SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(2104451722), new object[]
					{
						this.sharingMessage.InitiatorName
					}));
				}
				else
				{
					writer.Write(SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(-1568709809), new object[]
					{
						this.sharingMessage.InitiatorName,
						this.sharingMessage.SharedFolderName
					}));
				}
				writer.Write("</div>");
				this.RenderPublishItem(writer, "divViewAddrL", 1987785664, this.sharingMessage.BrowseUrl);
				this.RenderPublishItem(writer, "divSubscribeAddrL", -1797397441, this.sharingMessage.ICalUrl);
				this.RenderSettingsEnd(writer);
			}
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000D9CF0 File Offset: 0x000D7EF0
		private void RenderPublishItem(TextWriter writer, string divId, Strings.IDs labelStringId, object value)
		{
			writer.Write("<div class=\"shareOptionItem\">");
			writer.Write("<div id=\"");
			writer.Write(divId);
			writer.Write("\" class=\"fltBefore\">");
			writer.Write(SanitizedHtmlString.FromStringId(labelStringId));
			writer.Write("</div>");
			writer.Write("<div class=\"publishLink\">");
			if (value != null)
			{
				string value2 = value.ToString();
				writer.Write("<a target=\"_blank\" class=\"publishUrl\" href=\"");
				writer.Write(value2);
				writer.Write("\" title=\"");
				writer.Write(value2);
				writer.Write("\">");
				writer.Write(value2);
				writer.Write("</a>");
			}
			writer.Write("</div></div>");
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000D9DA0 File Offset: 0x000D7FA0
		private void RenderSettingsStart(TextWriter writer, bool isReadonly, string domId, Strings.IDs labelId, ThemeFileId iconId)
		{
			writer.Write("<div id=\"");
			writer.Write(domId);
			writer.Write("\" class=\"");
			if (isReadonly)
			{
				writer.Write("shareSettingRO");
			}
			else
			{
				writer.Write("shareSetting");
			}
			writer.Write("\"><div class=\"fltBefore\"><span class=\"shareLabel\">");
			writer.Write(SanitizedHtmlString.FromStringId(labelId));
			writer.Write("</span>");
			this.userContext.RenderThemeImage(writer, iconId);
			writer.Write("</div><div class=\"shareOption\">");
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000D9E21 File Offset: 0x000D8021
		private void RenderSettingsEnd(TextWriter writer)
		{
			writer.Write("</div></div>");
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000D9E30 File Offset: 0x000D8030
		public void RenderFolderInformation(TextWriter writer)
		{
			if (!this.sharingMessage.IsDraft && this.sharingMessage.SharingMessageType.IsInvitationOrAcceptOfRequest)
			{
				writer.Write("<div id=\"divShareInfo\"><div id=\"divShareIcon\" class=\"fltBefore\">");
				this.userContext.RenderThemeImage(writer, this.sharingMessage.IsPublishing ? ThemeFileId.WebCalendarBig : ThemeFileId.ExchangeCalendar);
				writer.Write("</div><div id=\"divShareName\">");
				if (this.sharingMessage.IsPublishing)
				{
					writer.Write(new SanitizedHtmlString(this.sharingMessage.SharedFolderName));
				}
				else
				{
					writer.Write(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-83764036), new object[]
					{
						this.sharingMessage.InitiatorName,
						this.sharingMessage.SharedFolderName
					}));
				}
				writer.Write("</div><div id=\"divShareType\">");
				writer.Write(SanitizedHtmlString.FromStringId(392101185));
				writer.Write("</div></div>");
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000D9F24 File Offset: 0x000D8124
		public void AddSharingInfoToInfobar(Infobar infobar)
		{
			if (this.disableSharingButton)
			{
				return;
			}
			ExDateTime? sharingLastSubscribeTime = this.sharingMessage.SharingLastSubscribeTime;
			if (sharingLastSubscribeTime != null)
			{
				infobar.AddMessage(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(388258761), new object[]
				{
					sharingLastSubscribeTime.Value.ToString(this.userContext.UserOptions.DateFormat),
					sharingLastSubscribeTime.Value.ToString(this.userContext.UserOptions.TimeFormat)
				}), InfobarMessageType.Informational, "divInfoAddCal");
			}
			if (this.sharingMessage.SharingMessageType.IsResponseToRequest)
			{
				this.AddResponseMessageInfobarToResponseMessage(infobar);
				return;
			}
			if (this.sharingMessage.SharingMessageType.IsInvitationOrRequest)
			{
				if (this.sharingMessage.SharingResponseType == SharingResponseType.None)
				{
					Strings.IDs localizedID;
					if (this.sharingMessage.SharingMessageType == SharingMessageType.InvitationAndRequest)
					{
						localizedID = 910454547;
					}
					else if (this.sharingMessage.SharingMessageType == SharingMessageType.Invitation)
					{
						if (!this.sharingMessage.IsPublishing)
						{
							localizedID = 176723821;
						}
						else if (CalendarUtilities.CanSubscribeInternetCalendar())
						{
							if (string.IsNullOrEmpty(this.sharingMessage.BrowseUrl))
							{
								localizedID = 1455056454;
							}
							else
							{
								localizedID = 1017528788;
							}
						}
						else
						{
							localizedID = -464342883;
						}
					}
					else
					{
						localizedID = -2085372735;
					}
					infobar.AddMessage(SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(localizedID), new object[]
					{
						this.sharingMessage.InitiatorName
					}), InfobarMessageType.Informational);
					return;
				}
				this.AddResponseMessageInfobarToRequestMessage(infobar);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000DA0A1 File Offset: 0x000D82A1
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x000DA0A9 File Offset: 0x000D82A9
		public Toolbar SharingToolbar { get; private set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x000DA0B2 File Offset: 0x000D82B2
		public bool ShouldShowSharingToolbar
		{
			get
			{
				return !this.sharingMessage.IsDraft && (this.sharingMessage.SharingMessageType == SharingMessageType.AcceptOfRequest || this.sharingMessage.SharingMessageType.IsInvitationOrRequest);
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000DA0E8 File Offset: 0x000D82E8
		private SharingLevel GetSharingLevel()
		{
			if (this.sharingMessage.SharingMessageType.IsInvitationOrAcceptOfRequest)
			{
				if (!this.sharingMessage.IsSharedFolderPrimary)
				{
					return this.GetSharingLevelByPermissionSetting();
				}
				switch (this.sharingMessage.SharingDetail)
				{
				case SharingContextDetailLevel.AvailabilityOnly:
					return SharingLevel.FreeBusy;
				case SharingContextDetailLevel.Limited:
					return SharingLevel.Limited;
				case SharingContextDetailLevel.FullDetails:
					return SharingLevel.FullDetailsReviewer;
				}
			}
			return SharingLevel.None;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000DA148 File Offset: 0x000D8348
		private SharingLevel GetSharingLevelByPermissionSetting()
		{
			SharingContextPermissions sharingPermissions = this.sharingMessage.SharingPermissions;
			if (sharingPermissions == SharingContextPermissions.Reviewer)
			{
				return SharingLevel.FullDetailsReviewer;
			}
			if (sharingPermissions != SharingContextPermissions.Editor)
			{
				return SharingLevel.None;
			}
			return SharingLevel.FullDetailsEditor;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000DA174 File Offset: 0x000D8374
		private void RenderSharingOptionItem(TextWriter writer, bool isReadOnly, string radioId, SharingLevel level, Strings.IDs labelStringId)
		{
			writer.Write("<div class=\"shareOptionItem\">");
			writer.Write("<input type=\"radio\" name=\"shOp\" id=\"");
			writer.Write(radioId);
			writer.Write("\" value=\"");
			writer.Write((int)level);
			writer.Write("\"");
			if (this.shareLevel == level)
			{
				writer.Write(" checked");
			}
			if (isReadOnly)
			{
				writer.Write(" disabled");
			}
			writer.Write("><label");
			if (isReadOnly)
			{
				writer.Write(" class=\"grayTxt\"");
			}
			writer.Write(" for=\"");
			writer.Write(radioId);
			writer.Write("\">");
			writer.Write(SanitizedHtmlString.FromStringId(labelStringId));
			writer.Write("</label>");
			writer.Write("</div>");
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000DA238 File Offset: 0x000D8438
		private void Initialize()
		{
			this.shareLevel = this.GetSharingLevel();
			this.disableSharingButton = (this.userContext.IsInOtherMailbox(this.sharingMessage) || Utilities.IsWebPartDelegateAccessRequest(OwaContext.Current) || Utilities.IsItemInDefaultFolder(this.sharingMessage, DefaultFolderType.SentItems) || Utilities.IsPublic(this.sharingMessage) || this.sharingMessage.IsDraft);
			this.disableSubscribeButton = this.disableSharingButton;
			if (this.sharingMessage.IsPublishing)
			{
				this.SharingToolbar = new ReadPublishInvitionMessageToolbar(string.IsNullOrEmpty(this.sharingMessage.BrowseUrl), this.disableSubscribeButton);
				return;
			}
			this.SharingToolbar = new ReadSharingMessageToolbar(this.sharingMessage.SharingMessageType, this.disableSharingButton);
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000DA2F8 File Offset: 0x000D84F8
		private void AddResponseMessageInfobarToResponseMessage(Infobar infobar)
		{
			Strings.IDs? ds = null;
			if (this.sharingMessage.SharingMessageType == SharingMessageType.AcceptOfRequest)
			{
				ds = new Strings.IDs?(456591501);
			}
			else if (this.sharingMessage.SharingMessageType == SharingMessageType.DenyOfRequest)
			{
				ds = new Strings.IDs?(-417268086);
			}
			if (ds != null)
			{
				string text = this.sharingMessage.Sender.DisplayName;
				if (string.IsNullOrEmpty(text))
				{
					text = (this.sharingMessage.Sender.TryGetProperty(ParticipantSchema.SmtpAddress) as string);
				}
				SanitizedHtmlString messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(ds.Value), new object[]
				{
					text
				});
				infobar.AddMessage(messageHtml, InfobarMessageType.Informational);
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000DA3B0 File Offset: 0x000D85B0
		private void AddResponseMessageInfobarToRequestMessage(Infobar infobar)
		{
			Strings.IDs? ds = null;
			switch (this.sharingMessage.SharingResponseType)
			{
			case SharingResponseType.Allowed:
				ds = new Strings.IDs?(-310218305);
				break;
			case SharingResponseType.Denied:
				ds = new Strings.IDs?(1130758124);
				break;
			}
			if (ds != null)
			{
				ExDateTime? sharingResponseTime = this.sharingMessage.SharingResponseTime;
				string text = string.Empty;
				string text2 = string.Empty;
				if (sharingResponseTime != null)
				{
					text = sharingResponseTime.Value.ToString(this.userContext.UserOptions.DateFormat);
					text2 = sharingResponseTime.Value.ToString(this.userContext.UserOptions.TimeFormat);
				}
				SanitizedHtmlString messageHtml = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(ds.Value), new object[]
				{
					text,
					text2
				});
				infobar.AddMessage(messageHtml, InfobarMessageType.Informational);
			}
		}

		// Token: 0x04001A04 RID: 6660
		private SharingMessageItem sharingMessage;

		// Token: 0x04001A05 RID: 6661
		private UserContext userContext;

		// Token: 0x04001A06 RID: 6662
		private SharingLevel shareLevel;

		// Token: 0x04001A07 RID: 6663
		private bool disableSharingButton;

		// Token: 0x04001A08 RID: 6664
		private bool disableSubscribeButton;
	}
}
