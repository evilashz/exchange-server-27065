using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D2 RID: 210
	[Serializable]
	public sealed class EsentInvalidPrereadException : EsentUsageException
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0001178A File Offset: 0x0000F98A
		public EsentInvalidPrereadException() : base("Cannot preread long values when current index secondary", JET_err.InvalidPreread)
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001179C File Offset: 0x0000F99C
		private EsentInvalidPrereadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
