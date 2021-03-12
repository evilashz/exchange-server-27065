using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Exchange.Datacenter.Management.ActiveMonitoring;
using Microsoft.Exchange.Management.Powershell.CentralAdmin;
using Microsoft.Office.Management.Azure;

namespace Microsoft.Forefront.Monitoring.Management.OutsideIn
{
	// Token: 0x02000002 RID: 2
	[Cmdlet("Get", "AzureDeploymentVips")]
	public class GetAzureDeploymentVipsCommand : PSCmdlet
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string Filter { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		[Parameter(Mandatory = false)]
		public int MaxDegreeOfParallelism { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020F2 File Offset: 0x000002F2
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020FA File Offset: 0x000002FA
		[Parameter(Mandatory = false)]
		public string Cloud { get; set; }

		// Token: 0x06000007 RID: 7 RVA: 0x00002104 File Offset: 0x00000304
		public new void WriteVerbose(string text)
		{
			base.WriteVerbose(string.Format("[{0}] {1}", DateTime.UtcNow.ToLongTimeString(), text));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022B4 File Offset: 0x000004B4
		protected override void ProcessRecord()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			X509Certificate2 azureManagementCertificate = CommonHelper.GetCertificate(CommonHelper.GetSecureValue("CertificateSubject"));
			if (azureManagementCertificate == null)
			{
				throw new Exception(string.Format("We were unable to find the certificate with the following subject: {0}. Without this certificate, we can't get the Azure Deployment Vip.", CommonHelper.GetSecureValue("CertificateSubject")));
			}
			IEnumerable<AzureInstance> azureInstances = AzureResourceHelper.GetAzureInstances(this.Filter);
			this.WriteVerbose(string.Format("Found {0} Azure instances.", azureInstances.Count<AzureInstance>()));
			foreach (AzureInstance azureInstance2 in azureInstances)
			{
				if (azureInstance2 == null)
				{
					this.WriteVerbose("Found one AzureInstance object null.");
				}
				else
				{
					if (string.IsNullOrWhiteSpace(azureInstance2.Name))
					{
						this.WriteVerbose("Found one AzureInstance without a name.");
					}
					if (string.IsNullOrWhiteSpace(azureInstance2.AzureSubscription))
					{
						this.WriteVerbose("Found one AzureInstance without a subscription.");
					}
				}
			}
			HashSet<IPAddress> vipIps = new HashSet<IPAddress>();
			ConcurrentQueue<GetAzureDeploymentVipsCommand.GetAzureDeploymentVipsException> exceptions = new ConcurrentQueue<GetAzureDeploymentVipsCommand.GetAzureDeploymentVipsException>();
			Parallel.ForEach<AzureInstance>(azureInstances, new ParallelOptions
			{
				MaxDegreeOfParallelism = ((this.MaxDegreeOfParallelism != 0) ? this.MaxDegreeOfParallelism : 10)
			}, delegate(AzureInstance azureInstance)
			{
				try
				{
					XElement xelement = CommonHelper.RetryAction<XElement>(new Func<XElement>(delegate()
					{
						DeploymentManagement deploymentManagement = new DeploymentManagement();
						string text = string.IsNullOrWhiteSpace(this.Cloud) ? "windows.net" : this.Cloud;
						return deploymentManagement.GetDeploymentInfo(azureInstance.Name, azureInstance.AzureSubscription, text, azureManagementCertificate, "Production", "2012-03-01");
					}), new object[0]);
					HashSet<IPAddress> vipIps;
					lock (vipIps)
					{
						foreach (IPAddress item in IPAddressManagement.GetUniqueVips(xelement))
						{
							vipIps.Add(item);
						}
					}
				}
				catch (Exception inner)
				{
					exceptions.Enqueue(new GetAzureDeploymentVipsCommand.GetAzureDeploymentVipsException(azureInstance, inner));
				}
			});
			if (exceptions.Count > 0)
			{
				this.WriteVerbose(string.Format("Found {0} exceptions. Unable to retrieve the VIP for the following instances: {1}.", exceptions.Count, string.Join(", ", from e in exceptions
				select e.AzureInstance.Name)));
				foreach (GetAzureDeploymentVipsCommand.GetAzureDeploymentVipsException ex in exceptions)
				{
					base.WriteWarning(ex.ToString());
				}
			}
			this.WriteVerbose(string.Format("Completed in {0} seconds. We found {1} IPs ({2}).", stopwatch.Elapsed.TotalSeconds, vipIps.Count, string.Join(", ", from ip in vipIps
			select ip.ToString())));
			foreach (IPAddress ipaddress in vipIps)
			{
				if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					base.WriteObject(ipaddress);
				}
				else
				{
					this.WriteVerbose(string.Format("Skipping IP {0} because it is not an IPv4 address", ipaddress.ToString()));
				}
			}
		}

		// Token: 0x02000003 RID: 3
		[Serializable]
		public class GetAzureDeploymentVipsException : Exception
		{
			// Token: 0x0600000C RID: 12 RVA: 0x00002584 File Offset: 0x00000784
			public GetAzureDeploymentVipsException()
			{
			}

			// Token: 0x0600000D RID: 13 RVA: 0x0000258C File Offset: 0x0000078C
			public GetAzureDeploymentVipsException(AzureInstance azureInstance, Exception inner) : base(string.Empty, inner)
			{
				this.AzureInstance = azureInstance;
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600000E RID: 14 RVA: 0x000025A1 File Offset: 0x000007A1
			// (set) Token: 0x0600000F RID: 15 RVA: 0x000025A9 File Offset: 0x000007A9
			public AzureInstance AzureInstance { get; private set; }

			// Token: 0x06000010 RID: 16 RVA: 0x000025B2 File Offset: 0x000007B2
			public override string ToString()
			{
				return string.Format("AzureInstanceName = {0}; Exception = {1}", this.AzureInstance.Name, base.ToString());
			}
		}
	}
}
