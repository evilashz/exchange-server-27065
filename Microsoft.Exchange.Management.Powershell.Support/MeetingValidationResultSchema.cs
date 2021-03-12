using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000007 RID: 7
	internal sealed class MeetingValidationResultSchema : InMemoryObjectSchema
	{
		// Token: 0x04000025 RID: 37
		public static readonly SimplePropertyDefinition MeetingType = new SimplePropertyDefinition("MeetingType", typeof(string), string.Empty);

		// Token: 0x04000026 RID: 38
		public static readonly SimplePropertyDefinition ValidatingRole = new SimplePropertyDefinition("ValidatingRole", typeof(string), string.Empty);

		// Token: 0x04000027 RID: 39
		public static readonly SimplePropertyDefinition PrimarySmtpAddress = new SimplePropertyDefinition("PrimarySmtpAddress", typeof(SmtpAddress), null);

		// Token: 0x04000028 RID: 40
		public static readonly SimplePropertyDefinition IntervalStartDate = new SimplePropertyDefinition("IntervalStartDate", typeof(ExDateTime), null);

		// Token: 0x04000029 RID: 41
		public static readonly SimplePropertyDefinition IntervalEndDate = new SimplePropertyDefinition("IntervalEndDate", typeof(ExDateTime), null);

		// Token: 0x0400002A RID: 42
		public static readonly SimplePropertyDefinition StartTime = new SimplePropertyDefinition("StartTime", typeof(ExDateTime), null);

		// Token: 0x0400002B RID: 43
		public static readonly SimplePropertyDefinition EndTime = new SimplePropertyDefinition("EndTime", typeof(ExDateTime), null);

		// Token: 0x0400002C RID: 44
		public static readonly SimplePropertyDefinition ErrorDescription = new SimplePropertyDefinition("ErrorDescription", typeof(string), null);

		// Token: 0x0400002D RID: 45
		public static readonly SimplePropertyDefinition MeetingId = new SimplePropertyDefinition("MeetingId", typeof(string), null);

		// Token: 0x0400002E RID: 46
		public static readonly SimplePropertyDefinition GlobalObjectId = new SimplePropertyDefinition("GlobalObjectId", typeof(string), null);

		// Token: 0x0400002F RID: 47
		public static readonly SimplePropertyDefinition CleanGlobalObjectId = new SimplePropertyDefinition("CleanGlobalObjectId", typeof(string), null);

		// Token: 0x04000030 RID: 48
		public static readonly SimplePropertyDefinition CreationTime = new SimplePropertyDefinition("CreationTime", typeof(ExDateTime), null);

		// Token: 0x04000031 RID: 49
		public static readonly SimplePropertyDefinition LastModifiedTime = new SimplePropertyDefinition("LastModifiedTime", typeof(ExDateTime), null);

		// Token: 0x04000032 RID: 50
		public static readonly SimplePropertyDefinition Location = new SimplePropertyDefinition("Location", typeof(string), null);

		// Token: 0x04000033 RID: 51
		public static readonly SimplePropertyDefinition Subject = new SimplePropertyDefinition("Subject", typeof(string), null);

		// Token: 0x04000034 RID: 52
		public static readonly SimplePropertyDefinition Organizer = new SimplePropertyDefinition("Organizer", typeof(string), null);

		// Token: 0x04000035 RID: 53
		public static readonly SimplePropertyDefinition IsConsistent = new SimplePropertyDefinition("IsConsistent", typeof(bool), null);

		// Token: 0x04000036 RID: 54
		public static readonly SimplePropertyDefinition DuplicatesDetected = new SimplePropertyDefinition("DuplicatesDetected", typeof(bool), null);

		// Token: 0x04000037 RID: 55
		public static readonly SimplePropertyDefinition HasConflicts = new SimplePropertyDefinition("HasConflicts", typeof(bool), null);

		// Token: 0x04000038 RID: 56
		public static readonly SimplePropertyDefinition ExtractVersion = new SimplePropertyDefinition("ExtractVersion", typeof(long), null);

		// Token: 0x04000039 RID: 57
		public static readonly SimplePropertyDefinition ExtractTime = new SimplePropertyDefinition("ExtractTime", typeof(ExDateTime), null);

		// Token: 0x0400003A RID: 58
		public static readonly SimplePropertyDefinition NumDelegates = new SimplePropertyDefinition("NumDelegates", typeof(int), null);

		// Token: 0x0400003B RID: 59
		public static readonly SimplePropertyDefinition InternetMessageId = new SimplePropertyDefinition("InternetMessageId", typeof(string), null);

		// Token: 0x0400003C RID: 60
		public static readonly SimplePropertyDefinition SequenceNumber = new SimplePropertyDefinition("SequenceNumber", typeof(int), null);

		// Token: 0x0400003D RID: 61
		public static readonly SimplePropertyDefinition OwnerApptId = new SimplePropertyDefinition("OwnerApptId", typeof(int), null);

		// Token: 0x0400003E RID: 62
		public static readonly SimplePropertyDefinition OwnerCriticalChangeTime = new SimplePropertyDefinition("OwnerCriticalChangeTime", typeof(ExDateTime), null);

		// Token: 0x0400003F RID: 63
		public static readonly SimplePropertyDefinition AttendeeCriticalChangeTime = new SimplePropertyDefinition("AttendeeCriticalChangeTime", typeof(ExDateTime), null);

		// Token: 0x04000040 RID: 64
		public static readonly SimplePropertyDefinition WasValidationSuccessful = new SimplePropertyDefinition("WasValidationSuccessful", typeof(bool), null);
	}
}
