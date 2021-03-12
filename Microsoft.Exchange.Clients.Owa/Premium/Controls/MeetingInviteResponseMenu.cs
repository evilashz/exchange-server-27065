using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003AE RID: 942
	public sealed class MeetingInviteResponseMenu : ContextMenu
	{
		// Token: 0x0600236A RID: 9066 RVA: 0x000CBC23 File Offset: 0x000C9E23
		internal static MeetingInviteResponseMenu Create(UserContext userContext, ResponseType responseType)
		{
			return new MeetingInviteResponseMenu(userContext, responseType);
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x000CBC2C File Offset: 0x000C9E2C
		private MeetingInviteResponseMenu(UserContext userContext, ResponseType responseType)
		{
			string str = "divRTM";
			int num = (int)responseType;
			base..ctor(str + num.ToString(CultureInfo.InvariantCulture), userContext, false);
			this.responseType = responseType;
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x000CBC60 File Offset: 0x000C9E60
		protected override void RenderMenuItems(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			string str = "mir";
			int num = (int)this.responseType;
			string str2 = str + num.ToString(CultureInfo.InvariantCulture);
			base.RenderMenuItem(output, 1050381195, str2 + "e");
			base.RenderMenuItem(output, -114654491, str2 + "s");
			base.RenderMenuItem(output, -990767046, str2 + "d");
		}

		// Token: 0x040018B7 RID: 6327
		private ResponseType responseType;
	}
}
