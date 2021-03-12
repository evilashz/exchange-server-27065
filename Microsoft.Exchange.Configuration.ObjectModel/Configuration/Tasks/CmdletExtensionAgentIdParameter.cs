using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000199 RID: 409
	[Serializable]
	public class CmdletExtensionAgentIdParameter : ADIdParameter
	{
		// Token: 0x06000EC3 RID: 3779 RVA: 0x0002B327 File Offset: 0x00029527
		public CmdletExtensionAgentIdParameter()
		{
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002B32F File Offset: 0x0002952F
		public CmdletExtensionAgentIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0002B338 File Offset: 0x00029538
		public CmdletExtensionAgentIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0002B341 File Offset: 0x00029541
		public CmdletExtensionAgentIdParameter(CmdletExtensionAgent agent) : base(agent.Id)
		{
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0002B34F File Offset: 0x0002954F
		public CmdletExtensionAgentIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0002B358 File Offset: 0x00029558
		public static CmdletExtensionAgentIdParameter Parse(string identity)
		{
			return new CmdletExtensionAgentIdParameter(identity);
		}
	}
}
