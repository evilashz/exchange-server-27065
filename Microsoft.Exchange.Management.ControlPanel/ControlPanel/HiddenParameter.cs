using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000445 RID: 1093
	[DataContract]
	public class HiddenParameter : FormletParameter
	{
		// Token: 0x06003620 RID: 13856 RVA: 0x000A7919 File Offset: 0x000A5B19
		public HiddenParameter(string name, object value) : base(name, LocalizedString.Empty, LocalizedString.Empty)
		{
			this.Value = value;
			base.ExactMatch = true;
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000A793A File Offset: 0x000A5B3A
		public HiddenParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, string[] taskParameterNames, object value) : base(name, dialogTitle, dialogLabel, taskParameterNames)
		{
			this.Value = value;
			base.ExactMatch = true;
		}

		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000A7956 File Offset: 0x000A5B56
		// (set) Token: 0x06003623 RID: 13859 RVA: 0x000A795E File Offset: 0x000A5B5E
		[DataMember]
		public object Value { get; set; }
	}
}
