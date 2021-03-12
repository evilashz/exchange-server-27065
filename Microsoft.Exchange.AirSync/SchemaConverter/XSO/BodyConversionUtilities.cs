using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.SchemaConverter;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001F1 RID: 497
	internal static class BodyConversionUtilities
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x00070AF4 File Offset: 0x0006ECF4
		public static Stream ConvertToBodyStream(Item item, long truncationSize, out long totalDataSize, out IList<AttachmentLink> attachmentLinks)
		{
			Microsoft.Exchange.Data.Storage.BodyFormat format = item.Body.Format;
			switch (format)
			{
			case Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain:
				return BodyConversionUtilities.ConvertToPlainTextStream(item, truncationSize, out totalDataSize, out attachmentLinks);
			case Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml:
				return BodyConversionUtilities.ConvertHtmlStream(item, truncationSize, out totalDataSize, out attachmentLinks);
			case Microsoft.Exchange.Data.Storage.BodyFormat.ApplicationRtf:
				return BodyConversionUtilities.ConvertToRtfStream(item, truncationSize, out totalDataSize, out attachmentLinks);
			default:
				throw new ConversionException("Unsupported bodyFormat for this function: " + format);
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00070B58 File Offset: 0x0006ED58
		public static Stream ConvertToPlainTextStream(Item item, long truncationSizeByChars, out long totalDataSize, out IList<AttachmentLink> attachmentLinks)
		{
			BodyReadConfiguration configuration = new BodyReadConfiguration(Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain, "utf-8");
			AirSyncStream airSyncStream = new AirSyncStream();
			int num = 0;
			Body body = null;
			if (BodyConversionUtilities.IsMessageRestrictedAndDecoded(item))
			{
				body = ((RightsManagedMessageItem)item).ProtectedBody;
			}
			else
			{
				body = item.Body;
			}
			uint streamHash;
			using (Stream stream = body.OpenReadStream(configuration))
			{
				num = StreamHelper.CopyStream(stream, airSyncStream, Encoding.UTF8, (int)truncationSizeByChars, true, out streamHash);
			}
			airSyncStream.StreamHash = (int)streamHash;
			totalDataSize = ((truncationSizeByChars < 0L || (long)num < truncationSizeByChars) ? ((long)num) : body.Size);
			attachmentLinks = null;
			return airSyncStream;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00070BF8 File Offset: 0x0006EDF8
		public static Stream ConvertHtmlStream(Item item, long truncationSizeByChars, out long totalDataSize, out IList<AttachmentLink> attachmentLinks)
		{
			SafeHtmlCallback safeHtmlCallback = new SafeHtmlCallback(item);
			bool flag = truncationSizeByChars == -1L;
			BodyReadConfiguration bodyReadConfiguration;
			if (flag)
			{
				bodyReadConfiguration = new BodyReadConfiguration(Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml, "utf-8");
				bodyReadConfiguration.SetHtmlOptions(HtmlStreamingFlags.FilterHtml, safeHtmlCallback);
			}
			else
			{
				bodyReadConfiguration = new BodyReadConfiguration(Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml, "utf-8");
				bodyReadConfiguration.SetHtmlOptions(HtmlStreamingFlags.FilterHtml, safeHtmlCallback, new int?(1024));
			}
			AirSyncStream airSyncStream = new AirSyncStream();
			Body body = null;
			if (BodyConversionUtilities.IsMessageRestrictedAndDecoded(item))
			{
				body = ((RightsManagedMessageItem)item).ProtectedBody;
			}
			else
			{
				body = item.Body;
			}
			uint streamHash;
			using (Stream stream = body.OpenReadStream(bodyReadConfiguration))
			{
				StreamHelper.CopyStream(stream, airSyncStream, Encoding.UTF8, (int)truncationSizeByChars, true, out streamHash);
			}
			airSyncStream.StreamHash = (int)streamHash;
			totalDataSize = ((truncationSizeByChars < 0L || airSyncStream.Length < truncationSizeByChars) ? airSyncStream.Length : body.Size);
			attachmentLinks = safeHtmlCallback.AttachmentLinks;
			return airSyncStream;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00070CE0 File Offset: 0x0006EEE0
		public static Stream ConvertToRtfStream(Item item, long truncationSizeByBytes, out long totalDataSize, out IList<AttachmentLink> attachmentLinks)
		{
			BodyReadConfiguration configuration = new BodyReadConfiguration(Microsoft.Exchange.Data.Storage.BodyFormat.ApplicationRtf, "utf-8");
			AirSyncStream airSyncStream = new AirSyncStream();
			Body body = null;
			if (BodyConversionUtilities.IsMessageRestrictedAndDecoded(item))
			{
				body = ((RightsManagedMessageItem)item).ProtectedBody;
			}
			else
			{
				body = item.Body;
			}
			uint streamHash;
			using (Stream stream = body.OpenReadStream(configuration))
			{
				airSyncStream.DoBase64Conversion = true;
				StreamHelper.CopyStream(stream, airSyncStream, (int)truncationSizeByBytes, out streamHash);
			}
			airSyncStream.StreamHash = (int)streamHash;
			totalDataSize = ((truncationSizeByBytes < 0L || airSyncStream.Length < truncationSizeByBytes) ? airSyncStream.Length : body.Size);
			attachmentLinks = null;
			return airSyncStream;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00070D84 File Offset: 0x0006EF84
		internal static void CopyBody(Item sourceItem, Item targetItem)
		{
			using (Stream stream = sourceItem.Body.OpenReadStream(new BodyReadConfiguration(sourceItem.Body.Format, sourceItem.Body.Charset)))
			{
				using (Stream stream2 = targetItem.Body.OpenWriteStream(new BodyWriteConfiguration(sourceItem.Body.Format, sourceItem.Body.Charset)))
				{
					StreamHelper.CopyStream(stream, stream2);
				}
			}
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00070E1C File Offset: 0x0006F01C
		internal static void OnBeforeItemLoadInConversation(object sender, LoadItemEventArgs e)
		{
			e.HtmlStreamOptionCallback = new HtmlStreamOptionCallback(BodyConversionUtilities.GetSafeHtmlCallbacks);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00070E30 File Offset: 0x0006F030
		internal static void OnBeforeItemLoadInConversationForceOpen(object sender, LoadItemEventArgs e)
		{
			e.HtmlStreamOptionCallback = new HtmlStreamOptionCallback(BodyConversionUtilities.GetSafeHtmlCallbacks);
			e.MessagePropertyDefinitions = new PropertyDefinition[]
			{
				ItemSchema.Importance
			};
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00070E68 File Offset: 0x0006F068
		private static KeyValuePair<HtmlStreamingFlags, HtmlCallbackBase> GetSafeHtmlCallbacks(Item item)
		{
			SafeHtmlCallback value = new SafeHtmlCallback(item);
			return new KeyValuePair<HtmlStreamingFlags, HtmlCallbackBase>(HtmlStreamingFlags.FilterHtml, value);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00070E84 File Offset: 0x0006F084
		internal static bool IsMessageRestrictedAndDecoded(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted && rightsManagedMessageItem.IsDecoded)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.XsoTracer, item, "The message is restricted and decoded");
				return true;
			}
			return false;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00070ECC File Offset: 0x0006F0CC
		internal static bool IsIRMFailedToDecode(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null && rightsManagedMessageItem.DecryptionStatus.Failed && rightsManagedMessageItem.DecryptionStatus.FailureCode != RightsManagementFailureCode.InternalLicensingDisabled && rightsManagedMessageItem.DecryptionStatus.FailureCode != RightsManagementFailureCode.ExternalLicensingDisabled)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.XsoTracer, item, "The message is failed to decode");
				return true;
			}
			return false;
		}

		// Token: 0x04000C22 RID: 3106
		public const string OutputDisplayCharset = "utf-8";

		// Token: 0x04000C23 RID: 3107
		private const int CSSTruncationLimit = 1024;
	}
}
