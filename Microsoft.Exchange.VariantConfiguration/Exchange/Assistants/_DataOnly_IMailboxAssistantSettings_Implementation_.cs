using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200007C RID: 124
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IMailboxAssistantSettings_Implementation_ : IMailboxAssistantSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00005E54 File Offset: 0x00004054
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00005E57 File Offset: 0x00004057
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00005E5A File Offset: 0x0000405A
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00005E62 File Offset: 0x00004062
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00005E6A File Offset: 0x0000406A
		public TimeSpan MailboxNotInterestingLogInterval
		{
			get
			{
				return this._MailboxNotInterestingLogInterval_MaterializedValue_;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00005E72 File Offset: 0x00004072
		public bool SpreadLoad
		{
			get
			{
				return this._SpreadLoad_MaterializedValue_;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00005E7A File Offset: 0x0000407A
		public bool SlaMonitoringEnabled
		{
			get
			{
				return this._SlaMonitoringEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00005E82 File Offset: 0x00004082
		public bool CompletionMonitoringEnabled
		{
			get
			{
				return this._CompletionMonitoringEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00005E8A File Offset: 0x0000408A
		public bool ActiveDatabaseProcessingMonitoringEnabled
		{
			get
			{
				return this._ActiveDatabaseProcessingMonitoringEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00005E92 File Offset: 0x00004092
		public float SlaUrgentThreshold
		{
			get
			{
				return this._SlaUrgentThreshold_MaterializedValue_;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00005E9A File Offset: 0x0000409A
		public float SlaNonUrgentThreshold
		{
			get
			{
				return this._SlaNonUrgentThreshold_MaterializedValue_;
			}
		}

		// Token: 0x04000240 RID: 576
		internal string _Name_MaterializedValue_;

		// Token: 0x04000241 RID: 577
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x04000242 RID: 578
		internal TimeSpan _MailboxNotInterestingLogInterval_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000243 RID: 579
		internal bool _SpreadLoad_MaterializedValue_;

		// Token: 0x04000244 RID: 580
		internal bool _SlaMonitoringEnabled_MaterializedValue_;

		// Token: 0x04000245 RID: 581
		internal bool _CompletionMonitoringEnabled_MaterializedValue_;

		// Token: 0x04000246 RID: 582
		internal bool _ActiveDatabaseProcessingMonitoringEnabled_MaterializedValue_;

		// Token: 0x04000247 RID: 583
		internal float _SlaUrgentThreshold_MaterializedValue_;

		// Token: 0x04000248 RID: 584
		internal float _SlaNonUrgentThreshold_MaterializedValue_;
	}
}
