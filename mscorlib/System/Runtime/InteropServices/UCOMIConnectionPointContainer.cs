using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094F RID: 2383
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IConnectionPointContainer instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIConnectionPointContainer
	{
		// Token: 0x06006164 RID: 24932
		void EnumConnectionPoints(out UCOMIEnumConnectionPoints ppEnum);

		// Token: 0x06006165 RID: 24933
		void FindConnectionPoint(ref Guid riid, out UCOMIConnectionPoint ppCP);
	}
}
