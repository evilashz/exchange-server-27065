using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000003 RID: 3
	internal class AmServerName : IEquatable<AmServerName>, IComparable<AmServerName>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000002D0
		public static StringComparer Comparer
		{
			get
			{
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020D7 File Offset: 0x000002D7
		public static StringComparison Comparison
		{
			get
			{
				return StringComparison.OrdinalIgnoreCase;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020DA File Offset: 0x000002DA
		internal AmServerName()
		{
			this.Initialize(string.Empty, string.Empty, null);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F3 File Offset: 0x000002F3
		internal AmServerName(string netbiosName, string domainSuffix)
		{
			this.Initialize(netbiosName, domainSuffix, null);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002104 File Offset: 0x00000304
		internal AmServerName(string serverName) : this(serverName, true)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002110 File Offset: 0x00000310
		internal AmServerName(string serverName, bool throwOnFqdnError)
		{
			if (!string.IsNullOrEmpty(serverName))
			{
				int num = serverName.IndexOf(".");
				if (num == -1)
				{
					string fqdn = AmServerNameCache.Instance.GetFqdn(serverName, throwOnFqdnError);
					num = fqdn.IndexOf(".");
					SharedDiag.RetailAssert(num != -1, "fqdn resolution should have thrown", new object[0]);
					serverName = fqdn;
				}
				string netbiosName = serverName.Substring(0, num);
				int num2 = num + 1;
				string dnsSuffix = serverName.Substring(num2, serverName.Length - num2);
				this.Initialize(netbiosName, dnsSuffix, serverName);
				return;
			}
			this.Initialize(string.Empty, string.Empty, null);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A9 File Offset: 0x000003A9
		internal AmServerName(ADObjectId serverId) : this(serverId.Name)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021B7 File Offset: 0x000003B7
		internal AmServerName(Server server) : this(server.Fqdn)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021C5 File Offset: 0x000003C5
		internal AmServerName(MiniServer miniServer) : this(miniServer.Fqdn)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021D3 File Offset: 0x000003D3
		internal AmServerName(AmServerName other) : this(other.Fqdn)
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021E1 File Offset: 0x000003E1
		internal static AmServerName Empty
		{
			get
			{
				return AmServerName.sm_emptyInstance;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021E8 File Offset: 0x000003E8
		internal static void TestResetLocalComputerName()
		{
			AmServerName.sm_localComputerName = null;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021F0 File Offset: 0x000003F0
		internal static AmServerName LocalComputerName
		{
			get
			{
				if (AmServerName.sm_localComputerName == null)
				{
					try
					{
						AmServerName.sm_localComputerName = new AmServerName(SharedDependencies.ManagementClassHelper.LocalComputerFqdn);
					}
					catch (CannotGetComputerNameException ex)
					{
						throw new AmGetFqdnFailedADErrorException("AmServerName.LocalComputerName", ex.Message, ex);
					}
				}
				return AmServerName.sm_localComputerName;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002244 File Offset: 0x00000444
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000224C File Offset: 0x0000044C
		internal bool IsEmpty { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002255 File Offset: 0x00000455
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000225D File Offset: 0x0000045D
		internal string NetbiosName { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002266 File Offset: 0x00000466
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000226E File Offset: 0x0000046E
		internal string DnsSuffix { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002277 File Offset: 0x00000477
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000227F File Offset: 0x0000047F
		internal string Fqdn
		{
			get
			{
				return this.m_fqdn;
			}
			private set
			{
				this.m_fqdn = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002288 File Offset: 0x00000488
		internal bool IsLocalComputerName
		{
			get
			{
				return AmServerName.IsEqual(this, AmServerName.LocalComputerName);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002295 File Offset: 0x00000495
		public static string GetSimpleName(string nodeName)
		{
			if (string.IsNullOrEmpty(nodeName))
			{
				return MachineName.Local;
			}
			if (nodeName.Contains("."))
			{
				return nodeName.Substring(0, nodeName.IndexOf('.'));
			}
			return nodeName;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022C3 File Offset: 0x000004C3
		public bool Equals(AmServerName dst)
		{
			return AmServerName.IsEqual(this, dst);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022CC File Offset: 0x000004CC
		public int CompareTo(AmServerName other)
		{
			if (other == null)
			{
				return 1;
			}
			return string.Compare(this.Fqdn, other.Fqdn, AmServerName.Comparison);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022EC File Offset: 0x000004EC
		public override bool Equals(object obj)
		{
			AmServerName amServerName = obj as AmServerName;
			return amServerName != null && AmServerName.IsEqual(this, amServerName);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000230C File Offset: 0x0000050C
		public override string ToString()
		{
			return this.Fqdn;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002314 File Offset: 0x00000514
		public override int GetHashCode()
		{
			return SharedHelper.GetStringIHashCode(this.Fqdn);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002321 File Offset: 0x00000521
		internal static bool IsEqual(AmServerName src, AmServerName dst)
		{
			return object.ReferenceEquals(src, dst) || (src != null && dst != null && SharedHelper.StringIEquals(src.Fqdn, dst.Fqdn));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002347 File Offset: 0x00000547
		internal static bool IsNullOrEmpty(AmServerName serverName)
		{
			return serverName == null || serverName.IsEmpty;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002358 File Offset: 0x00000558
		internal static bool IsArrayEquals(AmServerName[] left, AmServerName[] right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (!AmServerName.IsEqual(left[i], right[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000239C File Offset: 0x0000059C
		private void Initialize(string netbiosName, string dnsSuffix, string fqdn)
		{
			this.NetbiosName = netbiosName.ToLowerInvariant();
			this.DnsSuffix = dnsSuffix.ToLowerInvariant();
			if (string.IsNullOrEmpty(this.NetbiosName) && string.IsNullOrEmpty(this.DnsSuffix))
			{
				this.Fqdn = string.Empty;
				this.IsEmpty = true;
				return;
			}
			if (fqdn == null)
			{
				this.Fqdn = string.Format("{0}.{1}", this.NetbiosName, this.DnsSuffix);
			}
			else
			{
				this.Fqdn = fqdn;
			}
			this.IsEmpty = false;
		}

		// Token: 0x04000001 RID: 1
		private static AmServerName sm_emptyInstance = new AmServerName();

		// Token: 0x04000002 RID: 2
		private static AmServerName sm_localComputerName;

		// Token: 0x04000003 RID: 3
		private string m_fqdn;
	}
}
