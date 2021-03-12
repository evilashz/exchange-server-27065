using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000210 RID: 528
	[Serializable]
	public sealed class EsentCallbackNotResolvedException : EsentUsageException
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x00013A52 File Offset: 0x00011C52
		public EsentCallbackNotResolvedException() : base("A callback function could not be found", JET_err.CallbackNotResolved)
		{
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00013A64 File Offset: 0x00011C64
		private EsentCallbackNotResolvedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
