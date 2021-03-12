using System;
using System.Xml;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000CD RID: 205
	internal abstract class WatsonReportAction
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x000177CE File Offset: 0x000159CE
		internal WatsonReportAction(string expression, bool caseSensitive)
		{
			this.expression = (expression ?? string.Empty);
			this.caseSensitive = caseSensitive;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005C2 RID: 1474
		public abstract string ActionName { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000177ED File Offset: 0x000159ED
		protected string Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x060005C4 RID: 1476
		public abstract string Evaluate(WatsonReport watsonReport);

		// Token: 0x060005C5 RID: 1477 RVA: 0x000177F5 File Offset: 0x000159F5
		public override bool Equals(object obj)
		{
			return !(obj.GetType() != base.GetType()) && this.Expression.Equals(((WatsonReportAction)obj).Expression, this.caseSensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001782E File Offset: 0x00015A2E
		public override int GetHashCode()
		{
			return this.Expression.GetHashCode();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001783C File Offset: 0x00015A3C
		internal void WriteResult(WatsonReport watsonReport, XmlWriter writer)
		{
			using (SafeXmlTag safeXmlTag = new SafeXmlTag(writer, "action").WithAttribute("type", this.ActionName))
			{
				try
				{
					safeXmlTag.SetContent(this.Evaluate(watsonReport));
				}
				catch (Exception ex)
				{
					watsonReport.RecordExceptionWhileCreatingReport(ex);
					safeXmlTag.SetContent(string.Concat(new string[]
					{
						"Exception thrown while evaluating action \"",
						this.ActionName,
						"\" (expression: ",
						this.Expression,
						"):\r\n",
						ex.ToString()
					}));
				}
			}
		}

		// Token: 0x04000425 RID: 1061
		private string expression;

		// Token: 0x04000426 RID: 1062
		private bool caseSensitive;
	}
}
