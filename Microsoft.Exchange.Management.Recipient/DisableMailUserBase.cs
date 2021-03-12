using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000082 RID: 130
	public abstract class DisableMailUserBase<TIdentity> : RecipientObjectActionTask<TIdentity, ADUser> where TIdentity : MailUserIdParameterBase, new()
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x0002704D File Offset: 0x0002524D
		public DisableMailUserBase()
		{
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00027055 File Offset: 0x00025255
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0002705D File Offset: 0x0002525D
		[Parameter]
		public SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.InternalIgnoreDefaultScope;
			}
			set
			{
				base.InternalIgnoreDefaultScope = value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00027066 File Offset: 0x00025266
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0002708C File Offset: 0x0002528C
		[Parameter(Mandatory = false, ParameterSetName = "Archive")]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000270A4 File Offset: 0x000252A4
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x000270BB File Offset: 0x000252BB
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override TIdentity Identity
		{
			get
			{
				return (TIdentity)((object)base.Fields["Identity"]);
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000270D3 File Offset: 0x000252D3
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x000270F9 File Offset: 0x000252F9
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreLegalHold
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreLegalHold"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreLegalHold"] = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00027111 File Offset: 0x00025311
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00027137 File Offset: 0x00025337
		[Parameter(Mandatory = false)]
		public SwitchParameter PreventRecordingPreviousDatabase
		{
			get
			{
				return (SwitchParameter)(base.Fields["PreventRecordingPreviousDatabase"] ?? false);
			}
			set
			{
				base.Fields["PreventRecordingPreviousDatabase"] = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00027150 File Offset: 0x00025350
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Archive" == base.ParameterSetName)
				{
					TIdentity identity = this.Identity;
					return Strings.ConfirmationMessageDisableArchive(identity.ToString());
				}
				TIdentity identity2 = this.Identity;
				return Strings.ConfirmationMessageDisableMailUser(identity2.ToString());
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x000271A4 File Offset: 0x000253A4
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			MailboxTaskHelper.BlockRemoveOrDisableIfLitigationHoldEnabled(aduser, new Task.ErrorLoggerDelegate(base.WriteError), true, this.IgnoreLegalHold.ToBool());
			MailboxTaskHelper.BlockRemoveOrDisableIfDiscoveryHoldEnabled(aduser, new Task.ErrorLoggerDelegate(base.WriteError), true, this.IgnoreLegalHold.ToBool());
			if (ComplianceConfigImpl.JournalArchivingHardeningEnabled)
			{
				MailboxTaskHelper.BlockRemoveOrDisableMailUserIfJournalArchiveEnabled(base.DataSession as IRecipientSession, this.ConfigurationSession, aduser, new Task.ErrorLoggerDelegate(base.WriteError), true, false);
			}
			if (!aduser.ExchangeVersion.IsOlderThan(ADUserSchema.ArchiveGuid.VersionAdded))
			{
				if (aduser.ArchiveGuid != Guid.Empty)
				{
					if (!this.PreventRecordingPreviousDatabase)
					{
						aduser.DisabledArchiveGuid = aduser.ArchiveGuid;
						aduser.DisabledArchiveDatabase = aduser.ArchiveDatabase;
					}
					else
					{
						aduser.DisabledArchiveGuid = Guid.Empty;
						aduser.DisabledArchiveDatabase = null;
					}
				}
				aduser.ArchiveGuid = Guid.Empty;
				aduser.ArchiveName = null;
				aduser.ArchiveDatabase = null;
				aduser.ArchiveStatus = (aduser.ArchiveStatus &= ~ArchiveStatusFlags.Active);
				aduser.AllowArchiveAddressSync = false;
			}
			if ("Archive" == base.ParameterSetName)
			{
				MailboxTaskHelper.ClearExchangeProperties(aduser, DisableMailUserBase<MailUserIdParameter>.MailboxMovePropertiesToReset);
				TaskLogger.Trace("DisableMailbox -Archive skipping PrepareDataObject", new object[0]);
				TaskLogger.LogExit();
				return aduser;
			}
			int recipientSoftDeletedStatus = aduser.RecipientSoftDeletedStatus;
			DateTime? whenSoftDeleted = aduser.WhenSoftDeleted;
			Guid disabledArchiveGuid = aduser.DisabledArchiveGuid;
			ADObjectId disabledArchiveDatabase = aduser.DisabledArchiveDatabase;
			MailboxTaskHelper.ClearExchangeProperties(aduser, RecipientConstants.DisableMailUserBase_PropertiesToReset);
			aduser.SetExchangeVersion(null);
			aduser.OverrideCorruptedValuesWithDefault();
			aduser.propertyBag.SetField(ADRecipientSchema.RecipientSoftDeletedStatus, recipientSoftDeletedStatus);
			aduser.propertyBag.SetField(ADRecipientSchema.WhenSoftDeleted, whenSoftDeleted);
			if (disabledArchiveGuid != Guid.Empty)
			{
				aduser.propertyBag.SetField(ADUserSchema.DisabledArchiveGuid, disabledArchiveGuid);
				aduser.propertyBag.SetField(ADUserSchema.DisabledArchiveDatabase, disabledArchiveDatabase);
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000273A5 File Offset: 0x000255A5
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x040001ED RID: 493
		internal static readonly PropertyDefinition[] MailboxMovePropertiesToReset = new PropertyDefinition[]
		{
			ADUserSchema.MailboxMoveTargetMDB,
			ADUserSchema.MailboxMoveSourceMDB,
			ADUserSchema.MailboxMoveTargetArchiveMDB,
			ADUserSchema.MailboxMoveSourceArchiveMDB,
			ADUserSchema.MailboxMoveFlags,
			ADUserSchema.MailboxMoveStatus,
			ADUserSchema.MailboxMoveRemoteHostName,
			ADUserSchema.MailboxMoveBatchName
		};
	}
}
