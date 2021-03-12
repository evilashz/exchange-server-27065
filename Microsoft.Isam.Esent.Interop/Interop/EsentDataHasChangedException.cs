using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F5 RID: 501
	[Serializable]
	public sealed class EsentDataHasChangedException : EsentObsoleteException
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x0001375E File Offset: 0x0001195E
		public EsentDataHasChangedException() : base("Data has changed, operation aborted", JET_err.DataHasChanged)
		{
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00013770 File Offset: 0x00011970
		private EsentDataHasChangedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
