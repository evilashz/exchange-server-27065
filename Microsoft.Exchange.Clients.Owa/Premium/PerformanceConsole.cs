using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000448 RID: 1096
	public class PerformanceConsole : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x000E0B24 File Offset: 0x000DED24
		public static int PerformanceConsoleWidth
		{
			get
			{
				return 900;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600278A RID: 10122 RVA: 0x000E0B2B File Offset: 0x000DED2B
		public static int PerformanceConsoleHeight
		{
			get
			{
				return 641;
			}
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000E0B32 File Offset: 0x000DED32
		public static bool IsPerformanceConsoleEnabled(ISessionContext sessionContext)
		{
			if (sessionContext == null)
			{
				throw new ArgumentNullException("sessionContext");
			}
			return Globals.CollectPerRequestPerformanceStats && !sessionContext.IsExplicitLogon && !sessionContext.IsWebPartRequest;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000E0B5C File Offset: 0x000DED5C
		public PerformanceConsole() : base(false)
		{
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x000E0B70 File Offset: 0x000DED70
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000E0B78 File Offset: 0x000DED78
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000E0B81 File Offset: 0x000DED81
		protected void ReadHtml(TextWriter writer)
		{
			OwaContext.Current.UserContext.PerformanceConsoleNotifier.ReadDataAsHtml(writer);
		}

		// Token: 0x04001BAB RID: 7083
		private Infobar infobar = new Infobar();
	}
}
