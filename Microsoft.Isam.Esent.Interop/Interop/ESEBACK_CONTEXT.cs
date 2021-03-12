using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000023 RID: 35
	public class ESEBACK_CONTEXT
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003212 File Offset: 0x00001412
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000321A File Offset: 0x0000141A
		public string wszServerName { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003223 File Offset: 0x00001423
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000322B File Offset: 0x0000142B
		public IntPtr pvApplicationData { get; set; }

		// Token: 0x0600008A RID: 138 RVA: 0x00003234 File Offset: 0x00001434
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ESEBACK_CONTEXT({0})", new object[]
			{
				this.wszServerName
			});
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003264 File Offset: 0x00001464
		internal NATIVE_ESEBACK_CONTEXT GetNativeEsebackContext()
		{
			return new NATIVE_ESEBACK_CONTEXT
			{
				cbSize = (uint)Marshal.SizeOf(typeof(NATIVE_ESEBACK_CONTEXT)),
				wszServerName = Marshal.StringToHGlobalUni(this.wszServerName),
				pvApplicationData = this.pvApplicationData
			};
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000032B4 File Offset: 0x000014B4
		internal void SetFromNativeEsebackContext(IntPtr nativeBackupContextPtr)
		{
			if (nativeBackupContextPtr != IntPtr.Zero)
			{
				NATIVE_ESEBACK_CONTEXT native_ESEBACK_CONTEXT = (NATIVE_ESEBACK_CONTEXT)Marshal.PtrToStructure(nativeBackupContextPtr, typeof(NATIVE_ESEBACK_CONTEXT));
				this.wszServerName = Marshal.PtrToStringUni(native_ESEBACK_CONTEXT.wszServerName);
				this.pvApplicationData = native_ESEBACK_CONTEXT.pvApplicationData;
				return;
			}
			this.wszServerName = string.Empty;
			this.pvApplicationData = IntPtr.Zero;
		}
	}
}
