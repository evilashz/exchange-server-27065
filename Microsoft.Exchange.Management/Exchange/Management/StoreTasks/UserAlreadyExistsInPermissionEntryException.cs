using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020010FB RID: 4347
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UserAlreadyExistsInPermissionEntryException : StoragePermanentException
	{
		// Token: 0x0600B3D0 RID: 46032 RVA: 0x0029BBEE File Offset: 0x00299DEE
		public UserAlreadyExistsInPermissionEntryException(string userId) : base(Strings.ErrorUserAlreadyExistsInPermissionEntry(userId))
		{
			this.userId = userId;
		}

		// Token: 0x0600B3D1 RID: 46033 RVA: 0x0029BC03 File Offset: 0x00299E03
		public UserAlreadyExistsInPermissionEntryException(string userId, Exception innerException) : base(Strings.ErrorUserAlreadyExistsInPermissionEntry(userId), innerException)
		{
			this.userId = userId;
		}

		// Token: 0x0600B3D2 RID: 46034 RVA: 0x0029BC19 File Offset: 0x00299E19
		protected UserAlreadyExistsInPermissionEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userId = (string)info.GetValue("userId", typeof(string));
		}

		// Token: 0x0600B3D3 RID: 46035 RVA: 0x0029BC43 File Offset: 0x00299E43
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userId", this.userId);
		}

		// Token: 0x17003901 RID: 14593
		// (get) Token: 0x0600B3D4 RID: 46036 RVA: 0x0029BC5E File Offset: 0x00299E5E
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x04006267 RID: 25191
		private readonly string userId;
	}
}
