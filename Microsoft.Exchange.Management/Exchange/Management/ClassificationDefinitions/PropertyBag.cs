using System;
using Microsoft.Mce.Interop.Api;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000855 RID: 2133
	internal sealed class PropertyBag : IPropertyBag
	{
		// Token: 0x06004A00 RID: 18944 RVA: 0x00130848 File Offset: 0x0012EA48
		public int Read(string propertyName, ref object propertyValue, IErrorLog errorLog)
		{
			if (string.Equals(propertyName, "UseLazyRegexCompilation", StringComparison.OrdinalIgnoreCase))
			{
				propertyValue = false;
				return 0;
			}
			return -2147467259;
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x00130869 File Offset: 0x0012EA69
		public int Write(string propertyName, ref object propertyValue)
		{
			return -2147467263;
		}
	}
}
