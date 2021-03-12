using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000DE RID: 222
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OrganizationNotFoundException : MigrationPermanentException
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x0000F589 File Offset: 0x0000D789
		public OrganizationNotFoundException(string org) : base(UpgradeHandlerStrings.OrganizationNotFound(org))
		{
			this.org = org;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0000F59E File Offset: 0x0000D79E
		public OrganizationNotFoundException(string org, Exception innerException) : base(UpgradeHandlerStrings.OrganizationNotFound(org), innerException)
		{
			this.org = org;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		protected OrganizationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.org = (string)info.GetValue("org", typeof(string));
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000F5DE File Offset: 0x0000D7DE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("org", this.org);
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0000F5F9 File Offset: 0x0000D7F9
		public string Org
		{
			get
			{
				return this.org;
			}
		}

		// Token: 0x04000389 RID: 905
		private readonly string org;
	}
}
