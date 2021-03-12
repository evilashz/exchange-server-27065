using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000174 RID: 372
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationUserAlreadyRemovedException : MigrationPermanentException
	{
		// Token: 0x060016A8 RID: 5800 RVA: 0x0006FB4F File Offset: 0x0006DD4F
		public MigrationUserAlreadyRemovedException(string user) : base(Strings.MigrationUserAlreadyRemoved(user))
		{
			this.user = user;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0006FB64 File Offset: 0x0006DD64
		public MigrationUserAlreadyRemovedException(string user, Exception innerException) : base(Strings.MigrationUserAlreadyRemoved(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0006FB7A File Offset: 0x0006DD7A
		protected MigrationUserAlreadyRemovedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0006FBA4 File Offset: 0x0006DDA4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0006FBBF File Offset: 0x0006DDBF
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04000B00 RID: 2816
		private readonly string user;
	}
}
