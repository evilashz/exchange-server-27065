using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000430 RID: 1072
	[DataContract]
	internal class StringParameter : FormletParameter
	{
		// Token: 0x060035B4 RID: 13748 RVA: 0x000A6EC6 File Offset: 0x000A50C6
		public StringParameter(string name, LocalizedString displayName, LocalizedString description, Type objectType, bool multiLine) : this(name, displayName, description, objectType, multiLine, string.Empty)
		{
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000A6EDC File Offset: 0x000A50DC
		public StringParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, Type objectType, bool multiLine, string defaultText) : base(name, dialogTitle, dialogLabel)
		{
			this.MaxLength = StringParameter.GetMaxLength(objectType);
			this.MultiLine = multiLine;
			this.ValidatingExpression = StringParameter.GetValidatingExpression(objectType);
			this.DefaultText = defaultText;
			this.noSelectionText = Strings.TransportRuleStringParameterNoSelectionText;
			base.FormletType = (this.MultiLine ? typeof(MultilineStringModalEditor) : typeof(StringModalEditor));
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000A6F4B File Offset: 0x000A514B
		public StringParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, Type objectType, bool multiLine, string defaultText, LocalizedString noSelectionText) : this(name, dialogTitle, dialogLabel, objectType, multiLine, defaultText)
		{
			this.noSelectionText = noSelectionText;
		}

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x060035B7 RID: 13751 RVA: 0x000A6F64 File Offset: 0x000A5164
		// (set) Token: 0x060035B8 RID: 13752 RVA: 0x000A6F6C File Offset: 0x000A516C
		[DataMember]
		public int MaxLength { get; private set; }

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000A6F75 File Offset: 0x000A5175
		// (set) Token: 0x060035BA RID: 13754 RVA: 0x000A6F7D File Offset: 0x000A517D
		[DataMember]
		public bool MultiLine { get; private set; }

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x000A6F86 File Offset: 0x000A5186
		// (set) Token: 0x060035BC RID: 13756 RVA: 0x000A6F8E File Offset: 0x000A518E
		[DataMember]
		public string ValidatingExpression { get; private set; }

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x000A6F97 File Offset: 0x000A5197
		// (set) Token: 0x060035BE RID: 13758 RVA: 0x000A6F9F File Offset: 0x000A519F
		[DataMember]
		public string DefaultText { get; private set; }

		// Token: 0x060035BF RID: 13759 RVA: 0x000A6FA8 File Offset: 0x000A51A8
		internal static int GetMaxLength(Type strongType)
		{
			return FormletParameter.GetIntFieldValue(strongType, "MaxLength", 0);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000A6FB6 File Offset: 0x000A51B6
		internal static string GetValidatingExpression(Type strongType)
		{
			return FormletParameter.GetStringFieldValue(strongType, "ValidatingExpression", string.Empty);
		}
	}
}
