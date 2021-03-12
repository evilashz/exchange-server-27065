using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E2E RID: 3630
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerVersionNotSupportedException : LocalizedException
	{
		// Token: 0x0600A5EC RID: 42476 RVA: 0x00286D25 File Offset: 0x00284F25
		public ServerVersionNotSupportedException(string cmdletName, int cmdletVersion, int serverVersion) : base(Strings.ServerVersionNotSupportedException(cmdletName, cmdletVersion, serverVersion))
		{
			this.cmdletName = cmdletName;
			this.cmdletVersion = cmdletVersion;
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600A5ED RID: 42477 RVA: 0x00286D4A File Offset: 0x00284F4A
		public ServerVersionNotSupportedException(string cmdletName, int cmdletVersion, int serverVersion, Exception innerException) : base(Strings.ServerVersionNotSupportedException(cmdletName, cmdletVersion, serverVersion), innerException)
		{
			this.cmdletName = cmdletName;
			this.cmdletVersion = cmdletVersion;
			this.serverVersion = serverVersion;
		}

		// Token: 0x0600A5EE RID: 42478 RVA: 0x00286D74 File Offset: 0x00284F74
		protected ServerVersionNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.cmdletName = (string)info.GetValue("cmdletName", typeof(string));
			this.cmdletVersion = (int)info.GetValue("cmdletVersion", typeof(int));
			this.serverVersion = (int)info.GetValue("serverVersion", typeof(int));
		}

		// Token: 0x0600A5EF RID: 42479 RVA: 0x00286DE9 File Offset: 0x00284FE9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("cmdletName", this.cmdletName);
			info.AddValue("cmdletVersion", this.cmdletVersion);
			info.AddValue("serverVersion", this.serverVersion);
		}

		// Token: 0x17003651 RID: 13905
		// (get) Token: 0x0600A5F0 RID: 42480 RVA: 0x00286E26 File Offset: 0x00285026
		public string CmdletName
		{
			get
			{
				return this.cmdletName;
			}
		}

		// Token: 0x17003652 RID: 13906
		// (get) Token: 0x0600A5F1 RID: 42481 RVA: 0x00286E2E File Offset: 0x0028502E
		public int CmdletVersion
		{
			get
			{
				return this.cmdletVersion;
			}
		}

		// Token: 0x17003653 RID: 13907
		// (get) Token: 0x0600A5F2 RID: 42482 RVA: 0x00286E36 File Offset: 0x00285036
		public int ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x04005FB7 RID: 24503
		private readonly string cmdletName;

		// Token: 0x04005FB8 RID: 24504
		private readonly int cmdletVersion;

		// Token: 0x04005FB9 RID: 24505
		private readonly int serverVersion;
	}
}
