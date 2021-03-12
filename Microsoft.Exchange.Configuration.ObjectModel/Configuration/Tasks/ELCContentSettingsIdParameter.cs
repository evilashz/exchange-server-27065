using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	public class ELCContentSettingsIdParameter : ADIdParameter
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x00020F9E File Offset: 0x0001F19E
		public ELCContentSettingsIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00020FA7 File Offset: 0x0001F1A7
		public ELCContentSettingsIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00020FB0 File Offset: 0x0001F1B0
		public ELCContentSettingsIdParameter(ElcContentSettings elcContentSetting) : base(elcContentSetting.Id)
		{
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00020FBE File Offset: 0x0001F1BE
		public ELCContentSettingsIdParameter()
		{
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00020FC6 File Offset: 0x0001F1C6
		public ELCContentSettingsIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00020FCF File Offset: 0x0001F1CF
		public static ELCContentSettingsIdParameter Parse(string rawString)
		{
			return new ELCContentSettingsIdParameter(rawString);
		}
	}
}
