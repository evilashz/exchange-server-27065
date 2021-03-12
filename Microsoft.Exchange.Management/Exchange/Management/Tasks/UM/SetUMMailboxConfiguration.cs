using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D42 RID: 3394
	[Cmdlet("Set", "UMMailboxConfiguration", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetUMMailboxConfiguration : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17002868 RID: 10344
		// (get) Token: 0x06008213 RID: 33299 RVA: 0x00213E63 File Offset: 0x00212063
		// (set) Token: 0x06008214 RID: 33300 RVA: 0x00213E7A File Offset: 0x0021207A
		[Parameter]
		public MailboxGreetingEnum Greeting
		{
			get
			{
				return (MailboxGreetingEnum)base.Fields["Greeting"];
			}
			set
			{
				base.Fields["Greeting"] = value;
			}
		}

		// Token: 0x17002869 RID: 10345
		// (get) Token: 0x06008215 RID: 33301 RVA: 0x00213E92 File Offset: 0x00212092
		// (set) Token: 0x06008216 RID: 33302 RVA: 0x00213EA9 File Offset: 0x002120A9
		[Parameter]
		public MailboxFolderIdParameter FolderToReadEmailsFrom
		{
			get
			{
				return (MailboxFolderIdParameter)base.Fields["FolderToReadEmailsFrom"];
			}
			set
			{
				base.Fields["FolderToReadEmailsFrom"] = value;
			}
		}

		// Token: 0x1700286A RID: 10346
		// (get) Token: 0x06008217 RID: 33303 RVA: 0x00213EBC File Offset: 0x002120BC
		// (set) Token: 0x06008218 RID: 33304 RVA: 0x00213ED3 File Offset: 0x002120D3
		[Parameter]
		public bool ReadOldestUnreadVoiceMessagesFirst
		{
			get
			{
				return (bool)base.Fields["ReadOldestUnreadVoiceMessageFirst"];
			}
			set
			{
				base.Fields["ReadOldestUnreadVoiceMessageFirst"] = value;
			}
		}

		// Token: 0x1700286B RID: 10347
		// (get) Token: 0x06008219 RID: 33305 RVA: 0x00213EEB File Offset: 0x002120EB
		// (set) Token: 0x0600821A RID: 33306 RVA: 0x00213F02 File Offset: 0x00212102
		[Parameter]
		public string DefaultPlayOnPhoneNumber
		{
			get
			{
				return (string)base.Fields["DefaultPlayOnPhoneNumber"];
			}
			set
			{
				base.Fields["DefaultPlayOnPhoneNumber"] = value;
			}
		}

		// Token: 0x1700286C RID: 10348
		// (get) Token: 0x0600821B RID: 33307 RVA: 0x00213F15 File Offset: 0x00212115
		// (set) Token: 0x0600821C RID: 33308 RVA: 0x00213F2C File Offset: 0x0021212C
		[Parameter]
		public bool ReceivedVoiceMailPreviewEnabled
		{
			get
			{
				return (bool)base.Fields["ReceivedVoiceMailPreviewEnabled"];
			}
			set
			{
				base.Fields["ReceivedVoiceMailPreviewEnabled"] = value;
			}
		}

		// Token: 0x1700286D RID: 10349
		// (get) Token: 0x0600821D RID: 33309 RVA: 0x00213F44 File Offset: 0x00212144
		// (set) Token: 0x0600821E RID: 33310 RVA: 0x00213F5B File Offset: 0x0021215B
		[Parameter]
		public bool SentVoiceMailPreviewEnabled
		{
			get
			{
				return (bool)base.Fields["SentVoiceMailPreviewEnabled"];
			}
			set
			{
				base.Fields["SentVoiceMailPreviewEnabled"] = value;
			}
		}

		// Token: 0x1700286E RID: 10350
		// (get) Token: 0x0600821F RID: 33311 RVA: 0x00213F73 File Offset: 0x00212173
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetUMMailboxConfiguration(this.Identity.ToString());
			}
		}

		// Token: 0x06008220 RID: 33312 RVA: 0x00213F85 File Offset: 0x00212185
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x06008221 RID: 33313 RVA: 0x00213F98 File Offset: 0x00212198
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.ValidateParameters();
		}

		// Token: 0x06008222 RID: 33314 RVA: 0x00213FA8 File Offset: 0x002121A8
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, false))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1000, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x06008223 RID: 33315 RVA: 0x00214018 File Offset: 0x00212218
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			using (UMSubscriber umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(this.DataObject))
			{
				if (umsubscriber != null)
				{
					try
					{
						this.SetPropertiesOnUMSubscriber(umsubscriber);
						goto IL_4F;
					}
					catch (UserConfigurationException exception)
					{
						base.WriteError(exception, (ErrorCategory)1001, null);
						goto IL_4F;
					}
				}
				base.WriteError(new UserNotUmEnabledException(this.Identity.ToString()), (ErrorCategory)1000, null);
				IL_4F:;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008224 RID: 33316 RVA: 0x002140A4 File Offset: 0x002122A4
		private void ValidateParameters()
		{
			if (base.Fields.IsModified("FolderToReadEmailsFrom"))
			{
				this.mailboxFolder = ManageInboxRule.ResolveMailboxFolder(this.FolderToReadEmailsFrom, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADUser>), new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<MailboxFolder>), base.TenantGlobalCatalogSession, base.SessionSettings, this.DataObject, new ManageInboxRule.ThrowTerminatingErrorDelegate(base.WriteError));
			}
		}

		// Token: 0x06008225 RID: 33317 RVA: 0x0021410C File Offset: 0x0021230C
		private void SetPropertiesOnUMSubscriber(UMSubscriber subscriber)
		{
			bool flag = false;
			if (base.Fields.IsModified("Greeting"))
			{
				subscriber.ConfigFolder.CurrentMailboxGreetingType = this.Greeting;
				flag = true;
			}
			if (base.Fields.IsModified("ReadOldestUnreadVoiceMessageFirst"))
			{
				subscriber.ConfigFolder.ReadUnreadVoicemailInFIFOOrder = this.ReadOldestUnreadVoiceMessagesFirst;
				flag = true;
			}
			if (base.Fields.IsModified("DefaultPlayOnPhoneNumber"))
			{
				subscriber.ConfigFolder.PlayOnPhoneDialString = this.DefaultPlayOnPhoneNumber;
				flag = true;
			}
			if (base.Fields.IsModified("ReceivedVoiceMailPreviewEnabled"))
			{
				subscriber.ConfigFolder.ReceivedVoiceMailPreviewEnabled = this.ReceivedVoiceMailPreviewEnabled;
				flag = true;
			}
			if (base.Fields.IsModified("SentVoiceMailPreviewEnabled"))
			{
				subscriber.ConfigFolder.SentVoiceMailPreviewEnabled = this.SentVoiceMailPreviewEnabled;
				flag = true;
			}
			if (this.mailboxFolder != null)
			{
				StoreObjectId objectId = this.mailboxFolder.InternalFolderIdentity.ObjectId;
				subscriber.ConfigFolder.TelephoneAccessFolderEmail = Convert.ToBase64String(objectId.ProviderLevelItemId);
				flag = true;
			}
			if (flag)
			{
				subscriber.ConfigFolder.Save();
			}
		}

		// Token: 0x04003F39 RID: 16185
		private MailboxFolder mailboxFolder;
	}
}
