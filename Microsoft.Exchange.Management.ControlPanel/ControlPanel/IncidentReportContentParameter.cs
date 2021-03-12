using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200044D RID: 1101
	[DataContract]
	public class IncidentReportContentParameter : FormletParameter
	{
		// Token: 0x0600363E RID: 13886 RVA: 0x000A7B7C File Offset: 0x000A5D7C
		public IncidentReportContentParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, LocalizedString noSelectionText) : base(name, dialogTitle, dialogLabel, new string[]
		{
			"IncidentReportContent"
		})
		{
			base.FormletType = typeof(IncidentReportContentEditor);
			this.noSelectionText = noSelectionText;
			base.RequiredField = true;
		}
	}
}
