using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000BC RID: 188
	[Serializable]
	public sealed class EsentDisabledFunctionalityException : EsentUsageException
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00011528 File Offset: 0x0000F728
		public EsentDisabledFunctionalityException() : base("You are running MinESE, that does not have all features compiled in.  This functionality is only supported in a full version of ESE.", JET_err.DisabledFunctionality)
		{
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00011537 File Offset: 0x0000F737
		private EsentDisabledFunctionalityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
