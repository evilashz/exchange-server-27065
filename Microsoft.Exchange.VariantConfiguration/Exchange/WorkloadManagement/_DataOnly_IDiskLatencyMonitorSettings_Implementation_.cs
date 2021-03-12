using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000048 RID: 72
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DataOnly_IDiskLatencyMonitorSettings_Implementation_ : IDiskLatencyMonitorSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004770 File Offset: 0x00002970
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00004773 File Offset: 0x00002973
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00004776 File Offset: 0x00002976
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000477E File Offset: 0x0000297E
		public TimeSpan FixedTimeAverageWindowBucket
		{
			get
			{
				return this._FixedTimeAverageWindowBucket_MaterializedValue_;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00004786 File Offset: 0x00002986
		public int FixedTimeAverageNumberOfBuckets
		{
			get
			{
				return this._FixedTimeAverageNumberOfBuckets_MaterializedValue_;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000478E File Offset: 0x0000298E
		public TimeSpan ResourceHealthPollerInterval
		{
			get
			{
				return this._ResourceHealthPollerInterval_MaterializedValue_;
			}
		}

		// Token: 0x04000127 RID: 295
		internal string _Name_MaterializedValue_;

		// Token: 0x04000128 RID: 296
		internal TimeSpan _FixedTimeAverageWindowBucket_MaterializedValue_ = default(TimeSpan);

		// Token: 0x04000129 RID: 297
		internal int _FixedTimeAverageNumberOfBuckets_MaterializedValue_;

		// Token: 0x0400012A RID: 298
		internal TimeSpan _ResourceHealthPollerInterval_MaterializedValue_ = default(TimeSpan);
	}
}
