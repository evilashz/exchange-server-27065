using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Common.Sniff;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.AttachFilter
{
	// Token: 0x0200000A RID: 10
	internal class AttachmentInfo
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000358A File Offset: 0x0000178A
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00003592 File Offset: 0x00001792
		internal List<string> ContentTypes
		{
			get
			{
				return this.contentTypes;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000359A File Offset: 0x0000179A
		internal Attachment Attachment
		{
			get
			{
				return this.attachment;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000035A4 File Offset: 0x000017A4
		internal static AttachmentInfo BuildInfo(Attachment attachment)
		{
			AttachmentInfo attachmentInfo = new AttachmentInfo();
			attachmentInfo.attachment = attachment;
			attachmentInfo.name = attachment.FileName;
			ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string>((long)attachmentInfo.GetHashCode(), "Attachment name: {0}", attachmentInfo.name);
			if (attachmentInfo.name.Length > AttachmentInfo.MaxPath)
			{
				ExTraceGlobals.AttachmentFilteringTracer.TraceDebug((long)attachmentInfo.GetHashCode(), "The attachment name is too long");
				return null;
			}
			string contentType = attachment.ContentType;
			if (!string.IsNullOrEmpty(contentType))
			{
				ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string>((long)attachmentInfo.GetHashCode(), "Content-Type from MIME header: {0}", contentType);
				attachmentInfo.contentTypes.Add(contentType);
			}
			Stream file;
			if (!attachment.TryGetContentReadStream(out file))
			{
				return null;
			}
			string text = attachmentInfo.sniffer.FindMimeFromData(file);
			if (!string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string>((long)attachmentInfo.GetHashCode(), "Sniffed Content-Type: {0}", text);
				if (!text.Equals(contentType, StringComparison.OrdinalIgnoreCase))
				{
					attachmentInfo.contentTypes.Add(text);
				}
			}
			return attachmentInfo;
		}

		// Token: 0x0400002E RID: 46
		private const int SnifferSampleSize = 64;

		// Token: 0x0400002F RID: 47
		private static readonly int MaxPath = 260;

		// Token: 0x04000030 RID: 48
		private Attachment attachment;

		// Token: 0x04000031 RID: 49
		private string name;

		// Token: 0x04000032 RID: 50
		private List<string> contentTypes = new List<string>();

		// Token: 0x04000033 RID: 51
		private DataSniff sniffer = new DataSniff(64);
	}
}
