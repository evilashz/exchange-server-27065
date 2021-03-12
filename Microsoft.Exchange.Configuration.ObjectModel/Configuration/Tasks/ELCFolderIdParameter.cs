using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200010B RID: 267
	[Serializable]
	public class ELCFolderIdParameter : ADIdParameter
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x00020FD7 File Offset: 0x0001F1D7
		public ELCFolderIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00020FE0 File Offset: 0x0001F1E0
		public ELCFolderIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00020FE9 File Offset: 0x0001F1E9
		public ELCFolderIdParameter(ELCFolder elcFolder) : base(elcFolder.Id)
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00020FF7 File Offset: 0x0001F1F7
		public ELCFolderIdParameter()
		{
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00020FFF File Offset: 0x0001F1FF
		public ELCFolderIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00021008 File Offset: 0x0001F208
		public static ELCFolderIdParameter Parse(string rawString)
		{
			return new ELCFolderIdParameter(rawString);
		}
	}
}
