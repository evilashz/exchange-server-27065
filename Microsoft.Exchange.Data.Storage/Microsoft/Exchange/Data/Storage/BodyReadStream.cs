using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005A8 RID: 1448
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BodyReadStream : StreamBase, Body.IBodyStream
	{
		// Token: 0x06003B68 RID: 15208 RVA: 0x000F4868 File Offset: 0x000F2A68
		private BodyReadStream(Stream bodyStream, Stream readStream, ConversionCallbackBase conversionCallbacks) : this(bodyStream, readStream, conversionCallbacks, null)
		{
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x000F4887 File Offset: 0x000F2A87
		private BodyReadStream(Stream bodyStream, Stream readStream, ConversionCallbackBase conversionCallbacks, long? length) : base(StreamBase.Capabilities.Readable)
		{
			this.bodyStream = bodyStream;
			this.readStream = readStream;
			this.conversionCallbacks = conversionCallbacks;
			this.length = length;
			this.position = 0L;
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000F48B8 File Offset: 0x000F2AB8
		internal static BodyReadStream TryCreateBodyReadStream(ICoreItem coreItem, BodyReadConfiguration configuration, bool createEmtpyStreamIfNotFound)
		{
			BodyReadStream bodyReadStream = BodyReadStream.InternalTryCreateBodyStream(coreItem, configuration, createEmtpyStreamIfNotFound, null);
			if (configuration.ShouldCalculateLength)
			{
				long streamLength = BodyReadStream.GetStreamLength(bodyReadStream);
				bodyReadStream.Dispose();
				bodyReadStream = BodyReadStream.InternalTryCreateBodyStream(coreItem, configuration, createEmtpyStreamIfNotFound, new long?(streamLength));
			}
			return bodyReadStream;
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000F48FC File Offset: 0x000F2AFC
		private static long GetStreamLength(BodyReadStream stream)
		{
			if (stream == null)
			{
				return 0L;
			}
			long result;
			using (Stream stream2 = new BodyStreamSizeCounter(null))
			{
				result = Util.StreamHandler.CopyStreamData(stream, stream2);
			}
			return result;
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000F493C File Offset: 0x000F2B3C
		private static BodyReadStream InternalTryCreateBodyStream(ICoreItem coreItem, BodyReadConfiguration configuration, bool createEmtpyStreamIfNotFound, long? length)
		{
			BodyReadStream result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Stream stream = BodyReadStream.OpenBodyStream(coreItem);
				disposeGuard.Add<Stream>(stream);
				Stream stream2 = stream;
				if (stream2 == null)
				{
					if (!createEmtpyStreamIfNotFound)
					{
						return null;
					}
					stream2 = Body.GetEmptyStream();
				}
				ConversionCallbackBase conversionCallbackBase;
				Stream disposable = BodyReadDelegates.CreateStream(coreItem, configuration, stream2, out conversionCallbackBase);
				disposeGuard.Add<Stream>(disposable);
				BodyReadStream bodyReadStream = new BodyReadStream(stream, disposable, conversionCallbackBase, length);
				disposeGuard.Add<BodyReadStream>(bodyReadStream);
				disposeGuard.Success();
				result = bodyReadStream;
			}
			return result;
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000F49D0 File Offset: 0x000F2BD0
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BodyReadStream>(this);
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06003B6E RID: 15214 RVA: 0x000F49D8 File Offset: 0x000F2BD8
		public ReadOnlyCollection<AttachmentLink> AttachmentLinks
		{
			get
			{
				this.CheckDisposed();
				if (this.conversionCallbacks != null)
				{
					return this.conversionCallbacks.AttachmentLinks;
				}
				return null;
			}
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000F49F8 File Offset: 0x000F2BF8
		internal static Stream OpenBodyStream(ICoreItem coreItem)
		{
			if (coreItem.Session != null && !coreItem.AreOptionalAutoloadPropertiesLoaded)
			{
				throw new NotInBagPropertyErrorException(InternalSchema.TextBody);
			}
			if (!coreItem.Body.IsBodyDefined)
			{
				return null;
			}
			StorePropertyDefinition bodyProperty = Body.GetBodyProperty(coreItem.Body.RawFormat);
			Stream result;
			try
			{
				result = coreItem.Body.InternalOpenBodyStream(bodyProperty, PropertyOpenMode.ReadOnly);
			}
			catch (ObjectNotFoundException arg)
			{
				ExTraceGlobals.CcBodyTracer.TraceError<ObjectNotFoundException>((long)coreItem.GetHashCode(), "BodyReadStream.OpenBodyStream - ObjectNotFoundException caught {0}", arg);
				result = null;
			}
			return result;
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000F4AD4 File Offset: 0x000F2CD4
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			return ConvertUtils.CallCtsWithReturnValue<int>(ExTraceGlobals.CcBodyTracer, "BodyReadStream::Read", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				int num = this.readStream.Read(buffer, offset, count);
				if (num > 0)
				{
					this.position += (long)num;
				}
				return num;
			});
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000F4B2C File Offset: 0x000F2D2C
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

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06003B72 RID: 15218 RVA: 0x000F4B64 File Offset: 0x000F2D64
		public override long Length
		{
			get
			{
				if (this.length != null)
				{
					return this.length.Value;
				}
				return base.Length;
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000F4B85 File Offset: 0x000F2D85
		public override long Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x000F4B90 File Offset: 0x000F2D90
		private void CloseStream()
		{
			if (this.readStream != null)
			{
				try
				{
					this.readStream.Dispose();
					this.readStream = null;
				}
				catch (ExchangeDataException arg)
				{
					ExTraceGlobals.CcBodyTracer.TraceError<ExchangeDataException>((long)this.GetHashCode(), "BodyReadStream.CloseStream() exception {0}", arg);
				}
			}
			if (this.bodyStream != null)
			{
				this.bodyStream.Dispose();
				this.bodyStream = null;
			}
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x000F4C00 File Offset: 0x000F2E00
		public bool IsDisposed()
		{
			return this.isDisposed;
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000F4C08 File Offset: 0x000F2E08
		private void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04001FB1 RID: 8113
		private readonly ConversionCallbackBase conversionCallbacks;

		// Token: 0x04001FB2 RID: 8114
		private Stream bodyStream;

		// Token: 0x04001FB3 RID: 8115
		private Stream readStream;

		// Token: 0x04001FB4 RID: 8116
		private bool isDisposed;

		// Token: 0x04001FB5 RID: 8117
		private long? length;

		// Token: 0x04001FB6 RID: 8118
		private long position;
	}
}
