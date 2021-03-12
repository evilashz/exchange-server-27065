using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000214 RID: 532
	[Cmdlet("New", "ServiceConnectionPoint")]
	public sealed class NewServiceConnectionPoint : NewSystemConfigurationObjectTask<ADServiceConnectionPoint>
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x000502CD File Offset: 0x0004E4CD
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x000502F3 File Offset: 0x0004E4F3
		[Parameter(Mandatory = true, ParameterSetName = "TrustedHoster")]
		[Parameter(Mandatory = false)]
		public SwitchParameter TrustedHoster
		{
			get
			{
				return (SwitchParameter)(base.Fields["TrustedHoster"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["TrustedHoster"] = value;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0005030B File Offset: 0x0004E50B
		// (set) Token: 0x06001237 RID: 4663 RVA: 0x00050322 File Offset: 0x0004E522
		[Parameter(Mandatory = true, ParameterSetName = "TrustedHoster")]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> TrustedHostnames
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["TrustedHostnames"];
			}
			set
			{
				base.Fields["TrustedHostnames"] = value;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00050335 File Offset: 0x0004E535
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x0005035B File Offset: 0x0004E55B
		[Parameter(Mandatory = true, ParameterSetName = "DomainToApplicationUri")]
		[Parameter(Mandatory = false)]
		public SwitchParameter DomainToApplicationUri
		{
			get
			{
				return (SwitchParameter)(base.Fields["DomainToApplicationUri"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DomainToApplicationUri"] = value;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00050373 File Offset: 0x0004E573
		// (set) Token: 0x0600123B RID: 4667 RVA: 0x0005038A File Offset: 0x0004E58A
		[Parameter(Mandatory = false)]
		[Parameter(Mandatory = true, ParameterSetName = "DomainToApplicationUri")]
		public MultiValuedProperty<string> Domains
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["Domains"];
			}
			set
			{
				base.Fields["Domains"] = value;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0005039D File Offset: 0x0004E59D
		// (set) Token: 0x0600123D RID: 4669 RVA: 0x000503B4 File Offset: 0x0004E5B4
		[Parameter(Mandatory = false)]
		[Parameter(Mandatory = true, ParameterSetName = "DomainToApplicationUri")]
		public string ApplicationUri
		{
			get
			{
				return (string)base.Fields["ApplicationUri"];
			}
			set
			{
				base.Fields["ApplicationUri"] = value;
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000503C8 File Offset: 0x0004E5C8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.DataObject.Keywords = new MultiValuedProperty<string>();
			if (this.TrustedHoster)
			{
				this.DataObject.Keywords.Add("D3614C7C-D214-4F1F-BD4C-00D91C67F93F");
				this.DataObject.ServiceBindingInformation = this.TrustedHostnames;
			}
			else if (this.DomainToApplicationUri)
			{
				this.DataObject.Keywords.Add("E1AA5F5E-2341-4FCB-8560-E3AB6F081468");
				foreach (string str in this.Domains)
				{
					this.DataObject.Keywords.Add("Domain=" + str);
				}
				this.DataObject.ServiceBindingInformation = new MultiValuedProperty<string>(new string[]
				{
					this.ApplicationUri
				});
			}
			else
			{
				base.WriteError(new NewServiceConnectionPointInvalidParametersException(), (ErrorCategory)1001, null);
			}
			ITopologyConfigurationSession topologyConfigurationSession = (ITopologyConfigurationSession)base.DataSession;
			ADObjectId childId = topologyConfigurationSession.GetAutoDiscoverGlobalContainerId().GetChildId(this.DataObject.Name);
			this.DataObject.SetId(childId);
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00050514 File Offset: 0x0004E714
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			catch (ADObjectAlreadyExistsException)
			{
			}
		}
	}
}
