using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200076D RID: 1901
	[Serializable]
	public class RecurrenceFormatException : CorruptDataException
	{
		// Token: 0x060048A2 RID: 18594 RVA: 0x00131466 File Offset: 0x0012F666
		internal RecurrenceFormatException(LocalizedString message, Stream stream) : this(message, stream, null)
		{
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x00131474 File Offset: 0x0012F674
		internal RecurrenceFormatException(LocalizedString message, Stream stream, Exception innerException) : base(message, innerException)
		{
			this.position = (int)stream.Position;
			MemoryStream memoryStream = stream as MemoryStream;
			if (memoryStream != null)
			{
				this.blob = memoryStream.ToArray();
				return;
			}
			if (stream.CanSeek)
			{
				stream.Seek(0L, SeekOrigin.Begin);
				this.blob = new byte[stream.Length];
				stream.Read(this.blob, 0, this.blob.Length);
				stream.Seek((long)this.position, SeekOrigin.Begin);
			}
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x001314F8 File Offset: 0x0012F6F8
		protected RecurrenceFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.blob = (byte[])info.GetValue("blob", typeof(byte[]));
			this.position = (int)info.GetValue("position", typeof(int));
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x060048A5 RID: 18597 RVA: 0x0013154D File Offset: 0x0012F74D
		public byte[] Blob
		{
			get
			{
				return this.blob;
			}
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x060048A6 RID: 18598 RVA: 0x00131555 File Offset: 0x0012F755
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x0013155D File Offset: 0x0012F75D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("blob", this.blob);
			info.AddValue("position", this.position);
		}

		// Token: 0x04002762 RID: 10082
		private const string BlobLabel = "blob";

		// Token: 0x04002763 RID: 10083
		private const string PositionLabel = "position";

		// Token: 0x04002764 RID: 10084
		private byte[] blob;

		// Token: 0x04002765 RID: 10085
		private int position;
	}
}
