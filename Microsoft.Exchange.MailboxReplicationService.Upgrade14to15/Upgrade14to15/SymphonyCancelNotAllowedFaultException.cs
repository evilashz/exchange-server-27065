using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E9 RID: 233
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SymphonyCancelNotAllowedFaultException : MigrationTransientException
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x0000FC34 File Offset: 0x0000DE34
		public SymphonyCancelNotAllowedFaultException(string faultMessage) : base(UpgradeHandlerStrings.SymphonyCancelNotAllowedFault(faultMessage))
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000FC49 File Offset: 0x0000DE49
		public SymphonyCancelNotAllowedFaultException(string faultMessage, Exception innerException) : base(UpgradeHandlerStrings.SymphonyCancelNotAllowedFault(faultMessage), innerException)
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000FC5F File Offset: 0x0000DE5F
		protected SymphonyCancelNotAllowedFaultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.faultMessage = (string)info.GetValue("faultMessage", typeof(string));
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000FC89 File Offset: 0x0000DE89
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("faultMessage", this.faultMessage);
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		public string FaultMessage
		{
			get
			{
				return this.faultMessage;
			}
		}

		// Token: 0x04000398 RID: 920
		private readonly string faultMessage;
	}
}
