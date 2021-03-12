using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x0200001E RID: 30
	internal static class CommonStrings
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003E04 File Offset: 0x00002004
		static CommonStrings()
		{
			CommonStrings.stringIDs.Add(2988408489U, "InvalidTypeToCompare");
			CommonStrings.stringIDs.Add(1246015474U, "AsyncCopyGetException");
			CommonStrings.stringIDs.Add(1020926234U, "InvalidScheduleIntervalFormat");
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003E7B File Offset: 0x0000207B
		public static LocalizedString InvalidTypeToCompare
		{
			get
			{
				return new LocalizedString("InvalidTypeToCompare", "", false, false, CommonStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003E9C File Offset: 0x0000209C
		public static LocalizedString ExDbApiException(Win32Exception ex)
		{
			return new LocalizedString("ExDbApiException", "", false, false, CommonStrings.ResourceManager, new object[]
			{
				ex
			});
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003ECC File Offset: 0x000020CC
		public static LocalizedString ExClusTransientException(string funName)
		{
			return new LocalizedString("ExClusTransientException", "", false, false, CommonStrings.ResourceManager, new object[]
			{
				funName
			});
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003EFB File Offset: 0x000020FB
		public static LocalizedString AsyncCopyGetException
		{
			get
			{
				return new LocalizedString("AsyncCopyGetException", "", false, false, CommonStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003F1C File Offset: 0x0000211C
		public static LocalizedString CannotDetermineExchangeModeException(string reason)
		{
			return new LocalizedString("CannotDetermineExchangeModeException", "", false, false, CommonStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F4C File Offset: 0x0000214C
		public static LocalizedString AsyncExceptionMessage(object message)
		{
			return new LocalizedString("AsyncExceptionMessage", "ExC35295", false, true, CommonStrings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003F7C File Offset: 0x0000217C
		public static LocalizedString InvalidFailureItemException(string param)
		{
			return new LocalizedString("InvalidFailureItemException", "", false, false, CommonStrings.ResourceManager, new object[]
			{
				param
			});
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003FAB File Offset: 0x000021AB
		public static LocalizedString InvalidScheduleIntervalFormat
		{
			get
			{
				return new LocalizedString("InvalidScheduleIntervalFormat", "", false, false, CommonStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003FC9 File Offset: 0x000021C9
		public static LocalizedString GetLocalizedString(CommonStrings.IDs key)
		{
			return new LocalizedString(CommonStrings.stringIDs[(uint)key], CommonStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000070 RID: 112
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(3);

		// Token: 0x04000071 RID: 113
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Common.Strings", typeof(CommonStrings).GetTypeInfo().Assembly);

		// Token: 0x0200001F RID: 31
		public enum IDs : uint
		{
			// Token: 0x04000073 RID: 115
			InvalidTypeToCompare = 2988408489U,
			// Token: 0x04000074 RID: 116
			AsyncCopyGetException = 1246015474U,
			// Token: 0x04000075 RID: 117
			InvalidScheduleIntervalFormat = 1020926234U
		}

		// Token: 0x02000020 RID: 32
		private enum ParamIDs
		{
			// Token: 0x04000077 RID: 119
			ExDbApiException,
			// Token: 0x04000078 RID: 120
			ExClusTransientException,
			// Token: 0x04000079 RID: 121
			CannotDetermineExchangeModeException,
			// Token: 0x0400007A RID: 122
			AsyncExceptionMessage,
			// Token: 0x0400007B RID: 123
			InvalidFailureItemException
		}
	}
}
