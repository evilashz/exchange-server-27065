using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000B6 RID: 182
	public class MsgStorageWriter : IDisposable
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x0001A3C4 File Offset: 0x000185C4
		public MsgStorageWriter(string filename)
		{
			this.namedPropertyList = new NamedPropertyList();
			this.isTopLevelMessage = true;
			this.messageStorage = ComStorage.CreateFileStorage(filename, ComStorage.OpenMode.CreateWrite);
			this.Initialize(MsgSubStorageType.TopLevelMessage);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001A3F6 File Offset: 0x000185F6
		public MsgStorageWriter(Stream stream)
		{
			this.namedPropertyList = new NamedPropertyList();
			this.isTopLevelMessage = true;
			this.messageStorage = ComStorage.CreateStorageOnStream(stream, ComStorage.OpenMode.CreateWrite);
			this.Initialize(MsgSubStorageType.TopLevelMessage);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001A428 File Offset: 0x00018628
		internal MsgStorageWriter(MsgStorageWriter parent, ComStorage storage)
		{
			this.parent = parent;
			this.messageStorage = storage;
			this.namedPropertyList = parent.NamedPropertyList;
			this.isTopLevelMessage = false;
			this.Initialize(MsgSubStorageType.AttachedMessage);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001A458 File Offset: 0x00018658
		private void Initialize(MsgSubStorageType type)
		{
			this.messageStorage.StorageClass = Util.ClassIdMessage;
			this.lengthsBuffer = new MsgSubStorageWriter.WriterBuffer(512);
			this.valueBuffer = new MsgSubStorageWriter.WriterBuffer(2048);
			this.componentWriter = null;
			this.lastFailure = null;
			this.messageWriter = new MsgSubStorageWriter(this, type, this.messageStorage);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001A4B8 File Offset: 0x000186B8
		public void WriteProperty(TnefPropertyTag propertyTag, object propertyValue)
		{
			this.CheckDisposed("MsgStorageWriter::WriteProperty(1)");
			if (propertyTag.IsNamed)
			{
				throw new ArgumentException(MsgStorageStrings.InvalidPropertyTag(propertyTag), "propertyTag");
			}
			MsgStorageRulesTable.ThrowOnInvalidPropertyType(propertyTag);
			Util.ThrowOnNullArgument(propertyValue, "propertyValue");
			this.CheckFailure();
			this.CurrentWriter.WriteProperty(propertyTag, propertyValue);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001A514 File Offset: 0x00018714
		public void WriteProperty(Guid propertyGuid, string name, TnefPropertyType propertyType, object propertyValue)
		{
			this.CheckDisposed("MsgStorageWriter::WriteProperty(2)");
			Util.ThrowOnNullArgument(name, "name");
			MsgStorageRulesTable.ThrowOnInvalidPropertyType(propertyType);
			Util.ThrowOnNullArgument(propertyValue, "propertyValue");
			this.CheckFailure();
			TnefNameId namedProperty = new TnefNameId(propertyGuid, name);
			TnefPropertyId id = this.NamedPropertyList.Add(namedProperty);
			TnefPropertyTag propertyTag = new TnefPropertyTag(id, propertyType);
			this.CurrentWriter.WriteProperty(propertyTag, propertyValue);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001A57C File Offset: 0x0001877C
		public void WriteProperty(Guid propertyGuid, int namedId, TnefPropertyType propertyType, object propertyValue)
		{
			this.CheckDisposed("MsgStorageWriter::WriteProperty(3)");
			MsgStorageRulesTable.ThrowOnInvalidPropertyType(propertyType);
			Util.ThrowOnNullArgument(propertyValue, "propertyValue");
			this.CheckFailure();
			TnefNameId namedProperty = new TnefNameId(propertyGuid, namedId);
			TnefPropertyId id = this.NamedPropertyList.Add(namedProperty);
			TnefPropertyTag propertyTag = new TnefPropertyTag(id, propertyType);
			this.CurrentWriter.WriteProperty(propertyTag, propertyValue);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001A5DC File Offset: 0x000187DC
		public Stream OpenPropertyStream(TnefPropertyTag propertyTag)
		{
			this.CheckDisposed("MsgStorageWriter::OpenPropertyStream(1)");
			if (propertyTag.IsNamed)
			{
				throw new ArgumentException(MsgStorageStrings.InvalidPropertyTag(propertyTag), "propertyTag");
			}
			MsgStorageRulesTable.ThrowOnInvalidPropertyType(propertyTag);
			this.CheckFailure();
			if (propertyTag == TnefPropertyTag.AttachDataObj)
			{
				return this.CurrentWriter.OpenOleAttachmentStream();
			}
			return this.CurrentWriter.OpenPropertyStream(propertyTag);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001A64C File Offset: 0x0001884C
		public Stream OpenPropertyStream(Guid propertyGuid, string name, TnefPropertyType propertyType)
		{
			this.CheckDisposed("MsgStorageWriter::OpenPropertyStream(2)");
			Util.ThrowOnNullArgument(name, "name");
			MsgStorageRulesTable.ThrowOnInvalidPropertyType(propertyType);
			this.CheckFailure();
			TnefNameId namedProperty = new TnefNameId(propertyGuid, name);
			TnefPropertyId id = this.NamedPropertyList.Add(namedProperty);
			TnefPropertyTag propertyTag = new TnefPropertyTag(id, propertyType);
			return this.CurrentWriter.OpenPropertyStream(propertyTag);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001A6A8 File Offset: 0x000188A8
		public Stream OpenPropertyStream(Guid propertyGuid, int namedId, TnefPropertyType propertyType)
		{
			this.CheckDisposed("MsgStorageWriter::OpenPropertyStream(3)");
			MsgStorageRulesTable.ThrowOnInvalidPropertyType(propertyType);
			this.CheckFailure();
			TnefNameId namedProperty = new TnefNameId(propertyGuid, namedId);
			TnefPropertyId id = this.NamedPropertyList.Add(namedProperty);
			TnefPropertyTag propertyTag = new TnefPropertyTag(id, propertyType);
			return this.CurrentWriter.OpenPropertyStream(propertyTag);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001A6F8 File Offset: 0x000188F8
		public void StartAttachment()
		{
			this.CheckDisposed("MsgStorageWriter::StartAttachment");
			this.CheckFailure();
			MsgSubStorageWriter msgSubStorageWriter = this.messageWriter.OpenAttachmentWriter();
			if (this.componentWriter != null)
			{
				this.componentWriter.Close();
			}
			this.componentWriter = msgSubStorageWriter;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001A73C File Offset: 0x0001893C
		public void StartRecipient()
		{
			this.CheckDisposed("MsgStorageWriter::StartRecipient");
			this.CheckFailure();
			MsgSubStorageWriter msgSubStorageWriter = this.messageWriter.OpenRecipientWriter();
			if (this.componentWriter != null)
			{
				this.componentWriter.Close();
			}
			this.componentWriter = msgSubStorageWriter;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001A780 File Offset: 0x00018980
		public MsgStorageWriter GetEmbeddedMessageWriter()
		{
			this.CheckDisposed("MsgStorageWriter::GetEmbeddedMessageWriter");
			this.CheckFailure();
			return this.componentWriter.OpenAttachedMessageWriter();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001A7A0 File Offset: 0x000189A0
		public void Flush()
		{
			if (this.isFlushed)
			{
				return;
			}
			try
			{
				this.CheckFailure();
				if (this.componentWriter != null)
				{
					this.componentWriter.Close();
					this.componentWriter = null;
				}
				if (this.messageWriter != null)
				{
					if (this.isTopLevelMessage)
					{
						this.namedPropertyList.WriteTo(this.messageStorage);
					}
					this.messageWriter.Close();
					this.messageWriter = null;
				}
				this.CheckFailure();
			}
			finally
			{
				if (this.componentWriter != null)
				{
					this.componentWriter.Dispose();
					this.componentWriter = null;
				}
				if (this.messageWriter != null)
				{
					this.messageWriter.Dispose();
					this.messageWriter = null;
				}
			}
			this.isFlushed = true;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001A860 File Offset: 0x00018A60
		internal void SetFailure(Exception exc)
		{
			this.lastFailure = exc;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001A869 File Offset: 0x00018A69
		private void CheckFailure()
		{
			if (this.lastFailure != null)
			{
				throw this.lastFailure;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001A87A File Offset: 0x00018A7A
		private MsgSubStorageWriter CurrentWriter
		{
			get
			{
				return this.componentWriter ?? this.messageWriter;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001A88C File Offset: 0x00018A8C
		internal NamedPropertyList NamedPropertyList
		{
			get
			{
				return this.namedPropertyList;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001A894 File Offset: 0x00018A94
		internal MsgSubStorageWriter.WriterBuffer LengthsBuffer
		{
			get
			{
				return this.lengthsBuffer;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001A89C File Offset: 0x00018A9C
		internal MsgSubStorageWriter.WriterBuffer ValueBuffer
		{
			get
			{
				return this.valueBuffer;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(methodName);
			}
			if (this.isFlushed)
			{
				throw new InvalidOperationException("Cannot write any data after Flush()");
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001A8D7 File Offset: 0x00018AD7
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001A8F0 File Offset: 0x00018AF0
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					this.Flush();
				}
				catch (IOException exc)
				{
					if (this.parent != null)
					{
						this.parent.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWrite, MsgStorageStrings.ComExceptionThrown, exc));
					}
				}
				catch (COMException exc2)
				{
					if (this.parent != null)
					{
						this.parent.SetFailure(new MsgStorageException(MsgStorageErrorCode.FailedWrite, MsgStorageStrings.ComExceptionThrown, exc2));
					}
				}
			}
		}

		// Token: 0x040005A4 RID: 1444
		private MsgStorageWriter parent;

		// Token: 0x040005A5 RID: 1445
		private ComStorage messageStorage;

		// Token: 0x040005A6 RID: 1446
		private MsgSubStorageWriter messageWriter;

		// Token: 0x040005A7 RID: 1447
		private MsgSubStorageWriter componentWriter;

		// Token: 0x040005A8 RID: 1448
		private NamedPropertyList namedPropertyList;

		// Token: 0x040005A9 RID: 1449
		private MsgSubStorageWriter.WriterBuffer lengthsBuffer;

		// Token: 0x040005AA RID: 1450
		private MsgSubStorageWriter.WriterBuffer valueBuffer;

		// Token: 0x040005AB RID: 1451
		private Exception lastFailure;

		// Token: 0x040005AC RID: 1452
		private bool isTopLevelMessage;

		// Token: 0x040005AD RID: 1453
		private bool isDisposed;

		// Token: 0x040005AE RID: 1454
		private bool isFlushed;
	}
}
