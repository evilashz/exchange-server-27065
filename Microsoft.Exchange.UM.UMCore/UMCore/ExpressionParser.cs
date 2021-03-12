using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000085 RID: 133
	internal abstract class ExpressionParser
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x00019CD0 File Offset: 0x00017ED0
		protected ExpressionParser(ExpressionParser.ILexer lexer, ExpressionParser.Op[] ops)
		{
			this.lexer = lexer;
			this.ops = (ExpressionParser.Op[])ops.Clone();
			foreach (ExpressionParser.Op op in this.ops)
			{
				ExpressionParser.LeftParen leftParen = op as ExpressionParser.LeftParen;
				if (leftParen != null)
				{
					this.leftParen = leftParen;
					break;
				}
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00019D27 File Offset: 0x00017F27
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00019D2F File Offset: 0x00017F2F
		protected ActivityManagerConfig ManagerConfig
		{
			get
			{
				return this.managerConfig;
			}
			set
			{
				this.managerConfig = value;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00019D38 File Offset: 0x00017F38
		internal virtual ExpressionParser.Expression Parse(string exp, ActivityManagerConfig config)
		{
			this.ManagerConfig = config;
			ArrayList arrayList = new ArrayList(this.lexer.ToTokens(exp));
			Stack stack = new Stack();
			stack.Push(ExpressionParser.MarkerOp.Instance);
			while (arrayList.Count > 0)
			{
				object obj = this.ParseOp(arrayList);
				if (obj == null)
				{
					obj = this.ParseAtomicExpression(arrayList);
					if (obj == null)
					{
						throw new ExpressionSyntaxException(Strings.UnexpectedToken(((ExpressionParser.Token)arrayList[0]).Text));
					}
				}
				if (stack.Peek() is ExpressionParser.Expression)
				{
					ExpressionParser.BinaryOp binaryOp = obj as ExpressionParser.BinaryOp;
					if (binaryOp != null)
					{
						this.HandleBinaryOp(stack, binaryOp);
					}
					else if (obj is ExpressionParser.RightParen)
					{
						this.ReduceUntil(stack, this.leftParen);
					}
					else
					{
						if (obj is ExpressionParser.Expression)
						{
							throw new ExpressionSyntaxException(Strings.TwoExpressions);
						}
						if (obj is ExpressionParser.LeftParen)
						{
							throw new ExpressionSyntaxException(Strings.ExpressionLeftParen);
						}
						if (obj is ExpressionParser.UnaryOp)
						{
							throw new ExpressionSyntaxException(Strings.ExpressionUnaryOp);
						}
						throw new ExpressionSyntaxException(Strings.UnexpectedSymbol(obj.ToString()));
					}
				}
				else
				{
					if (obj is ExpressionParser.BinaryOp)
					{
						throw new ExpressionSyntaxException(Strings.OperatorBinaryOp);
					}
					if (obj is ExpressionParser.RightParen)
					{
						throw new ExpressionSyntaxException(Strings.OperatorRightParen);
					}
					stack.Push(obj);
				}
			}
			this.ReduceUntil(stack, ExpressionParser.MarkerOp.Instance);
			return stack.Peek() as ExpressionParser.Expression;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00019EA4 File Offset: 0x000180A4
		protected virtual ExpressionParser.Op ParseOp(ArrayList tokens)
		{
			if (tokens.Count > 0)
			{
				ExpressionParser.Token token = (ExpressionParser.Token)tokens[0];
				ExpressionParser.Op op = this.AsOp(token);
				if (op != null)
				{
					tokens.RemoveAt(0);
					return op;
				}
			}
			return null;
		}

		// Token: 0x060005F7 RID: 1527
		protected abstract ExpressionParser.AtomicExpression ParseAtomicExpression(ArrayList tokens);

		// Token: 0x060005F8 RID: 1528 RVA: 0x00019EDC File Offset: 0x000180DC
		private static ExpressionParser.Action CompareOp(ExpressionParser.Op op1, ExpressionParser.BinaryOp op2)
		{
			if (op1 is ExpressionParser.MarkerOp || op1 is ExpressionParser.LeftParen)
			{
				return ExpressionParser.Action.Shift;
			}
			if (op1.Precedence > op2.Precedence)
			{
				return ExpressionParser.Action.Reduction;
			}
			if (op1.Precedence < op2.Precedence)
			{
				return ExpressionParser.Action.Shift;
			}
			ExpressionParser.BinaryOp binaryOp = op1 as ExpressionParser.BinaryOp;
			if (binaryOp != null)
			{
				if (binaryOp.Assoc == ExpressionParser.BinaryOp.Associativity.Left && op2.Assoc == ExpressionParser.BinaryOp.Associativity.Left)
				{
					return ExpressionParser.Action.Reduction;
				}
				if (binaryOp.Assoc == ExpressionParser.BinaryOp.Associativity.Right && op2.Assoc == ExpressionParser.BinaryOp.Associativity.Right)
				{
					return ExpressionParser.Action.Shift;
				}
			}
			return ExpressionParser.Action.Error;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00019F50 File Offset: 0x00018150
		private ExpressionParser.Op AsOp(ExpressionParser.Token token)
		{
			foreach (ExpressionParser.Op op in this.ops)
			{
				if (op.Matches(token.Text))
				{
					return op;
				}
			}
			return null;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00019F8C File Offset: 0x0001818C
		private void Reduce(Stack stack, ExpressionParser.Expression e)
		{
			ExpressionParser.Op op = (ExpressionParser.Op)stack.Pop();
			ExpressionParser.UnaryOp unaryOp = op as ExpressionParser.UnaryOp;
			if (unaryOp != null)
			{
				stack.Push(new ExpressionParser.UnaryOpExpression(unaryOp, e));
				return;
			}
			ExpressionParser.BinaryOp binaryOp = op as ExpressionParser.BinaryOp;
			if (binaryOp != null)
			{
				ExpressionParser.Expression exp = (ExpressionParser.Expression)stack.Pop();
				stack.Push(new ExpressionParser.BinaryOpExpression(binaryOp, exp, e));
				return;
			}
			stack.Push(op);
			throw new ExpressionSyntaxException(Strings.InvalidOperator(op.ToString()));
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001A000 File Offset: 0x00018200
		private bool ReduceUnless(Stack stack, ExpressionParser.Op delim)
		{
			ExpressionParser.Expression expression = (ExpressionParser.Expression)stack.Pop();
			ExpressionParser.Op op = (ExpressionParser.Op)stack.Peek();
			if (op == delim)
			{
				stack.Push(expression);
				return false;
			}
			this.Reduce(stack, expression);
			return true;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001A03C File Offset: 0x0001823C
		private void HandleBinaryOp(Stack stack, ExpressionParser.BinaryOp bop)
		{
			ExpressionParser.Expression expression;
			for (;;)
			{
				expression = (ExpressionParser.Expression)stack.Pop();
				ExpressionParser.Op op = (ExpressionParser.Op)stack.Peek();
				switch (ExpressionParser.CompareOp(op, bop))
				{
				case ExpressionParser.Action.Shift:
					goto IL_34;
				case ExpressionParser.Action.Reduction:
					this.Reduce(stack, expression);
					continue;
				}
				break;
			}
			throw new ExpressionSyntaxException(Strings.InvalidSyntax);
			IL_34:
			stack.Push(expression);
			stack.Push(bop);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001A0A8 File Offset: 0x000182A8
		private void ReduceUntil(Stack stack, ExpressionParser.Op delim)
		{
			while (this.ReduceUnless(stack, delim))
			{
			}
			ExpressionParser.Expression obj = (ExpressionParser.Expression)stack.Pop();
			stack.Pop();
			stack.Push(obj);
		}

		// Token: 0x04000264 RID: 612
		private readonly ExpressionParser.Op[] ops;

		// Token: 0x04000265 RID: 613
		private ExpressionParser.ILexer lexer;

		// Token: 0x04000266 RID: 614
		private ExpressionParser.LeftParen leftParen;

		// Token: 0x04000267 RID: 615
		private ActivityManagerConfig managerConfig;

		// Token: 0x02000086 RID: 134
		private enum Action
		{
			// Token: 0x04000269 RID: 617
			Error,
			// Token: 0x0400026A RID: 618
			Shift,
			// Token: 0x0400026B RID: 619
			Reduction
		}

		// Token: 0x02000087 RID: 135
		internal interface ILexer
		{
			// Token: 0x060005FE RID: 1534
			ExpressionParser.Token[] ToTokens(string input);
		}

		// Token: 0x02000088 RID: 136
		internal abstract class Op
		{
			// Token: 0x060005FF RID: 1535 RVA: 0x0001A0D9 File Offset: 0x000182D9
			protected Op(string op, int precedence, bool ignoreCase)
			{
				this.operatorString = op;
				this.precedence = precedence;
				this.ignoreCase = ignoreCase;
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0001A0F6 File Offset: 0x000182F6
			protected Op(string op, int precedence) : this(op, precedence, true)
			{
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x0001A101 File Offset: 0x00018301
			protected Op(string op) : this(op, int.MinValue)
			{
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001A10F File Offset: 0x0001830F
			internal string OperatorString
			{
				get
				{
					return this.operatorString;
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001A117 File Offset: 0x00018317
			internal int Precedence
			{
				get
				{
					return this.precedence;
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x06000604 RID: 1540
			internal abstract int Arity { get; }

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x06000605 RID: 1541 RVA: 0x0001A11F File Offset: 0x0001831F
			protected bool IgnoreCase
			{
				get
				{
					return this.ignoreCase;
				}
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0001A127 File Offset: 0x00018327
			public override string ToString()
			{
				return this.operatorString;
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x0001A130 File Offset: 0x00018330
			internal bool Matches(string s)
			{
				StringComparison comparisonType = this.IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;
				return string.Compare(this.operatorString, s, comparisonType) == 0;
			}

			// Token: 0x0400026C RID: 620
			private readonly string operatorString;

			// Token: 0x0400026D RID: 621
			private readonly int precedence;

			// Token: 0x0400026E RID: 622
			private readonly bool ignoreCase;
		}

		// Token: 0x02000089 RID: 137
		internal class UnaryOp : ExpressionParser.Op
		{
			// Token: 0x06000608 RID: 1544 RVA: 0x0001A15A File Offset: 0x0001835A
			protected UnaryOp(string op, int precedence, bool ignoreCase) : base(op, precedence, ignoreCase)
			{
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x0001A165 File Offset: 0x00018365
			protected UnaryOp(string op, int precedence) : base(op, precedence)
			{
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001A16F File Offset: 0x0001836F
			internal override int Arity
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0001A172 File Offset: 0x00018372
			internal virtual object Eval(object operand)
			{
				return null;
			}
		}

		// Token: 0x0200008A RID: 138
		internal class IsNullOp : ExpressionParser.UnaryOp
		{
			// Token: 0x0600060C RID: 1548 RVA: 0x0001A175 File Offset: 0x00018375
			internal IsNullOp(string s) : base(s, 2, false)
			{
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x0001A180 File Offset: 0x00018380
			internal override object Eval(object operand1)
			{
				if (operand1 == null)
				{
					return true;
				}
				string text = operand1 as string;
				if (text != null && text.Length == 0)
				{
					return true;
				}
				return false;
			}
		}

		// Token: 0x0200008B RID: 139
		internal class BinaryOp : ExpressionParser.Op
		{
			// Token: 0x0600060E RID: 1550 RVA: 0x0001A1B6 File Offset: 0x000183B6
			protected BinaryOp(string op, ExpressionParser.BinaryOp.Associativity assoc, int precedence, bool ignoreCase) : base(op, precedence, ignoreCase)
			{
				this.assoc = assoc;
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x0001A1C9 File Offset: 0x000183C9
			protected BinaryOp(string op, ExpressionParser.BinaryOp.Associativity assoc, int precedence) : base(op, precedence)
			{
				this.assoc = assoc;
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001A1DA File Offset: 0x000183DA
			internal ExpressionParser.BinaryOp.Associativity Assoc
			{
				get
				{
					return this.assoc;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001A1E2 File Offset: 0x000183E2
			internal override int Arity
			{
				get
				{
					return 2;
				}
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x0001A1E5 File Offset: 0x000183E5
			internal virtual object Eval(object operand1, object operand2)
			{
				return null;
			}

			// Token: 0x0400026F RID: 623
			private readonly ExpressionParser.BinaryOp.Associativity assoc;

			// Token: 0x0200008C RID: 140
			internal enum Associativity
			{
				// Token: 0x04000271 RID: 625
				None,
				// Token: 0x04000272 RID: 626
				Left,
				// Token: 0x04000273 RID: 627
				Right
			}
		}

		// Token: 0x0200008D RID: 141
		internal class LessThanOp : ExpressionParser.BinaryOp
		{
			// Token: 0x06000613 RID: 1555 RVA: 0x0001A1E8 File Offset: 0x000183E8
			internal LessThanOp(string s) : base(s, ExpressionParser.BinaryOp.Associativity.Left, 2, true)
			{
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x0001A1F4 File Offset: 0x000183F4
			internal override object Eval(object operand1, object operand2)
			{
				IComparable comparable = (IComparable)(operand1 ?? 0);
				IComparable obj = (IComparable)(operand2 ?? 0);
				return comparable.CompareTo(obj) < 0;
			}
		}

		// Token: 0x0200008E RID: 142
		internal class EqualToOp : ExpressionParser.BinaryOp
		{
			// Token: 0x06000615 RID: 1557 RVA: 0x0001A232 File Offset: 0x00018432
			internal EqualToOp(string s) : base(s, ExpressionParser.BinaryOp.Associativity.Left, 2, true)
			{
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x0001A240 File Offset: 0x00018440
			internal override object Eval(object operand1, object operand2)
			{
				IComparable comparable;
				IComparable obj;
				if (operand1 is string || operand2 is string)
				{
					comparable = (IComparable)(operand1 ?? string.Empty);
					obj = (IComparable)(operand2 ?? string.Empty);
				}
				else
				{
					comparable = (IComparable)(operand1 ?? 0);
					obj = (IComparable)(operand2 ?? 0);
				}
				return comparable.CompareTo(obj) == 0;
			}
		}

		// Token: 0x0200008F RID: 143
		internal class NotEqualToOp : ExpressionParser.BinaryOp
		{
			// Token: 0x06000617 RID: 1559 RVA: 0x0001A2B4 File Offset: 0x000184B4
			internal NotEqualToOp(string s) : base(s, ExpressionParser.BinaryOp.Associativity.Left, 2, true)
			{
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x0001A2C0 File Offset: 0x000184C0
			internal override object Eval(object operand1, object operand2)
			{
				IComparable comparable;
				IComparable obj;
				if (operand1 is string || operand2 is string)
				{
					comparable = (IComparable)(operand1 ?? string.Empty);
					obj = (IComparable)(operand2 ?? string.Empty);
				}
				else
				{
					comparable = (IComparable)(operand1 ?? 0);
					obj = (IComparable)(operand2 ?? 0);
				}
				return comparable.CompareTo(obj) != 0;
			}
		}

		// Token: 0x02000090 RID: 144
		internal class GreaterThanOp : ExpressionParser.BinaryOp
		{
			// Token: 0x06000619 RID: 1561 RVA: 0x0001A337 File Offset: 0x00018537
			internal GreaterThanOp(string s) : base(s, ExpressionParser.BinaryOp.Associativity.Left, 2, true)
			{
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x0001A344 File Offset: 0x00018544
			internal override object Eval(object operand1, object operand2)
			{
				IComparable comparable = (IComparable)(operand1 ?? 0);
				IComparable obj = (IComparable)(operand2 ?? 0);
				return comparable.CompareTo(obj) > 0;
			}
		}

		// Token: 0x02000091 RID: 145
		internal class AndOp : ExpressionParser.BinaryOp
		{
			// Token: 0x0600061B RID: 1563 RVA: 0x0001A382 File Offset: 0x00018582
			internal AndOp(string s) : base(s, ExpressionParser.BinaryOp.Associativity.Left, 1, true)
			{
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x0001A390 File Offset: 0x00018590
			internal override object Eval(object operand1, object operand2)
			{
				bool flag = operand1 != null && (bool)operand1;
				bool flag2 = operand2 != null && (bool)operand2;
				return flag && flag2;
			}
		}

		// Token: 0x02000092 RID: 146
		internal class OrOp : ExpressionParser.BinaryOp
		{
			// Token: 0x0600061D RID: 1565 RVA: 0x0001A3C3 File Offset: 0x000185C3
			internal OrOp(string s) : base(s, ExpressionParser.BinaryOp.Associativity.Left, 1, true)
			{
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x0001A3D0 File Offset: 0x000185D0
			internal override object Eval(object operand1, object operand2)
			{
				bool flag = operand1 != null && (bool)operand1;
				bool flag2 = operand2 != null && (bool)operand2;
				return flag || flag2;
			}
		}

		// Token: 0x02000093 RID: 147
		internal class NotOp : ExpressionParser.UnaryOp
		{
			// Token: 0x0600061F RID: 1567 RVA: 0x0001A403 File Offset: 0x00018603
			internal NotOp(string s) : base(s, 3, false)
			{
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x0001A410 File Offset: 0x00018610
			internal override object Eval(object operand1)
			{
				bool flag = operand1 != null && (bool)operand1;
				return !flag;
			}
		}

		// Token: 0x02000094 RID: 148
		internal abstract class Expression
		{
			// Token: 0x06000621 RID: 1569
			internal abstract object Eval(ActivityManager manager);
		}

		// Token: 0x02000095 RID: 149
		internal abstract class AtomicExpression : ExpressionParser.Expression
		{
		}

		// Token: 0x02000096 RID: 150
		internal class LiteralExpr : ExpressionParser.AtomicExpression
		{
			// Token: 0x06000624 RID: 1572 RVA: 0x0001A443 File Offset: 0x00018643
			internal LiteralExpr(string literal)
			{
				this.literal = literal;
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x0001A452 File Offset: 0x00018652
			internal override object Eval(ActivityManager manager)
			{
				return this.literal;
			}

			// Token: 0x04000274 RID: 628
			private string literal;
		}

		// Token: 0x02000097 RID: 151
		internal class VariableExpr<T> : ExpressionParser.AtomicExpression
		{
			// Token: 0x06000626 RID: 1574 RVA: 0x0001A45A File Offset: 0x0001865A
			private VariableExpr(FsmVariable<T> fsmVar)
			{
				this.fsmVar = fsmVar;
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x0001A469 File Offset: 0x00018669
			internal static bool TryCreate(string varName, ActivityManagerConfig managerConfig, out ExpressionParser.VariableExpr<T> var)
			{
				var = ExpressionParser.VariableExpr<T>.TryCreate(varName, managerConfig);
				return null != var;
			}

			// Token: 0x06000628 RID: 1576 RVA: 0x0001A47C File Offset: 0x0001867C
			internal static ExpressionParser.VariableExpr<T> TryCreate(string varName, ActivityManagerConfig managerConfig)
			{
				FsmVariable<T> fsmVariable = null;
				QualifiedName variableName = new QualifiedName(varName, managerConfig);
				if (!FsmVariable<T>.TryCreate(variableName, managerConfig, out fsmVariable))
				{
					return null;
				}
				return new ExpressionParser.VariableExpr<T>(fsmVariable);
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x0001A4A8 File Offset: 0x000186A8
			internal static ExpressionParser.VariableExpr<T> Create(string varName, ActivityManagerConfig managerConfig)
			{
				ExpressionParser.VariableExpr<T> result = null;
				if (!ExpressionParser.VariableExpr<T>.TryCreate(varName, managerConfig, out result))
				{
					throw new FsmConfigurationException(Strings.InvalidCondition(varName));
				}
				return result;
			}

			// Token: 0x0600062A RID: 1578 RVA: 0x0001A4D4 File Offset: 0x000186D4
			internal override object Eval(ActivityManager manager)
			{
				return this.fsmVar.GetValue(manager);
			}

			// Token: 0x04000275 RID: 629
			private FsmVariable<T> fsmVar;
		}

		// Token: 0x02000098 RID: 152
		internal class IntegerExpr : ExpressionParser.AtomicExpression
		{
			// Token: 0x0600062B RID: 1579 RVA: 0x0001A4E7 File Offset: 0x000186E7
			internal IntegerExpr(int val)
			{
				this.val = val;
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x0001A4F6 File Offset: 0x000186F6
			internal override object Eval(ActivityManager manager)
			{
				return this.val;
			}

			// Token: 0x04000276 RID: 630
			private int val;
		}

		// Token: 0x02000099 RID: 153
		internal abstract class Token
		{
			// Token: 0x0600062D RID: 1581 RVA: 0x0001A503 File Offset: 0x00018703
			protected Token(string text)
			{
				this.text = text;
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001A512 File Offset: 0x00018712
			internal string Text
			{
				get
				{
					return this.text;
				}
			}

			// Token: 0x04000277 RID: 631
			private readonly string text;
		}

		// Token: 0x0200009A RID: 154
		internal class StringToken : ExpressionParser.Token
		{
			// Token: 0x0600062F RID: 1583 RVA: 0x0001A51A File Offset: 0x0001871A
			internal StringToken(string s) : base(s.Trim())
			{
			}
		}

		// Token: 0x0200009B RID: 155
		internal class IntToken : ExpressionParser.Token
		{
			// Token: 0x06000630 RID: 1584 RVA: 0x0001A528 File Offset: 0x00018728
			internal IntToken(string s) : base(s)
			{
				this.intVal = int.Parse(s, CultureInfo.InvariantCulture);
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000631 RID: 1585 RVA: 0x0001A542 File Offset: 0x00018742
			// (set) Token: 0x06000632 RID: 1586 RVA: 0x0001A54A File Offset: 0x0001874A
			internal int IntVal
			{
				get
				{
					return this.intVal;
				}
				set
				{
					this.intVal = value;
				}
			}

			// Token: 0x04000278 RID: 632
			private int intVal;
		}

		// Token: 0x0200009C RID: 156
		internal class ConditionLexer : ExpressionParser.ILexer
		{
			// Token: 0x06000633 RID: 1587 RVA: 0x0001A553 File Offset: 0x00018753
			internal ConditionLexer()
			{
			}

			// Token: 0x06000634 RID: 1588 RVA: 0x0001A55C File Offset: 0x0001875C
			public ExpressionParser.Token[] ToTokens(string input)
			{
				int i = 0;
				ArrayList arrayList = new ArrayList();
				while (i < input.Length)
				{
					int num = input.IndexOfAny(ExpressionParser.ConditionLexer.specialChars, i);
					if (num == i)
					{
						if (!char.IsWhiteSpace(input[num]))
						{
							arrayList.Add(new ExpressionParser.StringToken(input.Substring(i, 1)));
						}
						i++;
					}
					else
					{
						if (-1 == num)
						{
							num = input.Length;
						}
						string text = input.Substring(i, num - i);
						if (char.IsDigit(text[0]))
						{
							arrayList.Add(new ExpressionParser.IntToken(text));
						}
						else
						{
							arrayList.Add(new ExpressionParser.StringToken(text));
						}
						i = num;
					}
				}
				return (ExpressionParser.Token[])arrayList.ToArray(typeof(ExpressionParser.Token));
			}

			// Token: 0x04000279 RID: 633
			private static char[] specialChars = new char[]
			{
				'(',
				')',
				' '
			};
		}

		// Token: 0x0200009D RID: 157
		internal sealed class MarkerOp : ExpressionParser.Op
		{
			// Token: 0x06000636 RID: 1590 RVA: 0x0001A62E File Offset: 0x0001882E
			private MarkerOp() : base(string.Empty)
			{
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001A63B File Offset: 0x0001883B
			internal override int Arity
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0400027A RID: 634
			internal static readonly ExpressionParser.MarkerOp Instance = new ExpressionParser.MarkerOp();
		}

		// Token: 0x0200009E RID: 158
		internal class LeftParen : ExpressionParser.Op
		{
			// Token: 0x06000639 RID: 1593 RVA: 0x0001A64A File Offset: 0x0001884A
			internal LeftParen(string leftParen) : base(leftParen)
			{
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001A653 File Offset: 0x00018853
			internal override int Arity
			{
				get
				{
					return 0;
				}
			}
		}

		// Token: 0x0200009F RID: 159
		internal class RightParen : ExpressionParser.Op
		{
			// Token: 0x0600063B RID: 1595 RVA: 0x0001A656 File Offset: 0x00018856
			internal RightParen(string rightParen) : base(rightParen)
			{
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001A65F File Offset: 0x0001885F
			internal override int Arity
			{
				get
				{
					return 0;
				}
			}
		}

		// Token: 0x020000A0 RID: 160
		internal class UnaryOpExpression : ExpressionParser.Expression
		{
			// Token: 0x0600063D RID: 1597 RVA: 0x0001A662 File Offset: 0x00018862
			internal UnaryOpExpression(ExpressionParser.UnaryOp op, ExpressionParser.Expression exp)
			{
				this.op = op;
				this.exp = exp;
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001A678 File Offset: 0x00018878
			internal ExpressionParser.UnaryOp Op
			{
				get
				{
					return this.op;
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001A680 File Offset: 0x00018880
			internal ExpressionParser.Expression Exp
			{
				get
				{
					return this.exp;
				}
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x0001A688 File Offset: 0x00018888
			internal override object Eval(ActivityManager manager)
			{
				object operand = this.Exp.Eval(manager);
				return this.Op.Eval(operand);
			}

			// Token: 0x0400027B RID: 635
			private readonly ExpressionParser.UnaryOp op;

			// Token: 0x0400027C RID: 636
			private readonly ExpressionParser.Expression exp;
		}

		// Token: 0x020000A1 RID: 161
		internal class BinaryOpExpression : ExpressionParser.Expression
		{
			// Token: 0x06000641 RID: 1601 RVA: 0x0001A6AE File Offset: 0x000188AE
			internal BinaryOpExpression(ExpressionParser.BinaryOp op, ExpressionParser.Expression exp1, ExpressionParser.Expression exp2)
			{
				this.op = op;
				this.exp1 = exp1;
				this.exp2 = exp2;
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001A6CB File Offset: 0x000188CB
			internal ExpressionParser.BinaryOp Op
			{
				get
				{
					return this.op;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001A6D3 File Offset: 0x000188D3
			internal ExpressionParser.Expression Exp2
			{
				get
				{
					return this.exp2;
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001A6DB File Offset: 0x000188DB
			internal ExpressionParser.Expression Exp1
			{
				get
				{
					return this.exp1;
				}
			}

			// Token: 0x06000645 RID: 1605 RVA: 0x0001A6E4 File Offset: 0x000188E4
			internal override object Eval(ActivityManager manager)
			{
				object operand = this.Exp1.Eval(manager);
				object operand2 = this.Exp2.Eval(manager);
				return this.Op.Eval(operand, operand2);
			}

			// Token: 0x0400027D RID: 637
			private readonly ExpressionParser.BinaryOp op;

			// Token: 0x0400027E RID: 638
			private readonly ExpressionParser.Expression exp1;

			// Token: 0x0400027F RID: 639
			private readonly ExpressionParser.Expression exp2;
		}
	}
}
