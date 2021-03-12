using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x0200078E RID: 1934
	[Serializable]
	internal sealed class EnvoyInfo : IEnvoyInfo
	{
		// Token: 0x0600547F RID: 21631 RVA: 0x0012B58C File Offset: 0x0012978C
		[SecurityCritical]
		internal static IEnvoyInfo CreateEnvoyInfo(ServerIdentity serverID)
		{
			IEnvoyInfo result = null;
			if (serverID != null)
			{
				if (serverID.EnvoyChain == null)
				{
					serverID.RaceSetEnvoyChain(serverID.ServerContext.CreateEnvoyChain(serverID.TPOrObject));
				}
				if (!(serverID.EnvoyChain is EnvoyTerminatorSink))
				{
					result = new EnvoyInfo(serverID.EnvoyChain);
				}
			}
			return result;
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x0012B5DA File Offset: 0x001297DA
		[SecurityCritical]
		private EnvoyInfo(IMessageSink sinks)
		{
			this.EnvoySinks = sinks;
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06005481 RID: 21633 RVA: 0x0012B5E9 File Offset: 0x001297E9
		// (set) Token: 0x06005482 RID: 21634 RVA: 0x0012B5F1 File Offset: 0x001297F1
		public IMessageSink EnvoySinks
		{
			[SecurityCritical]
			get
			{
				return this.envoySinks;
			}
			[SecurityCritical]
			set
			{
				this.envoySinks = value;
			}
		}

		// Token: 0x040026AE RID: 9902
		private IMessageSink envoySinks;
	}
}
