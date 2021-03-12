using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001044 RID: 4164
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToStartClusSvcException : LocalizedException
	{
		// Token: 0x0600B010 RID: 45072 RVA: 0x002955AD File Offset: 0x002937AD
		public FailedToStartClusSvcException(string serverName, string state) : base(Strings.FailedToStartClusSvc(serverName, state))
		{
			this.serverName = serverName;
			this.state = state;
		}

		// Token: 0x0600B011 RID: 45073 RVA: 0x002955CA File Offset: 0x002937CA
		public FailedToStartClusSvcException(string serverName, string state, Exception innerException) : base(Strings.FailedToStartClusSvc(serverName, state), innerException)
		{
			this.serverName = serverName;
			this.state = state;
		}

		// Token: 0x0600B012 RID: 45074 RVA: 0x002955E8 File Offset: 0x002937E8
		protected FailedToStartClusSvcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.state = (string)info.GetValue("state", typeof(string));
		}

		// Token: 0x0600B013 RID: 45075 RVA: 0x0029563D File Offset: 0x0029383D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("state", this.state);
		}

		// Token: 0x1700381D RID: 14365
		// (get) Token: 0x0600B014 RID: 45076 RVA: 0x00295669 File Offset: 0x00293869
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700381E RID: 14366
		// (get) Token: 0x0600B015 RID: 45077 RVA: 0x00295671 File Offset: 0x00293871
		public string State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x04006183 RID: 24963
		private readonly string serverName;

		// Token: 0x04006184 RID: 24964
		private readonly string state;
	}
}
