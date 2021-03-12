using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000433 RID: 1075
	[DataContract]
	internal class ByteQuantifiedSizeParameter : NumberParameter
	{
		// Token: 0x060035CA RID: 13770 RVA: 0x000A707B File Offset: 0x000A527B
		public ByteQuantifiedSizeParameter(string name, LocalizedString displayName, LocalizedString description, long minValue, long maxValue, long defaultValue) : this(name, displayName, description, minValue, maxValue, defaultValue, LocalizedString.Empty)
		{
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000A7091 File Offset: 0x000A5291
		public ByteQuantifiedSizeParameter(string name, LocalizedString displayName, LocalizedString description, long minValue, long maxValue, long defaultValue, LocalizedString noSelectionText) : base(name, displayName, description, minValue, maxValue, defaultValue)
		{
			if (string.IsNullOrEmpty(noSelectionText))
			{
				this.noSelectionText = Strings.TransportRuleStringParameterNoSelectionText;
				return;
			}
			this.noSelectionText = noSelectionText;
		}

		// Token: 0x17002113 RID: 8467
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x000A70C4 File Offset: 0x000A52C4
		// (set) Token: 0x060035CD RID: 13773 RVA: 0x000A710D File Offset: 0x000A530D
		[DataMember]
		public string[] Unit
		{
			get
			{
				return new string[]
				{
					Strings.MailboxUsageUnitKB,
					Strings.MailboxUsageUnitMB,
					Strings.MailboxUsageUnitGB,
					Strings.MailboxUsageUnitTB
				};
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
