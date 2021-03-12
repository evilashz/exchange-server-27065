using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005AF RID: 1455
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BodyTextWriter : TextWriter, Body.IBodyStream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06003BCC RID: 15308 RVA: 0x000F6170 File Offset: 0x000F4370
		internal BodyTextWriter(ICoreItem coreItem, BodyWriteConfiguration configuration, Stream outputStream)
		{
			this.coreItem = coreItem;
			this.disposeTracker = this.GetDisposeTracker();
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Stream stream = null;
				if (outputStream == null)
				{
					stream = new StreamWrapper(BodyWriteStream.OpenBodyStream(coreItem, configuration), true);
					disposeGuard.Add<Stream>(stream);
					outputStream = stream;
				}
				TextWriter disposable = BodyWriteDelegates.CreateWriter(coreItem, configuration, outputStream, out this.conversionCallback);
				disposeGuard.Add<TextWriter>(disposable);
				disposeGuard.Success();
				this.writer = disposable;
				this.bodyStream = stream;
			}
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x000F620C File Offset: 0x000F440C
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BodyTextWriter>(this);
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x000F6214 File Offset: 0x000F4414
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06003BCF RID: 15311 RVA: 0x000F6236 File Offset: 0x000F4436
		public override Encoding Encoding
		{
			get
			{
				this.CheckDisposed();
				return ConvertUtils.CallCtsWithReturnValue<Encoding>(ExTraceGlobals.CcBodyTracer, "BodyTextWriter::Encoding::get", ServerStrings.ConversionBodyConversionFailed, () => this.writer.Encoding);
			}
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x000F6280 File Offset: 0x000F4480
		public override void Write(char value)
		{
			this.CheckDisposed();
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyTextWriter::Write(char)", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				this.writer.Write(value);
			});
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x000F62E8 File Offset: 0x000F44E8
		public override void Write(char[] buffer)
		{
			this.CheckDisposed();
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyTextWriter::Write(char[])", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				this.writer.Write(buffer);
			});
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000F635C File Offset: 0x000F455C
		public override void Write(char[] buffer, int index, int count)
		{
			this.CheckDisposed();
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyTextWriter::Write(char[]/int/int)", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				this.writer.Write(buffer, index, count);
			});
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000F63D4 File Offset: 0x000F45D4
		public override void Write(string value)
		{
			this.CheckDisposed();
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyTextWriter::Write(string)", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				this.writer.Write(value);
			});
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x000F643C File Offset: 0x000F463C
		public override void WriteLine(string value)
		{
			this.CheckDisposed();
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyTextWriter::WriteLine", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				this.writer.WriteLine(value);
			});
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x000F6490 File Offset: 0x000F4690
		public override void Flush()
		{
			this.CheckDisposed();
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyTextWriter::Flush", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				this.writer.Flush();
			});
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x000F64B8 File Offset: 0x000F46B8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.CloseWriter();
				}
				this.isDisposed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x000F6554 File Offset: 0x000F4754
		private void CloseWriter()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			ConvertUtils.CallCts(ExTraceGlobals.CcBodyTracer, "BodyTextWriter::Close", ServerStrings.ConversionBodyConversionFailed, delegate
			{
				if (this.writer != null)
				{
					try
					{
						this.writer.Dispose();
						this.writer = null;
					}
					catch (ExchangeDataException ex)
					{
						ExTraceGlobals.CcBodyTracer.TraceError<ExchangeDataException>((long)this.GetHashCode(), "BodyTextWriter.CloseWriter() exception {0}", ex);
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
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x000F65D6 File Offset: 0x000F47D6
		public bool IsDisposed()
		{
			return this.isDisposed;
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x000F65DE File Offset: 0x000F47DE
		private void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04001FCE RID: 8142
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04001FCF RID: 8143
		private Stream bodyStream;

		// Token: 0x04001FD0 RID: 8144
		private TextWriter writer;

		// Token: 0x04001FD1 RID: 8145
		private ConversionCallbackBase conversionCallback;

		// Token: 0x04001FD2 RID: 8146
		private ICoreItem coreItem;

		// Token: 0x04001FD3 RID: 8147
		private bool isDisposed;
	}
}
