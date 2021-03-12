using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200034B RID: 843
	public sealed class DefaultFlagMenu : ContextMenu
	{
		// Token: 0x06001FCC RID: 8140 RVA: 0x000B8006 File Offset: 0x000B6206
		public static DefaultFlagMenu Create(UserContext userContext)
		{
			return new DefaultFlagMenu(userContext);
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000B800E File Offset: 0x000B620E
		private DefaultFlagMenu(UserContext userContext) : base("divDftFM", userContext, true)
		{
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000B8020 File Offset: 0x000B6220
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			base.RenderMenuItem(output, -367521373, (this.userContext.UserOptions.FlagAction == FlagAction.Today) ? ThemeFileId.Flag : ThemeFileId.None, "divDFA" + DefaultFlagMenu.today, "d" + DefaultFlagMenu.today);
			base.RenderMenuItem(output, 1854511297, (this.userContext.UserOptions.FlagAction == FlagAction.Tomorrow) ? ThemeFileId.Flag : ThemeFileId.None, "divDFA" + DefaultFlagMenu.tomorrow, "d" + DefaultFlagMenu.tomorrow);
			base.RenderMenuItem(output, -1821673574, (this.userContext.UserOptions.FlagAction == FlagAction.ThisWeek) ? ThemeFileId.Flag : ThemeFileId.None, "divDFA" + DefaultFlagMenu.thisWeek, "d" + DefaultFlagMenu.thisWeek);
			base.RenderMenuItem(output, 1433854051, (this.userContext.UserOptions.FlagAction == FlagAction.NextWeek) ? ThemeFileId.Flag : ThemeFileId.None, "divDFA" + DefaultFlagMenu.nextWeek, "d" + DefaultFlagMenu.nextWeek);
			base.RenderMenuItem(output, 689037121, (this.userContext.UserOptions.FlagAction == FlagAction.NoDate) ? ThemeFileId.Flag : ThemeFileId.None, "divDFA" + DefaultFlagMenu.noDate, "d" + DefaultFlagMenu.noDate);
		}

		// Token: 0x04001720 RID: 5920
		private static readonly string today = 2.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001721 RID: 5921
		private static readonly string tomorrow = 3.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001722 RID: 5922
		private static readonly string thisWeek = 4.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001723 RID: 5923
		private static readonly string nextWeek = 5.ToString(CultureInfo.InvariantCulture);

		// Token: 0x04001724 RID: 5924
		private static readonly string noDate = 6.ToString(CultureInfo.InvariantCulture);
	}
}
