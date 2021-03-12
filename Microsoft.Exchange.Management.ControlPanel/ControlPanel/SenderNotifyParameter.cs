using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200044C RID: 1100
	[DataContract]
	public class SenderNotifyParameter : FormletParameter
	{
		// Token: 0x0600363B RID: 13883 RVA: 0x000A7AD8 File Offset: 0x000A5CD8
		public SenderNotifyParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, LocalizedString noSelectionText, string[] taskParameterNames) : base(name, dialogTitle, dialogLabel, taskParameterNames)
		{
			base.FormletType = typeof(SenderNotifyEditor);
			if (string.IsNullOrEmpty(noSelectionText))
			{
				this.noSelectionText = Strings.TransportRuleContainsSensitiveInformationParameterNoSelectionText;
			}
			else
			{
				this.noSelectionText = noSelectionText;
			}
			Array values = Enum.GetValues(typeof(NotifySenderType));
			EnumValue[] array = new EnumValue[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = new EnumValue((Enum)values.GetValue(i));
			}
			this.Values = array;
		}

		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x0600363C RID: 13884 RVA: 0x000A7B6B File Offset: 0x000A5D6B
		// (set) Token: 0x0600363D RID: 13885 RVA: 0x000A7B73 File Offset: 0x000A5D73
		[DataMember]
		public EnumValue[] Values { get; internal set; }
	}
}
