using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000B2 RID: 178
	public class MsgStorageReader : IDisposable
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0001976A File Offset: 0x0001796A
		internal MsgStorageReader(ComStorage storage, NamedPropertyList namedPropertyList, Encoding parentEncoding)
		{
			this.isTopLevelMessage = false;
			this.messageStorage = storage;
			this.namedPropertyList = namedPropertyList;
			this.Initialize(parentEncoding);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001978E File Offset: 0x0001798E
		public MsgStorageReader(string filename)
		{
			this.isTopLevelMessage = true;
			this.messageStorage = ComStorage.OpenFileStorage(filename, ComStorage.OpenMode.Read);
			this.Initialize(null);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000197B2 File Offset: 0x000179B2
		public MsgStorageReader(Stream contentStream)
		{
			this.isTopLevelMessage = true;
			this.messageStorage = ComStorage.OpenStorageOnStream(contentStream, ComStorage.OpenMode.Read);
			this.Initialize(null);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000197D8 File Offset: 0x000179D8
		private void Initialize(Encoding messageEncoding)
		{
			this.readBuffers = default(MsgSubStorageReader.ReaderBuffers);
			this.reader = new MsgStoragePropertyReader(this);
			MsgSubStorageType subStorageType;
			if (this.isTopLevelMessage)
			{
				subStorageType = MsgSubStorageType.TopLevelMessage;
				this.namedPropertyList = NamedPropertyList.ReadNamedPropertyList(this.messageStorage);
			}
			else
			{
				subStorageType = MsgSubStorageType.AttachedMessage;
			}
			this.subStorageParser = new MsgSubStorageReader(this, this.messageStorage, messageEncoding, subStorageType);
			this.attachmentCount = this.subStorageParser.AttachmentCount;
			this.recipientCount = this.subStorageParser.RecipientCount;
			this.encoding = this.subStorageParser.MessageEncoding;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00019863 File Offset: 0x00017A63
		public int AttachmentCount
		{
			get
			{
				this.CheckDisposed("MsgStorageReader::get_AttachmentCount");
				return this.attachmentCount;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00019876 File Offset: 0x00017A76
		public int RecipientCount
		{
			get
			{
				this.CheckDisposed("MsgStorageReader::get_RecipientCount");
				return this.recipientCount;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00019889 File Offset: 0x00017A89
		public MsgStoragePropertyReader PropertyReader
		{
			get
			{
				this.CheckDisposed("MsgStorageReader::get_PropertyReader");
				return this.reader;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001989C File Offset: 0x00017A9C
		internal MsgSubStorageReader SubStorageReader
		{
			get
			{
				return this.subStorageParser;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x000198A4 File Offset: 0x00017AA4
		internal NamedPropertyList NamedPropertyList
		{
			get
			{
				return this.namedPropertyList;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000198AC File Offset: 0x00017AAC
		internal MsgSubStorageReader.ReaderBuffers Buffers
		{
			get
			{
				return this.readBuffers;
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000198B4 File Offset: 0x00017AB4
		public void OpenAttachment(int attachmentIndex)
		{
			this.CheckDisposed("MsgStorageReader::OpenAttachment");
			if (attachmentIndex < 0 || attachmentIndex >= this.AttachmentCount)
			{
				throw new ArgumentOutOfRangeException("attachmentIndex");
			}
			this.OpenSubStorage(Util.AttachmentStorageName(attachmentIndex), MsgSubStorageType.Attachment);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000198E6 File Offset: 0x00017AE6
		public void OpenRecipient(int recipientIndex)
		{
			this.CheckDisposed("MsgStorageReader::OpenRecipient");
			if (recipientIndex < 0 || recipientIndex >= this.RecipientCount)
			{
				throw new ArgumentOutOfRangeException("recipientIndex");
			}
			this.OpenSubStorage(Util.RecipientStorageName(recipientIndex), MsgSubStorageType.Recipient);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00019918 File Offset: 0x00017B18
		private void OpenSubStorage(string subStorageName, MsgSubStorageType type)
		{
			ComStorage comStorage = null;
			try
			{
				comStorage = this.messageStorage.OpenStorage(subStorageName, ComStorage.OpenMode.Read);
				MsgSubStorageReader msgSubStorageReader = new MsgSubStorageReader(this, comStorage, this.encoding, type);
				if (this.subStorage != null)
				{
					this.subStorage.Dispose();
				}
				this.subStorage = comStorage;
				this.subStorageParser = msgSubStorageReader;
				comStorage = null;
			}
			finally
			{
				if (comStorage != null)
				{
					comStorage.Dispose();
				}
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00019988 File Offset: 0x00017B88
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00019994 File Offset: 0x00017B94
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.subStorage != null)
				{
					this.subStorage.Dispose();
					this.subStorage = null;
				}
				if (this.messageStorage != null)
				{
					this.messageStorage.Dispose();
					this.messageStorage = null;
				}
				GC.SuppressFinalize(this);
			}
			this.isDisposed = true;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000199E5 File Offset: 0x00017BE5
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(methodName);
			}
		}

		// Token: 0x0400058D RID: 1421
		private ComStorage messageStorage;

		// Token: 0x0400058E RID: 1422
		private ComStorage subStorage;

		// Token: 0x0400058F RID: 1423
		private MsgStoragePropertyReader reader;

		// Token: 0x04000590 RID: 1424
		private MsgSubStorageReader subStorageParser;

		// Token: 0x04000591 RID: 1425
		private MsgSubStorageReader.ReaderBuffers readBuffers;

		// Token: 0x04000592 RID: 1426
		private NamedPropertyList namedPropertyList;

		// Token: 0x04000593 RID: 1427
		private Encoding encoding;

		// Token: 0x04000594 RID: 1428
		private int recipientCount;

		// Token: 0x04000595 RID: 1429
		private int attachmentCount;

		// Token: 0x04000596 RID: 1430
		private bool isDisposed;

		// Token: 0x04000597 RID: 1431
		private bool isTopLevelMessage;
	}
}
