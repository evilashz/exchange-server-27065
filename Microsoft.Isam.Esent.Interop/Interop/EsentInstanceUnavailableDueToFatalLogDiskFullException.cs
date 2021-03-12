using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000170 RID: 368
	[Serializable]
	public sealed class EsentInstanceUnavailableDueToFatalLogDiskFullException : EsentFatalException
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x000128D2 File Offset: 0x00010AD2
		public EsentInstanceUnavailableDueToFatalLogDiskFullException() : base("This instance cannot be used because it encountered a log-disk-full error performing an operation (likely transaction rollback) that could not tolerate failure", JET_err.InstanceUnavailableDueToFatalLogDiskFull)
		{
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000128E4 File Offset: 0x00010AE4
		private EsentInstanceUnavailableDueToFatalLogDiskFullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
