using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200000D RID: 13
	public class ESE_ICON_DESCRIPTION
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000027BA File Offset: 0x000009BA
		public int ulSize
		{
			get
			{
				if (this.pvData == null)
				{
					return 0;
				}
				return this.pvData.Length;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000027CE File Offset: 0x000009CE
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000027D6 File Offset: 0x000009D6
		public byte[] pvData { get; set; }

		// Token: 0x06000023 RID: 35 RVA: 0x000027E0 File Offset: 0x000009E0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ESE_ICON_DESCRIPTION({0})", new object[]
			{
				(this.pvData == null) ? 0 : this.pvData.Length
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000281F File Offset: 0x00000A1F
		internal void SetFromNative(ref NATIVE_ESE_ICON_DESCRIPTION native)
		{
			this.pvData = new byte[native.ulSize];
			Marshal.Copy(native.pvData, this.pvData, 0, this.pvData.Length);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002850 File Offset: 0x00000A50
		internal NATIVE_ESE_ICON_DESCRIPTION GetNativeEseIconDescription()
		{
			int num = this.pvData.Length;
			IntPtr intPtr = IntPtr.Zero;
			if (this.pvData.Length > 0)
			{
				intPtr = Marshal.AllocHGlobal(num);
				Marshal.Copy(this.pvData, 0, intPtr, num);
			}
			return new NATIVE_ESE_ICON_DESCRIPTION
			{
				ulSize = (uint)num,
				pvData = intPtr
			};
		}
	}
}
