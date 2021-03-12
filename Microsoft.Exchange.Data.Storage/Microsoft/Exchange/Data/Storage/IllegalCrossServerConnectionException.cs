using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000735 RID: 1845
	[Serializable]
	public class IllegalCrossServerConnectionException : WrongServerException
	{
		// Token: 0x060047EE RID: 18414 RVA: 0x0013078E File Offset: 0x0012E98E
		public IllegalCrossServerConnectionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x00130797 File Offset: 0x0012E997
		public IllegalCrossServerConnectionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x001307A1 File Offset: 0x0012E9A1
		protected IllegalCrossServerConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
