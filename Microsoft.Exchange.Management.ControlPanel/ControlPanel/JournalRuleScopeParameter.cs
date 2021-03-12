using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000447 RID: 1095
	[DataContract]
	public class JournalRuleScopeParameter : HiddenParameter
	{
		// Token: 0x06003625 RID: 13861 RVA: 0x000A7984 File Offset: 0x000A5B84
		public JournalRuleScopeParameter(string defaultText) : base(defaultText, LocalizedString.Empty, LocalizedString.Empty, new string[]
		{
			"Scope"
		}, true)
		{
			this.DefaultText = defaultText;
		}

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000A79BF File Offset: 0x000A5BBF
		// (set) Token: 0x06003627 RID: 13863 RVA: 0x000A79C7 File Offset: 0x000A5BC7
		[DataMember]
		public string DefaultText { get; private set; }
	}
}
