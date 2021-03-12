using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000C RID: 12
	internal class PagedReceiver : DisposeTrackableBase, IDataImport, IDisposable
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00007C98 File Offset: 0x00005E98
		public PagedReceiver(TransmissionDoneStringDelegate stringDone, bool useCompression) : this(stringDone, null, useCompression)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007CA3 File Offset: 0x00005EA3
		public PagedReceiver(TransmissionDoneBlobDelegate blobDone, bool useCompression) : this(null, blobDone, useCompression)
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007CAE File Offset: 0x00005EAE
		private PagedReceiver(TransmissionDoneStringDelegate stringDone, TransmissionDoneBlobDelegate blobDone, bool useCompression)
		{
			this.stringDoneDelegate = stringDone;
			this.blobDoneDelegate = blobDone;
			this.useCompression = useCompression;
			this.chunks = new List<byte[]>();
			this.totalDataSize = 0;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007CE0 File Offset: 0x00005EE0
		void IDataImport.SendMessage(IDataMessage message)
		{
			PagedDataMessage pagedDataMessage = message as PagedDataMessage;
			if (pagedDataMessage == null)
			{
				throw new InputDataIsInvalidPermanentException();
			}
			if (pagedDataMessage.Buffer != null)
			{
				this.chunks.Add(pagedDataMessage.Buffer);
				this.totalDataSize += pagedDataMessage.Buffer.Length;
			}
			if (pagedDataMessage.IsLastChunk)
			{
				byte[] array;
				if (this.chunks.Count == 0)
				{
					array = null;
				}
				else if (this.chunks.Count == 1)
				{
					array = this.chunks[0];
				}
				else
				{
					array = new byte[this.totalDataSize];
					int num = 0;
					foreach (byte[] array2 in this.chunks)
					{
						array2.CopyTo(array, num);
						num += array2.Length;
					}
				}
				if (this.blobDoneDelegate != null)
				{
					if (this.useCompression)
					{
						array = CommonUtils.DecompressData(array);
					}
					this.blobDoneDelegate(array);
					return;
				}
				string data = CommonUtils.UnpackString(array, this.useCompression);
				this.stringDoneDelegate(data);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007E00 File Offset: 0x00006000
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage message)
		{
			return null;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007E03 File Offset: 0x00006003
		protected override void InternalDispose(bool calledFromDispose)
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007E05 File Offset: 0x00006005
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PagedReceiver>(this);
		}

		// Token: 0x04000032 RID: 50
		private TransmissionDoneStringDelegate stringDoneDelegate;

		// Token: 0x04000033 RID: 51
		private TransmissionDoneBlobDelegate blobDoneDelegate;

		// Token: 0x04000034 RID: 52
		private bool useCompression;

		// Token: 0x04000035 RID: 53
		private List<byte[]> chunks;

		// Token: 0x04000036 RID: 54
		private int totalDataSize;
	}
}
