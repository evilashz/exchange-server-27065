using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000297 RID: 663
	public struct ManagedStore_MapiTags
	{
		// Token: 0x040011A5 RID: 4517
		public const int General = 0;

		// Token: 0x040011A6 RID: 4518
		public const int SchemaMapEntryAdded = 1;

		// Token: 0x040011A7 RID: 4519
		public const int SchemaMapEntryUpdated = 2;

		// Token: 0x040011A8 RID: 4520
		public const int PropertyMapping = 3;

		// Token: 0x040011A9 RID: 4521
		public const int GetPropsProperties = 4;

		// Token: 0x040011AA RID: 4522
		public const int SetPropsProperties = 5;

		// Token: 0x040011AB RID: 4523
		public const int DeletePropsProperties = 6;

		// Token: 0x040011AC RID: 4524
		public const int CopyOperations = 7;

		// Token: 0x040011AD RID: 4525
		public const int StreamOperations = 8;

		// Token: 0x040011AE RID: 4526
		public const int AttachmentOperations = 9;

		// Token: 0x040011AF RID: 4527
		public const int Notification = 10;

		// Token: 0x040011B0 RID: 4528
		public const int CreateLogon = 11;

		// Token: 0x040011B1 RID: 4529
		public const int CreateSession = 12;

		// Token: 0x040011B2 RID: 4530
		public const int SubmitMessage = 13;

		// Token: 0x040011B3 RID: 4531
		public const int AccessCheck = 14;

		// Token: 0x040011B4 RID: 4532
		public const int TimedEvents = 15;

		// Token: 0x040011B5 RID: 4533
		public const int DeferredSend = 16;

		// Token: 0x040011B6 RID: 4534
		public const int MailboxSignature = 17;

		// Token: 0x040011B7 RID: 4535
		public const int Quota = 18;

		// Token: 0x040011B8 RID: 4536
		public const int FillRow = 19;

		// Token: 0x040011B9 RID: 4537
		public const int SecurityContextManager = 20;

		// Token: 0x040011BA RID: 4538
		public const int InTransitTransitions = 21;

		// Token: 0x040011BB RID: 4539
		public const int Restrict = 22;

		// Token: 0x040011BC RID: 4540
		public const int FaultInjection = 30;

		// Token: 0x040011BD RID: 4541
		public const int EnableBadItemInjection = 31;

		// Token: 0x040011BE RID: 4542
		public const int CreateMailbox = 32;

		// Token: 0x040011BF RID: 4543
		public static Guid guid = new Guid("7927e3f9-b2bc-461f-96e7-c78d73ed4f04");
	}
}
