using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000006 RID: 6
	[Cmdlet("set", "CASMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetCASMailbox : SetCASMailboxBase<MailboxIdParameter, CASMailbox>
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003324 File Offset: 0x00001524
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000334A File Offset: 0x0000154A
		[Parameter(Mandatory = false)]
		public SwitchParameter ResetAutoBlockedDevices
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResetAutoBlockedDevices"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ResetAutoBlockedDevices"] = value;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003364 File Offset: 0x00001564
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeMailboxPlan(adrecipient, false))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ErrorCategory.InvalidData, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000033CF File Offset: 0x000015CF
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return CASMailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000033DC File Offset: 0x000015DC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			CASMailbox casmailbox = (CASMailbox)this.GetDynamicParameters();
			if (casmailbox.EwsAllowListSpecified && casmailbox.EwsBlockListSpecified)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorEwsAllowListAndEwsBlockListSpecified), ErrorCategory.InvalidArgument, null);
			}
			if (casmailbox.IsModified(CASMailboxSchema.EwsApplicationAccessPolicy))
			{
				if (!casmailbox.EwsAllowListSpecified && !casmailbox.EwsBlockListSpecified)
				{
					casmailbox.EwsExceptions = null;
				}
				if (casmailbox.EwsApplicationAccessPolicy == EwsApplicationAccessPolicy.EnforceAllowList && casmailbox.EwsBlockListSpecified)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorEwsEnforceAllowListAndEwsBlockListSpecified), ErrorCategory.InvalidArgument, null);
				}
				if (casmailbox.EwsApplicationAccessPolicy == EwsApplicationAccessPolicy.EnforceBlockList && casmailbox.EwsAllowListSpecified)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorEwsEnforceBlockListAndEwsAllowListSpecified), ErrorCategory.InvalidArgument, null);
				}
			}
			else
			{
				if (casmailbox.EwsAllowListSpecified)
				{
					casmailbox.EwsApplicationAccessPolicy = new EwsApplicationAccessPolicy?(EwsApplicationAccessPolicy.EnforceAllowList);
				}
				if (casmailbox.EwsBlockListSpecified)
				{
					casmailbox.EwsApplicationAccessPolicy = new EwsApplicationAccessPolicy?(EwsApplicationAccessPolicy.EnforceBlockList);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003510 File Offset: 0x00001710
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurable configurable = base.PrepareDataObject();
			CASMailbox casmailbox = (CASMailbox)this.GetDynamicParameters();
			if (casmailbox.ActiveSyncDebugLoggingSpecified || this.ResetAutoBlockedDevices)
			{
				ADUser user = (ADUser)configurable;
				CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, user, false, this.ConfirmationMessage, delegate(PropertyBag parameters)
				{
					if (parameters.Contains("Confirm"))
					{
						parameters.Remove("Confirm");
					}
				});
			}
			return configurable;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003580 File Offset: 0x00001780
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			CASMailbox casmailbox = (CASMailbox)this.GetDynamicParameters();
			if (casmailbox.ActiveSyncDebugLoggingSpecified || this.ResetAutoBlockedDevices)
			{
				ADUser dataObject = this.DataObject;
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(dataObject, RemotingOptions.AllowCrossSite);
				if (exchangePrincipal == null)
				{
					base.WriteVerbose(Strings.ExchangePrincipalNotFoundException(dataObject.ToString()));
					TaskLogger.LogExit();
					return;
				}
				try
				{
					using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(exchangePrincipal, CultureInfo.InvariantCulture, "Client=Management;Action=Set-CasMailbox"))
					{
						if (casmailbox.ActiveSyncDebugLoggingSpecified)
						{
							SyncStateStorage.UpdateMailboxLoggingEnabled(mailboxSession, casmailbox.ActiveSyncDebugLogging, null);
						}
						if (this.ResetAutoBlockedDevices)
						{
							List<Exception> list = DeviceBehavior.ResetAutoBlockedDevices(mailboxSession);
							foreach (Exception ex in list)
							{
								base.WriteVerbose(Strings.ResetAutoBlockedDevicesException(ex.ToString()));
							}
						}
					}
				}
				catch (LocalizedException exception)
				{
					base.WriteError(exception, (ErrorCategory)1001, this.Identity);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000036B4 File Offset: 0x000018B4
		protected override bool IsObjectStateChanged()
		{
			CASMailbox casmailbox = (CASMailbox)this.GetDynamicParameters();
			return casmailbox.ActiveSyncDebugLoggingSpecified || base.IsObjectStateChanged();
		}
	}
}
