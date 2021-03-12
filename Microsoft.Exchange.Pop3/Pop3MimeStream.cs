using System;
using System.IO;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000005 RID: 5
	internal class Pop3MimeStream : StreamWrapper
	{
		// Token: 0x1700000C RID: 12
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002ABE File Offset: 0x00000CBE
		public int LinesToReturn
		{
			set
			{
				this.linesToReturn = value;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.linesToReturn > -1 && this.linesFound >= this.linesToReturn)
			{
				return 0;
			}
			int num = base.Read(buffer, offset, count);
			if (this.linesToReturn > -1)
			{
				for (int i = 0; i < num; i++)
				{
					if (buffer[i] == 10)
					{
						if (!this.textFound)
						{
							if (i < num - 1 && buffer[i + 1] == 10)
							{
								this.textFound = true;
								i++;
							}
							else if (i < num - 2 && buffer[i + 1] == 13 && buffer[i + 2] == 10)
							{
								this.textFound = true;
								i += 2;
							}
							else if (i == num - 2 && buffer[i + 1] == 13 && this.Position < this.Length - 1L)
							{
								byte[] array = new byte[1];
								int num2 = base.Read(array, 0, 1);
								base.Seek((long)(-(long)num2), SeekOrigin.Current);
								if (num2 == 1 && array[0] == 10)
								{
									this.textFound = true;
									break;
								}
							}
							else if (i == num - 1 && this.Position < this.Length - 1L)
							{
								byte[] array2 = new byte[2];
								int num3 = base.Read(array2, 0, 2);
								base.Seek((long)(-(long)num3), SeekOrigin.Current);
								if ((num3 > 0 && array2[0] == 10) || (num3 == 2 && array2[0] == 13 && array2[1] == 10))
								{
									this.textFound = true;
									break;
								}
							}
						}
						if (this.textFound && ++this.linesFound >= this.linesToReturn)
						{
							num = i + 1;
							break;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C54 File Offset: 0x00000E54
		public override void Write(byte[] buffer, int offset, int count)
		{
			byte b = 0;
			byte b2 = 0;
			if (this.Length > 1L)
			{
				base.Seek(-2L, SeekOrigin.End);
				b = (byte)this.ReadByte();
				b2 = (byte)this.ReadByte();
			}
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				if (b == 13 && b2 == 10 && buffer[offset + i] == 46)
				{
					base.Write(buffer, offset + num, i - num + 1);
					num = i;
				}
				b = b2;
				b2 = buffer[offset + i];
			}
			base.Write(buffer, offset + num, count - num);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002CD1 File Offset: 0x00000ED1
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (offset == 0L && origin == SeekOrigin.Begin)
			{
				this.linesFound = -1;
				this.textFound = false;
			}
			return base.Seek(offset, origin);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002CF1 File Offset: 0x00000EF1
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x04000015 RID: 21
		private int linesToReturn = -1;

		// Token: 0x04000016 RID: 22
		private int linesFound;

		// Token: 0x04000017 RID: 23
		private bool textFound;
	}
}
