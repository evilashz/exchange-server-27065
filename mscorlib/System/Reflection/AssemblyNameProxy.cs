using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200059B RID: 1435
	[ComVisible(true)]
	public class AssemblyNameProxy : MarshalByRefObject
	{
		// Token: 0x06004393 RID: 17299 RVA: 0x000F83CA File Offset: 0x000F65CA
		public AssemblyName GetAssemblyName(string assemblyFile)
		{
			return AssemblyName.GetAssemblyName(assemblyFile);
		}
	}
}
