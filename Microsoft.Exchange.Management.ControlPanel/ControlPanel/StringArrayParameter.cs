using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000434 RID: 1076
	[DataContract]
	public class StringArrayParameter : FormletParameter
	{
		// Token: 0x060035CE RID: 13774 RVA: 0x000A7114 File Offset: 0x000A5314
		public StringArrayParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, LocalizedString noSelectionText, Type objectType) : this(name, dialogTitle, dialogLabel, noSelectionText)
		{
			if (objectType.IsArray)
			{
				this.MaxLength = StringParameter.GetMaxLength(objectType.GetElementType());
			}
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000A713C File Offset: 0x000A533C
		public StringArrayParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, Type objectType) : this(name, dialogTitle, dialogLabel, LocalizedString.Empty, objectType)
		{
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000A714E File Offset: 0x000A534E
		public StringArrayParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel) : this(name, dialogTitle, dialogLabel, LocalizedString.Empty)
		{
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000A7160 File Offset: 0x000A5360
		public StringArrayParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, LocalizedString noSelectionText) : base(name, dialogTitle, dialogLabel)
		{
			this.MaxLength = 255;
			if (string.IsNullOrEmpty(noSelectionText))
			{
				this.noSelectionText = Strings.TransportRuleStringArrayParameterNoSelectionText;
			}
			else
			{
				this.noSelectionText = noSelectionText;
			}
			base.FormletType = typeof(StringArrayModalEditor);
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000A71B4 File Offset: 0x000A53B4
		public StringArrayParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, int maxLength, LocalizedString noSelectionText, string inputWaterMarkText, string validationExpression, string validationErrorMessage) : base(name, dialogTitle, dialogLabel, null)
		{
			this.MaxLength = maxLength;
			this.noSelectionText = noSelectionText;
			this.InputWaterMarkText = inputWaterMarkText;
			this.ValidationExpression = validationExpression;
			this.ValidationErrorMessage = validationErrorMessage;
			base.FormletType = typeof(StringArrayModalEditor);
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000A7204 File Offset: 0x000A5404
		public StringArrayParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, int maxLength, LocalizedString noSelectionText, string inputWaterMarkText, string validationExpression, string validationErrorMessage, DuplicateHandlingType duplicateHandlingType) : this(name, dialogTitle, dialogLabel, maxLength, noSelectionText, inputWaterMarkText, validationExpression, validationErrorMessage)
		{
			this.DuplicateHandlingType = duplicateHandlingType;
		}

		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x060035D4 RID: 13780 RVA: 0x000A722C File Offset: 0x000A542C
		// (set) Token: 0x060035D5 RID: 13781 RVA: 0x000A7234 File Offset: 0x000A5434
		[DataMember]
		public int MaxLength { get; private set; }

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x060035D6 RID: 13782 RVA: 0x000A723D File Offset: 0x000A543D
		// (set) Token: 0x060035D7 RID: 13783 RVA: 0x000A7245 File Offset: 0x000A5445
		[DataMember]
		public string InputWaterMarkText { get; private set; }

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x060035D8 RID: 13784 RVA: 0x000A724E File Offset: 0x000A544E
		// (set) Token: 0x060035D9 RID: 13785 RVA: 0x000A7256 File Offset: 0x000A5456
		[DataMember]
		public string ValidationExpression { get; private set; }

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x060035DA RID: 13786 RVA: 0x000A725F File Offset: 0x000A545F
		// (set) Token: 0x060035DB RID: 13787 RVA: 0x000A7267 File Offset: 0x000A5467
		[DataMember]
		public string ValidationErrorMessage { get; private set; }

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x060035DC RID: 13788 RVA: 0x000A7270 File Offset: 0x000A5470
		// (set) Token: 0x060035DD RID: 13789 RVA: 0x000A7278 File Offset: 0x000A5478
		[DataMember]
		public DuplicateHandlingType DuplicateHandlingType { get; private set; }
	}
}
