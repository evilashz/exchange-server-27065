using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000228 RID: 552
	[DataContract]
	public sealed class MigrationEndpointObject
	{
		// Token: 0x06002786 RID: 10118 RVA: 0x0007C71C File Offset: 0x0007A91C
		public MigrationEndpointObject(MigrationEndpoint endpoint)
		{
			this.Name = endpoint.Identity.Id;
			this.Identity = new Identity(endpoint.Identity);
			this.RemoteServer = endpoint.RemoteServer;
			this.Port = endpoint.Port;
			this.ExchangeServer = endpoint.ExchangeServer;
			this.RpcProxyServer = endpoint.RpcProxyServer;
			this.EndpointType = endpoint.EndpointType.ToString();
			this.MaxConcurrentMigrations = endpoint.MaxConcurrentMigrations.ToString();
			this.UserName = endpoint.Username;
			this.MailboxPermission = endpoint.MailboxPermission.ToString();
			if (endpoint.Authentication != null)
			{
				this.Authentication = endpoint.Authentication.ToString();
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x0007C807 File Offset: 0x0007AA07
		// (set) Token: 0x06002788 RID: 10120 RVA: 0x0007C80F File Offset: 0x0007AA0F
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x0007C818 File Offset: 0x0007AA18
		// (set) Token: 0x0600278A RID: 10122 RVA: 0x0007C820 File Offset: 0x0007AA20
		[DataMember]
		public Identity Identity { get; private set; }

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x0600278B RID: 10123 RVA: 0x0007C829 File Offset: 0x0007AA29
		// (set) Token: 0x0600278C RID: 10124 RVA: 0x0007C831 File Offset: 0x0007AA31
		[DataMember]
		public string RemoteServer { get; private set; }

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x0007C83A File Offset: 0x0007AA3A
		// (set) Token: 0x0600278E RID: 10126 RVA: 0x0007C842 File Offset: 0x0007AA42
		[DataMember]
		public string ExchangeServer { get; private set; }

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x0600278F RID: 10127 RVA: 0x0007C84B File Offset: 0x0007AA4B
		// (set) Token: 0x06002790 RID: 10128 RVA: 0x0007C853 File Offset: 0x0007AA53
		[DataMember]
		public string RpcProxyServer { get; private set; }

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x0007C85C File Offset: 0x0007AA5C
		// (set) Token: 0x06002792 RID: 10130 RVA: 0x0007C864 File Offset: 0x0007AA64
		[DataMember]
		public int? Port { get; private set; }

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x0007C86D File Offset: 0x0007AA6D
		// (set) Token: 0x06002794 RID: 10132 RVA: 0x0007C875 File Offset: 0x0007AA75
		[DataMember]
		public string EndpointType { get; private set; }

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x0007C87E File Offset: 0x0007AA7E
		// (set) Token: 0x06002796 RID: 10134 RVA: 0x0007C886 File Offset: 0x0007AA86
		[DataMember]
		public string UserName { get; private set; }

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x0007C88F File Offset: 0x0007AA8F
		// (set) Token: 0x06002798 RID: 10136 RVA: 0x0007C897 File Offset: 0x0007AA97
		[DataMember]
		public string MaxConcurrentMigrations { get; private set; }

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x0007C8A0 File Offset: 0x0007AAA0
		// (set) Token: 0x0600279A RID: 10138 RVA: 0x0007C8A8 File Offset: 0x0007AAA8
		[DataMember]
		public string MailboxPermission { get; private set; }

		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x0007C8B1 File Offset: 0x0007AAB1
		// (set) Token: 0x0600279C RID: 10140 RVA: 0x0007C8B9 File Offset: 0x0007AAB9
		[DataMember]
		public string Authentication { get; private set; }

		// Token: 0x0600279D RID: 10141 RVA: 0x0007C8C2 File Offset: 0x0007AAC2
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0007C8D0 File Offset: 0x0007AAD0
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			MigrationEndpointObject migrationEndpointObject = obj as MigrationEndpointObject;
			return migrationEndpointObject == this || (string.Equals(this.Name, migrationEndpointObject.Name) && string.Equals(this.RemoteServer, migrationEndpointObject.RemoteServer) && string.Equals(this.ExchangeServer, migrationEndpointObject.ExchangeServer) && string.Equals(this.RpcProxyServer, migrationEndpointObject.RpcProxyServer) && string.Equals(this.UserName, migrationEndpointObject.UserName) && this.Port == migrationEndpointObject.Port && this.EndpointType == migrationEndpointObject.EndpointType && this.MaxConcurrentMigrations == migrationEndpointObject.MaxConcurrentMigrations && string.Equals(this.MailboxPermission, migrationEndpointObject.MailboxPermission) && string.Equals(this.Authentication, migrationEndpointObject.Authentication));
		}
	}
}
