using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000012 RID: 18
	public class DiagnosticQueryCsvFormatter : DiagnosticQueryStringFormatter
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00004E2E File Offset: 0x0000302E
		private DiagnosticQueryCsvFormatter(DiagnosticQueryResults results) : base(results)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004E38 File Offset: 0x00003038
		private IList<object[]> Values
		{
			get
			{
				if (base.Results.Values.Count == 0)
				{
					string[] emptyStringArray = DiagnosticQueryCsvFormatter.GetEmptyStringArray(base.Results.Names.Count);
					return new object[][]
					{
						emptyStringArray
					};
				}
				return base.Results.Values;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004E85 File Offset: 0x00003085
		public static DiagnosticQueryCsvFormatter Create(DiagnosticQueryResults results)
		{
			return new DiagnosticQueryCsvFormatter(results);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004E90 File Offset: 0x00003090
		protected override void WriteHeader()
		{
			if (base.Results.Names.Count > 0)
			{
				for (int i = 0; i < base.Results.Names.Count; i++)
				{
					string format = string.Format("{0}\"{{0}}\"", (i == 0) ? string.Empty : ",");
					base.Builder.AppendFormat(format, base.Results.Names[i]);
				}
				base.Builder.AppendLine();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004F10 File Offset: 0x00003110
		protected override void WriteValues()
		{
			foreach (object[] array in this.Values)
			{
				for (int i = 0; i < array.Length; i++)
				{
					string format = string.Format("{0}\"{{0}}\"", (i == 0) ? string.Empty : ",");
					string text = array[i] as string;
					if (text != null)
					{
						base.Builder.AppendFormat(format, DiagnosticQueryFormatter<StringBuilder>.FormatValue(text.Replace("\"", "\"\"")));
					}
					else
					{
						base.Builder.AppendFormat(format, DiagnosticQueryFormatter<StringBuilder>.FormatValue(array[i]));
					}
				}
				base.Builder.AppendLine();
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004FDC File Offset: 0x000031DC
		private static string[] GetEmptyStringArray(int numberOfValues)
		{
			char[] array = new char[]
			{
				','
			};
			return new string(array[0], numberOfValues - 1).Split(array, StringSplitOptions.None);
		}
	}
}
