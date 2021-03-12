using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200016D RID: 365
	[Serializable]
	public sealed class EsentSystemParameterConflictException : EsentUsageException
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x0001287E File Offset: 0x00010A7E
		public EsentSystemParameterConflictException() : base("Global system parameters have already been set, but to a conflicting or disagreeable state to the specified values.", JET_err.SystemParameterConflict)
		{
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00012890 File Offset: 0x00010A90
		private EsentSystemParameterConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
