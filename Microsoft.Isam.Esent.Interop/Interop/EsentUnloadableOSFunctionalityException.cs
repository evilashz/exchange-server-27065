using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000BD RID: 189
	[Serializable]
	public sealed class EsentUnloadableOSFunctionalityException : EsentFatalException
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x00011541 File Offset: 0x0000F741
		public EsentUnloadableOSFunctionalityException() : base("The desired OS functionality could not be located and loaded / linked.", JET_err.UnloadableOSFunctionality)
		{
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00011550 File Offset: 0x0000F750
		private EsentUnloadableOSFunctionalityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
