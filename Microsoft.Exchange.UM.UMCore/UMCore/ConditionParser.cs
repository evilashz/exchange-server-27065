using System;
using System.Collections;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020000A2 RID: 162
	internal class ConditionParser : ExpressionParser
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x0001A718 File Offset: 0x00018918
		private ConditionParser(ExpressionParser.ILexer lex) : base(lex, ConditionParser.ops)
		{
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001A726 File Offset: 0x00018926
		internal static ConditionParser Instance
		{
			get
			{
				if (ConditionParser.instance == null)
				{
					ConditionParser.instance = new ConditionParser(new ExpressionParser.ConditionLexer());
				}
				return ConditionParser.instance;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001A743 File Offset: 0x00018943
		private static Hashtable Cache
		{
			get
			{
				if (ConditionParser.cache == null)
				{
					ConditionParser.cache = new Hashtable();
				}
				return ConditionParser.cache;
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001A75B File Offset: 0x0001895B
		internal static void Release()
		{
			ConditionParser.instance = null;
			ConditionParser.cache = null;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001A76C File Offset: 0x0001896C
		internal override ExpressionParser.Expression Parse(string exp, ActivityManagerConfig managerConfig)
		{
			base.ManagerConfig = managerConfig;
			string str = (managerConfig == null) ? string.Empty : managerConfig.ClassName;
			string key = str + exp;
			if (ConditionParser.Cache.ContainsKey(key))
			{
				return ConditionParser.Cache[key] as ExpressionParser.Expression;
			}
			ExpressionParser.Expression expression = base.Parse(exp, managerConfig);
			ConditionParser.Cache[key] = expression;
			return expression;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001A7D0 File Offset: 0x000189D0
		protected override ExpressionParser.AtomicExpression ParseAtomicExpression(ArrayList tokens)
		{
			ExpressionParser.Token token = (ExpressionParser.Token)tokens[0];
			ExpressionParser.AtomicExpression result;
			if (token is ExpressionParser.StringToken)
			{
				if (token.Text.StartsWith("@", StringComparison.InvariantCulture))
				{
					result = new ExpressionParser.LiteralExpr(token.Text.Substring(1));
				}
				else if ((result = ExpressionParser.VariableExpr<string>.TryCreate(token.Text, base.ManagerConfig)) == null && (result = ExpressionParser.VariableExpr<object>.TryCreate(token.Text, base.ManagerConfig)) == null && (result = ExpressionParser.VariableExpr<bool>.TryCreate(token.Text, base.ManagerConfig)) == null && (result = ExpressionParser.VariableExpr<int>.TryCreate(token.Text, base.ManagerConfig)) == null && (result = ExpressionParser.VariableExpr<ExDateTime>.TryCreate(token.Text, base.ManagerConfig)) == null)
				{
					throw new FsmConfigurationException(Strings.InvalidVariable(token.Text));
				}
			}
			else
			{
				ExpressionParser.IntToken intToken = (ExpressionParser.IntToken)token;
				result = new ExpressionParser.IntegerExpr(intToken.IntVal);
			}
			tokens.RemoveAt(0);
			return result;
		}

		// Token: 0x04000280 RID: 640
		internal static readonly string AndOperator = "AND";

		// Token: 0x04000281 RID: 641
		private static ExpressionParser.Op[] ops = new ExpressionParser.Op[]
		{
			new ExpressionParser.AndOp("AND"),
			new ExpressionParser.OrOp("OR"),
			new ExpressionParser.NotOp("NOT"),
			new ExpressionParser.IsNullOp("IsNull"),
			new ExpressionParser.GreaterThanOp("GT"),
			new ExpressionParser.LessThanOp("LT"),
			new ExpressionParser.EqualToOp("EQ"),
			new ExpressionParser.NotEqualToOp("NE"),
			new ExpressionParser.LeftParen("("),
			new ExpressionParser.RightParen(")")
		};

		// Token: 0x04000282 RID: 642
		private static ConditionParser instance;

		// Token: 0x04000283 RID: 643
		private static Hashtable cache;
	}
}
