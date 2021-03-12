using System;
using System.IO;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Transport.Pickup
{
	// Token: 0x02000522 RID: 1314
	internal sealed class FileList : IDisposable
	{
		// Token: 0x06003D4E RID: 15694 RVA: 0x000FFD7D File Offset: 0x000FDF7D
		internal FileList(string path, string filter)
		{
			this.path = path;
			this.filter = filter;
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x000FFD93 File Offset: 0x000FDF93
		public void Dispose()
		{
			this.StopSearch();
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x000FFD9C File Offset: 0x000FDF9C
		internal bool GetNextFile(out string fileName, out ulong fileSize)
		{
			bool flag = false;
			NativeMethods.WIN32_FIND_DATA win32_FIND_DATA;
			win32_FIND_DATA.FileName = string.Empty;
			win32_FIND_DATA.FileSizeHigh = 0U;
			win32_FIND_DATA.FileSizeLow = 0U;
			bool flag2;
			do
			{
				if (this.safeFindHandle == null)
				{
					string fileName2 = Path.Combine(this.path, this.filter);
					this.safeFindHandle = NativeMethods.FindFirstFile(fileName2, out win32_FIND_DATA);
					flag2 = !this.safeFindHandle.IsInvalid;
				}
				else
				{
					flag2 = NativeMethods.FindNextFile(this.safeFindHandle, out win32_FIND_DATA);
				}
				if (flag2 && (win32_FIND_DATA.FileAttributes & NativeMethods.FileAttributes.Directory) == NativeMethods.FileAttributes.None)
				{
					flag = true;
				}
			}
			while (flag2 && !flag);
			if (flag)
			{
				fileName = Path.Combine(this.path, win32_FIND_DATA.FileName);
				fileSize = (ulong)(win32_FIND_DATA.FileSizeHigh + win32_FIND_DATA.FileSizeLow);
			}
			else
			{
				fileName = string.Empty;
				fileSize = 0UL;
				this.StopSearch();
			}
			return flag;
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x000FFE64 File Offset: 0x000FE064
		internal void StopSearch()
		{
			if (this.safeFindHandle != null)
			{
				this.safeFindHandle.Close();
				this.safeFindHandle = null;
			}
		}

		// Token: 0x04001F36 RID: 7990
		private string path;

		// Token: 0x04001F37 RID: 7991
		private string filter;

		// Token: 0x04001F38 RID: 7992
		private SafeFindHandle safeFindHandle;
	}
}
