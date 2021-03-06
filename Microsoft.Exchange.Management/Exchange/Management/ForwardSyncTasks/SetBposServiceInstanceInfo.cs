using System;
using System.Management.Automation;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000378 RID: 888
	[Cmdlet("Set", "BposServiceInstanceInfo", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetBposServiceInstanceInfo : Task
	{
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x000864B8 File Offset: 0x000846B8
		// (set) Token: 0x06001F1C RID: 7964 RVA: 0x000864CF File Offset: 0x000846CF
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

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000864E2 File Offset: 0x000846E2
		// (set) Token: 0x06001F1E RID: 7966 RVA: 0x000864F9 File Offset: 0x000846F9
		[Parameter(Mandatory = true)]
		public Uri BackSyncUrl
		{
			get
			{
				return (Uri)base.Fields["BackSyncUrl"];
			}
			set
			{
				base.Fields["BackSyncUrl"] = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001F1F RID: 7967 RVA: 0x0008650C File Offset: 0x0008470C
		// (set) Token: 0x06001F20 RID: 7968 RVA: 0x00086523 File Offset: 0x00084723
		[Parameter(Mandatory = true)]
		public bool SupportsAuthorityTransfer
		{
			get
			{
				return (bool)base.Fields["SupportsAuthorityTransfer"];
			}
			set
			{
				base.Fields["SupportsAuthorityTransfer"] = value;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001F21 RID: 7969 RVA: 0x0008653B File Offset: 0x0008473B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetServiceInstanceInfo(this.Identity.ToString());
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00086550 File Offset: 0x00084750
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!this.BackSyncUrl.IsAbsoluteUri)
			{
				Exception exception = new BackSyncUrlNeedsToBeAbsoluteException();
				this.WriteError(exception, ErrorCategory.InvalidData, null, true);
			}
			if (this.BackSyncUrl.Scheme != Uri.UriSchemeHttps)
			{
				Exception exception2 = new BackSyncUrlInvalidProtcolFormatException();
				this.WriteError(exception2, ErrorCategory.InvalidData, null, true);
			}
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000865A8 File Offset: 0x000847A8
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			try
			{
				ResultCode? result = null;
				using (OnboardingService onboardingService = new MsoOnboardingService())
				{
					ServiceInstanceInfoValue serviceInstanceInfoValue = onboardingService.GetServiceInstanceInfo(this.Identity.ToString());
					if (serviceInstanceInfoValue == null)
					{
						serviceInstanceInfoValue = new ServiceInstanceInfoValue();
					}
					bool flag = false;
					if (serviceInstanceInfoValue.Endpoint != null)
					{
						foreach (ServiceEndpointValue serviceEndpointValue in serviceInstanceInfoValue.Endpoint)
						{
							if (string.Compare(serviceEndpointValue.Name, "BackSyncPSConnectionURI", true) == 0)
							{
								flag = true;
								serviceEndpointValue.Address = this.BackSyncUrl.ToString();
								break;
							}
						}
					}
					else
					{
						serviceInstanceInfoValue.Endpoint = new ServiceEndpointValue[0];
					}
					if (!flag)
					{
						ServiceEndpointValue serviceEndpointValue2 = new ServiceEndpointValue();
						serviceEndpointValue2.Name = "BackSyncPSConnectionURI";
						serviceEndpointValue2.Address = this.BackSyncUrl.ToString();
						int num = serviceInstanceInfoValue.Endpoint.Length + 1;
						ServiceEndpointValue[] array = new ServiceEndpointValue[num];
						serviceInstanceInfoValue.Endpoint.CopyTo(array, 0);
						array[num - 1] = serviceEndpointValue2;
						serviceInstanceInfoValue.Endpoint = array;
					}
					if (this.SupportsAuthorityTransfer)
					{
						bool flag2 = false;
						if (serviceInstanceInfoValue.Any != null)
						{
							foreach (XmlElement xmlElement in serviceInstanceInfoValue.Any)
							{
								if (string.Compare(xmlElement.Name, "SupportsAuthorityTransfer", true) == 0)
								{
									flag2 = true;
									break;
								}
							}
						}
						else
						{
							serviceInstanceInfoValue.Any = new XmlElement[0];
						}
						if (!flag2)
						{
							XmlDocument xmlDocument = new SafeXmlDocument();
							XmlElement xmlElement2 = xmlDocument.CreateElement("SupportsAuthorityTransfer", "http://schemas.microsoft.com/online/directoryservices/exchange/2011/01");
							int num2 = serviceInstanceInfoValue.Any.Length + 1;
							XmlElement[] array2 = new XmlElement[num2];
							serviceInstanceInfoValue.Any.CopyTo(array2, 0);
							array2[num2 - 1] = xmlElement2;
							serviceInstanceInfoValue.Any = array2;
						}
					}
					else if (serviceInstanceInfoValue.Any != null)
					{
						XmlElement[] array3 = new XmlElement[0];
						foreach (XmlElement xmlElement3 in serviceInstanceInfoValue.Any)
						{
							if (string.Compare(xmlElement3.Name, "SupportsAuthorityTransfer", true) != 0)
							{
								Array.Resize<XmlElement>(ref array3, array3.Length + 1);
								array3[array3.Length - 1] = xmlElement3;
							}
						}
						serviceInstanceInfoValue.Any = array3;
					}
					result = new ResultCode?(onboardingService.SetServiceInstanceInfo(this.Identity.ToString(), serviceInstanceInfoValue));
					if (result == null || result.Value != ResultCode.Success)
					{
						string errorStringForResultcode = MsoOnboardingService.GetErrorStringForResultcode(result);
						this.WriteError(new CouldNotUpdateServiceInstanceMapException(errorStringForResultcode), ErrorCategory.InvalidData, null, true);
					}
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
