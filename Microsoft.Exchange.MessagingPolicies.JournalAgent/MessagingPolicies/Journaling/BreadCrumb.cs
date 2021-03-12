using System;
using System.Collections;
using System.Linq;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x0200000F RID: 15
	internal class BreadCrumb
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00006B22 File Offset: 0x00004D22
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00006B2A File Offset: 0x00004D2A
		internal string FunctionName { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00006B33 File Offset: 0x00004D33
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00006B3B File Offset: 0x00004D3B
		internal object[] Info { get; set; }

		// Token: 0x06000054 RID: 84 RVA: 0x00006B44 File Offset: 0x00004D44
		public override string ToString()
		{
			return string.Format("[{0} {1}] {2}", this.createdTime, this.FunctionName, BreadCrumb.GetStringValue(this.Info));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00006B6C File Offset: 0x00004D6C
		public string GetAdditionStateInfoString()
		{
			return BreadCrumb.GetStringValue(this.Info);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00006B7C File Offset: 0x00004D7C
		private static string GetStringValue(object obj)
		{
			if (obj == null)
			{
				return "<NULL>";
			}
			IEnumerable enumerable = obj as IEnumerable;
			if (enumerable != null && !(obj is string))
			{
				return "(" + string.Join(",", enumerable.Cast<object>().Select(new Func<object, string>(BreadCrumb.GetStringValue))) + ")";
			}
			return obj.ToString();
		}

		// Token: 0x04000068 RID: 104
		private ExDateTime createdTime = ExDateTime.UtcNow;
	}
}
