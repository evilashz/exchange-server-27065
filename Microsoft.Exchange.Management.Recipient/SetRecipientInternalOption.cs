using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000AD RID: 173
	[Cmdlet("Set", "RecipientInternalOption", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRecipientInternalOption : SetRecipientObjectTask<RecipientIdParameter, ADRecipient, ADRecipient>
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002EC74 File Offset: 0x0002CE74
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x0002EC95 File Offset: 0x0002CE95
		[Parameter(Mandatory = false)]
		public SwitchParameter InternalOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields[ADRecipientSchema.InternalOnly] ?? false);
			}
			set
			{
				base.Fields[ADRecipientSchema.InternalOnly] = value;
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002ECB0 File Offset: 0x0002CEB0
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			if (base.Fields.IsModified(ADRecipientSchema.InternalOnly))
			{
				ADRecipient adrecipient = (ADRecipient)dataObject;
				adrecipient.InternalOnly = this.InternalOnly;
			}
			base.StampChangesOn(dataObject);
		}
	}
}
