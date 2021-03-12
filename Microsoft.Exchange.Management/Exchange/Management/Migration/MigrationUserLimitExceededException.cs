using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200112B RID: 4395
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationUserLimitExceededException : LocalizedException
	{
		// Token: 0x0600B4C6 RID: 46278 RVA: 0x0029D507 File Offset: 0x0029B707
		public MigrationUserLimitExceededException(int count) : base(Strings.MigrationUserLimitExceeded(count))
		{
			this.count = count;
		}

		// Token: 0x0600B4C7 RID: 46279 RVA: 0x0029D51C File Offset: 0x0029B71C
		public MigrationUserLimitExceededException(int count, Exception innerException) : base(Strings.MigrationUserLimitExceeded(count), innerException)
		{
			this.count = count;
		}

		// Token: 0x0600B4C8 RID: 46280 RVA: 0x0029D532 File Offset: 0x0029B732
		protected MigrationUserLimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.count = (int)info.GetValue("count", typeof(int));
		}

		// Token: 0x0600B4C9 RID: 46281 RVA: 0x0029D55C File Offset: 0x0029B75C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("count", this.count);
		}

		// Token: 0x17003937 RID: 14647
		// (get) Token: 0x0600B4CA RID: 46282 RVA: 0x0029D577 File Offset: 0x0029B777
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0400629D RID: 25245
		private readonly int count;
	}
}
