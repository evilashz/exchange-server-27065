using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000092 RID: 146
	public class SecurityContextsQueryTarget : IStoreSimpleQueryTarget<CountedClientSecurityContext>, IStoreQueryTargetBase<CountedClientSecurityContext>
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x000263E2 File Offset: 0x000245E2
		private SecurityContextsQueryTarget()
		{
			StoreQueryTargets.Register<CountedClientSecurityContext>(this, Visibility.Public);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000263F1 File Offset: 0x000245F1
		public static SecurityContextsQueryTarget Create()
		{
			return new SecurityContextsQueryTarget();
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x000263F8 File Offset: 0x000245F8
		string IStoreQueryTargetBase<CountedClientSecurityContext>.Name
		{
			get
			{
				return "CountedSecurityContext";
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000263FF File Offset: 0x000245FF
		Type[] IStoreQueryTargetBase<CountedClientSecurityContext>.ParameterTypes
		{
			get
			{
				return Array<Type>.Empty;
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x000265B4 File Offset: 0x000247B4
		IEnumerable<CountedClientSecurityContext> IStoreSimpleQueryTarget<CountedClientSecurityContext>.GetRows(object[] parameters)
		{
			IEnumerable<SecurityContextKey> keysCopy = SecurityContextManager.GetKeysInDictionary();
			foreach (SecurityContextKey key in keysCopy)
			{
				CountedClientSecurityContext info = SecurityContextManager.GetValueForKey(key);
				if (info != null)
				{
					yield return info;
				}
			}
			yield break;
		}
	}
}
