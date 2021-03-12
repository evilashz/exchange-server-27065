using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SecureMail
{
	// Token: 0x0200008A RID: 138
	[Cmdlet("Set", "MessageClassification", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMessageClassification : SetSystemConfigurationObjectTask<MessageClassificationIdParameter, MessageClassification>
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00012748 File Offset: 0x00010948
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMessageClassification(this.Identity.ToString());
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001275A File Offset: 0x0001095A
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00012760 File Offset: 0x00010960
		protected override void InternalValidate()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			configurationSession.SessionSettings.IsSharedConfigChecked = true;
			if (base.OptionalIdentityData != null)
			{
				base.OptionalIdentityData.ConfigurationContainerRdn = MessageClassificationIdParameter.DefaultsRoot;
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			if (!this.DataObject.IsDefault)
			{
				this.RejectNonDefault(ADObjectSchema.Name);
				this.RejectNonDefault(ClassificationSchema.ClassificationID);
				this.RejectNonDefault(ClassificationSchema.DisplayPrecedence);
				this.RejectNonDefault(ClassificationSchema.PermissionMenuVisible);
				this.RejectNonDefault(ClassificationSchema.RetainClassificationEnabled);
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00012824 File Offset: 0x00010A24
		protected override void InternalProcessRecord()
		{
			this.DataObject.Version++;
			if (this.DataObject.IsDefault)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ClassificationSchema.ClassificationID, this.DataObject.ClassificationID);
				IEnumerable<MessageClassification> enumerable = base.DataSession.FindPaged<MessageClassification>(filter, null, true, null, 0);
				foreach (MessageClassification messageClassification in enumerable)
				{
					if (!messageClassification.IsDefault)
					{
						messageClassification.Name = this.DataObject.Name;
						messageClassification.DisplayPrecedence = this.DataObject.DisplayPrecedence;
						messageClassification.PermissionMenuVisible = this.DataObject.PermissionMenuVisible;
						messageClassification.RetainClassificationEnabled = this.DataObject.RetainClassificationEnabled;
						base.DataSession.Save(messageClassification);
					}
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00012914 File Offset: 0x00010B14
		private void RejectNonDefault(ProviderPropertyDefinition def)
		{
			if (this.DataObject.IsModified(def))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotChangeLocaleProperty(def.Name, this.DataObject.Name)), ErrorCategory.ObjectNotFound, this.Identity);
			}
		}
	}
}
