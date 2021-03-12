using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020010FC RID: 4348
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserNotFoundInPermissionEntryException : StoragePermanentException
	{
		// Token: 0x0600B3D5 RID: 46037 RVA: 0x0029BC66 File Offset: 0x00299E66
		public UserNotFoundInPermissionEntryException(string userId) : base(Strings.ErrorUserNotFoundInPermissionEntry(userId))
		{
			this.userId = userId;
		}

		// Token: 0x0600B3D6 RID: 46038 RVA: 0x0029BC7B File Offset: 0x00299E7B
		public UserNotFoundInPermissionEntryException(string userId, Exception innerException) : base(Strings.ErrorUserNotFoundInPermissionEntry(userId), innerException)
		{
			this.userId = userId;
		}

		// Token: 0x0600B3D7 RID: 46039 RVA: 0x0029BC91 File Offset: 0x00299E91
		protected UserNotFoundInPermissionEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x0600B3D8 RID: 46040 RVA: 0x0029BCBB File Offset: 0x00299EBB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x17003902 RID: 14594
		// (get) Token: 0x0600B3D9 RID: 46041 RVA: 0x0029BCD6 File Offset: 0x00299ED6
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x04006268 RID: 25192
		private readonly string userId;
	}
}
