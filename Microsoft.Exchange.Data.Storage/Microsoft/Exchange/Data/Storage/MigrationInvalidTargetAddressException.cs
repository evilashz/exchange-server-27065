using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200010E RID: 270
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationInvalidTargetAddressException : MigrationPermanentException
	{
		// Token: 0x060013D5 RID: 5077 RVA: 0x00069D56 File Offset: 0x00067F56
		public MigrationInvalidTargetAddressException(string email) : base(ServerStrings.MigrationInvalidTargetAddress(email))
		{
			this.email = email;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00069D6B File Offset: 0x00067F6B
		public MigrationInvalidTargetAddressException(string email, Exception innerException) : base(ServerStrings.MigrationInvalidTargetAddress(email), innerException)
		{
			this.email = email;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00069D81 File Offset: 0x00067F81
		protected MigrationInvalidTargetAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.email = (string)info.GetValue("email", typeof(string));
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00069DAB File Offset: 0x00067FAB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("email", this.email);
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00069DC6 File Offset: 0x00067FC6
		public string Email
		{
			get
			{
				return this.email;
			}
		}

		// Token: 0x0400099C RID: 2460
		private readonly string email;
	}
}
