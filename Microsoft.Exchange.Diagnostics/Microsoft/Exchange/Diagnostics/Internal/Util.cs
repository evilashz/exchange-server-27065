using System;

namespace Microsoft.Exchange.Diagnostics.Internal
{
	// Token: 0x0200011E RID: 286
	internal static class Util
	{
		// Token: 0x0600085B RID: 2139 RVA: 0x00021850 File Offset: 0x0001FA50
		public static T EvaluateOrDefault<T>(Util.TryDelegate<T> expression, T defaultValue)
		{
			T result;
			try
			{
				result = expression();
			}
			catch
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002187C File Offset: 0x0001FA7C
		public static T EvaluateOrDefault<T>(Util.TryDelegate<T> expression, T defaultValue, Util.CatchDelegate onThrow)
		{
			T result;
			try
			{
				result = expression();
			}
			catch (Exception ex)
			{
				onThrow(ex);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0200011F RID: 287
		// (Invoke) Token: 0x0600085E RID: 2142
		public delegate T TryDelegate<T>();

		// Token: 0x02000120 RID: 288
		// (Invoke) Token: 0x06000862 RID: 2146
		public delegate void CatchDelegate(Exception ex);
	}
}
