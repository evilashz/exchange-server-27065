using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000B7 RID: 183
	internal class MsgSubStorageReader
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x0001A968 File Offset: 0x00018B68
		internal MsgSubStorageReader(MsgStorageReader owner, ComStorage propertiesStorage, Encoding messageEncoding, MsgSubStorageType subStorageType)
		{
			this.owner = owner;
			this.propertiesStorage = propertiesStorage;
			this.messageEncoding = messageEncoding;
			this.subStorageType = subStorageType;
			this.ReadPropertiesStream();
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0001A993 File Offset: 0x00018B93
		internal Encoding MessageEncoding
		{
			get
			{
				if (this.messageEncoding == null)
				{
					Charset.DefaultWindowsCharset.TryGetEncoding(out this.messageEncoding);
				}
				return this.messageEncoding;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001A9B4 File Offset: 0x00018BB4
		internal int AttachmentCount
		{
			get
			{
				return this.prefix.AttachmentCount;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001A9C1 File Offset: 0x00018BC1
		internal int RecipientCount
		{
			get
			{
				return this.prefix.RecipientCount;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001A9CE File Offset: 0x00018BCE
		internal TnefPropertyTag PropertyTag
		{
			get
			{
				this.CheckPropertyOffset();
				return this.currentProperty.Tag;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001A9E1 File Offset: 0x00018BE1
		internal int AttachMethod
		{
			get
			{
				if (this.subStorageType != MsgSubStorageType.Attachment)
				{
					throw new InvalidOperationException();
				}
				return this.attachMethod;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001A9F8 File Offset: 0x00018BF8
		private MsgSubStorageReader.ReaderBuffers Buffers
		{
			get
			{
				return this.owner.Buffers;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001AA05 File Offset: 0x00018C05
		internal bool ReadNextProperty()
		{
			if (!this.NextProperty())
			{
				return false;
			}
			this.ReadCurrentProperty();
			return true;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001AA18 File Offset: 0x00018C18
		internal bool IsLargeValue()
		{
			this.CheckPropertyOffset();
			if (this.currentProperty.Tag.TnefType == TnefPropertyType.Object)
			{
				return true;
			}
			if (this.currentProperty.PropertyLength <= 32768)
			{
				return false;
			}
			if (!this.currentProperty.Rule.CanOpenStream)
			{
				throw new MsgStorageException(MsgStorageErrorCode.NonStreamablePropertyTooLong, MsgStorageStrings.CorruptData);
			}
			if (this.subStorageType == MsgSubStorageType.Recipient)
			{
				throw new MsgStorageException(MsgStorageErrorCode.RecipientPropertyTooLong, MsgStorageStrings.RecipientPropertyTooLong);
			}
			return true;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001AA8B File Offset: 0x00018C8B
		internal object ReadPropertyValue()
		{
			this.CheckPropertyOffset();
			if (this.IsLargeValue())
			{
				throw new InvalidOperationException(MsgStorageStrings.PropertyLongValue);
			}
			return this.InternalReadValue();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001AAAC File Offset: 0x00018CAC
		internal Stream OpenPropertyStream()
		{
			this.CheckPropertyOffset();
			if (this.subStorageType == MsgSubStorageType.Recipient)
			{
				throw new InvalidOperationException(MsgStorageStrings.RecipientPropertiesNotStreamable);
			}
			return this.InternalOpenPropertyStream(this.currentProperty.Tag);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001AADC File Offset: 0x00018CDC
		internal Stream OpenPropertyStream(TnefPropertyTag propertyTag)
		{
			int num = this.prefix.Size;
			while (num + 16 <= this.propertiesContent.Length)
			{
				TnefPropertyTag propertyTag2 = MsgStoragePropertyData.ReadPropertyTag(this.propertiesContent, num);
				if (propertyTag2.ToUnicode() == propertyTag)
				{
					return this.InternalOpenPropertyStream(propertyTag2);
				}
				num += 16;
			}
			throw new InvalidOperationException(MsgStorageStrings.PropertyNotFound(propertyTag));
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001AB44 File Offset: 0x00018D44
		internal MsgStorageReader OpenAttachedMessage()
		{
			if (this.attachMethod != 5)
			{
				throw new InvalidOperationException(MsgStorageStrings.NotAMessageAttachment);
			}
			ComStorage comStorage = null;
			MsgStorageReader msgStorageReader = null;
			try
			{
				string storageName = Util.PropertyStreamName(TnefPropertyTag.AttachDataObj);
				comStorage = this.propertiesStorage.OpenStorage(storageName, ComStorage.OpenMode.Read);
				msgStorageReader = new MsgStorageReader(comStorage, this.owner.NamedPropertyList, this.MessageEncoding);
			}
			finally
			{
				if (comStorage != null && msgStorageReader == null)
				{
					comStorage.Dispose();
				}
			}
			return msgStorageReader;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001ABBC File Offset: 0x00018DBC
		internal byte[] ReadPropertyLengthsStream(TnefPropertyTag propertyTag, int bytesToRead)
		{
			byte[] lengthsBuffer = this.Buffers.GetLengthsBuffer(bytesToRead);
			string streamName = Util.PropertyStreamName(propertyTag);
			this.InternalReadStream(streamName, lengthsBuffer, bytesToRead, 0);
			return lengthsBuffer;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001ABEC File Offset: 0x00018DEC
		internal byte[] ReadPropertyStream(TnefPropertyTag propertyTag, int bytesToRead, int maxBytesSkipped)
		{
			byte[] valueBuffer = this.Buffers.GetValueBuffer(bytesToRead);
			this.ReadPropertyStream(propertyTag, valueBuffer, bytesToRead, maxBytesSkipped);
			return valueBuffer;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001AC14 File Offset: 0x00018E14
		internal void ReadPropertyStream(TnefPropertyTag propertyTag, byte[] buffer, int bytesToRead, int maxBytesSkipped)
		{
			string streamName = Util.PropertyStreamName(propertyTag);
			this.InternalReadStream(streamName, buffer, bytesToRead, maxBytesSkipped);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001AC34 File Offset: 0x00018E34
		internal byte[] ReadPropertyIndexStream(TnefPropertyTag propertyTag, int index, int bytesToRead, int maxBytesSkipped)
		{
			byte[] valueBuffer = this.Buffers.GetValueBuffer(bytesToRead);
			this.ReadPropertyIndexStream(propertyTag, index, valueBuffer, bytesToRead, maxBytesSkipped);
			return valueBuffer;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001AC60 File Offset: 0x00018E60
		internal void ReadPropertyIndexStream(TnefPropertyTag propertyTag, int index, byte[] buffer, int bytesToRead, int maxBytesSkipped)
		{
			string streamName = Util.PropertyStreamName(propertyTag, index);
			this.InternalReadStream(streamName, buffer, bytesToRead, maxBytesSkipped);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001AC81 File Offset: 0x00018E81
		private void CheckPropertyOffset()
		{
			if (this.currentOffset < this.prefix.Size)
			{
				throw new InvalidOperationException(MsgStorageStrings.CallReadNextProperty);
			}
			if (!this.IsCurrentPropertyValid())
			{
				throw new InvalidOperationException(MsgStorageStrings.AllPropertiesRead);
			}
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001ACB4 File Offset: 0x00018EB4
		private bool NextProperty()
		{
			if (this.currentOffset == 0)
			{
				this.currentOffset = this.prefix.Size;
			}
			else
			{
				if (!this.IsCurrentPropertyValid())
				{
					return false;
				}
				this.currentOffset += 16;
			}
			return this.IsCurrentPropertyValid();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001AD00 File Offset: 0x00018F00
		private bool IsCurrentPropertyValid()
		{
			return this.currentOffset >= this.prefix.Size && this.currentOffset + 16 <= this.propertiesContent.Length;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001AD30 File Offset: 0x00018F30
		private void ReadCurrentProperty()
		{
			TnefPropertyTag tnefPropertyTag = MsgStoragePropertyData.ReadPropertyTag(this.propertiesContent, this.currentOffset);
			int propertyLength = 0;
			MsgStoragePropertyTypeRule msgStoragePropertyTypeRule;
			if (!MsgStorageRulesTable.TryFindRule(tnefPropertyTag, out msgStoragePropertyTypeRule))
			{
				throw new MsgStorageException(MsgStorageErrorCode.InvalidPropertyType, MsgStorageStrings.CorruptData);
			}
			if (!msgStoragePropertyTypeRule.IsFixedValue)
			{
				propertyLength = MsgStoragePropertyData.ReadPropertyByteCount(this.propertiesContent, this.currentOffset);
			}
			this.currentProperty = default(MsgSubStorageReader.PropertyInfo);
			this.currentProperty.Tag = tnefPropertyTag;
			this.currentProperty.Rule = msgStoragePropertyTypeRule;
			this.currentProperty.PropertyLength = propertyLength;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		private void ReadPropertiesStream()
		{
			this.propertiesContent = this.propertiesStorage.ReadFromStreamMaxLength("__properties_version1.0", int.MaxValue);
			this.prefix = new MsgStoragePropertyPrefix(this.subStorageType);
			if (this.propertiesContent.Length == 0 && (this.subStorageType == MsgSubStorageType.Attachment || this.subStorageType == MsgSubStorageType.Recipient))
			{
				throw new MsgStorageNotFoundException(MsgStorageErrorCode.EmptyPropertiesStream, MsgStorageStrings.NotFound, null);
			}
			if (this.propertiesContent.Length < this.prefix.Size)
			{
				throw new MsgStorageException(MsgStorageErrorCode.PropertyListTruncated, MsgStorageStrings.CorruptData);
			}
			this.prefix.Read(this.propertiesContent);
			this.currentOffset = 0;
			while (this.ReadNextProperty())
			{
				this.InternalAddProperty();
			}
			this.currentOffset = 0;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001AE6C File Offset: 0x0001906C
		private void InternalAddProperty()
		{
			if (this.currentProperty.Tag == TnefPropertyTag.InternetCPID && this.messageEncoding == null && (this.subStorageType == MsgSubStorageType.AttachedMessage || this.subStorageType == MsgSubStorageType.TopLevelMessage))
			{
				int codePage = (int)this.ReadPropertyValue();
				Charset charset;
				if (Charset.TryGetCharset(codePage, out charset))
				{
					charset.Culture.WindowsCharset.TryGetEncoding(out this.messageEncoding);
				}
				if (this.messageEncoding == null)
				{
					this.messageEncoding = Charset.DefaultWindowsCharset.GetEncoding();
					return;
				}
			}
			else if (this.currentProperty.Tag == TnefPropertyTag.AttachMethod && this.subStorageType == MsgSubStorageType.Attachment)
			{
				this.attachMethod = (int)this.ReadPropertyValue();
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001AF2C File Offset: 0x0001912C
		private Stream InternalOpenPropertyStream(TnefPropertyTag propertyTag)
		{
			if (propertyTag == TnefPropertyTag.AttachDataObj)
			{
				return this.InternalOpenOleAttachmentStream();
			}
			MsgStoragePropertyTypeRule msgStoragePropertyTypeRule;
			MsgStorageRulesTable.TryFindRule(propertyTag, out msgStoragePropertyTypeRule);
			if (!msgStoragePropertyTypeRule.CanOpenStream)
			{
				throw new InvalidOperationException(MsgStorageStrings.NonStreamableProperty);
			}
			Stream stream = this.propertiesStorage.OpenStream(Util.PropertyStreamName(propertyTag), ComStorage.OpenMode.Read);
			if (propertyTag.TnefType == TnefPropertyType.String8)
			{
				stream = new ConverterStream(stream, new TextToText(TextToTextConversionMode.ConvertCodePageOnly)
				{
					InputEncoding = this.MessageEncoding,
					OutputEncoding = Util.UnicodeEncoding
				}, ConverterStreamAccess.Read);
			}
			return stream;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001AFB8 File Offset: 0x000191B8
		private Stream InternalOpenOleAttachmentStream()
		{
			if (this.subStorageType != MsgSubStorageType.Attachment || this.attachMethod != 6)
			{
				throw new InvalidOperationException(MsgStorageStrings.NotAnOleAttachment);
			}
			ComStorage comStorage = null;
			ComStorage comStorage2 = null;
			Stream stream = Streams.CreateTemporaryStorageStream();
			try
			{
				string storageName = Util.PropertyStreamName(TnefPropertyTag.AttachDataObj);
				comStorage = this.propertiesStorage.OpenStorage(storageName, ComStorage.OpenMode.Read);
				comStorage2 = ComStorage.CreateStorageOnStream(stream, ComStorage.OpenMode.CreateWrite);
				ComStorage.CopyStorageContent(comStorage, comStorage2);
				comStorage2.Flush();
			}
			finally
			{
				if (comStorage != null)
				{
					comStorage.Dispose();
				}
				if (comStorage2 != null)
				{
					comStorage2.Dispose();
				}
			}
			stream.Position = 0L;
			return stream;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001B050 File Offset: 0x00019250
		private void InternalReadStream(string streamName, byte[] buffer, int bytesToRead, int maxBytesSkipped)
		{
			if (bytesToRead != 0)
			{
				int num = this.propertiesStorage.ReadFromStream(streamName, buffer, bytesToRead);
				if (num < bytesToRead)
				{
					if (num < bytesToRead - maxBytesSkipped)
					{
						throw new MsgStorageException(MsgStorageErrorCode.PropertyValueTruncated, MsgStorageStrings.PropertyValueTruncated);
					}
					for (int i = num; i < bytesToRead; i++)
					{
						buffer[i] = 0;
					}
				}
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001B098 File Offset: 0x00019298
		private object InternalReadValue()
		{
			MsgStoragePropertyTypeRule rule = this.currentProperty.Rule;
			if (rule.IsFixedValue)
			{
				return rule.ReadFixedValue(this.propertiesContent, this.currentOffset);
			}
			return rule.ReadStreamedValue(this, this.currentProperty);
		}

		// Token: 0x040005AF RID: 1455
		private readonly MsgStorageReader owner;

		// Token: 0x040005B0 RID: 1456
		private readonly ComStorage propertiesStorage;

		// Token: 0x040005B1 RID: 1457
		private MsgSubStorageType subStorageType;

		// Token: 0x040005B2 RID: 1458
		private MsgStoragePropertyPrefix prefix;

		// Token: 0x040005B3 RID: 1459
		private byte[] propertiesContent;

		// Token: 0x040005B4 RID: 1460
		private int currentOffset;

		// Token: 0x040005B5 RID: 1461
		private MsgSubStorageReader.PropertyInfo currentProperty;

		// Token: 0x040005B6 RID: 1462
		private Encoding messageEncoding;

		// Token: 0x040005B7 RID: 1463
		private int attachMethod;

		// Token: 0x020000B8 RID: 184
		internal struct ReaderBuffers
		{
			// Token: 0x06000615 RID: 1557 RVA: 0x0001B0E3 File Offset: 0x000192E3
			public byte[] GetValueBuffer(int size)
			{
				if (this.valueBuffer == null || this.valueBuffer.Length < size)
				{
					size = (size + 2048 & -2048);
					this.valueBuffer = new byte[size];
				}
				return this.valueBuffer;
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x0001B119 File Offset: 0x00019319
			public byte[] GetLengthsBuffer(int size)
			{
				if (this.lengthsBuffer == null || this.lengthsBuffer.Length < size)
				{
					size = (size + 512 & -512);
					this.lengthsBuffer = new byte[size];
				}
				return this.lengthsBuffer;
			}

			// Token: 0x040005B8 RID: 1464
			private byte[] valueBuffer;

			// Token: 0x040005B9 RID: 1465
			private byte[] lengthsBuffer;
		}

		// Token: 0x020000B9 RID: 185
		internal struct PropertyInfo
		{
			// Token: 0x040005BA RID: 1466
			public TnefPropertyTag Tag;

			// Token: 0x040005BB RID: 1467
			public int PropertyLength;

			// Token: 0x040005BC RID: 1468
			public MsgStoragePropertyTypeRule Rule;
		}
	}
}
