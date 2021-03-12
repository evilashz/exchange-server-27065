using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000041 RID: 65
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WelcomeMessageBodyData
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		public WelcomeMessageBodyData(WelcomeToGroupMessageTemplate template, string joinHeaderMessage, bool isMailEnabledUser, bool isAddingUserDifferent, CultureInfo cultureInfo)
		{
			ArgumentValidator.ThrowIfNull("template", template);
			ArgumentValidator.ThrowIfNullOrEmpty("joiningHeaderMessage", joinHeaderMessage);
			ArgumentValidator.ThrowIfNull("cultureInfo", cultureInfo);
			this.groupDisplayName = template.EncodedGroupDisplayName;
			this.groupDescription = template.EncodedGroupDescription;
			this.joiningHeaderMessage = joinHeaderMessage;
			this.inboxUrl = template.GroupInboxUrl;
			this.calendarUrl = template.GroupCalendarUrl;
			this.sharePointUrl = template.GroupSharePointUrl;
			this.subscribeUrl = template.SubscribeUrl;
			this.unsubscribeUrl = template.UnsubscribeUrl;
			this.groupSmtpAddress = template.Group.PrimarySmtpAddress.ToString();
			this.showExchangeLinks = !isMailEnabledUser;
			this.isAddedByDifferentUser = isAddingUserDifferent;
			this.isGroupAutoSubscribed = template.GroupIsAutoSubscribe;
			this.isSharePointEnabled = !string.IsNullOrEmpty(template.GroupSharePointUrl);
			this.cultureInfo = cultureInfo;
			this.executingUserHasPhoto = (template.ExecutingUserPhoto != null);
			this.executingUserPhotoId = ((template.ExecutingUserPhoto != null) ? template.ExecutingUserPhoto.ImageId : WelcomeMessageBodyData.BlankGifImage.ImageId);
			this.groupHasPhoto = (template.GroupPhoto != null);
			this.groupPhotoId = ((template.GroupPhoto != null) ? template.GroupPhoto.ImageId : WelcomeMessageBodyData.BlankGifImage.ImageId);
			this.groupType = template.Group.ModernGroupType;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000D31C File Offset: 0x0000B51C
		public string GroupTitleHeading
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailHeader(this.groupDisplayName).ToString(this.cultureInfo);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000D344 File Offset: 0x0000B544
		public string GroupDescription
		{
			get
			{
				if (string.IsNullOrEmpty(this.groupDescription))
				{
					return ClientStrings.GroupMailboxWelcomeEmailDefaultDescription.ToString(this.cultureInfo);
				}
				return this.groupDescription;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000D378 File Offset: 0x0000B578
		public string JoiningHeaderMessage
		{
			get
			{
				return this.joiningHeaderMessage;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000D380 File Offset: 0x0000B580
		public string SubscribeActivityToInboxLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailSubscribeToInboxTitle.ToString(this.cultureInfo);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public string SubscribeActivityToInboxDescriptionLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailSubscribeToInboxSubtitle.ToString(this.cultureInfo);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		public string StartConversationLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailStartConversationTitle.ToString(this.cultureInfo);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public string StartConversationEmail
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailStartConversationSubtitle(this.GroupSmtpAddress).ToString(this.cultureInfo);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000D408 File Offset: 0x0000B608
		public string GroupDocumentsMainTitleHeaderLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailShareFilesTitle.ToString(this.cultureInfo);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000D428 File Offset: 0x0000B628
		public string GroupDocumentsMainDescriptionHeaderLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailShareFilesSubTitle.ToString(this.cultureInfo);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000D448 File Offset: 0x0000B648
		public string CollaborateHeadingLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailO365FooterTitle.ToString(this.cultureInfo) + "&nbsp;";
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000D474 File Offset: 0x0000B674
		public string GroupConversationsLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailO365FooterBrowseConversations.ToString(this.cultureInfo) + "&nbsp;";
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		public string GroupCalendarLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailO365FooterBrowseViewCalendar.ToString(this.cultureInfo) + "&nbsp;";
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000D4CC File Offset: 0x0000B6CC
		public string GroupDocumentsLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailO365FooterBrowseShareFiles.ToString(this.cultureInfo);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		public string GroupTypeStringLabel
		{
			get
			{
				if (this.groupType == ModernGroupObjectType.Private)
				{
					return ClientStrings.GroupMailboxWelcomeEmailPrivateTypeText.ToString(this.cultureInfo);
				}
				return ClientStrings.GroupMailboxWelcomeEmailPublicTypeText.ToString(this.cultureInfo);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000D52C File Offset: 0x0000B72C
		public string SubscribeFooterPrefixLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailFooterSubscribeDescriptionText.ToString(this.cultureInfo);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000D54C File Offset: 0x0000B74C
		public string SubscribeFooterLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailFooterSubscribeLinkText.ToString(this.cultureInfo);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000D56C File Offset: 0x0000B76C
		public string UnsubscribeFooterPrefixLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailFooterUnsubscribeDescirptionText.ToString(this.cultureInfo);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000D58C File Offset: 0x0000B78C
		public string UnsubscribeFooterLabel
		{
			get
			{
				return ClientStrings.GroupMailboxWelcomeEmailFooterUnsubscribeLinkText.ToString(this.cultureInfo);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public string InboxUrl
		{
			get
			{
				return this.inboxUrl;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000D5B4 File Offset: 0x0000B7B4
		public string CalendarUrl
		{
			get
			{
				return this.calendarUrl;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		public string SharePointUrl
		{
			get
			{
				return this.sharePointUrl;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public string SubscribeUrl
		{
			get
			{
				return this.subscribeUrl;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		public string UnsubscribeUrl
		{
			get
			{
				return this.unsubscribeUrl;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public string GroupSmtpAddress
		{
			get
			{
				return this.groupSmtpAddress;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public string ExecutingUserPhotoId
		{
			get
			{
				return "cid:" + this.executingUserPhotoId;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000D5EE File Offset: 0x0000B7EE
		public string GroupPhotoId
		{
			get
			{
				return "cid:" + this.groupPhotoId;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000D600 File Offset: 0x0000B800
		public string ConverasationsImageId
		{
			get
			{
				return "cid:" + WelcomeMessageBodyData.WelcomeConversationsIcon.ImageId;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000D616 File Offset: 0x0000B816
		public string FilesImageId
		{
			get
			{
				return "cid:" + WelcomeMessageBodyData.WelcomeDocumentIcon.ImageId;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000D62C File Offset: 0x0000B82C
		public string O365ImageId
		{
			get
			{
				return "cid:" + WelcomeMessageBodyData.WelcomeO365Icon.ImageId;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000D642 File Offset: 0x0000B842
		public string ArrowImageId
		{
			get
			{
				return "cid:" + WelcomeMessageBodyData.WelcomeArrowIcon.ImageId;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000D658 File Offset: 0x0000B858
		public string FlippedArrowImageId
		{
			get
			{
				return "cid:" + WelcomeMessageBodyData.WelcomeArrowFlippedIcon.ImageId;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000D66E File Offset: 0x0000B86E
		public bool IsRightToLeftLanguage
		{
			get
			{
				return this.cultureInfo.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000D680 File Offset: 0x0000B880
		public bool IsLeftToRightLanguage
		{
			get
			{
				return !this.cultureInfo.TextInfo.IsRightToLeft;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000D695 File Offset: 0x0000B895
		public string NormalTextFlowDirectionCss
		{
			get
			{
				if (!this.IsRightToLeftLanguage)
				{
					return "ltr";
				}
				return "rtl";
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000D6AA File Offset: 0x0000B8AA
		public string ReverseTextFlowDirectionCss
		{
			get
			{
				if (!this.IsRightToLeftLanguage)
				{
					return "rtl";
				}
				return "ltr";
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000D6BF File Offset: 0x0000B8BF
		public bool ShowSubscribeBody
		{
			get
			{
				return !this.isGroupAutoSubscribed;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000D6CA File Offset: 0x0000B8CA
		public bool ShowExchangeLinks
		{
			get
			{
				return this.showExchangeLinks;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000D6D2 File Offset: 0x0000B8D2
		public bool IsAddedByDifferentUser
		{
			get
			{
				return this.isAddedByDifferentUser;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000D6DA File Offset: 0x0000B8DA
		public bool ExecutingUserHasPhoto
		{
			get
			{
				return this.executingUserHasPhoto;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000D6E2 File Offset: 0x0000B8E2
		public bool GroupHasPhoto
		{
			get
			{
				return this.groupHasPhoto;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000D6EA File Offset: 0x0000B8EA
		public bool IsGroupAutoSubscribed
		{
			get
			{
				return this.isGroupAutoSubscribed;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000D6F2 File Offset: 0x0000B8F2
		public bool IsSharePointEnabled
		{
			get
			{
				return this.isSharePointEnabled;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000D6FA File Offset: 0x0000B8FA
		public bool ShowPhotosHeader
		{
			get
			{
				return this.ExecutingUserHasPhoto || this.GroupHasPhoto;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000D70C File Offset: 0x0000B90C
		public ModernGroupObjectType GroupType
		{
			get
			{
				return this.groupType;
			}
		}

		// Token: 0x040000FF RID: 255
		private const string AttachmentIdPrefix = "cid:";

		// Token: 0x04000100 RID: 256
		public static readonly string UserPhotoImageId = "user_photo";

		// Token: 0x04000101 RID: 257
		public static readonly string GroupPhotoImageId = "group_photo";

		// Token: 0x04000102 RID: 258
		public static readonly ImageAttachment WelcomeConversationsIcon = new ImageAttachment("welcome_conversations_icon.png", "welcome_conversations_icon", "image/png", null);

		// Token: 0x04000103 RID: 259
		public static readonly ImageAttachment WelcomeDocumentIcon = new ImageAttachment("welcome_files_icon.png", "welcome_files_icon", "image/png", null);

		// Token: 0x04000104 RID: 260
		public static readonly ImageAttachment WelcomeO365Icon = new ImageAttachment("welcome_O365_icon.png", "welcome_O365_icon", "image/png", null);

		// Token: 0x04000105 RID: 261
		public static readonly ImageAttachment WelcomeArrowIcon = new ImageAttachment("welcome_arrow.png", "welcome_arrow", "image/png", null);

		// Token: 0x04000106 RID: 262
		public static readonly ImageAttachment WelcomeArrowFlippedIcon = new ImageAttachment("welcome_arrow_flipped.png", "welcome_arrow_flipped", "image/png", null);

		// Token: 0x04000107 RID: 263
		public static readonly ImageAttachment BlankGifImage = new ImageAttachment("blank.gif", "blank", "image/gif", null);

		// Token: 0x04000108 RID: 264
		private readonly string groupDisplayName;

		// Token: 0x04000109 RID: 265
		private readonly string groupDescription;

		// Token: 0x0400010A RID: 266
		private readonly string joiningHeaderMessage;

		// Token: 0x0400010B RID: 267
		private readonly string inboxUrl;

		// Token: 0x0400010C RID: 268
		private readonly string calendarUrl;

		// Token: 0x0400010D RID: 269
		private readonly string sharePointUrl;

		// Token: 0x0400010E RID: 270
		private readonly string subscribeUrl;

		// Token: 0x0400010F RID: 271
		private readonly string unsubscribeUrl;

		// Token: 0x04000110 RID: 272
		private readonly string groupSmtpAddress;

		// Token: 0x04000111 RID: 273
		private readonly string executingUserPhotoId;

		// Token: 0x04000112 RID: 274
		private readonly string groupPhotoId;

		// Token: 0x04000113 RID: 275
		private readonly bool showExchangeLinks;

		// Token: 0x04000114 RID: 276
		private readonly bool isAddedByDifferentUser;

		// Token: 0x04000115 RID: 277
		private readonly bool executingUserHasPhoto;

		// Token: 0x04000116 RID: 278
		private readonly bool groupHasPhoto;

		// Token: 0x04000117 RID: 279
		private readonly bool isGroupAutoSubscribed;

		// Token: 0x04000118 RID: 280
		private readonly bool isSharePointEnabled;

		// Token: 0x04000119 RID: 281
		private ModernGroupObjectType groupType;

		// Token: 0x0400011A RID: 282
		private CultureInfo cultureInfo;
	}
}
