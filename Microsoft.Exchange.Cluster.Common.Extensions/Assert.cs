using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Common.Extensions
{
	// Token: 0x02000002 RID: 2
	internal class Assert : IAssert
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private Assert()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public static Assert Instance
		{
			get
			{
				return Assert.instance;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020DF File Offset: 0x000002DF
		public void Debug(bool condition, string formatString, params object[] parameters)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E1 File Offset: 0x000002E1
		public void Retail(bool condition, string formatString, params object[] parameters)
		{
			ExAssert.RetailAssert(condition, formatString, parameters);
		}

		// Token: 0x06000005 RID: 5
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsDebuggerPresent();

		// Token: 0x06000006 RID: 6
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "DebugBreak")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool Kernel32DebugBreak();

		// Token: 0x06000007 RID: 7
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, EntryPoint = "OutputDebugStringW")]
		private static extern void OutputDebugString(string message);

		// Token: 0x06000008 RID: 8 RVA: 0x000020EB File Offset: 0x000002EB
		private bool IsDebuggerAttached()
		{
			return Debugger.IsAttached || Assert.IsDebuggerPresent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002100 File Offset: 0x00000300
		private void DebugBreak()
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
				return;
			}
			if (Assert.IsDebuggerPresent())
			{
				Assert.Kernel32DebugBreak();
				return;
			}
			Debugger.Break();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002122 File Offset: 0x00000322
		private void PrintToDebugger(string message)
		{
			if (Assert.IsDebuggerPresent())
			{
				Assert.OutputDebugString(message);
			}
		}

		// Token: 0x04000001 RID: 1
		private static Assert instance = new Assert();
	}
}
