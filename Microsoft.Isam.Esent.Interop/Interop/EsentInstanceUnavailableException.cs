using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200016E RID: 366
	[Serializable]
	public sealed class EsentInstanceUnavailableException : EsentFatalException
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x0001289A File Offset: 0x00010A9A
		public EsentInstanceUnavailableException() : base("This instance cannot be used because it encountered a fatal error", JET_err.InstanceUnavailable)
		{
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000128AC File Offset: 0x00010AAC
		private EsentInstanceUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
