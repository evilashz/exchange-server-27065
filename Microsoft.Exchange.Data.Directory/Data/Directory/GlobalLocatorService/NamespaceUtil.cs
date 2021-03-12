using System;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x0200012C RID: 300
	internal static class NamespaceUtil
	{
		// Token: 0x06000C8D RID: 3213 RVA: 0x0003871B File Offset: 0x0003691B
		internal static string NamespaceWildcard(Namespace ns)
		{
			return NamespaceUtil.NamespaceToString(ns) + ".*";
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00038730 File Offset: 0x00036930
		internal static string NamespaceToString(Namespace ns)
		{
			switch (ns)
			{
			case Namespace.Invalid:
				throw new ArgumentException("Namespace is Invalid", "ns");
			case Namespace.TestOnly:
				return "ATT";
			case Namespace.Exo:
				return GlsProperty.ExoPrefix;
			case Namespace.Ffo:
				return GlsProperty.FfoPrefix;
			}
			throw new ArgumentException("unknown Namespace value", "ns");
		}
	}
}
