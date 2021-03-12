using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000243 RID: 579
	internal abstract class InstantMessageCommandBase<T> : ServiceCommand<T>
	{
		// Token: 0x060015C4 RID: 5572 RVA: 0x0004E216 File Offset: 0x0004C416
		public InstantMessageCommandBase(CallContext callContext) : base(callContext)
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(InstantMessagingLogMetadata), new Type[0]);
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0004E244 File Offset: 0x0004C444
		protected InstantMessageProvider Provider
		{
			get
			{
				InstantMessageManager instantMessageManager = this.InstantMessageManager;
				if (instantMessageManager != null)
				{
					return instantMessageManager.Provider;
				}
				return null;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0004E264 File Offset: 0x0004C464
		protected InstantMessageManager InstantMessageManager
		{
			get
			{
				UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
				if (userContext.IsInstantMessageEnabled)
				{
					return userContext.InstantMessageManager;
				}
				return null;
			}
		}
	}
}
