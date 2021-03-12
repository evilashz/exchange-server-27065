using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001B8 RID: 440
	internal abstract class ObjectLogSchema
	{
		// Token: 0x06000C1D RID: 3101 RVA: 0x0002C6D1 File Offset: 0x0002A8D1
		public ObjectLogSchema()
		{
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0002C6D9 File Offset: 0x0002A8D9
		public virtual string Software
		{
			get
			{
				return "Microsoft Exchange Server";
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0002C6E0 File Offset: 0x0002A8E0
		public virtual string Version
		{
			get
			{
				return ObjectLogSchema.DefaultVersion;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000C20 RID: 3104
		public abstract string LogType { get; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0002C6E7 File Offset: 0x0002A8E7
		public virtual HashSet<string> ExcludedProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400090E RID: 2318
		public const string DefaultSoftware = "Microsoft Exchange Server";

		// Token: 0x0400090F RID: 2319
		private static readonly string DefaultVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
	}
}
