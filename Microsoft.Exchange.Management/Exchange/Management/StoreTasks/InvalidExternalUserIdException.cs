using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020010FA RID: 4346
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidExternalUserIdException : StoragePermanentException
	{
		// Token: 0x0600B3CB RID: 46027 RVA: 0x0029BB76 File Offset: 0x00299D76
		public InvalidExternalUserIdException(string userId) : base(Strings.ErrorInvalidExternalUserId(userId))
		{
			this.userId = userId;
		}

		// Token: 0x0600B3CC RID: 46028 RVA: 0x0029BB8B File Offset: 0x00299D8B
		public InvalidExternalUserIdException(string userId, Exception innerException) : base(Strings.ErrorInvalidExternalUserId(userId), innerException)
		{
			this.userId = userId;
		}

		// Token: 0x0600B3CD RID: 46029 RVA: 0x0029BBA1 File Offset: 0x00299DA1
		protected InvalidExternalUserIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x0600B3CE RID: 46030 RVA: 0x0029BBCB File Offset: 0x00299DCB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x17003900 RID: 14592
		// (get) Token: 0x0600B3CF RID: 46031 RVA: 0x0029BBE6 File Offset: 0x00299DE6
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x04006266 RID: 25190
		private readonly string userId;
	}
}
