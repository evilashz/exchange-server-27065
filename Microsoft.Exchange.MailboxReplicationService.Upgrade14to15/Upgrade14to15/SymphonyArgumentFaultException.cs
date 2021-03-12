using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E8 RID: 232
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SymphonyArgumentFaultException : MigrationTransientException
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x0000FBBC File Offset: 0x0000DDBC
		public SymphonyArgumentFaultException(string faultMessage) : base(UpgradeHandlerStrings.SymphonyArgumentFault(faultMessage))
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000FBD1 File Offset: 0x0000DDD1
		public SymphonyArgumentFaultException(string faultMessage, Exception innerException) : base(UpgradeHandlerStrings.SymphonyArgumentFault(faultMessage), innerException)
		{
			this.faultMessage = faultMessage;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000FBE7 File Offset: 0x0000DDE7
		protected SymphonyArgumentFaultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.faultMessage = (string)info.GetValue("faultMessage", typeof(string));
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000FC11 File Offset: 0x0000DE11
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("faultMessage", this.faultMessage);
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		public string FaultMessage
		{
			get
			{
				return this.faultMessage;
			}
		}

		// Token: 0x04000397 RID: 919
		private readonly string faultMessage;
	}
}
