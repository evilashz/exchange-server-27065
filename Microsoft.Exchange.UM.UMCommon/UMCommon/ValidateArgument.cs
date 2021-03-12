using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200018E RID: 398
	internal static class ValidateArgument
	{
		// Token: 0x06000D39 RID: 3385 RVA: 0x00031587 File Offset: 0x0002F787
		public static void NotNullOrEmpty(string value, string name)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("Parameter cannot be null or empty", name);
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0003159D File Offset: 0x0002F79D
		public static void NotNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name, "Parameter cannot be null");
			}
		}
	}
}
