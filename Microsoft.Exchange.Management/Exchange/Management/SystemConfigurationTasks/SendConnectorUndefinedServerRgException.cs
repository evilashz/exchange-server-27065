using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F90 RID: 3984
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorUndefinedServerRgException : LocalizedException
	{
		// Token: 0x0600ACA7 RID: 44199 RVA: 0x002907CC File Offset: 0x0028E9CC
		public SendConnectorUndefinedServerRgException(string serverName) : base(Strings.SendConnectorUndefinedServerRg(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600ACA8 RID: 44200 RVA: 0x002907E1 File Offset: 0x0028E9E1
		public SendConnectorUndefinedServerRgException(string serverName, Exception innerException) : base(Strings.SendConnectorUndefinedServerRg(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600ACA9 RID: 44201 RVA: 0x002907F7 File Offset: 0x0028E9F7
		protected SendConnectorUndefinedServerRgException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600ACAA RID: 44202 RVA: 0x00290821 File Offset: 0x0028EA21
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003784 RID: 14212
		// (get) Token: 0x0600ACAB RID: 44203 RVA: 0x0029083C File Offset: 0x0028EA3C
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060EA RID: 24810
		private readonly string serverName;
	}
}
