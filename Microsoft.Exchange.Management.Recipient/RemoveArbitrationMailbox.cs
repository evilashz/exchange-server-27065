using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000074 RID: 116
	[Cmdlet("Remove", "ArbitrationMailbox", DefaultParameterSetName = "Identity", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveArbitrationMailbox : RemoveMailboxBase<MailboxIdParameter>
	{
		// Token: 0x06000884 RID: 2180 RVA: 0x00025E7C File Offset: 0x0002407C
		public RemoveArbitrationMailbox()
		{
			base.Arbitration = new SwitchParameter(true);
			base.RemoveLastArbitrationMailboxAllowed = new SwitchParameter(true);
			base.Permanent = true;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00025EA3 File Offset: 0x000240A3
		internal override bool ArbitrationMailboxUsageValidationRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00025EA8 File Offset: 0x000240A8
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (adrecipient.RecipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidArbitrationMbxType(adrecipient.Id.ToString())), ExchangeErrorCategory.Client, this.Identity);
			}
			return adrecipient;
		}
	}
}
