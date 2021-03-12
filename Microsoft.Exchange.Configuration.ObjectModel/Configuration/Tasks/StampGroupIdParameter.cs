using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200014A RID: 330
	[Serializable]
	public class StampGroupIdParameter : ADIdParameter
	{
		// Token: 0x06000BBE RID: 3006 RVA: 0x0002513A File Offset: 0x0002333A
		public StampGroupIdParameter(StampGroup stampGroup) : base(stampGroup.Id)
		{
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00025148 File Offset: 0x00023348
		public StampGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00025151 File Offset: 0x00023351
		public StampGroupIdParameter()
		{
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00025159 File Offset: 0x00023359
		public StampGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00025162 File Offset: 0x00023362
		protected StampGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002516B File Offset: 0x0002336B
		public static StampGroupIdParameter Parse(string identity)
		{
			return new StampGroupIdParameter(identity);
		}
	}
}
