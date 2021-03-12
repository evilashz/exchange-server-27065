using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	public sealed class EsentInvalidParameterException : EsentUsageException
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x0001217E File Offset: 0x0001037E
		public EsentInvalidParameterException() : base("Invalid API parameter", JET_err.InvalidParameter)
		{
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00012190 File Offset: 0x00010390
		private EsentInvalidParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
