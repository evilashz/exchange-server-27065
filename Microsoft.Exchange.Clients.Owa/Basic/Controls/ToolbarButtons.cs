using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200008B RID: 139
	public sealed class ToolbarButtons
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00021EE1 File Offset: 0x000200E1
		private ToolbarButtons()
		{
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00021EE9 File Offset: 0x000200E9
		public static ToolbarButton AddToContacts
		{
			get
			{
				return ToolbarButtons.addToContacts;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00021EF0 File Offset: 0x000200F0
		public static ToolbarButton AttachFile
		{
			get
			{
				return ToolbarButtons.attachFile;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00021EF7 File Offset: 0x000200F7
		public static ToolbarButton Cancel
		{
			get
			{
				return ToolbarButtons.cancel;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00021EFE File Offset: 0x000200FE
		public static ToolbarButton CheckMessages
		{
			get
			{
				return ToolbarButtons.checkMessages;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00021F05 File Offset: 0x00020105
		public static ToolbarButton CheckMessagesImage
		{
			get
			{
				return ToolbarButtons.checkMessagesImage;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00021F0C File Offset: 0x0002010C
		public static ToolbarButton CheckNames
		{
			get
			{
				return ToolbarButtons.checkNames;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00021F13 File Offset: 0x00020113
		public static ToolbarButton CheckNamesImageOnly
		{
			get
			{
				return ToolbarButtons.checkNamesImageOnly;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00021F1A File Offset: 0x0002011A
		public static ToolbarButton CloseImage
		{
			get
			{
				return ToolbarButtons.closeImage;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00021F21 File Offset: 0x00020121
		public static ToolbarButton CloseText
		{
			get
			{
				return ToolbarButtons.closeText;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00021F28 File Offset: 0x00020128
		public static ToolbarButton DayView
		{
			get
			{
				return ToolbarButtons.dayView;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00021F2F File Offset: 0x0002012F
		public static ToolbarButton Delete
		{
			get
			{
				return ToolbarButtons.delete;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00021F36 File Offset: 0x00020136
		public static ToolbarButton DeleteImage
		{
			get
			{
				return ToolbarButtons.deleteImage;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00021F3D File Offset: 0x0002013D
		public static ToolbarButton Done
		{
			get
			{
				return ToolbarButtons.done;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00021F44 File Offset: 0x00020144
		public static ToolbarButton EditContact
		{
			get
			{
				return ToolbarButtons.editContact;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00021F4B File Offset: 0x0002014B
		public static ToolbarButton EditSeries
		{
			get
			{
				return ToolbarButtons.editSeries;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00021F52 File Offset: 0x00020152
		public static ToolbarButton Forward
		{
			get
			{
				return ToolbarButtons.forward;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00021F59 File Offset: 0x00020159
		public static ToolbarButton ImportanceHigh
		{
			get
			{
				return ToolbarButtons.importanceHigh;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00021F60 File Offset: 0x00020160
		public static ToolbarButton ImportanceLow
		{
			get
			{
				return ToolbarButtons.importanceLow;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00021F67 File Offset: 0x00020167
		public static ToolbarButton Junk
		{
			get
			{
				return ToolbarButtons.junk;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00021F6E File Offset: 0x0002016E
		public static ToolbarButton InviteAttendees
		{
			get
			{
				return ToolbarButtons.inviteAttendees;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00021F75 File Offset: 0x00020175
		public static ToolbarButton MarkAsRead
		{
			get
			{
				return ToolbarButtons.markAsRead;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00021F7C File Offset: 0x0002017C
		public static ToolbarButton MarkAsUnread
		{
			get
			{
				return ToolbarButtons.markAsUnread;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00021F83 File Offset: 0x00020183
		public static ToolbarButton MeetingAccept
		{
			get
			{
				return ToolbarButtons.meetingAccept;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00021F8A File Offset: 0x0002018A
		public static ToolbarButton MeetingDecline
		{
			get
			{
				return ToolbarButtons.meetingDecline;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00021F91 File Offset: 0x00020191
		public static ToolbarButton MeetingNoResponseRequired
		{
			get
			{
				return ToolbarButtons.meetingNoResponseRequired;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00021F98 File Offset: 0x00020198
		public static ToolbarButton MeetingOutOfDate
		{
			get
			{
				return ToolbarButtons.meetingOutOfDate;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00021F9F File Offset: 0x0002019F
		public static ToolbarButton MeetingTentative
		{
			get
			{
				return ToolbarButtons.meetingTentative;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00021FA6 File Offset: 0x000201A6
		public static ToolbarButton MessageRecipients
		{
			get
			{
				return ToolbarButtons.messageRecipients;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00021FAD File Offset: 0x000201AD
		public static ToolbarButton Move
		{
			get
			{
				return ToolbarButtons.move;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00021FB4 File Offset: 0x000201B4
		public static ToolbarButton MoveImage
		{
			get
			{
				return ToolbarButtons.moveImage;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00021FBB File Offset: 0x000201BB
		public static ToolbarButton NewAppointment
		{
			get
			{
				return ToolbarButtons.newAppointment;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00021FC2 File Offset: 0x000201C2
		public static ToolbarButton NewContact
		{
			get
			{
				return ToolbarButtons.newContact;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00021FC9 File Offset: 0x000201C9
		public static ToolbarButton NewMeetingRequest
		{
			get
			{
				return ToolbarButtons.newMeetingRequest;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00021FD0 File Offset: 0x000201D0
		public static ToolbarButton NewMessage
		{
			get
			{
				return ToolbarButtons.newMessage;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00021FD7 File Offset: 0x000201D7
		public static ToolbarButton Next
		{
			get
			{
				return ToolbarButtons.next;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00021FDE File Offset: 0x000201DE
		public static ToolbarButton Previous
		{
			get
			{
				return ToolbarButtons.previous;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00021FE5 File Offset: 0x000201E5
		public static ToolbarButton NotJunk
		{
			get
			{
				return ToolbarButtons.notJunk;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00021FEC File Offset: 0x000201EC
		public static ToolbarButton Recurrence
		{
			get
			{
				return ToolbarButtons.recurrence;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00021FF3 File Offset: 0x000201F3
		public static ToolbarButton Remove
		{
			get
			{
				return ToolbarButtons.remove;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00021FFA File Offset: 0x000201FA
		public static ToolbarButton RemoveFromCalendar
		{
			get
			{
				return ToolbarButtons.removeFromCalendar;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00022001 File Offset: 0x00020201
		public static ToolbarButton RemoveRecurrence
		{
			get
			{
				return ToolbarButtons.removeRecurrence;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00022008 File Offset: 0x00020208
		public static ToolbarButton Reply
		{
			get
			{
				return ToolbarButtons.reply;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0002200F File Offset: 0x0002020F
		public static ToolbarButton ReplyAll
		{
			get
			{
				return ToolbarButtons.replyAll;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00022016 File Offset: 0x00020216
		public static ToolbarButton Save
		{
			get
			{
				return ToolbarButtons.save;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0002201D File Offset: 0x0002021D
		public static ToolbarButton SaveAndClose
		{
			get
			{
				return ToolbarButtons.saveAndClose;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00022024 File Offset: 0x00020224
		public static ToolbarButton SaveImageOnly
		{
			get
			{
				return ToolbarButtons.saveImageOnly;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0002202B File Offset: 0x0002022B
		public static ToolbarButton Send
		{
			get
			{
				return ToolbarButtons.send;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00022032 File Offset: 0x00020232
		public static ToolbarButton SendAgain
		{
			get
			{
				return ToolbarButtons.sendAgain;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00022039 File Offset: 0x00020239
		public static ToolbarButton SendEmail
		{
			get
			{
				return ToolbarButtons.sendEmail;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00022040 File Offset: 0x00020240
		public static ToolbarButton SendMeetingRequest
		{
			get
			{
				return ToolbarButtons.sendMeetingRequest;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00022047 File Offset: 0x00020247
		public static ToolbarButton SendEmailToContact
		{
			get
			{
				return ToolbarButtons.sendEmailToContact;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0002204E File Offset: 0x0002024E
		public static ToolbarButton SendMeetingRequestToContact
		{
			get
			{
				return ToolbarButtons.sendMeetingRequestToContact;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00022055 File Offset: 0x00020255
		public static ToolbarButton ShowCalendar
		{
			get
			{
				return ToolbarButtons.showCalendar;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0002205C File Offset: 0x0002025C
		public static ToolbarButton SendUpdate
		{
			get
			{
				return ToolbarButtons.sendUpdate;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00022063 File Offset: 0x00020263
		public static ToolbarButton SendCancellation
		{
			get
			{
				return ToolbarButtons.sendCancellation;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0002206A File Offset: 0x0002026A
		public static ToolbarButton Today
		{
			get
			{
				return ToolbarButtons.today;
			}
		}

		// Token: 0x0400030B RID: 779
		private static readonly ToolbarButton addToContacts = new ToolbarButton("AddToContacts", ToolbarButtonFlags.ImageAndText, 1775424225, ThemeFileId.AddToContacts);

		// Token: 0x0400030C RID: 780
		private static readonly ToolbarButton attachFile = new ToolbarButton("attachfile", ToolbarButtonFlags.Image, -1532412163, ThemeFileId.BasicToolbarAttach);

		// Token: 0x0400030D RID: 781
		private static readonly ToolbarButton cancel = new ToolbarButton("cancel", ToolbarButtonFlags.Text, -1936577052, ThemeFileId.None);

		// Token: 0x0400030E RID: 782
		private static readonly ToolbarButton checkMessages = new ToolbarButton("checkmessages", ToolbarButtonFlags.ImageAndText, 1476440846, ThemeFileId.CheckMessages);

		// Token: 0x0400030F RID: 783
		private static readonly ToolbarButton checkMessagesImage = new ToolbarButton("checkmessages", ToolbarButtonFlags.Image, 1476440846, ThemeFileId.CheckMessages);

		// Token: 0x04000310 RID: 784
		private static readonly ToolbarButton checkNames = new ToolbarButton("checknames", ToolbarButtonFlags.ImageAndText, -1374765726, ThemeFileId.BasicToolbarCheckNames);

		// Token: 0x04000311 RID: 785
		private static readonly ToolbarButton checkNamesImageOnly = new ToolbarButton("checknames", ToolbarButtonFlags.Image, -1374765726, ThemeFileId.BasicToolbarCheckNames);

		// Token: 0x04000312 RID: 786
		private static readonly ToolbarButton closeImage = new ToolbarButton("close", ToolbarButtonFlags.ImageAndNoHover, -1261564850, ThemeFileId.Close);

		// Token: 0x04000313 RID: 787
		private static readonly ToolbarButton closeText = new ToolbarButton("close", ToolbarButtonFlags.Text, -1261564850, ThemeFileId.None);

		// Token: 0x04000314 RID: 788
		private static readonly ToolbarButton dayView = new ToolbarButton("day", ToolbarButtonFlags.Text | ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky, -34880007, ThemeFileId.DayView);

		// Token: 0x04000315 RID: 789
		private static readonly ToolbarButton delete = new ToolbarButton("delete", ToolbarButtonFlags.ImageAndText, 1381996313, ThemeFileId.Delete);

		// Token: 0x04000316 RID: 790
		private static readonly ToolbarButton deleteImage = new ToolbarButton("delete", ToolbarButtonFlags.Image, 1381996313, ThemeFileId.Delete);

		// Token: 0x04000317 RID: 791
		private static readonly ToolbarButton done = new ToolbarButton("done", ToolbarButtonFlags.ImageAndText, 1414245946, ThemeFileId.Save);

		// Token: 0x04000318 RID: 792
		private static readonly ToolbarButton editContact = new ToolbarButton("editcontact", ToolbarButtonFlags.ImageAndText, 1133818274, ThemeFileId.Signature);

		// Token: 0x04000319 RID: 793
		private static readonly ToolbarButton editSeries = new ToolbarButton("editseries", ToolbarButtonFlags.ImageAndText, 1740466152, ThemeFileId.Recurrence);

		// Token: 0x0400031A RID: 794
		private static readonly ToolbarButton forward = new ToolbarButton("forward", ToolbarButtonFlags.ImageAndText, -1428116961, ThemeFileId.Forward);

		// Token: 0x0400031B RID: 795
		private static readonly ToolbarButton importanceHigh = new ToolbarButton("imphigh", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky, 1535769152, ThemeFileId.ImportanceHigh2);

		// Token: 0x0400031C RID: 796
		private static readonly ToolbarButton importanceLow = new ToolbarButton("implow", ToolbarButtonFlags.Image | ToolbarButtonFlags.Sticky, -1341425078, ThemeFileId.ImportanceLow2);

		// Token: 0x0400031D RID: 797
		private static readonly ToolbarButton junk = new ToolbarButton("junk", ToolbarButtonFlags.ImageAndText, 177541480, ThemeFileId.JunkEMail);

		// Token: 0x0400031E RID: 798
		private static readonly ToolbarButton inviteAttendees = new ToolbarButton("invite", ToolbarButtonFlags.ImageAndText, -775577546, ThemeFileId.MeetingRequest);

		// Token: 0x0400031F RID: 799
		private static readonly ToolbarButton markAsRead = new ToolbarButton("markread", ToolbarButtonFlags.Image, -228249127, ThemeFileId.MessageRead);

		// Token: 0x04000320 RID: 800
		private static readonly ToolbarButton markAsUnread = new ToolbarButton("markunread", ToolbarButtonFlags.Image, 556449500, ThemeFileId.MessageUnread);

		// Token: 0x04000321 RID: 801
		private static readonly ToolbarButton meetingAccept = new ToolbarButton("accept", ToolbarButtonFlags.ImageAndText, -475579318, ThemeFileId.MeetingAccept);

		// Token: 0x04000322 RID: 802
		private static readonly ToolbarButton meetingDecline = new ToolbarButton("decline", ToolbarButtonFlags.ImageAndText, -2119870632, ThemeFileId.MeetingDecline);

		// Token: 0x04000323 RID: 803
		private static readonly ToolbarButton meetingNoResponseRequired = new ToolbarButton(ToolbarButtonFlags.Text | ToolbarButtonFlags.Image | ToolbarButtonFlags.NoAction, -398794157, ThemeFileId.Informational);

		// Token: 0x04000324 RID: 804
		private static readonly ToolbarButton meetingOutOfDate = new ToolbarButton(ToolbarButtonFlags.Text | ToolbarButtonFlags.Image | ToolbarButtonFlags.NoAction, -1694210393, ThemeFileId.Informational);

		// Token: 0x04000325 RID: 805
		private static readonly ToolbarButton meetingTentative = new ToolbarButton("tentative", ToolbarButtonFlags.ImageAndText, 1797669216, ThemeFileId.MeetingTentative);

		// Token: 0x04000326 RID: 806
		private static readonly ToolbarButton messageRecipients = new ToolbarButton("messagerecipients", ToolbarButtonFlags.Tab, -324105221, ThemeFileId.EMailContact);

		// Token: 0x04000327 RID: 807
		private static readonly ToolbarButton move = new ToolbarButton("move", ToolbarButtonFlags.ImageAndText, 1414245993, ThemeFileId.Move);

		// Token: 0x04000328 RID: 808
		private static readonly ToolbarButton moveImage = new ToolbarButton("move", ToolbarButtonFlags.Image, 1414245993, ThemeFileId.Move);

		// Token: 0x04000329 RID: 809
		private static readonly ToolbarButton newAppointment = new ToolbarButton("newappt", ToolbarButtonFlags.ImageAndText, 321749823, ThemeFileId.Appointment);

		// Token: 0x0400032A RID: 810
		private static readonly ToolbarButton newContact = new ToolbarButton("newcontact", ToolbarButtonFlags.ImageAndText, 252880604, ThemeFileId.Contact2Small);

		// Token: 0x0400032B RID: 811
		private static readonly ToolbarButton newMeetingRequest = new ToolbarButton("newmtng", ToolbarButtonFlags.ImageAndText, 1720754026, ThemeFileId.MeetingRequest);

		// Token: 0x0400032C RID: 812
		private static readonly ToolbarButton newMessage = new ToolbarButton("newmsg", ToolbarButtonFlags.ImageAndText, 1601110671, ThemeFileId.EMail3);

		// Token: 0x0400032D RID: 813
		private static readonly ToolbarButton next = new ToolbarButton("next", ToolbarButtonFlags.ImageAndNoHover, -1846382016, ThemeFileId.Next);

		// Token: 0x0400032E RID: 814
		private static readonly ToolbarButton previous = new ToolbarButton("previous", ToolbarButtonFlags.ImageAndNoHover, -577308044, ThemeFileId.Previous);

		// Token: 0x0400032F RID: 815
		private static readonly ToolbarButton notJunk = new ToolbarButton("notjunk", ToolbarButtonFlags.ImageAndText, 856598503, ThemeFileId.Inbox);

		// Token: 0x04000330 RID: 816
		private static readonly ToolbarButton recurrence = new ToolbarButton("rcr", ToolbarButtonFlags.ImageAndText, -1955658819, ThemeFileId.Recurrence);

		// Token: 0x04000331 RID: 817
		private static readonly ToolbarButton remove = new ToolbarButton("remove", ToolbarButtonFlags.ImageAndText, 1388922078, ThemeFileId.Delete);

		// Token: 0x04000332 RID: 818
		private static readonly ToolbarButton removeFromCalendar = new ToolbarButton("remove", ToolbarButtonFlags.Text, -2115983576, ThemeFileId.None);

		// Token: 0x04000333 RID: 819
		private static readonly ToolbarButton removeRecurrence = new ToolbarButton("removerecurrence", ToolbarButtonFlags.ImageAndText, -56652072, ThemeFileId.Delete);

		// Token: 0x04000334 RID: 820
		private static readonly ToolbarButton reply = new ToolbarButton("reply", ToolbarButtonFlags.ImageAndText, -327372070, ThemeFileId.Reply);

		// Token: 0x04000335 RID: 821
		private static readonly ToolbarButton replyAll = new ToolbarButton("replyall", ToolbarButtonFlags.ImageAndText, 826363927, ThemeFileId.ReplyAll);

		// Token: 0x04000336 RID: 822
		private static readonly ToolbarButton save = new ToolbarButton("save", ToolbarButtonFlags.ImageAndText, -1966746939, ThemeFileId.Save);

		// Token: 0x04000337 RID: 823
		private static readonly ToolbarButton saveAndClose = new ToolbarButton("saveclose", ToolbarButtonFlags.ImageAndText, -224317800, ThemeFileId.Save);

		// Token: 0x04000338 RID: 824
		private static readonly ToolbarButton saveImageOnly = new ToolbarButton("save", ToolbarButtonFlags.Image, -1966746939, ThemeFileId.Save);

		// Token: 0x04000339 RID: 825
		private static readonly ToolbarButton send = new ToolbarButton("send", ToolbarButtonFlags.ImageAndText, -158743924, ThemeFileId.Send);

		// Token: 0x0400033A RID: 826
		private static readonly ToolbarButton sendAgain = new ToolbarButton("sendagain", ToolbarButtonFlags.ImageAndText, -1902695064, ThemeFileId.Send);

		// Token: 0x0400033B RID: 827
		private static readonly ToolbarButton sendEmail = new ToolbarButton("sendemail", ToolbarButtonFlags.ImageAndText, 1061463472, ThemeFileId.EMailContact);

		// Token: 0x0400033C RID: 828
		private static readonly ToolbarButton sendMeetingRequest = new ToolbarButton("sendmeetingrequest", ToolbarButtonFlags.ImageAndText, 649414410, ThemeFileId.MeetingRecipients);

		// Token: 0x0400033D RID: 829
		private static readonly ToolbarButton sendEmailToContact = new ToolbarButton("mail", ToolbarButtonFlags.Image, 1061463472, ThemeFileId.EMailContact);

		// Token: 0x0400033E RID: 830
		private static readonly ToolbarButton sendMeetingRequestToContact = new ToolbarButton("mtng", ToolbarButtonFlags.Image, 649414410, ThemeFileId.MeetingRecipients);

		// Token: 0x0400033F RID: 831
		private static readonly ToolbarButton showCalendar = new ToolbarButton("showcalendar", ToolbarButtonFlags.ImageAndText, -373408913, ThemeFileId.ShowCalendar);

		// Token: 0x04000340 RID: 832
		private static readonly ToolbarButton sendUpdate = new ToolbarButton("send", ToolbarButtonFlags.ImageAndText, 1302559757, ThemeFileId.Send);

		// Token: 0x04000341 RID: 833
		private static readonly ToolbarButton sendCancellation = new ToolbarButton("sendcancel", ToolbarButtonFlags.ImageAndText, 1665554947, ThemeFileId.Send);

		// Token: 0x04000342 RID: 834
		private static readonly ToolbarButton today = new ToolbarButton("today", ToolbarButtonFlags.Text, -367521373, ThemeFileId.None);
	}
}
