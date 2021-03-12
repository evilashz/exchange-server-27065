using System;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000021 RID: 33
	internal static class UcwaHttpMethod
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000034EC File Offset: 0x000016EC
		internal static bool IsSupportedMethod(string method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			string value = UcwaHttpMethod.Normalize(method);
			return "DELETE".Equals(value, StringComparison.Ordinal) || "GET".Equals(value, StringComparison.Ordinal) || "PATCH".Equals(value, StringComparison.Ordinal) || "PUT".Equals(value, StringComparison.Ordinal) || "POST".Equals(value, StringComparison.Ordinal);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003557 File Offset: 0x00001757
		internal static bool IsDeleteMethod(string method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return "DELETE".Equals(UcwaHttpMethod.Normalize(method), StringComparison.Ordinal);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003578 File Offset: 0x00001778
		internal static bool IsGetMethod(string method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return "GET".Equals(UcwaHttpMethod.Normalize(method), StringComparison.Ordinal);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003599 File Offset: 0x00001799
		internal static bool IsPatchMethod(string method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return "PATCH".Equals(UcwaHttpMethod.Normalize(method), StringComparison.Ordinal);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000035BA File Offset: 0x000017BA
		internal static bool IsPostMethod(string method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return "POST".Equals(UcwaHttpMethod.Normalize(method), StringComparison.Ordinal);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000035DB File Offset: 0x000017DB
		internal static bool IsPutMethod(string method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return "PUT".Equals(UcwaHttpMethod.Normalize(method), StringComparison.Ordinal);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000035FC File Offset: 0x000017FC
		internal static string Normalize(string method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return method.ToUpperInvariant();
		}

		// Token: 0x040000EE RID: 238
		public const string Delete = "DELETE";

		// Token: 0x040000EF RID: 239
		public const string Get = "GET";

		// Token: 0x040000F0 RID: 240
		public const string Patch = "PATCH";

		// Token: 0x040000F1 RID: 241
		public const string Post = "POST";

		// Token: 0x040000F2 RID: 242
		public const string Put = "PUT";
	}
}
