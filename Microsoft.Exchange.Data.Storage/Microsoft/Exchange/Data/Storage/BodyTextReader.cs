using System;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005AA RID: 1450
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BodyTextReader : TextReader, Body.IBodyStream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06003B7A RID: 15226 RVA: 0x000F4C70 File Offset: 0x000F2E70
		internal BodyTextReader(ICoreItem coreItem, BodyReadConfiguration configuration, Stream inputStream)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				Stream stream = null;
				TextReader textReader = null;
				bool flag = false;
				this.disposeTracker = this.GetDisposeTracker();
				try
				{
					if (inputStream == null)
					{
						stream = BodyReadStream.OpenBodyStream(coreItem);
						inputStream = stream;
					}
					if (inputStream == null)
					{
						inputStream = Body.GetEmptyStream();
					}
					textReader = BodyReadDelegates.CreateReader(coreItem, configuration, inputStream, out this.conversionCallbacks);
					flag = true;
				}
				finally
				{
					if (!flag && stream != null)
					{
						stream.Dispose();
					}
				}
				this.reader = textReader;
				this.bodyStream = stream;
				this.isDisposed = false;
				disposeGuard.Success();
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06003B7B RID: 15227 RVA: 0x000F4D1C File Offset: 0x000F2F1C
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

		// Token: 0x06003B7C RID: 15228 RVA: 0x000F4D39 File Offset: 0x000F2F39
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<BodyTextReader>(this);
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000F4D41 File Offset: 0x000F2F41
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000F4D63 File Offset: 0x000F2F63
		public override int Peek()
		{
			this.CheckDisposed();
			return ConvertUtils.CallCtsWithReturnValue<int>(ExTraceGlobals.CcBodyTracer, "BodyTextReader::Peek", ServerStrings.ConversionBodyConversionFailed, () => this.reader.Peek());
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000F4D98 File Offset: 0x000F2F98
		public override int Read()
		{
			this.CheckDisposed();
			return ConvertUtils.CallCtsWithReturnValue<int>(ExTraceGlobals.CcBodyTracer, "BodyTextReader::Read", ServerStrings.ConversionBodyConversionFailed, () => this.reader.Read());
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000F4DEC File Offset: 0x000F2FEC
		public override int Read(char[] buffer, int index, int count)
		{
			this.CheckDisposed();
			return ConvertUtils.CallCtsWithReturnValue<int>(ExTraceGlobals.CcBodyTracer, "BodyTextReader::Read", ServerStrings.ConversionBodyConversionFailed, () => this.reader.Read(buffer, index, count));
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000F4E44 File Offset: 0x000F3044
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.CloseReader();
				}
				this.isDisposed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000F4E7C File Offset: 0x000F307C
		private void CloseReader()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (this.reader != null)
			{
				try
				{
					this.reader.Dispose();
					this.reader = null;
				}
				catch (ExchangeDataException arg)
				{
					ExTraceGlobals.CcBodyTracer.TraceError<ExchangeDataException>((long)this.GetHashCode(), "BodyTextReader.CloseReader() exception {0}", arg);
				}
			}
			if (this.bodyStream != null)
			{
				this.bodyStream.Dispose();
				this.bodyStream = null;
			}
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x000F4EFC File Offset: 0x000F30FC
		public bool IsDisposed()
		{
			return this.isDisposed;
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x000F4F04 File Offset: 0x000F3104
		private void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04001FB8 RID: 8120
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04001FB9 RID: 8121
		private Stream bodyStream;

		// Token: 0x04001FBA RID: 8122
		private TextReader reader;

		// Token: 0x04001FBB RID: 8123
		private ConversionCallbackBase conversionCallbacks;

		// Token: 0x04001FBC RID: 8124
		private bool isDisposed;
	}
}
