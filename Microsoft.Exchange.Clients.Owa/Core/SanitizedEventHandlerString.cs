using System;
using System.Text;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002E2 RID: 738
	public sealed class SanitizedEventHandlerString : SanitizedStringBase<OwaHtml>
	{
		// Token: 0x06001C58 RID: 7256 RVA: 0x000A26E4 File Offset: 0x000A08E4
		public SanitizedEventHandlerString(string eventName, string handlerCode, bool returnFalse)
		{
			if (eventName == null)
			{
				throw new ArgumentNullException("eventName");
			}
			if (handlerCode == null)
			{
				throw new ArgumentNullException("handlerCode");
			}
			string text = Utilities.HtmlEncode(handlerCode);
			int capacity = eventName.Length * 3 + text.Length + 100;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			stringBuilder.Append(eventName);
			stringBuilder.Append("='_e(this,this.getAttribute(\"_e_");
			stringBuilder.Append(eventName);
			stringBuilder.Append("\"), event)");
			if (returnFalse)
			{
				stringBuilder.Append(";return false;");
			}
			stringBuilder.Append("' _e_");
			stringBuilder.Append(eventName);
			stringBuilder.Append("=\"");
			stringBuilder.Append(text);
			stringBuilder.Append("\"");
			base.TrustedValue = stringBuilder.ToString();
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000A27AB File Offset: 0x000A09AB
		public SanitizedEventHandlerString(string eventName, string handlerCode) : this(eventName, handlerCode, false)
		{
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000A27B6 File Offset: 0x000A09B6
		private SanitizedEventHandlerString()
		{
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000A27BE File Offset: 0x000A09BE
		protected override string Sanitize(string rawValue)
		{
			return base.TrustedValue;
		}
	}
}
