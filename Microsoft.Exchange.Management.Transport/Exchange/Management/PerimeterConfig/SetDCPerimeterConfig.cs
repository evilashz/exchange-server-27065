using System;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync.Ehf;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.PerimeterConfig
{
	// Token: 0x0200005E RID: 94
	[Cmdlet("Set", "DCPerimeterConfig", SupportsShouldProcess = true)]
	public class SetDCPerimeterConfig : DCPerimeterConfigTask
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000CBC2 File Offset: 0x0000ADC2
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000CBCA File Offset: 0x0000ADCA
		[Parameter]
		public MultiValuedProperty<IPAddress> OutboundIPAddresses
		{
			get
			{
				return this.outboundIPAddresses;
			}
			set
			{
				this.outboundIPAddresses = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000CBD3 File Offset: 0x0000ADD3
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000CBDB File Offset: 0x0000ADDB
		[Parameter]
		public MultiValuedProperty<IPAddress> DNSServerIPAddresses
		{
			get
			{
				return this.dnsServerIPAddresses;
			}
			set
			{
				this.dnsServerIPAddresses = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		[Parameter]
		public string FQDNTemplate
		{
			get
			{
				return this.fqdnTemplate;
			}
			set
			{
				this.fqdnTemplate = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000CBF5 File Offset: 0x0000ADF5
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000CBFD File Offset: 0x0000ADFD
		[Parameter]
		public SwitchParameter Force
		{
			get
			{
				return this.force;
			}
			set
			{
				this.force = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000CC06 File Offset: 0x0000AE06
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return new LocalizedString("Setting the DC Perimeter Configuration.  Have you shut down EdgeSync on all Hub servers in this AD forest?  Failure to do so can cause data corruption.");
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000CC14 File Offset: 0x0000AE14
		protected override void InternalValidate()
		{
			if (!this.force && !base.ShouldContinue(this.ConfirmationMessage))
			{
				base.WriteError(new InvalidOperationException("Make sure all EdgeSync services are shut down before running this task."), ErrorCategory.InvalidOperation, null);
			}
			if (this.dnsServerIPAddresses == null && this.fqdnTemplate == null && this.outboundIPAddresses == null)
			{
				base.WriteError(new ArgumentException("No parameters specified."), ErrorCategory.InvalidArgument, null);
			}
			if ((this.dnsServerIPAddresses == null && this.fqdnTemplate != null) || (this.dnsServerIPAddresses != null && this.fqdnTemplate == null))
			{
				base.WriteError(new ArgumentException("If one of DNSServerIPAddresses or FQDNTemplate is specified, both must be specified."), ErrorCategory.InvalidArgument, null);
			}
			if (this.fqdnTemplate != null && !this.fqdnTemplate.Contains("{0}"))
			{
				base.WriteError(new ArgumentException("FQDNTemplate must contain the string '{0}', which will be replaced by the site's partner ID when querying the DNS server."), ErrorCategory.InvalidArgument, null);
			}
			base.InternalValidate();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		internal override void InvokeWebService(IConfigurationSession session, EhfTargetServerConfig config, EhfProvisioningService provisioningService)
		{
			IPAddress[] array = null;
			IPAddress[] array2 = null;
			if (this.dnsServerIPAddresses != null && this.fqdnTemplate != null)
			{
				ADSite site = null;
				ADNotificationAdapter.TryRunADOperation(delegate()
				{
					site = ((ITopologyConfigurationSession)session).GetLocalSite();
				});
				if (site == null)
				{
					base.WriteError(new InvalidOperationException("Unable to find ADSite object"), ErrorCategory.InvalidOperation, null);
				}
				Dns dns = new Dns();
				dns.Timeout = TimeSpan.FromSeconds(30.0);
				dns.ServerList = new DnsServerList();
				dns.ServerList.Initialize(this.dnsServerIPAddresses.ToArray());
				array = this.ResolveInboundVirtualIPs(dns, site.PartnerId, this.fqdnTemplate);
			}
			if (this.outboundIPAddresses != null && this.outboundIPAddresses.Count > 0)
			{
				array2 = this.outboundIPAddresses.ToArray();
			}
			if (array != null || array2 != null)
			{
				CompanyResponseInfo companyResponseInfo = provisioningService.UpdateReseller(config.ResellerId, array, array2);
				if (companyResponseInfo.Status != ResponseStatus.Success)
				{
					this.HandleFailure(companyResponseInfo);
				}
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000CE24 File Offset: 0x0000B024
		private IPAddress[] ResolveInboundVirtualIPs(Dns dns, int partnerId, string fqdnTemplate)
		{
			string domainName = string.Format(fqdnTemplate, partnerId);
			IAsyncResult asyncResult = dns.BeginResolveToAddresses(domainName, AddressFamily.InterNetwork, null, null);
			asyncResult.AsyncWaitHandle.WaitOne();
			IPAddress[] result;
			DnsStatus dnsStatus = Dns.EndResolveToAddresses(asyncResult, out result);
			if (dnsStatus != DnsStatus.Success)
			{
				base.WriteError(new InvalidOperationException("Unable to resolve inbound IPs.  Dns status = " + dnsStatus), ErrorCategory.InvalidOperation, null);
			}
			return result;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000CE80 File Offset: 0x0000B080
		private void HandleFailure(ResponseInfo response)
		{
			string text = "none";
			if (response.TargetValue != null && response.TargetValue.Length > 0)
			{
				text = string.Join(", ", response.TargetValue);
			}
			string message = string.Format("Unable to update perimeter config: FaultId=<{0}>; FaultType=<{1}>; FaultDetail=<{2}>; Target=<{3}>; TargetValues=<{4}>", new object[]
			{
				response.Fault.Id,
				response.Status,
				response.Fault.Detail ?? "null",
				response.Target,
				text
			});
			base.WriteError(new InvalidOperationException(message), ErrorCategory.InvalidOperation, null);
		}

		// Token: 0x04000133 RID: 307
		private MultiValuedProperty<IPAddress> outboundIPAddresses;

		// Token: 0x04000134 RID: 308
		private MultiValuedProperty<IPAddress> dnsServerIPAddresses;

		// Token: 0x04000135 RID: 309
		private string fqdnTemplate;

		// Token: 0x04000136 RID: 310
		private SwitchParameter force;
	}
}
