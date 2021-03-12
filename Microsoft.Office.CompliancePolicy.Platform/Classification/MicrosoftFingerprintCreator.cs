using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200002B RID: 43
	[Guid("7D3549FB-E229-4AE0-A0C6-C381EF9F5858")]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComImport]
	public class MicrosoftFingerprintCreator : IMicrosoftFingerprintCreator, IFingerprintCreator
	{
		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern uint GetBase64EncodedFingerprint([MarshalAs(UnmanagedType.BStr)] [In] string strText, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] out byte[] fingerprint);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern MicrosoftFingerprintCreator();
	}
}
