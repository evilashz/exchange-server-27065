using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	public sealed class EsentDatabase500FormatException : EsentObsoleteException
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x00012D16 File Offset: 0x00010F16
		public EsentDatabase500FormatException() : base("The database is in an older (500) format", JET_err.Database500Format)
		{
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00012D28 File Offset: 0x00010F28
		private EsentDatabase500FormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
