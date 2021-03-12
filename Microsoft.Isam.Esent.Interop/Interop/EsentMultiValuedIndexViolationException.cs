using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C7 RID: 455
	[Serializable]
	public sealed class EsentMultiValuedIndexViolationException : EsentUsageException
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x00013256 File Offset: 0x00011456
		public EsentMultiValuedIndexViolationException() : base("Non-unique inter-record index keys generated for a multivalued index", JET_err.MultiValuedIndexViolation)
		{
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00013268 File Offset: 0x00011468
		private EsentMultiValuedIndexViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
