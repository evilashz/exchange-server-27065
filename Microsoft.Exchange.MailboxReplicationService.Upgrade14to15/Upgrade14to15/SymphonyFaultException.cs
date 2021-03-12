using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000EB RID: 235
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SymphonyFaultException : MigrationTransientException
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x0000FD24 File Offset: 0x0000DF24
		public SymphonyFaultException(string faultMessage) : base(UpgradeHandlerStrings.SymphonyFault(faultMessage))
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000FD39 File Offset: 0x0000DF39
		public SymphonyFaultException(string faultMessage, Exception innerException) : base(UpgradeHandlerStrings.SymphonyFault(faultMessage), innerException)
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000FD4F File Offset: 0x0000DF4F
		protected SymphonyFaultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.faultMessage = (string)info.GetValue("faultMessage", typeof(string));
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000FD79 File Offset: 0x0000DF79
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("faultMessage", this.faultMessage);
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0000FD94 File Offset: 0x0000DF94
		public string FaultMessage
		{
			get
			{
				return this.faultMessage;
			}
		}

		// Token: 0x0400039A RID: 922
		private readonly string faultMessage;
	}
}
