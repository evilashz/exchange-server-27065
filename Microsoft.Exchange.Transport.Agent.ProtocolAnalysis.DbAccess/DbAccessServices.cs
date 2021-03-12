using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000002 RID: 2
	internal class DbAccessServices
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static DataTable GetTableByType(Type t)
		{
			Type typeFromHandle = typeof(Database);
			return (DataTable)typeFromHandle.InvokeMember(t.Name, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, null, null, null, CultureInfo.InvariantCulture);
		}
	}
}
