using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D13 RID: 3347
	[Cmdlet("Enable", "UMAutoAttendant", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class EnableUMAutoAttendant : SystemConfigurationObjectActionTask<UMAutoAttendantIdParameter, UMAutoAttendant>
	{
		// Token: 0x170027D1 RID: 10193
		// (get) Token: 0x06008070 RID: 32880 RVA: 0x0020D3E0 File Offset: 0x0020B5E0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableUMAutoAttendant(this.Identity.ToString());
			}
		}

		// Token: 0x06008071 RID: 32881 RVA: 0x0020D3F2 File Offset: 0x0020B5F2
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || ValidationHelper.IsKnownException(exception);
		}

		// Token: 0x06008072 RID: 32882 RVA: 0x0020D40C File Offset: 0x0020B60C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (this.DataObject.Status == StatusEnum.Enabled)
				{
					AutoAttendantAlreadEnabledException exception = new AutoAttendantAlreadEnabledException(this.DataObject.Name);
					base.WriteError(exception, ErrorCategory.InvalidOperation, null);
				}
				if (this.DataObject.DTMFFallbackAutoAttendant != null)
				{
					ValidationHelper.ValidateLinkedAutoAttendant(this.ConfigurationSession, this.DataObject.DTMFFallbackAutoAttendant.Name, true, this.DataObject);
				}
				if (this.DataObject.BusinessHoursKeyMappingEnabled)
				{
					foreach (CustomMenuKeyMapping customMenuKeyMapping in this.DataObject.BusinessHoursKeyMapping)
					{
						if (!string.IsNullOrEmpty(customMenuKeyMapping.AutoAttendantName))
						{
							ValidationHelper.ValidateLinkedAutoAttendant(this.ConfigurationSession, customMenuKeyMapping.AutoAttendantName, true, this.DataObject);
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x0020D500 File Offset: 0x0020B700
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.DataObject.SetStatus(StatusEnum.Enabled);
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AutoAttendantEnabled, null, new object[]
				{
					this.DataObject.Identity.ToString()
				});
			}
			TaskLogger.LogExit();
		}
	}
}
