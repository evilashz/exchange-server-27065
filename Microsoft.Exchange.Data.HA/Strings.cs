using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Strings
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00004FE0 File Offset: 0x000031E0
		static Strings()
		{
			Strings.stringIDs.Add(1769372998U, "OperationAborted");
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005030 File Offset: 0x00003230
		public static LocalizedString MinimizedProperty(string propertyName)
		{
			return new LocalizedString("MinimizedProperty", Strings.ResourceManager, new object[]
			{
				propertyName
			});
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00005058 File Offset: 0x00003258
		public static LocalizedString OperationAborted
		{
			get
			{
				return new LocalizedString("OperationAborted", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005070 File Offset: 0x00003270
		public static LocalizedString OperationTimedOut(string timeout)
		{
			return new LocalizedString("OperationTimedOut", Strings.ResourceManager, new object[]
			{
				timeout
			});
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005098 File Offset: 0x00003298
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000092 RID: 146
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000093 RID: 147
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Data.HA.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000027 RID: 39
		public enum IDs : uint
		{
			// Token: 0x04000095 RID: 149
			OperationAborted = 1769372998U
		}

		// Token: 0x02000028 RID: 40
		private enum ParamIDs
		{
			// Token: 0x04000097 RID: 151
			MinimizedProperty,
			// Token: 0x04000098 RID: 152
			OperationTimedOut
		}
	}
}
