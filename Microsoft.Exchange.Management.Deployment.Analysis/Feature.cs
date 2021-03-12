using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000019 RID: 25
	public abstract class Feature
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00004358 File Offset: 0x00002558
		public override string ToString()
		{
			string text = "Feature";
			string text2 = base.GetType().Name;
			if (text2.EndsWith(text, StringComparison.OrdinalIgnoreCase))
			{
				text2 = text2.Remove(text2.Length - text.Length);
			}
			return text2;
		}
	}
}
