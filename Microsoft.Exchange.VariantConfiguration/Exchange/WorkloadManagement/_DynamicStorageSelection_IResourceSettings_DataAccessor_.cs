using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200009A RID: 154
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IResourceSettings_DataAccessor_ : VariantObjectDataAccessorBase<IResourceSettings, _DynamicStorageSelection_IResourceSettings_Implementation_, _DynamicStorageSelection_IResourceSettings_DataAccessor_>
	{
		// Token: 0x040002C8 RID: 712
		internal string _Name_MaterializedValue_;

		// Token: 0x040002C9 RID: 713
		internal bool _Enabled_MaterializedValue_;

		// Token: 0x040002CA RID: 714
		internal ValueProvider<bool> _Enabled_ValueProvider_;

		// Token: 0x040002CB RID: 715
		internal int _MaxConcurrency_MaterializedValue_;

		// Token: 0x040002CC RID: 716
		internal ValueProvider<int> _MaxConcurrency_ValueProvider_;

		// Token: 0x040002CD RID: 717
		internal int _DiscretionaryUnderloaded_MaterializedValue_;

		// Token: 0x040002CE RID: 718
		internal ValueProvider<int> _DiscretionaryUnderloaded_ValueProvider_;

		// Token: 0x040002CF RID: 719
		internal int _DiscretionaryOverloaded_MaterializedValue_;

		// Token: 0x040002D0 RID: 720
		internal ValueProvider<int> _DiscretionaryOverloaded_ValueProvider_;

		// Token: 0x040002D1 RID: 721
		internal int _DiscretionaryCritical_MaterializedValue_;

		// Token: 0x040002D2 RID: 722
		internal ValueProvider<int> _DiscretionaryCritical_ValueProvider_;

		// Token: 0x040002D3 RID: 723
		internal int _InternalMaintenanceUnderloaded_MaterializedValue_;

		// Token: 0x040002D4 RID: 724
		internal ValueProvider<int> _InternalMaintenanceUnderloaded_ValueProvider_;

		// Token: 0x040002D5 RID: 725
		internal int _InternalMaintenanceOverloaded_MaterializedValue_;

		// Token: 0x040002D6 RID: 726
		internal ValueProvider<int> _InternalMaintenanceOverloaded_ValueProvider_;

		// Token: 0x040002D7 RID: 727
		internal int _InternalMaintenanceCritical_MaterializedValue_;

		// Token: 0x040002D8 RID: 728
		internal ValueProvider<int> _InternalMaintenanceCritical_ValueProvider_;

		// Token: 0x040002D9 RID: 729
		internal int _CustomerExpectationUnderloaded_MaterializedValue_;

		// Token: 0x040002DA RID: 730
		internal ValueProvider<int> _CustomerExpectationUnderloaded_ValueProvider_;

		// Token: 0x040002DB RID: 731
		internal int _CustomerExpectationOverloaded_MaterializedValue_;

		// Token: 0x040002DC RID: 732
		internal ValueProvider<int> _CustomerExpectationOverloaded_ValueProvider_;

		// Token: 0x040002DD RID: 733
		internal int _CustomerExpectationCritical_MaterializedValue_;

		// Token: 0x040002DE RID: 734
		internal ValueProvider<int> _CustomerExpectationCritical_ValueProvider_;

		// Token: 0x040002DF RID: 735
		internal int _UrgentUnderloaded_MaterializedValue_;

		// Token: 0x040002E0 RID: 736
		internal ValueProvider<int> _UrgentUnderloaded_ValueProvider_;

		// Token: 0x040002E1 RID: 737
		internal int _UrgentOverloaded_MaterializedValue_;

		// Token: 0x040002E2 RID: 738
		internal ValueProvider<int> _UrgentOverloaded_ValueProvider_;

		// Token: 0x040002E3 RID: 739
		internal int _UrgentCritical_MaterializedValue_;

		// Token: 0x040002E4 RID: 740
		internal ValueProvider<int> _UrgentCritical_ValueProvider_;
	}
}
