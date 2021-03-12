using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000B4 RID: 180
	internal class MsgSubStorageWriter : IDisposable
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x00019B64 File Offset: 0x00017D64
		internal MsgSubStorageWriter(MsgStorageWriter owner, MsgSubStorageType type, ComStorage subStorage)
		{
			this.owner = owner;
			this.subStorage = subStorage;
			this.subStorageType = type;
			this.attachMethod = -1;
			this.propertiesCache = new MemoryStream();
			this.propertiesWriter = new BinaryWriter(this.propertiesCache);
			this.prefix = new MsgStoragePropertyPrefix(type);
			this.prefix.Write(this.propertiesWriter);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00019BD0 File Offset: 0x00017DD0
		internal void WriteProperty(TnefPropertyTag propertyTag, object propertyValue)
		{
			if (propertyTag == TnefPropertyTag.AttachMethod && this.subStorageType == MsgSubStorageType.Attachment)
			{
				int? num = propertyValue as int?;
				if (num != null)
				{
					this.attachMethod = num.Value;
				}
			}
			MsgStoragePropertyTypeRule msgStoragePropertyTypeRule;
			MsgStorageRulesTable.TryFindRule(propertyTag, out msgStoragePropertyTypeRule);
			msgStoragePropertyTypeRule.WriteValue(this, propertyTag, propertyValue);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00019CE8 File Offset: 0x00017EE8
		internal Stream OpenPropertyStream(TnefPropertyTag propertyTag)
		{
			if (this.subStorageType == MsgSubStorageType.Recipient)
			{
				throw new InvalidOperationException(MsgStorageStrings.RecipientPropertiesNotStreamable);
			}
			MsgStoragePropertyTypeRule msgStoragePropertyTypeRule;
			MsgStorageRulesTable.TryFindRule(propertyTag, out msgStoragePropertyTypeRule);
			if (!msgStoragePropertyTypeRule.CanOpenStream)
			{
				throw new InvalidOperationException(MsgStorageStrings.NonStreamableProperty);
			}
			Stream stream3 = null;
			Stream stream2 = null;
			try
			{
				int addStringTerminators = 0;
				if (propertyTag.TnefType == TnefPropertyType.Unicode)
				{
					addStringTerminators = 2;
				}
				else if (propertyTag.TnefType == TnefPropertyType.String8)
				{
					addStringTerminators = 1;
				}
				string streamName = Util.PropertyStreamName(propertyTag);
				stream3 = this.subStorage.CreateStream(streamName, ComStorage.OpenMode.CreateWrite);
				MsgStorageWriteStream msgStorageWriteStream = new MsgStorageWriteStream(stream3, addStringTerminators);
				stream3 = msgStorageWriteStream;
				stream3 = new BufferedStream(msgStorageWriteStream, 32768);
				msgStorageWriteStream.AddOnCloseNotifier(delegate(MsgStorageWriteStream stream, Exception onCloseException)
				{
					if (onCloseException != null)
					{
						this.owner.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWrite, MsgStorageStrings.ComExceptionThrown, onCloseException));
						return;
					}
					try
					{
						MsgStoragePropertyData.WriteStream(this.propertiesWriter, propertyTag, (int)stream.Length);
					}
					catch (IOException exc)
					{
						this.owner.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWriteOle, MsgStorageStrings.CorruptData, exc));
					}
					catch (COMException exc2)
					{
						this.owner.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWriteOle, MsgStorageStrings.CorruptData, exc2));
					}
				});
				stream2 = stream3;
			}
			finally
			{
				if (stream2 == null && stream3 != null)
				{
					stream3.Dispose();
				}
			}
			return stream2;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00019DEC File Offset: 0x00017FEC
		internal MsgSubStorageWriter OpenRecipientWriter()
		{
			int recipientCount = this.prefix.RecipientCount;
			ComStorage comStorage = null;
			MsgSubStorageWriter msgSubStorageWriter = null;
			try
			{
				string storageName = Util.RecipientStorageName(recipientCount);
				comStorage = this.subStorage.CreateStorage(storageName, ComStorage.OpenMode.CreateWrite);
				msgSubStorageWriter = new MsgSubStorageWriter(this.owner, MsgSubStorageType.Recipient, comStorage);
			}
			finally
			{
				if (msgSubStorageWriter == null && comStorage != null)
				{
					comStorage.Dispose();
				}
			}
			this.prefix.RecipientCount++;
			return msgSubStorageWriter;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00019E64 File Offset: 0x00018064
		internal MsgSubStorageWriter OpenAttachmentWriter()
		{
			int attachmentCount = this.prefix.AttachmentCount;
			ComStorage comStorage = null;
			MsgSubStorageWriter msgSubStorageWriter = null;
			try
			{
				string storageName = Util.AttachmentStorageName(attachmentCount);
				comStorage = this.subStorage.CreateStorage(storageName, ComStorage.OpenMode.CreateWrite);
				msgSubStorageWriter = new MsgSubStorageWriter(this.owner, MsgSubStorageType.Attachment, comStorage);
			}
			finally
			{
				if (msgSubStorageWriter == null && comStorage != null)
				{
					comStorage.Dispose();
				}
			}
			this.prefix.AttachmentCount++;
			return msgSubStorageWriter;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00019EDC File Offset: 0x000180DC
		internal MsgStorageWriter OpenAttachedMessageWriter()
		{
			if (this.attachMethod != 5)
			{
				throw new InvalidOperationException(MsgStorageStrings.NotAMessageAttachment);
			}
			ComStorage comStorage = null;
			MsgStorageWriter msgStorageWriter = null;
			try
			{
				TnefPropertyTag attachDataObj = TnefPropertyTag.AttachDataObj;
				string storageName = Util.PropertyStreamName(attachDataObj);
				MsgStoragePropertyData.WriteObject(this.propertiesWriter, attachDataObj, MsgStoragePropertyData.ObjectType.Message);
				comStorage = this.subStorage.CreateStorage(storageName, ComStorage.OpenMode.CreateWrite);
				comStorage.StorageClass = Util.ClassIdMessageAttachment;
				msgStorageWriter = new MsgStorageWriter(this.owner, comStorage);
			}
			finally
			{
				if (msgStorageWriter == null && comStorage != null)
				{
					comStorage.Dispose();
				}
			}
			return msgStorageWriter;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001A0AC File Offset: 0x000182AC
		internal Stream OpenOleAttachmentStream()
		{
			if (this.attachMethod != 6)
			{
				throw new InvalidOperationException(MsgStorageStrings.NotAnOleAttachment);
			}
			Stream cacheStream = Streams.CreateTemporaryStorageStream();
			MsgStorageWriteStream msgStorageWriteStream = new MsgStorageWriteStream(cacheStream, 0);
			msgStorageWriteStream.AddOnCloseNotifier(delegate(MsgStorageWriteStream stream, Exception onCloseException)
			{
				if (onCloseException != null)
				{
					this.owner.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWrite, MsgStorageStrings.ComExceptionThrown, onCloseException));
					return;
				}
				TnefPropertyTag attachDataObj = TnefPropertyTag.AttachDataObj;
				ComStorage comStorage = null;
				ComStorage comStorage2 = null;
				string storageName = Util.PropertyStreamName(attachDataObj);
				try
				{
					cacheStream.Flush();
					cacheStream.Position = 0L;
					comStorage = ComStorage.OpenStorageOnStream(cacheStream, ComStorage.OpenMode.Read);
					comStorage2 = this.subStorage.CreateStorage(storageName, ComStorage.OpenMode.CreateWrite);
					comStorage2.StorageClass = Util.ClassIdFileAttachment;
					ComStorage.CopyStorageContent(comStorage, comStorage2);
					comStorage2.Flush();
					MsgStoragePropertyData.WriteObject(this.propertiesWriter, attachDataObj, MsgStoragePropertyData.ObjectType.Storage);
				}
				catch (IOException exc)
				{
					this.owner.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWriteOle, MsgStorageStrings.CorruptData, exc));
				}
				catch (COMException exc2)
				{
					this.owner.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWriteOle, MsgStorageStrings.CorruptData, exc2));
				}
				finally
				{
					if (comStorage2 != null)
					{
						comStorage2.Dispose();
					}
					if (comStorage != null)
					{
						comStorage.Dispose();
					}
					if (cacheStream != null)
					{
						cacheStream.Dispose();
					}
				}
			});
			return msgStorageWriteStream;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001A105 File Offset: 0x00018305
		internal MsgStorageWriter Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001A10D File Offset: 0x0001830D
		internal BinaryWriter PropertiesWriter
		{
			get
			{
				return this.propertiesWriter;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001A115 File Offset: 0x00018315
		internal ComStorage Storage
		{
			get
			{
				return this.subStorage;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0001A11D File Offset: 0x0001831D
		internal NamedPropertyList NamedPropertyList
		{
			get
			{
				return this.owner.NamedPropertyList;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001A12A File Offset: 0x0001832A
		internal MsgSubStorageWriter.WriterBuffer LengthsBuffer
		{
			get
			{
				return this.owner.LengthsBuffer.Reset();
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001A13C File Offset: 0x0001833C
		internal MsgSubStorageWriter.WriterBuffer ValueBuffer
		{
			get
			{
				return this.owner.ValueBuffer.Reset();
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001A150 File Offset: 0x00018350
		public void Close()
		{
			try
			{
				if (this.propertiesCache != null)
				{
					if (this.prefix.AttachmentCount != 0 || this.prefix.RecipientCount != 0)
					{
						this.propertiesWriter.Flush();
						this.propertiesWriter.Seek(0, SeekOrigin.Begin);
						this.prefix.Write(this.propertiesWriter);
						this.propertiesWriter.Flush();
					}
					using (Stream stream = this.subStorage.CreateStream("__properties_version1.0", ComStorage.OpenMode.CreateWrite))
					{
						stream.Write(this.propertiesCache.GetBuffer(), 0, (int)this.propertiesCache.Length);
					}
					if (this.subStorage != null)
					{
						this.subStorage.Flush();
					}
				}
			}
			finally
			{
				if (this.propertiesCache != null)
				{
					this.propertiesCache.Flush();
					this.propertiesCache.Dispose();
				}
				if (this.subStorage != null)
				{
					this.subStorage.Dispose();
				}
				this.propertiesCache = null;
				this.subStorage = null;
			}
			this.isDisposed = true;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001A270 File Offset: 0x00018470
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(methodName);
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001A281 File Offset: 0x00018481
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001A290 File Offset: 0x00018490
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001A2A8 File Offset: 0x000184A8
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					this.Close();
				}
				catch (IOException exc)
				{
					this.owner.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWrite, MsgStorageStrings.ComExceptionThrown, exc));
				}
				catch (COMException exc2)
				{
					this.owner.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWrite, MsgStorageStrings.ComExceptionThrown, exc2));
				}
			}
		}

		// Token: 0x04000599 RID: 1433
		private const int BufferSize = 32768;

		// Token: 0x0400059A RID: 1434
		private readonly MsgStorageWriter owner;

		// Token: 0x0400059B RID: 1435
		private readonly MsgSubStorageType subStorageType;

		// Token: 0x0400059C RID: 1436
		private readonly MsgStoragePropertyPrefix prefix;

		// Token: 0x0400059D RID: 1437
		private ComStorage subStorage;

		// Token: 0x0400059E RID: 1438
		private MemoryStream propertiesCache;

		// Token: 0x0400059F RID: 1439
		private BinaryWriter propertiesWriter;

		// Token: 0x040005A0 RID: 1440
		private int attachMethod;

		// Token: 0x040005A1 RID: 1441
		private bool isDisposed;

		// Token: 0x020000B5 RID: 181
		internal class WriterBuffer
		{
			// Token: 0x060005DB RID: 1499 RVA: 0x0001A310 File Offset: 0x00018510
			internal WriterBuffer(int initialSize)
			{
				this.stream = new MemoryStream(initialSize);
				this.writer = new BinaryWriter(this.stream, Util.UnicodeEncoding);
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x0001A33A File Offset: 0x0001853A
			internal byte[] PreallocateBuffer(int size)
			{
				if (this.stream.Capacity < size)
				{
					this.stream.Capacity = (size + 2048 & -2048);
				}
				return this.stream.GetBuffer();
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001A36D File Offset: 0x0001856D
			internal BinaryWriter Writer
			{
				get
				{
					return this.writer;
				}
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x0001A375 File Offset: 0x00018575
			internal MsgSubStorageWriter.WriterBuffer Reset()
			{
				this.writer.Seek(0, SeekOrigin.Begin);
				this.stream.SetLength(0L);
				return this;
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x0001A393 File Offset: 0x00018593
			internal int GetLength()
			{
				this.writer.Flush();
				return (int)this.stream.Length;
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x0001A3AC File Offset: 0x000185AC
			internal byte[] GetBuffer()
			{
				this.writer.Flush();
				return this.stream.GetBuffer();
			}

			// Token: 0x040005A2 RID: 1442
			private MemoryStream stream;

			// Token: 0x040005A3 RID: 1443
			private BinaryWriter writer;
		}
	}
}
