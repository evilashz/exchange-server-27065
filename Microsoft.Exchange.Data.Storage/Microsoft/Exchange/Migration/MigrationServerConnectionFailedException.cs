using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000198 RID: 408
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationServerConnectionFailedException : MigrationTransientException
	{
		// Token: 0x0600174A RID: 5962 RVA: 0x00070746 File Offset: 0x0006E946
		public MigrationServerConnectionFailedException(string remoteHost) : base(Strings.ErrorConnectionFailed(remoteHost))
		{
			this.remoteHost = remoteHost;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0007075B File Offset: 0x0006E95B
		public MigrationServerConnectionFailedException(string remoteHost, Exception innerException) : base(Strings.ErrorConnectionFailed(remoteHost), innerException)
		{
			this.remoteHost = remoteHost;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00070771 File Offset: 0x0006E971
		protected MigrationServerConnectionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.remoteHost = (string)info.GetValue("remoteHost", typeof(string));
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0007079B File Offset: 0x0006E99B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("remoteHost", this.remoteHost);
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x000707B6 File Offset: 0x0006E9B6
		public string RemoteHost
		{
			get
			{
				return this.remoteHost;
			}
		}

		// Token: 0x04000B12 RID: 2834
		private readonly string remoteHost;
	}
}
