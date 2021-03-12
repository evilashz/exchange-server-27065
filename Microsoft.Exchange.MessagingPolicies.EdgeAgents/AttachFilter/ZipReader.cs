using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies.AttachFilter
{
	// Token: 0x0200000B RID: 11
	internal sealed class ZipReader : IDisposable, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000036BD File Offset: 0x000018BD
		public ZipReader(Stream input, int nestedLevel)
		{
			this.input = input;
			this.nestedLevel = nestedLevel;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000036D3 File Offset: 0x000018D3
		public void Dispose()
		{
			if (this.input != null)
			{
				this.input.Close();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000036E8 File Offset: 0x000018E8
		IEnumerator<string> IEnumerable<string>.GetEnumerator()
		{
			return new ZipReader.ZipEnumerator(this);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000036F0 File Offset: 0x000018F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ZipReader.ZipEnumerator(this);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000036F8 File Offset: 0x000018F8
		internal static bool Skip(BinaryReader reader, long bytes)
		{
			if (bytes + reader.BaseStream.Position <= 0L)
			{
				return false;
			}
			if (reader.BaseStream.CanSeek)
			{
				reader.BaseStream.Seek(bytes, SeekOrigin.Current);
			}
			else
			{
				long num;
				do
				{
					num = bytes;
					bytes = num - 1L;
				}
				while (num > 0L && reader.BaseStream.ReadByte() != -1);
				if (bytes > 0L)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003758 File Offset: 0x00001958
		internal static byte[] SafeReadHeader(BinaryReader reader, int length)
		{
			if (length <= 0)
			{
				throw new ExchangeDataException("bad zip file");
			}
			byte[] array = reader.ReadBytes(length);
			if (array == null || array.Length < length)
			{
				throw new ExchangeDataException("bad zip file");
			}
			return array;
		}

		// Token: 0x04000034 RID: 52
		private Stream input;

		// Token: 0x04000035 RID: 53
		private int nestedLevel;

		// Token: 0x0200000C RID: 12
		private sealed class ZipEnumerator : IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x06000038 RID: 56 RVA: 0x00003794 File Offset: 0x00001994
			public ZipEnumerator(ZipReader reader)
			{
				this.buffer = new BinaryReader(reader.input, ZipReader.ZipEnumerator.encoding);
				this.trail = new Stack<ZipReader.ZipEnumerator.BookMark>();
				this.bytesTally = 0L;
				this.nestedLevel = reader.nestedLevel;
				this.fileName = string.Empty;
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000039 RID: 57 RVA: 0x000037E7 File Offset: 0x000019E7
			public object Current
			{
				get
				{
					return this.fileName;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600003A RID: 58 RVA: 0x000037EF File Offset: 0x000019EF
			string IEnumerator<string>.Current
			{
				get
				{
					return this.fileName;
				}
			}

			// Token: 0x0600003B RID: 59 RVA: 0x000037F8 File Offset: 0x000019F8
			public bool MoveNext()
			{
				if (this.trail != null && this.trail.Count > this.nestedLevel)
				{
					throw new ExchangeDataException("bad zip file");
				}
				this.fileName = string.Empty;
				bool result;
				try
				{
					uint num = 0U;
					try
					{
						num = this.buffer.ReadUInt32();
					}
					catch (EndOfStreamException)
					{
						return false;
					}
					this.bytesTally += 4L;
					if (num != 67324752U)
					{
						if (this.trail.Count > 0)
						{
							ZipReader.ZipEnumerator.BookMark bookMark = this.trail.Pop();
							long num2 = bookMark.Length + bookMark.Offset - this.bytesTally;
							if (!ZipReader.Skip(this.buffer, num2))
							{
								result = false;
							}
							else
							{
								this.bytesTally += num2;
								result = true;
							}
						}
						else
						{
							if (num != 33639248U && (num & 65535U) != 23U)
							{
								throw new ExchangeDataException("bad zip file");
							}
							result = false;
						}
					}
					else
					{
						this.buffer.ReadUInt16();
						ushort num3 = this.buffer.ReadUInt16();
						this.bytesTally += 4L;
						this.buffer.ReadUInt16();
						this.buffer.ReadUInt32();
						this.bytesTally += 6L;
						this.buffer.ReadUInt32();
						long num4 = (long)((ulong)this.buffer.ReadUInt32());
						this.bytesTally += 8L;
						this.buffer.ReadUInt32();
						int num5 = (int)this.buffer.ReadUInt16();
						int num6 = (int)this.buffer.ReadUInt16();
						this.bytesTally += 8L;
						byte[] bytes = this.buffer.ReadBytes(num5);
						this.bytesTally += (long)num5;
						this.fileName = ZipReader.ZipEnumerator.encoding.GetString(bytes);
						if (num6 > 0)
						{
							if (!ZipReader.Skip(this.buffer, (long)num6))
							{
								return false;
							}
							this.bytesTally += (long)num6;
						}
						if (num4 > 0L)
						{
							if (this.fileName.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
							{
								this.trail.Push(new ZipReader.ZipEnumerator.BookMark(this.bytesTally, num4));
								return true;
							}
							if (!ZipReader.Skip(this.buffer, num4))
							{
								return false;
							}
							this.bytesTally += num4;
						}
						if ((num3 & 8) != 0)
						{
							if (num4 == 0L)
							{
								uint num7 = this.buffer.ReadUInt32();
								this.bytesTally += 4L;
								while (num7 != 134695760U)
								{
									uint num8 = (uint)this.buffer.ReadByte();
									num7 = (num7 >> 8 | num8 << 24);
									this.bytesTally += 1L;
								}
							}
							this.buffer.ReadUInt32();
							num4 = (long)((ulong)this.buffer.ReadUInt32());
							this.buffer.ReadUInt32();
							this.bytesTally += 12L;
						}
						result = true;
					}
				}
				catch (IOException)
				{
					throw new ExchangeDataException("bad zip file");
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new ExchangeDataException("bad zip file");
				}
				return result;
			}

			// Token: 0x0600003C RID: 60 RVA: 0x00003B54 File Offset: 0x00001D54
			public void Dispose()
			{
				if (this.buffer != null)
				{
					this.buffer.Close();
				}
			}

			// Token: 0x0600003D RID: 61 RVA: 0x00003B69 File Offset: 0x00001D69
			public void Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000036 RID: 54
			private const int DataDescriptorSignature = 134695760;

			// Token: 0x04000037 RID: 55
			private static Encoding encoding = new UTF8Encoding();

			// Token: 0x04000038 RID: 56
			private BinaryReader buffer;

			// Token: 0x04000039 RID: 57
			private Stack<ZipReader.ZipEnumerator.BookMark> trail;

			// Token: 0x0400003A RID: 58
			private long bytesTally;

			// Token: 0x0400003B RID: 59
			private string fileName;

			// Token: 0x0400003C RID: 60
			private int nestedLevel;

			// Token: 0x0200000D RID: 13
			private struct BookMark
			{
				// Token: 0x0600003F RID: 63 RVA: 0x00003B7C File Offset: 0x00001D7C
				public BookMark(long length, long offset)
				{
					this.Length = length;
					this.Offset = offset;
				}

				// Token: 0x0400003D RID: 61
				internal long Length;

				// Token: 0x0400003E RID: 62
				internal long Offset;
			}
		}
	}
}
