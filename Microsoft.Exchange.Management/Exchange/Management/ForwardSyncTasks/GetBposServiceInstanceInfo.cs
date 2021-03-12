using System;
using System.Management.Automation;
using System.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000349 RID: 841
	[Cmdlet("Get", "BposServiceInstanceInfo", DefaultParameterSetName = "Identity")]
	public sealed class GetBposServiceInstanceInfo : Task
	{
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0008050C File Offset: 0x0007E70C
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x00080523 File Offset: 0x0007E723
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public ServiceInstanceId Identity
		{
			get
			{
				return (ServiceInstanceId)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x00080538 File Offset: 0x0007E738
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			try
			{
				ServiceInstanceInfoValue serviceInstanceInfoValue = null;
				using (OnboardingService onboardingService = new MsoOnboardingService())
				{
					serviceInstanceInfoValue = onboardingService.GetServiceInstanceInfo(this.Identity.ToString());
				}
				if (serviceInstanceInfoValue != null)
				{
					Uri backSyncUrl = null;
					if (serviceInstanceInfoValue.Endpoint != null)
					{
						foreach (ServiceEndpointValue serviceEndpointValue in serviceInstanceInfoValue.Endpoint)
						{
							if (string.Compare(serviceEndpointValue.Name, "BackSyncPSConnectionURI", true) == 0)
							{
								backSyncUrl = new Uri(serviceEndpointValue.Address);
								break;
							}
						}
					}
					bool authorityTransferIsSupported = false;
					if (serviceInstanceInfoValue.Any != null)
					{
						foreach (XmlElement xmlElement in serviceInstanceInfoValue.Any)
						{
							if (string.Compare(xmlElement.Name, "SupportsAuthorityTransfer", true) == 0)
							{
								authorityTransferIsSupported = true;
								break;
							}
						}
					}
					BposServiceInstanceInfo sendToPipeline = new BposServiceInstanceInfo(this.Identity, "BackSyncPSConnectionURI", backSyncUrl, authorityTransferIsSupported);
					base.WriteObject(sendToPipeline);
				}
			}
			catch (CouldNotCreateMsoOnboardingServiceException exception)
			{
				this.WriteError(exception, ErrorCategory.ObjectNotFound, null, true);
			}
			catch (InvalidServiceInstanceMapXmlFormatException exception2)
			{
				this.WriteError(exception2, ErrorCategory.InvalidData, null, true);
			}
			catch (Exception exception3)
			{
				this.WriteError(exception3, ErrorCategory.InvalidOperation, null, true);
			}
		}
	}
}
