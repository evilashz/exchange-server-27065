using System;
using System.IO;
using Microsoft.Exchange.Common.Sniff;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001C7 RID: 455
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADPhotoReader : IADPhotoReader
	{
		// Token: 0x0600114F RID: 4431 RVA: 0x00047C98 File Offset: 0x00045E98
		public ADPhotoReader(ITracer upstreamTracer)
		{
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00047CE8 File Offset: 0x00045EE8
		public PhotoMetadata Read(IRecipientSession session, ADObjectId recipientId, Stream output)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (recipientId == null)
			{
				throw new ArgumentNullException("recipientId");
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			ADRecipient adrecipient = this.FindRecipient(session, recipientId);
			if (adrecipient.ThumbnailPhoto == null || adrecipient.ThumbnailPhoto.Length == 0)
			{
				this.tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "AD photo reader: user {0} does NOT have a photo in AD.", recipientId);
				throw new ADNoSuchObjectException(Strings.ADUserNoPhoto(recipientId));
			}
			output.Write(adrecipient.ThumbnailPhoto, 0, adrecipient.ThumbnailPhoto.Length);
			string contentType;
			using (MemoryStream memoryStream = new MemoryStream(adrecipient.ThumbnailPhoto))
			{
				contentType = this.sniffer.FindMimeFromData(memoryStream);
			}
			return new PhotoMetadata
			{
				Length = (long)adrecipient.ThumbnailPhoto.Length,
				ContentType = contentType
			};
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00047DC8 File Offset: 0x00045FC8
		private ADRecipient FindRecipient(IRecipientSession session, ADObjectId recipientId)
		{
			ADRecipient adrecipient = session.Read(recipientId);
			if (adrecipient == null)
			{
				this.tracer.TraceError<ADObjectId>((long)this.GetHashCode(), "AD photo reader: user {0} not found in AD.", recipientId);
				throw new ADNoSuchObjectException(DirectoryStrings.ExceptionADOperationFailedNoSuchObject(session.DomainController, recipientId.DistinguishedName));
			}
			return adrecipient;
		}

		// Token: 0x0400092E RID: 2350
		private const int SnifferSampleSize = 128;

		// Token: 0x0400092F RID: 2351
		private DataSniff sniffer = new DataSniff(128);

		// Token: 0x04000930 RID: 2352
		private ITracer tracer = ExTraceGlobals.UserPhotosTracer;
	}
}
