﻿using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D0D RID: 3341
	[Cmdlet("Disable", "UMAutoAttendant", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public class DisableUMAutoAttendant : SystemConfigurationObjectActionTask<UMAutoAttendantIdParameter, UMAutoAttendant>
	{
		// Token: 0x170027BD RID: 10173
		// (get) Token: 0x06008040 RID: 32832 RVA: 0x0020C913 File Offset: 0x0020AB13
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableUMAutoAttendant(this.Identity.ToString());
			}
		}

		// Token: 0x06008041 RID: 32833 RVA: 0x0020C925 File Offset: 0x0020AB25
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || ValidationHelper.IsKnownException(exception);
		}

		// Token: 0x06008042 RID: 32834 RVA: 0x0020C940 File Offset: 0x0020AB40
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (this.DataObject.Status == StatusEnum.Disabled)
				{
					AutoAttendantAlreadDisabledException exception = new AutoAttendantAlreadDisabledException(this.DataObject.Name);
					base.WriteError(exception, ErrorCategory.InvalidOperation, null);
				}
				else
				{
					UMDialPlan dialPlan = this.DataObject.GetDialPlan();
					if (dialPlan == null)
					{
						DialPlanNotFoundException exception2 = new DialPlanNotFoundException(this.DataObject.UMDialPlan.Name);
						base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
					}
					else
					{
						ValidationHelper.ValidateDisabledAA(this.ConfigurationSession, dialPlan, this.DataObject);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008043 RID: 32835 RVA: 0x0020C9D4 File Offset: 0x0020ABD4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.DataObject.SetStatus(StatusEnum.Disabled);
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AutoAttendantDisabled, null, new object[]
				{
					this.DataObject.Identity
				});
			}
			TaskLogger.LogExit();
		}
	}
}
