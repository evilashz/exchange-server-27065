using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200021B RID: 539
	[Serializable]
	public sealed class EsentLSNotSetException : EsentStateException
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x00013B86 File Offset: 0x00011D86
		public EsentLSNotSetException() : base("Attempted to retrieve Local Storage from an object which didn't have it set", JET_err.LSNotSet)
		{
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00013B98 File Offset: 0x00011D98
		private EsentLSNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
