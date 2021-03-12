using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	public sealed class EsentIndexInUseException : EsentStateException
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x00012552 File Offset: 0x00010752
		public EsentIndexInUseException() : base("Index is in use", JET_err.IndexInUse)
		{
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00012564 File Offset: 0x00010764
		private EsentIndexInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
