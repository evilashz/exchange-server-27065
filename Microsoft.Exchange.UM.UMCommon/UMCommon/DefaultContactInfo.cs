using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	internal class DefaultContactInfo : SimpleContactInfoBase
	{
		// Token: 0x06000374 RID: 884 RVA: 0x0000CE5D File Offset: 0x0000B05D
		internal DefaultContactInfo()
		{
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000CE70 File Offset: 0x0000B070
		internal override ICollection<string> SanitizedPhoneNumbers
		{
			get
			{
				return this.sanitizedPhoneNumbers;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000CE78 File Offset: 0x0000B078
		internal override LocalizedString GetVoicemailBodyDisplayLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			return Strings.VoiceMailBodyCallerUnresolved(MessageContentBuilder.FormatCallerIdWithAnchor(callerId, cultureInfo));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000CE86 File Offset: 0x0000B086
		internal override LocalizedString GetMissedCallBodyDisplayLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			return Strings.MissedCallBodyCallerUnresolved(MessageContentBuilder.FormatCallerIdWithAnchor(callerId, cultureInfo));
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000CE94 File Offset: 0x0000B094
		internal override LocalizedString GetFaxBodyDisplayLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			return Strings.FaxBodyCallerUnresolved(MessageContentBuilder.FormatCallerIdWithAnchor(callerId, cultureInfo));
		}

		// Token: 0x0400028E RID: 654
		private List<string> sanitizedPhoneNumbers = new List<string>();
	}
}
