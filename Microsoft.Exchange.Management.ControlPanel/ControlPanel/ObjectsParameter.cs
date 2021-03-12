using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000449 RID: 1097
	[DataContract]
	public class ObjectsParameter : ObjectArrayParameter
	{
		// Token: 0x06003629 RID: 13865 RVA: 0x000A79E3 File Offset: 0x000A5BE3
		public ObjectsParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, LocalizedString noSelectionText, string pickerUrl) : base(name, dialogTitle, dialogLabel)
		{
			this.PickerUrl = pickerUrl;
			this.noSelectionText = noSelectionText;
			base.FormletType = typeof(ObjectPicker);
		}

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000A7A0E File Offset: 0x000A5C0E
		// (set) Token: 0x0600362B RID: 13867 RVA: 0x000A7A16 File Offset: 0x000A5C16
		[DataMember]
		public string PickerUrl { get; private set; }

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000A7A1F File Offset: 0x000A5C1F
		// (set) Token: 0x0600362D RID: 13869 RVA: 0x000A7A27 File Offset: 0x000A5C27
		[DataMember]
		public string ValueProperty { get; set; }

		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x000A7A30 File Offset: 0x000A5C30
		// (set) Token: 0x0600362F RID: 13871 RVA: 0x000A7A38 File Offset: 0x000A5C38
		[DataMember]
		public int DialogWidth { get; set; }

		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000A7A41 File Offset: 0x000A5C41
		// (set) Token: 0x06003631 RID: 13873 RVA: 0x000A7A49 File Offset: 0x000A5C49
		[DataMember]
		public int DialogHeight { get; set; }
	}
}
