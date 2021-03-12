using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200000D RID: 13
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuditLogAccessDeniedException : AuditLogException
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00003746 File Offset: 0x00001946
		public AuditLogAccessDeniedException() : base(Strings.AuditLogAccessDenied)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003753 File Offset: 0x00001953
		public AuditLogAccessDeniedException(Exception innerException) : base(Strings.AuditLogAccessDenied, innerException)
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003761 File Offset: 0x00001961
		protected AuditLogAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000376B File Offset: 0x0000196B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
