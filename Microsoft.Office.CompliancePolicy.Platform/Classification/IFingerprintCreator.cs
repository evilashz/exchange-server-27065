using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x02000029 RID: 41
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeLibType(TypeLibTypeFlags.FNonExtensible)]
	[Guid("67F2E67D-1231-4F95-A1ED-D31364A5862C")]
	[ComImport]
	public interface IFingerprintCreator
	{
		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		uint GetBase64EncodedFingerprint([MarshalAs(UnmanagedType.BStr)] [In] string strText, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] out byte[] fingerprint);
	}
}
