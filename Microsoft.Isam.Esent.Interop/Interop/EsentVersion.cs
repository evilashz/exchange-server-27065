using System;
using Microsoft.Isam.Esent.Interop.Implementation;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000225 RID: 549
	public static class EsentVersion
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00015105 File Offset: 0x00013305
		public static bool SupportsServer2003Features
		{
			get
			{
				return EsentVersion.Capabilities.SupportsServer2003Features;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00015111 File Offset: 0x00013311
		public static bool SupportsVistaFeatures
		{
			get
			{
				return EsentVersion.Capabilities.SupportsVistaFeatures;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0001511D File Offset: 0x0001331D
		public static bool SupportsWindows7Features
		{
			get
			{
				return EsentVersion.Capabilities.SupportsWindows7Features;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00015129 File Offset: 0x00013329
		public static bool SupportsWindows8Features
		{
			get
			{
				return EsentVersion.Capabilities.SupportsWindows8Features;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00015135 File Offset: 0x00013335
		public static bool SupportsWindows81Features
		{
			get
			{
				return EsentVersion.Capabilities.SupportsWindows81Features;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00015141 File Offset: 0x00013341
		public static bool SupportsUnicodePaths
		{
			get
			{
				return EsentVersion.Capabilities.SupportsUnicodePaths;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0001514D File Offset: 0x0001334D
		public static bool SupportsLargeKeys
		{
			get
			{
				return EsentVersion.Capabilities.SupportsLargeKeys;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00015159 File Offset: 0x00013359
		private static JetCapabilities Capabilities
		{
			get
			{
				return Api.Impl.Capabilities;
			}
		}
	}
}
