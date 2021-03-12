using System;

namespace Microsoft.Exchange.Diagnostics.Components.PeopleIKnow.Agent
{
	// Token: 0x02000369 RID: 873
	public static class ExTraceGlobals
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00053C01 File Offset: 0x00051E01
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x04001933 RID: 6451
		private static Guid componentGuid = new Guid("fd252cbc-f22c-4a4f-8eb5-30ce53b9915d");

		// Token: 0x04001934 RID: 6452
		private static Trace generalTracer = null;
	}
}
