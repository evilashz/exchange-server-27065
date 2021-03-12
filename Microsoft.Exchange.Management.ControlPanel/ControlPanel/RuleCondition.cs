using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200042E RID: 1070
	[DataContract]
	public class RuleCondition : RulePhrase
	{
		// Token: 0x06003597 RID: 13719 RVA: 0x000A6C78 File Offset: 0x000A4E78
		public RuleCondition(string name, LocalizedString displayText, FormletParameter[] ruleParameters, string additionalRoles, LocalizedString namingFormat, bool isDisplayedInSimpleMode) : this(name, displayText, ruleParameters, additionalRoles, namingFormat, LocalizedString.Empty, LocalizedString.Empty, LocalizedString.Empty, LocalizedString.Empty, isDisplayedInSimpleMode)
		{
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000A6CA8 File Offset: 0x000A4EA8
		public RuleCondition(string name, LocalizedString displayText, FormletParameter[] ruleParameters, string additionalRoles, LocalizedString namingFormat, LocalizedString groupText, LocalizedString flyOutText, LocalizedString preCannedText, bool isDisplayedInSimpleMode) : this(name, displayText, ruleParameters, additionalRoles, namingFormat, groupText, flyOutText, preCannedText, LocalizedString.Empty, isDisplayedInSimpleMode)
		{
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000A6CD0 File Offset: 0x000A4ED0
		public RuleCondition(string name, LocalizedString displayText, FormletParameter[] ruleParameters, string additionalRoles, LocalizedString namingFormat, LocalizedString groupText, LocalizedString flyOutText, LocalizedString preCannedText, LocalizedString explanationText, bool isDisplayedInSimpleMode) : base(name, displayText, ruleParameters, additionalRoles, groupText, flyOutText, explanationText, isDisplayedInSimpleMode)
		{
			this.namingFormat = namingFormat;
			this.preCannedText = preCannedText;
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000A6D00 File Offset: 0x000A4F00
		// (set) Token: 0x0600359B RID: 13723 RVA: 0x000A6D13 File Offset: 0x000A4F13
		[DataMember]
		public string NamingFormat
		{
			get
			{
				return this.namingFormat.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x000A6D1A File Offset: 0x000A4F1A
		// (set) Token: 0x0600359D RID: 13725 RVA: 0x000A6D2D File Offset: 0x000A4F2D
		[DataMember(EmitDefaultValue = false)]
		public string PreCannedText
		{
			get
			{
				return this.preCannedText.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040025A0 RID: 9632
		private LocalizedString namingFormat;

		// Token: 0x040025A1 RID: 9633
		private LocalizedString preCannedText;
	}
}
