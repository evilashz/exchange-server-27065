using System;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000016 RID: 22
	internal class LogComparisonEvaluator : LogBinaryOperatorEvaluator
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00003D3C File Offset: 0x00001F3C
		public override bool Evaluate(LogCursor row)
		{
			IComparable comparable = this.Left.GetValue(row) as IComparable;
			object value = this.Right.GetValue(row);
			if (comparable == null)
			{
				if (value == null)
				{
					return this.Operator == LogComparisonOperator.Equals;
				}
				return this.Operator == LogComparisonOperator.NotEquals;
			}
			else
			{
				if (value == null)
				{
					return this.Operator == LogComparisonOperator.NotEquals;
				}
				return this.ApplyOperator(comparable.CompareTo(value));
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003D9C File Offset: 0x00001F9C
		protected bool ApplyOperator(int result)
		{
			switch (this.Operator)
			{
			case LogComparisonOperator.Equals:
				return result == 0;
			case LogComparisonOperator.NotEquals:
				return result != 0;
			case LogComparisonOperator.LessThan:
				return result < 0;
			case LogComparisonOperator.GreaterThan:
				return result > 0;
			case LogComparisonOperator.LessOrEquals:
				return result < 1;
			case LogComparisonOperator.GreaterOrEquals:
				return result > -1;
			default:
				return false;
			}
		}

		// Token: 0x04000032 RID: 50
		public LogComparisonOperator Operator;
	}
}
