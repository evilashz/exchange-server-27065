using System;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001CB RID: 459
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ADPhotoWriter : IADPhotoWriter
	{
		// Token: 0x0600115D RID: 4445 RVA: 0x00048154 File Offset: 0x00046354
		public ADPhotoWriter(IRecipientSession session, ITracer upstreamTracer)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.session = session;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000481A6 File Offset: 0x000463A6
		public void Write(ADObjectId recipientId, Stream photo)
		{
			if (recipientId == null)
			{
				throw new ArgumentNullException("recipientId");
			}
			if (photo == null || photo.Length == 0L)
			{
				this.Clear(recipientId);
				return;
			}
			this.WritePhoto(recipientId, photo);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000481D4 File Offset: 0x000463D4
		private void WritePhoto(ADObjectId recipientId, Stream photo)
		{
			ADRecipient adrecipient = this.FindRecipient(recipientId);
			if (adrecipient == null)
			{
				this.tracer.TraceError<ADObjectId>((long)this.GetHashCode(), "AD photo writer: user {0} not found in AD.", recipientId);
				throw new ADNoSuchObjectException(DirectoryStrings.ExceptionADOperationFailedNoSuchObject(this.session.DomainController, recipientId.DistinguishedName));
			}
			using (MemoryStream memoryStream = new MemoryStream())
			{
				photo.CopyTo(memoryStream);
				adrecipient.ThumbnailPhoto = memoryStream.ToArray();
				this.tracer.TraceDebug<long, ADRecipient>((long)this.GetHashCode(), "AD photo writer: saving photo with length {0} bytes to AD user {1}", memoryStream.Length, adrecipient);
				this.session.Save(adrecipient);
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00048280 File Offset: 0x00046480
		private void Clear(ADObjectId recipientId)
		{
			ADRecipient adrecipient = this.FindRecipient(recipientId);
			if (adrecipient == null)
			{
				this.tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "AD photo writer: request to clear photo of user {0} ignored because user could not be found in AD.", recipientId);
				return;
			}
			adrecipient.ThumbnailPhoto = null;
			this.tracer.TraceDebug<ADRecipient>((long)this.GetHashCode(), "AD photo writer: clearing photo of AD user {0}", adrecipient);
			this.session.Save(adrecipient);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x000482DC File Offset: 0x000464DC
		private ADRecipient FindRecipient(ADObjectId recipientId)
		{
			return this.session.Read(recipientId);
		}

		// Token: 0x04000936 RID: 2358
		private ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x04000937 RID: 2359
		private readonly IRecipientSession session;
	}
}
