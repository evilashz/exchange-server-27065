using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015D RID: 349
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CouldNotCreateCredentialsPermanentException : MigrationPermanentException
	{
		// Token: 0x06001636 RID: 5686 RVA: 0x0006F070 File Offset: 0x0006D270
		public CouldNotCreateCredentialsPermanentException(string username) : base(Strings.ErrorCouldNotCreateCredentials(username))
		{
			this.username = username;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0006F085 File Offset: 0x0006D285
		public CouldNotCreateCredentialsPermanentException(string username, Exception innerException) : base(Strings.ErrorCouldNotCreateCredentials(username), innerException)
		{
			this.username = username;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0006F09B File Offset: 0x0006D29B
		protected CouldNotCreateCredentialsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.username = (string)info.GetValue("username", typeof(string));
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0006F0C5 File Offset: 0x0006D2C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("username", this.username);
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0006F0E0 File Offset: 0x0006D2E0
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x04000AEA RID: 2794
		private readonly string username;
	}
}
