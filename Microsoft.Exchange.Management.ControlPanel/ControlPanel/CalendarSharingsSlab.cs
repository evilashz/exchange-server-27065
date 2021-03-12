using System;
using System.Web;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200007C RID: 124
	public class CalendarSharingsSlab : SlabControl
	{
		// Token: 0x06001B46 RID: 6982 RVA: 0x00056C06 File Offset: 0x00054E06
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.Title = CalendarSharingsSlab.GetDisplayName(this.Context.Request, "id");
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x00056C2C File Offset: 0x00054E2C
		internal static string GetDisplayName(HttpRequest request, string idParameter)
		{
			string text = request.QueryString[idParameter];
			string result = string.Empty;
			bool flag = false;
			if (!text.IsNullOrBlank())
			{
				Identity identity = Identity.ParseIdentity(text);
				if (identity != null)
				{
					if (string.Compare(identity.DisplayName, identity.RawIdentity) == 0)
					{
						identity = identity.ResolveByType(IdentityType.MailboxFolder);
					}
					if (identity != null)
					{
						result = identity.DisplayName;
					}
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				throw new BadQueryParameterException(idParameter);
			}
			return result;
		}
	}
}
