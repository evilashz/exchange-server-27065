using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015C RID: 348
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CouldNotEncryptPasswordException : MigrationPermanentException
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x0006EFF8 File Offset: 0x0006D1F8
		public CouldNotEncryptPasswordException(string username) : base(Strings.ErrorCouldNotEncryptPassword(username))
		{
			this.username = username;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0006F00D File Offset: 0x0006D20D
		public CouldNotEncryptPasswordException(string username, Exception innerException) : base(Strings.ErrorCouldNotEncryptPassword(username), innerException)
		{
			this.username = username;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0006F023 File Offset: 0x0006D223
		protected CouldNotEncryptPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.username = (string)info.GetValue("username", typeof(string));
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0006F04D File Offset: 0x0006D24D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("username", this.username);
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0006F068 File Offset: 0x0006D268
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x04000AE9 RID: 2793
		private readonly string username;
	}
}
