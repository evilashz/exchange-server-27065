using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200009C RID: 156
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IResourceSettings_Implementation_ : IResourceSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00006B87 File Offset: 0x00004D87
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00006B8A File Offset: 0x00004D8A
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00006B8D File Offset: 0x00004D8D
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00006B95 File Offset: 0x00004D95
		public bool Enabled
		{
			get
			{
				return this._Enabled_MaterializedValue_;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00006B9D File Offset: 0x00004D9D
		public int MaxConcurrency
		{
			get
			{
				return this._MaxConcurrency_MaterializedValue_;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00006BA5 File Offset: 0x00004DA5
		public int DiscretionaryUnderloaded
		{
			get
			{
				return this._DiscretionaryUnderloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00006BAD File Offset: 0x00004DAD
		public int DiscretionaryOverloaded
		{
			get
			{
				return this._DiscretionaryOverloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00006BB5 File Offset: 0x00004DB5
		public int DiscretionaryCritical
		{
			get
			{
				return this._DiscretionaryCritical_MaterializedValue_;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00006BBD File Offset: 0x00004DBD
		public int InternalMaintenanceUnderloaded
		{
			get
			{
				return this._InternalMaintenanceUnderloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00006BC5 File Offset: 0x00004DC5
		public int InternalMaintenanceOverloaded
		{
			get
			{
				return this._InternalMaintenanceOverloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00006BCD File Offset: 0x00004DCD
		public int InternalMaintenanceCritical
		{
			get
			{
				return this._InternalMaintenanceCritical_MaterializedValue_;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00006BD5 File Offset: 0x00004DD5
		public int CustomerExpectationUnderloaded
		{
			get
			{
				return this._CustomerExpectationUnderloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00006BDD File Offset: 0x00004DDD
		public int CustomerExpectationOverloaded
		{
			get
			{
				return this._CustomerExpectationOverloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00006BE5 File Offset: 0x00004DE5
		public int CustomerExpectationCritical
		{
			get
			{
				return this._CustomerExpectationCritical_MaterializedValue_;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00006BED File Offset: 0x00004DED
		public int UrgentUnderloaded
		{
			get
			{
				return this._UrgentUnderloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00006BF5 File Offset: 0x00004DF5
		public int UrgentOverloaded
		{
			get
			{
				return this._UrgentOverloaded_MaterializedValue_;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00006BFD File Offset: 0x00004DFD
		public int UrgentCritical
		{
			get
			{
				return this._UrgentCritical_MaterializedValue_;
			}
		}

		// Token: 0x040002E7 RID: 743
		internal string _Name_MaterializedValue_;

		// Token: 0x040002E8 RID: 744
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x040002E9 RID: 745
		internal int _MaxConcurrency_MaterializedValue_;

		// Token: 0x040002EA RID: 746
		internal int _DiscretionaryUnderloaded_MaterializedValue_;

		// Token: 0x040002EB RID: 747
		internal int _DiscretionaryOverloaded_MaterializedValue_;

		// Token: 0x040002EC RID: 748
		internal int _DiscretionaryCritical_MaterializedValue_;

		// Token: 0x040002ED RID: 749
		internal int _InternalMaintenanceUnderloaded_MaterializedValue_;

		// Token: 0x040002EE RID: 750
		internal int _InternalMaintenanceOverloaded_MaterializedValue_;

		// Token: 0x040002EF RID: 751
		internal int _InternalMaintenanceCritical_MaterializedValue_;

		// Token: 0x040002F0 RID: 752
		internal int _CustomerExpectationUnderloaded_MaterializedValue_;

		// Token: 0x040002F1 RID: 753
		internal int _CustomerExpectationOverloaded_MaterializedValue_;

		// Token: 0x040002F2 RID: 754
		internal int _CustomerExpectationCritical_MaterializedValue_;

		// Token: 0x040002F3 RID: 755
		internal int _UrgentUnderloaded_MaterializedValue_;

		// Token: 0x040002F4 RID: 756
		internal int _UrgentOverloaded_MaterializedValue_;

		// Token: 0x040002F5 RID: 757
		internal int _UrgentCritical_MaterializedValue_;
	}
}
