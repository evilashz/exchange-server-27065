using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200044A RID: 1098
	[DataContract]
	public class DLPParameter : FormletParameter
	{
		// Token: 0x06003632 RID: 13874 RVA: 0x000A7A52 File Offset: 0x000A5C52
		public DLPParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel) : this(name, dialogTitle, dialogLabel, LocalizedString.Empty)
		{
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000A7A62 File Offset: 0x000A5C62
		public DLPParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, LocalizedString noSelectionText) : base(name, dialogTitle, dialogLabel)
		{
			base.FormletType = typeof(DLPModalEditor);
			if (string.IsNullOrEmpty(noSelectionText))
			{
				this.noSelectionText = Strings.TransportRuleContainsSensitiveInformationParameterNoSelectionText;
				return;
			}
			this.noSelectionText = noSelectionText;
		}

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x000A7A9F File Offset: 0x000A5C9F
		// (set) Token: 0x06003635 RID: 13877 RVA: 0x000A7AA6 File Offset: 0x000A5CA6
		[DataMember]
		public string ClassificationNameKey
		{
			get
			{
				return "displayName";
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
