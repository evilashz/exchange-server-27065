using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000023 RID: 35
	internal class MimeHeaderStream : StreamWrapper
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00007158 File Offset: 0x00005358
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.isWriting)
			{
				return;
			}
			byte[] array;
			int num;
			if (this.lastThreeBytes != null)
			{
				array = new byte[this.lastThreeBytes.Length + count];
				Array.Copy(this.lastThreeBytes, array, this.lastThreeBytes.Length);
				Array.Copy(buffer, offset, array, this.lastThreeBytes.Length, count);
				num = this.lastThreeBytes.Length;
			}
			else
			{
				array = new byte[count];
				Array.Copy(buffer, offset, array, 0, count);
				num = 0;
			}
			for (int i = 0; i < array.Length - 3; i++)
			{
				if (array[i] == 13 && array[i + 1] == 10 && array[i + 2] == 13 && array[i + 3] == 10)
				{
					base.Write(buffer, offset, i - num + 4);
					this.isWriting = false;
					return;
				}
			}
			this.lastThreeBytes = new byte[Math.Min(3, array.Length)];
			Array.Copy(array, array.Length - this.lastThreeBytes.Length, this.lastThreeBytes, 0, this.lastThreeBytes.Length);
			base.Write(buffer, offset, count);
		}

		// Token: 0x04000111 RID: 273
		private bool isWriting = true;

		// Token: 0x04000112 RID: 274
		private byte[] lastThreeBytes;
	}
}
