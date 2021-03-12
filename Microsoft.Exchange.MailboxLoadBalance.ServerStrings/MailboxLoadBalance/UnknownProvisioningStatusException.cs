using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200001C RID: 28
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnknownProvisioningStatusException : MailboxLoadBalancePermanentException
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003024 File Offset: 0x00001224
		public UnknownProvisioningStatusException() : base(MigrationWorkflowServiceStrings.ErrorUnknownProvisioningStatus)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003031 File Offset: 0x00001231
		public UnknownProvisioningStatusException(Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorUnknownProvisioningStatus, innerException)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000303F File Offset: 0x0000123F
		protected UnknownProvisioningStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003049 File Offset: 0x00001249
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
