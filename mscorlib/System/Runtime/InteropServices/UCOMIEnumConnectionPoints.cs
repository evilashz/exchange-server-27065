﻿using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000956 RID: 2390
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumConnectionPoints instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumConnectionPoints
	{
		// Token: 0x06006177 RID: 24951
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] UCOMIConnectionPoint[] rgelt, out int pceltFetched);

		// Token: 0x06006178 RID: 24952
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006179 RID: 24953
		[PreserveSig]
		int Reset();

		// Token: 0x0600617A RID: 24954
		void Clone(out UCOMIEnumConnectionPoints ppenum);
	}
}
