using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Win32
{
	// Token: 0x02000B47 RID: 2887
	internal sealed class SafeSpnArrayHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003E2E RID: 15918 RVA: 0x000A27D8 File Offset: 0x000A09D8
		public SafeSpnArrayHandle() : base(true)
		{
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x000A27E1 File Offset: 0x000A09E1
		public void SetCount(uint count)
		{
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("SetCount() called on an invalid handle");
			}
			this.count = new uint?(count);
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x000A2804 File Offset: 0x000A0A04
		public string[] GetSpnStrings(uint count)
		{
			if (this.IsInvalid)
			{
				throw new InvalidOperationException("GetSpnStrings() called on an invalid handle");
			}
			this.SetCount(count);
			string[] array = new string[count];
			int num = 0;
			while ((long)num < (long)((ulong)count))
			{
				array[num] = Marshal.PtrToStringUni(Marshal.ReadIntPtr(this.handle, num * SafeSpnArrayHandle.SizeOfIntPtr));
				num++;
			}
			return array;
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x000A285C File Offset: 0x000A0A5C
		protected override bool ReleaseHandle()
		{
			if (this.count == null)
			{
				throw new InvalidOperationException("Unknown Spn elements count.  SetCount() or GetSpnStrings() must be called.");
			}
			SafeSpnArrayHandle.DsFreeSpnArray(this.count.Value, this.handle);
			return true;
		}

		// Token: 0x06003E32 RID: 15922
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("NTDSAPI.DLL", CharSet = CharSet.Unicode, EntryPoint = "DsFreeSpnArrayW")]
		private static extern void DsFreeSpnArray([In] uint spnCount, [In] IntPtr spnArray);

		// Token: 0x040035F2 RID: 13810
		private static readonly int SizeOfIntPtr = Marshal.SizeOf(typeof(IntPtr));

		// Token: 0x040035F3 RID: 13811
		private uint? count;
	}
}
