using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014B RID: 331
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationSubscriptionCreationFailedException : MigrationPermanentException
	{
		// Token: 0x060015E1 RID: 5601 RVA: 0x0006E95B File Offset: 0x0006CB5B
		public MigrationSubscriptionCreationFailedException(string user) : base(Strings.MigrationSubscriptionCreationFailed(user))
		{
			this.user = user;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0006E970 File Offset: 0x0006CB70
		public MigrationSubscriptionCreationFailedException(string user, Exception innerException) : base(Strings.MigrationSubscriptionCreationFailed(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0006E986 File Offset: 0x0006CB86
		protected MigrationSubscriptionCreationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0006E9B0 File Offset: 0x0006CBB0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0006E9CB File Offset: 0x0006CBCB
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04000ADD RID: 2781
		private readonly string user;
	}
}
