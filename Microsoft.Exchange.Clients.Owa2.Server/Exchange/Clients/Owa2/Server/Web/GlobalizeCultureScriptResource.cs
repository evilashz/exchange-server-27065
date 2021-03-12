using System;
using System.Threading;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200047D RID: 1149
	internal class GlobalizeCultureScriptResource : GlobalizeScriptResource
	{
		// Token: 0x060026D9 RID: 9945 RVA: 0x0008CBD6 File Offset: 0x0008ADD6
		public GlobalizeCultureScriptResource(ResourceTarget.Filter targetFilter, string currentOwaVersion) : base(null, targetFilter, currentOwaVersion, true)
		{
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x0008CBE2 File Offset: 0x0008ADE2
		internal override string ResourceName
		{
			get
			{
				return string.Format("globalize.culture.{0}.js", Thread.CurrentThread.CurrentUICulture.Name);
			}
		}

		// Token: 0x040016B2 RID: 5810
		private const string GlobalizationScriptFile = "globalize.culture.{0}.js";
	}
}
