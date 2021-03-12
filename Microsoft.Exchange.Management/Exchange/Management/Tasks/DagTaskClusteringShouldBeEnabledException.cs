using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001041 RID: 4161
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskClusteringShouldBeEnabledException : LocalizedException
	{
		// Token: 0x0600B000 RID: 45056 RVA: 0x002953F1 File Offset: 0x002935F1
		public DagTaskClusteringShouldBeEnabledException(string serverName) : base(Strings.DagTaskClusteringShouldBeEnabledException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B001 RID: 45057 RVA: 0x00295406 File Offset: 0x00293606
		public DagTaskClusteringShouldBeEnabledException(string serverName, Exception innerException) : base(Strings.DagTaskClusteringShouldBeEnabledException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B002 RID: 45058 RVA: 0x0029541C File Offset: 0x0029361C
		protected DagTaskClusteringShouldBeEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B003 RID: 45059 RVA: 0x00295446 File Offset: 0x00293646
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003819 RID: 14361
		// (get) Token: 0x0600B004 RID: 45060 RVA: 0x00295461 File Offset: 0x00293661
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400617F RID: 24959
		private readonly string serverName;
	}
}
