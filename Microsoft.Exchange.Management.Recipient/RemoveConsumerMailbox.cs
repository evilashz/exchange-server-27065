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
	// Token: 0x02000014 RID: 20
	[Cmdlet("Remove", "ConsumerMailbox", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveConsumerMailbox : RemoveADTaskBase<ConsumerMailboxIdParameter, ADUser>
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00006B4B File Offset: 0x00004D4B
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00006B53 File Offset: 0x00004D53
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00006B5C File Offset: 0x00004D5C
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00006B73 File Offset: 0x00004D73
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "ConsumerSecondaryMailbox", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "ConsumerPrimaryMailbox", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006B86 File Offset: 0x00004D86
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00006BAC File Offset: 0x00004DAC
		[Parameter(Mandatory = true, ParameterSetName = "ConsumerPrimaryMailbox")]
		public SwitchParameter RemoveExoPrimary
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveExoPrimary"] ?? false);
			}
			set
			{
				base.Fields["RemoveExoPrimary"] = value.ToBool();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006BCA File Offset: 0x00004DCA
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006BF0 File Offset: 0x00004DF0
		[Parameter(Mandatory = false, ParameterSetName = "ConsumerPrimaryMailbox")]
		public SwitchParameter SwitchToSecondary
		{
			get
			{
				return (SwitchParameter)(base.Fields["SwitchToSecondary"] ?? false);
			}
			set
			{
				base.Fields["SwitchToSecondary"] = value.ToBool();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006C0E File Offset: 0x00004E0E
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00006C34 File Offset: 0x00004E34
		[Parameter(Mandatory = true, ParameterSetName = "ConsumerSecondaryMailbox")]
		public SwitchParameter RemoveExoSecondary
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveExoSecondary"] ?? false);
			}
			set
			{
				base.Fields["RemoveExoSecondary"] = value.ToBool();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006C52 File Offset: 0x00004E52
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMailboxIdentityAndNotLiveId(this.Identity.ToString(), "<UNKNOWN>");
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006C6C File Offset: 0x00004E6C
		protected override IConfigDataProvider CreateSession()
		{
			IConfigDataProvider configDataProvider = ConsumerMailboxHelper.CreateConsumerOrganizationSession();
			((IAggregateSession)configDataProvider).MbxReadMode = MbxReadMode.NoMbxRead;
			return configDataProvider;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006C8C File Offset: 0x00004E8C
		protected override bool IsKnownException(Exception exception)
		{
			return ConsumerMailboxHelper.IsKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006CA0 File Offset: 0x00004EA0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (base.Fields.IsModified("RemoveExoPrimary") && base.Fields.IsModified("RemoveExoSecondary"))
			{
				base.ThrowTerminatingError(new ArgumentException("Cannot specify both -RemoveExoPrimary and -RemoveExoSecondary parameters"), ErrorCategory.InvalidArgument, this.Identity.ToString());
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006CF8 File Offset: 0x00004EF8
		private IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			base.DataObject = (ADUser)base.ResolveDataObject();
			ulong puidNum;
			if (ConsumerIdentityHelper.TryGetPuidByExternalDirectoryObjectId(base.DataObject.ExchangeGuid.ToString(), out puidNum))
			{
				base.Fields[ADUserSchema.NetID] = new NetID(ConsumerIdentityHelper.ConvertPuidNumberToPuidString(puidNum));
				TaskLogger.LogExit();
				return null;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, "Could not extract puid from ExchangeGuid for this user", new object[0]));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006D8C File Offset: 0x00004F8C
		protected override void InternalProcessRecord()
		{
			LocalizedString message = new LocalizedString("Error removing consumer mailbox. See inner exception for details.");
			try
			{
				this.PrepareDataObject();
				ConsumerMailboxHelper.RemoveConsumerMailbox(base.Fields, (IRecipientSession)base.DataSession, delegate(string s)
				{
					base.WriteVerbose(new LocalizedString(s));
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
