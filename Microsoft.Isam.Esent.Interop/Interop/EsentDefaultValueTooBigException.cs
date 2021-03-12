using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001E6 RID: 486
	[Serializable]
	public sealed class EsentDefaultValueTooBigException : EsentUsageException
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x000135BA File Offset: 0x000117BA
		public EsentDefaultValueTooBigException() : base("Default value exceeds maximum size", JET_err.DefaultValueTooBig)
		{
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000135CC File Offset: 0x000117CC
		private EsentDefaultValueTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
