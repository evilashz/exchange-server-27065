using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000040 RID: 64
	internal abstract class Writer : BaseObject
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000188 RID: 392
		// (set) Token: 0x06000189 RID: 393
		public abstract long Position { get; set; }

		// Token: 0x0600018A RID: 394 RVA: 0x00005E9C File Offset: 0x0000409C
		public void WriteBool(bool value)
		{
			this.WriteBool(value, byte.MaxValue);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005EAA File Offset: 0x000040AA
		public void WriteBool(bool value, byte trueValue)
		{
			this.WriteByte(value ? trueValue : 0);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005EB9 File Offset: 0x000040B9
		public void WriteBytes(byte[] value)
		{
			this.WriteBytes(value, 0, value.Length);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005EC8 File Offset: 0x000040C8
		public void WriteBytes(byte[] value, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (value.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this.InternalWriteBytes(value, offset, count);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005F21 File Offset: 0x00004121
		public void WriteBytesSegment(ArraySegment<byte> value)
		{
			this.WriteBytes(value.Array, value.Offset, value.Count);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005F3E File Offset: 0x0000413E
		public void WriteSizedBytesSegment(ArraySegment<byte> value, FieldLength lengthSize)
		{
			this.WriteCountOrSize(value.Count, lengthSize);
			if (value.Count > 0)
			{
				this.WriteBytes(value.Array, value.Offset, value.Count);
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005F73 File Offset: 0x00004173
		public void WriteSizedBytes(byte[] value)
		{
			this.WriteSizedBytes(value, FieldLength.WordSize);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005F7D File Offset: 0x0000417D
		public void WriteSizedBytes(byte[] value, FieldLength lengthSize)
		{
			Util.ThrowOnNullArgument(value, "value");
			this.WriteSizeAndBytesArray(value, value.Length, lengthSize);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005F95 File Offset: 0x00004195
		public void WriteSizeAndBytesArray(byte[] value, int byteCount, FieldLength lengthSize)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (byteCount > value.Length)
			{
				throw new ArgumentOutOfRangeException("Number of bytes to write exceeds the size of the buffer.");
			}
			this.WriteCountOrSize(byteCount, lengthSize);
			if (byteCount > 0)
			{
				this.WriteBytes(value, 0, byteCount);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005FCC File Offset: 0x000041CC
		public void WriteCountAndByteArrayList(byte[][] values, FieldLength lengthSize)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.WriteCountOrSize(values.GetLength(0), lengthSize);
			foreach (byte[] value in values)
			{
				this.WriteSizedBytes(value, lengthSize);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006011 File Offset: 0x00004211
		public void WriteUnicodeString(string value, StringFlags flags)
		{
			this.WriteString(value, Encoding.Unicode, flags);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006020 File Offset: 0x00004220
		public void WriteAsciiString(string value, StringFlags flags)
		{
			this.WriteString(value, CTSGlobals.AsciiEncoding, flags);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000602F File Offset: 0x0000422F
		public void WriteString8(string value, Encoding encoding, StringFlags flags)
		{
			if ((flags & StringFlags.IncludeNull) != StringFlags.IncludeNull)
			{
				throw new NotSupportedException("Writing non-terminated String8 values is not supported.");
			}
			String8Encodings.ThrowIfInvalidString8Encoding(encoding);
			this.WriteString(value, encoding, flags);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006051 File Offset: 0x00004251
		public void UpdateSize(long startPosition, long endPosition)
		{
			this.Position = startPosition;
			this.WriteUInt16((ushort)(endPosition - startPosition - 2L));
			this.Position = endPosition;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006070 File Offset: 0x00004270
		public void WriteSecurityIdentifier(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			this.WriteBytes(array);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000060AC File Offset: 0x000042AC
		public void WritePropertyTag(PropertyTag value)
		{
			this.WriteUInt32(value);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000060BA File Offset: 0x000042BA
		public void WritePropertyValue(PropertyValue value, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			this.WritePropertyTag(value.PropertyTag);
			this.WritePropertyValueWithoutTag(value, string8Encoding, wireFormatStyle);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000060D4 File Offset: 0x000042D4
		public void WriteNullablePropertyValue(PropertyValue? propertyValue, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			bool flag = propertyValue != null;
			this.WriteBool(flag);
			if (flag)
			{
				this.WritePropertyValue(propertyValue.Value, string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006104 File Offset: 0x00004304
		public void WritePropertyValueWithoutTag(PropertyValue value, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			if (string8Encoding == null)
			{
				throw new ArgumentNullException("string8Encoding");
			}
			if (value.IsNullValue && value.PropertyTag.PropertyType != PropertyType.Null && wireFormatStyle != WireFormatStyle.Nspi)
			{
				throw new NotSupportedException(string.Format("Property value cannot be null: {0}.", value.PropertyTag));
			}
			if (value.IsError)
			{
				this.WriteUInt32((uint)value.ErrorCodeValue);
				return;
			}
			this.WritePropertyValueInternal(value.PropertyTag.PropertyType, value.Value, string8Encoding, wireFormatStyle);
		}

		// Token: 0x0600019D RID: 413
		public abstract void WriteByte(byte value);

		// Token: 0x0600019E RID: 414
		public abstract void WriteDouble(double value);

		// Token: 0x0600019F RID: 415
		public abstract void WriteSingle(float value);

		// Token: 0x060001A0 RID: 416
		public abstract void WriteGuid(Guid value);

		// Token: 0x060001A1 RID: 417
		public abstract void WriteInt32(int value);

		// Token: 0x060001A2 RID: 418
		public abstract void WriteInt64(long value);

		// Token: 0x060001A3 RID: 419
		public abstract void WriteInt16(short value);

		// Token: 0x060001A4 RID: 420
		public abstract void WriteUInt32(uint value);

		// Token: 0x060001A5 RID: 421
		public abstract void WriteUInt64(ulong value);

		// Token: 0x060001A6 RID: 422
		public abstract void WriteUInt16(ushort value);

		// Token: 0x060001A7 RID: 423
		public abstract void SkipArraySegment(ArraySegment<byte> buffer);

		// Token: 0x060001A8 RID: 424
		protected abstract void InternalWriteBytes(byte[] value, int offset, int count);

		// Token: 0x060001A9 RID: 425
		protected abstract void InternalWriteString(string value, int length, Encoding encoding);

		// Token: 0x060001AA RID: 426 RVA: 0x0000618F File Offset: 0x0000438F
		internal void WriteCountOrSize(int count, FieldLength lengthSize)
		{
			if (count < 0)
			{
				throw new ArgumentException("Cannot serialize a negative count", "count");
			}
			this.WriteCount((uint)count, lengthSize);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000061B0 File Offset: 0x000043B0
		internal void WriteCount(uint count, FieldLength lengthSize)
		{
			switch (lengthSize)
			{
			case FieldLength.WordSize:
				if (count > 65535U)
				{
					string message = string.Format("Value ({0}) is too large to be serialized as a ushort", count);
					throw new ArgumentException(message, "count");
				}
				this.WriteUInt16((ushort)count);
				return;
			case FieldLength.DWordSize:
				this.WriteUInt32(count);
				return;
			default:
				throw new ArgumentException("Unrecognized FieldLength: " + lengthSize, "lengthSize");
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000621F File Offset: 0x0000441F
		internal void WriteCountOrSize(int count, WireFormatStyle wireFormatStyle)
		{
			if (count < 0)
			{
				throw new ArgumentException("Cannot serialize a negative count", "count");
			}
			this.WriteCount((uint)count, wireFormatStyle);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006240 File Offset: 0x00004440
		internal void WriteCount(uint count, WireFormatStyle wireFormatStyle)
		{
			switch (wireFormatStyle)
			{
			case WireFormatStyle.Rop:
				if (count > 65535U)
				{
					string message = string.Format("Value ({0}) is too large to be serialized as a ushort", count);
					throw new ArgumentException(message, "count");
				}
				this.WriteUInt16((ushort)count);
				return;
			case WireFormatStyle.Nspi:
				this.WriteUInt32(count);
				return;
			default:
				throw new ArgumentException("Unrecognized wire format style: " + wireFormatStyle, "wireFormatStyle");
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000062B0 File Offset: 0x000044B0
		private bool TryWriteHasValue(object value, WireFormatStyle wireFormatStyle)
		{
			if (wireFormatStyle == WireFormatStyle.Nspi)
			{
				bool flag = value != null;
				this.WriteBool(flag, byte.MaxValue);
				return flag;
			}
			return true;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000062D8 File Offset: 0x000044D8
		private void WritePropertyValueInternal(PropertyType propertyType, object value, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			if (wireFormatStyle != WireFormatStyle.Nspi && propertyType != PropertyType.Null && value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (propertyType <= PropertyType.Binary)
			{
				if (propertyType <= PropertyType.SysTime)
				{
					switch (propertyType)
					{
					case PropertyType.Unspecified:
					case (PropertyType)8:
					case (PropertyType)9:
					case (PropertyType)12:
					case (PropertyType)14:
					case (PropertyType)15:
					case (PropertyType)16:
					case (PropertyType)17:
					case (PropertyType)18:
					case (PropertyType)19:
						break;
					case PropertyType.Null:
						return;
					case PropertyType.Int16:
						this.WriteInt16((short)value);
						return;
					case PropertyType.Int32:
						this.WriteInt32((int)value);
						return;
					case PropertyType.Float:
						this.WriteSingle((float)value);
						return;
					case PropertyType.Double:
					case PropertyType.AppTime:
						this.WriteDouble((double)value);
						return;
					case PropertyType.Currency:
					case PropertyType.Int64:
						this.WriteInt64((long)value);
						return;
					case PropertyType.Error:
						this.WriteUInt32((uint)value);
						return;
					case PropertyType.Bool:
						this.WriteBool((bool)value, 1);
						return;
					case PropertyType.Object:
						if (wireFormatStyle != WireFormatStyle.Nspi)
						{
							throw new NotImplementedException(string.Format("Property type not implemented: {0}.", propertyType));
						}
						return;
					default:
						switch (propertyType)
						{
						case PropertyType.String8:
							if (this.TryWriteHasValue(value, wireFormatStyle))
							{
								this.WriteString8((string)value, string8Encoding, StringFlags.IncludeNull);
								return;
							}
							return;
						case PropertyType.Unicode:
							if (this.TryWriteHasValue(value, wireFormatStyle))
							{
								this.WriteUnicodeString((string)value, StringFlags.IncludeNull);
								return;
							}
							return;
						default:
							if (propertyType == PropertyType.SysTime)
							{
								this.WriteInt64(PropertyValue.ExDateTimeToFileTimeUtc((ExDateTime)value));
								return;
							}
							break;
						}
						break;
					}
				}
				else if (propertyType != PropertyType.Guid)
				{
					switch (propertyType)
					{
					case PropertyType.ServerId:
						break;
					case (PropertyType)252:
						goto IL_3A1;
					case PropertyType.Restriction:
						if (this.TryWriteHasValue(value, wireFormatStyle))
						{
							((Restriction)value).Serialize(this, string8Encoding, wireFormatStyle);
							return;
						}
						return;
					case PropertyType.Actions:
						if (wireFormatStyle == WireFormatStyle.Nspi)
						{
							throw new NotSupportedException(string.Format("Property type not supported: {0}.", propertyType));
						}
						this.WriteSizedRuleActions((RuleAction[])value, string8Encoding);
						return;
					default:
						if (propertyType != PropertyType.Binary)
						{
							goto IL_3A1;
						}
						break;
					}
					if (this.TryWriteHasValue(value, wireFormatStyle))
					{
						byte[] array = (byte[])value;
						this.WriteCountOrSize(array.Length, wireFormatStyle);
						this.WriteBytes(array);
						return;
					}
					return;
				}
				else
				{
					if (this.TryWriteHasValue(value, wireFormatStyle))
					{
						this.WriteGuid((Guid)value);
						return;
					}
					return;
				}
			}
			else if (propertyType <= PropertyType.MultiValueUnicode)
			{
				switch (propertyType)
				{
				case PropertyType.MultiValueInt16:
					if (this.TryWriteHasValue(value, wireFormatStyle))
					{
						this.SerializeMultiValue<short>(PropertyType.Int16, value, string8Encoding, wireFormatStyle);
						return;
					}
					return;
				case PropertyType.MultiValueInt32:
					if (this.TryWriteHasValue(value, wireFormatStyle))
					{
						this.SerializeMultiValue<int>(PropertyType.Int32, value, string8Encoding, wireFormatStyle);
						return;
					}
					return;
				case PropertyType.MultiValueFloat:
					if (this.TryWriteHasValue(value, wireFormatStyle))
					{
						this.SerializeMultiValue<float>(PropertyType.Float, value, string8Encoding, wireFormatStyle);
						return;
					}
					return;
				case PropertyType.MultiValueDouble:
				case PropertyType.MultiValueAppTime:
					if (this.TryWriteHasValue(value, wireFormatStyle))
					{
						this.SerializeMultiValue<double>(PropertyType.Double, value, string8Encoding, wireFormatStyle);
						return;
					}
					return;
				case PropertyType.MultiValueCurrency:
					break;
				default:
					if (propertyType != PropertyType.MultiValueInt64)
					{
						switch (propertyType)
						{
						case PropertyType.MultiValueString8:
							if (this.TryWriteHasValue(value, wireFormatStyle))
							{
								this.SerializeMultiValue<string>(PropertyType.String8, value, string8Encoding, wireFormatStyle);
								return;
							}
							return;
						case PropertyType.MultiValueUnicode:
							if (this.TryWriteHasValue(value, wireFormatStyle))
							{
								this.SerializeMultiValue<string>(PropertyType.Unicode, value, string8Encoding, wireFormatStyle);
								return;
							}
							return;
						default:
							goto IL_3A1;
						}
					}
					break;
				}
				if (this.TryWriteHasValue(value, wireFormatStyle))
				{
					this.SerializeMultiValue<long>(PropertyType.Int64, value, string8Encoding, wireFormatStyle);
					return;
				}
				return;
			}
			else if (propertyType != PropertyType.MultiValueSysTime)
			{
				if (propertyType != PropertyType.MultiValueGuid)
				{
					if (propertyType == PropertyType.MultiValueBinary)
					{
						if (this.TryWriteHasValue(value, wireFormatStyle))
						{
							this.SerializeMultiValue<byte[]>(PropertyType.Binary, value, string8Encoding, wireFormatStyle);
							return;
						}
						return;
					}
				}
				else
				{
					if (this.TryWriteHasValue(value, wireFormatStyle))
					{
						this.SerializeMultiValue<Guid>(PropertyType.Guid, value, string8Encoding, wireFormatStyle);
						return;
					}
					return;
				}
			}
			else
			{
				if (this.TryWriteHasValue(value, wireFormatStyle))
				{
					this.SerializeMultiValue<ExDateTime>(PropertyType.SysTime, value, string8Encoding, wireFormatStyle);
					return;
				}
				return;
			}
			IL_3A1:
			throw new NotSupportedException(string.Format("Property type not supported: {0}.", propertyType));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000669C File Offset: 0x0000489C
		private void WriteString(string value, Encoding encoding, StringFlags flags)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			int byteCount = encoding.GetByteCount(value);
			byte[] array = null;
			if ((flags & StringFlags.IncludeNull) == StringFlags.IncludeNull)
			{
				array = ((encoding == Encoding.Unicode) ? Writer.UnicodeNullTerminator : Writer.AsciiNullTerminator);
			}
			if ((flags & (StringFlags.Sized | StringFlags.Sized16 | StringFlags.Sized32)) != StringFlags.None)
			{
				int num = byteCount + ((array != null) ? array.Length : 0);
				if ((flags & StringFlags.Sized16) == StringFlags.Sized16)
				{
					if (num > 65535)
					{
						throw new BufferParseException("Sized Unicode string is larger than the maximum size allowed.");
					}
					this.WriteUInt16((ushort)num);
				}
				else if ((flags & StringFlags.Sized) == StringFlags.Sized)
				{
					if (num > 255)
					{
						string message = string.Format("Sized string serialization size {0} larger than {1} limit.", num, byte.MaxValue);
						throw new BufferParseException(message);
					}
					this.WriteByte((byte)num);
				}
				else
				{
					this.WriteUInt32((uint)num);
				}
			}
			this.InternalWriteString(value, byteCount, encoding);
			if (array != null)
			{
				this.WriteBytes(array);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006774 File Offset: 0x00004974
		private void SerializeMultiValue<T>(PropertyType elementPropertyType, object value, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			T[] array = value as T[];
			if (array == null)
			{
				throw new ArgumentException("Value is of the incorrect type.", "value");
			}
			this.WriteUInt32((uint)array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				this.WritePropertyValueInternal(elementPropertyType, array[i], string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000067C8 File Offset: 0x000049C8
		// Note: this type is marked as 'beforefieldinit'.
		static Writer()
		{
			byte[] asciiNullTerminator = new byte[1];
			Writer.AsciiNullTerminator = asciiNullTerminator;
			byte[] unicodeNullTerminator = new byte[2];
			Writer.UnicodeNullTerminator = unicodeNullTerminator;
		}

		// Token: 0x040000C7 RID: 199
		private static readonly byte[] AsciiNullTerminator;

		// Token: 0x040000C8 RID: 200
		private static readonly byte[] UnicodeNullTerminator;
	}
}
