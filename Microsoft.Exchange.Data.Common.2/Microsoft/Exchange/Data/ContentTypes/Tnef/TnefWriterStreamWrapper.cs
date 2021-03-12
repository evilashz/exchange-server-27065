using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000E1 RID: 225
	internal class TnefWriterStreamWrapper : Stream
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x000310CF File Offset: 0x0002F2CF
		public TnefWriterStreamWrapper(TnefWriter writer)
		{
			this.Writer = writer;
			this.Writer.Child = this;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x000310EA File Offset: 0x0002F2EA
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x000310ED File Offset: 0x0002F2ED
		public override bool CanWrite
		{
			get
			{
				return this.Writer != null;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x000310FB File Offset: 0x0002F2FB
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x000310FE File Offset: 0x0002F2FE
		public override long Length
		{
			get
			{
				throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0003110A File Offset: 0x0002F30A
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x00031116 File Offset: 0x0002F316
		public override long Position
		{
			get
			{
				throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
			}
			set
			{
				throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00031122 File Offset: 0x0002F322
		public override void Flush()
		{
			if (this.Writer == null)
			{
				throw new ObjectDisposedException("TnefWriterStreamWrapper");
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00031137 File Offset: 0x0002F337
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(TnefStrings.StreamDoesNotSupportRead);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00031143 File Offset: 0x0002F343
		public override void SetLength(long value)
		{
			throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0003114F File Offset: 0x0002F34F
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.Writer == null)
			{
				throw new ObjectDisposedException("TnefWriterStreamWrapper");
			}
			this.Writer.WritePropertyRawValueImpl(buffer, offset, count, true);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00031173 File Offset: 0x0002F373
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0003117F File Offset: 0x0002F37F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.Writer != null)
			{
				this.Writer.Child = null;
			}
			this.Writer = null;
			base.Dispose(disposing);
		}

		// Token: 0x040007CC RID: 1996
		internal TnefWriter Writer;
	}
}
