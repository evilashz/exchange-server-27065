using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E7 RID: 231
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SymphonyAccessDeniedFaultException : MigrationTransientException
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public SymphonyAccessDeniedFaultException(string faultMessage) : base(UpgradeHandlerStrings.SymphonyAccessDeniedFault(faultMessage))
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000FB59 File Offset: 0x0000DD59
		public SymphonyAccessDeniedFaultException(string faultMessage, Exception innerException) : base(UpgradeHandlerStrings.SymphonyAccessDeniedFault(faultMessage), innerException)
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000FB6F File Offset: 0x0000DD6F
		protected SymphonyAccessDeniedFaultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.faultMessage = (string)info.GetValue("faultMessage", typeof(string));
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000FB99 File Offset: 0x0000DD99
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("faultMessage", this.faultMessage);
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0000FBB4 File Offset: 0x0000DDB4
		public string FaultMessage
		{
			get
			{
				return this.faultMessage;
			}
		}

		// Token: 0x04000396 RID: 918
		private readonly string faultMessage;
	}
}
