using System;
using System.Globalization;
using System.IO;
using Microsoft.Ceres.ContentEngine.Fields.Stream;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200000D RID: 13
	public class ExchangeStream : WrappedNativeStream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004A28 File Offset: 0x00002C28
		public static Uri GenerateNewAttachmentUri(string flowIdentifier, Guid mdbGuid, Guid mailboxGuid, Guid correlationId, string attachmentPath, string attachmentFileName)
		{
			string uriString = string.Format("exchange://localhost/{0}/{1}/{2}/{3}/{4}/{5}/{6}", new object[]
			{
				"Attachment",
				flowIdentifier,
				mdbGuid,
				mailboxGuid,
				correlationId,
				attachmentPath,
				attachmentFileName
			});
			return new Uri(uriString);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004A7F File Offset: 0x00002C7F
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeStream>(this);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004A87 File Offset: 0x00002C87
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004A9C File Offset: 0x00002C9C
		public override void Init()
		{
			this.disposeTracker = this.GetDisposeTracker();
			Uri uri = base.Uri;
			ExTraceGlobals.RetrieverOperatorTracer.TraceDebug<string>((long)this.GetHashCode(), "Initializing ExchangeStream for URI: {0}", uri.AbsolutePath);
			if (uri.Segments.Length < 7)
			{
				throw new InvalidDataException("Exchange Stream URI was invalid.");
			}
			string text = ExchangeStream.RemoveTrailingSlash(uri.Segments[1]);
			if ("Attachment" == text)
			{
				string flowIdentifier = ExchangeStream.RemoveTrailingSlash(uri.Segments[2]);
				string input = ExchangeStream.RemoveTrailingSlash(uri.Segments[5]);
				string path = ExchangeStream.RemoveTrailingSlash(uri.Segments[6]);
				base.Stream = ExchangeStream.GetAttachmentStream(flowIdentifier, Guid.Parse(input), path);
				return;
			}
			throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Property '{0}' is not a supported Stream type.", new object[]
			{
				text
			}));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004B6C File Offset: 0x00002D6C
		private static Stream GetAttachmentStream(string flowIdentifier, Guid correlationId, string path)
		{
			ExTraceGlobals.RetrieverOperatorTracer.TraceDebug<string>(0L, "Getting Stream for: {0}", path);
			ExTraceGlobals.RetrieverOperatorTracer.TraceDebug<string>(0L, "    flowIdentifier: {0}", flowIdentifier);
			ExTraceGlobals.RetrieverOperatorTracer.TraceDebug<Guid>(0L, "     correlationId: {0}", correlationId);
			return (Stream)ItemDepot.Instance.GetItem(flowIdentifier, path);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private static string RemoveTrailingSlash(string segment)
		{
			if (!string.IsNullOrEmpty(segment) && segment[segment.Length - 1] == '/')
			{
				segment = segment.Substring(0, segment.Length - 1);
			}
			return segment;
		}

		// Token: 0x04000054 RID: 84
		internal const string AttachmentProperty = "Attachment";

		// Token: 0x04000055 RID: 85
		private DisposeTracker disposeTracker;
	}
}
