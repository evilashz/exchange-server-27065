using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000B2 RID: 178
	[Cmdlet("Enable", "RemoteMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "EnabledUser")]
	public sealed class EnableRemoteMailbox : EnableMailUserBase
	{
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002FD7F File Offset: 0x0002DF7F
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0002FD96 File Offset: 0x0002DF96
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "EnabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		public ProxyAddress RemoteRoutingAddress
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

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002FDA9 File Offset: 0x0002DFA9
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0002FDCF File Offset: 0x0002DFCF
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002FDE7 File Offset: 0x0002DFE7
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x0002FDFE File Offset: 0x0002DFFE
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

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002FE11 File Offset: 0x0002E011
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x0002FE37 File Offset: 0x0002E037
		[Parameter(Mandatory = true, ParameterSetName = "Room")]
		public SwitchParameter Room
		{
			get
			{
				return (SwitchParameter)(base.Fields["Room"] ?? false);
			}
			set
			{
				base.Fields["Room"] = value;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0002FE4F File Offset: 0x0002E04F
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0002FE75 File Offset: 0x0002E075
		[Parameter(Mandatory = true, ParameterSetName = "Equipment")]
		public SwitchParameter Equipment
		{
			get
			{
				return (SwitchParameter)(base.Fields["Equipment"] ?? false);
			}
			set
			{
				base.Fields["Equipment"] = value;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0002FE8D File Offset: 0x0002E08D
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0002FEB3 File Offset: 0x0002E0B3
		[Parameter(Mandatory = true, ParameterSetName = "Shared")]
		public SwitchParameter Shared
		{
			get
			{
				return (SwitchParameter)(base.Fields["Shared"] ?? false);
			}
			set
			{
				base.Fields["Shared"] = value;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002FECB File Offset: 0x0002E0CB
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0002FEF1 File Offset: 0x0002E0F1
		[Parameter(Mandatory = false)]
		public SwitchParameter ACLableSyncedObjectEnabled
		{
			get
			{
				return (SwitchParameter)(base.Fields["ACLableSyncedObjectEnabled"] ?? false);
			}
			set
			{
				base.Fields["ACLableSyncedObjectEnabled"] = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0002FF09 File Offset: 0x0002E109
		private new SwitchParameter OverrideRecipientQuotas
		{
			get
			{
				return base.OverrideRecipientQuotas;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0002FF11 File Offset: 0x0002E111
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x0002FF19 File Offset: 0x0002E119
		public override Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002FF20 File Offset: 0x0002E120
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x0002FF28 File Offset: 0x0002E128
		public override MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return base.AddOnSKUCapability;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0002FF2F File Offset: 0x0002E12F
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x0002FF37 File Offset: 0x0002E137
		public override bool SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002FF40 File Offset: 0x0002E140
		protected override bool IsValidUser(ADUser user)
		{
			return ("Archive" != base.ParameterSetName && (RecipientType.User == user.RecipientType || RecipientType.MailUser == user.RecipientType)) || ("Archive" == base.ParameterSetName && RecipientType.MailUser == user.RecipientType);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002FF90 File Offset: 0x0002E190
		protected override void PrepareRecipientObject(ref ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(ref user);
			if ("EnabledUser" == base.ParameterSetName || "Room" == base.ParameterSetName || "Equipment" == base.ParameterSetName || "Shared" == base.ParameterSetName)
			{
				if (RemoteMailbox.IsRemoteMailbox(user.RecipientTypeDetails))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorInvalidRecipientType(user.Identity.ToString(), user.RecipientTypeDetails.ToString())), ErrorCategory.InvalidArgument, user.Id);
				}
				if (null == this.RemoteRoutingAddress)
				{
					if (user.RecipientType == RecipientType.User)
					{
						if (this.remoteRoutingAddressGenerator == null)
						{
							this.remoteRoutingAddressGenerator = new RemoteRoutingAddressGenerator(this.ConfigurationSession);
						}
						user.ExternalEmailAddress = this.remoteRoutingAddressGenerator.GenerateRemoteRoutingAddress(user.Alias, new Task.ErrorLoggerDelegate(base.WriteError));
					}
				}
				else
				{
					user.ExternalEmailAddress = this.RemoteRoutingAddress;
				}
				user.RemoteRecipientType = RemoteRecipientType.ProvisionMailbox;
				RemoteMailboxType remoteMailboxType = (RemoteMailboxType)((ulong)int.MinValue);
				if (this.Room.IsPresent)
				{
					remoteMailboxType = RemoteMailboxType.Room;
				}
				else if (this.Equipment.IsPresent)
				{
					remoteMailboxType = RemoteMailboxType.Equipment;
				}
				else if (this.Shared.IsPresent)
				{
					remoteMailboxType = RemoteMailboxType.Shared;
				}
				user.UpdateRemoteMailboxType(remoteMailboxType, this.ACLableSyncedObjectEnabled);
				user.SetExchangeVersion(ExchangeObjectVersion.Current);
			}
			if ("Archive" == base.ParameterSetName)
			{
				if (!RemoteMailbox.IsRemoteMailbox(user.RecipientTypeDetails))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorInvalidRecipientType(user.Identity.ToString(), user.RecipientTypeDetails.ToString())), ErrorCategory.InvalidArgument, user.Id);
				}
				if ((user.RemoteRecipientType & RemoteRecipientType.ProvisionArchive) != RemoteRecipientType.ProvisionArchive)
				{
					if (user.ArchiveGuid == Guid.Empty)
					{
						if (user.DisabledArchiveGuid != Guid.Empty)
						{
							user.ArchiveGuid = user.DisabledArchiveGuid;
						}
						else
						{
							user.ArchiveGuid = Guid.NewGuid();
						}
					}
					if (this.ArchiveName == null)
					{
						if (user.ArchiveName == null || user.ArchiveName.Count == 0)
						{
							user.ArchiveName = new MultiValuedProperty<string>(Strings.ArchiveNamePrefix + user.DisplayName);
						}
					}
					else
					{
						user.ArchiveName = this.ArchiveName;
					}
					user.RemoteRecipientType = ((user.RemoteRecipientType &= ~RemoteRecipientType.DeprovisionArchive) | RemoteRecipientType.ProvisionArchive);
					TaskLogger.LogExit();
					return;
				}
				base.WriteError(new RecipientTaskException(Strings.ErrorArchiveAlreadyPresent(this.Identity.ToString())), (ErrorCategory)1003, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00030264 File Offset: 0x0002E464
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Archive" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailUserArchive(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageEnableRemoteMailbox(this.Identity.ToString());
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0003029C File Offset: 0x0002E49C
		protected override void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Id
			});
			RemoteMailbox sendToPipeline = new RemoteMailbox(this.DataObject);
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000302DC File Offset: 0x0002E4DC
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return RemoteMailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x04000273 RID: 627
		public const string ParameterACLableSyncedEnabled = "ACLableSyncedObjectEnabled";

		// Token: 0x04000274 RID: 628
		private RemoteRoutingAddressGenerator remoteRoutingAddressGenerator;
	}
}
