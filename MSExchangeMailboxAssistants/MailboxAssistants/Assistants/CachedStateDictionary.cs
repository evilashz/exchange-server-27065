using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants
{
	// Token: 0x02000013 RID: 19
	internal sealed class CachedStateDictionary
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004E67 File Offset: 0x00003067
		public CachedStateDictionary(int sizeOfCachedState)
		{
			this.sizeOfCachedState = sizeOfCachedState;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004E8D File Offset: 0x0000308D
		public CachedState GetCachedState(Guid mbxGuid)
		{
			return this.dictionary.GetResource(mbxGuid, this.sizeOfCachedState);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004EA8 File Offset: 0x000030A8
		private static CachedState InternalCreateCachedStateObj(object obj)
		{
			int num = (int)obj;
			return new CachedState(num);
		}

		// Token: 0x040000E6 RID: 230
		private int sizeOfCachedState;

		// Token: 0x040000E7 RID: 231
		private DataCache<Guid, CachedState> dictionary = new DataCache<Guid, CachedState>(new DataCache<Guid, CachedState>.CreateResource(CachedStateDictionary.InternalCreateCachedStateObj));
	}
}
