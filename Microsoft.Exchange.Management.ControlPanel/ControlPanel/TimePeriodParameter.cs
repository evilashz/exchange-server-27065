using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000440 RID: 1088
	[DataContract]
	internal class TimePeriodParameter : FormletParameter
	{
		// Token: 0x06003613 RID: 13843 RVA: 0x000A7816 File Offset: 0x000A5A16
		public TimePeriodParameter(string name) : base(name, LocalizedString.Empty, LocalizedString.Empty)
		{
			this.noSelectionText = Strings.TimePeriodParameterNoSelectionText;
			base.FormletType = typeof(TimePeriodEditor);
		}
	}
}
