using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF1 RID: 3569
	internal static class TypeExtensions
	{
		// Token: 0x06005C86 RID: 23686 RVA: 0x001206BC File Offset: 0x0011E8BC
		public static string MakeODataCollectionTypeName(this Type type)
		{
			return string.Format("Collection({0})", type.IsEnum ? type.FullName : type.FullName.Replace("System.", "Edm."));
		}
	}
}
