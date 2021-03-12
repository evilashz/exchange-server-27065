using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E3 RID: 227
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnsupportedUpgradeRequestTypeException : MigrationTransientException
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x0000F9F1 File Offset: 0x0000DBF1
		public UnsupportedUpgradeRequestTypeException(UpgradeRequestTypes upgradeRequest) : base(UpgradeHandlerStrings.UnsupportedUpgradeRequestType(upgradeRequest))
		{
			this.upgradeRequest = upgradeRequest;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000FA06 File Offset: 0x0000DC06
		public UnsupportedUpgradeRequestTypeException(UpgradeRequestTypes upgradeRequest, Exception innerException) : base(UpgradeHandlerStrings.UnsupportedUpgradeRequestType(upgradeRequest), innerException)
		{
			this.upgradeRequest = upgradeRequest;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000FA1C File Offset: 0x0000DC1C
		protected UnsupportedUpgradeRequestTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.upgradeRequest = (UpgradeRequestTypes)info.GetValue("upgradeRequest", typeof(UpgradeRequestTypes));
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000FA46 File Offset: 0x0000DC46
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("upgradeRequest", this.upgradeRequest);
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0000FA66 File Offset: 0x0000DC66
		public UpgradeRequestTypes UpgradeRequest
		{
			get
			{
				return this.upgradeRequest;
			}
		}

		// Token: 0x04000394 RID: 916
		private readonly UpgradeRequestTypes upgradeRequest;
	}
}
