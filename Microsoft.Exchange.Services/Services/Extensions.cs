using System;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services
{
	// Token: 0x0200001E RID: 30
	public static class Extensions
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00008982 File Offset: 0x00006B82
		internal static bool Contains(this PlacesSource sourcesMask, PlacesSource sourceFlag)
		{
			return (sourcesMask & sourceFlag) != PlacesSource.None;
		}
	}
}
