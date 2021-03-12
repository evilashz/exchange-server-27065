using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000177 RID: 375
	internal class UMSipPeer
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x0002B838 File Offset: 0x00029A38
		public UMSipPeer(UMSmartHost address, int port, bool allowOutboundCalls, bool useMutualTls, IPAddressFamily ipAddressFamily) : this(address, port, allowOutboundCalls, useMutualTls, false, ipAddressFamily)
		{
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002B848 File Offset: 0x00029A48
		public UMSipPeer(UMSmartHost address, int port, bool allowOutboundCalls, bool useMutualTls, bool isOcs, IPAddressFamily ipAddressFamily)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "UMSipPeer::ctor(Endpoint: {0}:{1} outCallsAllowed={2} IPAddressFamily={3})", new object[]
			{
				address,
				port,
				allowOutboundCalls,
				ipAddressFamily
			});
			this.Address = address;
			this.UseMutualTLS = useMutualTls;
			this.AllowOutboundCalls = allowOutboundCalls;
			this.ResolvedIPAddress = new List<IPAddress>();
			this.IsOcs = isOcs;
			this.IPAddressFamily = ipAddressFamily;
			if (port == 0)
			{
				port = Utils.GetRedirectPort(this.UseMutualTLS);
			}
			this.Port = port;
			if (address.IsIPAddress)
			{
				this.ResolvedIPAddress.Add(address.Address);
			}
			this.NextHopForOutboundRouting = this;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002B900 File Offset: 0x00029B00
		public static UMSipPeer CreateForTlsAuth(string fqdn)
		{
			return new UMSipPeer(new UMSmartHost(fqdn), 10000, false, true, IPAddressFamily.Any);
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0002B915 File Offset: 0x00029B15
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0002B91D File Offset: 0x00029B1D
		public bool AllowOutboundCalls { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0002B926 File Offset: 0x00029B26
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0002B92E File Offset: 0x00029B2E
		public List<IPAddress> ResolvedIPAddress { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002B937 File Offset: 0x00029B37
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0002B93F File Offset: 0x00029B3F
		public virtual bool IsOcs { get; private set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0002B948 File Offset: 0x00029B48
		public virtual string Name
		{
			get
			{
				return this.Address.ToString();
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0002B955 File Offset: 0x00029B55
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0002B95D File Offset: 0x00029B5D
		public UMSmartHost Address { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0002B966 File Offset: 0x00029B66
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0002B96E File Offset: 0x00029B6E
		public IPAddressFamily IPAddressFamily { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0002B977 File Offset: 0x00029B77
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0002B97F File Offset: 0x00029B7F
		public int Port { get; private set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0002B988 File Offset: 0x00029B88
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0002B990 File Offset: 0x00029B90
		public bool UseMutualTLS { get; private set; }

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002B99C File Offset: 0x00029B9C
		public virtual UMIPGateway ToUMIPGateway(OrganizationId orgId)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "UMSipPeer.ToUMIPGateway - {0}", new object[]
			{
				orgId
			});
			if (CommonConstants.UseDataCenterCallRouting && OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				ExAssert.RetailAssert(false, "Incorrectly scoped orgId - OrganizationalUnit = '{0}', ConfigurationUnit = '{1}'. Both OrganizationalUnit and ConfigurationUnit should be non-null.", new object[]
				{
					(orgId.OrganizationalUnit != null) ? orgId.OrganizationalUnit.ToString() : "<null>",
					(orgId.ConfigurationUnit != null) ? orgId.ConfigurationUnit.ToString() : "<null>"
				});
			}
			return new UMIPGateway
			{
				Port = this.Port,
				Name = this.Name,
				Address = this.Address,
				OutcallsAllowed = this.AllowOutboundCalls,
				MessageWaitingIndicatorAllowed = true,
				OrganizationId = orgId,
				IPAddressFamily = this.IPAddressFamily
			};
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0002BA82 File Offset: 0x00029C82
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0002BA8A File Offset: 0x00029C8A
		public UMSipPeer NextHopForOutboundRouting { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0002BA94 File Offset: 0x00029C94
		public string AddressWithTransport
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}:{1}{2}", new object[]
				{
					this.Address,
					this.Port,
					this.UseMutualTLS ? ";transport=TLS" : ";transport=TCP"
				});
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002BAE8 File Offset: 0x00029CE8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "({0} {1}:{2} {3} outbound={4} secured={5} )", new object[]
			{
				this.Name,
				this.Address,
				this.Port,
				this.IPAddressFamily,
				this.AllowOutboundCalls,
				this.UseMutualTLS
			});
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002BB58 File Offset: 0x00029D58
		public string ToHostPortString()
		{
			return this.Address.ToString() + ":" + this.Port.ToString();
		}
	}
}
