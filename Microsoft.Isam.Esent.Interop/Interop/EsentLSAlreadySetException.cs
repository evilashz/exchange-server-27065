using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200021A RID: 538
	[Serializable]
	public sealed class EsentLSAlreadySetException : EsentUsageException
	{
		// Token: 0x06000A47 RID: 2631 RVA: 0x00013B6A File Offset: 0x00011D6A
		public EsentLSAlreadySetException() : base("Attempted to set Local Storage for an object which already had it set", JET_err.LSAlreadySet)
		{
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00013B7C File Offset: 0x00011D7C
		private EsentLSAlreadySetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
