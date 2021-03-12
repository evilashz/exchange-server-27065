using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000047 RID: 71
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IDiskLatencyMonitorSettings_Implementation_ : IDiskLatencyMonitorSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IDiskLatencyMonitorSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000046A8 File Offset: 0x000028A8
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000046B0 File Offset: 0x000028B0
		_DynamicStorageSelection_IDiskLatencyMonitorSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IDiskLatencyMonitorSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000046B8 File Offset: 0x000028B8
		void IDataAccessorBackedObject<_DynamicStorageSelection_IDiskLatencyMonitorSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IDiskLatencyMonitorSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000046C8 File Offset: 0x000028C8
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000046D5 File Offset: 0x000028D5
		public TimeSpan FixedTimeAverageWindowBucket
		{
			get
			{
				if (this.dataAccessor._FixedTimeAverageWindowBucket_ValueProvider_ != null)
				{
					return this.dataAccessor._FixedTimeAverageWindowBucket_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._FixedTimeAverageWindowBucket_MaterializedValue_;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004706 File Offset: 0x00002906
		public int FixedTimeAverageNumberOfBuckets
		{
			get
			{
				if (this.dataAccessor._FixedTimeAverageNumberOfBuckets_ValueProvider_ != null)
				{
					return this.dataAccessor._FixedTimeAverageNumberOfBuckets_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._FixedTimeAverageNumberOfBuckets_MaterializedValue_;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004737 File Offset: 0x00002937
		public TimeSpan ResourceHealthPollerInterval
		{
			get
			{
				if (this.dataAccessor._ResourceHealthPollerInterval_ValueProvider_ != null)
				{
					return this.dataAccessor._ResourceHealthPollerInterval_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ResourceHealthPollerInterval_MaterializedValue_;
			}
		}

		// Token: 0x04000125 RID: 293
		private _DynamicStorageSelection_IDiskLatencyMonitorSettings_DataAccessor_ dataAccessor;

		// Token: 0x04000126 RID: 294
		private VariantContextSnapshot context;
	}
}
