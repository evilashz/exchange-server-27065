using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000438 RID: 1080
	[DataContract]
	internal class NumberRangeParameter : FormletParameter
	{
		// Token: 0x060035EF RID: 13807 RVA: 0x000A7598 File Offset: 0x000A5798
		public NumberRangeParameter(string name, string[] taskParameterNames, LocalizedString dialogTitle, LocalizedString atMostOnlyDisplayTemplate, LocalizedString atLeastAtMostDisplayTemplate) : base(name, dialogTitle, LocalizedString.Empty, taskParameterNames)
		{
			this.locAtMostOnlyDisplayTemplate = atMostOnlyDisplayTemplate;
			this.locAtLeastAtMostDisplayTemplate = atLeastAtMostDisplayTemplate;
			this.MaxValue = 999999;
			this.MinValue = 0;
			base.FormletType = typeof(NumberRangeModalEditor);
		}

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x060035F0 RID: 13808 RVA: 0x000A75E5 File Offset: 0x000A57E5
		// (set) Token: 0x060035F1 RID: 13809 RVA: 0x000A75ED File Offset: 0x000A57ED
		[DataMember]
		public int MaxValue { get; private set; }

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x060035F2 RID: 13810 RVA: 0x000A75F6 File Offset: 0x000A57F6
		// (set) Token: 0x060035F3 RID: 13811 RVA: 0x000A75FE File Offset: 0x000A57FE
		[DataMember]
		public int MinValue { get; private set; }

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x000A7607 File Offset: 0x000A5807
		// (set) Token: 0x060035F5 RID: 13813 RVA: 0x000A761A File Offset: 0x000A581A
		[DataMember]
		public string AtMostOnlyDisplayTemplate
		{
			get
			{
				return this.locAtMostOnlyDisplayTemplate.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x060035F6 RID: 13814 RVA: 0x000A7621 File Offset: 0x000A5821
		// (set) Token: 0x060035F7 RID: 13815 RVA: 0x000A7634 File Offset: 0x000A5834
		[DataMember]
		public string AtLeastAtMostDisplayTemplate
		{
			get
			{
				return this.locAtLeastAtMostDisplayTemplate.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000A763B File Offset: 0x000A583B
		internal static int GetMaxValue(Type strongType)
		{
			return FormletParameter.GetIntFieldValue(strongType, "MaxValue", int.MaxValue);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000A764D File Offset: 0x000A584D
		internal static int GetMinValue(Type strongType)
		{
			return FormletParameter.GetIntFieldValue(strongType, "MinValue", 0);
		}

		// Token: 0x040025BC RID: 9660
		private LocalizedString locAtMostOnlyDisplayTemplate;

		// Token: 0x040025BD RID: 9661
		private LocalizedString locAtLeastAtMostDisplayTemplate;
	}
}
