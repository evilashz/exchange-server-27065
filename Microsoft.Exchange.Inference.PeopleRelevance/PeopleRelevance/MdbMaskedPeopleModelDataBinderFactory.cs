using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000027 RID: 39
	internal class MdbMaskedPeopleModelDataBinderFactory
	{
		// Token: 0x0600014F RID: 335 RVA: 0x0000703C File Offset: 0x0000523C
		protected MdbMaskedPeopleModelDataBinderFactory()
		{
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007044 File Offset: 0x00005244
		public static MdbMaskedPeopleModelDataBinderFactory Current
		{
			get
			{
				return MdbMaskedPeopleModelDataBinderFactory.hookableInstance.Value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00007050 File Offset: 0x00005250
		internal static Hookable<MdbMaskedPeopleModelDataBinderFactory> HookableInstance
		{
			get
			{
				return MdbMaskedPeopleModelDataBinderFactory.hookableInstance;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007057 File Offset: 0x00005257
		public virtual MdbMaskedPeopleModelDataBinder CreateInstance(MailboxSession session)
		{
			return new MdbMaskedPeopleModelDataBinder(session);
		}

		// Token: 0x0400009E RID: 158
		private static Hookable<MdbMaskedPeopleModelDataBinderFactory> hookableInstance = Hookable<MdbMaskedPeopleModelDataBinderFactory>.Create(true, new MdbMaskedPeopleModelDataBinderFactory());
	}
}
