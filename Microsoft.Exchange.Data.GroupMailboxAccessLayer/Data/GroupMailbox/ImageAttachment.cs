using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200003E RID: 62
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ImageAttachment
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public ImageAttachment(string imageName, string imageId, string contentType, byte[] bytes = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("imageName", imageName);
			ArgumentValidator.ThrowIfNullOrEmpty("imageId", imageId);
			ArgumentValidator.ThrowIfNullOrEmpty("contentType", contentType);
			this.ImageName = imageName;
			this.ImageId = imageId;
			this.contentType = contentType;
			this.bytes = (bytes ?? ImageAttachment.ReadImageFromEmbeddedResources(imageName));
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000CCEB File Offset: 0x0000AEEB
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000CCF3 File Offset: 0x0000AEF3
		public string ImageName { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000CD04 File Offset: 0x0000AF04
		public string ImageId { get; private set; }

		// Token: 0x060001C7 RID: 455 RVA: 0x0000CD10 File Offset: 0x0000AF10
		public void AddImageAsAttachment(MessageItem message)
		{
			ImageAttachment.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "ImageAttachment.AddImageAsAttachment: Image Name: {0}. Image Id: {1}", this.ImageName, this.ImageId);
			using (StreamAttachment streamAttachment = message.AttachmentCollection.Create(AttachmentType.Stream) as StreamAttachment)
			{
				using (Stream contentStream = streamAttachment.GetContentStream())
				{
					streamAttachment.FileName = this.ImageName;
					streamAttachment.ContentId = this.ImageId;
					streamAttachment.ContentType = this.contentType;
					contentStream.Write(this.bytes, 0, this.bytes.Length);
					streamAttachment.Save();
				}
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		private static byte[] ReadImageFromEmbeddedResources(string imageName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("imageName", imageName);
			byte[] array = null;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(imageName))
			{
				if (manifestResourceStream != null)
				{
					ImageAttachment.Tracer.TraceDebug<string>(0L, "Found image {0} as embedded resource.", imageName);
					array = new byte[manifestResourceStream.Length];
					int arg = manifestResourceStream.Read(array, 0, array.Length);
					ImageAttachment.Tracer.TraceDebug<int, string>(0L, "Read {0} bytes for image {1}.", arg, imageName);
				}
				else
				{
					ImageAttachment.Tracer.TraceError<string>(0L, "Couldn't find image {0} as embedded resource. Returning null.", imageName);
				}
			}
			return array;
		}

		// Token: 0x040000F4 RID: 244
		private static readonly Trace Tracer = ExTraceGlobals.GroupEmailNotificationHandlerTracer;

		// Token: 0x040000F5 RID: 245
		private readonly string contentType;

		// Token: 0x040000F6 RID: 246
		private readonly byte[] bytes;
	}
}
