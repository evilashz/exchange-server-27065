using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.TextConverters;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000987 RID: 2439
	public static class DataConversionUtils
	{
		// Token: 0x060045CF RID: 17871 RVA: 0x000F53F1 File Offset: 0x000F35F1
		public static string[] ToStringArray<T>(this MultiValuedProperty<T> objectProperty)
		{
			if (objectProperty == null || MultiValuedPropertyBase.IsNullOrEmpty(objectProperty))
			{
				return null;
			}
			return (from e in objectProperty
			select e.ToString()).ToArray<string>();
		}

		// Token: 0x060045D0 RID: 17872 RVA: 0x000F5418 File Offset: 0x000F3618
		public static string ConvertHtmlToText(string html)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(html);
			string @string;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					new HtmlToText
					{
						InputEncoding = Encoding.UTF8,
						OutputEncoding = Encoding.UTF8
					}.Convert(memoryStream, memoryStream2);
					@string = Encoding.UTF8.GetString(memoryStream2.ToArray());
				}
			}
			return @string;
		}
	}
}
