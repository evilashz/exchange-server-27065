using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000E0 RID: 224
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserNotFoundException : MigrationPermanentException
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x0000F77A File Offset: 0x0000D97A
		public UserNotFoundException(string org) : base(UpgradeHandlerStrings.UserNotFound(org))
		{
			this.org = org;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000F78F File Offset: 0x0000D98F
		public UserNotFoundException(string org, Exception innerException) : base(UpgradeHandlerStrings.UserNotFound(org), innerException)
		{
			this.org = org;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0000F7A5 File Offset: 0x0000D9A5
		protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.org = (string)info.GetValue("org", typeof(string));
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0000F7CF File Offset: 0x0000D9CF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("org", this.org);
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0000F7EA File Offset: 0x0000D9EA
		public string Org
		{
			get
			{
				return this.org;
			}
		}

		// Token: 0x0400038E RID: 910
		private readonly string org;
	}
}
