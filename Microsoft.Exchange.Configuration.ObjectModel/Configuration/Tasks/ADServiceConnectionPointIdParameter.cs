using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E7 RID: 231
	[Serializable]
	public class ADServiceConnectionPointIdParameter : ADIdParameter
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x0001E51E File Offset: 0x0001C71E
		public ADServiceConnectionPointIdParameter()
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001E526 File Offset: 0x0001C726
		public ADServiceConnectionPointIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001E52F File Offset: 0x0001C72F
		public ADServiceConnectionPointIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001E538 File Offset: 0x0001C738
		public ADServiceConnectionPointIdParameter(ADServiceConnectionPoint connectionPoint) : base(connectionPoint.Id)
		{
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001E546 File Offset: 0x0001C746
		public ADServiceConnectionPointIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001E54F File Offset: 0x0001C74F
		public static ADServiceConnectionPointIdParameter Parse(string identity)
		{
			return new ADServiceConnectionPointIdParameter(identity);
		}
	}
}
