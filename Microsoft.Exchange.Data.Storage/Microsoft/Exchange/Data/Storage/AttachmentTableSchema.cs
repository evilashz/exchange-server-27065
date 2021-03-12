using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C17 RID: 3095
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AttachmentTableSchema : Schema
	{
		// Token: 0x17001DDD RID: 7645
		// (get) Token: 0x06006E4E RID: 28238 RVA: 0x001DA671 File Offset: 0x001D8871
		public new static AttachmentTableSchema Instance
		{
			get
			{
				if (AttachmentTableSchema.instance == null)
				{
					AttachmentTableSchema.instance = new AttachmentTableSchema();
				}
				return AttachmentTableSchema.instance;
			}
		}

		// Token: 0x04004081 RID: 16513
		private static AttachmentTableSchema instance = null;

		// Token: 0x04004082 RID: 16514
		[Autoload]
		internal static readonly StorePropertyDefinition RecordKey = AttachmentSchema.RecordKey;

		// Token: 0x04004083 RID: 16515
		[Autoload]
		internal static readonly StorePropertyDefinition AttachNum = AttachmentSchema.AttachNum;

		// Token: 0x04004084 RID: 16516
		[Autoload]
		internal static readonly StorePropertyDefinition AttachFileName = AttachmentSchema.AttachFileName;

		// Token: 0x04004085 RID: 16517
		[Autoload]
		internal static readonly StorePropertyDefinition AttachLongFileName = AttachmentSchema.AttachLongFileName;

		// Token: 0x04004086 RID: 16518
		[Autoload]
		internal static readonly StorePropertyDefinition AttachSize = AttachmentSchema.AttachSize;

		// Token: 0x04004087 RID: 16519
		[Autoload]
		internal static readonly StorePropertyDefinition AttachInConflict = InternalSchema.AttachInConflict;

		// Token: 0x04004088 RID: 16520
		[Autoload]
		internal static readonly StorePropertyDefinition DisplayName = AttachmentSchema.DisplayName;

		// Token: 0x04004089 RID: 16521
		[Autoload]
		internal static readonly StorePropertyDefinition AttachMimTag = AttachmentSchema.AttachMimeTag;

		// Token: 0x0400408A RID: 16522
		[Autoload]
		internal static readonly StorePropertyDefinition AttachMethod = AttachmentSchema.AttachMethod;

		// Token: 0x0400408B RID: 16523
		[Autoload]
		internal static readonly StorePropertyDefinition AttachContentId = AttachmentSchema.AttachContentId;

		// Token: 0x0400408C RID: 16524
		[Autoload]
		internal static readonly StorePropertyDefinition AttachContentLocation = AttachmentSchema.AttachContentLocation;

		// Token: 0x0400408D RID: 16525
		[Autoload]
		internal static readonly StorePropertyDefinition AttachCalendarFlags = AttachmentSchema.AttachCalendarFlags;

		// Token: 0x0400408E RID: 16526
		[Autoload]
		internal static readonly StorePropertyDefinition AttachCalendarHidden = AttachmentSchema.AttachCalendarHidden;

		// Token: 0x0400408F RID: 16527
		[Autoload]
		internal static readonly StorePropertyDefinition AppointmentExceptionStartTime = AttachmentSchema.AppointmentExceptionStartTime;

		// Token: 0x04004090 RID: 16528
		[Autoload]
		internal static readonly StorePropertyDefinition AppointmentExceptionEndTime = AttachmentSchema.AppointmentExceptionEndTime;

		// Token: 0x04004091 RID: 16529
		[Autoload]
		internal static readonly StorePropertyDefinition AttachContentBase = AttachmentSchema.AttachContentBase;

		// Token: 0x04004092 RID: 16530
		[Autoload]
		internal static readonly StorePropertyDefinition AttachMhtmlFlags = AttachmentSchema.AttachMhtmlFlags;

		// Token: 0x04004093 RID: 16531
		[Autoload]
		internal static readonly StorePropertyDefinition IsContactPhoto = AttachmentSchema.IsContactPhoto;

		// Token: 0x04004094 RID: 16532
		[Autoload]
		internal static readonly StorePropertyDefinition RenderingPosition = AttachmentSchema.RenderingPosition;

		// Token: 0x04004095 RID: 16533
		[Autoload]
		internal static readonly StorePropertyDefinition AttachEncoding = AttachmentSchema.AttachEncoding;

		// Token: 0x04004096 RID: 16534
		[Autoload]
		internal static readonly StorePropertyDefinition TextAttachmentCharset = InternalSchema.TextAttachmentCharset;
	}
}
