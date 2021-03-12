using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Misc
{
	// Token: 0x02000020 RID: 32
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("22F55882-280B-11D0-A8A9-00A0C90C2004")]
	internal interface IPropertyBag2
	{
		// Token: 0x060000F1 RID: 241
		void Read([In] int cProperties, [In] ref PROPBAG2 pPropBag, [MarshalAs(UnmanagedType.Interface)] [In] IErrorLog pErrLog, [MarshalAs(UnmanagedType.Struct)] out object pvarValue, [MarshalAs(UnmanagedType.Error)] [In] [Out] ref int phrError);

		// Token: 0x060000F2 RID: 242
		void Write([In] int cProperties, [In] ref PROPBAG2 pPropBag, [MarshalAs(UnmanagedType.Struct)] [In] ref object pvarValue);

		// Token: 0x060000F3 RID: 243
		void CountProperties(out int pcProperties);

		// Token: 0x060000F4 RID: 244
		void GetPropertyInfo([In] int iProperty, [In] int cProperties, out PROPBAG2 pPropBag, out int pcProperties);

		// Token: 0x060000F5 RID: 245
		void LoadObject([MarshalAs(UnmanagedType.LPWStr)] [In] string pstrName, [In] int dwHint, [MarshalAs(UnmanagedType.IUnknown)] [In] object pUnkObject, [MarshalAs(UnmanagedType.Interface)] [In] IErrorLog pErrLog);
	}
}
