using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000943 RID: 2371
	[Guid("F1C3BF77-C3E4-11d3-88E7-00902754C43A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibExporterNotifySink
	{
		// Token: 0x0600611A RID: 24858
		void ReportEvent(ExporterEventKind eventKind, int eventCode, string eventMsg);

		// Token: 0x0600611B RID: 24859
		[return: MarshalAs(UnmanagedType.Interface)]
		object ResolveRef(Assembly assembly);
	}
}
