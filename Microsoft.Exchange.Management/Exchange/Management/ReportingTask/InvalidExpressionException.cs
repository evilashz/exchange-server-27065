using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x02001166 RID: 4454
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidExpressionException : ReportingException
	{
		// Token: 0x0600B5EC RID: 46572 RVA: 0x0029F0D6 File Offset: 0x0029D2D6
		public InvalidExpressionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B5ED RID: 46573 RVA: 0x0029F0DF File Offset: 0x0029D2DF
		public InvalidExpressionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B5EE RID: 46574 RVA: 0x0029F0E9 File Offset: 0x0029D2E9
		protected InvalidExpressionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B5EF RID: 46575 RVA: 0x0029F0F3 File Offset: 0x0029D2F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
