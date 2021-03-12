using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000ED RID: 237
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidOrganizationVersionException : MigrationPermanentException
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x0000FFBF File Offset: 0x0000E1BF
		public InvalidOrganizationVersionException(string org, ExchangeObjectVersion version) : base(UpgradeHandlerStrings.InvalidOrganizationVersion(org, version))
		{
			this.org = org;
			this.version = version;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		public InvalidOrganizationVersionException(string org, ExchangeObjectVersion version, Exception innerException) : base(UpgradeHandlerStrings.InvalidOrganizationVersion(org, version), innerException)
		{
			this.org = org;
			this.version = version;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		protected InvalidOrganizationVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.org = (string)info.GetValue("org", typeof(string));
			this.version = (ExchangeObjectVersion)info.GetValue("version", typeof(ExchangeObjectVersion));
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00010051 File Offset: 0x0000E251
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("org", this.org);
			info.AddValue("version", this.version);
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001007D File Offset: 0x0000E27D
		public string Org
		{
			get
			{
				return this.org;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00010085 File Offset: 0x0000E285
		public ExchangeObjectVersion Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x040003A1 RID: 929
		private readonly string org;

		// Token: 0x040003A2 RID: 930
		private readonly ExchangeObjectVersion version;
	}
}
