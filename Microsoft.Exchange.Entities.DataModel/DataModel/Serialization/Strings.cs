using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Entities.DataModel.Serialization
{
	// Token: 0x02000002 RID: 2
	internal static class Strings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static LocalizedString ValueIsOutOfRange(string name, object value)
		{
			return new LocalizedString("ValueIsOutOfRange", Strings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020FC File Offset: 0x000002FC
		public static LocalizedString ErrorNonEntityType(string type, string baseType)
		{
			return new LocalizedString("ErrorNonEntityType", Strings.ResourceManager, new object[]
			{
				type,
				baseType
			});
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002128 File Offset: 0x00000328
		public static LocalizedString ErrorNoDefaultConstructor(string type)
		{
			return new LocalizedString("ErrorNoDefaultConstructor", Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002150 File Offset: 0x00000350
		public static LocalizedString ErrorUnknownType(string typeName)
		{
			return new LocalizedString("ErrorUnknownType", Strings.ResourceManager, new object[]
			{
				typeName
			});
		}

		// Token: 0x04000001 RID: 1
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Entities.DataModel.Serialization.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000003 RID: 3
		private enum ParamIDs
		{
			// Token: 0x04000003 RID: 3
			ValueIsOutOfRange,
			// Token: 0x04000004 RID: 4
			ErrorNonEntityType,
			// Token: 0x04000005 RID: 5
			ErrorNoDefaultConstructor,
			// Token: 0x04000006 RID: 6
			ErrorUnknownType
		}
	}
}
