using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000829 RID: 2089
	internal interface IInternalMessage
	{
		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06005958 RID: 22872
		// (set) Token: 0x06005959 RID: 22873
		ServerIdentity ServerIdentityObject { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x0600595A RID: 22874
		// (set) Token: 0x0600595B RID: 22875
		Identity IdentityObject { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x0600595C RID: 22876
		[SecurityCritical]
		void SetURI(string uri);

		// Token: 0x0600595D RID: 22877
		[SecurityCritical]
		void SetCallContext(LogicalCallContext callContext);

		// Token: 0x0600595E RID: 22878
		[SecurityCritical]
		bool HasProperties();
	}
}
