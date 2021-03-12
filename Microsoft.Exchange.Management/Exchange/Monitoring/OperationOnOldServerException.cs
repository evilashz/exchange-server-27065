using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000EFE RID: 3838
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OperationOnOldServerException : LocalizedException
	{
		// Token: 0x0600A9DF RID: 43487 RVA: 0x0028C725 File Offset: 0x0028A925
		public OperationOnOldServerException(string serverName) : base(Strings.ErrorOperationOnOldServer(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A9E0 RID: 43488 RVA: 0x0028C73A File Offset: 0x0028A93A
		public OperationOnOldServerException(string serverName, Exception innerException) : base(Strings.ErrorOperationOnOldServer(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A9E1 RID: 43489 RVA: 0x0028C750 File Offset: 0x0028A950
		protected OperationOnOldServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600A9E2 RID: 43490 RVA: 0x0028C77A File Offset: 0x0028A97A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003704 RID: 14084
		// (get) Token: 0x0600A9E3 RID: 43491 RVA: 0x0028C795 File Offset: 0x0028A995
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400606A RID: 24682
		private readonly string serverName;
	}
}
