using System;
using System.DirectoryServices.Protocols;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	internal class ADServerInfo : ICloneable
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x0001E814 File Offset: 0x0001CA14
		protected ADServerInfo(string serverFqdn, string partitionFqdn, string writableNC, int port, int dnsWeight, AuthType authType, bool isServerSuitable = true)
		{
			ArgumentValidator.ThrowIfNegative("port", port);
			ArgumentValidator.ThrowIfNegative("dnsWeight", dnsWeight);
			if (authType != AuthType.Negotiate && authType != AuthType.Anonymous && authType != AuthType.Kerberos)
			{
				throw new ArgumentException("Invalid value for authType. Supported values 'Negotiate', 'Anonymous', 'Kerberos'. Actual " + authType.ToString());
			}
			this.Mapping = -1;
			this.PartitionFqdn = partitionFqdn;
			this.Fqdn = ADServerInfo.CurrentADServerInfoMapper.GetMappedFqdn(serverFqdn);
			this.Port = ADServerInfo.CurrentADServerInfoMapper.GetMappedPortNumber(serverFqdn, string.IsNullOrEmpty(this.PartitionFqdn) ? writableNC : this.PartitionFqdn, port);
			this.authType = ADServerInfo.CurrentADServerInfoMapper.GetMappedAuthType(serverFqdn, authType);
			this.writableNC = (writableNC ?? string.Empty);
			this.configNC = string.Empty;
			this.rootNC = string.Empty;
			this.schemaNC = string.Empty;
			this.DnsWeight = dnsWeight;
			this.IsServerSuitable = isServerSuitable;
			this.fqdnPlusPort = (string.IsNullOrEmpty(this.Fqdn) ? string.Empty : (this.Fqdn + ":" + this.Port));
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001E93B File Offset: 0x0001CB3B
		public ADServerInfo(string serverFqdn, int port, string writableNC = null, int dnsWeight = 100, AuthType authType = AuthType.Kerberos) : this(serverFqdn, TopologyProvider.LocalForestFqdn, writableNC, port, dnsWeight, authType, true)
		{
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001E950 File Offset: 0x0001CB50
		public ADServerInfo(string serverFqdn, string partitionFqdn, int port, string writableNC = null, int dnsWeight = 100, AuthType authType = AuthType.Kerberos, bool isServerSuitable = true) : this(serverFqdn, partitionFqdn, writableNC, port, dnsWeight, authType, isServerSuitable)
		{
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001E963 File Offset: 0x0001CB63
		public static IADServerInfoMapper CurrentADServerInfoMapper
		{
			get
			{
				return ADServerInfo.hookableInstance.Value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001E96F File Offset: 0x0001CB6F
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001E977 File Offset: 0x0001CB77
		public string Fqdn { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001E980 File Offset: 0x0001CB80
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0001E988 File Offset: 0x0001CB88
		public string SiteName
		{
			get
			{
				return this.siteName;
			}
			internal set
			{
				this.siteName = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001E991 File Offset: 0x0001CB91
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0001E999 File Offset: 0x0001CB99
		public string WritableNC
		{
			get
			{
				return this.writableNC;
			}
			internal set
			{
				this.writableNC = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001E9A2 File Offset: 0x0001CBA2
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0001E9AA File Offset: 0x0001CBAA
		public string ConfigNC
		{
			get
			{
				return this.configNC;
			}
			internal set
			{
				this.configNC = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001E9B3 File Offset: 0x0001CBB3
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001E9BB File Offset: 0x0001CBBB
		public string RootDomainNC
		{
			get
			{
				return this.rootNC;
			}
			internal set
			{
				this.rootNC = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001E9CC File Offset: 0x0001CBCC
		public int Mapping
		{
			get
			{
				return this.mapping;
			}
			internal set
			{
				this.mapping = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001E9D5 File Offset: 0x0001CBD5
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0001E9DD File Offset: 0x0001CBDD
		public string PartitionFqdn { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001E9E6 File Offset: 0x0001CBE6
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0001E9EE File Offset: 0x0001CBEE
		public string SchemaNC
		{
			get
			{
				return this.schemaNC;
			}
			internal set
			{
				this.schemaNC = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0001E9F7 File Offset: 0x0001CBF7
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0001E9FF File Offset: 0x0001CBFF
		public int Port { get; private set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0001EA08 File Offset: 0x0001CC08
		public int TotalRequestCount
		{
			get
			{
				return this.totalRequests;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001EA10 File Offset: 0x0001CC10
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x0001EA18 File Offset: 0x0001CC18
		public int DnsWeight { get; private set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001EA21 File Offset: 0x0001CC21
		public string FqdnPlusPort
		{
			get
			{
				return this.fqdnPlusPort;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0001EA29 File Offset: 0x0001CC29
		public AuthType AuthType
		{
			get
			{
				return this.authType;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001EA31 File Offset: 0x0001CC31
		internal static IDisposable SetTestHook(IADServerInfoMapper wrapper)
		{
			return ADServerInfo.hookableInstance.SetTestHook(wrapper);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001EA3E File Offset: 0x0001CC3E
		internal void IncrementRequestCount()
		{
			Interlocked.Increment(ref this.totalRequests);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001EA4C File Offset: 0x0001CC4C
		public ADRole GetADServerRole()
		{
			return ADServerInfo.hookableInstance.Value.GetADRole(this);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001EA60 File Offset: 0x0001CC60
		public override bool Equals(object rhs)
		{
			ADServerInfo adserverInfo = rhs as ADServerInfo;
			return adserverInfo != null && this.fqdnPlusPort.Equals(adserverInfo.fqdnPlusPort, StringComparison.OrdinalIgnoreCase) && this.AuthType.Equals(adserverInfo.AuthType);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001EAAA File Offset: 0x0001CCAA
		public override int GetHashCode()
		{
			return this.fqdnPlusPort.GetHashCode();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001EAB7 File Offset: 0x0001CCB7
		public override string ToString()
		{
			return this.FqdnPlusPort;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001EABF File Offset: 0x0001CCBF
		public object Clone()
		{
			return this.CloneWithServerNameResolved(this.Fqdn);
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0001EACD File Offset: 0x0001CCCD
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0001EAD5 File Offset: 0x0001CCD5
		public bool IsServerSuitable
		{
			get
			{
				return this.isServerSuitable;
			}
			internal set
			{
				this.isServerSuitable = value;
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		public ADServerInfo CloneWithServerNameResolved(string serverFqdn)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("serverFqdn", serverFqdn);
			return this.InternalClone(serverFqdn, null);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001EB08 File Offset: 0x0001CD08
		public ADServerInfo CloneAsRole(ADRole role)
		{
			return this.InternalClone(null, new ADRole?(role));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001EB18 File Offset: 0x0001CD18
		private ADServerInfo InternalClone(string serverFqdn, ADRole? role = null)
		{
			if (string.IsNullOrEmpty(serverFqdn) && string.IsNullOrEmpty(this.Fqdn))
			{
				throw new NotSupportedException("ServerFqdn can't null if this.Fqdn is null.");
			}
			if (!string.IsNullOrEmpty(this.Fqdn) && !string.IsNullOrEmpty(serverFqdn) && !string.Equals(this.Fqdn, serverFqdn, StringComparison.OrdinalIgnoreCase))
			{
				throw new NotSupportedException("Current ADServerInfo already has serverName resolved. Names doesn't match");
			}
			int port = this.Port;
			if (role != null)
			{
				ADRole adserverRole = this.GetADServerRole();
				if (role.Value != adserverRole)
				{
					if (ADRole.GlobalCatalog == role.Value)
					{
						port = 3268;
					}
					else
					{
						port = 389;
					}
				}
			}
			ADServerInfo adserverInfo;
			if (!string.IsNullOrEmpty(this.PartitionFqdn))
			{
				adserverInfo = new ADServerInfo(serverFqdn ?? this.Fqdn, this.PartitionFqdn, port, this.writableNC, this.DnsWeight, this.authType, true);
			}
			else
			{
				adserverInfo = new ADServerInfo(serverFqdn ?? this.Fqdn, port, this.writableNC, this.DnsWeight, this.authType);
			}
			if (!string.IsNullOrEmpty(this.siteName))
			{
				adserverInfo.siteName = this.siteName;
			}
			if (!string.IsNullOrEmpty(this.ConfigNC))
			{
				adserverInfo.ConfigNC = this.ConfigNC;
			}
			if (!string.IsNullOrEmpty(this.RootDomainNC))
			{
				adserverInfo.RootDomainNC = this.RootDomainNC;
			}
			if (!string.IsNullOrEmpty(this.SchemaNC))
			{
				adserverInfo.SchemaNC = this.SchemaNC;
			}
			return adserverInfo;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001EC74 File Offset: 0x0001CE74
		internal LdapDirectoryIdentifier GetLdapDirectoryIdentifier()
		{
			bool flag = !string.IsNullOrEmpty(this.FqdnPlusPort);
			if (!flag)
			{
				flag = (this.PartitionFqdn == null || TopologyProvider.LocalForestFqdn.Equals(this.PartitionFqdn, StringComparison.OrdinalIgnoreCase));
			}
			string text = flag ? this.FqdnPlusPort : this.PartitionFqdn;
			if (string.IsNullOrEmpty(text))
			{
				return new LdapDirectoryIdentifier(text, this.Port, flag, false);
			}
			return new LdapDirectoryIdentifier(text, flag, false);
		}

		// Token: 0x04000247 RID: 583
		private static Hookable<IADServerInfoMapper> hookableInstance = Hookable<IADServerInfoMapper>.Create(true, new ADServerInfo.ADServerInfoMapper());

		// Token: 0x04000248 RID: 584
		private readonly string fqdnPlusPort;

		// Token: 0x04000249 RID: 585
		private string siteName;

		// Token: 0x0400024A RID: 586
		private string writableNC;

		// Token: 0x0400024B RID: 587
		private string configNC;

		// Token: 0x0400024C RID: 588
		private string rootNC;

		// Token: 0x0400024D RID: 589
		private string schemaNC;

		// Token: 0x0400024E RID: 590
		private AuthType authType;

		// Token: 0x0400024F RID: 591
		private bool isServerSuitable;

		// Token: 0x04000250 RID: 592
		[NonSerialized]
		private int totalRequests;

		// Token: 0x04000251 RID: 593
		[NonSerialized]
		private int mapping;

		// Token: 0x02000077 RID: 119
		internal class ADServerInfoMapper : IADServerInfoMapper
		{
			// Token: 0x0600057E RID: 1406 RVA: 0x0001ECF7 File Offset: 0x0001CEF7
			public ADRole GetADRole(ADServerInfo adServerInfo)
			{
				ArgumentValidator.ThrowIfNull("adServerInfo", adServerInfo);
				if (adServerInfo.Port != 389)
				{
					return ADRole.GlobalCatalog;
				}
				return ADRole.DomainController;
			}

			// Token: 0x0600057F RID: 1407 RVA: 0x0001ED14 File Offset: 0x0001CF14
			public string GetMappedFqdn(string serverFqdn)
			{
				return serverFqdn ?? string.Empty;
			}

			// Token: 0x06000580 RID: 1408 RVA: 0x0001ED20 File Offset: 0x0001CF20
			public int GetMappedPortNumber(string serverFqdn, string dnsDomainName, int portNumber)
			{
				return portNumber;
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x0001ED23 File Offset: 0x0001CF23
			public AuthType GetMappedAuthType(string serverFqdn, AuthType authType)
			{
				return authType;
			}
		}
	}
}
