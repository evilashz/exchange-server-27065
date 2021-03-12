﻿using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200008D RID: 141
	public abstract class SetMailUserBase<TIdentity, TPublicObject> : SetUserBase<TIdentity, TPublicObject> where TIdentity : MailUserIdParameterBase, new() where TPublicObject : MailUser, new()
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00028A74 File Offset: 0x00026C74
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.DataObject.IsModified(MailboxSchema.WindowsLiveID) && this.DataObject.WindowsLiveID != SmtpAddress.Empty)
				{
					TIdentity identity = this.Identity;
					return Strings.ConfirmationMessageSetMailuserWithWindowsLiveID(identity.ToString(), this.DataObject.WindowsLiveID.ToString());
				}
				TIdentity identity2 = this.Identity;
				return Strings.ConfirmationMessageSetMailUser(identity2.ToString());
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00028AF6 File Offset: 0x00026CF6
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00028AF9 File Offset: 0x00026CF9
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return base.ShouldUpgradeExchangeVersion(adObject) || adObject.IsModified(MailUserSchema.WindowsLiveID);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00028B14 File Offset: 0x00026D14
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			if (aduser.IsChanged(MailUserSchema.WindowsLiveID))
			{
				SmtpAddress value = (SmtpAddress)aduser.GetOriginalObject()[MailUserSchema.WindowsLiveID];
				if (value != SmtpAddress.Empty && !aduser.EmailAddresses.Contains(ProxyAddress.Parse("smtp", value.ToString())))
				{
					aduser.EmailAddresses.Add(ProxyAddress.Parse("smtp", value.ToString()));
				}
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
			{
				if (aduser.IsChanged(ADRecipientSchema.ExternalEmailAddress))
				{
					MailUserTaskHelper.ValidateExternalEmailAddress(aduser, this.ConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
				}
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.WindowsLiveID.Enabled)
				{
					if (aduser.WindowsLiveID != SmtpAddress.Empty && !aduser.WindowsLiveID.Equals(aduser.UserPrincipalName))
					{
						aduser.UserPrincipalName = aduser.WindowsLiveID.ToString();
					}
				}
				else if (!aduser.IsModified(ADUserSchema.UserPrincipalName))
				{
					aduser.UserPrincipalName = aduser.PrimarySmtpAddress.ToString();
				}
				if (this.DataObject.IsSoftDeleted && this.DataObject.IsModified(MailUserSchema.ExchangeGuid))
				{
					SoftDeletedTaskHelper.UpdateExchangeGuidForMailEnabledUser(this.DataObject);
				}
				if ((aduser.IsChanged(ADUserSchema.LitigationHoldEnabled) && aduser.LitigationHoldEnabled) || (aduser.IsChanged(ADRecipientSchema.InPlaceHoldsRaw) && aduser.IsInLitigationHoldOrInplaceHold) || (aduser.IsChanged(ADUserSchema.ElcMailboxFlags) && aduser.LitigationHoldEnabled))
				{
					RecoverableItemsQuotaHelper.IncreaseRecoverableItemsQuotaIfNeeded(aduser);
				}
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00028D00 File Offset: 0x00026F00
		protected override void InternalValidate()
		{
			this.latencyContext = ProvisioningPerformanceHelper.StartLatencyDetection(this);
			base.InternalValidate();
			base.VerifyIsWithinScopes((IRecipientSession)base.DataSession, this.DataObject, true, new DataAccessTask<ADUser>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			if (this.DataObject.IsModified(ADMailboxRecipientSchema.ExchangeGuid) && this.DataObject.ExchangeGuid != Guid.Empty && this.DataObject.IsModified(ADUserSchema.ArchiveGuid) && this.DataObject.ArchiveGuid != Guid.Empty && this.DataObject.ExchangeGuid == this.DataObject.ArchiveGuid)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidParameterValue("ExchangeGuid", this.DataObject.ExchangeGuid.ToString())), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			if (this.DataObject.IsModified(ADMailboxRecipientSchema.ExchangeGuid) && this.DataObject.ExchangeGuid != Guid.Empty && !this.DataObject.IsSoftDeleted)
			{
				RecipientTaskHelper.IsExchangeGuidOrArchiveGuidUnique(this.DataObject, ADMailboxRecipientSchema.ExchangeGuid, this.DataObject.ExchangeGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if (this.DataObject.IsModified(ADUserSchema.ArchiveGuid) && this.DataObject.ArchiveGuid != Guid.Empty)
			{
				RecipientTaskHelper.IsExchangeGuidOrArchiveGuidUnique(this.DataObject, ADUserSchema.ArchiveGuid, this.DataObject.ArchiveGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if (ComplianceConfigImpl.JournalArchivingHardeningEnabled && this.DataObject.IsModified(ADRecipientSchema.EmailAddresses))
			{
				bool flag = false;
				foreach (ProxyAddress proxyAddress in this.DataObject.EmailAddresses)
				{
					if (proxyAddress.Prefix == ProxyAddressPrefix.JRNL)
					{
						if (flag)
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorMultipleJournalArchiveAddress), ExchangeErrorCategory.Client, this.DataObject.Identity);
						}
						flag = true;
					}
				}
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00028F60 File Offset: 0x00027160
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				base.InternalProcessRecord();
				if (base.IsSetRandomPassword)
				{
					MailboxTaskHelper.SetMailboxPassword((IRecipientSession)base.DataSession, this.DataObject, null, new Task.ErrorLoggerDelegate(base.WriteError));
				}
				SmtpAddress value = (SmtpAddress)this.DataObject[ADRecipientSchema.WindowsLiveID];
				if (value != SmtpAddress.Empty)
				{
					UserAccountControlFlags userAccountControlFlags = (UserAccountControlFlags)this.DataObject[ADUserSchema.UserAccountControl];
					if ((userAccountControlFlags & UserAccountControlFlags.AccountDisabled) == UserAccountControlFlags.AccountDisabled)
					{
						this.DataObject[ADUserSchema.UserAccountControl] = (userAccountControlFlags & ~UserAccountControlFlags.AccountDisabled);
						using (TaskPerformanceData.SaveResult.StartRequestTimer())
						{
							base.DataSession.Save(this.DataObject);
						}
					}
				}
			}
			finally
			{
				ProvisioningPerformanceHelper.StopLatencyDetection(this.latencyContext);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00029064 File Offset: 0x00027264
		protected override void UpgradeExchangeVersion(ADObject adObject)
		{
			base.UpgradeExchangeVersion(adObject);
			adObject.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00029078 File Offset: 0x00027278
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00029088 File Offset: 0x00027288
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ADUser aduser = (ADUser)dataObject;
			this.originalJournalArchiveAddress = aduser.JournalArchiveAddress;
			base.StampChangesOn(dataObject);
		}

		// Token: 0x040001F6 RID: 502
		private LatencyDetectionContext latencyContext;

		// Token: 0x040001F7 RID: 503
		protected SmtpAddress originalJournalArchiveAddress = SmtpAddress.Empty;
	}
}
