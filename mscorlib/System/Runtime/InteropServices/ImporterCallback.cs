using System;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000924 RID: 2340
	internal class ImporterCallback : ITypeLibImporterNotifySink
	{
		// Token: 0x0600608D RID: 24717 RVA: 0x001499B1 File Offset: 0x00147BB1
		public void ReportEvent(ImporterEventKind EventKind, int EventCode, string EventMsg)
		{
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x001499B4 File Offset: 0x00147BB4
		[SecuritySafeCritical]
		public Assembly ResolveRef(object TypeLib)
		{
			Assembly result;
			try
			{
				ITypeLibConverter typeLibConverter = new TypeLibConverter();
				result = typeLibConverter.ConvertTypeLibToAssembly(TypeLib, Marshal.GetTypeLibName((ITypeLib)TypeLib) + ".dll", TypeLibImporterFlags.None, new ImporterCallback(), null, null, null, null);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
	}
}
