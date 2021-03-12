using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200009B RID: 155
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IResourceSettings_Implementation_ : IResourceSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IResourceSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000068A4 File Offset: 0x00004AA4
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060003BB RID: 955 RVA: 0x000068AC File Offset: 0x00004AAC
		_DynamicStorageSelection_IResourceSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IResourceSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000068B4 File Offset: 0x00004AB4
		void IDataAccessorBackedObject<_DynamicStorageSelection_IResourceSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IResourceSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000068C4 File Offset: 0x00004AC4
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000068D1 File Offset: 0x00004AD1
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

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00006902 File Offset: 0x00004B02
		public int MaxConcurrency
		{
			get
			{
				if (this.dataAccessor._MaxConcurrency_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxConcurrency_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxConcurrency_MaterializedValue_;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00006933 File Offset: 0x00004B33
		public int DiscretionaryUnderloaded
		{
			get
			{
				if (this.dataAccessor._DiscretionaryUnderloaded_ValueProvider_ != null)
				{
					return this.dataAccessor._DiscretionaryUnderloaded_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DiscretionaryUnderloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00006964 File Offset: 0x00004B64
		public int DiscretionaryOverloaded
		{
			get
			{
				if (this.dataAccessor._DiscretionaryOverloaded_ValueProvider_ != null)
				{
					return this.dataAccessor._DiscretionaryOverloaded_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DiscretionaryOverloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00006995 File Offset: 0x00004B95
		public int DiscretionaryCritical
		{
			get
			{
				if (this.dataAccessor._DiscretionaryCritical_ValueProvider_ != null)
				{
					return this.dataAccessor._DiscretionaryCritical_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DiscretionaryCritical_MaterializedValue_;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x000069C6 File Offset: 0x00004BC6
		public int InternalMaintenanceUnderloaded
		{
			get
			{
				if (this.dataAccessor._InternalMaintenanceUnderloaded_ValueProvider_ != null)
				{
					return this.dataAccessor._InternalMaintenanceUnderloaded_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InternalMaintenanceUnderloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x000069F7 File Offset: 0x00004BF7
		public int InternalMaintenanceOverloaded
		{
			get
			{
				if (this.dataAccessor._InternalMaintenanceOverloaded_ValueProvider_ != null)
				{
					return this.dataAccessor._InternalMaintenanceOverloaded_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InternalMaintenanceOverloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00006A28 File Offset: 0x00004C28
		public int InternalMaintenanceCritical
		{
			get
			{
				if (this.dataAccessor._InternalMaintenanceCritical_ValueProvider_ != null)
				{
					return this.dataAccessor._InternalMaintenanceCritical_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InternalMaintenanceCritical_MaterializedValue_;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00006A59 File Offset: 0x00004C59
		public int CustomerExpectationUnderloaded
		{
			get
			{
				if (this.dataAccessor._CustomerExpectationUnderloaded_ValueProvider_ != null)
				{
					return this.dataAccessor._CustomerExpectationUnderloaded_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._CustomerExpectationUnderloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00006A8A File Offset: 0x00004C8A
		public int CustomerExpectationOverloaded
		{
			get
			{
				if (this.dataAccessor._CustomerExpectationOverloaded_ValueProvider_ != null)
				{
					return this.dataAccessor._CustomerExpectationOverloaded_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._CustomerExpectationOverloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00006ABB File Offset: 0x00004CBB
		public int CustomerExpectationCritical
		{
			get
			{
				if (this.dataAccessor._CustomerExpectationCritical_ValueProvider_ != null)
				{
					return this.dataAccessor._CustomerExpectationCritical_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._CustomerExpectationCritical_MaterializedValue_;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00006AEC File Offset: 0x00004CEC
		public int UrgentUnderloaded
		{
			get
			{
				if (this.dataAccessor._UrgentUnderloaded_ValueProvider_ != null)
				{
					return this.dataAccessor._UrgentUnderloaded_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._UrgentUnderloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00006B1D File Offset: 0x00004D1D
		public int UrgentOverloaded
		{
			get
			{
				if (this.dataAccessor._UrgentOverloaded_ValueProvider_ != null)
				{
					return this.dataAccessor._UrgentOverloaded_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._UrgentOverloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00006B4E File Offset: 0x00004D4E
		public int UrgentCritical
		{
			get
			{
				if (this.dataAccessor._UrgentCritical_ValueProvider_ != null)
				{
					return this.dataAccessor._UrgentCritical_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._UrgentCritical_MaterializedValue_;
			}
		}

		// Token: 0x040002E5 RID: 741
		private _DynamicStorageSelection_IResourceSettings_DataAccessor_ dataAccessor;

		// Token: 0x040002E6 RID: 742
		private VariantContextSnapshot context;
	}
}
