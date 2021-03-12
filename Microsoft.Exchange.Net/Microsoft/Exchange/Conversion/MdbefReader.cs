using System;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Conversion
{
	// Token: 0x020006AD RID: 1709
	internal class MdbefReader
	{
		// Token: 0x06001FAF RID: 8111 RVA: 0x0003C268 File Offset: 0x0003A468
		public MdbefReader(byte[] data, int startIndex, int length)
		{
			this.data = data;
			this.currentIndex = startIndex;
			this.endIndex = startIndex + length;
			this.expectedCount = this.ReadInt32();
			if (this.expectedCount > length / 4)
			{
				throw new MdbefException(NetException.TruncatedData);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0003C2B9 File Offset: 0x0003A4B9
		public int PropertyId
		{
			get
			{
				return this.propId;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x0003C2C1 File Offset: 0x0003A4C1
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0003C2CC File Offset: 0x0003A4CC
		public bool ReadNextProperty()
		{
			bool flag = true;
			while (flag)
			{
				if (this.currentIndex == this.endIndex)
				{
					if (this.parsedCount < this.expectedCount)
					{
						throw new MdbefException(NetException.TruncatedData);
					}
					return false;
				}
				else
				{
					if (this.parsedCount == this.expectedCount)
					{
						throw new MdbefException(NetException.ReadPastEnd);
					}
					this.value = null;
					this.propId = this.ReadInt32();
					MapiPropType mapiPropType = (MapiPropType)(this.propId & 65535);
					flag = false;
					MapiPropType mapiPropType2 = mapiPropType;
					if (mapiPropType2 <= MapiPropType.ServerId)
					{
						if (mapiPropType2 <= MapiPropType.String)
						{
							switch (mapiPropType2)
							{
							case MapiPropType.Null:
							case (MapiPropType)8:
							case (MapiPropType)9:
							case MapiPropType.Error:
								goto IL_33F;
							case MapiPropType.Short:
								this.value = this.ReadInt16();
								goto IL_34F;
							case MapiPropType.Int:
								this.value = this.ReadInt32();
								goto IL_34F;
							case MapiPropType.Float:
								this.value = this.ReadSingle();
								goto IL_34F;
							case MapiPropType.Double:
							case MapiPropType.AppTime:
								this.value = this.ReadDouble();
								goto IL_34F;
							case MapiPropType.Currency:
								break;
							case MapiPropType.Boolean:
								this.value = this.ReadBoolean();
								goto IL_34F;
							default:
								if (mapiPropType2 != MapiPropType.Long)
								{
									switch (mapiPropType2)
									{
									case MapiPropType.AnsiString:
										try
										{
											this.value = this.ReadANSIString();
											goto IL_34F;
										}
										catch (DecoderFallbackException innerException)
										{
											if (this.propId >> 16 == 3098 || this.propId >> 16 == 12289)
											{
												flag = true;
												goto IL_34F;
											}
											throw new MdbefException("Invalid ANSI string while decoding property with propId: " + this.propId, innerException);
										}
										break;
									case MapiPropType.String:
										break;
									default:
										goto IL_33F;
									}
									this.value = this.ReadUTF16String();
									goto IL_34F;
								}
								break;
							}
							this.value = this.ReadInt64();
						}
						else if (mapiPropType2 != MapiPropType.SysTime)
						{
							if (mapiPropType2 != MapiPropType.Guid)
							{
								if (mapiPropType2 != MapiPropType.ServerId)
								{
									goto IL_33F;
								}
								goto IL_29C;
							}
							else
							{
								this.value = this.ReadGuid();
							}
						}
						else
						{
							this.value = this.ReadDateTime();
						}
					}
					else if (mapiPropType2 <= MapiPropType.LongArray)
					{
						if (mapiPropType2 == MapiPropType.Binary)
						{
							goto IL_29C;
						}
						switch (mapiPropType2)
						{
						case MapiPropType.ShortArray:
							this.value = this.ReadInt16Array();
							goto IL_34F;
						case MapiPropType.IntArray:
							this.value = this.ReadInt32Array();
							goto IL_34F;
						case MapiPropType.FloatArray:
							this.value = this.ReadSingleArray();
							goto IL_34F;
						case MapiPropType.DoubleArray:
						case MapiPropType.AppTimeArray:
							this.value = this.ReadDoubleArray();
							goto IL_34F;
						case MapiPropType.CurrencyArray:
							break;
						default:
							if (mapiPropType2 != MapiPropType.LongArray)
							{
								goto IL_33F;
							}
							break;
						}
						this.value = this.ReadInt64Array();
					}
					else if (mapiPropType2 <= MapiPropType.SysTimeArray)
					{
						switch (mapiPropType2)
						{
						case MapiPropType.AnsiStringArray:
							this.value = this.ReadANSIStringArray();
							break;
						case MapiPropType.StringArray:
							this.value = this.ReadUTF16StringArray();
							break;
						default:
							if (mapiPropType2 != MapiPropType.SysTimeArray)
							{
								goto IL_33F;
							}
							this.value = this.ReadDateTimeArray();
							break;
						}
					}
					else if (mapiPropType2 != MapiPropType.GuidArray)
					{
						if (mapiPropType2 != MapiPropType.BinaryArray)
						{
							goto IL_33F;
						}
						this.value = this.ReadBinaryArray();
					}
					else
					{
						this.value = this.ReadGuidArray();
					}
					IL_34F:
					this.parsedCount++;
					continue;
					IL_29C:
					this.value = this.ReadByteArray();
					goto IL_34F;
					IL_33F:
					throw new MdbefException(NetException.UnknownPropertyType);
				}
			}
			return true;
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0003C650 File Offset: 0x0003A850
		private short ReadInt16()
		{
			if (this.currentIndex > this.endIndex - 2)
			{
				throw new MdbefException(NetException.TruncatedData);
			}
			short result = BitConverter.ToInt16(this.data, this.currentIndex);
			this.currentIndex += 2;
			return result;
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0003C69E File Offset: 0x0003A89E
		private bool ReadBoolean()
		{
			return 0 != this.ReadInt16();
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0003C6AC File Offset: 0x0003A8AC
		private int ReadInt32()
		{
			if (this.currentIndex > this.endIndex - 4)
			{
				throw new MdbefException(NetException.TruncatedData);
			}
			int result = BitConverter.ToInt32(this.data, this.currentIndex);
			this.currentIndex += 4;
			return result;
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0003C6FC File Offset: 0x0003A8FC
		private long ReadInt64()
		{
			if (this.currentIndex > this.endIndex - 8)
			{
				throw new MdbefException(NetException.TruncatedData);
			}
			long result = BitConverter.ToInt64(this.data, this.currentIndex);
			this.currentIndex += 8;
			return result;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0003C74C File Offset: 0x0003A94C
		private float ReadSingle()
		{
			if (this.currentIndex > this.endIndex - 4)
			{
				throw new MdbefException(NetException.TruncatedData);
			}
			float result = BitConverter.ToSingle(this.data, this.currentIndex);
			this.currentIndex += 4;
			return result;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0003C79C File Offset: 0x0003A99C
		private double ReadDouble()
		{
			if (this.currentIndex > this.endIndex - 8)
			{
				throw new MdbefException(NetException.TruncatedData);
			}
			double result = BitConverter.ToDouble(this.data, this.currentIndex);
			this.currentIndex += 8;
			return result;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0003C7EC File Offset: 0x0003A9EC
		private DateTime ReadDateTime()
		{
			DateTime result;
			try
			{
				result = DateTime.FromFileTimeUtc(this.ReadInt64());
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw new MdbefException(NetException.InvalidDateValue, innerException);
			}
			return result;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0003C82C File Offset: 0x0003AA2C
		private Guid ReadGuid()
		{
			if (this.currentIndex > this.endIndex - 16)
			{
				throw new MdbefException(NetException.TruncatedData);
			}
			byte[] array = new byte[16];
			Array.Copy(this.data, this.currentIndex, array, 0, 16);
			this.currentIndex += 16;
			return new Guid(array);
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0003C88C File Offset: 0x0003AA8C
		private string ReadANSIString()
		{
			int num = this.ReadInt32();
			if (num < 1 || num > this.endIndex - this.currentIndex)
			{
				throw new MdbefException("invalid string length prefix");
			}
			int num2 = Array.IndexOf<byte>(this.data, 0, this.currentIndex, num);
			if (num2 == -1)
			{
				throw new MdbefException("string is not null-terminated");
			}
			FeInboundCharsetDetector feInboundCharsetDetector = new FeInboundCharsetDetector(Encoding.ASCII.CodePage, false, true, true, true);
			feInboundCharsetDetector.AddBytes(this.data, this.currentIndex, num2 - this.currentIndex, false);
			int codePageChoice = feInboundCharsetDetector.GetCodePageChoice();
			Encoding encoding = Encoding.GetEncoding(codePageChoice, new EncoderExceptionFallback(), new DecoderExceptionFallback());
			string result = null;
			try
			{
				result = encoding.GetString(this.data, this.currentIndex, num2 - this.currentIndex);
			}
			finally
			{
				this.currentIndex += num;
			}
			return result;
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0003C970 File Offset: 0x0003AB70
		private string ReadUTF16String()
		{
			int num = this.ReadInt32();
			if (num < 2 || num > this.endIndex - this.currentIndex)
			{
				throw new MdbefException(NetException.CorruptStringSize);
			}
			int num2 = this.currentIndex;
			while (num2 < this.currentIndex + num - 2 && (this.data[num2] != 0 || this.data[num2 + 1] != 0))
			{
				num2 += 2;
			}
			if (this.data[num2] != 0 || this.data[num2 + 1] != 0)
			{
				throw new MdbefException(NetException.MissingNullTerminator);
			}
			string result = null;
			try
			{
				result = MdbefReader.CheckedUTF16.GetString(this.data, this.currentIndex, num2 - this.currentIndex);
			}
			catch (DecoderFallbackException innerException)
			{
				throw new MdbefException(NetException.InvalidUnicodeString, innerException);
			}
			this.currentIndex += num;
			return result;
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0003CA54 File Offset: 0x0003AC54
		private byte[] ReadByteArray()
		{
			int num = this.ReadInt32();
			if (num < 0 || num > this.endIndex - this.currentIndex)
			{
				throw new MdbefException(NetException.CorruptArraySize);
			}
			byte[] array = new byte[num];
			Array.Copy(this.data, this.currentIndex, array, 0, num);
			this.currentIndex += num;
			return array;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0003CAB8 File Offset: 0x0003ACB8
		private T[] ReadArray<T>(MdbefReader.ReadOne<T> readOne, int minimumElementSize)
		{
			int num = this.ReadInt32();
			if (num < 0)
			{
				throw new MdbefException(NetException.CorruptArraySize);
			}
			if (num > (this.endIndex - this.currentIndex) / minimumElementSize)
			{
				throw new MdbefException(NetException.CorruptArraySize);
			}
			T[] array = new T[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = readOne();
			}
			return array;
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0003CB23 File Offset: 0x0003AD23
		private short[] ReadInt16Array()
		{
			return this.ReadArray<short>(new MdbefReader.ReadOne<short>(this.ReadInt16), 2);
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0003CB38 File Offset: 0x0003AD38
		private int[] ReadInt32Array()
		{
			return this.ReadArray<int>(new MdbefReader.ReadOne<int>(this.ReadInt32), 4);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0003CB4D File Offset: 0x0003AD4D
		private long[] ReadInt64Array()
		{
			return this.ReadArray<long>(new MdbefReader.ReadOne<long>(this.ReadInt64), 8);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0003CB62 File Offset: 0x0003AD62
		private float[] ReadSingleArray()
		{
			return this.ReadArray<float>(new MdbefReader.ReadOne<float>(this.ReadSingle), 4);
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0003CB77 File Offset: 0x0003AD77
		private double[] ReadDoubleArray()
		{
			return this.ReadArray<double>(new MdbefReader.ReadOne<double>(this.ReadDouble), 8);
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0003CB8C File Offset: 0x0003AD8C
		private DateTime[] ReadDateTimeArray()
		{
			return this.ReadArray<DateTime>(new MdbefReader.ReadOne<DateTime>(this.ReadDateTime), 8);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0003CBA1 File Offset: 0x0003ADA1
		private Guid[] ReadGuidArray()
		{
			return this.ReadArray<Guid>(new MdbefReader.ReadOne<Guid>(this.ReadGuid), 16);
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0003CBB7 File Offset: 0x0003ADB7
		private string[] ReadANSIStringArray()
		{
			return this.ReadArray<string>(new MdbefReader.ReadOne<string>(this.ReadANSIString), 4);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0003CBCC File Offset: 0x0003ADCC
		private string[] ReadUTF16StringArray()
		{
			return this.ReadArray<string>(new MdbefReader.ReadOne<string>(this.ReadUTF16String), 4);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0003CBE1 File Offset: 0x0003ADE1
		private byte[][] ReadBinaryArray()
		{
			return this.ReadArray<byte[]>(new MdbefReader.ReadOne<byte[]>(this.ReadByteArray), 4);
		}

		// Token: 0x04001ECD RID: 7885
		private static readonly Encoding CheckedUTF16 = new UnicodeEncoding(false, false, true);

		// Token: 0x04001ECE RID: 7886
		private int expectedCount;

		// Token: 0x04001ECF RID: 7887
		private int parsedCount;

		// Token: 0x04001ED0 RID: 7888
		private byte[] data;

		// Token: 0x04001ED1 RID: 7889
		private int currentIndex;

		// Token: 0x04001ED2 RID: 7890
		private int endIndex;

		// Token: 0x04001ED3 RID: 7891
		private int propId;

		// Token: 0x04001ED4 RID: 7892
		private object value;

		// Token: 0x020006AE RID: 1710
		// (Invoke) Token: 0x06001FCB RID: 8139
		private delegate T ReadOne<T>();
	}
}
