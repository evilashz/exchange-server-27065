using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000194 RID: 404
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationRemoteEndpointSettingsCouldNotBeAutodiscoveredException : MigrationPermanentException
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x00070410 File Offset: 0x0006E610
		public MigrationRemoteEndpointSettingsCouldNotBeAutodiscoveredException(string serverName) : base(Strings.CouldNotDetermineExchangeRemoteSettings(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00070425 File Offset: 0x0006E625
		public MigrationRemoteEndpointSettingsCouldNotBeAutodiscoveredException(string serverName, Exception innerException) : base(Strings.CouldNotDetermineExchangeRemoteSettings(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0007043B File Offset: 0x0006E63B
		protected MigrationRemoteEndpointSettingsCouldNotBeAutodiscoveredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00070465 File Offset: 0x0006E665
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00070480 File Offset: 0x0006E680
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04000B0A RID: 2826
		private readonly string serverName;
	}
}
