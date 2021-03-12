using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200087F RID: 2175
	public static class RuntimeFeature
	{
		// Token: 0x06005C7A RID: 23674 RVA: 0x001448E5 File Offset: 0x00142AE5
		public static bool IsSupported(string feature)
		{
			return feature == "PortablePdb" && !AppContextSwitches.IgnorePortablePDBsInStackTraces;
		}

		// Token: 0x04002955 RID: 10581
		public const string PortablePdb = "PortablePdb";
	}
}
