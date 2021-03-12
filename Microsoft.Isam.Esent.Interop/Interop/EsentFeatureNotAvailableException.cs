using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200012B RID: 299
	[Serializable]
	public sealed class EsentFeatureNotAvailableException : EsentUsageException
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x00012146 File Offset: 0x00010346
		public EsentFeatureNotAvailableException() : base("API not supported", JET_err.FeatureNotAvailable)
		{
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00012158 File Offset: 0x00010358
		private EsentFeatureNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
