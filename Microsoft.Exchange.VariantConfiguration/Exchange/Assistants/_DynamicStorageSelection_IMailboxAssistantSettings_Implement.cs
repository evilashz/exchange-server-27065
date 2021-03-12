using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200007B RID: 123
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IMailboxAssistantSettings_Implementation_ : IMailboxAssistantSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IMailboxAssistantSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00005C97 File Offset: 0x00003E97
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00005C9F File Offset: 0x00003E9F
		_DynamicStorageSelection_IMailboxAssistantSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IMailboxAssistantSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00005CA7 File Offset: 0x00003EA7
		void IDataAccessorBackedObject<_DynamicStorageSelection_IMailboxAssistantSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IMailboxAssistantSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00005CB7 File Offset: 0x00003EB7
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00005CC4 File Offset: 0x00003EC4
		public bool Enabled
		{
			get
			{
				if (this.dataAccessor._Enabled_ValueProvider_ != null)
				{
					return this.dataAccessor._Enabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00005CF5 File Offset: 0x00003EF5
		public TimeSpan MailboxNotInterestingLogInterval
		{
			get
			{
				if (this.dataAccessor._MailboxNotInterestingLogInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._MailboxNotInterestingLogInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MailboxNotInterestingLogInterval_MaterializedValue_;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00005D26 File Offset: 0x00003F26
		public bool SpreadLoad
		{
			get
			{
				if (this.dataAccessor._SpreadLoad_ValueProvider_ != null)
				{
					return this.dataAccessor._SpreadLoad_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SpreadLoad_MaterializedValue_;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00005D57 File Offset: 0x00003F57
		public bool SlaMonitoringEnabled
		{
			get
			{
				if (this.dataAccessor._SlaMonitoringEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._SlaMonitoringEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SlaMonitoringEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00005D88 File Offset: 0x00003F88
		public bool CompletionMonitoringEnabled
		{
			get
			{
				if (this.dataAccessor._CompletionMonitoringEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._CompletionMonitoringEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._CompletionMonitoringEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00005DB9 File Offset: 0x00003FB9
		public bool ActiveDatabaseProcessingMonitoringEnabled
		{
			get
			{
				if (this.dataAccessor._ActiveDatabaseProcessingMonitoringEnabled_ValueProvider_ != null)
				{
					return this.dataAccessor._ActiveDatabaseProcessingMonitoringEnabled_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ActiveDatabaseProcessingMonitoringEnabled_MaterializedValue_;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00005DEA File Offset: 0x00003FEA
		public float SlaUrgentThreshold
		{
			get
			{
				if (this.dataAccessor._SlaUrgentThreshold_ValueProvider_ != null)
				{
					return this.dataAccessor._SlaUrgentThreshold_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SlaUrgentThreshold_MaterializedValue_;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00005E1B File Offset: 0x0000401B
		public float SlaNonUrgentThreshold
		{
			get
			{
				if (this.dataAccessor._SlaNonUrgentThreshold_ValueProvider_ != null)
				{
					return this.dataAccessor._SlaNonUrgentThreshold_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SlaNonUrgentThreshold_MaterializedValue_;
			}
		}

		// Token: 0x0400023E RID: 574
		private _DynamicStorageSelection_IMailboxAssistantSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400023F RID: 575
		private VariantContextSnapshot context;
	}
}
