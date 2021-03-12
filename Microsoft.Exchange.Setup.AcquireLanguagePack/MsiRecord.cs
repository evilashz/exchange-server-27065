using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000011 RID: 17
	internal sealed class MsiRecord : MsiBase
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002F4B File Offset: 0x0000114B
		public MsiRecord(MsiView view)
		{
			this.FetchNextRecord(view);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F5C File Offset: 0x0000115C
		public void SaveStream(uint field, string fileName)
		{
			byte[] array = new byte[256];
			int num = array.Length;
			using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
			{
				for (;;)
				{
					uint errorCode = MsiNativeMethods.RecordReadStream(base.Handle, field, array, ref num);
					MsiHelper.ThrowIfNotSuccess(errorCode);
					if (num == 0)
					{
						break;
					}
					fileStream.Write(array, 0, num);
				}
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002FC0 File Offset: 0x000011C0
		public string GetString(uint field)
		{
			string empty = string.Empty;
			uint dataSize = this.GetDataSize(field);
			if (dataSize == 0U)
			{
				throw new MsiException(Strings.InvalidFieldDataSize(field));
			}
			StringBuilder stringBuilder = new StringBuilder((int)(dataSize + 1U));
			uint capacity = (uint)stringBuilder.Capacity;
			uint errorCode = MsiNativeMethods.RecordGetString(base.Handle, field, stringBuilder, ref capacity);
			MsiHelper.ThrowIfNotSuccess(errorCode);
			return stringBuilder.ToString();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000301C File Offset: 0x0000121C
		private void FetchNextRecord(MsiView view)
		{
			ValidationHelper.ThrowIfNull(view, "view");
			SafeMsiHandle handle;
			uint errorCode = MsiNativeMethods.ViewFetch(view.Handle, out handle);
			MsiHelper.ThrowIfNotSuccess(errorCode);
			base.Handle = handle;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000304F File Offset: 0x0000124F
		private uint GetDataSize(uint field)
		{
			return MsiNativeMethods.RecordDataSize(base.Handle, field);
		}

		// Token: 0x04000030 RID: 48
		private const int MaxBufferSize = 256;

		// Token: 0x02000012 RID: 18
		public enum Fields
		{
			// Token: 0x04000032 RID: 50
			Property = 1,
			// Token: 0x04000033 RID: 51
			Stream
		}
	}
}
