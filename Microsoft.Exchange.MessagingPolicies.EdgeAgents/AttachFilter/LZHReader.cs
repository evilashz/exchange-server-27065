using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Microsoft.Exchange.MessagingPolicies.AttachFilter
{
	// Token: 0x0200000E RID: 14
	internal sealed class LZHReader : IDisposable, IEnumerable<string>, IEnumerable
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003B8C File Offset: 0x00001D8C
		public LZHReader(Stream input)
		{
			this.input = input;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003B9B File Offset: 0x00001D9B
		public void Dispose()
		{
			if (this.input != null)
			{
				this.input.Close();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003BB0 File Offset: 0x00001DB0
		IEnumerator<string> IEnumerable<string>.GetEnumerator()
		{
			return new LZHReader.LZHEnumerator(this);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003BB8 File Offset: 0x00001DB8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new LZHReader.LZHEnumerator(this);
		}

		// Token: 0x0400003F RID: 63
		private Stream input;

		// Token: 0x0200000F RID: 15
		private sealed class LZHEnumerator : IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x06000044 RID: 68 RVA: 0x00003BC0 File Offset: 0x00001DC0
			public LZHEnumerator(LZHReader reader)
			{
				this.bytesTally = 0;
				this.fileName = string.Empty;
				Encoding encoding = LZHReader.LZHEnumerator.encoding;
				if (encoding == null)
				{
					bool flag = false;
					try
					{
						encoding = Encoding.GetEncoding(932);
					}
					catch (NotSupportedException)
					{
						flag = true;
					}
					catch (ArgumentException)
					{
						flag = true;
					}
					if (flag)
					{
						encoding = new ASCIIEncoding();
					}
					Interlocked.Exchange<Encoding>(ref LZHReader.LZHEnumerator.encoding, encoding);
				}
				this.buffer = new BinaryReader(reader.input, LZHReader.LZHEnumerator.encoding);
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000045 RID: 69 RVA: 0x00003C50 File Offset: 0x00001E50
			public object Current
			{
				get
				{
					return this.fileName;
				}
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000046 RID: 70 RVA: 0x00003C58 File Offset: 0x00001E58
			string IEnumerator<string>.Current
			{
				get
				{
					return this.fileName;
				}
			}

			// Token: 0x06000047 RID: 71 RVA: 0x00003C60 File Offset: 0x00001E60
			public bool MoveNext()
			{
				this.fileName = string.Empty;
				bool result;
				try
				{
					byte[] array = this.buffer.ReadBytes(21);
					this.bytesTally += array.Length;
					if (array.Length < 21)
					{
						result = false;
					}
					else
					{
						int num = (int)array[10] << 24 | (int)array[9] << 16 | (int)array[8] << 8 | (int)array[7];
						switch (array[20])
						{
						case 0:
						case 1:
						{
							int num2 = (int)this.buffer.ReadByte();
							this.bytesTally++;
							byte[] bytes = ZipReader.SafeReadHeader(this.buffer, num2);
							this.bytesTally += num2;
							this.fileName = LZHReader.LZHEnumerator.encoding.GetString(bytes);
							if (array[20] == 0)
							{
								this.SkipLevel0Data();
							}
							else
							{
								this.SkipLevel0Data();
								this.buffer.ReadByte();
								this.SkipOtherExtensionHeaders();
							}
							break;
						}
						case 2:
							this.GetLevel2FileNameEntry();
							this.SkipOtherExtensionHeaders();
							break;
						}
						if (!ZipReader.Skip(this.buffer, (long)num))
						{
							result = false;
						}
						else
						{
							this.bytesTally += num;
							result = true;
						}
					}
				}
				catch (IOException)
				{
					result = false;
				}
				catch (ArgumentOutOfRangeException)
				{
					result = false;
				}
				return result;
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00003DC0 File Offset: 0x00001FC0
			public void Dispose()
			{
				if (this.buffer != null)
				{
					this.buffer.Close();
				}
			}

			// Token: 0x06000049 RID: 73 RVA: 0x00003DD5 File Offset: 0x00001FD5
			public void Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00003DDC File Offset: 0x00001FDC
			private void SkipLevel0Data()
			{
				ZipReader.Skip(this.buffer, 2L);
				this.bytesTally += 2;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00003DFC File Offset: 0x00001FFC
			private void GetLevel2FileNameEntry()
			{
				ZipReader.Skip(this.buffer, 3L);
				this.bytesTally += 3;
				byte[] array = ZipReader.SafeReadHeader(this.buffer, 2);
				int num = (int)array[1] << 8 | (int)array[0];
				this.bytesTally += 2;
				if (num > 0)
				{
					byte b = this.buffer.ReadByte();
					this.bytesTally++;
					while (b != 1)
					{
						ZipReader.Skip(this.buffer, (long)(num - 3));
						this.bytesTally += num - 3;
						array = ZipReader.SafeReadHeader(this.buffer, 2);
						num = ((int)array[1] << 8 | (int)array[0]);
						this.bytesTally += 2;
						if (num <= 0)
						{
							break;
						}
						b = this.buffer.ReadByte();
						this.bytesTally++;
					}
					if (b == 1)
					{
						byte[] bytes = ZipReader.SafeReadHeader(this.buffer, num - 3);
						this.bytesTally += num - 3;
						this.fileName = LZHReader.LZHEnumerator.encoding.GetString(bytes);
					}
				}
			}

			// Token: 0x0600004C RID: 76 RVA: 0x00003F08 File Offset: 0x00002108
			private void SkipOtherExtensionHeaders()
			{
				for (;;)
				{
					byte[] array = ZipReader.SafeReadHeader(this.buffer, 2);
					int num = (int)array[1] << 8 | (int)array[0];
					this.bytesTally += 2;
					if (num > 0 && !ZipReader.Skip(this.buffer, (long)(num - 2)))
					{
						break;
					}
					if (num <= 0)
					{
						return;
					}
				}
			}

			// Token: 0x04000040 RID: 64
			private static Encoding encoding;

			// Token: 0x04000041 RID: 65
			private BinaryReader buffer;

			// Token: 0x04000042 RID: 66
			private int bytesTally;

			// Token: 0x04000043 RID: 67
			private string fileName;

			// Token: 0x02000010 RID: 16
			private struct BookMark
			{
				// Token: 0x0600004D RID: 77 RVA: 0x00003F56 File Offset: 0x00002156
				public BookMark(int length, int offset)
				{
					this.Length = length;
					this.Offset = offset;
				}

				// Token: 0x04000044 RID: 68
				internal int Length;

				// Token: 0x04000045 RID: 69
				internal int Offset;
			}
		}
	}
}
