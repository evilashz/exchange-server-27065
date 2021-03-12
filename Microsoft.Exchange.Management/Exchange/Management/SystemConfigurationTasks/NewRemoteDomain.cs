using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD1 RID: 2769
	[Cmdlet("New", "RemoteDomain", SupportsShouldProcess = true)]
	public class NewRemoteDomain : NewMultitenancySystemConfigurationObjectTask<DomainContentConfig>
	{
		// Token: 0x17001DE0 RID: 7648
		// (get) Token: 0x06006265 RID: 25189 RVA: 0x0019AB3B File Offset: 0x00198D3B
		// (set) Token: 0x06006266 RID: 25190 RVA: 0x0019AB48 File Offset: 0x00198D48
		[Parameter(Mandatory = true)]
		public SmtpDomainWithSubdomains DomainName
		{
			get
			{
				return this.DataObject.DomainName;
			}
			set
			{
				this.DataObject.DomainName = value;
			}
		}

		// Token: 0x17001DE1 RID: 7649
		// (get) Token: 0x06006267 RID: 25191 RVA: 0x0019AB56 File Offset: 0x00198D56
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewRemoteDomain(base.Name.ToString(), this.DomainName.ToString());
			}
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x0019AB74 File Offset: 0x00198D74
		internal static void ValidateNoDuplicates(DomainContentConfig domain, IConfigurationSession session, Task.TaskErrorLoggingDelegate errorWriter)
		{
			string domain2 = domain.DomainName.Domain;
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Guid, domain.Guid),
				new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, EdgeDomainContentConfigSchema.DomainName, domain2),
					new ComparisonFilter(ComparisonOperator.Equal, EdgeDomainContentConfigSchema.DomainName, "*." + domain2)
				})
			});
			DomainContentConfig[] array = session.Find<DomainContentConfig>(session.GetOrgContainerId().GetDescendantId(domain.ParentPath), QueryScope.SubTree, filter, null, 1);
			if (array.Length > 0)
			{
				errorWriter(new DuplicateRemoteDomainException(domain2), ErrorCategory.ResourceExists, domain);
			}
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x0019AC24 File Offset: 0x00198E24
		protected override IConfigurable PrepareDataObject()
		{
			DomainContentConfig domainContentConfig = (DomainContentConfig)base.PrepareDataObject();
			domainContentConfig.SetId(base.DataSession as IConfigurationSession, base.Name);
			return domainContentConfig;
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x0019AC58 File Offset: 0x00198E58
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.LimitRemoteDomains.Enabled)
			{
				DomainContentConfig[] array = base.DataSession.FindPaged<DomainContentConfig>(null, null, true, null, 0).ToArray<DomainContentConfig>();
				if (array != null && array.Length >= 200)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorTooManyRemoteDomains(200)), ErrorCategory.InvalidOperation, null);
				}
			}
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			NewRemoteDomain.ValidateNoDuplicates(this.DataObject, this.ConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x0019AD0D File Offset: 0x00198F0D
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			FfoDualWriter.SaveToFfo<DomainContentConfig>(this, this.DataObject, null);
			TaskLogger.LogExit();
		}

		// Token: 0x040035D4 RID: 13780
		private const int MaxRemoteDomains = 200;
	}
}
