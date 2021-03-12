using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AB1 RID: 2737
	[Cmdlet("New", "X400AuthoritativeDomain", SupportsShouldProcess = true)]
	public sealed class NewX400AuthoritativeDomain : NewSystemConfigurationObjectTask<X400AuthoritativeDomain>
	{
		// Token: 0x17001D56 RID: 7510
		// (get) Token: 0x060060E1 RID: 24801 RVA: 0x00194737 File Offset: 0x00192937
		// (set) Token: 0x060060E2 RID: 24802 RVA: 0x00194744 File Offset: 0x00192944
		[Parameter(Mandatory = true)]
		public X400Domain X400DomainName
		{
			get
			{
				return this.DataObject.X400DomainName;
			}
			set
			{
				this.DataObject.X400DomainName = value;
			}
		}

		// Token: 0x17001D57 RID: 7511
		// (get) Token: 0x060060E3 RID: 24803 RVA: 0x00194752 File Offset: 0x00192952
		// (set) Token: 0x060060E4 RID: 24804 RVA: 0x0019475F File Offset: 0x0019295F
		[Parameter]
		public bool X400ExternalRelay
		{
			get
			{
				return this.DataObject.X400ExternalRelay;
			}
			set
			{
				this.DataObject.X400ExternalRelay = value;
			}
		}

		// Token: 0x17001D58 RID: 7512
		// (get) Token: 0x060060E5 RID: 24805 RVA: 0x0019476D File Offset: 0x0019296D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAcceptedDomain(base.Name, this.X400DomainName.ToString());
			}
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x00194788 File Offset: 0x00192988
		internal static void ValidateNoDuplicates(X400AuthoritativeDomain domain, IConfigurationSession session, Task.TaskErrorLoggingDelegate errorWriter)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Guid, domain.Guid),
				new ComparisonFilter(ComparisonOperator.Equal, X400AuthoritativeDomainSchema.DomainName, domain.X400DomainName)
			});
			X400AuthoritativeDomain[] array = session.Find<X400AuthoritativeDomain>(session.GetOrgContainerId().GetDescendantId(domain.ParentPath), QueryScope.SubTree, filter, null, 1);
			if (array.Length > 0)
			{
				errorWriter(new DuplicateX400DomainException(domain.X400DomainName), ErrorCategory.ResourceExists, domain);
			}
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x00194808 File Offset: 0x00192A08
		protected override IConfigurable PrepareDataObject()
		{
			X400AuthoritativeDomain x400AuthoritativeDomain = (X400AuthoritativeDomain)base.PrepareDataObject();
			x400AuthoritativeDomain.SetId(this.ConfigurationSession, base.Name);
			return x400AuthoritativeDomain;
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x00194834 File Offset: 0x00192A34
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (TopologyProvider.IsAdamTopology())
			{
				base.WriteError(new CannotRunOnEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			NewX400AuthoritativeDomain.ValidateNoDuplicates(this.DataObject, this.ConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			base.InternalValidate();
			TaskLogger.LogExit();
		}
	}
}
