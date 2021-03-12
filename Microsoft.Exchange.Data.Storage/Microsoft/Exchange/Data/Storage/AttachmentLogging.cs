using System;
using Microsoft.Exchange.Data.ImageAnalysis;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000351 RID: 849
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AttachmentLogging
	{
		// Token: 0x060025D4 RID: 9684 RVA: 0x000979B8 File Offset: 0x00095BB8
		internal static void LogImageAnalysisResultCode(ImageAnalysisResult resultCode, ImageAnalysisLogData logData)
		{
			AttachmentProcessingLogger.Initialize();
			switch (resultCode)
			{
			case ImageAnalysisResult.ThumbnailSuccess:
				AttachmentProcessingLogger.LogEvent("StorageAttachmentImageAnalysis", "ThumbnailResult", "Success", "ThumbnailOperationTime", logData.operationTimeMs.ToString());
				AttachmentProcessingLogger.LogEvent("StorageAttachmentImageAnalysis", "ThumbnailSize", logData.thumbnailSize.ToString());
				AttachmentProcessingLogger.LogEvent("StorageAttachmentImageAnalysis", "ThumbnailWidth", logData.thumbnailWidth.ToString(), "ThumbnailHeight", logData.thumbnailHeight.ToString());
				return;
			case ImageAnalysisResult.SalientRegionSuccess:
				AttachmentProcessingLogger.LogEvent("StorageAttachmentImageAnalysis", "SalientRegionResult", "Success", "SalientRegionOperationTime", logData.operationTimeMs.ToString());
				return;
			case ImageAnalysisResult.UnknownFailure:
				AttachmentProcessingLogger.LogEvent("StorageAttachmentImageAnalysis", "Failure", logData.operationTimeMs.ToString());
				return;
			case ImageAnalysisResult.ImageTooSmallForAnalysis:
				AttachmentProcessingLogger.LogEvent("StorageAttachmentImageAnalysis", "ThumbnailResult", "ImageTooSmall", "ThumbnailOperationTime", logData.operationTimeMs.ToString());
				return;
			case ImageAnalysisResult.UnableToPerformSalientRegionAnalysis:
				AttachmentProcessingLogger.LogEvent("StorageAttachmentImageAnalysis", "SalientRegionResult", "Failure", "SalientRegionOperationTime", logData.operationTimeMs.ToString());
				return;
			case ImageAnalysisResult.ImageTooBigForAnalysis:
				AttachmentProcessingLogger.LogEvent("StorageAttachmentImageAnalysis", "ThumbnailResult", "ImageTooBigForAnalysis", "ImageSize", logData.thumbnailSize.ToString());
				return;
			default:
				return;
			}
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x00097B0B File Offset: 0x00095D0B
		internal static void LogImageAnalysisException(string callStack)
		{
			AttachmentProcessingLogger.Initialize();
			AttachmentProcessingLogger.LogEvent("StorageAttachmentImageAnalysis", "ThumbnailException", callStack);
		}

		// Token: 0x040016C5 RID: 5829
		private const string ImageAnalysisLogKeyword = "StorageAttachmentImageAnalysis";

		// Token: 0x040016C6 RID: 5830
		private const string ThumbnailResultKeyword = "ThumbnailResult";

		// Token: 0x040016C7 RID: 5831
		private const string ThumbnailResultTimeKeyword = "ThumbnailOperationTime";

		// Token: 0x040016C8 RID: 5832
		private const string SalientRegionResultKeyword = "SalientRegionResult";

		// Token: 0x040016C9 RID: 5833
		private const string SalientRegionResultTimeKeyword = "SalientRegionOperationTime";
	}
}
