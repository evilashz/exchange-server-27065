using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.ServiceHost;

namespace Microsoft.Exchange.Servicelets.AuthAdmin
{
	// Token: 0x02000002 RID: 2
	public class AuthAdmin : Servicelet
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public override void Work()
		{
			using (AuthAdminContext authAdminContext = new AuthAdminContext("AuthAdmin"))
			{
				AnchorApplication anchorApplication = new AnchorApplication(authAdminContext, base.StopEvent);
				anchorApplication.Process();
			}
		}

		// Token: 0x04000001 RID: 1
		internal const string ApplicationName = "AuthAdmin";
	}
}
