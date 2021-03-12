using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000EFF RID: 3839
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OperationOnlyOnMailboxServerException : LocalizedException
	{
		// Token: 0x0600A9E4 RID: 43492 RVA: 0x0028C79D File Offset: 0x0028A99D
		public OperationOnlyOnMailboxServerException(string serverName) : base(Strings.ErrorOperationOnlyOnMailboxServer(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A9E5 RID: 43493 RVA: 0x0028C7B2 File Offset: 0x0028A9B2
		public OperationOnlyOnMailboxServerException(string serverName, Exception innerException) : base(Strings.ErrorOperationOnlyOnMailboxServer(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A9E6 RID: 43494 RVA: 0x0028C7C8 File Offset: 0x0028A9C8
		protected OperationOnlyOnMailboxServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600A9E7 RID: 43495 RVA: 0x0028C7F2 File Offset: 0x0028A9F2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003705 RID: 14085
		// (get) Token: 0x0600A9E8 RID: 43496 RVA: 0x0028C80D File Offset: 0x0028AA0D
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400606B RID: 24683
		private readonly string serverName;
	}
}
