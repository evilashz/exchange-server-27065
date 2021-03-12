using System;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000A2 RID: 162
	[Flags]
	public enum ParameterId
	{
		// Token: 0x0400053F RID: 1343
		Unknown = 1,
		// Token: 0x04000540 RID: 1344
		AlternateRepresentation = 2,
		// Token: 0x04000541 RID: 1345
		CommonName = 4,
		// Token: 0x04000542 RID: 1346
		CalendarUserType = 8,
		// Token: 0x04000543 RID: 1347
		Delegator = 16,
		// Token: 0x04000544 RID: 1348
		Delegatee = 32,
		// Token: 0x04000545 RID: 1349
		Directory = 64,
		// Token: 0x04000546 RID: 1350
		Encoding = 128,
		// Token: 0x04000547 RID: 1351
		FormatType = 256,
		// Token: 0x04000548 RID: 1352
		FreeBusyType = 512,
		// Token: 0x04000549 RID: 1353
		Language = 1024,
		// Token: 0x0400054A RID: 1354
		Membership = 2048,
		// Token: 0x0400054B RID: 1355
		ParticipationStatus = 4096,
		// Token: 0x0400054C RID: 1356
		RecurrenceRange = 8192,
		// Token: 0x0400054D RID: 1357
		TriggerRelationship = 16384,
		// Token: 0x0400054E RID: 1358
		RelationshipType = 32768,
		// Token: 0x0400054F RID: 1359
		ParticipationRole = 65536,
		// Token: 0x04000550 RID: 1360
		RsvpExpectation = 131072,
		// Token: 0x04000551 RID: 1361
		SentBy = 262144,
		// Token: 0x04000552 RID: 1362
		TimeZoneId = 524288,
		// Token: 0x04000553 RID: 1363
		ValueType = 1048576
	}
}
