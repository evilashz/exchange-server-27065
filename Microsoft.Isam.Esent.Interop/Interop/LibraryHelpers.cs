using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002DB RID: 731
	internal static class LibraryHelpers
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0001AF08 File Offset: 0x00019108
		public static Encoding EncodingASCII
		{
			get
			{
				return Encoding.ASCII;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0001AF0F File Offset: 0x0001910F
		public static Encoding NewEncodingASCII
		{
			get
			{
				return new ASCIIEncoding();
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0001AF16 File Offset: 0x00019116
		public static CultureInfo CreateCultureInfoByLcid(int lcid)
		{
			return new CultureInfo(lcid);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0001AF1E File Offset: 0x0001911E
		public static IntPtr MarshalAllocHGlobal(int size)
		{
			return Marshal.AllocHGlobal(size);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0001AF26 File Offset: 0x00019126
		public static void MarshalFreeHGlobal(IntPtr buffer)
		{
			Marshal.FreeHGlobal(buffer);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0001AF2E File Offset: 0x0001912E
		public static IntPtr MarshalStringToHGlobalUni(string managedString)
		{
			return Marshal.StringToHGlobalUni(managedString);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0001AF36 File Offset: 0x00019136
		public static int GetCurrentManagedThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0001AF42 File Offset: 0x00019142
		public static void ThreadResetAbort()
		{
			Thread.ResetAbort();
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0001AF49 File Offset: 0x00019149
		public static DateTime FromOADate(double d)
		{
			return DateTime.FromOADate(d);
		}

		// Token: 0x04000909 RID: 2313
		public static readonly char DirectorySeparatorChar = '\\';

		// Token: 0x0400090A RID: 2314
		public static readonly char AltDirectorySeparatorChar = '/';
	}
}
