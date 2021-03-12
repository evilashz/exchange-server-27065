using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D44 RID: 3396
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Service
	{
		// Token: 0x060075C6 RID: 30150 RVA: 0x0020962D File Offset: 0x0020782D
		internal Service(TopologyServerInfo serverInfo, ServiceType serviceType, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod)
		{
			this.ServerInfo = serverInfo;
			this.ServiceType = serviceType;
			this.ClientAccessType = clientAccessType;
			this.AuthenticationMethod = authenticationMethod;
		}

		// Token: 0x17001F8B RID: 8075
		// (get) Token: 0x060075C7 RID: 30151 RVA: 0x00209652 File Offset: 0x00207852
		// (set) Token: 0x060075C8 RID: 30152 RVA: 0x0020965A File Offset: 0x0020785A
		public TopologyServerInfo ServerInfo { get; private set; }

		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x060075C9 RID: 30153 RVA: 0x00209663 File Offset: 0x00207863
		// (set) Token: 0x060075CA RID: 30154 RVA: 0x0020966B File Offset: 0x0020786B
		public ServiceType ServiceType { get; private set; }

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x060075CB RID: 30155 RVA: 0x00209674 File Offset: 0x00207874
		// (set) Token: 0x060075CC RID: 30156 RVA: 0x0020967C File Offset: 0x0020787C
		public ClientAccessType ClientAccessType { get; private set; }

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x060075CD RID: 30157 RVA: 0x00209685 File Offset: 0x00207885
		// (set) Token: 0x060075CE RID: 30158 RVA: 0x0020968D File Offset: 0x0020788D
		public AuthenticationMethod AuthenticationMethod { get; private set; }

		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x060075CF RID: 30159 RVA: 0x00209696 File Offset: 0x00207896
		public string ServerFullyQualifiedDomainName
		{
			get
			{
				return this.ServerInfo.ServerFullyQualifiedDomainName;
			}
		}

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x060075D0 RID: 30160 RVA: 0x002096A3 File Offset: 0x002078A3
		public Site Site
		{
			get
			{
				return this.ServerInfo.Site;
			}
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x060075D1 RID: 30161 RVA: 0x002096B0 File Offset: 0x002078B0
		public int ServerVersionNumber
		{
			get
			{
				return this.ServerInfo.VersionNumber;
			}
		}

		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x060075D2 RID: 30162 RVA: 0x002096BD File Offset: 0x002078BD
		public ServerVersion AdminDisplayVersionNumber
		{
			get
			{
				return this.ServerInfo.AdminDisplayVersionNumber;
			}
		}

		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x060075D3 RID: 30163 RVA: 0x002096CA File Offset: 0x002078CA
		public ServerRole ServerRole
		{
			get
			{
				return this.ServerInfo.Role;
			}
		}

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x060075D4 RID: 30164 RVA: 0x002096D7 File Offset: 0x002078D7
		public bool IsOutOfService
		{
			get
			{
				return this.ServerInfo.IsOutOfService;
			}
		}

		// Token: 0x060075D5 RID: 30165 RVA: 0x002096E4 File Offset: 0x002078E4
		public override string ToString()
		{
			return string.Format("Service. Type = {0}. ClientAccessType = {1}. AuthenticationMethod = {2}.", this.ServiceType, this.ClientAccessType, this.AuthenticationMethod);
		}

		// Token: 0x060075D6 RID: 30166 RVA: 0x00209714 File Offset: 0x00207914
		internal static ReadOnlyCollection<T> ConvertToReadOnlyCollection<T>(MultiValuedProperty<T> properties)
		{
			if (properties == null)
			{
				return null;
			}
			List<T> list = new List<T>(properties);
			return list.AsReadOnly();
		}

		// Token: 0x060075D7 RID: 30167 RVA: 0x00209733 File Offset: 0x00207933
		internal static bool TryCreateService(TopologyServerInfo serverInfo, ClientAccessType clientAccessType, AuthenticationMethod authenticationMethod, out Service service)
		{
			service = new Service(serverInfo, ServiceType.Invalid, clientAccessType, authenticationMethod);
			return true;
		}
	}
}
