using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C16 RID: 3094
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AttachmentSchema : Schema
	{
		// Token: 0x17001DDC RID: 7644
		// (get) Token: 0x06006E4A RID: 28234 RVA: 0x001DA463 File Offset: 0x001D8663
		public new static AttachmentSchema Instance
		{
			get
			{
				if (AttachmentSchema.instance == null)
				{
					AttachmentSchema.instance = new AttachmentSchema();
				}
				return AttachmentSchema.instance;
			}
		}

		// Token: 0x06006E4B RID: 28235 RVA: 0x001DA47B File Offset: 0x001D867B
		internal virtual void CoreObjectUpdate(CoreAttachment coreAttachment)
		{
		}

		// Token: 0x04004051 RID: 16465
		[Autoload]
		public static readonly StorePropertyDefinition IsInline = InternalSchema.AttachmentIsInline;

		// Token: 0x04004052 RID: 16466
		[Autoload]
		internal static readonly StorePropertyDefinition AppointmentExceptionEndTime = InternalSchema.AppointmentExceptionEndTime;

		// Token: 0x04004053 RID: 16467
		[Autoload]
		internal static readonly StorePropertyDefinition HasDlpDetectedClassifications = InternalSchema.HasDlpDetectedClassifications;

		// Token: 0x04004054 RID: 16468
		[Autoload]
		internal static readonly StorePropertyDefinition AppointmentExceptionStartTime = InternalSchema.AppointmentExceptionStartTime;

		// Token: 0x04004055 RID: 16469
		[Autoload]
		internal static readonly StorePropertyDefinition AttachCalendarFlags = InternalSchema.AttachCalendarFlags;

		// Token: 0x04004056 RID: 16470
		[Autoload]
		public static readonly StorePropertyDefinition AttachCalendarHidden = InternalSchema.AttachCalendarHidden;

		// Token: 0x04004057 RID: 16471
		[Autoload]
		internal static readonly StorePropertyDefinition AttachCalendarLinkId = InternalSchema.AttachCalendarLinkId;

		// Token: 0x04004058 RID: 16472
		[Autoload]
		internal static readonly StorePropertyDefinition FailedInboundICalAsAttachment = InternalSchema.FailedInboundICalAsAttachment;

		// Token: 0x04004059 RID: 16473
		[Autoload]
		public static readonly StorePropertyDefinition AttachExtension = InternalSchema.AttachExtension;

		// Token: 0x0400405A RID: 16474
		[Autoload]
		public static readonly StorePropertyDefinition AttachFileName = InternalSchema.AttachFileName;

		// Token: 0x0400405B RID: 16475
		public static readonly StorePropertyDefinition AttachAdditionalInfo = InternalSchema.AttachAdditionalInfo;

		// Token: 0x0400405C RID: 16476
		[Autoload]
		public static readonly StorePropertyDefinition AttachLongFileName = InternalSchema.AttachLongFileName;

		// Token: 0x0400405D RID: 16477
		[Autoload]
		public static readonly StorePropertyDefinition AttachLongPathName = InternalSchema.AttachLongPathName;

		// Token: 0x0400405E RID: 16478
		[Autoload]
		public static readonly StorePropertyDefinition IsContactPhoto = InternalSchema.IsContactPhoto;

		// Token: 0x0400405F RID: 16479
		[Autoload]
		public static readonly StorePropertyDefinition AttachMethod = InternalSchema.AttachMethod;

		// Token: 0x04004060 RID: 16480
		[Autoload]
		internal static readonly StorePropertyDefinition AttachMhtmlFlags = InternalSchema.AttachMhtmlFlags;

		// Token: 0x04004061 RID: 16481
		[Autoload]
		public static readonly StorePropertyDefinition AttachNum = InternalSchema.AttachNum;

		// Token: 0x04004062 RID: 16482
		[Autoload]
		public static readonly StorePropertyDefinition CreationTime = InternalSchema.CreationTime;

		// Token: 0x04004063 RID: 16483
		[DetectCodepage]
		public static readonly StorePropertyDefinition DisplayName = InternalSchema.DisplayName;

		// Token: 0x04004064 RID: 16484
		[Autoload]
		public static readonly StorePropertyDefinition LastModifiedTime = InternalSchema.LastModifiedTime;

		// Token: 0x04004065 RID: 16485
		[Autoload]
		internal static readonly StorePropertyDefinition OriginalMimeReadTime = InternalSchema.OriginalMimeReadTime;

		// Token: 0x04004066 RID: 16486
		[Autoload]
		internal static readonly StorePropertyDefinition RecordKey = InternalSchema.RecordKey;

		// Token: 0x04004067 RID: 16487
		[Autoload]
		public static readonly StorePropertyDefinition AttachContentBase = InternalSchema.AttachContentBase;

		// Token: 0x04004068 RID: 16488
		[Autoload]
		public static readonly StorePropertyDefinition AttachContentId = InternalSchema.AttachContentId;

		// Token: 0x04004069 RID: 16489
		[Autoload]
		public static readonly StorePropertyDefinition AttachContentLocation = InternalSchema.AttachContentLocation;

		// Token: 0x0400406A RID: 16490
		[Autoload]
		internal static readonly StorePropertyDefinition AttachEncoding = InternalSchema.AttachEncoding;

		// Token: 0x0400406B RID: 16491
		[Autoload]
		internal static readonly StorePropertyDefinition AttachSize = InternalSchema.AttachSize;

		// Token: 0x0400406C RID: 16492
		[Autoload]
		internal static readonly StorePropertyDefinition AttachInConflict = InternalSchema.AttachInConflict;

		// Token: 0x0400406D RID: 16493
		[Autoload]
		internal static readonly StorePropertyDefinition AttachMimeTag = InternalSchema.AttachMimeTag;

		// Token: 0x0400406E RID: 16494
		[Autoload]
		internal static readonly StorePropertyDefinition AttachmentMacInfo = InternalSchema.AttachmentMacInfo;

		// Token: 0x0400406F RID: 16495
		[Autoload]
		internal static readonly StorePropertyDefinition AttachmentMacContentType = InternalSchema.AttachmentMacContentType;

		// Token: 0x04004070 RID: 16496
		[Autoload]
		internal static readonly StorePropertyDefinition RenderingPosition = InternalSchema.RenderingPosition;

		// Token: 0x04004071 RID: 16497
		[Autoload]
		public static readonly StorePropertyDefinition AttachDataBin = InternalSchema.AttachDataBin;

		// Token: 0x04004072 RID: 16498
		[Autoload]
		public static readonly PropertyDefinition TextAttachmentCharset = InternalSchema.TextAttachmentCharset;

		// Token: 0x04004073 RID: 16499
		[Autoload]
		public static readonly StorePropertyDefinition AttachRendering = InternalSchema.AttachRendering;

		// Token: 0x04004074 RID: 16500
		[Autoload]
		internal static readonly StorePropertyDefinition ItemClass = InternalSchema.ItemClass;

		// Token: 0x04004075 RID: 16501
		[Autoload]
		public static readonly StorePropertyDefinition DRMServerLicenseCompressed = InternalSchema.DRMServerLicenseCompressed;

		// Token: 0x04004076 RID: 16502
		[Autoload]
		public static readonly StorePropertyDefinition DRMRights = InternalSchema.DRMRights;

		// Token: 0x04004077 RID: 16503
		[Autoload]
		public static readonly StorePropertyDefinition DRMExpiryTime = InternalSchema.DRMExpiryTime;

		// Token: 0x04004078 RID: 16504
		[Autoload]
		public static readonly StorePropertyDefinition DRMPropsSignature = InternalSchema.DRMPropsSignature;

		// Token: 0x04004079 RID: 16505
		[Autoload]
		public static readonly StorePropertyDefinition AttachHash = InternalSchema.AttachHash;

		// Token: 0x0400407A RID: 16506
		[Autoload]
		public static readonly StorePropertyDefinition AttachmentProviderEndpointUrl = InternalSchema.AttachmentProviderEndpointUrl;

		// Token: 0x0400407B RID: 16507
		[Autoload]
		public static readonly StorePropertyDefinition AttachmentProviderType = InternalSchema.AttachmentProviderType;

		// Token: 0x0400407C RID: 16508
		public static readonly StorePropertyDefinition ImageThumbnail = InternalSchema.ImageThumbnail;

		// Token: 0x0400407D RID: 16509
		public static readonly StorePropertyDefinition ImageThumbnailSalientRegions = InternalSchema.ImageThumbnailSalientRegions;

		// Token: 0x0400407E RID: 16510
		public static readonly StorePropertyDefinition ImageThumbnailHeight = InternalSchema.ImageThumbnailHeight;

		// Token: 0x0400407F RID: 16511
		public static readonly StorePropertyDefinition ImageThumbnailWidth = InternalSchema.ImageThumbnailWidth;

		// Token: 0x04004080 RID: 16512
		private static AttachmentSchema instance = null;
	}
}
