using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000431 RID: 1073
	[DataContract]
	internal class ADAttributeParameter : FormletParameter
	{
		// Token: 0x060035C1 RID: 13761 RVA: 0x000A6FC8 File Offset: 0x000A51C8
		public ADAttributeParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel) : this(name, dialogTitle, dialogLabel, LocalizedString.Empty)
		{
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000A6FD8 File Offset: 0x000A51D8
		public ADAttributeParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, LocalizedString noSelectionText) : base(name, dialogTitle, dialogLabel)
		{
			base.FormletType = typeof(ADAttributeModalEditor);
			if (string.IsNullOrEmpty(noSelectionText))
			{
				this.noSelectionText = Strings.TransportRuleADAttributeParameterNoSelectionText;
				return;
			}
			this.noSelectionText = noSelectionText;
		}
	}
}
