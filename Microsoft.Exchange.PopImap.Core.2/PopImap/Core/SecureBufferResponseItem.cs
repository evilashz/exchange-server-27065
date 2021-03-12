using System;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000038 RID: 56
	internal class SecureBufferResponseItem : BufferResponseItem
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0001105C File Offset: 0x0000F25C
		public SecureBufferResponseItem(byte[] buf) : base(buf)
		{
			if (buf.Length < 2 || buf[buf.Length - 2] != 13 || buf[buf.Length - 1] != 10)
			{
				byte[] array = new byte[buf.Length + 2];
				Array.Copy(buf, array, buf.Length);
				array[buf.Length] = 13;
				array[buf.Length + 1] = 10;
				base.BindData(array, 0, array.Length, null);
				Array.Clear(buf, 0, buf.Length);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000110C7 File Offset: 0x0000F2C7
		public override int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
		{
			if (base.DataSent)
			{
				base.ClearData();
				buffer = null;
				offset = 0;
				return 0;
			}
			return base.GetNextChunk(session, out buffer, out offset);
		}
	}
}
