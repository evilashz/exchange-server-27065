using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	public sealed class EsentEndingRestoreLogTooLowException : EsentInconsistentException
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x00011CCA File Offset: 0x0000FECA
		public EsentEndingRestoreLogTooLowException() : base("The starting log number too low for the restore", JET_err.EndingRestoreLogTooLow)
		{
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00011CDC File Offset: 0x0000FEDC
		private EsentEndingRestoreLogTooLowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
