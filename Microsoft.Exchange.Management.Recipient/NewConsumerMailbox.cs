using System;
using System.Management.Automation;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxLoadBalanceClient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000013 RID: 19
	[Cmdlet("New", "ConsumerMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "ConsumerPrimaryMailbox")]
	public sealed class NewConsumerMailbox : NewADTaskBase<ADUser>
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00006317 File Offset: 0x00004517
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000631F File Offset: 0x0000451F
		private new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00006328 File Offset: 0x00004528
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000633F File Offset: 0x0000453F
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public WindowsLiveId WindowsLiveID
		{
			get
			{
				return (WindowsLiveId)base.Fields["WindowsLiveID"];
			}
			set
			{
				base.Fields["WindowsLiveID"] = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00006352 File Offset: 0x00004552
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00006378 File Offset: 0x00004578
		[Parameter(Mandatory = false, ParameterSetName = "ConsumerPrimaryMailbox")]
		public SwitchParameter MakeExoPrimary
		{
			get
			{
				return (SwitchParameter)(base.Fields["MakeExoPrimary"] ?? true);
			}
			set
			{
				base.Fields["MakeExoPrimary"] = value.ToBool();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00006396 File Offset: 0x00004596
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000063BC File Offset: 0x000045BC
		[Parameter(Mandatory = true, ParameterSetName = "ConsumerSecondaryMailbox")]
		public SwitchParameter MakeExoSecondary
		{
			get
			{
				return (SwitchParameter)(base.Fields["MakeExoSecondary"] ?? false);
			}
			set
			{
				base.Fields["MakeExoSecondary"] = value.ToBool();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000063DA File Offset: 0x000045DA
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000063F1 File Offset: 0x000045F1
		[Parameter(Mandatory = false)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00006404 File Offset: 0x00004604
		// (set) Token: 0x060000BF RID: 191 RVA: 0x0000642A File Offset: 0x0000462A
		[Parameter(Mandatory = false, ParameterSetName = "ConsumerPrimaryMailbox")]
		public SwitchParameter SkipMigration
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipMigration"] ?? false);
			}
			set
			{
				base.Fields["SkipMigration"] = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00006442 File Offset: 0x00004642
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000644A File Offset: 0x0000464A
		[Parameter(Mandatory = false)]
		public SwitchParameter Repair { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00006453 File Offset: 0x00004653
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000646A File Offset: 0x0000466A
		[Parameter(Mandatory = false)]
		public string Gender
		{
			get
			{
				return (string)base.Fields[ADUserSchema.Gender];
			}
			set
			{
				base.Fields[ADUserSchema.Gender] = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000647D File Offset: 0x0000467D
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00006494 File Offset: 0x00004694
		[Parameter(Mandatory = false)]
		public string Occupation
		{
			get
			{
				return (string)base.Fields[ADUserSchema.Occupation];
			}
			set
			{
				base.Fields[ADUserSchema.Occupation] = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000064A7 File Offset: 0x000046A7
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000064BE File Offset: 0x000046BE
		[Parameter(Mandatory = false)]
		public string Region
		{
			get
			{
				return (string)base.Fields[ADUserSchema.Region];
			}
			set
			{
				base.Fields[ADUserSchema.Region] = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000064D1 File Offset: 0x000046D1
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000064E8 File Offset: 0x000046E8
		[Parameter(Mandatory = false)]
		public string Timezone
		{
			get
			{
				return (string)base.Fields[ADUserSchema.Timezone];
			}
			set
			{
				base.Fields[ADUserSchema.Timezone] = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000064FB File Offset: 0x000046FB
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00006512 File Offset: 0x00004712
		[Parameter(Mandatory = false)]
		public DateTime Birthdate
		{
			get
			{
				return (DateTime)base.Fields[ADUserSchema.Birthdate];
			}
			set
			{
				base.Fields[ADUserSchema.Birthdate] = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000652A File Offset: 0x0000472A
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00006541 File Offset: 0x00004741
		[Parameter(Mandatory = false)]
		public string BirthdayPrecision
		{
			get
			{
				return (string)base.Fields[ADUserSchema.BirthdayPrecision];
			}
			set
			{
				base.Fields[ADUserSchema.BirthdayPrecision] = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00006554 File Offset: 0x00004754
		// (set) Token: 0x060000CF RID: 207 RVA: 0x0000656B File Offset: 0x0000476B
		[Parameter(Mandatory = false)]
		public string NameVersion
		{
			get
			{
				return (string)base.Fields[ADUserSchema.NameVersion];
			}
			set
			{
				base.Fields[ADUserSchema.NameVersion] = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000657E File Offset: 0x0000477E
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00006595 File Offset: 0x00004795
		[Parameter(Mandatory = false)]
		public string AlternateSupportEmailAddresses
		{
			get
			{
				return (string)base.Fields[ADUserSchema.AlternateSupportEmailAddresses];
			}
			set
			{
				base.Fields[ADUserSchema.AlternateSupportEmailAddresses] = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000065A8 File Offset: 0x000047A8
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000065BF File Offset: 0x000047BF
		[Parameter(Mandatory = false)]
		public string PostalCode
		{
			get
			{
				return (string)base.Fields[ADUserSchema.PostalCode];
			}
			set
			{
				base.Fields[ADUserSchema.PostalCode] = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000065D2 File Offset: 0x000047D2
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000065E9 File Offset: 0x000047E9
		[Parameter(Mandatory = false)]
		public string OptInUser
		{
			get
			{
				return (string)base.Fields[ADUserSchema.OptInUser];
			}
			set
			{
				base.Fields[ADUserSchema.OptInUser] = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000065FC File Offset: 0x000047FC
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00006613 File Offset: 0x00004813
		[Parameter(Mandatory = false)]
		public string MigrationDryRun
		{
			get
			{
				return (string)base.Fields[ADUserSchema.MigrationDryRun];
			}
			set
			{
				base.Fields[ADUserSchema.MigrationDryRun] = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006626 File Offset: 0x00004826
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000663D File Offset: 0x0000483D
		[Parameter(Mandatory = false)]
		public string FirstName
		{
			get
			{
				return (string)base.Fields[ADUserSchema.FirstName];
			}
			set
			{
				base.Fields[ADUserSchema.FirstName] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006650 File Offset: 0x00004850
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00006667 File Offset: 0x00004867
		[Parameter(Mandatory = false)]
		public string LastName
		{
			get
			{
				return (string)base.Fields[ADUserSchema.LastName];
			}
			set
			{
				base.Fields[ADUserSchema.LastName] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000667A File Offset: 0x0000487A
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00006691 File Offset: 0x00004891
		[Parameter(Mandatory = false)]
		public CountryInfo UsageLocation
		{
			get
			{
				return (CountryInfo)base.Fields[ADRecipientSchema.UsageLocation];
			}
			set
			{
				base.Fields[ADRecipientSchema.UsageLocation] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000066A4 File Offset: 0x000048A4
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000066BB File Offset: 0x000048BB
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)base.Fields[ADRecipientSchema.EmailAddresses];
			}
			set
			{
				base.Fields[ADRecipientSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000066CE File Offset: 0x000048CE
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000066E5 File Offset: 0x000048E5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<int> LocaleID
		{
			get
			{
				return (MultiValuedProperty<int>)base.Fields[ADUserSchema.LocaleID];
			}
			set
			{
				base.Fields[ADUserSchema.LocaleID] = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000066F8 File Offset: 0x000048F8
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000670F File Offset: 0x0000490F
		[Parameter(Mandatory = false)]
		public bool FblEnabled
		{
			get
			{
				return (bool)base.Fields[ADUserSchema.FblEnabled];
			}
			set
			{
				base.Fields[ADUserSchema.FblEnabled] = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00006728 File Offset: 0x00004928
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMailboxUser((this.WindowsLiveID.NetId != null) ? this.WindowsLiveID.NetId.ToString() : string.Empty, this.WindowsLiveID.SmtpAddress.ToString(), TemplateTenantConfiguration.TemplateTenantExternalDirectoryOrganizationIdGuid.ToString());
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006790 File Offset: 0x00004990
		protected override IConfigDataProvider CreateSession()
		{
			return ConsumerMailboxHelper.CreateConsumerOrganizationSession();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006797 File Offset: 0x00004997
		protected override bool IsKnownException(Exception exception)
		{
			return ConsumerMailboxHelper.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000067AC File Offset: 0x000049AC
		protected override void InternalValidate()
		{
			if (this.WindowsLiveID != null && this.WindowsLiveID.NetId != null && this.WindowsLiveID.SmtpAddress != SmtpAddress.Empty)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorCannotProvideNetIDAndSmtpAddress), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006804 File Offset: 0x00004A04
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			if (base.Fields.IsModified("Database"))
			{
				ADObjectId value = this.ResolveDatabaseParameterId((DatabaseIdParameter)base.Fields["Database"]);
				base.Fields[ADMailboxRecipientSchema.Database] = value;
			}
			else
			{
				ILoadBalanceServicePort loadBalanceServicePort = LoadBalanceServiceAdapter.Create(NullAnchorLogger.Instance);
				base.Fields[ADMailboxRecipientSchema.Database] = loadBalanceServicePort.GetDatabaseForNewConsumerMailbox();
			}
			if (this.WindowsLiveID.NetId != null)
			{
				base.Fields[ADUserSchema.NetID] = this.WindowsLiveID.NetId;
				TaskLogger.LogExit();
				return null;
			}
			SmtpAddress smtpAddress = this.WindowsLiveID.SmtpAddress;
			throw new NotImplementedException("See OfficeMain bug# 1505962");
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000068E4 File Offset: 0x00004AE4
		protected override void InternalProcessRecord()
		{
			LocalizedString message = new LocalizedString("Error creating consumer mailbox. See inner exception for details.");
			try
			{
				this.PrepareDataObject();
				WriteOperationType writeOperationType = this.Repair.ToBool() ? WriteOperationType.RepairCreate : WriteOperationType.Create;
				ConsumerMailboxHelper.CreateOrUpdateConsumerMailbox(writeOperationType, base.Fields, (IRecipientSession)base.DataSession, delegate(string s)
				{
					base.WriteVerbose(new LocalizedString(s));
				}, delegate(string s)
				{
					this.WriteWarning(new LocalizedString(s));
				});
				if (!base.HasErrors && !this.SkipWriteResult)
				{
					this.WriteResult();
				}
			}
			catch (ADNoSuchObjectException innerException)
			{
				throw new ManagementObjectNotFoundException(message, innerException);
			}
			catch (ADObjectAlreadyExistsException innerException2)
			{
				throw new ManagementObjectAlreadyExistsException(message, innerException2);
			}
			catch (ArgumentException innerException3)
			{
				throw new TaskArgumentException(message, innerException3);
			}
			catch (InvalidOperationException innerException4)
			{
				throw new TaskInvalidOperationException(message, innerException4);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000069D4 File Offset: 0x00004BD4
		protected override void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			ConsumerMailbox consumerMailbox = null;
			NetID netID = (NetID)base.Fields[ADUserSchema.NetID];
			try
			{
				using (TaskPerformanceData.ReadResult.StartRequestTimer())
				{
					ADUser aduser = ConsumerMailboxHelper.ReadUser((IRecipientSession)base.DataSession, netID.ToUInt64(), false);
					if (aduser != null)
					{
						consumerMailbox = new ConsumerMailbox(aduser);
					}
				}
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			if (consumerMailbox == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(netID.ToString(), typeof(ConsumerMailbox).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, netID.ToString());
			}
			using (TaskPerformanceData.WriteResult.StartRequestTimer())
			{
				this.WriteResult(consumerMailbox);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006AFC File Offset: 0x00004CFC
		private ADObjectId ResolveDatabaseParameterId(DatabaseIdParameter dbId)
		{
			MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(dbId, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(dbId.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(dbId.ToString())));
			return mailboxDatabase.Id;
		}
	}
}
