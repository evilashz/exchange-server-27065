using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x0200001D RID: 29
	public static class DbStrings
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000043B4 File Offset: 0x000025B4
		static DbStrings()
		{
			DbStrings.stringIDs.Add(3885054659U, "DatabaseInstanceName");
			DbStrings.stringIDs.Add(777731237U, "InvalidTraceObject");
			DbStrings.stringIDs.Add(1694836300U, "DetachRefCountFailed");
			DbStrings.stringIDs.Add(2935401978U, "AttachRefCountFailed");
			DbStrings.stringIDs.Add(1207509604U, "PaRecordNotLoaded");
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004453 File Offset: 0x00002653
		public static LocalizedString DatabaseInstanceName
		{
			get
			{
				return new LocalizedString("DatabaseInstanceName", "Ex8E136F", false, true, DbStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004471 File Offset: 0x00002671
		public static LocalizedString InvalidTraceObject
		{
			get
			{
				return new LocalizedString("InvalidTraceObject", "Ex102DA4", false, true, DbStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000448F File Offset: 0x0000268F
		public static LocalizedString DetachRefCountFailed
		{
			get
			{
				return new LocalizedString("DetachRefCountFailed", "Ex00CB9A", false, true, DbStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000044AD File Offset: 0x000026AD
		public static LocalizedString AttachRefCountFailed
		{
			get
			{
				return new LocalizedString("AttachRefCountFailed", "ExF5B4CD", false, true, DbStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000044CC File Offset: 0x000026CC
		public static LocalizedString DatabaseAttachFailed(string databaseName)
		{
			return new LocalizedString("DatabaseAttachFailed", "", false, false, DbStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000044FB File Offset: 0x000026FB
		public static LocalizedString PaRecordNotLoaded
		{
			get
			{
				return new LocalizedString("PaRecordNotLoaded", "Ex813A29", false, true, DbStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000451C File Offset: 0x0000271C
		public static LocalizedString DataBaseInUse(string databaseName)
		{
			return new LocalizedString("DataBaseInUse", "", false, false, DbStrings.ResourceManager, new object[]
			{
				databaseName
			});
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000454B File Offset: 0x0000274B
		public static LocalizedString GetLocalizedString(DbStrings.IDs key)
		{
			return new LocalizedString(DbStrings.stringIDs[(uint)key], DbStrings.ResourceManager, new object[0]);
		}

		// Token: 0x0400005E RID: 94
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(5);

		// Token: 0x0400005F RID: 95
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess.DbStrings", typeof(DbStrings).GetTypeInfo().Assembly);

		// Token: 0x0200001E RID: 30
		public enum IDs : uint
		{
			// Token: 0x04000061 RID: 97
			DatabaseInstanceName = 3885054659U,
			// Token: 0x04000062 RID: 98
			InvalidTraceObject = 777731237U,
			// Token: 0x04000063 RID: 99
			DetachRefCountFailed = 1694836300U,
			// Token: 0x04000064 RID: 100
			AttachRefCountFailed = 2935401978U,
			// Token: 0x04000065 RID: 101
			PaRecordNotLoaded = 1207509604U
		}

		// Token: 0x0200001F RID: 31
		private enum ParamIDs
		{
			// Token: 0x04000067 RID: 103
			DatabaseAttachFailed,
			// Token: 0x04000068 RID: 104
			DataBaseInUse
		}
	}
}
