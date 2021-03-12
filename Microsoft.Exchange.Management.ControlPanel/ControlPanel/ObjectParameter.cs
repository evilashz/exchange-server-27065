using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000444 RID: 1092
	[DataContract]
	public class ObjectParameter : FormletParameter
	{
		// Token: 0x06003619 RID: 13849 RVA: 0x000A78A1 File Offset: 0x000A5AA1
		public ObjectParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, Type objectType) : this(name, dialogTitle, dialogLabel, objectType, "~/Pickers/PeoplePicker.aspx", "Identity")
		{
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000A78B8 File Offset: 0x000A5AB8
		public ObjectParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, Type objectType, string pickerUrl) : this(name, dialogTitle, dialogLabel, objectType, pickerUrl, "Identity")
		{
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000A78CC File Offset: 0x000A5ACC
		public ObjectParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, Type objectType, string pickerUrl, string valueProperty) : base(name, dialogTitle, dialogLabel)
		{
			this.PickerUrl = pickerUrl;
			base.FormletType = typeof(ObjectPicker);
			this.ValueProperty = valueProperty;
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000A78F7 File Offset: 0x000A5AF7
		// (set) Token: 0x0600361D RID: 13853 RVA: 0x000A78FF File Offset: 0x000A5AFF
		[DataMember]
		public string ValueProperty { get; private set; }

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000A7908 File Offset: 0x000A5B08
		// (set) Token: 0x0600361F RID: 13855 RVA: 0x000A7910 File Offset: 0x000A5B10
		[DataMember]
		public string PickerUrl { get; private set; }
	}
}
