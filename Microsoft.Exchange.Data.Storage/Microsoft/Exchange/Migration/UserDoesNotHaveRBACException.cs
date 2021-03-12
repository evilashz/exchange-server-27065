using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000156 RID: 342
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserDoesNotHaveRBACException : MigrationPermanentException
	{
		// Token: 0x06001615 RID: 5653 RVA: 0x0006EDBA File Offset: 0x0006CFBA
		public UserDoesNotHaveRBACException(string username) : base(Strings.ErrorUserMissingOrWithoutRBAC(username))
		{
			this.username = username;
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0006EDCF File Offset: 0x0006CFCF
		public UserDoesNotHaveRBACException(string username, Exception innerException) : base(Strings.ErrorUserMissingOrWithoutRBAC(username), innerException)
		{
			this.username = username;
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0006EDE5 File Offset: 0x0006CFE5
		protected UserDoesNotHaveRBACException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.username = (string)info.GetValue("username", typeof(string));
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0006EE0F File Offset: 0x0006D00F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("username", this.username);
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0006EE2A File Offset: 0x0006D02A
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x04000AE5 RID: 2789
		private readonly string username;
	}
}
