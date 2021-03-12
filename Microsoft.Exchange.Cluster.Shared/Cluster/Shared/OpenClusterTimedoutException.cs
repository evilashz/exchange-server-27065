using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000C5 RID: 197
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OpenClusterTimedoutException : ClusterException
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0001B6F8 File Offset: 0x000198F8
		public OpenClusterTimedoutException(string serverName, int timeoutInSeconds, string context) : base(Strings.OpenClusterTimedoutException(serverName, timeoutInSeconds, context))
		{
			this.serverName = serverName;
			this.timeoutInSeconds = timeoutInSeconds;
			this.context = context;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001B722 File Offset: 0x00019922
		public OpenClusterTimedoutException(string serverName, int timeoutInSeconds, string context, Exception innerException) : base(Strings.OpenClusterTimedoutException(serverName, timeoutInSeconds, context), innerException)
		{
			this.serverName = serverName;
			this.timeoutInSeconds = timeoutInSeconds;
			this.context = context;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001B750 File Offset: 0x00019950
		protected OpenClusterTimedoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.timeoutInSeconds = (int)info.GetValue("timeoutInSeconds", typeof(int));
			this.context = (string)info.GetValue("context", typeof(string));
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001B7C5 File Offset: 0x000199C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("timeoutInSeconds", this.timeoutInSeconds);
			info.AddValue("context", this.context);
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001B802 File Offset: 0x00019A02
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001B80A File Offset: 0x00019A0A
		public int TimeoutInSeconds
		{
			get
			{
				return this.timeoutInSeconds;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001B812 File Offset: 0x00019A12
		public string Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x04000717 RID: 1815
		private readonly string serverName;

		// Token: 0x04000718 RID: 1816
		private readonly int timeoutInSeconds;

		// Token: 0x04000719 RID: 1817
		private readonly string context;
	}
}
