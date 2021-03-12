using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000432 RID: 1074
	[DataContract]
	internal class NumberParameter : FormletParameter
	{
		// Token: 0x060035C3 RID: 13763 RVA: 0x000A7015 File Offset: 0x000A5215
		public NumberParameter(string name, LocalizedString displayName, LocalizedString description, long minValue, long maxValue, long defaultValue) : base(name, displayName, description)
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this.DefaultValue = defaultValue;
			base.FormletType = typeof(NumberModalEditor);
		}

		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x000A7048 File Offset: 0x000A5248
		// (set) Token: 0x060035C5 RID: 13765 RVA: 0x000A7050 File Offset: 0x000A5250
		[DataMember]
		public long MinValue { get; private set; }

		// Token: 0x17002111 RID: 8465
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000A7059 File Offset: 0x000A5259
		// (set) Token: 0x060035C7 RID: 13767 RVA: 0x000A7061 File Offset: 0x000A5261
		[DataMember]
		public long MaxValue { get; private set; }

		// Token: 0x17002112 RID: 8466
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000A706A File Offset: 0x000A526A
		// (set) Token: 0x060035C9 RID: 13769 RVA: 0x000A7072 File Offset: 0x000A5272
		[DataMember]
		public long DefaultValue { get; private set; }
	}
}
