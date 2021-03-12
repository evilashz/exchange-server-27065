using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020010F9 RID: 4345
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidInternalUserIdException : StoragePermanentException
	{
		// Token: 0x0600B3C6 RID: 46022 RVA: 0x0029BAFE File Offset: 0x00299CFE
		public InvalidInternalUserIdException(string userId) : base(Strings.ErrorInvalidInternalUserId(userId))
		{
			this.userId = userId;
		}

		// Token: 0x0600B3C7 RID: 46023 RVA: 0x0029BB13 File Offset: 0x00299D13
		public InvalidInternalUserIdException(string userId, Exception innerException) : base(Strings.ErrorInvalidInternalUserId(userId), innerException)
		{
			this.userId = userId;
		}

		// Token: 0x0600B3C8 RID: 46024 RVA: 0x0029BB29 File Offset: 0x00299D29
		protected InvalidInternalUserIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x0600B3C9 RID: 46025 RVA: 0x0029BB53 File Offset: 0x00299D53
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x170038FF RID: 14591
		// (get) Token: 0x0600B3CA RID: 46026 RVA: 0x0029BB6E File Offset: 0x00299D6E
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x04006265 RID: 25189
		private readonly string userId;
	}
}
