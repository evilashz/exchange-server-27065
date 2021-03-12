using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000025 RID: 37
	public class DiagnosticQueryTableFormatter : DiagnosticQueryStringFormatter
	{
		// Token: 0x06000139 RID: 313 RVA: 0x000099B2 File Offset: 0x00007BB2
		private DiagnosticQueryTableFormatter(DiagnosticQueryResults results) : base(results)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000099BB File Offset: 0x00007BBB
		public static DiagnosticQueryTableFormatter Create(DiagnosticQueryResults results)
		{
			return new DiagnosticQueryTableFormatter(results);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000099C4 File Offset: 0x00007BC4
		public static StringBuilder FormatException(DiagnosticQueryException e)
		{
			StringBuilder stringBuilder = new StringBuilder(e.Message.Length);
			string name = e.GetType().Name;
			string value = new string('-', name.Length);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(name);
			stringBuilder.AppendLine(value);
			stringBuilder.AppendLine(e.Message);
			return stringBuilder;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009A24 File Offset: 0x00007C24
		protected override void WriteHeader()
		{
			if (base.Results.Names.Count > 0)
			{
				base.Builder.AppendLine();
				for (int i = 0; i < base.Results.Names.Count; i++)
				{
					string format = string.Format("{{0,-{0}}} ", base.Results.Widths[i]);
					base.Builder.AppendFormat(format, base.Results.Names[i]);
				}
				base.Builder.AppendLine();
				for (int j = 0; j < base.Results.Names.Count; j++)
				{
					base.Builder.AppendFormat("{0} ", new string('-', (int)base.Results.Widths[j]));
				}
				base.Builder.AppendLine();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00009B0C File Offset: 0x00007D0C
		protected override void WriteValues()
		{
			foreach (object[] array in base.Results.Values)
			{
				for (int i = 0; i < array.Length; i++)
				{
					string format = string.Format("{{0,-{0}}} ", base.Results.Widths[i]);
					base.Builder.AppendFormat(format, DiagnosticQueryFormatter<StringBuilder>.FormatValue(array[i]));
				}
				base.Builder.AppendLine();
			}
		}
	}
}
