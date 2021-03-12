using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001EC RID: 492
	internal class GuidConverter : BaseConverter
	{
		// Token: 0x06000CFF RID: 3327 RVA: 0x0004281C File Offset: 0x00040A1C
		public static Guid Parse(string propertyString)
		{
			return new Guid(propertyString);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00042824 File Offset: 0x00040A24
		public static string ToString(Guid propertyValue)
		{
			return propertyValue.ToString("D");
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00042832 File Offset: 0x00040A32
		public override object ConvertToObject(string propertyString)
		{
			return GuidConverter.Parse(propertyString);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0004283F File Offset: 0x00040A3F
		public override string ConvertToString(object propertyValue)
		{
			return GuidConverter.ToString((Guid)propertyValue);
		}
	}
}
