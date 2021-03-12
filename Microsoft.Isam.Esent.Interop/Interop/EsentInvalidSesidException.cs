using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000174 RID: 372
	[Serializable]
	public sealed class EsentInvalidSesidException : EsentUsageException
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x00012942 File Offset: 0x00010B42
		public EsentInvalidSesidException() : base("Invalid session handle", JET_err.InvalidSesid)
		{
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00012954 File Offset: 0x00010B54
		private EsentInvalidSesidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
