using System;
using System.IO;
using System.Runtime.InteropServices;
using Interop.NetFw;
using Microsoft.Win32;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000137 RID: 311
	[CLSCompliant(false)]
	public abstract class ExchangeFirewallRule
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x000185B3 File Offset: 0x000167B3
		internal string Name
		{
			get
			{
				if (string.IsNullOrEmpty(this.name))
				{
					this.name = string.Format("{0} ({1}-{2})", this.ComponentName, this.GetProtocolString(), this.GetDirectionString());
				}
				return this.name;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x000185EC File Offset: 0x000167EC
		internal NetFwRule NetFwFirewallRule
		{
			get
			{
				if (this.netfwFirewallRule == null)
				{
					try
					{
						this.netfwFirewallRule = this.FirewallPolicy.Rules.Item(this.Name);
					}
					catch (FileNotFoundException)
					{
						this.netfwFirewallRule = null;
					}
					catch (COMException)
					{
						this.netfwFirewallRule = null;
					}
				}
				return this.netfwFirewallRule;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000765 RID: 1893
		protected abstract string ComponentName { get; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000766 RID: 1894
		protected abstract IndirectStrings DescriptionIndirectString { get; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000767 RID: 1895
		protected abstract string ApplicationPath { get; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000768 RID: 1896
		protected abstract string ServiceName { get; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000769 RID: 1897
		protected abstract string LocalPorts { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00018658 File Offset: 0x00016858
		protected virtual bool InhibitApplicationPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001865B File Offset: 0x0001685B
		protected virtual bool InhibitServiceName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001865E File Offset: 0x0001685E
		protected virtual NET_FW_RULE_DIRECTION_ Direction
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00018661 File Offset: 0x00016861
		protected virtual NET_FW_IP_PROTOCOL_ Protocol
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00018664 File Offset: 0x00016864
		protected virtual IndirectStrings GroupingIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLGROUP_EXCHANGE;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001866B File Offset: 0x0001686B
		protected virtual string LocalAddresses
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00018672 File Offset: 0x00016872
		protected virtual string RemoteAddresses
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00018679 File Offset: 0x00016879
		protected virtual string RemotePorts
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00018680 File Offset: 0x00016880
		protected virtual NET_FW_PROFILE_TYPE2_ Profile
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00018687 File Offset: 0x00016887
		protected virtual NET_FW_ACTION_ Action
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001868A File Offset: 0x0001688A
		protected virtual bool Enabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001868D File Offset: 0x0001688D
		protected virtual bool EdgeTraversal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00018690 File Offset: 0x00016890
		private string Grouping
		{
			get
			{
				return "@%ExchangeInstallPath%\\Bin\\FirewallRes.dll,-" + (int)this.GroupingIndirectString;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x000186A7 File Offset: 0x000168A7
		private string Description
		{
			get
			{
				return "@%ExchangeInstallPath%\\Bin\\FirewallRes.dll,-" + (int)this.DescriptionIndirectString;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x000186BE File Offset: 0x000168BE
		private NetFwPolicy2 FirewallPolicy
		{
			get
			{
				if (this.firewallPolicy == null)
				{
					this.firewallPolicy = new NetFwPolicy2Class();
				}
				return this.firewallPolicy;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x000186D9 File Offset: 0x000168D9
		internal bool IsInstalled
		{
			get
			{
				return this.isInstalled;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000186E4 File Offset: 0x000168E4
		internal void Add()
		{
			if (this.NetFwFirewallRule == null)
			{
				NetFwRule netFwRule = new NetFwRuleClass();
				netFwRule.Name = this.Name;
				netFwRule.Description = this.Description;
				netFwRule.Grouping = this.Grouping;
				if (!this.InhibitApplicationPath && !string.IsNullOrEmpty(this.ApplicationPath))
				{
					netFwRule.ApplicationName = this.ApplicationPath;
				}
				if (!this.InhibitServiceName && !string.IsNullOrEmpty(this.ServiceName))
				{
					netFwRule.serviceName = this.ServiceName;
				}
				netFwRule.Protocol = this.Protocol;
				netFwRule.Direction = this.Direction;
				netFwRule.Profiles = this.Profile;
				netFwRule.Action = this.Action;
				netFwRule.Enabled = this.Enabled;
				netFwRule.LocalAddresses = this.LocalAddresses;
				netFwRule.LocalPorts = this.LocalPorts;
				netFwRule.RemoteAddresses = this.RemoteAddresses;
				netFwRule.RemotePorts = this.RemotePorts;
				netFwRule.EdgeTraversal = this.EdgeTraversal;
				this.FirewallPolicy.Rules.Add(netFwRule);
			}
			this.isInstalled = true;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000187F8 File Offset: 0x000169F8
		internal void Remove()
		{
			this.netfwFirewallRule = null;
			if (this.NetFwFirewallRule != null)
			{
				this.FirewallPolicy.Rules.Remove(this.Name);
			}
			this.isInstalled = false;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00018828 File Offset: 0x00016A28
		internal void SetLocalPort(string portString)
		{
			this.netfwFirewallRule = null;
			if (this.NetFwFirewallRule != null)
			{
				try
				{
					this.NetFwFirewallRule.LocalPorts = portString;
				}
				catch (COMException ex)
				{
					uint errorCode = (uint)ex.ErrorCode;
					uint num = errorCode;
					if (num == 2147500035U)
					{
						throw new InvalidOperationException("Invalid pointer while trying to set firewall local ports");
					}
					if (num == 2147942405U)
					{
						throw new UnauthorizedAccessException("Unauthorized to set firewall local ports");
					}
					switch (num)
					{
					case 2147942413U:
						throw new ArgumentException("Invalid firewall local ports parameter");
					case 2147942414U:
						throw new OutOfMemoryException("Out of memory while trying to set firewall local ports");
					default:
						throw;
					}
				}
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000188C0 File Offset: 0x00016AC0
		internal string GetLocalPorts()
		{
			this.netfwFirewallRule = null;
			if (this.NetFwFirewallRule != null)
			{
				try
				{
					return this.NetFwFirewallRule.LocalPorts;
				}
				catch (COMException ex)
				{
					uint errorCode = (uint)ex.ErrorCode;
					uint num = errorCode;
					if (num == 2147500035U)
					{
						throw new InvalidOperationException("Invalid pointer while trying to get firewall local ports");
					}
					if (num == 2147942405U)
					{
						throw new UnauthorizedAccessException("Unauthorized to get firewall local ports");
					}
					if (num != 2147942414U)
					{
						throw;
					}
					throw new OutOfMemoryException("Out of memory while trying to get firewall local ports");
				}
			}
			return null;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00018944 File Offset: 0x00016B44
		internal string ToStringDefaultValue()
		{
			return string.Format("{0}{1}Protocol={2} Direction={3}{4}LocalPorts='{5}'{6}EdgeTraverse={7}{8}InhibitApplicationPath={9} InhibitServiceName={10}{11}'{12}'{13}'{14}'{15}'{16}'{17}'{18}'", new object[]
			{
				this.ComponentName,
				Environment.NewLine,
				this.GetProtocolString(),
				this.GetDirectionString(),
				Environment.NewLine,
				this.LocalPorts,
				Environment.NewLine,
				this.EdgeTraversal,
				Environment.NewLine,
				this.InhibitApplicationPath,
				this.InhibitServiceName,
				Environment.NewLine,
				this.ApplicationPath,
				Environment.NewLine,
				this.ServiceName,
				Environment.NewLine,
				this.Description,
				Environment.NewLine,
				this.Grouping
			});
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00018A20 File Offset: 0x00016C20
		internal string ToStringInstalledValue()
		{
			return string.Format("{0}{1}Protocol={2} Direction={3}{4}LocalPorts='{5}'{6}EdgeTraverse={7}{8}InhibitApplicationPath={9} InhibitServiceName={10}{11}'{12}'{13}'{14}'{15}'{16}'{17}'{18}'", new object[]
			{
				this.ComponentName,
				Environment.NewLine,
				this.GetProtocolString(),
				this.GetDirectionString(),
				Environment.NewLine,
				this.GetLocalPorts(),
				Environment.NewLine,
				this.EdgeTraversal,
				Environment.NewLine,
				this.InhibitApplicationPath,
				this.InhibitServiceName,
				Environment.NewLine,
				this.ApplicationPath,
				Environment.NewLine,
				this.ServiceName,
				Environment.NewLine,
				this.Description,
				Environment.NewLine,
				this.Grouping
			});
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00018AFC File Offset: 0x00016CFC
		private static string GetExchangeInstallPath()
		{
			return (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", null);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00018B20 File Offset: 0x00016D20
		private string GetProtocolString()
		{
			string result = string.Empty;
			NET_FW_IP_PROTOCOL_ protocol = this.Protocol;
			if (protocol != 6)
			{
				if (protocol != 17)
				{
					if (protocol == 256)
					{
						result = "ANY";
					}
				}
				else
				{
					result = "UDP";
				}
			}
			else
			{
				result = "TCP";
			}
			return result;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00018B64 File Offset: 0x00016D64
		private string GetDirectionString()
		{
			string result = string.Empty;
			switch (this.Direction)
			{
			case 1:
				result = "In";
				break;
			case 2:
				result = "Out";
				break;
			}
			return result;
		}

		// Token: 0x040005F2 RID: 1522
		internal const string AnyPort = "*";

		// Token: 0x040005F3 RID: 1523
		internal const string RpcPorts = "RPC";

		// Token: 0x040005F4 RID: 1524
		internal const string RPCEpMapPorts = "RPC-EPMap";

		// Token: 0x040005F5 RID: 1525
		internal const string TeredoPorts = "Teredo";

		// Token: 0x040005F6 RID: 1526
		protected const string AnyAddress = "*";

		// Token: 0x040005F7 RID: 1527
		protected const string LocalSubnetAddress = "LocalSubnet";

		// Token: 0x040005F8 RID: 1528
		protected const string DNSAddress = "DNS";

		// Token: 0x040005F9 RID: 1529
		private const string ProtocolAny = "ANY";

		// Token: 0x040005FA RID: 1530
		private const string ProtocolTcp = "TCP";

		// Token: 0x040005FB RID: 1531
		private const string ProtocolUdp = "UDP";

		// Token: 0x040005FC RID: 1532
		private const string DirectionIn = "In";

		// Token: 0x040005FD RID: 1533
		private const string DirectionOut = "Out";

		// Token: 0x040005FE RID: 1534
		private const string RuleNameFormat = "{0} ({1}-{2})";

		// Token: 0x040005FF RID: 1535
		private const string IndirectStringPrefix = "@%ExchangeInstallPath%\\Bin\\FirewallRes.dll,-";

		// Token: 0x04000600 RID: 1536
		protected static readonly string ExchangeInstallPath = ExchangeFirewallRule.GetExchangeInstallPath();

		// Token: 0x04000601 RID: 1537
		private string name;

		// Token: 0x04000602 RID: 1538
		private NetFwPolicy2 firewallPolicy;

		// Token: 0x04000603 RID: 1539
		private NetFwRule netfwFirewallRule;

		// Token: 0x04000604 RID: 1540
		private bool isInstalled;
	}
}
