using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000196 RID: 406
	[Serializable]
	public sealed class EsentDatabase400FormatException : EsentObsoleteException
	{
		// Token: 0x0600093F RID: 2367 RVA: 0x00012CFA File Offset: 0x00010EFA
		public EsentDatabase400FormatException() : base("The database is in an older (400) format", JET_err.Database400Format)
		{
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00012D0C File Offset: 0x00010F0C
		private EsentDatabase400FormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
