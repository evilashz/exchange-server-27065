using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F36 RID: 3894
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerConfigurationException : LocalizedException
	{
		// Token: 0x0600AB07 RID: 43783 RVA: 0x0028E705 File Offset: 0x0028C905
		public ServerConfigurationException(string serverName, string errorMessage) : base(Strings.ServerConfigurationException(serverName, errorMessage))
		{
			this.serverName = serverName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AB08 RID: 43784 RVA: 0x0028E722 File Offset: 0x0028C922
		public ServerConfigurationException(string serverName, string errorMessage, Exception innerException) : base(Strings.ServerConfigurationException(serverName, errorMessage), innerException)
		{
			this.serverName = serverName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AB09 RID: 43785 RVA: 0x0028E740 File Offset: 0x0028C940
		protected ServerConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600AB0A RID: 43786 RVA: 0x0028E795 File Offset: 0x0028C995
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x1700374C RID: 14156
		// (get) Token: 0x0600AB0B RID: 43787 RVA: 0x0028E7C1 File Offset: 0x0028C9C1
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x1700374D RID: 14157
		// (get) Token: 0x0600AB0C RID: 43788 RVA: 0x0028E7C9 File Offset: 0x0028C9C9
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040060B2 RID: 24754
		private readonly string serverName;

		// Token: 0x040060B3 RID: 24755
		private readonly string errorMessage;
	}
}
