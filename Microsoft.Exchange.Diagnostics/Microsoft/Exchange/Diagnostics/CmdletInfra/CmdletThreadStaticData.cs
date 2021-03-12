using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x020000FB RID: 251
	internal static class CmdletThreadStaticData
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001C760 File Offset: 0x0001A960
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001C76D File Offset: 0x0001A96D
		internal static int? CacheHitCount
		{
			get
			{
				return CmdletThreadStaticData.GetValue<int>("CacheHitCount", 0);
			}
			set
			{
				CmdletThreadStaticData.SetValue<int?>("CacheHitCount", value);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001C77A File Offset: 0x0001A97A
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001C787 File Offset: 0x0001A987
		internal static int? CacheMissCount
		{
			get
			{
				return CmdletThreadStaticData.GetValue<int>("CacheMissCount", 0);
			}
			set
			{
				CmdletThreadStaticData.SetValue<int?>("CacheMissCount", value);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001C794 File Offset: 0x0001A994
		private static bool OnlyOneActiveCmdletInCurrentThread
		{
			get
			{
				return CmdletThreadStaticData.registeredCmdletUniqueIds != null && CmdletThreadStaticData.registeredCmdletUniqueIds.Count == 1;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		internal static bool TryGetCurrentCmdletUniqueId(out Guid cmdletUniqueId)
		{
			cmdletUniqueId = Guid.Empty;
			if (!CmdletThreadStaticData.OnlyOneActiveCmdletInCurrentThread)
			{
				return false;
			}
			cmdletUniqueId = CmdletThreadStaticData.registeredCmdletUniqueIds[0];
			return true;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
		internal static void RegisterCmdletUniqueId(Guid cmdletUniqueId)
		{
			if (CmdletThreadStaticData.registeredCmdletUniqueIds == null)
			{
				CmdletThreadStaticData.registeredCmdletUniqueIds = new List<Guid>();
			}
			CmdletThreadStaticData.registeredCmdletUniqueIds.Add(cmdletUniqueId);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001C7F2 File Offset: 0x0001A9F2
		internal static void UnRegisterCmdletUniqueId(Guid cmdletUniqueId)
		{
			if (CmdletThreadStaticData.registeredCmdletUniqueIds == null)
			{
				return;
			}
			CmdletThreadStaticData.registeredCmdletUniqueIds.Remove(cmdletUniqueId);
			if (CmdletThreadStaticData.currentThreadValueDic != null)
			{
				CmdletThreadStaticData.currentThreadValueDic.Clear();
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001C81C File Offset: 0x0001AA1C
		private static T? GetValue<T>(string key, T defaultValue) where T : struct
		{
			if (!CmdletThreadStaticData.OnlyOneActiveCmdletInCurrentThread)
			{
				return null;
			}
			object obj;
			if (CmdletThreadStaticData.currentThreadValueDic == null || !CmdletThreadStaticData.currentThreadValueDic.TryGetValue(key, out obj))
			{
				return new T?(defaultValue);
			}
			return new T?((T)((object)obj));
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001C862 File Offset: 0x0001AA62
		private static void SetValue<T>(string key, T value)
		{
			if (!CmdletThreadStaticData.OnlyOneActiveCmdletInCurrentThread)
			{
				return;
			}
			if (CmdletThreadStaticData.currentThreadValueDic == null)
			{
				CmdletThreadStaticData.currentThreadValueDic = new Dictionary<string, object>();
			}
			CmdletThreadStaticData.currentThreadValueDic[key] = value;
		}

		// Token: 0x0400048C RID: 1164
		[ThreadStatic]
		private static List<Guid> registeredCmdletUniqueIds;

		// Token: 0x0400048D RID: 1165
		[ThreadStatic]
		private static Dictionary<string, object> currentThreadValueDic;
	}
}
