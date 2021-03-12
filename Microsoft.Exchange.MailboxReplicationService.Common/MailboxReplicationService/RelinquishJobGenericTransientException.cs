using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000307 RID: 775
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobGenericTransientException : RelinquishJobTransientException
	{
		// Token: 0x060024DA RID: 9434 RVA: 0x0005092C File Offset: 0x0004EB2C
		public RelinquishJobGenericTransientException() : base(MrsStrings.JobHasBeenRelinquished)
		{
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x00050939 File Offset: 0x0004EB39
		public RelinquishJobGenericTransientException(Exception innerException) : base(MrsStrings.JobHasBeenRelinquished, innerException)
		{
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x00050947 File Offset: 0x0004EB47
		protected RelinquishJobGenericTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00050951 File Offset: 0x0004EB51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
