using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000207 RID: 519
	[Serializable]
	public sealed class EsentEntryPointNotFoundException : EsentUsageException
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x00013956 File Offset: 0x00011B56
		public EsentEntryPointNotFoundException() : base("An entry point in a DLL we require could not be found", JET_err.EntryPointNotFound)
		{
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00013968 File Offset: 0x00011B68
		private EsentEntryPointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
