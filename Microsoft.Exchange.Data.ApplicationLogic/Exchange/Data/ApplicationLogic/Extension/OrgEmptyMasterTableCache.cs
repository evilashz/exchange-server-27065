using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200010A RID: 266
	internal class OrgEmptyMasterTableCache
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0002D7BE File Offset: 0x0002B9BE
		internal static OrgEmptyMasterTableCache Singleton
		{
			get
			{
				return OrgEmptyMasterTableCache.singleton;
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002D7C8 File Offset: 0x0002B9C8
		internal bool IsEmpty(OrganizationId organizationId)
		{
			bool flag2;
			bool flag = this.emptyMasterTableDictionary.TryGetValue(organizationId.GetHashCode(), out flag2);
			OrgEmptyMasterTableCache.Tracer.TraceDebug<OrganizationId, bool>(0L, "OrgEmptyMasterTableCache.IsEmpty: Org: {0} IsEmpty: {1}", organizationId, flag);
			return flag;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002D7FD File Offset: 0x0002B9FD
		internal void Update(OrganizationId organizationId, bool isEmpty)
		{
			OrgEmptyMasterTableCache.Tracer.TraceDebug<OrganizationId, bool>(0L, "OrgEmptyMasterTableCache.Update: Org: {0} IsEmpty: {1}", organizationId, isEmpty);
			if (isEmpty)
			{
				this.emptyMasterTableDictionary.Add(organizationId.GetHashCode(), isEmpty);
				return;
			}
			this.emptyMasterTableDictionary.Remove(organizationId.GetHashCode());
		}

		// Token: 0x040005AC RID: 1452
		private const int MaxEntryCount = 20000;

		// Token: 0x040005AD RID: 1453
		private static OrgEmptyMasterTableCache singleton = new OrgEmptyMasterTableCache();

		// Token: 0x040005AE RID: 1454
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x040005AF RID: 1455
		private MruDictionary<int, bool> emptyMasterTableDictionary = new MruDictionary<int, bool>(20000, new OrgEmptyMasterTableCache.IntComparer(), null);

		// Token: 0x0200010B RID: 267
		private sealed class IntComparer : IComparer<int>
		{
			// Token: 0x06000B4C RID: 2892 RVA: 0x0002D86E File Offset: 0x0002BA6E
			int IComparer<int>.Compare(int x, int y)
			{
				return x.CompareTo(y);
			}
		}
	}
}
