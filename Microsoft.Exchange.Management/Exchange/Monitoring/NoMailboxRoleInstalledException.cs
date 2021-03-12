using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F35 RID: 3893
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoMailboxRoleInstalledException : LocalizedException
	{
		// Token: 0x0600AB02 RID: 43778 RVA: 0x0028E68D File Offset: 0x0028C88D
		public NoMailboxRoleInstalledException(string serverName) : base(Strings.NoMailboxRoleInstalledException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AB03 RID: 43779 RVA: 0x0028E6A2 File Offset: 0x0028C8A2
		public NoMailboxRoleInstalledException(string serverName, Exception innerException) : base(Strings.NoMailboxRoleInstalledException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AB04 RID: 43780 RVA: 0x0028E6B8 File Offset: 0x0028C8B8
		protected NoMailboxRoleInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AB05 RID: 43781 RVA: 0x0028E6E2 File Offset: 0x0028C8E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700374B RID: 14155
		// (get) Token: 0x0600AB06 RID: 43782 RVA: 0x0028E6FD File Offset: 0x0028C8FD
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060B1 RID: 24753
		private readonly string serverName;
	}
}
