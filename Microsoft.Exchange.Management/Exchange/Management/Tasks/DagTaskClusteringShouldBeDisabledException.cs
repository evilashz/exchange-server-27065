using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001040 RID: 4160
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskClusteringShouldBeDisabledException : LocalizedException
	{
		// Token: 0x0600AFFB RID: 45051 RVA: 0x00295379 File Offset: 0x00293579
		public DagTaskClusteringShouldBeDisabledException(string serverName) : base(Strings.DagTaskClusteringShouldBeDisabledException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AFFC RID: 45052 RVA: 0x0029538E File Offset: 0x0029358E
		public DagTaskClusteringShouldBeDisabledException(string serverName, Exception innerException) : base(Strings.DagTaskClusteringShouldBeDisabledException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AFFD RID: 45053 RVA: 0x002953A4 File Offset: 0x002935A4
		protected DagTaskClusteringShouldBeDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AFFE RID: 45054 RVA: 0x002953CE File Offset: 0x002935CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003818 RID: 14360
		// (get) Token: 0x0600AFFF RID: 45055 RVA: 0x002953E9 File Offset: 0x002935E9
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400617E RID: 24958
		private readonly string serverName;
	}
}
