using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	public sealed class EsentSessionInUseException : EsentUsageException
	{
		// Token: 0x06000A27 RID: 2599 RVA: 0x000139AA File Offset: 0x00011BAA
		public EsentSessionInUseException() : base("Tried to terminate session in use", JET_err.SessionInUse)
		{
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000139BC File Offset: 0x00011BBC
		private EsentSessionInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
