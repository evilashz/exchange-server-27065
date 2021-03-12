using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000292 RID: 658
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OutlookBlobReader
	{
		// Token: 0x06001B5C RID: 7004 RVA: 0x0007E4E0 File Offset: 0x0007C6E0
		internal OutlookBlobReader(BinaryReader reader, Encoding encoding)
		{
			this.reader = reader;
			this.encoding = encoding;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0007E4F6 File Offset: 0x0007C6F6
		internal short ReadShort()
		{
			return this.reader.ReadInt16();
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0007E503 File Offset: 0x0007C703
		internal int ReadInt()
		{
			return this.reader.ReadInt32();
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0007E510 File Offset: 0x0007C710
		internal uint ReadUint()
		{
			return this.reader.ReadUInt32();
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0007E51D File Offset: 0x0007C71D
		internal bool ReadBool()
		{
			return this.reader.ReadUInt32() != 0U;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0007E530 File Offset: 0x0007C730
		internal string ReadString()
		{
			byte b = this.reader.ReadByte();
			short count;
			if (b < 255)
			{
				count = (short)b;
			}
			else
			{
				count = this.ReadShort();
			}
			return new string(this.reader.ReadChars((int)count));
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0007E570 File Offset: 0x0007C770
		internal void ReadSkipBlock()
		{
			for (int i = this.ReadInt(); i > 0; i = this.ReadInt())
			{
				this.reader.BaseStream.Seek((long)i, SeekOrigin.Current);
			}
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0007E5A8 File Offset: 0x0007C7A8
		internal StoreId[] ReadEntryIdList()
		{
			this.ReadInt();
			int num = this.ReadInt();
			this.ReadInt();
			int[] array = new int[num];
			StoreObjectId[] array2 = new StoreObjectId[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = this.ReadInt();
				this.ReadInt();
			}
			for (int j = 0; j < num; j++)
			{
				array2[j] = StoreObjectId.FromProviderSpecificId(this.reader.ReadBytes(array[j]));
			}
			return array2;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0007E620 File Offset: 0x0007C820
		internal Restriction ReadRestriction()
		{
			OutlookBlobWriter.RestrictionType restrictionType = (OutlookBlobWriter.RestrictionType)this.ReadInt();
			switch (restrictionType)
			{
			case OutlookBlobWriter.RestrictionType.And:
			case OutlookBlobWriter.RestrictionType.Or:
			{
				int num = this.ReadInt();
				Restriction[] array = new Restriction[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = this.ReadRestriction();
				}
				if (restrictionType == OutlookBlobWriter.RestrictionType.And)
				{
					return Restriction.And(array);
				}
				return Restriction.Or(array);
			}
			case OutlookBlobWriter.RestrictionType.Not:
				return Restriction.Not(this.ReadRestriction());
			case OutlookBlobWriter.RestrictionType.Content:
			{
				ContentFlags flags = (ContentFlags)this.ReadInt();
				PropTag tag = (PropTag)this.ReadInt();
				return Restriction.Content(tag, this.ReadPropValue().Value, flags);
			}
			case OutlookBlobWriter.RestrictionType.Property:
			{
				Restriction.RelOp relOp = (Restriction.RelOp)this.ReadInt();
				PropTag tag2 = (PropTag)this.ReadInt();
				return new Restriction.PropertyRestriction(relOp, tag2, this.ReadPropValue().Value);
			}
			case OutlookBlobWriter.RestrictionType.PropertyComparison:
			{
				Restriction.RelOp relOp2 = (Restriction.RelOp)this.ReadInt();
				PropTag tagLeft = (PropTag)this.ReadInt();
				PropTag tagRight = (PropTag)this.ReadInt();
				return new Restriction.ComparePropertyRestriction(relOp2, tagLeft, tagRight);
			}
			case OutlookBlobWriter.RestrictionType.Bitmask:
				if (this.ReadInt() == 0)
				{
					return Restriction.BitMaskZero((PropTag)this.ReadInt(), this.ReadInt());
				}
				return Restriction.BitMaskNonZero((PropTag)this.ReadInt(), this.ReadInt());
			case OutlookBlobWriter.RestrictionType.Size:
			{
				Restriction.RelOp relop = (Restriction.RelOp)this.ReadInt();
				PropTag tag3 = (PropTag)this.ReadInt();
				int size = this.ReadInt();
				return new Restriction.SizeRestriction(relop, tag3, size);
			}
			case OutlookBlobWriter.RestrictionType.Exist:
			{
				PropTag tag4 = (PropTag)this.ReadInt();
				return Restriction.Exist(tag4);
			}
			case OutlookBlobWriter.RestrictionType.Subfilter:
			{
				PropTag propTag = (PropTag)this.ReadInt();
				if (propTag == PropTag.MessageAttachments || propTag == PropTag.MessageRecipients)
				{
					return Restriction.Sub(propTag, this.ReadRestriction());
				}
				throw new CorruptDataException(ServerStrings.ExUnknownRestrictionType);
			}
			default:
				throw new CorruptDataException(ServerStrings.ExUnknownRestrictionType);
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0007E7C4 File Offset: 0x0007C9C4
		internal PropValue ReadPropValue()
		{
			PropTag propTag = (PropTag)this.ReadInt();
			PropType propType = propTag.ValueType();
			PropType propType2 = propType;
			object value;
			if (propType2 <= PropType.SysTime)
			{
				if (propType2 <= PropType.Boolean)
				{
					if (propType2 == PropType.Int)
					{
						value = this.ReadInt();
						goto IL_236;
					}
					if (propType2 == PropType.Boolean)
					{
						value = this.ReadPropValueBool();
						goto IL_236;
					}
				}
				else
				{
					switch (propType2)
					{
					case PropType.AnsiString:
						value = this.ReadPropValueAnsiString();
						goto IL_236;
					case PropType.String:
						value = this.ReadPropValueString();
						goto IL_236;
					default:
						if (propType2 == PropType.SysTime)
						{
							value = this.ReadPropValueDateTime();
							goto IL_236;
						}
						break;
					}
				}
			}
			else if (propType2 <= PropType.StringArray)
			{
				if (propType2 == PropType.Binary)
				{
					short count = this.ReadShort();
					value = this.reader.ReadBytes((int)count);
					goto IL_236;
				}
				switch (propType2)
				{
				case PropType.AnsiStringArray:
				{
					int num = this.ReadInt();
					if (num < 0)
					{
						throw new CorruptDataException(ServerStrings.ExSearchFolderCorruptOutlookBlob("PropType " + propType));
					}
					string[] array = new string[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = this.ReadPropValueAnsiString();
					}
					value = array;
					goto IL_236;
				}
				case PropType.StringArray:
				{
					int num2 = this.ReadInt();
					if (num2 < 0)
					{
						throw new CorruptDataException(ServerStrings.ExSearchFolderCorruptOutlookBlob("PropType " + propType));
					}
					string[] array2 = new string[num2];
					for (int j = 0; j < num2; j++)
					{
						array2[j] = this.ReadPropValueString();
					}
					value = array2;
					goto IL_236;
				}
				}
			}
			else
			{
				if (propType2 == PropType.SysTimeArray)
				{
					int num3 = this.ReadInt();
					DateTime[] array3 = new DateTime[num3];
					for (int k = 0; k < num3; k++)
					{
						array3[k] = this.ReadPropValueDateTime();
					}
					value = array3;
					goto IL_236;
				}
				if (propType2 == PropType.BinaryArray)
				{
					int num4 = this.ReadInt();
					byte[][] array4 = new byte[num4][];
					for (int l = 0; l < num4; l++)
					{
						int count2 = this.ReadInt();
						array4[l] = this.reader.ReadBytes(count2);
					}
					value = array4;
					goto IL_236;
				}
			}
			throw new CorruptDataException(ServerStrings.ExSearchFolderCorruptOutlookBlob("PropType " + propType));
			IL_236:
			return new PropValue(propTag, value);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0007EA0E File Offset: 0x0007CC0E
		private bool ReadPropValueBool()
		{
			return this.reader.ReadUInt16() != 0;
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0007EA20 File Offset: 0x0007CC20
		private string ReadPropValueString()
		{
			short num = this.ReadShort();
			num /= 2;
			if (num < 1)
			{
				throw new CorruptDataException(ServerStrings.ExSearchFolderCorruptOutlookBlob("PropType " + PropType.String));
			}
			num -= 1;
			string result = new string(this.reader.ReadChars((int)num));
			char c = this.reader.ReadChar();
			if (c != '\0')
			{
				throw new CorruptDataException(ServerStrings.ExSearchFolderCorruptOutlookBlob("PropType " + PropType.String));
			}
			return result;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0007EA9C File Offset: 0x0007CC9C
		private string ReadPropValueAnsiString()
		{
			short num = this.ReadShort();
			if (num < 1)
			{
				throw new CorruptDataException(ServerStrings.ExSearchFolderCorruptOutlookBlob("PropType " + PropType.AnsiString));
			}
			num -= 1;
			byte[] bytes = this.reader.ReadBytes((int)num);
			byte b = this.reader.ReadByte();
			if (b != 0)
			{
				throw new CorruptDataException(ServerStrings.ExSearchFolderCorruptOutlookBlob("PropType " + PropType.AnsiString));
			}
			return this.encoding.GetString(bytes, 0, (int)num);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0007EB20 File Offset: 0x0007CD20
		private DateTime ReadPropValueDateTime()
		{
			uint num = this.reader.ReadUInt32();
			uint num2 = this.reader.ReadUInt32();
			long num3 = (long)((ulong)num2);
			num3 <<= 32;
			num3 += (long)((ulong)num);
			return (DateTime)ExDateTime.FromFileTimeUtc(num3);
		}

		// Token: 0x04001301 RID: 4865
		private readonly BinaryReader reader;

		// Token: 0x04001302 RID: 4866
		private readonly Encoding encoding;
	}
}
