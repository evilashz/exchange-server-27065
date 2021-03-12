using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000070 RID: 112
	internal static class MailboxTableQuery
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00010483 File Offset: 0x0000E683
		internal static IDisposable SetGetPropValuesFromMailboxTableTestHook(Func<string, Guid, PropTag[], PropValue[][]> delegateFunction)
		{
			return MailboxTableQuery.getPropValuesFromMailboxTableHook.SetTestHook(delegateFunction);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010490 File Offset: 0x0000E690
		internal static PropValue GetMailboxProperty(PropValue[] propValueArray, PropTag property)
		{
			foreach (PropValue result in propValueArray)
			{
				uint num = (uint)(result.PropTag & (PropTag)4294901760U);
				uint num2 = (uint)(property & (PropTag)4294901760U);
				if (num == num2)
				{
					return result;
				}
			}
			throw new ArgumentException(string.Format("Property {0} not found in the provided property set.", property));
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000104F8 File Offset: 0x0000E6F8
		internal static PropValue[][] GetMailboxes(string clientId, DatabaseInfo databaseInfo, PropTag[] properties)
		{
			ExTraceGlobals.DatabaseInfoTracer.TraceDebug<DatabaseInfo>(0L, "{0}: Get list of mailboxes from mailbox table.", databaseInfo);
			PropValue[][] array = MailboxTableQuery.getPropValuesFromMailboxTableHook.Value(clientId, databaseInfo.Guid, properties);
			ExTraceGlobals.DatabaseInfoTracer.TraceDebug<DatabaseInfo, int>(0L, "{0}: {1} mailboxes retrieved.", databaseInfo, array.Length);
			return array;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010548 File Offset: 0x0000E748
		private static PropValue[][] GetPropValuesFromMailboxTable(string clientId, Guid databaseGuid, PropTag[] properties)
		{
			PropValue[][] mailboxTable;
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create(clientId, null, null, null, null))
			{
				mailboxTable = exRpcAdmin.GetMailboxTable(databaseGuid, properties);
			}
			return mailboxTable;
		}

		// Token: 0x040001E3 RID: 483
		private static Hookable<Func<string, Guid, PropTag[], PropValue[][]>> getPropValuesFromMailboxTableHook = Hookable<Func<string, Guid, PropTag[], PropValue[][]>>.Create(true, new Func<string, Guid, PropTag[], PropValue[][]>(MailboxTableQuery.GetPropValuesFromMailboxTable));

		// Token: 0x040001E4 RID: 484
		internal static readonly PropTag[] RequiredMailboxTableProperties = new PropTag[]
		{
			PropTag.UserGuid,
			PropTag.DisplayName,
			PropTag.DateDiscoveredAbsentInDS,
			PropTag.MailboxMiscFlags,
			PropTag.MailboxType,
			PropTag.PersistableTenantPartitionHint,
			PropTag.MailboxTypeDetail,
			PropTag.LastLogonTime
		};
	}
}
