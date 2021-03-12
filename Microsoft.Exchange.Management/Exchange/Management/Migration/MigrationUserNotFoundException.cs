using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001112 RID: 4370
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MigrationUserNotFoundException : LocalizedException
	{
		// Token: 0x0600B448 RID: 46152 RVA: 0x0029C891 File Offset: 0x0029AA91
		public MigrationUserNotFoundException(string userName) : base(Strings.MigrationUserNotFound(userName))
		{
			this.userName = userName;
		}

		// Token: 0x0600B449 RID: 46153 RVA: 0x0029C8A6 File Offset: 0x0029AAA6
		public MigrationUserNotFoundException(string userName, Exception innerException) : base(Strings.MigrationUserNotFound(userName), innerException)
		{
			this.userName = userName;
		}

		// Token: 0x0600B44A RID: 46154 RVA: 0x0029C8BC File Offset: 0x0029AABC
		protected MigrationUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
		}

		// Token: 0x0600B44B RID: 46155 RVA: 0x0029C8E6 File Offset: 0x0029AAE6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
		}

		// Token: 0x1700391D RID: 14621
		// (get) Token: 0x0600B44C RID: 46156 RVA: 0x0029C901 File Offset: 0x0029AB01
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x04006283 RID: 25219
		private readonly string userName;
	}
}
