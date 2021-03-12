using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000197 RID: 407
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationServerConnectionTimeoutException : MigrationTransientException
	{
		// Token: 0x06001744 RID: 5956 RVA: 0x00070672 File Offset: 0x0006E872
		public MigrationServerConnectionTimeoutException(string remoteHost, TimeSpan timeout) : base(Strings.ErrorConnectionTimeout(remoteHost, timeout))
		{
			this.remoteHost = remoteHost;
			this.timeout = timeout;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0007068F File Offset: 0x0006E88F
		public MigrationServerConnectionTimeoutException(string remoteHost, TimeSpan timeout, Exception innerException) : base(Strings.ErrorConnectionTimeout(remoteHost, timeout), innerException)
		{
			this.remoteHost = remoteHost;
			this.timeout = timeout;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000706B0 File Offset: 0x0006E8B0
		protected MigrationServerConnectionTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.remoteHost = (string)info.GetValue("remoteHost", typeof(string));
			this.timeout = (TimeSpan)info.GetValue("timeout", typeof(TimeSpan));
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00070705 File Offset: 0x0006E905
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("remoteHost", this.remoteHost);
			info.AddValue("timeout", this.timeout);
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x00070736 File Offset: 0x0006E936
		public string RemoteHost
		{
			get
			{
				return this.remoteHost;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x0007073E File Offset: 0x0006E93E
		public TimeSpan Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x04000B10 RID: 2832
		private readonly string remoteHost;

		// Token: 0x04000B11 RID: 2833
		private readonly TimeSpan timeout;
	}
}
