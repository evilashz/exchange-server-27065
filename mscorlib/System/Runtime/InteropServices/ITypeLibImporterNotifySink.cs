using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000942 RID: 2370
	[Guid("F1C3BF76-C3E4-11d3-88E7-00902754C43A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibImporterNotifySink
	{
		// Token: 0x06006118 RID: 24856
		void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg);

		// Token: 0x06006119 RID: 24857
		Assembly ResolveRef([MarshalAs(UnmanagedType.Interface)] object typeLib);
	}
}
