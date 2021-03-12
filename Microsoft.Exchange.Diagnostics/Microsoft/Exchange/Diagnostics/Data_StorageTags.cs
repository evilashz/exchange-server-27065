using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200022C RID: 556
	public struct Data_StorageTags
	{
		// Token: 0x04000C7E RID: 3198
		public const int Storage = 0;

		// Token: 0x04000C7F RID: 3199
		public const int Interop = 1;

		// Token: 0x04000C80 RID: 3200
		public const int MeetingMessage = 2;

		// Token: 0x04000C81 RID: 3201
		public const int Event = 3;

		// Token: 0x04000C82 RID: 3202
		public const int Dispose = 4;

		// Token: 0x04000C83 RID: 3203
		public const int ServiceDiscovery = 5;

		// Token: 0x04000C84 RID: 3204
		public const int Context = 6;

		// Token: 0x04000C85 RID: 3205
		public const int ContextShadow = 7;

		// Token: 0x04000C86 RID: 3206
		public const int CcGeneric = 8;

		// Token: 0x04000C87 RID: 3207
		public const int CcOle = 9;

		// Token: 0x04000C88 RID: 3208
		public const int CcBody = 10;

		// Token: 0x04000C89 RID: 3209
		public const int CcInboundGeneric = 11;

		// Token: 0x04000C8A RID: 3210
		public const int CcInboundMime = 12;

		// Token: 0x04000C8B RID: 3211
		public const int CcInboundTnef = 13;

		// Token: 0x04000C8C RID: 3212
		public const int CcOutboundGeneric = 14;

		// Token: 0x04000C8D RID: 3213
		public const int CcOutboundMime = 15;

		// Token: 0x04000C8E RID: 3214
		public const int CcOutboundTnef = 16;

		// Token: 0x04000C8F RID: 3215
		public const int CcPFD = 17;

		// Token: 0x04000C90 RID: 3216
		public const int Session = 18;

		// Token: 0x04000C91 RID: 3217
		public const int DefaultFolders = 19;

		// Token: 0x04000C92 RID: 3218
		public const int UserConfiguration = 20;

		// Token: 0x04000C93 RID: 3219
		public const int PropertyBag = 21;

		// Token: 0x04000C94 RID: 3220
		public const int Task = 22;

		// Token: 0x04000C95 RID: 3221
		public const int Recurrence = 23;

		// Token: 0x04000C96 RID: 3222
		public const int WorkHours = 24;

		// Token: 0x04000C97 RID: 3223
		public const int Sync = 25;

		// Token: 0x04000C98 RID: 3224
		public const int ICal = 26;

		// Token: 0x04000C99 RID: 3225
		public const int ActiveManagerClient = 27;

		// Token: 0x04000C9A RID: 3226
		public const int CcOutboundVCard = 28;

		// Token: 0x04000C9B RID: 3227
		public const int CcInboundVCard = 29;

		// Token: 0x04000C9C RID: 3228
		public const int Sharing = 30;

		// Token: 0x04000C9D RID: 3229
		public const int RightsManagement = 31;

		// Token: 0x04000C9E RID: 3230
		public const int DatabaseAvailabilityGroup = 32;

		// Token: 0x04000C9F RID: 3231
		public const int FaultInjection = 33;

		// Token: 0x04000CA0 RID: 3232
		public const int SmtpService = 34;

		// Token: 0x04000CA1 RID: 3233
		public const int MapiConnectivity = 35;

		// Token: 0x04000CA2 RID: 3234
		public const int Xtc = 36;

		// Token: 0x04000CA3 RID: 3235
		public const int CalendarLogging = 38;

		// Token: 0x04000CA4 RID: 3236
		public const int CalendarSeries = 39;

		// Token: 0x04000CA5 RID: 3237
		public const int BirthdayCalendar = 40;

		// Token: 0x04000CA6 RID: 3238
		public const int PropertyMapping = 50;

		// Token: 0x04000CA7 RID: 3239
		public const int MdbResourceHealth = 51;

		// Token: 0x04000CA8 RID: 3240
		public const int ContactLinking = 52;

		// Token: 0x04000CA9 RID: 3241
		public const int UserPhotos = 53;

		// Token: 0x04000CAA RID: 3242
		public const int ContactFoldersEnumerator = 54;

		// Token: 0x04000CAB RID: 3243
		public const int MyContactsFolder = 55;

		// Token: 0x04000CAC RID: 3244
		public const int Aggregation = 56;

		// Token: 0x04000CAD RID: 3245
		public const int OutlookSocialConnectorInterop = 57;

		// Token: 0x04000CAE RID: 3246
		public const int Person = 58;

		// Token: 0x04000CAF RID: 3247
		public const int DatabasePinger = 60;

		// Token: 0x04000CB0 RID: 3248
		public const int ContactsEnumerator = 62;

		// Token: 0x04000CB1 RID: 3249
		public const int ContactChangeLogging = 63;

		// Token: 0x04000CB2 RID: 3250
		public const int ContactExporter = 64;

		// Token: 0x04000CB3 RID: 3251
		public const int SiteMailboxPermissionCheck = 70;

		// Token: 0x04000CB4 RID: 3252
		public const int SiteMailboxDocumentSync = 71;

		// Token: 0x04000CB5 RID: 3253
		public const int SiteMailboxMembershipSync = 72;

		// Token: 0x04000CB6 RID: 3254
		public const int SiteMailboxClientOperation = 73;

		// Token: 0x04000CB7 RID: 3255
		public const int SiteMailboxMessageDedup = 74;

		// Token: 0x04000CB8 RID: 3256
		public const int Reminders = 81;

		// Token: 0x04000CB9 RID: 3257
		public const int PeopleIKnow = 82;

		// Token: 0x04000CBA RID: 3258
		public const int AggregatedConversations = 83;

		// Token: 0x04000CBB RID: 3259
		public const int Delegate = 84;

		// Token: 0x04000CBC RID: 3260
		public const int GroupMailboxSession = 85;

		// Token: 0x04000CBD RID: 3261
		public const int SyncProcess = 86;

		// Token: 0x04000CBE RID: 3262
		public const int Conversation = 87;

		// Token: 0x04000CBF RID: 3263
		public const int ReliableTimer = 88;

		// Token: 0x04000CC0 RID: 3264
		public const int FavoritePublicFolders = 89;

		// Token: 0x04000CC1 RID: 3265
		public const int PublicFolders = 90;

		// Token: 0x04000CC2 RID: 3266
		public static Guid guid = new Guid("6d031d1d-5908-457a-a6c4-cdd0f6e74d81");
	}
}
