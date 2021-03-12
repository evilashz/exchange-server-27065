using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B49 RID: 2889
	internal sealed class ThreadTimes
	{
		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000A28E8 File Offset: 0x000A0AE8
		// (set) Token: 0x06003E3B RID: 15931 RVA: 0x000A28F0 File Offset: 0x000A0AF0
		public TimeSpan Kernel { get; private set; }

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x000A28F9 File Offset: 0x000A0AF9
		// (set) Token: 0x06003E3D RID: 15933 RVA: 0x000A2901 File Offset: 0x000A0B01
		public TimeSpan User { get; private set; }

		// Token: 0x06003E3E RID: 15934 RVA: 0x000A290A File Offset: 0x000A0B0A
		private ThreadTimes(TimeSpan kernel, TimeSpan user)
		{
			this.Kernel = kernel;
			this.User = user;
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x000A2920 File Offset: 0x000A0B20
		public static ThreadTimes GetFromCurrentThread()
		{
			TimeSpan kernel;
			TimeSpan user;
			if (ThreadTimes.GetFromCurrentThread(out kernel, out user))
			{
				return new ThreadTimes(kernel, user);
			}
			return null;
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x000A2944 File Offset: 0x000A0B44
		public static bool GetFromCurrentThread(out TimeSpan kernelTime, out TimeSpan userTime)
		{
			bool result;
			using (SafeThreadHandle currentThread = NativeMethods.GetCurrentThread())
			{
				long num;
				long num2;
				long value;
				long value2;
				if (ThreadTimes.GetThreadTimes(currentThread.DangerousGetHandle(), out num, out num2, out value, out value2))
				{
					kernelTime = TimeSpan.FromTicks(value);
					userTime = TimeSpan.FromTicks(value2);
					result = true;
				}
				else
				{
					kernelTime = default(TimeSpan);
					userTime = default(TimeSpan);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06003E41 RID: 15937
		[DllImport("KERNEL32.DLL", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetThreadTimes(IntPtr hThread, out long lpCreationTime, out long lpExitTime, out long lpKernelTime, out long lpUserTime);
	}
}
