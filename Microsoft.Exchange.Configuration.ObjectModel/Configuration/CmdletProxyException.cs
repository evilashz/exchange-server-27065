using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration
{
	// Token: 0x020002E0 RID: 736
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CmdletProxyException : LocalizedException
	{
		// Token: 0x060019C1 RID: 6593 RVA: 0x0005D815 File Offset: 0x0005BA15
		public CmdletProxyException(string command, string serverFqn, string serverVersion, string proxyMethod, string errorMessage) : base(Strings.ErrorCmdletProxy(command, serverFqn, serverVersion, proxyMethod, errorMessage))
		{
			this.command = command;
			this.serverFqn = serverFqn;
			this.serverVersion = serverVersion;
			this.proxyMethod = proxyMethod;
			this.errorMessage = errorMessage;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0005D84E File Offset: 0x0005BA4E
		public CmdletProxyException(string command, string serverFqn, string serverVersion, string proxyMethod, string errorMessage, Exception innerException) : base(Strings.ErrorCmdletProxy(command, serverFqn, serverVersion, proxyMethod, errorMessage), innerException)
		{
			this.command = command;
			this.serverFqn = serverFqn;
			this.serverVersion = serverVersion;
			this.proxyMethod = proxyMethod;
			this.errorMessage = errorMessage;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0005D88C File Offset: 0x0005BA8C
		protected CmdletProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.command = (string)info.GetValue("command", typeof(string));
			this.serverFqn = (string)info.GetValue("serverFqn", typeof(string));
			this.serverVersion = (string)info.GetValue("serverVersion", typeof(string));
			this.proxyMethod = (string)info.GetValue("proxyMethod", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0005D944 File Offset: 0x0005BB44
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("command", this.command);
			info.AddValue("serverFqn", this.serverFqn);
			info.AddValue("serverVersion", this.serverVersion);
			info.AddValue("proxyMethod", this.proxyMethod);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0005D9AE File Offset: 0x0005BBAE
		public string Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0005D9B6 File Offset: 0x0005BBB6
		public string ServerFqn
		{
			get
			{
				return this.serverFqn;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0005D9BE File Offset: 0x0005BBBE
		public string ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x0005D9C6 File Offset: 0x0005BBC6
		public string ProxyMethod
		{
			get
			{
				return this.proxyMethod;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0005D9CE File Offset: 0x0005BBCE
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040009A6 RID: 2470
		private readonly string command;

		// Token: 0x040009A7 RID: 2471
		private readonly string serverFqn;

		// Token: 0x040009A8 RID: 2472
		private readonly string serverVersion;

		// Token: 0x040009A9 RID: 2473
		private readonly string proxyMethod;

		// Token: 0x040009AA RID: 2474
		private readonly string errorMessage;
	}
}
