using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200043D RID: 1085
	[DataContract]
	internal class PeopleParameter : ObjectArrayParameter
	{
		// Token: 0x0600360C RID: 13836 RVA: 0x000A7730 File Offset: 0x000A5930
		public PeopleParameter(string name, PickerType pickerType, LocalizedString noSelectionText) : base(name, LocalizedString.Empty, LocalizedString.Empty)
		{
			this.PickerType = pickerType.ToString();
			if (string.IsNullOrEmpty(noSelectionText))
			{
				this.noSelectionText = Strings.TransportRulePeopleParameterNoSelectionText;
			}
			else
			{
				this.noSelectionText = noSelectionText;
			}
			base.FormletType = typeof(PeoplePicker);
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000A7790 File Offset: 0x000A5990
		public PeopleParameter(string name, PickerType pickerType) : this(name, pickerType, LocalizedString.Empty)
		{
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000A779F File Offset: 0x000A599F
		public PeopleParameter(string name) : this(name, Microsoft.Exchange.Management.ControlPanel.PickerType.PickTo)
		{
		}

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000A77A9 File Offset: 0x000A59A9
		// (set) Token: 0x06003610 RID: 13840 RVA: 0x000A77B1 File Offset: 0x000A59B1
		[DataMember]
		public string PickerType { get; private set; }
	}
}
