using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000F1 RID: 241
	public class TnefReader : IDisposable
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x000348E6 File Offset: 0x00032AE6
		public TnefReader(Stream inputStream) : this(inputStream, 0, TnefComplianceMode.Strict)
		{
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000348F4 File Offset: 0x00032AF4
		public TnefReader(Stream inputStream, int defaultMessageCodepage, TnefComplianceMode complianceMode)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (!inputStream.CanRead)
			{
				throw new NotSupportedException(TnefStrings.StreamDoesNotSupportRead);
			}
			this.InputStream = inputStream;
			this.streamMaxLength = int.MaxValue;
			this.complianceMode = complianceMode;
			this.complianceStatus = TnefComplianceStatus.Compliant;
			if (TnefCommon.IsUnicodeCodepage(defaultMessageCodepage))
			{
				defaultMessageCodepage = 0;
			}
			this.messageCodepage = defaultMessageCodepage;
			this.readBuffer = new byte[4096];
			this.fabricatedBuffer = new byte[512];
			this.propertyReader = new TnefPropertyReader(this);
			this.unicodeDecoder = Encoding.Unicode.GetDecoder();
			this.ReadTnefHeader();
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0003499C File Offset: 0x00032B9C
		internal TnefReader(TnefReader parent)
		{
			this.embeddingDepth = parent.embeddingDepth + 1;
			this.parent = parent;
			this.parent.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			this.InputStream = parent.InputStream;
			this.streamOffset = -parent.readOffset;
			this.streamMaxLength = Math.Min(parent.propertyValueLength, parent.streamMaxLength - parent.propertyValueStreamOffset);
			this.complianceMode = parent.complianceMode;
			this.complianceStatus = TnefComplianceStatus.Compliant;
			this.messageCodepage = parent.MessageCodepage;
			this.checksumDisabled = parent.checksumDisabled;
			this.readBuffer = parent.readBuffer;
			this.readOffset = parent.readOffset;
			this.readEnd = parent.readEnd;
			this.readEndReal = parent.readEndReal;
			this.endOfFile = parent.endOfFile;
			if (this.streamOffset + this.readEnd > this.streamMaxLength)
			{
				this.readEnd = this.streamMaxLength - this.streamOffset;
				this.endOfFile = true;
			}
			this.propertyReader = new TnefPropertyReader(this);
			this.unicodeDecoder = parent.unicodeDecoder;
			this.string8Decoder = parent.string8Decoder;
			this.fabricatedBuffer = parent.fabricatedBuffer;
			this.decodeBuffer = parent.decodeBuffer;
			this.ReadTnefHeader();
			this.parent.Child = this;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x00034AEC File Offset: 0x00032CEC
		public TnefComplianceMode ComplianceMode
		{
			get
			{
				this.AssertGoodToUse(false);
				return this.complianceMode;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00034AFB File Offset: 0x00032CFB
		public TnefComplianceStatus ComplianceStatus
		{
			get
			{
				this.AssertGoodToUse(false);
				return this.complianceStatus;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00034B0A File Offset: 0x00032D0A
		public int StreamOffset
		{
			get
			{
				this.AssertGoodToUse(true);
				return this.streamOffset + this.readOffset;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00034B20 File Offset: 0x00032D20
		public int TnefVersion
		{
			get
			{
				this.AssertGoodToUse(false);
				return this.tnefVersion;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00034B2F File Offset: 0x00032D2F
		public short AttachmentKey
		{
			get
			{
				this.AssertGoodToUse(false);
				return this.attachmentKey;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00034B3E File Offset: 0x00032D3E
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00034B4D File Offset: 0x00032D4D
		public int MessageCodepage
		{
			get
			{
				this.AssertGoodToUse(false);
				return this.messageCodepage;
			}
			set
			{
				this.AssertGoodToUse(false);
				this.messageCodepage = value;
				this.string8Decoder = null;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00034B64 File Offset: 0x00032D64
		public TnefAttributeLevel AttributeLevel
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInAttribute();
				return this.attributeLevel;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00034B79 File Offset: 0x00032D79
		public TnefAttributeTag AttributeTag
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInAttribute();
				return this.attributeTag;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00034B8E File Offset: 0x00032D8E
		public TnefPropertyReader PropertyReader
		{
			get
			{
				this.AssertGoodToUse(true);
				this.AssertInAttribute();
				if (this.ReadStateValue == TnefReader.ReadState.ReadAttributeValue)
				{
					throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationPropAfterRaw);
				}
				return this.propertyReader;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00034BB7 File Offset: 0x00032DB7
		public int AttributeRawValueStreamOffset
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInAttribute();
				return this.attributeValueStreamOffset;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00034BCC File Offset: 0x00032DCC
		public int AttributeRawValueLength
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInAttribute();
				return this.attributeValueLength;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00034BE1 File Offset: 0x00032DE1
		internal int RowCount
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInAttribute();
				if (this.attributeTag != TnefAttributeTag.RecipientTable)
				{
					throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationRowsOnlyInRecipientTable);
				}
				return this.rowCount;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00034C0E File Offset: 0x00032E0E
		internal int PropertyCount
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInAttribute();
				return this.propertyCount;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00034C23 File Offset: 0x00032E23
		internal TnefPropertyTag PropertyTag
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInProperty();
				return this.propertyTag;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00034C38 File Offset: 0x00032E38
		internal int PropertyValueCount
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInProperty();
				return this.propertyValueCount;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00034C4D File Offset: 0x00032E4D
		internal Guid PropertyValueOleIID
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInPropertyValue();
				if (this.propertyTag.ValueTnefType != TnefPropertyType.Object)
				{
					throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationNotObjectProperty);
				}
				return this.propertyValueIId;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00034C7C File Offset: 0x00032E7C
		internal bool IsPropertyEmbeddedMessage
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInProperty();
				if (this.propertyTag.ValueTnefType != TnefPropertyType.Object)
				{
					return false;
				}
				this.AssertInPropertyValue();
				return this.propertyValueIId == TnefCommon.MessageIID;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00034CB2 File Offset: 0x00032EB2
		internal TnefNameId PropertyNameId
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInProperty();
				if (!this.propertyTag.IsNamed)
				{
					throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationNotNamedProperty);
				}
				return this.propertyNameId;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00034CDF File Offset: 0x00032EDF
		internal bool IsComputedProperty
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInProperty();
				return !this.directRead;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00034CF7 File Offset: 0x00032EF7
		internal int PropertyRawValueStreamOffset
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInPropertyValue();
				if (this.IsComputedProperty)
				{
					return -1;
				}
				return this.propertyValueStreamOffset;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00034D16 File Offset: 0x00032F16
		internal int PropertyRawValueLength
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInPropertyValue();
				return this.propertyValueLength;
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00034D2C File Offset: 0x00032F2C
		public int ReadAttributeRawValue(byte[] buffer, int offset, int count)
		{
			this.AssertGoodToUse(true);
			this.AssertInAttribute();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TnefStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TnefStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TnefStrings.CountTooLarge);
			}
			if (this.ReadStateValue == TnefReader.ReadState.BeginAttribute)
			{
				this.ReadStateValue = TnefReader.ReadState.ReadAttributeValue;
			}
			else if (this.ReadStateValue != TnefReader.ReadState.ReadAttributeValue)
			{
				if (this.ReadStateValue == TnefReader.ReadState.EndAttribute)
				{
					return 0;
				}
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationRawAfterProp);
			}
			int num = 0;
			while (this.attributeValueOffset < this.attributeValueLength && count != 0)
			{
				if (!this.EnsureMoreDataLoaded(1))
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
					this.ReadStateValue = TnefReader.ReadState.EndAttribute;
					this.error = true;
					break;
				}
				int num2 = Math.Min(count, Math.Min(this.AttributeRemainingCount(), this.AvailableCount()));
				this.ReadBytes(buffer, offset, num2);
				offset += num2;
				count -= num2;
				num += num2;
			}
			if (this.attributeValueOffset == this.attributeValueLength && !this.error)
			{
				this.VerifyAttributeChecksum();
			}
			return num;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00034E58 File Offset: 0x00033058
		public bool ReadNextAttribute()
		{
			this.AssertGoodToUse(true);
			if (this.ReadStateValue == TnefReader.ReadState.EndOfFile)
			{
				return false;
			}
			if (this.ReadStateValue > TnefReader.ReadState.EndAttribute && this.attributeValueOffset <= this.attributeValueLength)
			{
				this.SkipRemainderOfCurrentAttribute();
			}
			if (this.error)
			{
				this.ReadStateValue = TnefReader.ReadState.EndOfFile;
				return false;
			}
			if (!this.ReadAttributeHeader())
			{
				this.ReadStateValue = TnefReader.ReadState.EndOfFile;
				return false;
			}
			return true;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00034EB6 File Offset: 0x000330B6
		public void ResetComplianceStatus()
		{
			this.complianceStatus = TnefComplianceStatus.Compliant;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00034EBF File Offset: 0x000330BF
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00034EC7 File Offset: 0x000330C7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00034ED8 File Offset: 0x000330D8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.InputStream == null)
				{
					return;
				}
				if (this.Child != null)
				{
					if (this.Child is TnefReaderStreamWrapper)
					{
						(this.Child as TnefReaderStreamWrapper).Dispose();
					}
					else if (this.Child is TnefReader)
					{
						(this.Child as TnefReader).Dispose();
					}
				}
				if (this.parent == null)
				{
					this.InputStream.Dispose();
				}
				else
				{
					this.parent.propertyValueOffset += this.StreamOffset;
					this.parent.attributeValueOffset += this.StreamOffset;
					TnefReader tnefReader = this.parent;
					tnefReader.checksum += this.checksum;
					this.parent.streamOffset += this.parent.readOffset + this.streamOffset;
					this.parent.readBuffer = this.readBuffer;
					this.parent.readOffset = this.readOffset;
					this.parent.readEnd = this.readEndReal;
					this.parent.readEndReal = this.readEndReal;
					if (this.parent.streamOffset + this.parent.readEnd > this.parent.streamMaxLength)
					{
						this.parent.readEnd = this.parent.streamMaxLength - this.parent.streamOffset;
						this.parent.endOfFile = true;
					}
					this.parent.Child = null;
				}
			}
			this.InputStream = null;
			this.parent = null;
			this.readBuffer = null;
			this.fabricatedBuffer = null;
			this.decoder = null;
			this.unicodeDecoder = null;
			this.string8Decoder = null;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00035090 File Offset: 0x00033290
		private void ReadTnefHeader()
		{
			if (!this.EnsureMoreDataLoaded(6))
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidTnefSignature, TnefStrings.ReaderComplianceInvalidTnefSignature);
				this.ReadStateValue = TnefReader.ReadState.EndOfFile;
				this.error = true;
				return;
			}
			int num = this.ReadDword();
			if (num != 574529400)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidTnefSignature, TnefStrings.ReaderComplianceInvalidTnefSignature);
				this.ReadStateValue = TnefReader.ReadState.EndOfFile;
				this.error = true;
				return;
			}
			this.attachmentKey = this.ReadWord();
			if (this.EnsureMoreDataLoaded(15) && this.PeekByte(0) == 1 && this.PeekDword(1) == 561158 && this.PeekDword(5) == 4)
			{
				this.tnefVersion = this.PeekDword(9);
				if (this.tnefVersion > 65536)
				{
					this.SetComplianceStatus(TnefComplianceStatus.InvalidTnefVersion, TnefStrings.ReaderComplianceInvalidTnefVersion);
				}
			}
			if (this.messageCodepage == 0 && this.EnsureMoreDataLoaded(34) && this.PeekByte(15) == 1 && this.PeekDword(16) == 430087 && this.PeekDword(20) == 8)
			{
				int messageCodePage = this.PeekDword(24);
				if (!TnefCommon.IsUnicodeCodepage(messageCodePage))
				{
					this.messageCodepage = messageCodePage;
					this.string8Decoder = null;
				}
			}
			this.ReadStateValue = TnefReader.ReadState.Begin;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000351B0 File Offset: 0x000333B0
		private bool ReadAttributeHeader()
		{
			if (!this.EnsureMoreDataLoaded(9))
			{
				return false;
			}
			this.attributeLevel = (TnefAttributeLevel)this.ReadByte();
			this.attributeTag = (TnefAttributeTag)this.ReadDword();
			this.attributeValueLength = this.ReadDword();
			if (this.attributeValueLength < 0)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeLength, TnefStrings.ReaderComplianceInvalidAttributeLength);
				this.error = true;
				return false;
			}
			if (this.attributeLevel != TnefAttributeLevel.Message && this.attributeLevel != TnefAttributeLevel.Attachment)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeLevel, TnefStrings.ReaderComplianceInvalidAttributeLevel);
			}
			this.legacyAttribute = (this.attributeTag != TnefAttributeTag.MapiProperties && this.attributeTag != TnefAttributeTag.Attachment && this.attributeTag != TnefAttributeTag.RecipientTable);
			this.attributeValueStreamOffset = this.StreamOffset;
			this.attributeValueOffset = 0;
			this.attributeStartChecksum = this.checksum;
			this.ReadStateValue = TnefReader.ReadState.BeginAttribute;
			return this.PreviewAttributeContent();
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00035289 File Offset: 0x00033489
		private void SkipRemainderOfCurrentAttribute()
		{
			if (this.attributeValueOffset <= this.attributeValueLength)
			{
				if (this.attributeValueOffset < this.attributeValueLength)
				{
					this.EatAttributeBytes(this.AttributeRemainingCount());
				}
				if (!this.error)
				{
					this.VerifyAttributeChecksum();
				}
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000352C4 File Offset: 0x000334C4
		private void VerifyAttributeChecksum()
		{
			if (!this.EnsureMoreDataLoaded(2))
			{
				this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				this.error = true;
				return;
			}
			ushort num = this.checksum - this.attributeStartChecksum;
			ushort num2 = (ushort)this.ReadWord();
			if (!this.checksumDisabled && num != num2 && this.attributeTag != TnefAttributeTag.MessageClass && this.attributeTag != TnefAttributeTag.OriginalMessageClass)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeChecksum, TnefStrings.ReaderComplianceInvalidAttributeChecksum);
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0003533C File Offset: 0x0003353C
		internal bool ReadNextRow()
		{
			this.AssertGoodToUse(true);
			this.AssertInAttribute();
			if (this.attributeTag != TnefAttributeTag.RecipientTable)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationReadNextRowOnlyInRecipientTable);
			}
			if (this.ReadStateValue != TnefReader.ReadState.EndRow)
			{
				if (this.ReadStateValue == TnefReader.ReadState.EndAttribute)
				{
					return false;
				}
				if (this.ReadStateValue == TnefReader.ReadState.BeginAttribute)
				{
					this.ReadDword();
					this.rowIndex = -1;
				}
				else
				{
					if (this.ReadStateValue == TnefReader.ReadState.ReadAttributeValue)
					{
						throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationPropAfterRaw);
					}
					while (this.ReadNextProperty())
					{
					}
					if (this.error)
					{
						this.ReadStateValue = TnefReader.ReadState.EndAttribute;
						return false;
					}
				}
			}
			if (++this.rowIndex == this.rowCount)
			{
				this.SkipRemainderOfCurrentAttribute();
				this.ReadStateValue = TnefReader.ReadState.EndAttribute;
				return false;
			}
			if (!this.CheckAndEnsureMoreAttributeDataLoaded(4))
			{
				this.ReadStateValue = TnefReader.ReadState.EndAttribute;
				return false;
			}
			this.propertyCount = this.PeekDword(0);
			this.propertyIndex = -1;
			this.ReadStateValue = TnefReader.ReadState.BeginRow;
			return true;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00035420 File Offset: 0x00033620
		internal bool ReadNextProperty()
		{
			this.AssertGoodToUse(true);
			this.AssertInAttribute();
			if (this.ReadStateValue != TnefReader.ReadState.EndProperty)
			{
				if (this.ReadStateValue == TnefReader.ReadState.EndRow || this.ReadStateValue == TnefReader.ReadState.EndAttribute)
				{
					return false;
				}
				if (this.ReadStateValue == TnefReader.ReadState.BeginAttribute)
				{
					if (!this.PrepareFirstProperty())
					{
						this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
						this.error = true;
						return false;
					}
					this.propertyIndex = -1;
				}
				else if (this.ReadStateValue == TnefReader.ReadState.BeginRow)
				{
					this.ReadDword();
					this.propertyIndex = -1;
				}
				else
				{
					if (this.ReadStateValue == TnefReader.ReadState.ReadAttributeValue)
					{
						throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationPropAfterRaw);
					}
					if (!this.legacyAttribute)
					{
						if (this.propertyTag.IsMultiValued)
						{
							while (this.ReadNextPropertyValue())
							{
							}
						}
						else if (this.propertyTag.ValueTnefType == TnefPropertyType.Null)
						{
							this.ReadStateValue = TnefReader.ReadState.EndProperty;
						}
						else
						{
							if (this.ReadStateValue == TnefReader.ReadState.BeginProperty && !this.ReadNextPropertyValue())
							{
								this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
								return false;
							}
							if (this.propertyValueOffset != this.propertyValueLength)
							{
								this.EatPropertyBytes(this.PropertyRemainingCount());
							}
							if (this.propertyValuePaddingLength != 0)
							{
								this.EatAttributeBytes(this.propertyValuePaddingLength);
								this.propertyValuePaddingLength = 0;
							}
							if (this.propertyPaddingLength != 0)
							{
								this.EatAttributeBytes(this.propertyPaddingLength);
								this.propertyPaddingLength = 0;
							}
						}
						if (this.error)
						{
							this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
							return false;
						}
					}
				}
			}
			if (++this.propertyIndex == this.propertyCount)
			{
				if (this.attributeTag != TnefAttributeTag.RecipientTable)
				{
					this.SkipRemainderOfCurrentAttribute();
					this.ReadStateValue = TnefReader.ReadState.EndAttribute;
				}
				else
				{
					this.ReadStateValue = TnefReader.ReadState.EndRow;
				}
				return false;
			}
			if (this.legacyAttribute)
			{
				if (!this.PrepareLegacyProperty())
				{
					this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
					this.error = true;
					return false;
				}
				if (!this.CheckPropertyType())
				{
					this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
					return false;
				}
			}
			else
			{
				this.directRead = true;
				if (!this.CheckAndEnsureMoreAttributeDataLoaded(4))
				{
					this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
					return false;
				}
				this.propertyTag = this.ReadDword();
				if (!this.CheckPropertyType())
				{
					this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
					return false;
				}
				if (this.propertyTag.IsNamed)
				{
					if (!this.CheckAndEnsureMoreAttributeDataLoaded(24))
					{
						this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
						return false;
					}
					Guid propertySetGuid = this.ReadGuid();
					int num = this.ReadDword();
					if (num == 1)
					{
						int num2 = this.ReadDword();
						int num3 = (4 - num2 % 4) % 4;
						if (num2 <= 0 || num2 > 10240)
						{
							this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidNamedPropertyNameLength);
							this.error = true;
							this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
							return false;
						}
						if (!this.CheckAndEnsureMoreAttributeDataLoaded(num2 + num3))
						{
							this.error = true;
							this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
							return false;
						}
						if (this.PeekWord(num2 + num3 - 2) != 0)
						{
							this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidNamedPropertyNameNotZeroTerminated);
							this.error = true;
							this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
							return false;
						}
						string name = this.ReadAttributeUnicodeString(num2);
						if (num3 != 0)
						{
							this.SkipBytes(num3);
						}
						this.propertyNameId.Set(propertySetGuid, name);
					}
					else
					{
						this.propertyNameId.Set(propertySetGuid, this.ReadDword());
					}
				}
				this.propertyValueIndex = -1;
				if (this.propertyTag.IsMultiValued)
				{
					if (!this.CheckAndEnsureMoreAttributeDataLoaded(4))
					{
						this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
						return false;
					}
					this.propertyValueCount = this.ReadDword();
					if (this.propertyValueCount < 0)
					{
						this.SetComplianceStatus(TnefComplianceStatus.InvalidPropertyLength, TnefStrings.ReaderComplianceInvalidPropertyValueCount);
						this.error = true;
						this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
						return false;
					}
				}
				else if (this.propertyTag.ValueTnefType == TnefPropertyType.Null)
				{
					this.propertyValueCount = 0;
				}
				else
				{
					this.propertyValueCount = 1;
					if (this.propertyTag.ValueTnefType == TnefPropertyType.Binary || this.propertyTag.ValueTnefType == TnefPropertyType.String8 || this.propertyTag.ValueTnefType == TnefPropertyType.Unicode || this.propertyTag.ValueTnefType == TnefPropertyType.Object)
					{
						if (!this.CheckAndEnsureMoreAttributeDataLoaded(4))
						{
							this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
							return false;
						}
						int num4 = this.ReadDword();
						if (num4 != 1)
						{
							this.SetComplianceStatus(TnefComplianceStatus.InvalidPropertyLength, TnefStrings.ReaderComplianceInvalidPropertyValueCount);
							this.error = true;
							this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
							return false;
						}
					}
					else if (this.propertyValueFixedLength != 0 && !this.CheckAndEnsureMoreAttributeDataLoaded(Math.Max(this.propertyValueFixedLength, 4)))
					{
						this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
						return false;
					}
				}
				this.propertyPaddingLength = (4 - this.propertyValueCount * this.propertyValueFixedLength % 4) % 4;
				if (this.propertyValueFixedLength != 0 && this.propertyValueCount * this.propertyValueFixedLength + this.propertyPaddingLength > this.AttributeRemainingCount())
				{
					this.SetComplianceStatus(TnefComplianceStatus.AttributeOverflow, TnefStrings.ReaderComplianceAttributeValueOverflow);
					this.error = true;
					this.ReadStateValue = ((this.attributeTag != TnefAttributeTag.RecipientTable) ? TnefReader.ReadState.EndAttribute : TnefReader.ReadState.EndRow);
					return false;
				}
			}
			this.ReadStateValue = TnefReader.ReadState.BeginProperty;
			return true;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000359A4 File Offset: 0x00033BA4
		internal bool ReadNextPropertyValue()
		{
			this.AssertGoodToUse(true);
			this.AssertInProperty();
			if (this.ReadStateValue != TnefReader.ReadState.EndPropertyValue)
			{
				if (this.ReadStateValue == TnefReader.ReadState.EndProperty)
				{
					return false;
				}
				if (this.ReadStateValue == TnefReader.ReadState.BeginProperty)
				{
					this.propertyValueIndex = -1;
				}
				else
				{
					if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue && !this.propertyTag.IsMultiValued)
					{
						return true;
					}
					if (!this.legacyAttribute)
					{
						if (this.propertyValueOffset != this.propertyValueLength)
						{
							this.EatPropertyBytes(this.PropertyRemainingCount());
						}
						if (this.propertyValuePaddingLength != 0)
						{
							this.EatAttributeBytes(this.propertyValuePaddingLength);
							this.propertyValuePaddingLength = 0;
						}
						if (this.error)
						{
							this.ReadStateValue = TnefReader.ReadState.EndProperty;
							return false;
						}
					}
				}
			}
			if (++this.propertyValueIndex == this.propertyValueCount)
			{
				this.ReadStateValue = TnefReader.ReadState.EndProperty;
				return false;
			}
			this.ReadStateValue = TnefReader.ReadState.BeginPropertyValue;
			this.propertyValueOffset = 0;
			this.propertyValueLength = 0;
			this.propertyValuePaddingLength = 0;
			this.propertyValueStreamOffset = this.StreamOffset;
			if (this.legacyAttribute)
			{
				if (!this.PrepareLegacyPropertyValue())
				{
					this.ReadStateValue = (this.propertyTag.IsMultiValued ? TnefReader.ReadState.EndProperty : TnefReader.ReadState.BeginPropertyValue);
					return false;
				}
			}
			else
			{
				this.directRead = true;
				if (!this.CheckAndEnsureMoreAttributeDataLoaded(Math.Max(this.propertyValueFixedLength + this.propertyPaddingLength, 4)))
				{
					this.ReadStateValue = (this.propertyTag.IsMultiValued ? TnefReader.ReadState.EndProperty : TnefReader.ReadState.BeginPropertyValue);
					return false;
				}
				this.propertyValueLength = this.GetPropertyValueLength();
				this.propertyValueStreamOffset = this.StreamOffset;
				if (this.error)
				{
					this.ReadStateValue = (this.propertyTag.IsMultiValued ? TnefReader.ReadState.EndProperty : TnefReader.ReadState.BeginPropertyValue);
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00035B3E File Offset: 0x00033D3E
		internal bool IsLargePropertyValue
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInProperty();
				if (this.propertyTag.TnefType == TnefPropertyType.Null)
				{
					return false;
				}
				this.AssertInPropertyValue();
				return this.propertyValueLength > 32768 && this.propertyValueFixedLength == 0;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00035B7C File Offset: 0x00033D7C
		internal Type PropertyValueClrType
		{
			get
			{
				this.AssertGoodToUse(false);
				this.AssertInProperty();
				TnefPropertyType valueTnefType = this.propertyTag.ValueTnefType;
				if (valueTnefType <= TnefPropertyType.Unicode)
				{
					switch (valueTnefType)
					{
					case TnefPropertyType.I2:
						return typeof(short);
					case TnefPropertyType.Long:
						return typeof(int);
					case TnefPropertyType.R4:
						return typeof(float);
					case TnefPropertyType.Double:
						return typeof(double);
					case TnefPropertyType.Currency:
						return typeof(long);
					case TnefPropertyType.AppTime:
						return typeof(DateTime);
					case (TnefPropertyType)8:
					case (TnefPropertyType)9:
					case (TnefPropertyType)12:
					case (TnefPropertyType)14:
					case (TnefPropertyType)15:
					case (TnefPropertyType)16:
					case (TnefPropertyType)17:
					case (TnefPropertyType)18:
					case (TnefPropertyType)19:
						break;
					case TnefPropertyType.Error:
						return typeof(int);
					case TnefPropertyType.Boolean:
						return typeof(bool);
					case TnefPropertyType.Object:
						return typeof(byte[]);
					case TnefPropertyType.I8:
						return typeof(long);
					default:
						switch (valueTnefType)
						{
						case TnefPropertyType.String8:
							return typeof(string);
						case TnefPropertyType.Unicode:
							return typeof(string);
						}
						break;
					}
				}
				else
				{
					if (valueTnefType == TnefPropertyType.SysTime)
					{
						return typeof(DateTime);
					}
					if (valueTnefType == TnefPropertyType.ClassId)
					{
						return typeof(Guid);
					}
					if (valueTnefType == TnefPropertyType.Binary)
					{
						return typeof(byte[]);
					}
				}
				return null;
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00035CD4 File Offset: 0x00033ED4
		internal object ReadPropertyValue()
		{
			this.AssertGoodToUse(true);
			this.AssertInProperty();
			TnefPropertyType valueTnefType = this.propertyTag.ValueTnefType;
			if (valueTnefType <= TnefPropertyType.Unicode)
			{
				switch (valueTnefType)
				{
				case TnefPropertyType.I2:
					return this.ReadPropertyValueAsShort();
				case TnefPropertyType.Long:
					return this.ReadPropertyValueAsInt();
				case TnefPropertyType.R4:
					return this.ReadPropertyValueAsFloat();
				case TnefPropertyType.Double:
					return this.ReadPropertyValueAsDouble();
				case TnefPropertyType.Currency:
					return this.ReadPropertyValueAsLong();
				case TnefPropertyType.AppTime:
					return this.ReadPropertyValueAsDateTime();
				case (TnefPropertyType)8:
				case (TnefPropertyType)9:
				case (TnefPropertyType)12:
				case (TnefPropertyType)14:
				case (TnefPropertyType)15:
				case (TnefPropertyType)16:
				case (TnefPropertyType)17:
				case (TnefPropertyType)18:
				case (TnefPropertyType)19:
					break;
				case TnefPropertyType.Error:
					return this.ReadPropertyValueAsInt();
				case TnefPropertyType.Boolean:
					return this.ReadPropertyValueAsBool();
				case TnefPropertyType.Object:
					return this.ReadPropertyValueAsByteArray();
				case TnefPropertyType.I8:
					return this.ReadPropertyValueAsLong();
				default:
					switch (valueTnefType)
					{
					case TnefPropertyType.String8:
						return this.ReadPropertyValueAsString();
					case TnefPropertyType.Unicode:
						return this.ReadPropertyValueAsString();
					}
					break;
				}
			}
			else
			{
				if (valueTnefType == TnefPropertyType.SysTime)
				{
					return this.ReadPropertyValueAsDateTime();
				}
				if (valueTnefType == TnefPropertyType.ClassId)
				{
					return this.ReadPropertyValueAsGuid();
				}
				if (valueTnefType == TnefPropertyType.Binary)
				{
					return this.ReadPropertyValueAsByteArray();
				}
			}
			return null;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00035E2C File Offset: 0x0003402C
		internal bool ReadPropertyValueAsBool()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.propertyTag.ValueTnefType != TnefPropertyType.Boolean)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.error || !this.EnsureMorePropertyDataLoaded(this.propertyValueFixedLength))
			{
				if (!this.error)
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				}
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
				this.error = true;
				return false;
			}
			this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			bool result = 0 != this.ReadPropertyWord();
			if (this.propertyValueOffset == this.propertyValueLength)
			{
				this.ProcessEndOfValue();
			}
			return result;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00035EC0 File Offset: 0x000340C0
		internal short ReadPropertyValueAsShort()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.propertyTag.ValueTnefType != TnefPropertyType.I2 && this.propertyTag.ValueTnefType != TnefPropertyType.Boolean)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.error || !this.EnsureMorePropertyDataLoaded(this.propertyValueFixedLength))
			{
				if (!this.error)
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				}
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
				this.error = true;
				return 0;
			}
			this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			short result = this.ReadPropertyWord();
			if (this.propertyValueOffset == this.propertyValueLength)
			{
				this.ProcessEndOfValue();
			}
			return result;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00035F5C File Offset: 0x0003415C
		internal int ReadPropertyValueAsInt()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.propertyTag.ValueTnefType != TnefPropertyType.I2 && this.propertyTag.ValueTnefType != TnefPropertyType.Boolean && this.propertyTag.ValueTnefType != TnefPropertyType.Long && this.propertyTag.ValueTnefType != TnefPropertyType.Error)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.error || !this.EnsureMorePropertyDataLoaded(this.propertyValueFixedLength))
			{
				if (!this.error)
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				}
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
				this.error = true;
				return 0;
			}
			this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			int result;
			if (this.propertyTag.ValueTnefType == TnefPropertyType.I2 || this.propertyTag.ValueTnefType == TnefPropertyType.Boolean)
			{
				if (this.propertyTag.IsMultiValued || this.legacyAttribute)
				{
					result = (int)this.ReadPropertyWord();
				}
				else
				{
					this.propertyValueFixedLength = 4;
					this.propertyPaddingLength = 0;
					this.propertyValueLength = 4;
					result = this.ReadPropertyDword();
				}
			}
			else
			{
				result = this.ReadPropertyDword();
			}
			if (this.propertyValueOffset == this.propertyValueLength)
			{
				this.ProcessEndOfValue();
			}
			return result;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00036070 File Offset: 0x00034270
		internal long ReadPropertyValueAsLong()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.propertyTag.ValueTnefType != TnefPropertyType.I2 && this.propertyTag.ValueTnefType != TnefPropertyType.Boolean && this.propertyTag.ValueTnefType != TnefPropertyType.Long && this.propertyTag.ValueTnefType != TnefPropertyType.Error && this.propertyTag.ValueTnefType != TnefPropertyType.Currency && this.propertyTag.ValueTnefType != TnefPropertyType.I8 && this.propertyTag.ValueTnefType != TnefPropertyType.AppTime && this.propertyTag.ValueTnefType != TnefPropertyType.SysTime)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.error || !this.EnsureMorePropertyDataLoaded(this.propertyValueFixedLength))
			{
				if (!this.error)
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				}
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
				this.error = true;
				return 0L;
			}
			this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			long result;
			if (this.propertyTag.ValueTnefType == TnefPropertyType.I2 || this.propertyTag.ValueTnefType == TnefPropertyType.Boolean)
			{
				result = (long)this.ReadPropertyWord();
			}
			else if (this.propertyTag.ValueTnefType == TnefPropertyType.Long || this.propertyTag.ValueTnefType == TnefPropertyType.Error)
			{
				result = (long)this.ReadPropertyDword();
			}
			else
			{
				result = this.ReadPropertyQword();
			}
			if (this.propertyValueOffset == this.propertyValueLength)
			{
				this.ProcessEndOfValue();
			}
			return result;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000361B4 File Offset: 0x000343B4
		internal Guid ReadPropertyValueAsGuid()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.propertyTag.ValueTnefType != TnefPropertyType.ClassId)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.error || !this.EnsureMorePropertyDataLoaded(this.propertyValueFixedLength))
			{
				if (!this.error)
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				}
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
				this.error = true;
				return default(Guid);
			}
			this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			Guid result = this.ReadPropertyGuid();
			if (this.propertyValueOffset == this.propertyValueLength)
			{
				this.ProcessEndOfValue();
			}
			return result;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0003624C File Offset: 0x0003444C
		internal float ReadPropertyValueAsFloat()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.propertyTag.ValueTnefType != TnefPropertyType.R4)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.error || !this.EnsureMorePropertyDataLoaded(this.propertyValueFixedLength))
			{
				if (!this.error)
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				}
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
				this.error = true;
				return 0f;
			}
			this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			float result = this.ReadPropertyFloat();
			if (this.propertyValueOffset == this.propertyValueLength)
			{
				this.ProcessEndOfValue();
			}
			return result;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000362DC File Offset: 0x000344DC
		internal double ReadPropertyValueAsDouble()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.propertyTag.ValueTnefType != TnefPropertyType.R4 && this.propertyTag.ValueTnefType != TnefPropertyType.Double)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.error || !this.EnsureMorePropertyDataLoaded(this.propertyValueFixedLength))
			{
				if (!this.error)
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				}
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
				this.error = true;
				return 0.0;
			}
			this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			double result;
			if (this.propertyTag.ValueTnefType == TnefPropertyType.R4)
			{
				result = (double)this.ReadPropertyFloat();
			}
			else
			{
				result = this.ReadPropertyDouble();
			}
			if (this.propertyValueOffset == this.propertyValueLength)
			{
				this.ProcessEndOfValue();
			}
			return result;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00036398 File Offset: 0x00034598
		internal DateTime ReadPropertyValueAsDateTime()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.propertyTag.ValueTnefType != TnefPropertyType.AppTime && this.propertyTag.ValueTnefType != TnefPropertyType.SysTime)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.error || !this.EnsureMorePropertyDataLoaded(this.propertyValueFixedLength))
			{
				if (!this.error)
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				}
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
				this.error = true;
				return TnefReader.MinDateTime;
			}
			this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			DateTime result;
			if (this.propertyTag.ValueTnefType == TnefPropertyType.AppTime)
			{
				result = this.ReadPropertyAppTime();
			}
			else
			{
				result = this.ReadPropertySysTime();
			}
			if (this.propertyValueOffset == this.propertyValueLength)
			{
				this.ProcessEndOfValue();
			}
			return result;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00036450 File Offset: 0x00034650
		internal string ReadPropertyValueAsString()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.propertyTag.ValueTnefType != TnefPropertyType.String8 && this.propertyTag.ValueTnefType != TnefPropertyType.Unicode)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.IsLargePropertyValue)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationTextPropertyTooLong);
			}
			if (this.decodeBuffer == null)
			{
				this.decodeBuffer = new char[512];
			}
			int num = this.ReadPropertyTextValue(this.decodeBuffer, 0, this.decodeBuffer.Length);
			if (this.ReadStateValue != TnefReader.ReadState.EndPropertyValue)
			{
				StringBuilder stringBuilder = new StringBuilder();
				do
				{
					stringBuilder.Append(this.decodeBuffer, 0, num);
					num = this.ReadPropertyTextValue(this.decodeBuffer, 0, this.decodeBuffer.Length);
				}
				while (num != 0);
				return stringBuilder.ToString();
			}
			if (num == 0)
			{
				return string.Empty;
			}
			return new string(this.decodeBuffer, 0, num);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00036524 File Offset: 0x00034724
		internal byte[] ReadPropertyValueAsByteArray()
		{
			this.AssertAtTheBeginningOfPropertyValue();
			if (this.IsLargePropertyValue)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationPropertyRawValueTooLong);
			}
			byte[] array = new byte[this.PropertyRawValueLength];
			this.ReadPropertyRawValue(array, 0, array.Length, false);
			return array;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00036564 File Offset: 0x00034764
		internal int ReadPropertyTextValue(char[] buffer, int offset, int count)
		{
			this.AssertGoodToUse(true);
			if (this.ReadStateValue < TnefReader.ReadState.BeginPropertyValue)
			{
				if (this.ReadStateValue == TnefReader.ReadState.EndPropertyValue)
				{
					return 0;
				}
				if (this.ReadStateValue != TnefReader.ReadState.BeginProperty || this.propertyTag.IsMultiValued || this.propertyTag.ValueTnefType == TnefPropertyType.Null)
				{
					throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationMustBeInProperty);
				}
				this.ReadNextPropertyValue();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TnefStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TnefStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TnefStrings.CountTooLarge);
			}
			if (this.propertyTag.ValueTnefType != TnefPropertyType.String8 && this.propertyTag.ValueTnefType != TnefPropertyType.Unicode)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationCannotConvertValue);
			}
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				if (this.propertyTag.ValueTnefType == TnefPropertyType.String8)
				{
					if (this.string8Decoder == null)
					{
						Encoding encoding = null;
						if (this.messageCodepage == 0)
						{
							encoding = Charset.DefaultWindowsCharset.GetEncoding();
							this.messageCodepage = CodePageMap.GetCodePage(encoding);
						}
						else if (!Charset.TryGetEncoding(this.messageCodepage, out encoding))
						{
							this.SetComplianceStatus(TnefComplianceStatus.InvalidMessageCodepage, TnefStrings.ReaderComplianceInvalidMessageCodepage);
							encoding = Charset.DefaultWindowsCharset.GetEncoding();
						}
						int codePage = CodePageMap.GetCodePage(encoding);
						if (TnefCommon.IsUnicodeCodepage(codePage))
						{
							this.messageCodepage = 1252;
							encoding = Charset.GetEncoding(this.messageCodepage);
						}
						this.string8Decoder = encoding.GetDecoder();
					}
					this.decoder = this.string8Decoder;
				}
				else
				{
					this.decoder = this.unicodeDecoder;
				}
				this.decoder.Reset();
				this.decoderFlushed = false;
				this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			}
			else if (this.decoder == null)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationPropTextAfterRaw);
			}
			int num = 0;
			while ((this.PropertyRemainingCount() != 0 || !this.decoderFlushed) && count > 12)
			{
				if (this.MorePropertyData(1) && !this.EnsureMorePropertyDataLoaded(1))
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
					this.error = true;
					break;
				}
				int num2 = this.PropertyAvailableCount();
				int count2;
				int num3;
				if (this.directRead)
				{
					this.decoder.Convert(this.readBuffer, this.readOffset, num2, buffer, offset, count, num2 == this.PropertyRemainingCount(), out count2, out num3, out this.decoderFlushed);
				}
				else
				{
					this.decoder.Convert(this.fabricatedBuffer, this.fabricatedOffset, num2, buffer, offset, count, num2 == this.PropertyRemainingCount(), out count2, out num3, out this.decoderFlushed);
				}
				this.SkipPropertyBytes(count2);
				offset += num3;
				count -= num3;
				num += num3;
				for (int i = offset - num3; i < offset; i++)
				{
					if (buffer[i] == '\0')
					{
						num -= offset - i;
						this.EatPropertyBytes(this.PropertyRemainingCount());
						this.decoderFlushed = true;
						break;
					}
				}
			}
			if (this.error)
			{
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
			}
			else if (this.propertyValueOffset == this.propertyValueLength && this.decoderFlushed)
			{
				this.ProcessEndOfValue();
			}
			return num;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00036870 File Offset: 0x00034A70
		internal int ReadPropertyRawValue(byte[] buffer, int offset, int count, bool fromWrapper)
		{
			this.AssertGoodToUse(!fromWrapper);
			if (this.ReadStateValue < TnefReader.ReadState.BeginPropertyValue)
			{
				if (this.ReadStateValue == TnefReader.ReadState.EndPropertyValue)
				{
					return 0;
				}
				if (this.ReadStateValue != TnefReader.ReadState.BeginProperty || this.propertyTag.IsMultiValued || this.propertyTag.ValueTnefType == TnefPropertyType.Null)
				{
					throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationMustBeInProperty);
				}
				this.ReadNextPropertyValue();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", TnefStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", TnefStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", TnefStrings.CountTooLarge);
			}
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				this.decoder = null;
				this.ReadStateValue = TnefReader.ReadState.ReadPropertyValue;
			}
			else if (this.decoder != null)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationPropRawAfterText);
			}
			int num = 0;
			while (this.MorePropertyData(1) && count != 0)
			{
				if (!this.EnsureMorePropertyDataLoaded(1))
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
					this.error = true;
					break;
				}
				int num2 = Math.Min(count, this.PropertyAvailableCount());
				this.ReadPropertyBytes(buffer, offset, num2);
				offset += num2;
				count -= num2;
				num += num2;
			}
			if (this.error)
			{
				this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
			}
			else if (this.propertyValueOffset == this.propertyValueLength)
			{
				this.ProcessEndOfValue();
			}
			return num;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000369D8 File Offset: 0x00034BD8
		internal TnefReader GetEmbeddedMessageReader()
		{
			this.AssertGoodToUse(true);
			this.AssertAtTheBeginningOfPropertyValue();
			if (!this.IsPropertyEmbeddedMessage)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationNotEmbeddedMessage);
			}
			if (this.embeddingDepth + 1 == 100)
			{
				this.SetComplianceStatus(TnefComplianceStatus.NestingTooDeep, TnefStrings.ReaderComplianceTooDeepEmbedding);
			}
			return new TnefReader(this);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00036A27 File Offset: 0x00034C27
		internal Stream GetRawPropertyValueReadStream()
		{
			this.AssertGoodToUse(true);
			this.AssertAtTheBeginningOfPropertyValue();
			return new TnefReaderStreamWrapper(this);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00036A3C File Offset: 0x00034C3C
		private void ProcessEndOfValue()
		{
			if (this.propertyValuePaddingLength != 0)
			{
				if (!this.EnsureMoreDataLoaded(this.propertyValuePaddingLength))
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
					this.error = true;
					this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
					return;
				}
				this.SkipBytes(this.propertyValuePaddingLength);
				this.propertyValuePaddingLength = 0;
			}
			if (!this.propertyTag.IsMultiValued && this.propertyPaddingLength != 0)
			{
				if (!this.EnsureMoreDataLoaded(this.propertyPaddingLength))
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
					this.error = true;
					this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
					return;
				}
				this.SkipBytes(this.propertyPaddingLength);
				this.propertyPaddingLength = 0;
			}
			this.ReadStateValue = TnefReader.ReadState.EndPropertyValue;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00036AEC File Offset: 0x00034CEC
		private bool CheckPropertyType()
		{
			TnefPropertyType valueTnefType = this.propertyTag.ValueTnefType;
			if (valueTnefType <= TnefPropertyType.Unicode)
			{
				switch (valueTnefType)
				{
				case TnefPropertyType.Null:
					this.SetComplianceStatus(TnefComplianceStatus.UnsupportedPropertyType, TnefStrings.ReaderComplianceInvalidPropertyTypeError);
					if (!this.propertyTag.IsMultiValued)
					{
						this.propertyValueFixedLength = 0;
						return true;
					}
					goto IL_186;
				case TnefPropertyType.I2:
					this.propertyValueFixedLength = 2;
					return true;
				case TnefPropertyType.Long:
				case TnefPropertyType.R4:
					this.propertyValueFixedLength = 4;
					return true;
				case TnefPropertyType.Double:
				case TnefPropertyType.Currency:
				case TnefPropertyType.AppTime:
				case TnefPropertyType.I8:
					break;
				case (TnefPropertyType)8:
				case (TnefPropertyType)9:
				case (TnefPropertyType)12:
				case (TnefPropertyType)14:
				case (TnefPropertyType)15:
				case (TnefPropertyType)16:
				case (TnefPropertyType)17:
				case (TnefPropertyType)18:
				case (TnefPropertyType)19:
					goto IL_186;
				case TnefPropertyType.Error:
					this.propertyValueFixedLength = 4;
					this.SetComplianceStatus(TnefComplianceStatus.UnsupportedPropertyType, TnefStrings.ReaderComplianceInvalidPropertyTypeError);
					return true;
				case TnefPropertyType.Boolean:
					this.propertyValueFixedLength = 2;
					if (this.propertyTag.IsMultiValued)
					{
						this.SetComplianceStatus(TnefComplianceStatus.UnsupportedPropertyType, TnefStrings.ReaderComplianceInvalidPropertyTypeMvBoolean);
						return true;
					}
					return true;
				case TnefPropertyType.Object:
					this.propertyValueFixedLength = 0;
					if (this.propertyTag.IsMultiValued)
					{
						this.SetComplianceStatus(TnefComplianceStatus.UnsupportedPropertyType, TnefStrings.ReaderComplianceInvalidPropertyTypeMvObject);
						goto IL_186;
					}
					if (this.attributeTag == TnefAttributeTag.RecipientTable)
					{
						this.SetComplianceStatus(TnefComplianceStatus.UnsupportedPropertyType, TnefStrings.ReaderComplianceInvalidPropertyTypeObjectInRecipientTable);
						return true;
					}
					return true;
				default:
					switch (valueTnefType)
					{
					case TnefPropertyType.String8:
					case TnefPropertyType.Unicode:
						goto IL_17D;
					default:
						goto IL_186;
					}
					break;
				}
			}
			else if (valueTnefType != TnefPropertyType.SysTime)
			{
				if (valueTnefType == TnefPropertyType.ClassId)
				{
					this.propertyValueFixedLength = 16;
					return true;
				}
				if (valueTnefType != TnefPropertyType.Binary)
				{
					goto IL_186;
				}
				goto IL_17D;
			}
			this.propertyValueFixedLength = 8;
			return true;
			IL_17D:
			this.propertyValueFixedLength = 0;
			return true;
			IL_186:
			this.propertyValueFixedLength = 0;
			this.SetComplianceStatus(TnefComplianceStatus.UnsupportedPropertyType, TnefStrings.ReaderComplianceInvalidPropertyType);
			this.error = true;
			return false;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00036CA0 File Offset: 0x00034EA0
		private int GetPropertyValueLength()
		{
			TnefPropertyType valueTnefType = this.propertyTag.ValueTnefType;
			if (valueTnefType != TnefPropertyType.Object)
			{
				switch (valueTnefType)
				{
				case TnefPropertyType.String8:
				case TnefPropertyType.Unicode:
					break;
				default:
					if (valueTnefType != TnefPropertyType.Binary)
					{
						this.propertyValuePaddingLength = 0;
						return this.propertyValueFixedLength;
					}
					break;
				}
				int num = this.ReadDword();
				this.propertyValuePaddingLength = (4 - num % 4) % 4;
				if (num < 0 || (long)this.attributeValueOffset + (long)num + (long)this.propertyValuePaddingLength > (long)this.attributeValueLength)
				{
					this.SetComplianceStatus(TnefComplianceStatus.InvalidPropertyLength, TnefStrings.ReaderComplianceInvalidPropertyLength);
					this.error = true;
					return 0;
				}
				return num;
			}
			else
			{
				int num = this.ReadDword();
				this.propertyValuePaddingLength = (4 - num % 4) % 4;
				if (num < 16 || (long)this.attributeValueOffset + (long)num + (long)this.propertyValuePaddingLength > (long)this.attributeValueLength)
				{
					if (num < 0 || (long)this.attributeValueOffset + (long)num + (long)this.propertyValuePaddingLength > (long)this.attributeValueLength)
					{
						this.SetComplianceStatus(TnefComplianceStatus.InvalidPropertyLength, TnefStrings.ReaderComplianceInvalidPropertyLength);
						this.error = true;
						return 0;
					}
					this.SetComplianceStatus(TnefComplianceStatus.InvalidPropertyLength, TnefStrings.ReaderComplianceInvalidPropertyLengthObject);
					return num;
				}
				else
				{
					if (!this.CheckAndEnsureMoreAttributeDataLoaded(16))
					{
						this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
						return num;
					}
					this.propertyValueIId = this.ReadGuid();
					return num - 16;
				}
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00036DE4 File Offset: 0x00034FE4
		private short ReadPropertyWord()
		{
			this.propertyValueOffset += 2;
			if (!this.directRead)
			{
				short result = BitConverter.ToInt16(this.fabricatedBuffer, this.fabricatedOffset);
				this.fabricatedOffset += 2;
				return result;
			}
			return this.ReadWord();
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00036E30 File Offset: 0x00035030
		private int ReadPropertyDword()
		{
			this.propertyValueOffset += 4;
			if (!this.directRead)
			{
				int result = BitConverter.ToInt32(this.fabricatedBuffer, this.fabricatedOffset);
				this.fabricatedOffset += 4;
				return result;
			}
			return this.ReadDword();
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00036E7C File Offset: 0x0003507C
		private long ReadPropertyQword()
		{
			this.propertyValueOffset += 8;
			if (!this.directRead)
			{
				long result = BitConverter.ToInt64(this.fabricatedBuffer, this.fabricatedOffset);
				this.fabricatedOffset += 8;
				return result;
			}
			return this.ReadQword();
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00036EC8 File Offset: 0x000350C8
		private Guid ReadPropertyGuid()
		{
			this.propertyValueOffset += 16;
			if (!this.directRead)
			{
				Guid result = new Guid(BitConverter.ToInt32(this.fabricatedBuffer, this.fabricatedOffset), BitConverter.ToInt16(this.fabricatedBuffer, this.fabricatedOffset + 4), BitConverter.ToInt16(this.fabricatedBuffer, this.fabricatedOffset + 6), this.fabricatedBuffer[this.fabricatedOffset + 8], this.fabricatedBuffer[this.fabricatedOffset + 9], this.fabricatedBuffer[this.fabricatedOffset + 10], this.fabricatedBuffer[this.fabricatedOffset + 11], this.fabricatedBuffer[this.fabricatedOffset + 12], this.fabricatedBuffer[this.fabricatedOffset + 13], this.fabricatedBuffer[this.fabricatedOffset + 14], this.fabricatedBuffer[this.fabricatedOffset + 15]);
				this.fabricatedOffset += 16;
				return result;
			}
			return this.ReadGuid();
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00036FC4 File Offset: 0x000351C4
		private float ReadPropertyFloat()
		{
			this.propertyValueOffset += 4;
			if (!this.directRead)
			{
				float result = BitConverter.ToSingle(this.fabricatedBuffer, this.fabricatedOffset);
				this.fabricatedOffset += 4;
				return result;
			}
			return this.ReadFloat();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00037010 File Offset: 0x00035210
		private double ReadPropertyDouble()
		{
			this.propertyValueOffset += 8;
			if (!this.directRead)
			{
				double result = BitConverter.ToDouble(this.fabricatedBuffer, this.fabricatedOffset);
				this.fabricatedOffset += 8;
				return result;
			}
			return this.ReadDouble();
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0003705C File Offset: 0x0003525C
		private DateTime ReadPropertyAppTime()
		{
			double num = this.ReadPropertyDouble();
			DateTime result;
			try
			{
				if (num != 0.0)
				{
					result = DateTime.SpecifyKind(TnefReader.FromOADate(num), DateTimeKind.Utc);
				}
				else
				{
					result = TnefReader.MinDateTime;
				}
			}
			catch (ArgumentException)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidDate, TnefStrings.ReaderComplianceInvalidPropertyValueDate);
				result = DateTime.UtcNow;
			}
			return result;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x000370C0 File Offset: 0x000352C0
		private DateTime ReadPropertySysTime()
		{
			long num = this.ReadPropertyQword();
			DateTime result;
			try
			{
				if (num != 0L)
				{
					result = DateTime.FromFileTimeUtc(num);
				}
				else
				{
					result = TnefReader.MinDateTime;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidDate, TnefStrings.ReaderComplianceInvalidPropertyValueDate);
				result = DateTime.UtcNow;
			}
			return result;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00037114 File Offset: 0x00035314
		private void ReadPropertyBytes(byte[] buffer, int offset, int count)
		{
			this.propertyValueOffset += count;
			if (!this.directRead)
			{
				Buffer.BlockCopy(this.fabricatedBuffer, this.fabricatedOffset, buffer, offset, count);
				this.fabricatedOffset += count;
				return;
			}
			this.ReadBytes(buffer, offset, count);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00037163 File Offset: 0x00035363
		private void SkipPropertyBytes(int count)
		{
			this.propertyValueOffset += count;
			if (!this.directRead)
			{
				this.fabricatedOffset += count;
				return;
			}
			this.SkipBytes(count);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00037194 File Offset: 0x00035394
		private void EatPropertyBytes(int count)
		{
			while (this.MorePropertyData(1) && count != 0)
			{
				if (!this.EnsureMorePropertyDataLoaded(1))
				{
					this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
					this.error = true;
					return;
				}
				int num = Math.Min(count, this.PropertyAvailableCount());
				this.SkipPropertyBytes(num);
				count -= num;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000371E6 File Offset: 0x000353E6
		private int PropertyRemainingCount()
		{
			return this.propertyValueLength - this.propertyValueOffset;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000371F5 File Offset: 0x000353F5
		private int PropertyAvailableCount()
		{
			if (!this.directRead)
			{
				return this.fabricatedEnd - this.fabricatedOffset;
			}
			return Math.Min(this.AvailableCount(), this.PropertyRemainingCount());
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0003721E File Offset: 0x0003541E
		private bool MorePropertyData(int count)
		{
			return this.propertyValueOffset + count <= this.propertyValueLength;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00037233 File Offset: 0x00035433
		private bool EnsureMorePropertyDataLoaded(int count)
		{
			if (!this.directRead)
			{
				return this.EnsureMoreFabricatedDataAvailable(count);
			}
			return this.EnsureMoreDataLoaded(count);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0003724C File Offset: 0x0003544C
		private bool EnsureMoreFabricatedDataAvailable(int count)
		{
			return this.fabricatedOffset + count <= this.fabricatedEnd || this.FabricateMorePropertyData(count);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00037268 File Offset: 0x00035468
		private bool PreviewAttributeContent()
		{
			this.rowCount = -1;
			this.propertyCount = 0;
			TnefAttributeTag tnefAttributeTag = this.attributeTag;
			if (tnefAttributeTag <= TnefAttributeTag.DateModified)
			{
				if (tnefAttributeTag <= TnefAttributeTag.AttachTitle)
				{
					if (tnefAttributeTag <= TnefAttributeTag.From)
					{
						if (tnefAttributeTag == TnefAttributeTag.Null)
						{
							return true;
						}
						if (tnefAttributeTag != TnefAttributeTag.From)
						{
							return true;
						}
						this.propertyCount = 2;
						return true;
					}
					else if (tnefAttributeTag != TnefAttributeTag.Subject)
					{
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.MessageId:
						case TnefAttributeTag.ParentId:
						case TnefAttributeTag.ConversationId:
							goto IL_2CF;
						default:
							if (tnefAttributeTag != TnefAttributeTag.AttachTitle)
							{
								return true;
							}
							goto IL_324;
						}
					}
				}
				else if (tnefAttributeTag <= TnefAttributeTag.DateEnd)
				{
					if (tnefAttributeTag == TnefAttributeTag.Body)
					{
						goto IL_2CF;
					}
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.DateStart:
					case TnefAttributeTag.DateEnd:
						goto IL_381;
					default:
						return true;
					}
				}
				else
				{
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.DateSent:
					case TnefAttributeTag.DateReceived:
						break;
					default:
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.AttachCreateDate:
						case TnefAttributeTag.AttachModifyDate:
							goto IL_324;
						default:
							if (tnefAttributeTag != TnefAttributeTag.DateModified)
							{
								return true;
							}
							break;
						}
						break;
					}
				}
				this.propertyCount = 1;
				return true;
			}
			if (tnefAttributeTag <= TnefAttributeTag.MessageStatus)
			{
				if (tnefAttributeTag <= TnefAttributeTag.Priority)
				{
					if (tnefAttributeTag == TnefAttributeTag.RequestResponse)
					{
						goto IL_381;
					}
					if (tnefAttributeTag != TnefAttributeTag.Priority)
					{
						return true;
					}
				}
				else
				{
					if (tnefAttributeTag == TnefAttributeTag.AidOwner)
					{
						goto IL_381;
					}
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.Owner:
					case TnefAttributeTag.SentFor:
						if (this.messageClassSPlus)
						{
							this.propertyCount = 2;
							return true;
						}
						this.propertyCount = 0;
						return true;
					case TnefAttributeTag.Delegate:
						goto IL_381;
					default:
						if (tnefAttributeTag != TnefAttributeTag.MessageStatus)
						{
							return true;
						}
						break;
					}
				}
			}
			else if (tnefAttributeTag <= TnefAttributeTag.OemCodepage)
			{
				switch (tnefAttributeTag)
				{
				case TnefAttributeTag.AttachData:
					this.propertyCount = 1;
					if (this.currentAttachmentIsOle)
					{
						this.propertyCount++;
						return true;
					}
					return true;
				case (TnefAttributeTag)426000:
					return true;
				case TnefAttributeTag.AttachMetaFile:
					goto IL_324;
				default:
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.AttachTransportFilename:
						goto IL_324;
					case TnefAttributeTag.AttachRenderData:
						if (this.CheckAndEnsureMoreAttributeDataLoaded(14))
						{
							short num = this.PeekWord(0);
							int num2 = this.PeekDword(10);
							this.propertyCount = 2;
							if (num2 == 1)
							{
								this.propertyCount++;
							}
							this.currentAttachmentIsOle = (num != 1);
							return true;
						}
						return true;
					case TnefAttributeTag.MapiProperties:
					case TnefAttributeTag.Attachment:
						if (!this.CheckAndEnsureMoreAttributeDataLoaded(4))
						{
							return false;
						}
						this.propertyCount = this.PeekDword(0);
						if (this.propertyCount < 0)
						{
							this.SetComplianceStatus(TnefComplianceStatus.InvalidRowCount, TnefStrings.ReaderComplianceInvalidPropertyCount);
							this.error = true;
							return false;
						}
						return true;
					case TnefAttributeTag.RecipientTable:
						this.propertyCount = -1;
						if (!this.CheckAndEnsureMoreAttributeDataLoaded(4))
						{
							return false;
						}
						this.rowCount = this.PeekDword(0);
						if (this.rowCount < 0)
						{
							this.SetComplianceStatus(TnefComplianceStatus.InvalidRowCount, TnefStrings.ReaderComplianceInvalidRowCount);
							this.error = true;
							this.rowCount = 0;
							return false;
						}
						return true;
					case (TnefAttributeTag)430086:
						return true;
					case TnefAttributeTag.OemCodepage:
					{
						if (!this.CheckAndEnsureMoreAttributeDataLoaded(8))
						{
							this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeLength, TnefStrings.ReaderComplianceInvalidOemCodepageAttributeLength);
							return true;
						}
						int messageCodePage = this.PeekDword(0);
						if (this.messageCodepage == 0 && !TnefCommon.IsUnicodeCodepage(messageCodePage))
						{
							this.messageCodepage = messageCodePage;
							this.string8Decoder = null;
							return true;
						}
						return true;
					}
					default:
						return true;
					}
					break;
				}
			}
			else if (tnefAttributeTag != TnefAttributeTag.OriginalMessageClass && tnefAttributeTag != TnefAttributeTag.MessageClass)
			{
				if (tnefAttributeTag != TnefAttributeTag.TnefVersion)
				{
					return true;
				}
				if (!this.CheckAndEnsureMoreAttributeDataLoaded(4))
				{
					this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeLength, TnefStrings.ReaderComplianceInvalidTnefVersionAttributeLength);
					return true;
				}
				this.tnefVersion = this.PeekDword(0);
				if (this.tnefVersion > 65536)
				{
					this.SetComplianceStatus(TnefComplianceStatus.InvalidTnefVersion, TnefStrings.ReaderComplianceInvalidTnefVersion);
					return true;
				}
				return true;
			}
			else
			{
				if (this.attributeValueLength == 0 || this.attributeValueLength > 255)
				{
					this.SetComplianceStatus(TnefComplianceStatus.InvalidMessageClass, TnefStrings.ReaderComplianceInvalidMessageClassLength);
					this.directReadForMessageClass = true;
					return true;
				}
				if (!this.CheckAndEnsureMoreAttributeDataLoaded(this.attributeValueLength))
				{
					return false;
				}
				if (this.PeekByte(this.attributeValueLength - 1) != 0)
				{
					this.SetComplianceStatus(TnefComplianceStatus.InvalidMessageClass, TnefStrings.ReaderComplianceInvalidMessageClassNotZeroTerminated);
				}
				this.propertyCount = 1;
				this.FabricateMessageClass(this.attributeTag == TnefAttributeTag.MessageClass);
				return true;
			}
			IL_2CF:
			this.propertyCount = 1;
			return true;
			IL_324:
			this.propertyCount = 1;
			return true;
			IL_381:
			this.propertyCount = 1;
			return true;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00037688 File Offset: 0x00035888
		private bool PrepareFirstProperty()
		{
			TnefAttributeTag tnefAttributeTag = this.attributeTag;
			if (tnefAttributeTag <= TnefAttributeTag.DateModified)
			{
				if (tnefAttributeTag <= TnefAttributeTag.AttachTitle)
				{
					if (tnefAttributeTag <= TnefAttributeTag.From)
					{
						if (tnefAttributeTag != TnefAttributeTag.Null)
						{
							if (tnefAttributeTag == TnefAttributeTag.From)
							{
								if (this.attributeValueLength <= 8 || this.attributeValueLength > 32768)
								{
									this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidFrom);
									return false;
								}
								if (!this.CheckAndEnsureMoreAttributeDataLoaded(this.attributeValueLength))
								{
									return false;
								}
								if (!this.CrackTriple())
								{
									return false;
								}
							}
						}
					}
					else if (tnefAttributeTag != TnefAttributeTag.Subject)
					{
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.MessageId:
						case TnefAttributeTag.ParentId:
						case TnefAttributeTag.ConversationId:
							if (this.attributeValueLength == 0)
							{
								this.SetComplianceStatus(TnefComplianceStatus.InvalidAttribute, TnefStrings.ReaderComplianceInvalidConversationId);
							}
							break;
						default:
							if (tnefAttributeTag != TnefAttributeTag.AttachTitle)
							{
							}
							break;
						}
					}
				}
				else if (tnefAttributeTag <= TnefAttributeTag.DateEnd)
				{
					if (tnefAttributeTag != TnefAttributeTag.Body)
					{
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.DateStart:
						case TnefAttributeTag.DateEnd:
							if (!this.CheckAndEnsureMoreAttributeDataLoaded(14))
							{
								return false;
							}
							break;
						}
					}
				}
				else
				{
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.DateSent:
					case TnefAttributeTag.DateReceived:
						break;
					default:
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.AttachCreateDate:
						case TnefAttributeTag.AttachModifyDate:
							if (!this.CheckAndEnsureMoreAttributeDataLoaded(14))
							{
								return false;
							}
							return true;
						default:
							if (tnefAttributeTag != TnefAttributeTag.DateModified)
							{
								return true;
							}
							break;
						}
						break;
					}
					if (!this.CheckAndEnsureMoreAttributeDataLoaded(14))
					{
						this.error = true;
						return false;
					}
				}
			}
			else if (tnefAttributeTag <= TnefAttributeTag.MessageStatus)
			{
				if (tnefAttributeTag <= TnefAttributeTag.Priority)
				{
					if (tnefAttributeTag != TnefAttributeTag.RequestResponse)
					{
						if (tnefAttributeTag == TnefAttributeTag.Priority)
						{
							if (!this.CheckAndEnsureMoreAttributeDataLoaded(2))
							{
								return false;
							}
						}
					}
					else if (!this.CheckAndEnsureMoreAttributeDataLoaded(2))
					{
						return false;
					}
				}
				else if (tnefAttributeTag != TnefAttributeTag.AidOwner)
				{
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.Owner:
					case TnefAttributeTag.SentFor:
						if (this.messageClassSPlus)
						{
							if (this.attributeValueLength <= 4 || this.attributeValueLength > 32768)
							{
								this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidSchedulePlus);
								return false;
							}
							if (!this.CheckAndEnsureMoreAttributeDataLoaded(this.attributeValueLength))
							{
								return false;
							}
							if (!this.CrackSchedulePlusId())
							{
								return false;
							}
						}
						break;
					case TnefAttributeTag.Delegate:
						break;
					default:
						if (tnefAttributeTag == TnefAttributeTag.MessageStatus)
						{
							if (!this.CheckAndEnsureMoreAttributeDataLoaded(1))
							{
								return false;
							}
						}
						break;
					}
				}
				else if (!this.CheckAndEnsureMoreAttributeDataLoaded(4))
				{
					return false;
				}
			}
			else if (tnefAttributeTag <= TnefAttributeTag.OemCodepage)
			{
				switch (tnefAttributeTag)
				{
				case TnefAttributeTag.AttachData:
				case (TnefAttributeTag)426000:
				case TnefAttributeTag.AttachMetaFile:
					break;
				default:
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.MapiProperties:
					case TnefAttributeTag.Attachment:
						this.ReadDword();
						break;
					case TnefAttributeTag.RecipientTable:
						throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationMustBeInARow);
					}
					break;
				}
			}
			else if (tnefAttributeTag != TnefAttributeTag.OriginalMessageClass && tnefAttributeTag != TnefAttributeTag.MessageClass && tnefAttributeTag != TnefAttributeTag.TnefVersion)
			{
			}
			return true;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00037958 File Offset: 0x00035B58
		private bool PrepareLegacyProperty()
		{
			this.propertyPaddingLength = 0;
			this.propertyValueCount = 1;
			this.directRead = false;
			TnefAttributeTag tnefAttributeTag = this.attributeTag;
			if (tnefAttributeTag <= TnefAttributeTag.DateModified)
			{
				if (tnefAttributeTag <= TnefAttributeTag.AttachTitle)
				{
					if (tnefAttributeTag <= TnefAttributeTag.Subject)
					{
						if (tnefAttributeTag != TnefAttributeTag.From)
						{
							if (tnefAttributeTag == TnefAttributeTag.Subject)
							{
								this.propertyTag = TnefPropertyTag.SubjectA;
								this.directRead = true;
								return true;
							}
						}
						else
						{
							if (this.propertyIndex == 0)
							{
								this.propertyTag = TnefPropertyTag.SenderEntryId;
								return true;
							}
							this.propertyTag = TnefPropertyTag.SenderNameA;
							return true;
						}
					}
					else
					{
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.MessageId:
							this.propertyTag = TnefPropertyTag.SearchKey;
							return true;
						case TnefAttributeTag.ParentId:
							this.propertyTag = TnefPropertyTag.ParentKey;
							return true;
						case TnefAttributeTag.ConversationId:
							this.propertyTag = TnefPropertyTag.ConversationKey;
							return true;
						default:
							if (tnefAttributeTag == TnefAttributeTag.AttachTitle)
							{
								this.propertyTag = TnefPropertyTag.AttachFilenameA;
								this.directRead = true;
								return true;
							}
							break;
						}
					}
				}
				else if (tnefAttributeTag <= TnefAttributeTag.DateEnd)
				{
					if (tnefAttributeTag == TnefAttributeTag.Body)
					{
						this.propertyTag = TnefPropertyTag.BodyA;
						this.directRead = true;
						return true;
					}
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.DateStart:
						this.propertyTag = TnefPropertyTag.StartDate;
						return true;
					case TnefAttributeTag.DateEnd:
						this.propertyTag = TnefPropertyTag.EndDate;
						return true;
					}
				}
				else
				{
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.DateSent:
						this.propertyTag = TnefPropertyTag.ClientSubmitTime;
						return true;
					case TnefAttributeTag.DateReceived:
						this.propertyTag = TnefPropertyTag.MessageDeliveryTime;
						return true;
					default:
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.AttachCreateDate:
							this.propertyTag = TnefPropertyTag.CreationTime;
							return true;
						case TnefAttributeTag.AttachModifyDate:
							this.propertyTag = TnefPropertyTag.LastModificationTime;
							return true;
						default:
							if (tnefAttributeTag == TnefAttributeTag.DateModified)
							{
								this.propertyTag = TnefPropertyTag.LastModificationTime;
								return true;
							}
							break;
						}
						break;
					}
				}
			}
			else if (tnefAttributeTag <= TnefAttributeTag.Delegate)
			{
				if (tnefAttributeTag <= TnefAttributeTag.Priority)
				{
					if (tnefAttributeTag == TnefAttributeTag.RequestResponse)
					{
						this.propertyTag = TnefPropertyTag.ResponseRequested;
						this.directRead = true;
						return true;
					}
					if (tnefAttributeTag == TnefAttributeTag.Priority)
					{
						this.propertyTag = TnefPropertyTag.Importance;
						return true;
					}
				}
				else
				{
					if (tnefAttributeTag == TnefAttributeTag.AidOwner)
					{
						this.propertyTag = TnefPropertyTag.OwnerApptId;
						this.directRead = true;
						return true;
					}
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.Owner:
						if (this.propertyIndex == 0)
						{
							this.propertyTag = (this.messageClassSPlusResponse ? TnefPropertyTag.RcvdRepresentingEntryId : TnefPropertyTag.SentRepresentingEntryId);
							return true;
						}
						this.propertyTag = (this.messageClassSPlusResponse ? TnefPropertyTag.RcvdRepresentingNameA : TnefPropertyTag.SentRepresentingNameA);
						return true;
					case TnefAttributeTag.SentFor:
						if (this.propertyIndex == 0)
						{
							this.propertyTag = TnefPropertyTag.SentRepresentingEntryId;
							return true;
						}
						this.propertyTag = TnefPropertyTag.SentRepresentingNameA;
						return true;
					case TnefAttributeTag.Delegate:
						this.propertyTag = TnefPropertyTag.Delegation;
						this.directRead = true;
						return true;
					}
				}
			}
			else if (tnefAttributeTag <= TnefAttributeTag.AttachMetaFile)
			{
				if (tnefAttributeTag == TnefAttributeTag.MessageStatus)
				{
					this.propertyTag = TnefPropertyTag.MessageFlags;
					return true;
				}
				switch (tnefAttributeTag)
				{
				case TnefAttributeTag.AttachData:
					if (this.propertyIndex == 0)
					{
						this.propertyTag = TnefPropertyTag.AttachDataBin;
						this.directRead = true;
						return true;
					}
					this.propertyTag = TnefPropertyTag.AttachTag;
					return true;
				case TnefAttributeTag.AttachMetaFile:
					this.propertyTag = TnefPropertyTag.AttachRendering;
					this.directRead = true;
					return true;
				}
			}
			else
			{
				switch (tnefAttributeTag)
				{
				case TnefAttributeTag.AttachTransportFilename:
					this.propertyTag = TnefPropertyTag.AttachTransportNameA;
					this.directRead = true;
					return true;
				case TnefAttributeTag.AttachRenderData:
					if (this.propertyIndex == 0)
					{
						this.propertyTag = TnefPropertyTag.RenderingPosition;
						return true;
					}
					if (this.propertyIndex == 1)
					{
						this.propertyTag = TnefPropertyTag.AttachMethod;
						return true;
					}
					this.propertyTag = TnefPropertyTag.AttachEncoding;
					return true;
				case TnefAttributeTag.MapiProperties:
				case TnefAttributeTag.RecipientTable:
				case TnefAttributeTag.Attachment:
					break;
				default:
					if (tnefAttributeTag == TnefAttributeTag.OriginalMessageClass)
					{
						this.propertyTag = TnefPropertyTag.OrigMessageClassA;
						this.directRead = this.directReadForMessageClass;
						return true;
					}
					if (tnefAttributeTag == TnefAttributeTag.MessageClass)
					{
						this.propertyTag = TnefPropertyTag.MessageClassA;
						this.directRead = this.directReadForMessageClass;
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00037D90 File Offset: 0x00035F90
		private bool PrepareLegacyPropertyValue()
		{
			this.propertyValueOffset = 0;
			this.propertyValueLength = this.propertyValueFixedLength;
			this.propertyValueStreamOffset = this.attributeValueStreamOffset;
			this.propertyValuePaddingLength = 0;
			TnefAttributeTag tnefAttributeTag = this.attributeTag;
			if (tnefAttributeTag <= TnefAttributeTag.DateModified)
			{
				if (tnefAttributeTag <= TnefAttributeTag.AttachTitle)
				{
					if (tnefAttributeTag <= TnefAttributeTag.Subject)
					{
						if (tnefAttributeTag != TnefAttributeTag.From)
						{
							if (tnefAttributeTag == TnefAttributeTag.Subject)
							{
								this.propertyValueLength = this.attributeValueLength;
								return true;
							}
						}
						else
						{
							if (this.propertyIndex == 0)
							{
								this.FabricateEntryIdFromTriple();
								return true;
							}
							this.FabricateNameFromTriple();
							return true;
						}
					}
					else
					{
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.MessageId:
							if (!this.FabricateTextizedBinary())
							{
								return false;
							}
							return true;
						case TnefAttributeTag.ParentId:
							if (!this.FabricateTextizedBinary())
							{
								return false;
							}
							return true;
						case TnefAttributeTag.ConversationId:
							if (!this.FabricateTextizedBinary())
							{
								return false;
							}
							return true;
						default:
							if (tnefAttributeTag == TnefAttributeTag.AttachTitle)
							{
								this.propertyValueLength = this.attributeValueLength;
								return true;
							}
							break;
						}
					}
				}
				else if (tnefAttributeTag <= TnefAttributeTag.DateEnd)
				{
					if (tnefAttributeTag == TnefAttributeTag.Body)
					{
						this.propertyValueLength = this.attributeValueLength;
						return true;
					}
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.DateStart:
						if (!this.FabricateSysTimeFromDTR())
						{
							return false;
						}
						return true;
					case TnefAttributeTag.DateEnd:
						if (!this.FabricateSysTimeFromDTR())
						{
							return false;
						}
						return true;
					}
				}
				else
				{
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.DateSent:
						if (!this.FabricateSysTimeFromDTR())
						{
							return false;
						}
						return true;
					case TnefAttributeTag.DateReceived:
						if (!this.FabricateSysTimeFromDTR())
						{
							return false;
						}
						return true;
					default:
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.AttachCreateDate:
							if (!this.FabricateSysTimeFromDTR())
							{
								return false;
							}
							return true;
						case TnefAttributeTag.AttachModifyDate:
							if (!this.FabricateSysTimeFromDTR())
							{
								return false;
							}
							return true;
						default:
							if (tnefAttributeTag == TnefAttributeTag.DateModified)
							{
								if (!this.FabricateSysTimeFromDTR())
								{
									return false;
								}
								return true;
							}
							break;
						}
						break;
					}
				}
			}
			else if (tnefAttributeTag <= TnefAttributeTag.Delegate)
			{
				if (tnefAttributeTag <= TnefAttributeTag.Priority)
				{
					if (tnefAttributeTag == TnefAttributeTag.RequestResponse)
					{
						this.propertyValueLength = 2;
						return true;
					}
					if (tnefAttributeTag == TnefAttributeTag.Priority)
					{
						this.FabricateImportanceFromPriority();
						return true;
					}
				}
				else
				{
					if (tnefAttributeTag == TnefAttributeTag.AidOwner)
					{
						this.propertyValueLength = 4;
						return true;
					}
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.Owner:
						if (this.propertyIndex == 0)
						{
							this.FabricateEntryIdFromTriple();
							return true;
						}
						this.FabricateNameFromTriple();
						return true;
					case TnefAttributeTag.SentFor:
						if (this.propertyIndex == 0)
						{
							this.FabricateEntryIdFromTriple();
							return true;
						}
						this.FabricateNameFromTriple();
						return true;
					case TnefAttributeTag.Delegate:
						this.propertyValueLength = this.attributeValueLength;
						return true;
					}
				}
			}
			else if (tnefAttributeTag <= TnefAttributeTag.AttachMetaFile)
			{
				if (tnefAttributeTag == TnefAttributeTag.MessageStatus)
				{
					this.FabricateMessageFlagsFromMessageStatus();
					return true;
				}
				switch (tnefAttributeTag)
				{
				case TnefAttributeTag.AttachData:
					if (this.propertyIndex == 0)
					{
						this.propertyValueLength = this.attributeValueLength;
						return true;
					}
					this.FabricateAttachTagOle1Storage();
					return true;
				case TnefAttributeTag.AttachMetaFile:
					this.propertyValueLength = this.attributeValueLength;
					return true;
				}
			}
			else
			{
				switch (tnefAttributeTag)
				{
				case TnefAttributeTag.AttachTransportFilename:
					this.propertyValueLength = this.attributeValueLength;
					return true;
				case TnefAttributeTag.AttachRenderData:
					if (this.propertyIndex == 0)
					{
						this.FabricateRenderingPositionFromRendData();
						return true;
					}
					if (this.propertyIndex == 1)
					{
						this.FabricateAttachMethodFromRendData();
						return true;
					}
					this.FabricateAttachEncodingFromRendData();
					return true;
				case TnefAttributeTag.MapiProperties:
				case TnefAttributeTag.RecipientTable:
				case TnefAttributeTag.Attachment:
					break;
				default:
					if (tnefAttributeTag == TnefAttributeTag.OriginalMessageClass || tnefAttributeTag == TnefAttributeTag.MessageClass)
					{
						if (!this.directRead)
						{
							this.propertyValueLength = this.fabricatedEnd - this.fabricatedOffset;
							return true;
						}
						this.propertyValueLength = this.AttributeRemainingCount();
						return true;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00038110 File Offset: 0x00036310
		private bool FabricateMorePropertyData(int count)
		{
			TnefAttributeTag tnefAttributeTag = this.attributeTag;
			if (tnefAttributeTag <= TnefAttributeTag.DateModified)
			{
				if (tnefAttributeTag <= TnefAttributeTag.AttachTitle)
				{
					if (tnefAttributeTag <= TnefAttributeTag.Subject)
					{
						if (tnefAttributeTag == TnefAttributeTag.From)
						{
							goto IL_18B;
						}
						if (tnefAttributeTag != TnefAttributeTag.Subject)
						{
						}
					}
					else
					{
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.MessageId:
						case TnefAttributeTag.ParentId:
						case TnefAttributeTag.ConversationId:
							if (!this.FabricateTextizedBinary())
							{
								return false;
							}
							goto IL_1A1;
						default:
							if (tnefAttributeTag != TnefAttributeTag.AttachTitle)
							{
							}
							break;
						}
					}
				}
				else if (tnefAttributeTag <= TnefAttributeTag.DateEnd)
				{
					if (tnefAttributeTag != TnefAttributeTag.Body)
					{
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.DateStart:
						case TnefAttributeTag.DateEnd:
							goto IL_16F;
						}
					}
				}
				else
				{
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.DateSent:
					case TnefAttributeTag.DateReceived:
						goto IL_16F;
					default:
						switch (tnefAttributeTag)
						{
						case TnefAttributeTag.AttachCreateDate:
						case TnefAttributeTag.AttachModifyDate:
							goto IL_16F;
						default:
							if (tnefAttributeTag == TnefAttributeTag.DateModified)
							{
								goto IL_16F;
							}
							break;
						}
						break;
					}
				}
			}
			else if (tnefAttributeTag <= TnefAttributeTag.Delegate)
			{
				if (tnefAttributeTag <= TnefAttributeTag.Priority)
				{
					if (tnefAttributeTag != TnefAttributeTag.RequestResponse)
					{
						if (tnefAttributeTag == TnefAttributeTag.Priority)
						{
							goto IL_16F;
						}
					}
				}
				else if (tnefAttributeTag != TnefAttributeTag.AidOwner)
				{
					switch (tnefAttributeTag)
					{
					case TnefAttributeTag.Owner:
					case TnefAttributeTag.SentFor:
						goto IL_18B;
					}
				}
			}
			else if (tnefAttributeTag <= TnefAttributeTag.AttachMetaFile)
			{
				if (tnefAttributeTag == TnefAttributeTag.MessageStatus)
				{
					goto IL_16F;
				}
				switch (tnefAttributeTag)
				{
				case TnefAttributeTag.AttachData:
					goto IL_16F;
				}
			}
			else
			{
				switch (tnefAttributeTag)
				{
				case TnefAttributeTag.AttachTransportFilename:
				case TnefAttributeTag.MapiProperties:
				case TnefAttributeTag.RecipientTable:
				case TnefAttributeTag.Attachment:
					break;
				case TnefAttributeTag.AttachRenderData:
					goto IL_16F;
				default:
					if (tnefAttributeTag == TnefAttributeTag.OriginalMessageClass || tnefAttributeTag == TnefAttributeTag.MessageClass)
					{
						goto IL_16F;
					}
					break;
				}
			}
			return false;
			IL_16F:
			this.fabricatedEnd = (this.fabricatedOffset = 0);
			return false;
			IL_18B:
			if (this.propertyIndex == 0)
			{
				this.FabricateEntryIdFromTriple();
			}
			else
			{
				this.FabricateNameFromTriple();
			}
			IL_1A1:
			if (this.fabricatedEnd - this.fabricatedOffset < count)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeLength, TnefStrings.ReaderComplianceInvalidComputedPropertyLength);
				this.error = true;
				return false;
			}
			return true;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000382E8 File Offset: 0x000364E8
		private bool FabricateSysTimeFromDTR()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				try
				{
					DateTime dateTime = new DateTime((int)this.PeekWord(0), (int)this.PeekWord(2), (int)this.PeekWord(4), (int)this.PeekWord(6), (int)this.PeekWord(8), (int)this.PeekWord(10), DateTimeKind.Utc);
					TnefBitConverter.GetBytes(this.fabricatedBuffer, 0, dateTime.ToFileTimeUtc());
				}
				catch (ArgumentException)
				{
					this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidDateOrTimeValue);
					DateTime dateTime = DateTime.UtcNow;
					TnefBitConverter.GetBytes(this.fabricatedBuffer, 0, dateTime.ToFileTimeUtc());
				}
				this.fabricatedEnd = 8;
				this.propertyValueLength = this.fabricatedEnd;
			}
			else
			{
				this.fabricatedEnd = 0;
			}
			this.fabricatedOffset = 0;
			return true;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x000383AC File Offset: 0x000365AC
		private void FabricateEntryIdFromTriple()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				this.fabricatedOffset = 0;
				this.fabricatedEnd = 0;
				this.propertyValueLength = 24 + this.tripleNameLength + 1 + this.tripleAddressTypeLength + 1 + this.tripleAddressLength + 1;
				TnefBitConverter.GetBytes(this.fabricatedBuffer, 0, 0);
				Buffer.BlockCopy(TnefCommon.MuidOOP, 0, this.fabricatedBuffer, 4, 16);
				TnefBitConverter.GetBytes(this.fabricatedBuffer, 20, 0);
				this.fabricatedEnd = 24;
			}
			else
			{
				this.fabricatedOffset = 0;
				this.fabricatedEnd = 0;
			}
			int num = this.propertyValueOffset + (this.fabricatedEnd - this.fabricatedOffset);
			int num2 = this.fabricatedBuffer.Length - this.fabricatedEnd;
			if (num < 24 + this.tripleNameLength && num2 > 0)
			{
				int num3 = Math.Min(num2, 24 + this.tripleNameLength - num);
				int num4 = num - 24;
				Buffer.BlockCopy(this.readBuffer, this.readOffset + this.tripleNameOffset + num4, this.fabricatedBuffer, this.fabricatedEnd, num3);
				num += num3;
				num2 -= num3;
				this.fabricatedEnd += num3;
			}
			if (num == 24 + this.tripleNameLength && num2 > 0)
			{
				this.fabricatedBuffer[this.fabricatedEnd++] = 0;
				num++;
				num2--;
			}
			int num5 = 24 + this.tripleNameLength + 1;
			if (num < num5 + this.tripleAddressTypeLength && num2 > 0)
			{
				int num6 = Math.Min(num2, num5 + this.tripleAddressTypeLength - num);
				int num7 = num - num5;
				for (int i = 0; i < num6; i++)
				{
					this.fabricatedBuffer[this.fabricatedEnd + i] = (byte)char.ToUpperInvariant((char)this.readBuffer[this.readOffset + this.tripleAddressTypeOffset + num7]);
					num7++;
				}
				num += num6;
				num2 -= num6;
				this.fabricatedEnd += num6;
			}
			if (num == num5 + this.tripleAddressTypeLength && num2 > 0)
			{
				this.fabricatedBuffer[this.fabricatedEnd++] = 0;
				num++;
				num2--;
			}
			int num8 = num5 + this.tripleAddressTypeLength + 1;
			if (num < num8 + this.tripleAddressLength && num2 > 0)
			{
				int num9 = Math.Min(num2, num8 + this.tripleAddressLength - num);
				int num10 = num - num8;
				Buffer.BlockCopy(this.readBuffer, this.readOffset + this.tripleAddressOffset + num10, this.fabricatedBuffer, this.fabricatedEnd, num9);
				num += num9;
				num2 -= num9;
				this.fabricatedEnd += num9;
			}
			if (num == num8 + this.tripleAddressLength && num2 > 0)
			{
				this.fabricatedBuffer[this.fabricatedEnd++] = 0;
				num++;
				num2--;
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00038668 File Offset: 0x00036868
		private void FabricateNameFromTriple()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				this.fabricatedOffset = 0;
				this.fabricatedEnd = 0;
				this.propertyValueLength = this.tripleNameLength + 1;
			}
			else
			{
				this.fabricatedOffset = 0;
				this.fabricatedEnd = 0;
			}
			int num = Math.Min(this.PropertyRemainingCount() - (this.fabricatedEnd - this.fabricatedOffset), this.fabricatedBuffer.Length - this.fabricatedEnd);
			Buffer.BlockCopy(this.readBuffer, this.readOffset + this.tripleNameOffset + this.propertyValueOffset, this.fabricatedBuffer, this.fabricatedEnd, num);
			this.fabricatedEnd += num;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00038710 File Offset: 0x00036910
		private void FabricateAttachTagOle1Storage()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				Buffer.BlockCopy(TnefCommon.OidOle1Storage, 0, this.fabricatedBuffer, 0, TnefCommon.OidOle1Storage.Length);
				this.fabricatedEnd = TnefCommon.OidOle1Storage.Length;
				this.propertyValueLength = this.fabricatedEnd;
			}
			else
			{
				this.fabricatedEnd = 0;
			}
			this.fabricatedOffset = 0;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0003876C File Offset: 0x0003696C
		private void FabricateRenderingPositionFromRendData()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				int value = this.PeekDword(2);
				TnefBitConverter.GetBytes(this.fabricatedBuffer, 0, value);
				this.fabricatedEnd = 4;
				this.propertyValueLength = this.fabricatedEnd;
			}
			else
			{
				this.fabricatedEnd = 0;
			}
			this.fabricatedOffset = 0;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000387BC File Offset: 0x000369BC
		private void FabricateAttachMethodFromRendData()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				short num = this.PeekWord(0);
				int value = (num == 1) ? 1 : 6;
				TnefBitConverter.GetBytes(this.fabricatedBuffer, 0, value);
				this.fabricatedEnd = 4;
				this.propertyValueLength = this.fabricatedEnd;
			}
			else
			{
				this.fabricatedEnd = 0;
			}
			this.fabricatedOffset = 0;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00038814 File Offset: 0x00036A14
		private void FabricateAttachEncodingFromRendData()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				Buffer.BlockCopy(TnefCommon.OidMacBinary, 0, this.fabricatedBuffer, 0, TnefCommon.OidMacBinary.Length);
				this.fabricatedEnd = TnefCommon.OidMacBinary.Length;
				this.propertyValueLength = this.fabricatedEnd;
			}
			else
			{
				this.fabricatedEnd = 0;
			}
			this.fabricatedOffset = 0;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00038870 File Offset: 0x00036A70
		private bool FabricateTextizedBinary()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				this.fabricatedOffset = 0;
				this.fabricatedEnd = 0;
				this.propertyValueLength = (this.attributeValueLength & -2) / 2;
			}
			else
			{
				this.fabricatedOffset = 0;
				this.fabricatedEnd = 0;
			}
			while (this.attributeValueOffset != this.propertyValueLength * 2 && this.fabricatedEnd < this.fabricatedBuffer.Length)
			{
				if (!this.CheckAndEnsureMoreAttributeDataLoaded(2))
				{
					this.error = true;
					return false;
				}
				int val = 2 * Math.Min(this.PropertyRemainingCount() - (this.fabricatedEnd - this.fabricatedOffset), this.fabricatedBuffer.Length - this.fabricatedEnd);
				int num = Math.Min(val, this.AvailableCount()) & -2;
				for (int i = 0; i < num; i += 2)
				{
					this.fabricatedBuffer[this.fabricatedEnd++] = (byte)(((int)TnefReader.NumFromHex[(int)this.PeekByte(i)] << 4) + (int)TnefReader.NumFromHex[(int)this.PeekByte(i + 1)]);
				}
				this.SkipBytes(num);
			}
			return true;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0003897C File Offset: 0x00036B7C
		private void FabricateImportanceFromPriority()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				int value;
				switch (this.PeekWord(0))
				{
				case 1:
					value = 2;
					goto IL_34;
				case 3:
					value = 0;
					goto IL_34;
				}
				value = 1;
				IL_34:
				TnefBitConverter.GetBytes(this.fabricatedBuffer, 0, value);
				this.fabricatedEnd = 4;
				this.propertyValueLength = this.fabricatedEnd;
			}
			else
			{
				this.fabricatedEnd = 0;
			}
			this.fabricatedOffset = 0;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000389F0 File Offset: 0x00036BF0
		private void FabricateMessageFlagsFromMessageStatus()
		{
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				short num = (short)this.PeekByte(0);
				int num2 = 0;
				num2 |= (((num & 32) != 0) ? 1 : 0);
				num2 |= (((num & 1) != 0) ? 0 : 2);
				num2 |= (((num & 4) != 0) ? 4 : 0);
				num2 |= (((num & 128) != 0) ? 16 : 0);
				num2 |= (((num & 2) != 0) ? 8 : 0);
				TnefBitConverter.GetBytes(this.fabricatedBuffer, 0, num2);
				this.fabricatedEnd = 4;
				this.propertyValueLength = this.fabricatedEnd;
			}
			else
			{
				this.fabricatedEnd = 0;
			}
			this.fabricatedOffset = 0;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00038A84 File Offset: 0x00036C84
		private void FabricateMessageClass(bool rememberMessageClass)
		{
			if (TnefCommon.BytesEqualToPattern(this.readBuffer, this.readOffset, TnefCommon.MessageClassLegacyPrefix))
			{
				this.SkipBytes(TnefCommon.MessageClassLegacyPrefix.Length);
			}
			for (int i = 0; i < TnefCommon.MessageClassMappingTable.Length; i++)
			{
				if (this.AttributeRemainingCount() == TnefCommon.MessageClassMappingTable[i].LegacyName.Length + 1 && TnefCommon.BytesEqualToPattern(this.readBuffer, this.readOffset, TnefCommon.MessageClassMappingTable[i].LegacyName) && this.readBuffer[this.readOffset + this.AttributeRemainingCount() - 1] == 0)
				{
					if (rememberMessageClass)
					{
						this.messageClassSPlus = TnefCommon.MessageClassMappingTable[i].Splus;
						this.messageClassSPlusResponse = TnefCommon.MessageClassMappingTable[i].SplusResponse;
					}
					this.fabricatedOffset = 0;
					this.fabricatedEnd = CTSGlobals.AsciiEncoding.GetBytes(TnefCommon.MessageClassMappingTable[i].MapiName, 0, TnefCommon.MessageClassMappingTable[i].MapiName.Length, this.fabricatedBuffer, 0);
					this.fabricatedBuffer[this.fabricatedEnd++] = 0;
					this.directReadForMessageClass = false;
					this.propertyValueLength = this.fabricatedEnd;
					return;
				}
			}
			this.directReadForMessageClass = true;
			this.propertyValueLength = this.AttributeRemainingCount();
			this.fabricatedOffset = 0;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00038BF0 File Offset: 0x00036DF0
		private string ReadAttributeUnicodeString(int bytes)
		{
			int num = 0;
			while (num < bytes - 1 && this.PeekWord(num) != 0)
			{
				num += 2;
			}
			if (this.decodeBuffer == null)
			{
				this.decodeBuffer = new char[512];
			}
			int num2;
			int num3;
			bool flag;
			this.unicodeDecoder.Convert(this.readBuffer, this.readOffset, num, this.decodeBuffer, 0, this.decodeBuffer.Length, true, out num2, out num3, out flag);
			if (flag)
			{
				this.SkipBytes(bytes);
				return new string(this.decodeBuffer, 0, num3);
			}
			StringBuilder stringBuilder = new StringBuilder(num / 2 + 1);
			stringBuilder.Append(this.decodeBuffer, 0, num3);
			this.SkipBytes(num2);
			num -= num2;
			bytes -= num2;
			do
			{
				this.unicodeDecoder.Convert(this.readBuffer, this.readOffset, num, this.decodeBuffer, 0, this.decodeBuffer.Length, true, out num2, out num3, out flag);
				stringBuilder.Append(this.decodeBuffer, 0, num3);
				this.SkipBytes(num2);
				num -= num2;
				bytes -= num2;
			}
			while (!flag);
			if (bytes != 0)
			{
				this.SkipBytes(bytes);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00038D00 File Offset: 0x00036F00
		private int AttributeRemainingCount()
		{
			return this.attributeValueLength - this.attributeValueOffset;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00038D0F File Offset: 0x00036F0F
		private bool MoreAttributeData(int bytes)
		{
			return this.AttributeRemainingCount() >= bytes;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00038D20 File Offset: 0x00036F20
		private bool CheckAndEnsureMoreAttributeDataLoaded(int bytes)
		{
			if (this.AttributeRemainingCount() < bytes)
			{
				this.SetComplianceStatus(TnefComplianceStatus.AttributeOverflow, TnefStrings.ReaderComplianceAttributeValueOverflow);
				this.error = true;
				return false;
			}
			if (!this.EnsureMoreDataLoaded(bytes))
			{
				this.SetComplianceStatus(TnefComplianceStatus.StreamTruncated, TnefStrings.ReaderComplianceTnefTruncated);
				this.error = true;
				return false;
			}
			return true;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00038D70 File Offset: 0x00036F70
		private void EatAttributeBytes(int count)
		{
			while (this.attributeValueOffset < this.attributeValueLength && count != 0)
			{
				if (!this.CheckAndEnsureMoreAttributeDataLoaded(1))
				{
					return;
				}
				int num = Math.Min(count, Math.Min(this.AttributeRemainingCount(), this.AvailableCount()));
				this.SkipBytes(num);
				count -= num;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00038DC0 File Offset: 0x00036FC0
		private byte PeekByte(int offsetFromCurrentPosition)
		{
			return this.readBuffer[this.readOffset + offsetFromCurrentPosition];
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00038DE0 File Offset: 0x00036FE0
		private short PeekWord(int offsetFromCurrentPosition)
		{
			return BitConverter.ToInt16(this.readBuffer, this.readOffset + offsetFromCurrentPosition);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00038E04 File Offset: 0x00037004
		private int PeekDword(int offsetFromCurrentPosition)
		{
			return BitConverter.ToInt32(this.readBuffer, this.readOffset + offsetFromCurrentPosition);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00038E28 File Offset: 0x00037028
		private short ReadByte()
		{
			byte b = this.readBuffer[this.readOffset];
			this.Checksum1(b);
			this.readOffset++;
			return (short)b;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00038E5C File Offset: 0x0003705C
		private short ReadWord()
		{
			short num = BitConverter.ToInt16(this.readBuffer, this.readOffset);
			this.Checksum2(num);
			this.readOffset += 2;
			this.attributeValueOffset += 2;
			return num;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00038EA0 File Offset: 0x000370A0
		private int ReadDword()
		{
			int result = BitConverter.ToInt32(this.readBuffer, this.readOffset);
			this.Checksum4();
			this.readOffset += 4;
			this.attributeValueOffset += 4;
			return result;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00038EE4 File Offset: 0x000370E4
		private long ReadQword()
		{
			long result = BitConverter.ToInt64(this.readBuffer, this.readOffset);
			this.Checksum(8);
			this.readOffset += 8;
			this.attributeValueOffset += 8;
			return result;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00038F28 File Offset: 0x00037128
		private float ReadFloat()
		{
			float result = BitConverter.ToSingle(this.readBuffer, this.readOffset);
			this.Checksum4();
			this.readOffset += 4;
			this.attributeValueOffset += 4;
			return result;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00038F6C File Offset: 0x0003716C
		private double ReadDouble()
		{
			double result = BitConverter.ToDouble(this.readBuffer, this.readOffset);
			this.Checksum(8);
			this.readOffset += 8;
			this.attributeValueOffset += 8;
			return result;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00038FB0 File Offset: 0x000371B0
		private Guid ReadGuid()
		{
			Guid result = new Guid(BitConverter.ToInt32(this.readBuffer, this.readOffset), BitConverter.ToInt16(this.readBuffer, this.readOffset + 4), BitConverter.ToInt16(this.readBuffer, this.readOffset + 6), this.readBuffer[this.readOffset + 8], this.readBuffer[this.readOffset + 9], this.readBuffer[this.readOffset + 10], this.readBuffer[this.readOffset + 11], this.readBuffer[this.readOffset + 12], this.readBuffer[this.readOffset + 13], this.readBuffer[this.readOffset + 14], this.readBuffer[this.readOffset + 15]);
			this.Checksum(16);
			this.readOffset += 16;
			this.attributeValueOffset += 16;
			return result;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000390A1 File Offset: 0x000372A1
		private void ReadBytes(byte[] buffer, int offset, int count)
		{
			Buffer.BlockCopy(this.readBuffer, this.readOffset, buffer, offset, count);
			this.Checksum(count);
			this.readOffset += count;
			this.attributeValueOffset += count;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x000390DA File Offset: 0x000372DA
		private void SkipBytes(int count)
		{
			this.Checksum(count);
			this.readOffset += count;
			this.attributeValueOffset += count;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x000390FF File Offset: 0x000372FF
		private int AvailableCount()
		{
			return this.readEnd - this.readOffset;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0003910E File Offset: 0x0003730E
		private bool EnsureMoreDataLoaded(int bytes)
		{
			return !this.NeedToLoadMoreData(bytes) || this.LoadMoreData(bytes);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00039122 File Offset: 0x00037322
		private bool NeedToLoadMoreData(int bytes)
		{
			return this.AvailableCount() < bytes;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00039130 File Offset: 0x00037330
		private bool LoadMoreData(int bytes)
		{
			if (this.endOfFile)
			{
				return false;
			}
			if (this.readBuffer.Length < bytes)
			{
				byte[] dst = new byte[Math.Max(this.readBuffer.Length * 2, bytes)];
				if (this.readEndReal - this.readOffset != 0)
				{
					Buffer.BlockCopy(this.readBuffer, this.readOffset, dst, 0, this.readEndReal - this.readOffset);
				}
				this.readBuffer = dst;
			}
			else if (this.readEndReal - this.readOffset != 0 && this.readOffset != 0)
			{
				Buffer.BlockCopy(this.readBuffer, this.readOffset, this.readBuffer, 0, this.readEndReal - this.readOffset);
			}
			int num = this.readOffset;
			this.readEndReal -= this.readOffset;
			this.readOffset = 0;
			this.streamOffset += num;
			int num2 = this.InputStream.Read(this.readBuffer, this.readEndReal, this.readBuffer.Length - this.readEndReal);
			this.readEndReal += num2;
			this.endOfFile = (num2 == 0);
			this.readEnd = this.readEndReal;
			if (this.streamOffset + this.readEnd > this.streamMaxLength)
			{
				this.readEnd = this.streamMaxLength - this.streamOffset;
				this.endOfFile = true;
			}
			return this.readEnd >= bytes;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00039291 File Offset: 0x00037491
		private void Checksum1(byte value)
		{
			if (!this.checksumDisabled)
			{
				this.checksum += (ushort)value;
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x000392AA File Offset: 0x000374AA
		private void Checksum2(short value)
		{
			if (!this.checksumDisabled)
			{
				this.checksum += (ushort)(value & 255);
				this.checksum += (ushort)(value >> 8 & 255);
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000392E4 File Offset: 0x000374E4
		private void Checksum4()
		{
			if (!this.checksumDisabled)
			{
				this.checksum += (ushort)this.readBuffer[this.readOffset];
				this.checksum += (ushort)this.readBuffer[this.readOffset + 1];
				this.checksum += (ushort)this.readBuffer[this.readOffset + 2];
				this.checksum += (ushort)this.readBuffer[this.readOffset + 3];
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0003936C File Offset: 0x0003756C
		private void Checksum(int count)
		{
			if (!this.checksumDisabled)
			{
				int num = this.readOffset;
				while (count-- > 0)
				{
					this.checksum += (ushort)this.readBuffer[num++];
				}
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000393AD File Offset: 0x000375AD
		private void SetComplianceStatus(TnefComplianceStatus status, string explanation)
		{
			this.complianceStatus |= status;
			if (this.complianceMode == TnefComplianceMode.Strict)
			{
				throw new TnefException(explanation);
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x000393D0 File Offset: 0x000375D0
		private bool CrackTriple()
		{
			short num = this.PeekWord(0);
			short num2 = this.PeekWord(4);
			short num3 = this.PeekWord(6);
			if (this.attributeValueLength < (int)(8 + num2 + num3) || num2 <= 0 || num3 <= 0)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidFrom);
				return false;
			}
			if (this.PeekByte((int)(8 + num2 + num3 - 1)) != 0)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidFrom);
				return false;
			}
			this.tripleNameOffset = 8;
			byte b;
			while ((b = this.PeekByte(this.tripleNameOffset)) == 32 || b == 9)
			{
				this.tripleNameOffset++;
			}
			switch (num)
			{
			case 3:
			case 4:
			case 9:
			{
				int num4 = this.tripleNameOffset;
				while (this.PeekByte(num4) != 0)
				{
					num4++;
				}
				this.tripleNameLength = num4 - this.tripleNameOffset;
				this.tripleAddressTypeOffset = (int)(8 + num2);
				while ((b = this.PeekByte(this.tripleAddressTypeOffset)) == 32 || b == 9)
				{
					this.tripleAddressTypeOffset++;
				}
				this.tripleAddressTypeLength = 0;
				this.tripleAddressOffset = 0;
				this.tripleAddressLength = 0;
				num4 = this.tripleAddressTypeOffset;
				while ((b = this.PeekByte(num4)) != 0 && b != 58)
				{
					num4++;
				}
				if (b == 58)
				{
					this.tripleAddressTypeLength = num4 - this.tripleAddressTypeOffset;
					while ((this.tripleAddressTypeLength > 0 && (b = this.PeekByte(this.tripleAddressTypeOffset + this.tripleAddressTypeLength - 1)) == 32) || b == 9)
					{
						this.tripleAddressTypeLength--;
					}
					this.tripleAddressOffset = num4 + 1;
					while ((b = this.PeekByte(this.tripleAddressOffset)) == 32 || b == 9)
					{
						this.tripleAddressOffset++;
					}
					num4 = this.tripleAddressOffset;
					while (this.PeekByte(num4) != 0)
					{
						num4++;
					}
					this.tripleAddressLength = num4 - this.tripleAddressOffset;
				}
				if (this.tripleAddressTypeLength == 0 || this.tripleAddressLength == 0)
				{
					this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidFrom);
					return false;
				}
				return true;
			}
			}
			this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidFrom);
			return false;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00039610 File Offset: 0x00037810
		private bool CrackSchedulePlusId()
		{
			short num = this.PeekWord(0);
			if (num <= 0 || (int)(2 + num + 2) > this.attributeValueLength || this.PeekByte((int)(2 + num - 1)) != 0)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidSchedulePlus);
				return false;
			}
			this.tripleNameOffset = 2;
			byte b;
			while ((b = this.PeekByte(this.tripleNameOffset)) == 32 || b == 9)
			{
				this.tripleNameOffset++;
			}
			int num2 = this.tripleNameOffset;
			while (this.PeekByte(num2) != 0)
			{
				num2++;
			}
			this.tripleNameLength = num2 - this.tripleNameOffset;
			short num3 = this.PeekWord((int)(2 + num));
			if (num3 <= 0 || (int)(2 + num + 2 + num3) > this.attributeValueLength || this.PeekByte((int)(2 + num + 2 + num3 - 1)) != 0)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidSchedulePlus);
				return false;
			}
			this.tripleAddressTypeOffset = (int)(2 + num + 2);
			while ((b = this.PeekByte(this.tripleAddressTypeOffset)) == 32 || b == 9)
			{
				this.tripleAddressTypeOffset++;
			}
			this.tripleAddressTypeLength = 0;
			this.tripleAddressOffset = 0;
			this.tripleAddressLength = 0;
			num2 = this.tripleAddressTypeOffset;
			while ((b = this.PeekByte(num2)) != 0 && b != 58)
			{
				num2++;
			}
			if (b == 58)
			{
				this.tripleAddressTypeLength = num2 - this.tripleAddressTypeOffset;
				while ((this.tripleAddressTypeLength > 0 && (b = this.PeekByte(this.tripleAddressTypeOffset + this.tripleAddressTypeLength - 1)) == 32) || b == 9)
				{
					this.tripleAddressTypeLength--;
				}
				this.tripleAddressOffset = num2 + 1;
				while ((b = this.PeekByte(this.tripleAddressOffset)) == 32 || b == 9)
				{
					this.tripleAddressOffset++;
				}
				num2 = this.tripleAddressOffset;
				while (this.PeekByte(num2) != 0)
				{
					num2++;
				}
				this.tripleAddressLength = num2 - this.tripleAddressOffset;
			}
			if (this.tripleNameLength == 0 || this.tripleAddressTypeLength == 0 || this.tripleAddressLength == 0)
			{
				this.SetComplianceStatus(TnefComplianceStatus.InvalidAttributeValue, TnefStrings.ReaderComplianceInvalidSchedulePlus);
				return false;
			}
			return true;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0003981D File Offset: 0x00037A1D
		internal void AssertGoodToUse(bool affectsChild)
		{
			if (this.InputStream == null)
			{
				throw new ObjectDisposedException("TnefReader");
			}
			if (affectsChild && this.Child != null)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationChildActive);
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00039848 File Offset: 0x00037A48
		internal void AssertInAttribute()
		{
			if (this.ReadStateValue < TnefReader.ReadState.EndAttribute)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationMustBeInAttribute);
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0003985E File Offset: 0x00037A5E
		internal void AssertInProperty()
		{
			if (this.ReadStateValue < TnefReader.ReadState.EndProperty)
			{
				throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationMustBeInProperty);
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00039874 File Offset: 0x00037A74
		internal void AssertInPropertyValue()
		{
			if (this.ReadStateValue >= TnefReader.ReadState.EndPropertyValue)
			{
				return;
			}
			if (this.ReadStateValue == TnefReader.ReadState.BeginProperty && !this.propertyTag.IsMultiValued && this.propertyTag.ValueTnefType != TnefPropertyType.Null)
			{
				this.ReadNextPropertyValue();
				return;
			}
			throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationMustBeInPropertyValue);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000398C4 File Offset: 0x00037AC4
		private void AssertAtTheBeginningOfPropertyValue()
		{
			this.AssertGoodToUse(true);
			if (this.ReadStateValue == TnefReader.ReadState.BeginPropertyValue)
			{
				return;
			}
			if (this.ReadStateValue == TnefReader.ReadState.BeginProperty && !this.propertyTag.IsMultiValued && this.propertyTag.ValueTnefType != TnefPropertyType.Null)
			{
				this.ReadNextPropertyValue();
				return;
			}
			throw new InvalidOperationException(TnefStrings.ReaderInvalidOperationMustBeInPropertyValue);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00039919 File Offset: 0x00037B19
		private static DateTime FromOADate(double value)
		{
			return DateTime.FromOADate(value);
		}

		// Token: 0x04000CEA RID: 3306
		private const int ReadBufferSize = 4096;

		// Token: 0x04000CEB RID: 3307
		private static readonly DateTime MinDateTime = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);

		// Token: 0x04000CEC RID: 3308
		private static readonly byte[] NumFromHex = new byte[]
		{
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16,
			16
		};

		// Token: 0x04000CED RID: 3309
		private TnefComplianceMode complianceMode;

		// Token: 0x04000CEE RID: 3310
		private bool checksumDisabled;

		// Token: 0x04000CEF RID: 3311
		private TnefComplianceStatus complianceStatus;

		// Token: 0x04000CF0 RID: 3312
		internal Stream InputStream;

		// Token: 0x04000CF1 RID: 3313
		private TnefReader parent;

		// Token: 0x04000CF2 RID: 3314
		internal object Child;

		// Token: 0x04000CF3 RID: 3315
		private int embeddingDepth;

		// Token: 0x04000CF4 RID: 3316
		private int streamMaxLength;

		// Token: 0x04000CF5 RID: 3317
		private bool endOfFile;

		// Token: 0x04000CF6 RID: 3318
		private int streamOffset;

		// Token: 0x04000CF7 RID: 3319
		private byte[] readBuffer;

		// Token: 0x04000CF8 RID: 3320
		private int readOffset;

		// Token: 0x04000CF9 RID: 3321
		private int readEnd;

		// Token: 0x04000CFA RID: 3322
		private int readEndReal;

		// Token: 0x04000CFB RID: 3323
		internal TnefReader.ReadState ReadStateValue;

		// Token: 0x04000CFC RID: 3324
		private bool error;

		// Token: 0x04000CFD RID: 3325
		private int tnefVersion;

		// Token: 0x04000CFE RID: 3326
		private short attachmentKey;

		// Token: 0x04000CFF RID: 3327
		private int messageCodepage;

		// Token: 0x04000D00 RID: 3328
		private TnefAttributeLevel attributeLevel;

		// Token: 0x04000D01 RID: 3329
		private TnefAttributeTag attributeTag;

		// Token: 0x04000D02 RID: 3330
		private bool legacyAttribute;

		// Token: 0x04000D03 RID: 3331
		private int attributeValueStreamOffset;

		// Token: 0x04000D04 RID: 3332
		private int attributeValueLength;

		// Token: 0x04000D05 RID: 3333
		private int attributeValueOffset;

		// Token: 0x04000D06 RID: 3334
		private ushort checksum;

		// Token: 0x04000D07 RID: 3335
		private ushort attributeStartChecksum;

		// Token: 0x04000D08 RID: 3336
		private int rowCount;

		// Token: 0x04000D09 RID: 3337
		private int rowIndex;

		// Token: 0x04000D0A RID: 3338
		private int propertyCount;

		// Token: 0x04000D0B RID: 3339
		private int propertyIndex;

		// Token: 0x04000D0C RID: 3340
		private TnefPropertyTag propertyTag;

		// Token: 0x04000D0D RID: 3341
		private TnefNameId propertyNameId;

		// Token: 0x04000D0E RID: 3342
		private int propertyValueCount;

		// Token: 0x04000D0F RID: 3343
		private int propertyValueIndex;

		// Token: 0x04000D10 RID: 3344
		private Guid propertyValueIId;

		// Token: 0x04000D11 RID: 3345
		private int propertyValueStreamOffset;

		// Token: 0x04000D12 RID: 3346
		private int propertyValueLength;

		// Token: 0x04000D13 RID: 3347
		private int propertyValueOffset;

		// Token: 0x04000D14 RID: 3348
		private int propertyValueFixedLength;

		// Token: 0x04000D15 RID: 3349
		private int propertyValuePaddingLength;

		// Token: 0x04000D16 RID: 3350
		private int propertyPaddingLength;

		// Token: 0x04000D17 RID: 3351
		private bool decoderFlushed;

		// Token: 0x04000D18 RID: 3352
		private Decoder decoder;

		// Token: 0x04000D19 RID: 3353
		private char[] decodeBuffer;

		// Token: 0x04000D1A RID: 3354
		private Decoder unicodeDecoder;

		// Token: 0x04000D1B RID: 3355
		private Decoder string8Decoder;

		// Token: 0x04000D1C RID: 3356
		private TnefPropertyReader propertyReader;

		// Token: 0x04000D1D RID: 3357
		private bool directRead;

		// Token: 0x04000D1E RID: 3358
		private byte[] fabricatedBuffer;

		// Token: 0x04000D1F RID: 3359
		private int fabricatedOffset;

		// Token: 0x04000D20 RID: 3360
		private int fabricatedEnd;

		// Token: 0x04000D21 RID: 3361
		private bool messageClassSPlus;

		// Token: 0x04000D22 RID: 3362
		private bool messageClassSPlusResponse;

		// Token: 0x04000D23 RID: 3363
		private bool currentAttachmentIsOle;

		// Token: 0x04000D24 RID: 3364
		private bool directReadForMessageClass;

		// Token: 0x04000D25 RID: 3365
		private int tripleNameOffset;

		// Token: 0x04000D26 RID: 3366
		private int tripleNameLength;

		// Token: 0x04000D27 RID: 3367
		private int tripleAddressTypeOffset;

		// Token: 0x04000D28 RID: 3368
		private int tripleAddressTypeLength;

		// Token: 0x04000D29 RID: 3369
		private int tripleAddressOffset;

		// Token: 0x04000D2A RID: 3370
		private int tripleAddressLength;

		// Token: 0x020000F2 RID: 242
		internal enum ReadState
		{
			// Token: 0x04000D2C RID: 3372
			EndOfFile,
			// Token: 0x04000D2D RID: 3373
			Begin,
			// Token: 0x04000D2E RID: 3374
			EndAttribute,
			// Token: 0x04000D2F RID: 3375
			BeginAttribute,
			// Token: 0x04000D30 RID: 3376
			ReadAttributeValue,
			// Token: 0x04000D31 RID: 3377
			EndRow,
			// Token: 0x04000D32 RID: 3378
			BeginRow,
			// Token: 0x04000D33 RID: 3379
			EndProperty,
			// Token: 0x04000D34 RID: 3380
			BeginProperty,
			// Token: 0x04000D35 RID: 3381
			EndPropertyValue,
			// Token: 0x04000D36 RID: 3382
			BeginPropertyValue,
			// Token: 0x04000D37 RID: 3383
			ReadPropertyValue
		}
	}
}
