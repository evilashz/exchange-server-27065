using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	public sealed class EsentIndexMustStayException : EsentUsageException
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x000131E6 File Offset: 0x000113E6
		public EsentIndexMustStayException() : base("Cannot delete clustered index", JET_err.IndexMustStay)
		{
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000131F8 File Offset: 0x000113F8
		private EsentIndexMustStayException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
