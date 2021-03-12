using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000018 RID: 24
	public class CommandSprite : BaseSprite
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x0004B8B0 File Offset: 0x00049AB0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CssClass = this.CssClass + " " + this.SpriteCssClass;
			this.ImageUrl = BaseSprite.GetSpriteImageSrc(this);
			string value = (this.AlternateText == null) ? "" : this.AlternateText;
			base.Attributes.Add("title", value);
			this.GenerateEmptyAlternateText = true;
		}

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0004B91F File Offset: 0x00049B1F
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x0004B927 File Offset: 0x00049B27
		public CommandSprite.SpriteId ImageId { get; set; }

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0004B930 File Offset: 0x00049B30
		public string SpriteCssClass
		{
			get
			{
				return CommandSprite.GetCssClass(this.ImageId);
			}
		}

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0004B93D File Offset: 0x00049B3D
		public bool IsRenderable
		{
			get
			{
				return this.ImageId != CommandSprite.SpriteId.NONE;
			}
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0004B94B File Offset: 0x00049B4B
		public static string GetCssClass(CommandSprite.SpriteId spriteid)
		{
			if (spriteid == CommandSprite.SpriteId.NONE)
			{
				return string.Empty;
			}
			return CommandSprite.GetBaseCssClass() + " " + CommandSprite.ImageStyles[(int)spriteid];
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0004B96C File Offset: 0x00049B6C
		private static string GetBaseCssClass()
		{
			string text = CommandSprite.BaseCssClass;
			if (CommandSprite.HasDCImage && BaseSprite.IsDataCenter)
			{
				text = "DC" + text;
			}
			return text;
		}

		// Token: 0x04001857 RID: 6231
		public static readonly string BaseCssClass = "CommandSprite";

		// Token: 0x04001858 RID: 6232
		public static readonly bool HasDCImage = false;

		// Token: 0x04001859 RID: 6233
		private static readonly string[] ImageStyles = new string[]
		{
			string.Empty,
			"CV-CS",
			"AX-CS",
			"AM-CS",
			"AN-CS",
			"DisabledButtonPart .B-CS",
			"B-CS",
			"EnabledButtonPart:hover .B-CS",
			"I-CS",
			"J-CS",
			"K-CS",
			"H-CS",
			"F-CS",
			"G-CS",
			"EnabledToolBarItem:hover .CB-CS",
			"CB-CS",
			"BZ-CS",
			"BX-CS",
			"BY-CS",
			"CA-CS",
			"DisabledToolBarItem .BZ-CS",
			"EnabledToolBarItem:hover .CA-CS",
			"DisabledToolBarItem .CA-CS",
			"EnabledToolBarItem:hover .BZ-CS",
			"DisabledToolBarItem .CB-CS",
			"search-publicfoldersearch",
			"CF-CS",
			"CG-CS",
			"CI-CS",
			"CH-CS",
			"DisabledToolBarItem .CE-CS",
			"CD-CS",
			"CC-CS",
			"EnabledToolBarItem:hover .CD-CS",
			"EnabledToolBarItem:hover .CE-CS",
			"CE-CS",
			"BW-CS",
			"MoveDown",
			"DisabledToolBarItem .BP-CS",
			"EnabledToolBarItem:hover .BP-CS",
			"MoveUp",
			"DisabledToolBarItem .MoveDown",
			"EnabledToolBarItem:hover .MoveDown",
			"BN-CS",
			"BM-CS",
			"BL-CS",
			"BP-CS",
			"EnabledToolBarItem:hover .BO-CS",
			"BO-CS",
			"BU-CS",
			"BT-CS",
			"DisabledToolBarItem .BS-CS",
			"DisabledToolBarItem .BV-CS",
			"EnabledToolBarItem:hover .BV-CS",
			"BV-CS",
			"BQ-CS",
			"DisabledToolBarItem .MoveUp",
			"EnabledToolBarItem:hover .MoveUp",
			"EnabledToolBarItem:hover .BS-CS",
			"BS-CS",
			"BR-CS",
			"CJ-CS",
			"CS-CS",
			"DisabledToolBarItem .ToolBarRemoveAll",
			"EnabledToolBarItem:hover .ToolBarRemoveAll",
			"DisabledToolBarItem .CT-CS",
			"EnabledToolBarItem:hover .CT-CS",
			"CT-CS",
			"CR-CS",
			"WhiteEditButton",
			"DisabledToolBarItem .EditButton",
			"ToolBarRemoveAll",
			"DisabledToolBarItem .CR-CS",
			"EnabledToolBarItem:hover .CR-CS",
			"CU-CS",
			"DC-CS",
			"DisabledToolBarItem .DB-CS",
			"EnabledToolBarItem:hover .DB-CS",
			"DisabledToolBarItem .DD-CS",
			"EnabledToolBarItem:hover .DD-CS",
			"DD-CS",
			"CY-CS",
			"CX-CS",
			"CW-CS",
			"DB-CS",
			"DA-CS",
			"CZ-CS",
			"CM-CS",
			"DisabledToolBarItem .CL-CS",
			"EnabledToolBarItem:hover .CL-CS",
			"CN-CS",
			"DisabledToolBarItem .CM-CS",
			"EnabledToolBarItem:hover .CM-CS",
			"CK-CS",
			"DisabledToolBarItem .CJ-CS",
			"EnabledToolBarItem:hover .CJ-CS",
			"CL-CS",
			"DisabledToolBarItem .CK-CS",
			"EnabledToolBarItem:hover .CK-CS",
			"EnabledToolBarItem:hover .CN-CS",
			"EnabledToolBarItem:hover .CQ-CS",
			"CQ-CS",
			"WhiteCloseButton",
			"EnabledToolBarItem:hover .EditButton",
			"EditButton",
			"DisabledToolBarItem .CQ-CS",
			"EnabledToolBarItem:hover .CO-CS",
			"CO-CS",
			"DisabledToolBarItem .CN-CS",
			"CloseButton",
			"CP-CS",
			"DisabledToolBarItem .CO-CS",
			"BK-CS",
			"DisabledArrow",
			"DisabledToolBarItem .X-CS",
			"EnabledToolBarItem:hover .X-CS",
			"AA-CS",
			"Z-CS",
			"Y-CS",
			"X-CS",
			"DisabledToolBarItem .V-CS",
			"EnabledToolBarItem:hover .V-CS",
			"V-CS",
			"DisabledButtonPart .W-CS",
			"EnabledButtonPart:hover .W-CS",
			"W-CS",
			"DisabledToolBarItem .AE-CS",
			"EnabledToolBarItem:hover .AE-CS",
			"AE-CS",
			"EnabledToolBarItem:hover .AF-CS",
			"A-CS",
			"AF-CS",
			"AD-CS",
			"DisabledToolBarItem .AB-CS",
			"EnabledToolBarItem:hover .AB-CS",
			"AB-CS",
			"DisabledToolBarItem .AC-CS",
			"EnabledToolBarItem:hover .AC-CS",
			"AC-CS",
			"DisabledToolBarItem .M-CS",
			"EnabledToolBarItem:hover .M-CS",
			"M-CS",
			"Q-CS",
			"P-CS",
			"O-CS",
			"DisabledToolBarItem .L-CS",
			"D-CS",
			"C-CS",
			"EnabledToolBarItem:hover .A-CS",
			"EnabledToolBarItem:hover .L-CS",
			"L-CS",
			"E-CS",
			"Copy",
			"T-CS",
			"S-CS",
			"DisabledButtonPart .U-CS",
			"EnabledButtonPart:hover .U-CS",
			"U-CS",
			"DisabledToolBarItem .R-CS",
			"ChoiceButton",
			"DisabledToolBarItem .Q-CS",
			"EnabledToolBarItem:hover .Q-CS",
			"EnabledToolBarItem:hover .R-CS",
			"R-CS",
			"clear-default",
			"DisabledToolBarItem .AF-CS",
			"AU-CS",
			"DisabledToolBarItem .AT-CS",
			"AW-CS",
			"AZ-CS",
			"AY-CS",
			"EnabledToolBarItem:hover .AT-CS",
			"BG-CS",
			"FacebookLogo",
			"BI-CS",
			"AT-CS",
			"AS-CS",
			"BA-CS",
			"BE-CS",
			"BD-CS",
			"BF-CS",
			"DisabledToolBarItem .BF-CS",
			"EnabledToolBarItem:hover .BF-CS",
			"DisabledToolBarItem .BC-CS",
			"LinkedInLogo",
			"BB-CS",
			"BC-CS",
			"BH-CS",
			"EnabledToolBarItem:hover .BC-CS",
			"DisabledToolBarItem .AR-CS",
			"AK-CS",
			"DisabledButtonPart .Ellipsis",
			"EnabledButtonPart:hover .Ellipsis",
			"EnabledToolBarItem:hover .AO-CS",
			"AO-CS",
			"AL-CS",
			"BJ-CS",
			"AH-CS",
			"AG-CS",
			"Ellipsis",
			"AJ-CS",
			"AI-CS",
			"EnabledToolBarItem:hover .AQ-CS",
			"AQ-CS",
			"DisabledToolBarItem .AQ-CS",
			"EnabledToolBarItem:hover .AR-CS",
			"DisabledToolBarItem .AO-CS",
			"AR-CS",
			"EnabledArrow",
			"AP-CS",
			"EnabledToolBarItem:hover .search-default",
			"search-default",
			"DisabledToolBarItem .search-default",
			"AV-CS",
			"PagePre",
			"PageFirst",
			"PageLast",
			"PageNext",
			"SrtDsc",
			"SrtAsc",
			"DeleteButtonSmall",
			"N-CS"
		};

		// Token: 0x02000019 RID: 25
		public enum SpriteId
		{
			// Token: 0x0400185C RID: 6236
			NONE,
			// Token: 0x0400185D RID: 6237
			UMAutoAttendant32,
			// Token: 0x0400185E RID: 6238
			HotmailSubscription28,
			// Token: 0x0400185F RID: 6239
			EmailSubscription28,
			// Token: 0x04001860 RID: 6240
			Eml,
			// Token: 0x04001861 RID: 6241
			AddDropdown_disable,
			// Token: 0x04001862 RID: 6242
			AddDropdown,
			// Token: 0x04001863 RID: 6243
			AddDropdown_hover,
			// Token: 0x04001864 RID: 6244
			AudioQualityOneBar,
			// Token: 0x04001865 RID: 6245
			AudioQualityThreeBars,
			// Token: 0x04001866 RID: 6246
			AudioQualityTwoBars,
			// Token: 0x04001867 RID: 6247
			AudioQualityNotAvailable,
			// Token: 0x04001868 RID: 6248
			AudioQualityFiveBars,
			// Token: 0x04001869 RID: 6249
			AudioQualityFourBars,
			// Token: 0x0400186A RID: 6250
			ReleaseMessage_hover,
			// Token: 0x0400186B RID: 6251
			ReleaseMessage,
			// Token: 0x0400186C RID: 6252
			Print,
			// Token: 0x0400186D RID: 6253
			PerfConsoleCommand,
			// Token: 0x0400186E RID: 6254
			PreviewMailBoxSearch,
			// Token: 0x0400186F RID: 6255
			RecoverMailbox16,
			// Token: 0x04001870 RID: 6256
			Print_disable,
			// Token: 0x04001871 RID: 6257
			RecoverMailbox16_hover,
			// Token: 0x04001872 RID: 6258
			RecoverMailbox16_disable,
			// Token: 0x04001873 RID: 6259
			Print_hover,
			// Token: 0x04001874 RID: 6260
			ReleaseMessage_disable,
			// Token: 0x04001875 RID: 6261
			SearchPublicFolder,
			// Token: 0x04001876 RID: 6262
			Room16,
			// Token: 0x04001877 RID: 6263
			Shared16,
			// Token: 0x04001878 RID: 6264
			SmileFaceGray16,
			// Token: 0x04001879 RID: 6265
			SingUpAddress,
			// Token: 0x0400187A RID: 6266
			RetrieveLog16_disable,
			// Token: 0x0400187B RID: 6267
			ReSetVDir16,
			// Token: 0x0400187C RID: 6268
			RemoteMailbox16,
			// Token: 0x0400187D RID: 6269
			ReSetVDir16_hover,
			// Token: 0x0400187E RID: 6270
			RetrieveLog16_hover,
			// Token: 0x0400187F RID: 6271
			RetrieveLog16,
			// Token: 0x04001880 RID: 6272
			Password16,
			// Token: 0x04001881 RID: 6273
			MoveDownCommand,
			// Token: 0x04001882 RID: 6274
			MetroAdd_disable,
			// Token: 0x04001883 RID: 6275
			MetroAdd_hover,
			// Token: 0x04001884 RID: 6276
			MoveUpCommand,
			// Token: 0x04001885 RID: 6277
			MoveDownCommand_disable,
			// Token: 0x04001886 RID: 6278
			MoveDownCommand_hover,
			// Token: 0x04001887 RID: 6279
			MailUser,
			// Token: 0x04001888 RID: 6280
			MailEnabledPublicFolder,
			// Token: 0x04001889 RID: 6281
			MailboxSearchSucceeded,
			// Token: 0x0400188A RID: 6282
			MetroAdd,
			// Token: 0x0400188B RID: 6283
			ManageDAGMembership16_hover,
			// Token: 0x0400188C RID: 6284
			ManageDAGMembership16,
			// Token: 0x0400188D RID: 6285
			O365Reports,
			// Token: 0x0400188E RID: 6286
			NewRoleAssignmentPolicy16,
			// Token: 0x0400188F RID: 6287
			NewDeviceAccessRule16_disable,
			// Token: 0x04001890 RID: 6288
			OutlookSettings_disable,
			// Token: 0x04001891 RID: 6289
			OutlookSettings_hover,
			// Token: 0x04001892 RID: 6290
			OutlookSettings,
			// Token: 0x04001893 RID: 6291
			NewActiveSyncPolicy16,
			// Token: 0x04001894 RID: 6292
			MoveUpCommand_disable,
			// Token: 0x04001895 RID: 6293
			MoveUpCommand_hover,
			// Token: 0x04001896 RID: 6294
			NewDeviceAccessRule16_hover,
			// Token: 0x04001897 RID: 6295
			NewDeviceAccessRule16,
			// Token: 0x04001898 RID: 6296
			NewAdminRoleGroup16,
			// Token: 0x04001899 RID: 6297
			Start,
			// Token: 0x0400189A RID: 6298
			TransportRule16,
			// Token: 0x0400189B RID: 6299
			ToolBarRemoveAll_disable,
			// Token: 0x0400189C RID: 6300
			ToolBarRemoveAll_hover,
			// Token: 0x0400189D RID: 6301
			UMAudioQualityDetails_disable,
			// Token: 0x0400189E RID: 6302
			UMAudioQualityDetails_hover,
			// Token: 0x0400189F RID: 6303
			UMAudioQualityDetails,
			// Token: 0x040018A0 RID: 6304
			ToolBarRemove,
			// Token: 0x040018A1 RID: 6305
			ToolBarPropertiesWhite,
			// Token: 0x040018A2 RID: 6306
			ToolBarProperties_disable,
			// Token: 0x040018A3 RID: 6307
			ToolBarRemoveAll,
			// Token: 0x040018A4 RID: 6308
			ToolBarRemove_disable,
			// Token: 0x040018A5 RID: 6309
			ToolBarRemove_hover,
			// Token: 0x040018A6 RID: 6310
			UMAutoAttendant16,
			// Token: 0x040018A7 RID: 6311
			Users16,
			// Token: 0x040018A8 RID: 6312
			UnblockDevice16_disable,
			// Token: 0x040018A9 RID: 6313
			UnblockDevice16_hover,
			// Token: 0x040018AA RID: 6314
			WipeMobileDevice16_disable,
			// Token: 0x040018AB RID: 6315
			WipeMobileDevice16_hover,
			// Token: 0x040018AC RID: 6316
			WipeMobileDevice16,
			// Token: 0x040018AD RID: 6317
			UMIPGateways16,
			// Token: 0x040018AE RID: 6318
			UMHuntGroups16,
			// Token: 0x040018AF RID: 6319
			UMDialPlans16,
			// Token: 0x040018B0 RID: 6320
			UnblockDevice16,
			// Token: 0x040018B1 RID: 6321
			UMMailboxPolicy16,
			// Token: 0x040018B2 RID: 6322
			UMMailboxFeature,
			// Token: 0x040018B3 RID: 6323
			Stop,
			// Token: 0x040018B4 RID: 6324
			StartMailBoxSearch_disable,
			// Token: 0x040018B5 RID: 6325
			StartMailBoxSearch_hover,
			// Token: 0x040018B6 RID: 6326
			StopAndRetrieveLog16,
			// Token: 0x040018B7 RID: 6327
			Stop_disable,
			// Token: 0x040018B8 RID: 6328
			Stop_hover,
			// Token: 0x040018B9 RID: 6329
			StartLogging16,
			// Token: 0x040018BA RID: 6330
			Start_disable,
			// Token: 0x040018BB RID: 6331
			Start_hover,
			// Token: 0x040018BC RID: 6332
			StartMailBoxSearch,
			// Token: 0x040018BD RID: 6333
			StartLogging16_disable,
			// Token: 0x040018BE RID: 6334
			StartLogging16_hover,
			// Token: 0x040018BF RID: 6335
			StopAndRetrieveLog16_hover,
			// Token: 0x040018C0 RID: 6336
			ToolBarDelete_hover,
			// Token: 0x040018C1 RID: 6337
			ToolBarDelete,
			// Token: 0x040018C2 RID: 6338
			ToolBarCloseWhite,
			// Token: 0x040018C3 RID: 6339
			ToolBarProperties_hover,
			// Token: 0x040018C4 RID: 6340
			ToolBarProperties,
			// Token: 0x040018C5 RID: 6341
			ToolBarDelete_disable,
			// Token: 0x040018C6 RID: 6342
			StopMailboxSearch_hover,
			// Token: 0x040018C7 RID: 6343
			StopMailboxSearch,
			// Token: 0x040018C8 RID: 6344
			StopAndRetrieveLog16_disable,
			// Token: 0x040018C9 RID: 6345
			ToolBarClose,
			// Token: 0x040018CA RID: 6346
			StopRequest,
			// Token: 0x040018CB RID: 6347
			StopMailboxSearch_disable,
			// Token: 0x040018CC RID: 6348
			MailboxSearchStopping,
			// Token: 0x040018CD RID: 6349
			DisabledArrow,
			// Token: 0x040018CE RID: 6350
			Disable_disable,
			// Token: 0x040018CF RID: 6351
			Disable_hover,
			// Token: 0x040018D0 RID: 6352
			DistributionGroup16,
			// Token: 0x040018D1 RID: 6353
			DistributionGroup,
			// Token: 0x040018D2 RID: 6354
			DisplayDiscoveryPassword16,
			// Token: 0x040018D3 RID: 6355
			Disable,
			// Token: 0x040018D4 RID: 6356
			CopyAdminRoleGroup16_disable,
			// Token: 0x040018D5 RID: 6357
			CopyAdminRoleGroup16_hover,
			// Token: 0x040018D6 RID: 6358
			CopyAdminRoleGroup16,
			// Token: 0x040018D7 RID: 6359
			CopyAdminRoleGroup16Dropdown_disable,
			// Token: 0x040018D8 RID: 6360
			CopyAdminRoleGroup16Dropdown_hover,
			// Token: 0x040018D9 RID: 6361
			CopyAdminRoleGroup16Dropdown,
			// Token: 0x040018DA RID: 6362
			EASAllowUser16_disable,
			// Token: 0x040018DB RID: 6363
			EASAllowUser16_hover,
			// Token: 0x040018DC RID: 6364
			EASAllowUser16,
			// Token: 0x040018DD RID: 6365
			EASBlockUser16_hover,
			// Token: 0x040018DE RID: 6366
			AddDAGNetwork16,
			// Token: 0x040018DF RID: 6367
			EASBlockUser16,
			// Token: 0x040018E0 RID: 6368
			DynamicDistributionGroup,
			// Token: 0x040018E1 RID: 6369
			DistributionGroupAdd_disable,
			// Token: 0x040018E2 RID: 6370
			DistributionGroupAdd_hover,
			// Token: 0x040018E3 RID: 6371
			DistributionGroupAdd,
			// Token: 0x040018E4 RID: 6372
			DistributionGroupDepart_disable,
			// Token: 0x040018E5 RID: 6373
			DistributionGroupDepart_hover,
			// Token: 0x040018E6 RID: 6374
			DistributionGroupDepart,
			// Token: 0x040018E7 RID: 6375
			BlockDevice16_disable,
			// Token: 0x040018E8 RID: 6376
			BlockDevice16_hover,
			// Token: 0x040018E9 RID: 6377
			BlockDevice16,
			// Token: 0x040018EA RID: 6378
			CancelDeviceWipe16,
			// Token: 0x040018EB RID: 6379
			ButtonsPanelNext16,
			// Token: 0x040018EC RID: 6380
			ButtonsPanelBack16,
			// Token: 0x040018ED RID: 6381
			AutodiscoverDomain_disable,
			// Token: 0x040018EE RID: 6382
			AdvancedSearchDefault,
			// Token: 0x040018EF RID: 6383
			AddressBook16,
			// Token: 0x040018F0 RID: 6384
			AddDAGNetwork16_hover,
			// Token: 0x040018F1 RID: 6385
			AutodiscoverDomain_hover,
			// Token: 0x040018F2 RID: 6386
			AutodiscoverDomain,
			// Token: 0x040018F3 RID: 6387
			Archive16,
			// Token: 0x040018F4 RID: 6388
			Copy,
			// Token: 0x040018F5 RID: 6389
			Contacts16,
			// Token: 0x040018F6 RID: 6390
			Contact,
			// Token: 0x040018F7 RID: 6391
			Copy16_disable,
			// Token: 0x040018F8 RID: 6392
			Copy16_hover,
			// Token: 0x040018F9 RID: 6393
			Copy16,
			// Token: 0x040018FA RID: 6394
			ConfigCASDomain_disable,
			// Token: 0x040018FB RID: 6395
			ChoiceButton16,
			// Token: 0x040018FC RID: 6396
			CancelDeviceWipe16_disable,
			// Token: 0x040018FD RID: 6397
			CancelDeviceWipe16_hover,
			// Token: 0x040018FE RID: 6398
			ConfigCASDomain_hover,
			// Token: 0x040018FF RID: 6399
			ConfigCASDomain,
			// Token: 0x04001900 RID: 6400
			ClearDefault,
			// Token: 0x04001901 RID: 6401
			EASBlockUser16_disable,
			// Token: 0x04001902 RID: 6402
			FVAEnabled,
			// Token: 0x04001903 RID: 6403
			ForwardEmail_disable,
			// Token: 0x04001904 RID: 6404
			HotmailSubscription16,
			// Token: 0x04001905 RID: 6405
			JournalRule16,
			// Token: 0x04001906 RID: 6406
			InboxRule16,
			// Token: 0x04001907 RID: 6407
			ForwardEmail_hover,
			// Token: 0x04001908 RID: 6408
			MailboxSearchFailed,
			// Token: 0x04001909 RID: 6409
			FacebookLogo,
			// Token: 0x0400190A RID: 6410
			MailboxSearchPartiallySucceeded,
			// Token: 0x0400190B RID: 6411
			ForwardEmail,
			// Token: 0x0400190C RID: 6412
			Federated16,
			// Token: 0x0400190D RID: 6413
			Legacy16,
			// Token: 0x0400190E RID: 6414
			Mailbox16,
			// Token: 0x0400190F RID: 6415
			Mailbox,
			// Token: 0x04001910 RID: 6416
			MailboxSearch,
			// Token: 0x04001911 RID: 6417
			MailboxSearch_disable,
			// Token: 0x04001912 RID: 6418
			MailboxSearch_hover,
			// Token: 0x04001913 RID: 6419
			ListViewRefresh_disable,
			// Token: 0x04001914 RID: 6420
			LinkedInLogo,
			// Token: 0x04001915 RID: 6421
			Linked16,
			// Token: 0x04001916 RID: 6422
			ListViewRefresh,
			// Token: 0x04001917 RID: 6423
			MailboxSearchInProgress,
			// Token: 0x04001918 RID: 6424
			ListViewRefresh_hover,
			// Token: 0x04001919 RID: 6425
			ExportPST_disable,
			// Token: 0x0400191A RID: 6426
			EmailMigration16,
			// Token: 0x0400191B RID: 6427
			Ellipsis_disable,
			// Token: 0x0400191C RID: 6428
			Ellipsis_hover,
			// Token: 0x0400191D RID: 6429
			Enable_hover,
			// Token: 0x0400191E RID: 6430
			Enable,
			// Token: 0x0400191F RID: 6431
			EmailSubscription16,
			// Token: 0x04001920 RID: 6432
			MailboxSearchStopped,
			// Token: 0x04001921 RID: 6433
			EditMailboxSearchPage,
			// Token: 0x04001922 RID: 6434
			EditMailBoxSearch16,
			// Token: 0x04001923 RID: 6435
			Ellipsis,
			// Token: 0x04001924 RID: 6436
			ElevationReject,
			// Token: 0x04001925 RID: 6437
			ElevationAccept,
			// Token: 0x04001926 RID: 6438
			ExportDay16_hover,
			// Token: 0x04001927 RID: 6439
			ExportDay16,
			// Token: 0x04001928 RID: 6440
			ExportDay16_disable,
			// Token: 0x04001929 RID: 6441
			ExportPST_hover,
			// Token: 0x0400192A RID: 6442
			Enable_disable,
			// Token: 0x0400192B RID: 6443
			ExportPST,
			// Token: 0x0400192C RID: 6444
			EnabledArrow,
			// Token: 0x0400192D RID: 6445
			Equipment16,
			// Token: 0x0400192E RID: 6446
			SearchDefault_hover,
			// Token: 0x0400192F RID: 6447
			SearchDefault,
			// Token: 0x04001930 RID: 6448
			SearchDefault_disable,
			// Token: 0x04001931 RID: 6449
			HelpCommand,
			// Token: 0x04001932 RID: 6450
			PagePre,
			// Token: 0x04001933 RID: 6451
			PageFirst,
			// Token: 0x04001934 RID: 6452
			PageLast,
			// Token: 0x04001935 RID: 6453
			PageNext,
			// Token: 0x04001936 RID: 6454
			SortOrderDesc,
			// Token: 0x04001937 RID: 6455
			SortOrderAsc,
			// Token: 0x04001938 RID: 6456
			ToolBarDeleteSmall,
			// Token: 0x04001939 RID: 6457
			Bullet
		}
	}
}
