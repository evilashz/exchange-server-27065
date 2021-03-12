using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E5 RID: 485
	[Serializable]
	public sealed class EsentCannotBeTaggedException : EsentUsageException
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x0001359E File Offset: 0x0001179E
		public EsentCannotBeTaggedException() : base("AutoIncrement and Version cannot be tagged", JET_err.CannotBeTagged)
		{
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000135B0 File Offset: 0x000117B0
		private EsentCannotBeTaggedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
