using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x0200000A RID: 10
	internal class MdbPeopleModelDataBinderFactory
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002C6D File Offset: 0x00000E6D
		protected MdbPeopleModelDataBinderFactory()
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002C75 File Offset: 0x00000E75
		public static MdbPeopleModelDataBinderFactory Current
		{
			get
			{
				return MdbPeopleModelDataBinderFactory.hookableInstance.Value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002C81 File Offset: 0x00000E81
		internal static Hookable<MdbPeopleModelDataBinderFactory> HookableInstance
		{
			get
			{
				return MdbPeopleModelDataBinderFactory.hookableInstance;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002C88 File Offset: 0x00000E88
		public virtual MdbPeopleModelDataBinder CreateInstance(MailboxSession session)
		{
			return new MdbPeopleModelDataBinder(session);
		}

		// Token: 0x04000024 RID: 36
		private static Hookable<MdbPeopleModelDataBinderFactory> hookableInstance = Hookable<MdbPeopleModelDataBinderFactory>.Create(true, new MdbPeopleModelDataBinderFactory());
	}
}
