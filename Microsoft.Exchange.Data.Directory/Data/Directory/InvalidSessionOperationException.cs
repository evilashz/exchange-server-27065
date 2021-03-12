using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AA2 RID: 2722
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidSessionOperationException : ADOperationException
	{
		// Token: 0x06007FF5 RID: 32757 RVA: 0x001A4AFE File Offset: 0x001A2CFE
		public InvalidSessionOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007FF6 RID: 32758 RVA: 0x001A4B07 File Offset: 0x001A2D07
		public InvalidSessionOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007FF7 RID: 32759 RVA: 0x001A4B11 File Offset: 0x001A2D11
		protected InvalidSessionOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FF8 RID: 32760 RVA: 0x001A4B1B File Offset: 0x001A2D1B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
