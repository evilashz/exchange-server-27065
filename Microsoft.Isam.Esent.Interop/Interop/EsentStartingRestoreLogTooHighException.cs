using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000103 RID: 259
	[Serializable]
	public sealed class EsentStartingRestoreLogTooHighException : EsentInconsistentException
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x00011CE6 File Offset: 0x0000FEE6
		public EsentStartingRestoreLogTooHighException() : base("The starting log number too high for the restore", JET_err.StartingRestoreLogTooHigh)
		{
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00011CF8 File Offset: 0x0000FEF8
		private EsentStartingRestoreLogTooHighException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
