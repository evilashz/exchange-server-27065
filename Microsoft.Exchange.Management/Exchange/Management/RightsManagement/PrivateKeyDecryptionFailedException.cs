using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000719 RID: 1817
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class PrivateKeyDecryptionFailedException : Exception
	{
		// Token: 0x060040A9 RID: 16553 RVA: 0x0010871D File Offset: 0x0010691D
		public PrivateKeyDecryptionFailedException()
		{
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x00108725 File Offset: 0x00106925
		public PrivateKeyDecryptionFailedException(string message, Exception innerException = null) : base(message, innerException)
		{
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x0010872F File Offset: 0x0010692F
		protected PrivateKeyDecryptionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
