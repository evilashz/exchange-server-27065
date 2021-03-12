using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000159 RID: 345
	[Serializable]
	public sealed class EsentInvalidLCMapStringFlagsException : EsentUsageException
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x0001264E File Offset: 0x0001084E
		public EsentInvalidLCMapStringFlagsException() : base("Invalid flags for LCMapString()", JET_err.InvalidLCMapStringFlags)
		{
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00012660 File Offset: 0x00010860
		private EsentInvalidLCMapStringFlagsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
