using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.SecureMail
{
	// Token: 0x02000089 RID: 137
	[Cmdlet("remove", "MessageClassification", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveClassification : RemoveSystemConfigurationObjectTask<MessageClassificationIdParameter, MessageClassification>
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00012567 File Offset: 0x00010767
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (base.DataObject.IsDefault && this.GetLocalizations().Length > 1)
				{
					return Strings.ConfirmationMessageRemoveMessageClassificationExtended(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageRemoveMessageClassification(this.Identity.ToString());
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x000125A2 File Offset: 0x000107A2
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000125A8 File Offset: 0x000107A8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity,
				base.DataObject
			});
			if (base.DataObject.IsDefault)
			{
				foreach (MessageClassification messageClassification in this.GetLocalizations())
				{
					if (!messageClassification.IsDefault)
					{
						this.DeleteOne(messageClassification);
					}
				}
			}
			this.DeleteOne(base.DataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001261C File Offset: 0x0001081C
		protected override void InternalValidate()
		{
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = MessageClassificationIdParameter.DefaultsRoot;
			}
			base.InternalValidate();
			if (Utils.IsMessageClassificationUsedByTransportRule((IConfigurationSession)base.DataSession, base.DataObject))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorRemoveClassificationUsedByTransportRule(base.DataObject.Identity.ToString())), ErrorCategory.InvalidOperation, base.DataObject);
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000126BC File Offset: 0x000108BC
		private MessageClassification[] GetLocalizations()
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ClassificationSchema.ClassificationID, base.DataObject.ClassificationID);
			return ((IConfigurationSession)base.DataSession).FindPaged<MessageClassification>(null, QueryScope.SubTree, filter, null, 0).ReadAllPages();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00012700 File Offset: 0x00010900
		private void DeleteOne(MessageClassification cl)
		{
			try
			{
				base.DataSession.Delete(cl);
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, this.Identity);
			}
		}
	}
}
