using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F13 RID: 3859
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthCouldNotBuildTestUserNameException : LocalizedException
	{
		// Token: 0x0600AA50 RID: 43600 RVA: 0x0028D3B4 File Offset: 0x0028B5B4
		public CasHealthCouldNotBuildTestUserNameException(string serverName) : base(Strings.CasHealthCouldNotBuildTestUserName(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AA51 RID: 43601 RVA: 0x0028D3C9 File Offset: 0x0028B5C9
		public CasHealthCouldNotBuildTestUserNameException(string serverName, Exception innerException) : base(Strings.CasHealthCouldNotBuildTestUserName(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AA52 RID: 43602 RVA: 0x0028D3DF File Offset: 0x0028B5DF
		protected CasHealthCouldNotBuildTestUserNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AA53 RID: 43603 RVA: 0x0028D409 File Offset: 0x0028B609
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003721 RID: 14113
		// (get) Token: 0x0600AA54 RID: 43604 RVA: 0x0028D424 File Offset: 0x0028B624
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04006087 RID: 24711
		private readonly string serverName;
	}
}
