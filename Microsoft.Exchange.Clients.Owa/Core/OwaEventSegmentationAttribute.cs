using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000190 RID: 400
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
	public sealed class OwaEventSegmentationAttribute : Attribute
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0005D5AB File Offset: 0x0005B7AB
		public OwaEventSegmentationAttribute(Feature features)
		{
			this.segmentationFlags = (ulong)features;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0005D5BA File Offset: 0x0005B7BA
		internal ulong SegmentationFlags
		{
			get
			{
				return this.segmentationFlags;
			}
		}

		// Token: 0x040009FD RID: 2557
		private ulong segmentationFlags;
	}
}
