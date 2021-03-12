using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200108B RID: 4235
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskNoNetworksRunningDhcpException : LocalizedException
	{
		// Token: 0x0600B1A8 RID: 45480 RVA: 0x00298819 File Offset: 0x00296A19
		public DagTaskNoNetworksRunningDhcpException(string serverName) : base(Strings.DagTaskNoNetworksRunningDhcp(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B1A9 RID: 45481 RVA: 0x0029882E File Offset: 0x00296A2E
		public DagTaskNoNetworksRunningDhcpException(string serverName, Exception innerException) : base(Strings.DagTaskNoNetworksRunningDhcp(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B1AA RID: 45482 RVA: 0x00298844 File Offset: 0x00296A44
		protected DagTaskNoNetworksRunningDhcpException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B1AB RID: 45483 RVA: 0x0029886E File Offset: 0x00296A6E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003899 RID: 14489
		// (get) Token: 0x0600B1AC RID: 45484 RVA: 0x00298889 File Offset: 0x00296A89
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040061FF RID: 25087
		private readonly string serverName;
	}
}
