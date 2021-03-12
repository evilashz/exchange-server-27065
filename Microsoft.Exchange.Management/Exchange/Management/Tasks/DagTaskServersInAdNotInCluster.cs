using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001089 RID: 4233
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServersInAdNotInCluster : LocalizedException
	{
		// Token: 0x0600B19E RID: 45470 RVA: 0x00298729 File Offset: 0x00296929
		public DagTaskServersInAdNotInCluster(string serverNames) : base(Strings.DagTaskServersInAdNotInCluster(serverNames))
		{
			this.serverNames = serverNames;
		}

		// Token: 0x0600B19F RID: 45471 RVA: 0x0029873E File Offset: 0x0029693E
		public DagTaskServersInAdNotInCluster(string serverNames, Exception innerException) : base(Strings.DagTaskServersInAdNotInCluster(serverNames), innerException)
		{
			this.serverNames = serverNames;
		}

		// Token: 0x0600B1A0 RID: 45472 RVA: 0x00298754 File Offset: 0x00296954
		protected DagTaskServersInAdNotInCluster(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverNames = (string)info.GetValue("serverNames", typeof(string));
		}

		// Token: 0x0600B1A1 RID: 45473 RVA: 0x0029877E File Offset: 0x0029697E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverNames", this.serverNames);
		}

		// Token: 0x17003897 RID: 14487
		// (get) Token: 0x0600B1A2 RID: 45474 RVA: 0x00298799 File Offset: 0x00296999
		public string ServerNames
		{
			get
			{
				return this.serverNames;
			}
		}

		// Token: 0x040061FD RID: 25085
		private readonly string serverNames;
	}
}
