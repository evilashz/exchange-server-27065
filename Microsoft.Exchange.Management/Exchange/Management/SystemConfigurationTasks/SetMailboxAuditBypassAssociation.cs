using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000425 RID: 1061
	[Cmdlet("Set", "MailboxAuditBypassAssociation", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxAuditBypassAssociation : SetRecipientObjectTask<MailboxAuditBypassAssociationIdParameter, MailboxAuditBypassAssociation, ADRecipient>
	{
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x00093A20 File Offset: 0x00091C20
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMailboxAuditBypassAssociation(this.Identity.ToString());
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x00093A32 File Offset: 0x00091C32
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x00093A49 File Offset: 0x00091C49
		[Parameter(Mandatory = true)]
		public bool AuditBypassEnabled
		{
			get
			{
				return (bool)base.Fields[ADRecipientSchema.AuditBypassEnabled];
			}
			set
			{
				base.Fields[ADRecipientSchema.AuditBypassEnabled] = value;
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00093A64 File Offset: 0x00091C64
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			ADRecipient adrecipient = (ADRecipient)dataObject;
			adrecipient.BypassAudit = this.AuditBypassEnabled;
			base.StampChangesOn(adrecipient);
			TaskLogger.LogExit();
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x00093A98 File Offset: 0x00091C98
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADRecipient dataObject2 = (ADRecipient)dataObject;
			return MailboxAuditBypassAssociation.FromDataObject(dataObject2);
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x00093AB2 File Offset: 0x00091CB2
		internal new SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return new SwitchParameter(false);
			}
		}
	}
}
