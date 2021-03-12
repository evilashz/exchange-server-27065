using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E1 RID: 225
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidUpgradeStatusException : MigrationTransientException
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x0000F7F2 File Offset: 0x0000D9F2
		public InvalidUpgradeStatusException(string id, UpgradeStatusTypes currentStatus) : base(UpgradeHandlerStrings.InvalidUpgradeStatus(id, currentStatus))
		{
			this.id = id;
			this.currentStatus = currentStatus;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000F80F File Offset: 0x0000DA0F
		public InvalidUpgradeStatusException(string id, UpgradeStatusTypes currentStatus, Exception innerException) : base(UpgradeHandlerStrings.InvalidUpgradeStatus(id, currentStatus), innerException)
		{
			this.id = id;
			this.currentStatus = currentStatus;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000F830 File Offset: 0x0000DA30
		protected InvalidUpgradeStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
			this.currentStatus = (UpgradeStatusTypes)info.GetValue("currentStatus", typeof(UpgradeStatusTypes));
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0000F885 File Offset: 0x0000DA85
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
			info.AddValue("currentStatus", this.currentStatus);
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0000F8B6 File Offset: 0x0000DAB6
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0000F8BE File Offset: 0x0000DABE
		public UpgradeStatusTypes CurrentStatus
		{
			get
			{
				return this.currentStatus;
			}
		}

		// Token: 0x0400038F RID: 911
		private readonly string id;

		// Token: 0x04000390 RID: 912
		private readonly UpgradeStatusTypes currentStatus;
	}
}
