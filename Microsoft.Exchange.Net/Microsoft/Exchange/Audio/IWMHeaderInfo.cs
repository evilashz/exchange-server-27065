using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000630 RID: 1584
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("96406BDA-2B2B-11d3-B36B-00C04F6108FF")]
	[ComImport]
	internal interface IWMHeaderInfo
	{
		// Token: 0x06001CC3 RID: 7363
		void GetAttributeCount();

		// Token: 0x06001CC4 RID: 7364
		void GetAttributeByIndex();

		// Token: 0x06001CC5 RID: 7365
		void GetAttributeByName([In] [Out] ref ushort pwStreamNum, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszName, out WindowsMediaNativeMethods.WMT_ATTR_DATATYPE pType, ref ulong pValue, [In] [Out] ref ushort pcbLength);

		// Token: 0x06001CC6 RID: 7366
		void SetAttribute();

		// Token: 0x06001CC7 RID: 7367
		void GetMarkerCount();

		// Token: 0x06001CC8 RID: 7368
		void GetMarker();

		// Token: 0x06001CC9 RID: 7369
		void AddMarker();

		// Token: 0x06001CCA RID: 7370
		void RemoveMarker();

		// Token: 0x06001CCB RID: 7371
		void GetScriptCount();

		// Token: 0x06001CCC RID: 7372
		void GetScript();

		// Token: 0x06001CCD RID: 7373
		void AddScript();

		// Token: 0x06001CCE RID: 7374
		void RemoveScript();
	}
}
