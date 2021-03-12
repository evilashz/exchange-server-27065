using System;
using System.Management.Automation;
using System.ServiceModel;
using System.ServiceModel.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AB2 RID: 2738
	[Cmdlet("Remove", "AcceptedDomain", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAcceptedDomain : RemoveSystemConfigurationObjectTask<AcceptedDomainIdParameter, AcceptedDomain>
	{
		// Token: 0x17001D59 RID: 7513
		// (get) Token: 0x060060EA RID: 24810 RVA: 0x0019488A File Offset: 0x00192A8A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAcceptedDomain(this.Identity.ToString());
			}
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x0019489C File Offset: 0x00192A9C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
			RemoveAcceptedDomain.CheckDomainForRemoval(base.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			RemoveAcceptedDomain.DomainRemovalValidator domainRemovalValidator = new RemoveAcceptedDomain.DomainRemovalValidator(this);
			domainRemovalValidator.ValidateAllPolicies();
			TaskLogger.LogExit();
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x001948F8 File Offset: 0x00192AF8
		protected override void InternalProcessRecord()
		{
			if (base.DataObject.IsCoexistenceDomain)
			{
				try
				{
					AcceptedDomainUtility.DeregisterCoexistenceDomain(base.DataObject.DomainName.Domain);
				}
				catch (TimeoutException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				}
				catch (InvalidOperationException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
				}
				catch (SecurityAccessDeniedException exception3)
				{
					base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
				}
				catch (CommunicationException exception4)
				{
					base.WriteError(exception4, ErrorCategory.InvalidArgument, null);
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x00194994 File Offset: 0x00192B94
		internal static void CheckDomainForRemoval(AcceptedDomain acceptedDomain, Task.TaskErrorLoggingDelegate writeError)
		{
			if (acceptedDomain.Default)
			{
				writeError(new CannotRemoveDefaultAcceptedDomainException(), ErrorCategory.InvalidOperation, acceptedDomain);
			}
		}

		// Token: 0x02000AB3 RID: 2739
		private class DomainRemovalValidator : SetAcceptedDomain.DomainEditValidator
		{
			// Token: 0x060060EF RID: 24815 RVA: 0x001949B3 File Offset: 0x00192BB3
			public DomainRemovalValidator(RemoveAcceptedDomain task) : base(new Task.TaskErrorLoggingDelegate(task.WriteError), (IConfigurationSession)task.DataSession, task.DataObject, null)
			{
			}

			// Token: 0x060060F0 RID: 24816 RVA: 0x001949D9 File Offset: 0x00192BD9
			protected override void WriteInvalidTemplate(SmtpProxyAddressTemplate template)
			{
				if (base.IsUsedBy(template))
				{
					base.ErrorWriter(new LocalizedException(Strings.AcceptedDomainIsReferencedByAddressTemplate(base.OldDomain.DomainName, template)), ErrorCategory.InvalidOperation, base.OldDomain.Identity);
				}
			}
		}
	}
}
