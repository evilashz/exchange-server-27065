using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200042B RID: 1067
	public static class ADRecipientOrAddressExtensions
	{
		// Token: 0x0600356B RID: 13675 RVA: 0x000A6064 File Offset: 0x000A4264
		public static ADRecipientOrAddress ToADRecipientOrAddress(this SmtpAddress? smtpAddress)
		{
			if (smtpAddress == null)
			{
				return null;
			}
			Participant participant = new Participant(smtpAddress.Value.ToString(), smtpAddress.Value.ToString(), "SMTP");
			return new ADRecipientOrAddress(participant);
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000A60B8 File Offset: 0x000A42B8
		public static ADRecipientOrAddress ToADRecipientOrAddress(this ADIdParameter identity)
		{
			Participant participant = new Participant(identity.ToString(), identity.ToString(), "SMTP");
			return new ADRecipientOrAddress(participant);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000A60EA File Offset: 0x000A42EA
		public static IEnumerable<ADRecipientOrAddress> ToADRecipientOrAddresses(this IEnumerable<ADIdParameter> identities)
		{
			if (identities != null && identities.Count<ADIdParameter>() > 0)
			{
				return from identity in identities
				select identity.ToADRecipientOrAddress();
			}
			return null;
		}
	}
}
