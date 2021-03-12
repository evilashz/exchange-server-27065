using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005AE RID: 1454
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BodyWriteStream : StreamBase, Body.IBodyStream
	{
		// Token: 0x06003BC1 RID: 15297 RVA: 0x000F5ED4 File Offset: 0x000F40D4
		internal BodyWriteStream(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream outputStream) : base(StreamBase.Capabilities.Writable)
		{
			this.coreItem = coreItem;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Stream stream = null;
				if (outputStream == null)
				{
					stream = BodyWriteStream.OpenBodyStream(coreItem, configuration);
					disposeGuard.Add<Stream>(stream);
					outputStream = stream;
				}
				Stream disposable = BodyWriteDelegates.CreateStream(coreItem, configuration, outputStream, out this.conversionCallback);
				disposeGuard.Add<Stream>(disposable);
				disposeGuard.Success();
				this.writeStream = disposable;
				this.bodyStream = stream;
			}
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000F5F60 File Offset: 0x000F4160
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BodyWriteStream>(this);
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000F5F68 File Offset: 0x000F4168
		internal static Stream OpenBodyStream(ICoreItem coreItem, BodyWriteConfiguration configuration)
		{
			StorePropertyDefinition bodyProperty = Body.GetBodyProperty(configuration.TargetFormat);
			return coreItem.Body.InternalOpenBodyStream(bodyProperty, PropertyOpenMode.Create);
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000F5FBC File Offset: 0x000F41BC
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyWriteStream::Write", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				this.writeStream.Write(buffer, offset, count);
			});
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000F601E File Offset: 0x000F421E
		public override void Flush()
		{
			this.CheckDisposed();
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyWriteStream::Flush", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				this.writeStream.Flush();
			});
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000F6048 File Offset: 0x000F4248
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.CloseStream();
					this.isDisposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x000F60E4 File Offset: 0x000F42E4
		private void CloseStream()
		{
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyWriteStream::Close", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				if (this.writeStream != null)
				{
					try
					{
						this.writeStream.Dispose();
						this.writeStream = null;
					}
					catch (ExchangeDataException ex)
					{
						ExTraceGlobals.CcBodyTracer.TraceError<ExchangeDataException>((long)this.GetHashCode(), "BodyWriteStream.CloseStream() exception {0}", ex);
						this.coreItem.Body.SetBodyStreamingException(ex);
					}
				}
			});
			if (this.bodyStream != null)
			{
				this.bodyStream.Dispose();
				this.bodyStream = null;
			}
			if (this.conversionCallback != null)
			{
				this.conversionCallback.SaveChanges();
			}
			this.conversionCallback = null;
			this.isDisposed = true;
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000F614D File Offset: 0x000F434D
		public bool IsDisposed()
		{
			return this.isDisposed;
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000F6155 File Offset: 0x000F4355
		private void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04001FC9 RID: 8137
		private Stream bodyStream;

		// Token: 0x04001FCA RID: 8138
		private Stream writeStream;

		// Token: 0x04001FCB RID: 8139
		private ConversionCallbackBase conversionCallback;

		// Token: 0x04001FCC RID: 8140
		private ICoreItem coreItem;

		// Token: 0x04001FCD RID: 8141
		private bool isDisposed;
	}
}
