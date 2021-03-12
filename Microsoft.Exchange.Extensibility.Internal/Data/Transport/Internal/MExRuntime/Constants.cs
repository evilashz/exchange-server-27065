using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000077 RID: 119
	internal static class Constants
	{
		// Token: 0x04000479 RID: 1145
		public static readonly string MExRuntimeLocation = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
	}
}
