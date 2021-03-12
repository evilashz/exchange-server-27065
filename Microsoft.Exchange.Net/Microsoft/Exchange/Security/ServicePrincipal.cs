using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000AE1 RID: 2785
	internal class ServicePrincipal : IPrincipal
	{
		// Token: 0x06003BC2 RID: 15298 RVA: 0x000997F2 File Offset: 0x000979F2
		public ServicePrincipal(IIdentity identity, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("identity", identity);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.Identity = identity;
			this.Tracer = tracer;
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x0009981E File Offset: 0x00097A1E
		// (set) Token: 0x06003BC4 RID: 15300 RVA: 0x00099826 File Offset: 0x00097A26
		public IIdentity Identity { get; private set; }

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x0009982F File Offset: 0x00097A2F
		// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x00099837 File Offset: 0x00097A37
		private protected ITracer Tracer { protected get; private set; }

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x00099840 File Offset: 0x00097A40
		protected virtual double LocalIPCacheRefreshInMilliseconds
		{
			get
			{
				return ServicePrincipal.DefaultLocalIPAddressesCacheRefreshInterval;
			}
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x00099848 File Offset: 0x00097A48
		public bool IsInRole(string role)
		{
			if (string.IsNullOrWhiteSpace(role))
			{
				return true;
			}
			WindowsIdentity windowsIdentity = this.Identity as WindowsIdentity;
			if (windowsIdentity == null)
			{
				this.Tracer.TraceError<string>((long)this.GetHashCode(), "ServicePrincipal.IsInRole: Unsupported IIdentity. Identity type {0}", this.Identity.GetType().FullName);
				return false;
			}
			WindowsPrincipal principal = new WindowsPrincipal(windowsIdentity);
			string[] array = role.Split(ServicePrincipal.RoleSeparator, StringSplitOptions.RemoveEmptyEntries);
			foreach (string role2 in array)
			{
				if (!this.IsInRoleInternal(principal, role2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000998DC File Offset: 0x00097ADC
		protected virtual bool IsInRoleInternal(WindowsPrincipal principal, string role)
		{
			bool flag;
			if (role != null)
			{
				if (role == "LocalAdministrators")
				{
					flag = principal.IsInRole(WindowsBuiltInRole.Administrator);
					this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ServicePrincipal.IsInternalInRole: User is {0}Administrator.", flag ? string.Empty : "NOT ");
					return flag;
				}
				if (role == "LocalCall")
				{
					flag = this.IsLocalCall();
					this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ServicePrincipal.IsInternalInRole: User is {0}LocalCall.", flag ? string.Empty : "NOT ");
					return flag;
				}
				if (role == "UserService")
				{
					SecurityIdentifier securityIdentifier = this.Identity.GetSecurityIdentifier();
					flag = (securityIdentifier.IsWellKnown(WellKnownSidType.NetworkServiceSid) || securityIdentifier.IsWellKnown(WellKnownSidType.LocalSystemSid));
					this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ServicePrincipal.IsInternalInRole: User is {0}NetworkServiceSid or LocalSystemSid.", flag ? string.Empty : "NOT ");
					return flag;
				}
			}
			flag = principal.IsInRole(role);
			return flag;
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x000999F0 File Offset: 0x00097BF0
		private bool IsLocalCall()
		{
			Breadcrumbs<byte> breadcrumbs = new Breadcrumbs<byte>(8);
			if (OperationContext.Current == null)
			{
				return false;
			}
			breadcrumbs.Drop(1);
			if (OperationContext.Current.IncomingMessageProperties == null)
			{
				return false;
			}
			breadcrumbs.Drop(2);
			RemoteEndpointMessageProperty remoteEndpointMessageProperty = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
			if (remoteEndpointMessageProperty == null || string.IsNullOrEmpty(remoteEndpointMessageProperty.Address))
			{
				this.Tracer.TraceDebug((long)this.GetHashCode(), "ServicePrincipal.IsLocalCall: clientEndpoint is null or address is empty");
				return false;
			}
			breadcrumbs.Drop(3);
			if ("localhost".Equals(remoteEndpointMessageProperty.Address, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			breadcrumbs.Drop(4);
			IPAddress address;
			if (!IPAddress.TryParse(remoteEndpointMessageProperty.Address, out address))
			{
				this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ServicePrincipal.IsLocalCall: Unable to parse {0} to an IP", remoteEndpointMessageProperty.Address);
				return false;
			}
			breadcrumbs.Drop(5);
			if (IPAddress.IsLoopback(address))
			{
				this.Tracer.TraceDebug((long)this.GetHashCode(), "ServicePrincipal.IsLocalCall: Client has loopback address");
				return true;
			}
			breadcrumbs.Drop(6);
			this.RefreshMachineIpAddressesIfRequired();
			List<IPAddress> list = ServicePrincipal.localIPAddresses;
			if (list != null)
			{
				breadcrumbs.Drop(7);
				IPAddress ipaddress = list.Find((IPAddress x) => x.Equals(address));
				this.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "ServicePrincipal.IsLocalCall: Ip {0} {1}found in local set of IPs", remoteEndpointMessageProperty.Address, (ipaddress == null) ? "NOT " : string.Empty);
				return null != ipaddress;
			}
			breadcrumbs.Drop(8);
			this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ServicePrincipal.IsLocalCall: Ip not found in local set of ips {0}", remoteEndpointMessageProperty.Address);
			return false;
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x00099B90 File Offset: 0x00097D90
		private void RefreshMachineIpAddressesIfRequired()
		{
			if (-1 != ServicePrincipal.lastIpAddressRefreshTick && TickDiffer.GetTickDifference(ServicePrincipal.lastIpAddressRefreshTick, Environment.TickCount) <= this.LocalIPCacheRefreshInMilliseconds)
			{
				return;
			}
			if (Interlocked.Increment(ref ServicePrincipal.refreshingLocalIpAddresses) != 1)
			{
				Interlocked.Decrement(ref ServicePrincipal.refreshingLocalIpAddresses);
				return;
			}
			List<IPAddress> list = null;
			try
			{
				list = ComputerInformation.GetLocalIPAddresses();
			}
			catch (NetworkInformationException arg)
			{
				this.Tracer.TraceError<Exception>((long)this.GetHashCode(), "ServicePrincipal.RefreshMachineIpAddressesIfRequired: Unable to get local IP addresses {0}", arg);
			}
			finally
			{
				Interlocked.Decrement(ref ServicePrincipal.refreshingLocalIpAddresses);
				if (list != null)
				{
					ServicePrincipal.localIPAddresses = list;
					Interlocked.Exchange(ref ServicePrincipal.lastIpAddressRefreshTick, Environment.TickCount);
				}
			}
		}

		// Token: 0x040034A1 RID: 13473
		public const string AndRole = "+";

		// Token: 0x040034A2 RID: 13474
		public const string LocalAdministrators = "LocalAdministrators";

		// Token: 0x040034A3 RID: 13475
		public const string LocalCall = "LocalCall";

		// Token: 0x040034A4 RID: 13476
		public const string UserService = "UserService";

		// Token: 0x040034A5 RID: 13477
		private const string LocalHost = "localhost";

		// Token: 0x040034A6 RID: 13478
		private static readonly double DefaultLocalIPAddressesCacheRefreshInterval = TimeSpan.FromHours(24.0).TotalMilliseconds;

		// Token: 0x040034A7 RID: 13479
		private static readonly string[] RoleSeparator = new string[]
		{
			"+"
		};

		// Token: 0x040034A8 RID: 13480
		private static List<IPAddress> localIPAddresses;

		// Token: 0x040034A9 RID: 13481
		private static int refreshingLocalIpAddresses = 0;

		// Token: 0x040034AA RID: 13482
		private static int lastIpAddressRefreshTick = -1;
	}
}
