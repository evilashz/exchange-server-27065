using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000494 RID: 1172
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AutomaticLinkRegularContactComparer : AutomaticLinkContactComparer
	{
		// Token: 0x060033EF RID: 13295 RVA: 0x000D325C File Offset: 0x000D145C
		private AutomaticLinkRegularContactComparer()
		{
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060033F0 RID: 13296 RVA: 0x000D3264 File Offset: 0x000D1464
		internal static AutomaticLinkRegularContactComparer Instance
		{
			get
			{
				return AutomaticLinkRegularContactComparer.instance;
			}
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000D326B File Offset: 0x000D146B
		internal override ContactLinkingOperation Match(IContactLinkingMatchProperties contact1, IContactLinkingMatchProperties contact2)
		{
			if (base.MatchEmails(contact1, contact2))
			{
				return ContactLinkingOperation.AutoLinkViaEmailAddress;
			}
			if (this.MatchIMAddresses(contact1, contact2))
			{
				return ContactLinkingOperation.AutoLinkViaIMAddress;
			}
			return ContactLinkingOperation.None;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000D3288 File Offset: 0x000D1488
		private bool MatchIMAddresses(IContactLinkingMatchProperties contact1, IContactLinkingMatchProperties contact2)
		{
			string text = this.RemoveSipPrefix(contact1.IMAddress);
			if (text != null && SmtpAddress.IsValidSmtpAddress(text))
			{
				string s = this.RemoveSipPrefix(contact2.IMAddress);
				return base.EqualsIgnoreCaseAndWhiteSpace(text, s);
			}
			return false;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000D32C4 File Offset: 0x000D14C4
		private string RemoveSipPrefix(string imAddress)
		{
			string text = imAddress;
			if (!string.IsNullOrEmpty(text) && text.StartsWith("sip:", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Substring("sip:".Length);
			}
			return text;
		}

		// Token: 0x04001BF2 RID: 7154
		internal const string SipPrefix = "sip:";

		// Token: 0x04001BF3 RID: 7155
		private static readonly AutomaticLinkRegularContactComparer instance = new AutomaticLinkRegularContactComparer();
	}
}
