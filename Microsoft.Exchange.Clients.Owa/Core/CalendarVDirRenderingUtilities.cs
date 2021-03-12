using System;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200029E RID: 670
	public static class CalendarVDirRenderingUtilities
	{
		// Token: 0x060019E0 RID: 6624 RVA: 0x00095FEC File Offset: 0x000941EC
		public static void RenderInlineScripts(TextWriter writer)
		{
			Utilities.RenderScriptTagStart(writer);
			Utilities.RenderBootUpScripts(writer);
			Utilities.RenderCDNEndpointVariable(writer);
			Utilities.RenderScriptTagEnd(writer);
		}
	}
}
