using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200006A RID: 106
	public class LOGSHIP_INFO
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000BF32 File Offset: 0x0000A132
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0000BF3A File Offset: 0x0000A13A
		public LogshipType ulType { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000BF43 File Offset: 0x0000A143
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0000BF4B File Offset: 0x0000A14B
		public string wszName { get; set; }

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000BF54 File Offset: 0x0000A154
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "LOGSHIP_INFO({0})", new object[]
			{
				this.wszName
			});
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000BF81 File Offset: 0x0000A181
		internal void SetFromNative(ref NATIVE_LOGSHIP_INFO native)
		{
			this.ulType = (LogshipType)native.ulType;
			this.wszName = Marshal.PtrToStringUni(native.wszName);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		internal NATIVE_LOGSHIP_INFO GetNativeLogshipInfo()
		{
			return new NATIVE_LOGSHIP_INFO
			{
				ulType = (uint)this.ulType,
				cchName = (uint)(this.wszName.Length + 1),
				wszName = Marshal.StringToHGlobalUni(this.wszName)
			};
		}
	}
}
