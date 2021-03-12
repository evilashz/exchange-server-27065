using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000015 RID: 21
	[Cmdlet("Set", "ConsumerMailbox", SupportsShouldProcess = true)]
	public sealed class SetConsumerMailbox : SetADTaskBase<ConsumerMailboxIdParameter, ADUser, ADUser>
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006E40 File Offset: 0x00005040
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00006E48 File Offset: 0x00005048
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006E51 File Offset: 0x00005051
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00006E68 File Offset: 0x00005068
		[Parameter(Mandatory = true, ParameterSetName = "ConsumerPrimaryMailbox", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "ConsumerSecondaryMailbox", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override ConsumerMailboxIdParameter Identity
		{
			get
			{
				return (ConsumerMailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006E7B File Offset: 0x0000507B
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00006EA1 File Offset: 0x000050A1
		[Parameter(Mandatory = true, ParameterSetName = "ConsumerPrimaryMailbox")]
		public SwitchParameter MakeExoPrimary
		{
			get
			{
				return (SwitchParameter)(base.Fields["MakeExoPrimary"] ?? false);
			}
			set
			{
				base.Fields["MakeExoPrimary"] = value.ToBool();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006EBF File Offset: 0x000050BF
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006EE5 File Offset: 0x000050E5
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006F03 File Offset: 0x00005103
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006F0B File Offset: 0x0000510B
		[Parameter(Mandatory = false)]
		public SwitchParameter Repair { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006F14 File Offset: 0x00005114
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006F2B File Offset: 0x0000512B
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006F3E File Offset: 0x0000513E
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006F55 File Offset: 0x00005155
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006F68 File Offset: 0x00005168
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006F7F File Offset: 0x0000517F
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006F92 File Offset: 0x00005192
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006FA9 File Offset: 0x000051A9
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006FBC File Offset: 0x000051BC
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006FD3 File Offset: 0x000051D3
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006FEB File Offset: 0x000051EB
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00007002 File Offset: 0x00005202
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00007015 File Offset: 0x00005215
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000702C File Offset: 0x0000522C
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000703F File Offset: 0x0000523F
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00007056 File Offset: 0x00005256
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00007069 File Offset: 0x00005269
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00007080 File Offset: 0x00005280
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007093 File Offset: 0x00005293
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000070AA File Offset: 0x000052AA
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000070BD File Offset: 0x000052BD
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000070D4 File Offset: 0x000052D4
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000070E7 File Offset: 0x000052E7
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000070FE File Offset: 0x000052FE
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00007111 File Offset: 0x00005311
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00007128 File Offset: 0x00005328
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000713B File Offset: 0x0000533B
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00007152 File Offset: 0x00005352
		[Parameter(Mandatory = false)]
		public string Description
		{
			get
			{
				return (string)base.Fields[ADRecipientSchema.Description];
			}
			set
			{
				base.Fields[ADRecipientSchema.Description] = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00007165 File Offset: 0x00005365
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000717C File Offset: 0x0000537C
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000718F File Offset: 0x0000538F
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000071A6 File Offset: 0x000053A6
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000071B9 File Offset: 0x000053B9
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000071D0 File Offset: 0x000053D0
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000071E3 File Offset: 0x000053E3
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000071FA File Offset: 0x000053FA
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007212 File Offset: 0x00005412
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMailbox(this.Identity.ToString());
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007224 File Offset: 0x00005424
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider configDataProvider = ConsumerMailboxHelper.CreateConsumerOrganizationSession();
			((IAggregateSession)configDataProvider).MbxReadMode = MbxReadMode.NoMbxRead;
			return configDataProvider;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007244 File Offset: 0x00005444
		protected override bool IsKnownException(Exception exception)
		{
			return ConsumerMailboxHelper.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007258 File Offset: 0x00005458
		protected override void InternalValidate()
		{
			if (base.Fields.IsModified("MakeExoPrimary") && base.Fields.IsModified("MakeExoSecondary"))
			{
				base.ThrowTerminatingError(new ArgumentException("Cannot specify both -MakeExoPrimary and -MakeExoSecondary parameters"), ErrorCategory.InvalidArgument, this.Identity.ToString());
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000072A8 File Offset: 0x000054A8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (ADUser)base.PrepareDataObject();
			ulong puidNum;
			if (ConsumerIdentityHelper.TryGetPuidFromGuid(this.DataObject.ExchangeGuid, out puidNum))
			{
				base.Fields[ADUserSchema.NetID] = new NetID(ConsumerIdentityHelper.ConvertPuidNumberToPuidString(puidNum));
				TaskLogger.LogExit();
				return null;
			}
			throw new TaskInvalidOperationException(new LocalizedString(string.Format(CultureInfo.CurrentUICulture, "Could not extract puid from ExchangeGuid for this user", new object[0])));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007340 File Offset: 0x00005540
		protected override void InternalProcessRecord()
		{
			LocalizedString message = new LocalizedString("Error updating consumer mailbox. See inner exception for details.");
			try
			{
				this.PrepareDataObject();
				WriteOperationType writeOperationType = this.Repair.ToBool() ? WriteOperationType.RepairUpdate : WriteOperationType.Update;
				ConsumerMailboxHelper.CreateOrUpdateConsumerMailbox(writeOperationType, base.Fields, (IRecipientSession)base.DataSession, delegate(string s)
				{
					base.WriteVerbose(new LocalizedString(s));
				}, delegate(string s)
				{
					this.WriteWarning(new LocalizedString(s));
				});
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
	}
}
