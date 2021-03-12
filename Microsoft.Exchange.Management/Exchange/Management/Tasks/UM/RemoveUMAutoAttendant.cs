using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.Prompts.Provisioning;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D3B RID: 3387
	[Cmdlet("Remove", "UMAutoAttendant", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveUMAutoAttendant : RemoveSystemConfigurationObjectTask<UMAutoAttendantIdParameter, UMAutoAttendant>
	{
		// Token: 0x17002851 RID: 10321
		// (get) Token: 0x060081CF RID: 33231 RVA: 0x00212CF4 File Offset: 0x00210EF4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveUMAutoAttendant(this.Identity.ToString());
			}
		}

		// Token: 0x060081D0 RID: 33232 RVA: 0x00212D06 File Offset: 0x00210F06
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || ValidationHelper.IsKnownException(exception);
		}

		// Token: 0x060081D1 RID: 33233 RVA: 0x00212D20 File Offset: 0x00210F20
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				UMDialPlan dialPlan = base.DataObject.GetDialPlan();
				if (dialPlan == null)
				{
					DialPlanNotFoundException exception = new DialPlanNotFoundException(base.DataObject.UMDialPlan.Name);
					base.WriteError(exception, ErrorCategory.InvalidOperation, null);
				}
				else
				{
					ValidationHelper.ValidateDisabledAA(this.ConfigurationSession, dialPlan, base.DataObject);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x00212D87 File Offset: 0x00210F87
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.DeleteContentFromPublishingPoint();
			if (!base.HasErrors)
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060081D3 RID: 33235 RVA: 0x00212DA8 File Offset: 0x00210FA8
		private void DeleteContentFromPublishingPoint()
		{
			if (base.DataObject.GetDialPlan() == null)
			{
				throw new DialPlanNotFoundException(base.DataObject.UMDialPlan.Name);
			}
			try
			{
				using (IPublishingSession publishingSession = PublishingPoint.GetPublishingSession(Environment.UserName, base.DataObject))
				{
					publishingSession.Delete();
				}
			}
			catch (PublishingException ex)
			{
				base.WriteWarning(ex.Message);
			}
		}
	}
}
