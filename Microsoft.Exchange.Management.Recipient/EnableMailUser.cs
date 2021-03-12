using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.MapiTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000085 RID: 133
	[Cmdlet("Enable", "MailUser", SupportsShouldProcess = true, DefaultParameterSetName = "EnabledUser")]
	public sealed class EnableMailUser : EnableMailUserBase
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00027643 File Offset: 0x00025843
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x00027669 File Offset: 0x00025869
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

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00027681 File Offset: 0x00025881
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x000276A6 File Offset: 0x000258A6
		[Parameter(Mandatory = true, ParameterSetName = "Archive")]
		[ValidateNotEmptyGuid]
		public Guid ArchiveGuid
		{
			get
			{
				return (Guid)(base.Fields[ADUserSchema.ArchiveGuid] ?? Guid.Empty);
			}
			set
			{
				base.Fields[ADUserSchema.ArchiveGuid] = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x000276BE File Offset: 0x000258BE
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x000276D5 File Offset: 0x000258D5
		[Parameter(Mandatory = false, ParameterSetName = "Archive")]
		public MultiValuedProperty<string> ArchiveName
		{
			get
			{
				return base.Fields[ADUserSchema.ArchiveName] as MultiValuedProperty<string>;
			}
			set
			{
				base.Fields[ADUserSchema.ArchiveName] = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x000276E8 File Offset: 0x000258E8
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0002770E File Offset: 0x0002590E
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassModerationCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassModerationCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassModerationCheck"] = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00027726 File Offset: 0x00025926
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0002774B File Offset: 0x0002594B
		[Parameter(Mandatory = false)]
		public SmtpAddress JournalArchiveAddress
		{
			get
			{
				return (SmtpAddress)(base.Fields[ADRecipientSchema.JournalArchiveAddress] ?? SmtpAddress.Empty);
			}
			set
			{
				base.Fields[ADRecipientSchema.JournalArchiveAddress] = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00027763 File Offset: 0x00025963
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x0002777A File Offset: 0x0002597A
		[Parameter(Mandatory = true, ParameterSetName = "EnabledUser")]
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)base.Fields[ADRecipientSchema.ExternalEmailAddress];
			}
			set
			{
				base.Fields[ADRecipientSchema.ExternalEmailAddress] = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0002778D File Offset: 0x0002598D
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00027795 File Offset: 0x00025995
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		public override CountryInfo UsageLocation
		{
			get
			{
				return base.UsageLocation;
			}
			set
			{
				base.UsageLocation = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0002779E File Offset: 0x0002599E
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x000277BF File Offset: 0x000259BF
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		public bool UsePreferMessageFormat
		{
			get
			{
				return (bool)(base.Fields[ADRecipientSchema.UsePreferMessageFormat] ?? false);
			}
			set
			{
				base.Fields[ADRecipientSchema.UsePreferMessageFormat] = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x000277D7 File Offset: 0x000259D7
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x000277FC File Offset: 0x000259FC
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		public MessageFormat MessageFormat
		{
			get
			{
				return (MessageFormat)(base.Fields[ADRecipientSchema.MessageFormat] ?? MessageFormat.Mime);
			}
			set
			{
				base.Fields[ADRecipientSchema.MessageFormat] = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00027814 File Offset: 0x00025A14
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x00027839 File Offset: 0x00025A39
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		public MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return (MessageBodyFormat)(base.Fields[ADRecipientSchema.MessageBodyFormat] ?? MessageBodyFormat.TextAndHtml);
			}
			set
			{
				base.Fields[ADRecipientSchema.MessageBodyFormat] = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00027851 File Offset: 0x00025A51
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00027872 File Offset: 0x00025A72
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		public MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return (MacAttachmentFormat)(base.Fields[ADRecipientSchema.MacAttachmentFormat] ?? MacAttachmentFormat.BinHex);
			}
			set
			{
				base.Fields[ADRecipientSchema.MacAttachmentFormat] = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002788A File Offset: 0x00025A8A
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00027892 File Offset: 0x00025A92
		[Parameter]
		public override Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
			set
			{
				base.SKUCapability = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0002789B File Offset: 0x00025A9B
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x000278A3 File Offset: 0x00025AA3
		[Parameter]
		public override MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return base.AddOnSKUCapability;
			}
			set
			{
				base.AddOnSKUCapability = value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x000278AC File Offset: 0x00025AAC
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x000278B4 File Offset: 0x00025AB4
		[Parameter]
		public override bool SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
			set
			{
				base.SKUAssigned = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x000278BD File Offset: 0x00025ABD
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x000278E3 File Offset: 0x00025AE3
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSoftDeletedObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["SoftDeletedMailUser"] ?? false);
			}
			set
			{
				base.Fields["SoftDeletedMailUser"] = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x000278FB File Offset: 0x00025AFB
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00027921 File Offset: 0x00025B21
		[Parameter(Mandatory = false)]
		public SwitchParameter PreserveEmailAddresses
		{
			get
			{
				return (SwitchParameter)(base.Fields["PreserveEmailAddresses"] ?? false);
			}
			set
			{
				base.Fields["PreserveEmailAddresses"] = value;
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002793C File Offset: 0x00025B3C
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			if (this.IncludeSoftDeletedObjects.IsPresent)
			{
				base.SessionSettings.IncludeSoftDeletedObjects = true;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00027975 File Offset: 0x00025B75
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Archive" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailUserArchive(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageEnableMailUser(this.Identity.ToString(), this.ExternalEmailAddress.ToString());
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000279B8 File Offset: 0x00025BB8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			base.InternalProcessRecord();
			if (this.recoverArchive && this.DataObject.ArchiveDatabase != null)
			{
				MailboxDatabase database = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(new DatabaseIdParameter(this.DataObject.ArchiveDatabase), base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.DataObject.ArchiveDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.DataObject.ArchiveDatabase.ToString())));
				using (MapiAdministrationSession adminSession = MapiTaskHelper.GetAdminSession(RecipientTaskHelper.GetActiveManagerInstance(), this.DataObject.ArchiveDatabase.ObjectGuid))
				{
					ConnectMailbox.UpdateSDAndRefreshMailbox(adminSession, this.DataObject, database, this.DataObject.ArchiveGuid, null, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00027AC4 File Offset: 0x00025CC4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.DataObject.IsModified(ADUserSchema.ArchiveGuid) && this.ArchiveGuid != Guid.Empty)
			{
				RecipientTaskHelper.IsExchangeGuidOrArchiveGuidUnique(this.DataObject, ADUserSchema.ArchiveGuid, this.ArchiveGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00027B2F File Offset: 0x00025D2F
		protected override bool IsValidUser(ADUser user)
		{
			return ("Archive" != base.ParameterSetName && RecipientType.User == user.RecipientType) || ("Archive" == base.ParameterSetName && RecipientType.MailUser == user.RecipientType);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00027B6C File Offset: 0x00025D6C
		protected override void PrepareRecipientObject(ref ADUser user)
		{
			TaskLogger.LogEnter();
			ProxyAddressCollection emailAddresses = user.EmailAddresses;
			base.PrepareRecipientObject(ref user);
			if (this.PreserveEmailAddresses)
			{
				user.EmailAddresses = emailAddresses;
			}
			if (this.BypassModerationCheck.IsPresent)
			{
				user.BypassModerationCheck = true;
			}
			if (base.ParameterSetName == "Archive")
			{
				if (user.RecipientType == RecipientType.MailUser)
				{
					this.CreateArchiveIfNecessary(user);
					TaskLogger.LogExit();
					return;
				}
				RecipientIdParameter recipientIdParameter = new RecipientIdParameter((ADObjectId)user.Identity);
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidArchiveRecipientType(recipientIdParameter.ToString(), user.RecipientType.ToString())), ErrorCategory.InvalidArgument, recipientIdParameter);
			}
			if (base.ParameterSetName != "Archive")
			{
				user.ExternalEmailAddress = this.ExternalEmailAddress;
				user.UsePreferMessageFormat = this.UsePreferMessageFormat;
				user.MessageFormat = this.MessageFormat;
				user.MessageBodyFormat = this.MessageBodyFormat;
				user.MacAttachmentFormat = this.MacAttachmentFormat;
				user.UseMapiRichTextFormat = UseMapiRichTextFormat.UseDefaultSettings;
				user.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.RemoteMailUser);
				user.RecipientTypeDetails = RecipientTypeDetails.MailUser;
				MailUserTaskHelper.ValidateExternalEmailAddress(user, this.ConfigurationSession, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
			}
			if (this.JournalArchiveAddress != SmtpAddress.Empty)
			{
				user.JournalArchiveAddress = this.JournalArchiveAddress;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00027CD8 File Offset: 0x00025ED8
		protected override void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Id
			});
			MailUser sendToPipeline = new MailUser(this.DataObject);
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00027D18 File Offset: 0x00025F18
		private void CreateArchiveIfNecessary(ADUser user)
		{
			if (user.ArchiveGuid == Guid.Empty)
			{
				if (user.DisabledArchiveGuid != Guid.Empty && this.ArchiveGuid == user.DisabledArchiveGuid)
				{
					this.recoverArchive = MailboxTaskHelper.IsArchiveRecoverable(user, this.ConfigurationSession, RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, user.Id));
					if (this.recoverArchive)
					{
						user.ArchiveDatabase = user.DisabledArchiveDatabase;
					}
				}
				user.ArchiveGuid = this.ArchiveGuid;
				user.ArchiveName = ((this.ArchiveName == null) ? new MultiValuedProperty<string>(Strings.ArchiveNamePrefix + user.DisplayName) : this.ArchiveName);
				user.ArchiveQuota = RecipientConstants.ArchiveAddOnQuota;
				user.ArchiveWarningQuota = RecipientConstants.ArchiveAddOnWarningQuota;
				user.ArchiveStatus |= ArchiveStatusFlags.Active;
				user.AllowArchiveAddressSync = true;
				MailboxTaskHelper.ApplyDefaultArchivePolicy(user, this.ConfigurationSession);
				return;
			}
			base.WriteError(new RecipientTaskException(Strings.ErrorArchiveAlreadyPresent(this.Identity.ToString())), ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x040001EE RID: 494
		private bool recoverArchive;
	}
}
