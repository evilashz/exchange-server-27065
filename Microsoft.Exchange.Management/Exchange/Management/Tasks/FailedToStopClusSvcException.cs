using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001045 RID: 4165
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToStopClusSvcException : LocalizedException
	{
		// Token: 0x0600B016 RID: 45078 RVA: 0x00295679 File Offset: 0x00293879
		public FailedToStopClusSvcException(string serverName, string state) : base(Strings.FailedToStopClusSvc(serverName, state))
		{
			this.serverName = serverName;
			this.state = state;
		}

		// Token: 0x0600B017 RID: 45079 RVA: 0x00295696 File Offset: 0x00293896
		public FailedToStopClusSvcException(string serverName, string state, Exception innerException) : base(Strings.FailedToStopClusSvc(serverName, state), innerException)
		{
			this.serverName = serverName;
			this.state = state;
		}

		// Token: 0x0600B018 RID: 45080 RVA: 0x002956B4 File Offset: 0x002938B4
		protected FailedToStopClusSvcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.state = (string)info.GetValue("state", typeof(string));
		}

		// Token: 0x0600B019 RID: 45081 RVA: 0x00295709 File Offset: 0x00293909
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("state", this.state);
		}

		// Token: 0x1700381F RID: 14367
		// (get) Token: 0x0600B01A RID: 45082 RVA: 0x00295735 File Offset: 0x00293935
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17003820 RID: 14368
		// (get) Token: 0x0600B01B RID: 45083 RVA: 0x0029573D File Offset: 0x0029393D
		public string State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x04006185 RID: 24965
		private readonly string serverName;

		// Token: 0x04006186 RID: 24966
		private readonly string state;
	}
}
