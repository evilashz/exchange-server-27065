using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000D2 RID: 210
	[Serializable]
	internal class MultipleResolvedContactInfo : SimpleContactInfoBase
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001AD41 File Offset: 0x00018F41
		internal MultipleResolvedContactInfo(List<PersonalContactInfo> matches, PhoneNumber callerId, CultureInfo culture)
		{
			this.InitializeDisplayName(matches, callerId, culture);
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001AD52 File Offset: 0x00018F52
		internal override string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001AD5A File Offset: 0x00018F5A
		internal override bool ResolvesToMultipleContacts
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001AD5D File Offset: 0x00018F5D
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0001AD65 File Offset: 0x00018F65
		internal string MultipleContactNames { get; private set; }

		// Token: 0x060006EE RID: 1774 RVA: 0x0001AD70 File Offset: 0x00018F70
		private void InitializeDisplayName(List<PersonalContactInfo> matches, PhoneNumber callerId, CultureInfo culture)
		{
			this.displayName = callerId.ToDisplay;
			string firstContact = matches[0].DisplayName;
			string secondContact = matches[1].DisplayName;
			string number = MessageContentBuilder.FormatCallerIdWithAnchor(callerId, culture);
			this.MultipleContactNames = ((matches.Count == 2) ? Strings.MultipleResolvedContactDisplayWithTwoMatches(number, firstContact, secondContact).ToString(culture) : Strings.MultipleResolvedContactDisplayWithMoreThanTwoMatches(number, firstContact, secondContact).ToString(culture));
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001ADDF File Offset: 0x00018FDF
		internal override LocalizedString GetVoicemailBodyDisplayLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			return Strings.VoiceMailBodyCallerMultipleResolved(this.MultipleContactNames);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001ADEC File Offset: 0x00018FEC
		internal override LocalizedString GetMissedCallBodyDisplayLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			return Strings.MissedCallBodyCallerMultipleResolved(this.MultipleContactNames);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001ADF9 File Offset: 0x00018FF9
		internal override LocalizedString GetFaxBodyDisplayLabel(PhoneNumber callerId, CultureInfo cultureInfo)
		{
			return Strings.FaxBodyCallerMultipleResolved(this.MultipleContactNames);
		}

		// Token: 0x04000405 RID: 1029
		private string displayName;
	}
}
