using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000359 RID: 857
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OleAttachment : StreamAttachmentBase
	{
		// Token: 0x06002629 RID: 9769 RVA: 0x00098C20 File Offset: 0x00096E20
		internal OleAttachment(CoreAttachment coreAttachment) : base(coreAttachment)
		{
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00098C29 File Offset: 0x00096E29
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<OleAttachment>(this);
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x00098C34 File Offset: 0x00096E34
		public bool TryConvertToImage(Stream outStream, ImageFormat format)
		{
			bool result;
			try
			{
				this.ConvertToImage(outStream, format);
				result = true;
			}
			catch (OleConversionFailedException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00098C64 File Offset: 0x00096E64
		public Stream TryConvertToImage(ImageFormat format)
		{
			Stream result;
			try
			{
				result = this.ConvertToImage(format);
			}
			catch (OleConversionFailedException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x00098C94 File Offset: 0x00096E94
		public Stream ConvertToImage(ImageFormat format)
		{
			MemoryStream memoryStream = new MemoryStream();
			bool flag = true;
			Stream result;
			try
			{
				this.ConvertToImage(memoryStream, format);
				flag = false;
				memoryStream.Position = 0L;
				result = memoryStream;
			}
			finally
			{
				if (flag)
				{
					memoryStream.Dispose();
				}
			}
			return result;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x00098CDC File Offset: 0x00096EDC
		internal override Attachment CreateCopy(AttachmentCollection collection, BodyFormat? targetBodyFormat)
		{
			bool flag = targetBodyFormat != null && targetBodyFormat != BodyFormat.ApplicationRtf;
			if (flag)
			{
				return this.ConvertToImageAttachment(collection.CoreAttachmentCollection, ImageFormat.Jpeg);
			}
			return (Attachment)Attachment.CreateCopy(this, collection, new AttachmentType?(AttachmentType.Ole));
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00098D34 File Offset: 0x00096F34
		public StreamAttachment ConvertToImageAttachment(CoreAttachmentCollection collection, ImageFormat format)
		{
			base.CheckDisposed("ConvertToImageAttachment");
			Util.ThrowOnNullArgument(collection, "collection");
			EnumValidator.ThrowIfInvalid<ImageFormat>(format);
			StreamAttachment result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				CoreAttachment coreAttachment = collection.InternalCreateCopy(new AttachmentType?(AttachmentType.Stream), base.CoreAttachment);
				disposeGuard.Add<CoreAttachment>(coreAttachment);
				StreamAttachment streamAttachment = (StreamAttachment)AttachmentCollection.CreateTypedAttachment(coreAttachment, new AttachmentType?(AttachmentType.Stream));
				disposeGuard.Add<StreamAttachment>(streamAttachment);
				string text = streamAttachment.FileName;
				if (string.IsNullOrEmpty(text))
				{
					text = Attachment.GenerateFilename();
				}
				string str = null;
				switch (format)
				{
				case ImageFormat.Jpeg:
					str = ".jpg";
					break;
				case ImageFormat.Png:
					str = ".png";
					break;
				}
				streamAttachment.FileName = text + str;
				streamAttachment.ContentType = "image/jpeg";
				streamAttachment.IsInline = true;
				using (Stream contentStream = streamAttachment.GetContentStream(PropertyOpenMode.Create))
				{
					if (!this.TryConvertToImage(contentStream, ImageFormat.Jpeg))
					{
						ConvertUtils.SaveDefaultImage(contentStream);
					}
				}
				disposeGuard.Success();
				result = streamAttachment;
			}
			return result;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x00098E5C File Offset: 0x0009705C
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			base.PropertyBag[InternalSchema.AttachMethod] = 6;
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x00098E7A File Offset: 0x0009707A
		protected override PropertyTagPropertyDefinition ContentStreamProperty
		{
			get
			{
				return InternalSchema.AttachDataObj;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x00098E81 File Offset: 0x00097081
		public override AttachmentType AttachmentType
		{
			get
			{
				base.CheckDisposed("AttachmentType::get");
				return AttachmentType.Ole;
			}
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x00098E90 File Offset: 0x00097090
		public void ConvertToImage(Stream outStream, ImageFormat format)
		{
			EnumValidator.ThrowIfInvalid<ImageFormat>(format, "format");
			using (Stream stream = this.ConvertToBitmap())
			{
				try
				{
					using (Image image = Image.FromStream(stream))
					{
						try
						{
							switch (format)
							{
							case ImageFormat.Jpeg:
								image.Save(outStream, ImageFormat.Jpeg);
								break;
							case ImageFormat.Png:
								image.Save(outStream, ImageFormat.Png);
								break;
							}
						}
						catch (ExternalException innerException)
						{
							StorageGlobals.ContextTraceError(ExTraceGlobals.CcOleTracer, "OleAttachment::ConvertToImage: result stream is corrupt.");
							throw new OleConversionFailedException(ServerStrings.OleConversionFailed, innerException);
						}
					}
				}
				catch (ArgumentException innerException2)
				{
					StorageGlobals.ContextTraceError(ExTraceGlobals.CcOleTracer, "OleAttachment::ConvertToImage: result stream is corrupt.");
					throw new OleConversionFailedException(ServerStrings.OleConversionFailed, innerException2);
				}
			}
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00098F6C File Offset: 0x0009716C
		private Stream ConvertToBitmap()
		{
			Stream stream = null;
			using (StorageGlobals.SetTraceContext(this))
			{
				if (StandaloneFuzzing.IsEnabled)
				{
					using (Bitmap bitmap = new Bitmap(1, 1))
					{
						stream = new MemoryStream();
						bitmap.Save(stream, ImageFormat.Jpeg);
						goto IL_5C;
					}
				}
				OleConverter instance = OleConverter.Instance;
				using (Stream contentStream = this.GetContentStream(PropertyOpenMode.ReadOnly))
				{
					stream = instance.ConvertToBitmap(contentStream);
				}
				IL_5C:;
			}
			return stream;
		}

		// Token: 0x040016EA RID: 5866
		internal const string JpegExtension = ".jpg";

		// Token: 0x040016EB RID: 5867
		internal const string PngExtension = ".png";

		// Token: 0x040016EC RID: 5868
		internal const int AttachMethod = 6;
	}
}
