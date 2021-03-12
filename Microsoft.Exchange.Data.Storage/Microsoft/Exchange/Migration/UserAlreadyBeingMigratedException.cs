using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000175 RID: 373
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserAlreadyBeingMigratedException : MigrationPermanentException
	{
		// Token: 0x060016AD RID: 5805 RVA: 0x0006FBC7 File Offset: 0x0006DDC7
		public UserAlreadyBeingMigratedException(string identity) : base(Strings.ErrorUserAlreadyBeingMigrated(identity))
		{
			this.identity = identity;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0006FBDC File Offset: 0x0006DDDC
		public UserAlreadyBeingMigratedException(string identity, Exception innerException) : base(Strings.ErrorUserAlreadyBeingMigrated(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0006FBF2 File Offset: 0x0006DDF2
		protected UserAlreadyBeingMigratedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0006FC1C File Offset: 0x0006DE1C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0006FC37 File Offset: 0x0006DE37
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04000B01 RID: 2817
		private readonly string identity;
	}
}
