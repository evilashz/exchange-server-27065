using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000437 RID: 1079
	[DataContract]
	internal class EnhancedEnumParameter : EnumParameter
	{
		// Token: 0x060035EA RID: 13802 RVA: 0x000A753D File Offset: 0x000A573D
		public EnhancedEnumParameter(string name, LocalizedString dialogTitle, LocalizedString dialogLabel, string servicePath, LocalizedString noSelectionWarningText, string defaultValue) : base(name, dialogTitle, dialogLabel, defaultValue)
		{
			this.ServicePath = servicePath;
			this.locNoSelectionWarningText = noSelectionWarningText;
			base.FormletType = typeof(EnhancedEnumComboBoxModalEditor);
		}

		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x000A756A File Offset: 0x000A576A
		// (set) Token: 0x060035EC RID: 13804 RVA: 0x000A7572 File Offset: 0x000A5772
		[DataMember]
		public string ServicePath { get; private set; }

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x000A757B File Offset: 0x000A577B
		// (set) Token: 0x060035EE RID: 13806 RVA: 0x000A758E File Offset: 0x000A578E
		[DataMember]
		public string NoSelectionWarningText
		{
			get
			{
				return this.locNoSelectionWarningText.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040025BA RID: 9658
		private LocalizedString locNoSelectionWarningText;
	}
}
