using System;
using System.Web;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.CmdletInfra;

namespace Microsoft.Exchange.Configuration
{
	// Token: 0x020000B3 RID: 179
	public class RemotePowershellClientAccessRulesAuthorizer : IClientAccessRulesAuthorizer
	{
		// Token: 0x06000730 RID: 1840 RVA: 0x0001ABE3 File Offset: 0x00018DE3
		public void SafeAppendGenericInfo(HttpContext httpContext, string key, string value)
		{
			HttpLogger.SafeAppendGenericInfo(key, value);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001ABEC File Offset: 0x00018DEC
		public void ResponseToError(HttpContext httpContext)
		{
			httpContext.Response.StatusCode = 400;
			httpContext.Response.Write(string.Format("[FailureCategory={0}] ", FailureCategory.ClientAccessRules) + Environment.NewLine + Strings.ErrorRemotePowershellConnectionBlocked + Environment.NewLine);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001AC40 File Offset: 0x00018E40
		public OrganizationId GetUserOrganization()
		{
			UserToken userToken = HttpContext.Current.CurrentUserToken();
			if (userToken != null)
			{
				return userToken.Organization;
			}
			return null;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001AC63 File Offset: 0x00018E63
		public ClientAccessProtocol Protocol
		{
			get
			{
				return ClientAccessProtocol.RemotePowerShell;
			}
		}
	}
}
