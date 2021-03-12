using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000084 RID: 132
	public abstract class EnableMailUserBase : EnableRecipientObjectTask<UserIdParameter, ADUser>
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x0002747F File Offset: 0x0002567F
		public EnableMailUserBase()
		{
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00027487 File Offset: 0x00025687
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x0002749E File Offset: 0x0002569E
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override UserIdParameter Identity
		{
			get
			{
				return (UserIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000274B1 File Offset: 0x000256B1
		protected override bool DelayProvisioning
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000274B4 File Offset: 0x000256B4
		internal override bool SkipPrepareDataObject()
		{
			return "Archive" == base.ParameterSetName || base.SkipPrepareDataObject();
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000274D0 File Offset: 0x000256D0
		protected override void PrepareRecipientObject(ref ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(ref user);
			if (!this.IsValidUser(user))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidRecipientType(user.Identity.ToString(), user.RecipientType.ToString())), ErrorCategory.InvalidArgument, user.Id);
			}
			if (user.RecipientType != RecipientType.MailUser)
			{
				user.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
				List<PropertyDefinition> list = new List<PropertyDefinition>(RecipientConstants.DisableMailUserBase_PropertiesToReset);
				MailboxTaskHelper.RemovePersistentProperties(list);
				MailboxTaskHelper.ClearExchangeProperties(user, list);
				user.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
				if (this.DelayProvisioning && base.IsProvisioningLayerAvailable)
				{
					this.ProvisionDefaultValues(new ADUser(), user);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600093F RID: 2367
		protected abstract bool IsValidUser(ADUser user);

		// Token: 0x06000940 RID: 2368 RVA: 0x00027588 File Offset: 0x00025788
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			base.InternalProcessRecord();
			this.WriteResult();
			TaskLogger.LogExit();
		}

		// Token: 0x06000941 RID: 2369
		protected abstract void WriteResult();

		// Token: 0x06000942 RID: 2370 RVA: 0x000275BC File Offset: 0x000257BC
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000275CC File Offset: 0x000257CC
		protected override void PrepareRecipientAlias(ADUser dataObject)
		{
			if (!string.IsNullOrEmpty(base.Alias))
			{
				dataObject.Alias = base.Alias;
				return;
			}
			if (string.IsNullOrEmpty(dataObject.Alias))
			{
				dataObject.Alias = RecipientTaskHelper.GenerateUniqueAlias(base.TenantGlobalCatalogSession, dataObject.OrganizationId, string.IsNullOrEmpty(dataObject.UserPrincipalName) ? dataObject.SamAccountName : RecipientTaskHelper.GetLocalPartOfUserPrincalName(dataObject.UserPrincipalName), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
		}
	}
}
