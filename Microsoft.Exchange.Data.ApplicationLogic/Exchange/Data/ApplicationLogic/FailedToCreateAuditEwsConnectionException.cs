using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200000A RID: 10
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToCreateAuditEwsConnectionException : AuditLogException
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003587 File Offset: 0x00001787
		public FailedToCreateAuditEwsConnectionException() : base(Strings.FailedToCreateAuditEwsConnection)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003594 File Offset: 0x00001794
		public FailedToCreateAuditEwsConnectionException(Exception innerException) : base(Strings.FailedToCreateAuditEwsConnection, innerException)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000035A2 File Offset: 0x000017A2
		protected FailedToCreateAuditEwsConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000035AC File Offset: 0x000017AC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
