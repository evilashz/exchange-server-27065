using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001091 RID: 4241
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerDifferentExchangeVersionException : LocalizedException
	{
		// Token: 0x0600B1CA RID: 45514 RVA: 0x00298C35 File Offset: 0x00296E35
		public DagTaskServerDifferentExchangeVersionException(string dagName, string existingServer, object existingVersion, string serverName, object serverVersion) : base(Strings.DagTaskServerDifferentExchangeVersion(dagName, existingServer, existingVersion, serverName, serverVersion))
		{
			this.dagName = dagName;
			this.existingServer = existingServer;
			this.existingVersion = existingVersion;
			this.serverName = serverName;
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600B1CB RID: 45515 RVA: 0x00298C6E File Offset: 0x00296E6E
		public DagTaskServerDifferentExchangeVersionException(string dagName, string existingServer, object existingVersion, string serverName, object serverVersion, Exception innerException) : base(Strings.DagTaskServerDifferentExchangeVersion(dagName, existingServer, existingVersion, serverName, serverVersion), innerException)
		{
			this.dagName = dagName;
			this.existingServer = existingServer;
			this.existingVersion = existingVersion;
			this.serverName = serverName;
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600B1CC RID: 45516 RVA: 0x00298CAC File Offset: 0x00296EAC
		protected DagTaskServerDifferentExchangeVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
			this.existingServer = (string)info.GetValue("existingServer", typeof(string));
			this.existingVersion = info.GetValue("existingVersion", typeof(object));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.serverVersion = info.GetValue("serverVersion", typeof(object));
		}

		// Token: 0x0600B1CD RID: 45517 RVA: 0x00298D58 File Offset: 0x00296F58
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
			info.AddValue("existingServer", this.existingServer);
			info.AddValue("existingVersion", this.existingVersion);
			info.AddValue("serverName", this.serverName);
			info.AddValue("serverVersion", this.serverVersion);
		}

		// Token: 0x170038A3 RID: 14499
		// (get) Token: 0x0600B1CE RID: 45518 RVA: 0x00298DC2 File Offset: 0x00296FC2
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x170038A4 RID: 14500
		// (get) Token: 0x0600B1CF RID: 45519 RVA: 0x00298DCA File Offset: 0x00296FCA
		public string ExistingServer
		{
			get
			{
				return this.existingServer;
			}
		}

		// Token: 0x170038A5 RID: 14501
		// (get) Token: 0x0600B1D0 RID: 45520 RVA: 0x00298DD2 File Offset: 0x00296FD2
		public object ExistingVersion
		{
			get
			{
				return this.existingVersion;
			}
		}

		// Token: 0x170038A6 RID: 14502
		// (get) Token: 0x0600B1D1 RID: 45521 RVA: 0x00298DDA File Offset: 0x00296FDA
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x170038A7 RID: 14503
		// (get) Token: 0x0600B1D2 RID: 45522 RVA: 0x00298DE2 File Offset: 0x00296FE2
		public object ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x04006209 RID: 25097
		private readonly string dagName;

		// Token: 0x0400620A RID: 25098
		private readonly string existingServer;

		// Token: 0x0400620B RID: 25099
		private readonly object existingVersion;

		// Token: 0x0400620C RID: 25100
		private readonly string serverName;

		// Token: 0x0400620D RID: 25101
		private readonly object serverVersion;
	}
}
