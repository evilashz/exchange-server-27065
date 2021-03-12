using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000EA RID: 234
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SymphonyInvalidOperationFaultException : MigrationTransientException
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		public SymphonyInvalidOperationFaultException(string faultMessage) : base(UpgradeHandlerStrings.SymphonyInvalidOperationFault(faultMessage))
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000FCC1 File Offset: 0x0000DEC1
		public SymphonyInvalidOperationFaultException(string faultMessage, Exception innerException) : base(UpgradeHandlerStrings.SymphonyInvalidOperationFault(faultMessage), innerException)
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000FCD7 File Offset: 0x0000DED7
		protected SymphonyInvalidOperationFaultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.faultMessage = (string)info.GetValue("faultMessage", typeof(string));
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000FD01 File Offset: 0x0000DF01
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("faultMessage", this.faultMessage);
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		public string FaultMessage
		{
			get
			{
				return this.faultMessage;
			}
		}

		// Token: 0x04000399 RID: 921
		private readonly string faultMessage;
	}
}
