using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x020006F3 RID: 1779
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceExposureAttribute : Attribute
	{
		// Token: 0x0600503B RID: 20539 RVA: 0x0011A2F8 File Offset: 0x001184F8
		public ResourceExposureAttribute(ResourceScope exposureLevel)
		{
			this._resourceExposureLevel = exposureLevel;
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600503C RID: 20540 RVA: 0x0011A307 File Offset: 0x00118507
		public ResourceScope ResourceExposureLevel
		{
			get
			{
				return this._resourceExposureLevel;
			}
		}

		// Token: 0x04002356 RID: 9046
		private ResourceScope _resourceExposureLevel;
	}
}
