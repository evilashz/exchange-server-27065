using System;
using System.Globalization;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002EA RID: 746
	internal class TranscriptionContext
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000605FC File Offset: 0x0005E7FC
		// (set) Token: 0x060016A7 RID: 5799 RVA: 0x00060604 File Offset: 0x0005E804
		internal CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
			set
			{
				this.culture = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x0006060D File Offset: 0x0005E80D
		// (set) Token: 0x060016A9 RID: 5801 RVA: 0x00060615 File Offset: 0x0005E815
		internal bool ShouldRunTranscriptionStage
		{
			get
			{
				return this.transcriptionEnabled;
			}
			set
			{
				this.transcriptionEnabled = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x0006061E File Offset: 0x0005E81E
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x00060626 File Offset: 0x0005E826
		internal TimeSpan BacklogContribution
		{
			get
			{
				return this.backlogContribution;
			}
			set
			{
				this.backlogContribution = value;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0006062F File Offset: 0x0005E82F
		// (set) Token: 0x060016AD RID: 5805 RVA: 0x00060637 File Offset: 0x0005E837
		internal TopNData TopN
		{
			get
			{
				return this.topN;
			}
			set
			{
				this.topN = value;
			}
		}

		// Token: 0x04000D67 RID: 3431
		private CultureInfo culture;

		// Token: 0x04000D68 RID: 3432
		private bool transcriptionEnabled;

		// Token: 0x04000D69 RID: 3433
		private TimeSpan backlogContribution = TimeSpan.Zero;

		// Token: 0x04000D6A RID: 3434
		private TopNData topN;
	}
}
