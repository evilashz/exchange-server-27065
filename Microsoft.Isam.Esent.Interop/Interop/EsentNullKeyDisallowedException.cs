using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000152 RID: 338
	[Serializable]
	public sealed class EsentNullKeyDisallowedException : EsentUsageException
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x0001258A File Offset: 0x0001078A
		public EsentNullKeyDisallowedException() : base("Null keys are disallowed on index", JET_err.NullKeyDisallowed)
		{
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001259C File Offset: 0x0001079C
		private EsentNullKeyDisallowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
