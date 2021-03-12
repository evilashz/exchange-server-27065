using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	public sealed class EsentDTCCallbackUnexpectedErrorException : EsentObsoleteException
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x00012BC6 File Offset: 0x00010DC6
		public EsentDTCCallbackUnexpectedErrorException() : base("Unexpected error code returned from DTC callback", JET_err.DTCCallbackUnexpectedError)
		{
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00012BD8 File Offset: 0x00010DD8
		private EsentDTCCallbackUnexpectedErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
