﻿using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D0C RID: 3340
	[Cmdlet("Disable", "UMService", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class DisableUMServer : SystemConfigurationObjectActionTask<UMServerIdParameter, Server>
	{
		// Token: 0x170027BB RID: 10171
		// (get) Token: 0x0600803A RID: 32826 RVA: 0x0020C723 File Offset: 0x0020A923
		// (set) Token: 0x0600803B RID: 32827 RVA: 0x0020C73A File Offset: 0x0020A93A
		[Parameter(Mandatory = false)]
		public bool Immediate
		{
			get
			{
				return (bool)base.Fields["Immediate"];
			}
			set
			{
				base.Fields["Immediate"] = value;
			}
		}

		// Token: 0x170027BC RID: 10172
		// (get) Token: 0x0600803C RID: 32828 RVA: 0x0020C752 File Offset: 0x0020A952
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (base.Fields["Immediate"] != null && (bool)base.Fields["Immediate"])
				{
					return Strings.ConfirmationMessageDisableUMServerImmediately;
				}
				return Strings.ConfirmationMessageDisableUMServer;
			}
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x0020C788 File Offset: 0x0020A988
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (this.DataObject.IsE15OrLater)
				{
					base.WriteError(new StatusChangeException(this.DataObject.Name), ErrorCategory.InvalidOperation, null);
				}
				if (base.Fields["Immediate"] != null && (bool)base.Fields["Immediate"])
				{
					if (this.DataObject.Status == ServerStatus.Disabled)
					{
						UMServerAlreadDisabledException exception = new UMServerAlreadDisabledException(this.DataObject.Name);
						base.WriteError(exception, ErrorCategory.InvalidOperation, null);
						return;
					}
				}
				else
				{
					if (this.DataObject.Status == ServerStatus.NoNewCalls)
					{
						UMServerAlreadDisabledException exception2 = new UMServerAlreadDisabledException(this.DataObject.Name);
						base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
						return;
					}
					if (this.DataObject.Status == ServerStatus.Disabled)
					{
						InvalidUMServerStateOperationException exception3 = new InvalidUMServerStateOperationException(this.DataObject.Name);
						base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
						return;
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x0020C87C File Offset: 0x0020AA7C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.Fields["Immediate"] != null && (bool)base.Fields["Immediate"])
			{
				this.DataObject.Status = ServerStatus.Disabled;
			}
			else
			{
				this.DataObject.Status = ServerStatus.NoNewCalls;
			}
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServerDisabled, null, new object[]
				{
					this.DataObject.Name
				});
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003ED7 RID: 16087
		private const string ImmediateField = "Immediate";
	}
}
