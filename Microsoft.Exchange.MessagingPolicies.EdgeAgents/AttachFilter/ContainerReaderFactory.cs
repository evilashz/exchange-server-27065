using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.AttachFilter
{
	// Token: 0x02000008 RID: 8
	internal class ContainerReaderFactory
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000347C File Offset: 0x0000167C
		internal static bool Create(AttachmentInfo attachmentInfo, out IEnumerable<string> reader)
		{
			reader = null;
			ContainerReaderFactory.ContainerType containerType = ContainerReaderFactory.GetContainerTypeFromName(attachmentInfo.Name);
			ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string>(0L, "Attachment container-type: {0}", Enum<ContainerReaderFactory.ContainerType>.ToString((int)containerType));
			if (attachmentInfo.ContentTypes.Contains("application/x-zip-compressed"))
			{
				if (containerType != ContainerReaderFactory.ContainerType.None && containerType != ContainerReaderFactory.ContainerType.Zip)
				{
					ExTraceGlobals.AttachmentFilteringTracer.TraceDebug<string, string>(0L, "The attachment name {0} indicates that this is a {1}, but the Content-Type (either present in the attachment, or sniffed from the content) indicates that it is a Zip. This is inconsistent information and the message will be rejected.", attachmentInfo.Name, Enum<ContainerReaderFactory.ContainerType>.ToString((int)containerType));
					return false;
				}
				containerType = ContainerReaderFactory.ContainerType.Zip;
			}
			switch (containerType)
			{
			case ContainerReaderFactory.ContainerType.Zip:
				reader = new ZipReader(attachmentInfo.Attachment.GetContentReadStream(), 15);
				return true;
			case ContainerReaderFactory.ContainerType.Lzh:
				reader = new LZHReader(attachmentInfo.Attachment.GetContentReadStream());
				return true;
			default:
				reader = null;
				return true;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000352C File Offset: 0x0000172C
		private static ContainerReaderFactory.ContainerType GetContainerTypeFromName(string attachmentName)
		{
			for (int i = 0; i < 2; i++)
			{
				if (attachmentName.EndsWith(ContainerReaderFactory.containerExtensions[i], StringComparison.OrdinalIgnoreCase))
				{
					return (ContainerReaderFactory.ContainerType)i;
				}
			}
			return ContainerReaderFactory.ContainerType.None;
		}

		// Token: 0x04000028 RID: 40
		private const int ZipNestedLevels = 15;

		// Token: 0x04000029 RID: 41
		private static string[] containerExtensions = new string[]
		{
			".zip",
			".lzh"
		};

		// Token: 0x02000009 RID: 9
		private enum ContainerType
		{
			// Token: 0x0400002B RID: 43
			Zip,
			// Token: 0x0400002C RID: 44
			Lzh,
			// Token: 0x0400002D RID: 45
			None
		}
	}
}
