using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000082 RID: 130
	internal class ExpressionParser
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x0001037C File Offset: 0x0000E57C
		public static void EnrolPredefinedTypes(Type type)
		{
			lock (ExpressionParser.syncRoot)
			{
				if (null != type && !ExpressionParser.predefinedTypes.Contains(type))
				{
					ExpressionParser.predefinedTypes.Add(type);
				}
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000103D8 File Offset: 0x0000E5D8
		public static void RemovePredefinedTypes(Type type)
		{
			lock (ExpressionParser.syncRoot)
			{
				if (null != type && ExpressionParser.predefinedTypes.Contains(type))
				{
					ExpressionParser.predefinedTypes.Remove(type);
				}
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00010434 File Offset: 0x0000E634
		public ExpressionParser(ParameterExpression[] parameters, string expression, object[] values) : this(parameters, expression, null, values)
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00010440 File Offset: 0x0000E640
		public ExpressionParser(ParameterExpression[] parameters, string expression, Type[] servicePredefinedTypes, object[] values)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (this.keywords == null)
			{
				this.keywords = ExpressionParser.CreateKeywords(servicePredefinedTypes);
			}
			this.symbols = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			this.literals = new Dictionary<Expression, string>();
			if (parameters != null)
			{
				this.ProcessParameters(parameters);
			}
			if (values != null)
			{
				this.ProcessValues(values);
			}
			this.text = expression;
			this.textLen = this.text.Length;
			this.SetTextPos(0);
			this.NextToken();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000104CC File Offset: 0x0000E6CC
		private void ProcessParameters(ParameterExpression[] parameters)
		{
			foreach (ParameterExpression parameterExpression in parameters)
			{
				if (!string.IsNullOrEmpty(parameterExpression.Name))
				{
					this.AddSymbol(parameterExpression.Name, parameterExpression);
				}
			}
			if (parameters.Length == 1 && string.IsNullOrEmpty(parameters[0].Name))
			{
				this.it = parameters[0];
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00010528 File Offset: 0x0000E728
		private void ProcessValues(object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				object obj = values[i];
				if (i == values.Length - 1 && obj is IDictionary<string, object>)
				{
					this.externals = (IDictionary<string, object>)obj;
				}
				else
				{
					this.AddSymbol("@" + i.ToString(CultureInfo.InvariantCulture), obj);
				}
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00010584 File Offset: 0x0000E784
		private void AddSymbol(string name, object value)
		{
			if (this.symbols.ContainsKey(name))
			{
				throw this.ParseError("The identifier '{0}' was defined more than once", new object[]
				{
					name
				});
			}
			this.symbols.Add(name, value);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000105C4 File Offset: 0x0000E7C4
		public Expression Parse(Type resultType)
		{
			int pos = this.token.pos;
			Expression expression = this.ParseExpression();
			if (resultType != null && (expression = this.PromoteExpression(expression, resultType, true)) == null)
			{
				throw this.ParseError(pos, "Expression of type '{0}' expected", new object[]
				{
					ExpressionParser.GetTypeName(resultType)
				});
			}
			this.ValidateToken(ExpressionParser.TokenId.End, "Syntax error");
			return expression;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00010628 File Offset: 0x0000E828
		public IEnumerable<DynamicOrdering> ParseOrdering()
		{
			List<DynamicOrdering> list = new List<DynamicOrdering>();
			for (;;)
			{
				Expression selector = this.ParseExpression();
				bool ascending = true;
				if (this.TokenIdentifierIs("asc") || this.TokenIdentifierIs("ascending"))
				{
					this.NextToken();
				}
				else if (this.TokenIdentifierIs("desc") || this.TokenIdentifierIs("descending"))
				{
					this.NextToken();
					ascending = false;
				}
				list.Add(new DynamicOrdering
				{
					Selector = selector,
					Ascending = ascending
				});
				if (this.token.id != ExpressionParser.TokenId.Comma)
				{
					break;
				}
				this.NextToken();
			}
			this.ValidateToken(ExpressionParser.TokenId.End, "Syntax error");
			return list;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000106C8 File Offset: 0x0000E8C8
		private Expression ParseExpression()
		{
			int pos = this.token.pos;
			Expression expression = this.ParseLogicalOr();
			if (this.token.id == ExpressionParser.TokenId.Question)
			{
				this.NextToken();
				Expression expr = this.ParseExpression();
				this.ValidateToken(ExpressionParser.TokenId.Colon, "':' expected");
				this.NextToken();
				Expression expr2 = this.ParseExpression();
				expression = this.GenerateConditional(expression, expr, expr2, pos);
			}
			return expression;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001072C File Offset: 0x0000E92C
		private Expression ParseLogicalOr()
		{
			Expression expression = this.ParseLogicalAnd();
			while (this.token.id == ExpressionParser.TokenId.DoubleBar || this.TokenIdentifierIs("or"))
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression right = this.ParseLogicalAnd();
				this.CheckAndPromoteOperands(typeof(ExpressionParser.ILogicalSignatures), token.text, ref expression, ref right, token.pos);
				expression = Expression.OrElse(expression, right);
			}
			return expression;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000107A0 File Offset: 0x0000E9A0
		private Expression ParseLogicalAnd()
		{
			Expression expression = this.ParseComparison();
			while (this.token.id == ExpressionParser.TokenId.DoubleAmphersand || this.TokenIdentifierIs("and"))
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression right = this.ParseComparison();
				this.CheckAndPromoteOperands(typeof(ExpressionParser.ILogicalSignatures), token.text, ref expression, ref right, token.pos);
				expression = Expression.AndAlso(expression, right);
			}
			return expression;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00010814 File Offset: 0x0000EA14
		private Expression ParseComparison()
		{
			Expression expression = this.ParseAdditive();
			while (this.token.id == ExpressionParser.TokenId.Equal || this.token.id == ExpressionParser.TokenId.DoubleEqual || this.token.id == ExpressionParser.TokenId.ExclamationEqual || this.token.id == ExpressionParser.TokenId.LessGreater || this.token.id == ExpressionParser.TokenId.GreaterThan || this.token.id == ExpressionParser.TokenId.GreaterThanEqual || this.token.id == ExpressionParser.TokenId.LessThan || this.token.id == ExpressionParser.TokenId.LessThanEqual)
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression expression2 = this.ParseAdditive();
				bool flag = token.id == ExpressionParser.TokenId.Equal || token.id == ExpressionParser.TokenId.DoubleEqual || token.id == ExpressionParser.TokenId.ExclamationEqual || token.id == ExpressionParser.TokenId.LessGreater;
				if (flag && !expression.Type.IsValueType && !expression2.Type.IsValueType)
				{
					if (expression.Type != expression2.Type)
					{
						if (expression.Type.IsAssignableFrom(expression2.Type))
						{
							expression2 = Expression.Convert(expression2, expression.Type);
						}
						else
						{
							if (!expression2.Type.IsAssignableFrom(expression.Type))
							{
								throw this.IncompatibleOperandsError(token.text, expression, expression2, token.pos);
							}
							expression = Expression.Convert(expression, expression2.Type);
						}
					}
				}
				else if (ExpressionParser.IsEnumType(expression.Type) || ExpressionParser.IsEnumType(expression2.Type))
				{
					if (expression.Type != expression2.Type)
					{
						Expression expression3;
						if ((expression3 = this.PromoteExpression(expression2, expression.Type, true)) != null)
						{
							expression2 = expression3;
						}
						else
						{
							if ((expression3 = this.PromoteExpression(expression, expression2.Type, true)) == null)
							{
								throw this.IncompatibleOperandsError(token.text, expression, expression2, token.pos);
							}
							expression = expression3;
						}
					}
				}
				else
				{
					this.CheckAndPromoteOperands(flag ? typeof(ExpressionParser.IEqualitySignatures) : typeof(ExpressionParser.IRelationalSignatures), token.text, ref expression, ref expression2, token.pos);
				}
				switch (token.id)
				{
				case ExpressionParser.TokenId.LessThan:
					expression = this.GenerateLessThan(expression, expression2);
					break;
				case ExpressionParser.TokenId.Equal:
				case ExpressionParser.TokenId.DoubleEqual:
					expression = this.GenerateEqual(expression, expression2);
					break;
				case ExpressionParser.TokenId.GreaterThan:
					expression = this.GenerateGreaterThan(expression, expression2);
					break;
				case ExpressionParser.TokenId.ExclamationEqual:
				case ExpressionParser.TokenId.LessGreater:
					expression = this.GenerateNotEqual(expression, expression2);
					break;
				case ExpressionParser.TokenId.LessThanEqual:
					expression = this.GenerateLessThanEqual(expression, expression2);
					break;
				case ExpressionParser.TokenId.GreaterThanEqual:
					expression = this.GenerateGreaterThanEqual(expression, expression2);
					break;
				}
			}
			return expression;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00010ACC File Offset: 0x0000ECCC
		private Expression ParseAdditive()
		{
			Expression expression = this.ParseMultiplicative();
			while (this.token.id == ExpressionParser.TokenId.Plus || this.token.id == ExpressionParser.TokenId.Minus || this.token.id == ExpressionParser.TokenId.Amphersand)
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression expression2 = this.ParseMultiplicative();
				ExpressionParser.TokenId id = token.id;
				if (id != ExpressionParser.TokenId.Amphersand)
				{
					switch (id)
					{
					case ExpressionParser.TokenId.Plus:
						if (!(expression.Type == typeof(string)) && !(expression2.Type == typeof(string)))
						{
							this.CheckAndPromoteOperands(typeof(ExpressionParser.IAddSignatures), token.text, ref expression, ref expression2, token.pos);
							expression = this.GenerateAdd(expression, expression2);
							continue;
						}
						break;
					case ExpressionParser.TokenId.Comma:
						continue;
					case ExpressionParser.TokenId.Minus:
						this.CheckAndPromoteOperands(typeof(ExpressionParser.ISubtractSignatures), token.text, ref expression, ref expression2, token.pos);
						expression = this.GenerateSubtract(expression, expression2);
						continue;
					default:
						continue;
					}
				}
				expression = this.GenerateStringConcat(expression, expression2);
			}
			return expression;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00010BEC File Offset: 0x0000EDEC
		private Expression ParseMultiplicative()
		{
			Expression expression = this.ParseUnary();
			while (this.token.id == ExpressionParser.TokenId.Asterisk || this.token.id == ExpressionParser.TokenId.Slash || this.token.id == ExpressionParser.TokenId.Percent || this.TokenIdentifierIs("mod"))
			{
				ExpressionParser.Token token = this.token;
				this.NextToken();
				Expression right = this.ParseUnary();
				this.CheckAndPromoteOperands(typeof(ExpressionParser.IArithmeticSignatures), token.text, ref expression, ref right, token.pos);
				ExpressionParser.TokenId id = token.id;
				if (id <= ExpressionParser.TokenId.Percent)
				{
					if (id == ExpressionParser.TokenId.Identifier || id == ExpressionParser.TokenId.Percent)
					{
						expression = Expression.Modulo(expression, right);
					}
				}
				else if (id != ExpressionParser.TokenId.Asterisk)
				{
					if (id == ExpressionParser.TokenId.Slash)
					{
						expression = Expression.Divide(expression, right);
					}
				}
				else
				{
					expression = Expression.Multiply(expression, right);
				}
			}
			return expression;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00010CBC File Offset: 0x0000EEBC
		private Expression ParseUnary()
		{
			if (this.token.id != ExpressionParser.TokenId.Minus && this.token.id != ExpressionParser.TokenId.Exclamation && !this.TokenIdentifierIs("not"))
			{
				return this.ParsePrimary();
			}
			ExpressionParser.Token token = this.token;
			this.NextToken();
			if (token.id == ExpressionParser.TokenId.Minus && (this.token.id == ExpressionParser.TokenId.IntegerLiteral || this.token.id == ExpressionParser.TokenId.RealLiteral))
			{
				this.token.text = "-" + this.token.text;
				this.token.pos = token.pos;
				return this.ParsePrimary();
			}
			Expression expression = this.ParseUnary();
			if (token.id == ExpressionParser.TokenId.Minus)
			{
				this.CheckAndPromoteOperand(typeof(ExpressionParser.INegationSignatures), token.text, ref expression, token.pos);
				expression = Expression.Negate(expression);
			}
			else
			{
				this.CheckAndPromoteOperand(typeof(ExpressionParser.INotSignatures), token.text, ref expression, token.pos);
				expression = Expression.Not(expression);
			}
			return expression;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00010DD0 File Offset: 0x0000EFD0
		private Expression ParsePrimary()
		{
			Expression expression = this.ParsePrimaryStart();
			for (;;)
			{
				if (this.token.id == ExpressionParser.TokenId.Dot)
				{
					this.NextToken();
					expression = this.ParseMemberAccess(null, expression);
				}
				else
				{
					if (this.token.id != ExpressionParser.TokenId.OpenBracket)
					{
						break;
					}
					expression = this.ParseElementAccess(expression);
				}
			}
			return expression;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00010E20 File Offset: 0x0000F020
		private Expression ParsePrimaryStart()
		{
			switch (this.token.id)
			{
			case ExpressionParser.TokenId.Identifier:
				return this.ParseIdentifier();
			case ExpressionParser.TokenId.StringLiteral:
				return this.ParseStringLiteral();
			case ExpressionParser.TokenId.IntegerLiteral:
				return this.ParseIntegerLiteral();
			case ExpressionParser.TokenId.RealLiteral:
				return this.ParseRealLiteral();
			case ExpressionParser.TokenId.OpenParen:
				return this.ParseParenExpression();
			}
			throw this.ParseError("Expression expected", new object[0]);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00010E98 File Offset: 0x0000F098
		private Expression ParseStringLiteral()
		{
			this.ValidateToken(ExpressionParser.TokenId.StringLiteral);
			char c = this.token.text[0];
			string text = this.token.text.Substring(1, this.token.text.Length - 2);
			int startIndex = 0;
			for (;;)
			{
				int num = text.IndexOf(c, startIndex);
				if (num < 0)
				{
					break;
				}
				text = text.Remove(num, 1);
				startIndex = num + 1;
			}
			if (c != '\'')
			{
				this.NextToken();
				return this.CreateLiteral(text, text);
			}
			if (text.Length != 1)
			{
				throw this.ParseError("Character literal must contain exactly one character", new object[0]);
			}
			this.NextToken();
			return this.CreateLiteral(text[0], text);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00010F48 File Offset: 0x0000F148
		private Expression ParseIntegerLiteral()
		{
			this.ValidateToken(ExpressionParser.TokenId.IntegerLiteral);
			string text = this.token.text;
			if (text[0] != '-')
			{
				ulong num;
				if (!ulong.TryParse(text, out num))
				{
					throw this.ParseError("Invalid integer literal '{0}'", new object[]
					{
						text
					});
				}
				this.NextToken();
				if (num <= 2147483647UL)
				{
					return this.CreateLiteral((int)num, text);
				}
				if (num <= (ulong)-1)
				{
					return this.CreateLiteral((uint)num, text);
				}
				if (num <= 9223372036854775807UL)
				{
					return this.CreateLiteral((long)num, text);
				}
				return this.CreateLiteral(num, text);
			}
			else
			{
				long num2;
				if (!long.TryParse(text, out num2))
				{
					throw this.ParseError("Invalid integer literal '{0}'", new object[]
					{
						text
					});
				}
				this.NextToken();
				if (num2 >= -2147483648L && num2 <= 2147483647L)
				{
					return this.CreateLiteral((int)num2, text);
				}
				return this.CreateLiteral(num2, text);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00011048 File Offset: 0x0000F248
		private Expression ParseRealLiteral()
		{
			this.ValidateToken(ExpressionParser.TokenId.RealLiteral);
			string text = this.token.text;
			object obj = null;
			char c = text[text.Length - 1];
			double num2;
			if (c == 'F' || c == 'f')
			{
				float num;
				if (float.TryParse(text.Substring(0, text.Length - 1), out num))
				{
					obj = num;
				}
			}
			else if (double.TryParse(text, out num2))
			{
				obj = num2;
			}
			if (obj == null)
			{
				throw this.ParseError("Invalid real literal '{0}'", new object[]
				{
					text
				});
			}
			this.NextToken();
			return this.CreateLiteral(obj, text);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000110E4 File Offset: 0x0000F2E4
		private Expression CreateLiteral(object value, string text)
		{
			ConstantExpression constantExpression = Expression.Constant(value);
			this.literals.Add(constantExpression, text);
			return constantExpression;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00011108 File Offset: 0x0000F308
		private Expression ParseParenExpression()
		{
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, "'(' expected");
			this.NextToken();
			Expression result = this.ParseExpression();
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, "')' or operator expected");
			this.NextToken();
			return result;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00011144 File Offset: 0x0000F344
		private Expression ParseIdentifier()
		{
			this.ValidateToken(ExpressionParser.TokenId.Identifier);
			object obj;
			if (this.keywords.TryGetValue(this.token.text, out obj))
			{
				if (obj is Type)
				{
					return this.ParseTypeAccess((Type)obj);
				}
				if (obj == ExpressionParser.keywordIt)
				{
					return this.ParseIt();
				}
				if (obj == ExpressionParser.keywordIif)
				{
					return this.ParseIif();
				}
				if (obj == ExpressionParser.keywordNew)
				{
					return this.ParseNew();
				}
				this.NextToken();
				return (Expression)obj;
			}
			else
			{
				if (this.symbols.TryGetValue(this.token.text, out obj) || (this.externals != null && this.externals.TryGetValue(this.token.text, out obj)))
				{
					Expression expression = obj as Expression;
					if (expression == null)
					{
						expression = Expression.Constant(obj);
					}
					else
					{
						LambdaExpression lambdaExpression = expression as LambdaExpression;
						if (lambdaExpression != null)
						{
							return this.ParseLambdaInvocation(lambdaExpression);
						}
					}
					this.NextToken();
					return expression;
				}
				if (this.it != null)
				{
					return this.ParseMemberAccess(null, this.it);
				}
				throw this.ParseError("Unknown identifier '{0}'", new object[]
				{
					this.token.text
				});
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00011261 File Offset: 0x0000F461
		private Expression ParseIt()
		{
			if (this.it == null)
			{
				throw this.ParseError("No 'it' is in scope", new object[0]);
			}
			this.NextToken();
			return this.it;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001128C File Offset: 0x0000F48C
		private Expression ParseIif()
		{
			int pos = this.token.pos;
			this.NextToken();
			Expression[] array = this.ParseArgumentList();
			if (array.Length != 3)
			{
				throw this.ParseError(pos, "The 'iif' function requires three arguments", new object[0]);
			}
			return this.GenerateConditional(array[0], array[1], array[2], pos);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000112DC File Offset: 0x0000F4DC
		private Expression GenerateConditional(Expression test, Expression expr1, Expression expr2, int errorPos)
		{
			if (test.Type != typeof(bool))
			{
				throw this.ParseError(errorPos, "The first expression must be of type 'Boolean'", new object[0]);
			}
			if (expr1.Type != expr2.Type)
			{
				Expression expression = (expr2 != ExpressionParser.nullLiteral) ? this.PromoteExpression(expr1, expr2.Type, true) : null;
				Expression expression2 = (expr1 != ExpressionParser.nullLiteral) ? this.PromoteExpression(expr2, expr1.Type, true) : null;
				if (expression != null && expression2 == null)
				{
					expr1 = expression;
				}
				else if (expression2 != null && expression == null)
				{
					expr2 = expression2;
				}
				else
				{
					string text = (expr1 != ExpressionParser.nullLiteral) ? expr1.Type.Name : "null";
					string text2 = (expr2 != ExpressionParser.nullLiteral) ? expr2.Type.Name : "null";
					if (expression != null && expression2 != null)
					{
						throw this.ParseError(errorPos, "Both of the types '{0}' and '{1}' convert to the other", new object[]
						{
							text,
							text2
						});
					}
					throw this.ParseError(errorPos, "Neither of the types '{0}' and '{1}' converts to the other", new object[]
					{
						text,
						text2
					});
				}
			}
			return Expression.Condition(test, expr1, expr2);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00011404 File Offset: 0x0000F604
		private Expression ParseNew()
		{
			this.NextToken();
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, "'(' expected");
			this.NextToken();
			List<DynamicProperty> list = new List<DynamicProperty>();
			List<Expression> list2 = new List<Expression>();
			int pos;
			for (;;)
			{
				pos = this.token.pos;
				Expression expression = this.ParseExpression();
				string name;
				if (this.TokenIdentifierIs("as"))
				{
					this.NextToken();
					name = this.GetIdentifier();
					this.NextToken();
				}
				else
				{
					MemberExpression memberExpression = expression as MemberExpression;
					if (memberExpression == null)
					{
						break;
					}
					name = memberExpression.Member.Name;
				}
				list2.Add(expression);
				list.Add(new DynamicProperty(name, expression.Type));
				if (this.token.id != ExpressionParser.TokenId.Comma)
				{
					goto IL_BC;
				}
				this.NextToken();
			}
			throw this.ParseError(pos, "Expression is missing an 'as' clause", new object[0]);
			IL_BC:
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, "')' or ',' expected");
			this.NextToken();
			Type type = DynamicExpression.CreateClass(list);
			MemberBinding[] array = new MemberBinding[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Expression.Bind(type.GetPropertyEx(list[i].Name), list2[i]);
			}
			return Expression.MemberInit(Expression.New(type), array);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001153C File Offset: 0x0000F73C
		private Expression ParseLambdaInvocation(LambdaExpression lambda)
		{
			int pos = this.token.pos;
			this.NextToken();
			Expression[] array = this.ParseArgumentList();
			MethodBase methodBase;
			if (this.FindMethod(lambda.Type, "Invoke", false, array, out methodBase) != 1)
			{
				throw this.ParseError(pos, "Argument list incompatible with lambda expression", new object[0]);
			}
			return Expression.Invoke(lambda, array);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00011594 File Offset: 0x0000F794
		private Expression ParseTypeAccess(Type type)
		{
			int pos = this.token.pos;
			this.NextToken();
			if (this.token.id == ExpressionParser.TokenId.Question)
			{
				if (!type.IsValueType || ExpressionParser.IsNullableType(type))
				{
					throw this.ParseError(pos, "Type '{0}' has no nullable form", new object[]
					{
						ExpressionParser.GetTypeName(type)
					});
				}
				type = typeof(Nullable<>).MakeGenericType(new Type[]
				{
					type
				});
				this.NextToken();
			}
			if (this.token.id != ExpressionParser.TokenId.OpenParen)
			{
				this.ValidateToken(ExpressionParser.TokenId.Dot, "'.' or '(' expected");
				this.NextToken();
				return this.ParseMemberAccess(type, null);
			}
			Expression[] array = this.ParseArgumentList();
			MethodBase methodBase;
			switch (this.FindBestMethod(type.GetConstructors(), array, out methodBase))
			{
			case 0:
				if (array.Length == 1)
				{
					return this.GenerateConversion(array[0], type, pos);
				}
				throw this.ParseError(pos, "No matching constructor in type '{0}'", new object[]
				{
					ExpressionParser.GetTypeName(type)
				});
			case 1:
				return Expression.New((ConstructorInfo)methodBase, array);
			default:
				throw this.ParseError(pos, "Ambiguous invocation of '{0}' constructor", new object[]
				{
					ExpressionParser.GetTypeName(type)
				});
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000116D0 File Offset: 0x0000F8D0
		private Expression GenerateConversion(Expression expr, Type type, int errorPos)
		{
			Type type2 = expr.Type;
			if (type2 == type)
			{
				return expr;
			}
			if (type2.IsValueType && type.IsValueType)
			{
				if ((ExpressionParser.IsNullableType(type2) || ExpressionParser.IsNullableType(type)) && ExpressionParser.GetNonNullableType(type2) == ExpressionParser.GetNonNullableType(type))
				{
					return Expression.Convert(expr, type);
				}
				if (((ExpressionParser.IsNumericType(type2) || ExpressionParser.IsEnumType(type2)) && ExpressionParser.IsNumericType(type)) || ExpressionParser.IsEnumType(type))
				{
					return Expression.ConvertChecked(expr, type);
				}
			}
			if (type2.IsAssignableFrom(type) || type.IsAssignableFrom(type2) || type2.IsInterface || type.IsInterface)
			{
				return Expression.Convert(expr, type);
			}
			throw this.ParseError(errorPos, "A value of type '{0}' cannot be converted to type '{1}'", new object[]
			{
				ExpressionParser.GetTypeName(type2),
				ExpressionParser.GetTypeName(type)
			});
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000117A4 File Offset: 0x0000F9A4
		private Expression ParseMemberAccess(Type type, Expression instance)
		{
			if (instance != null)
			{
				type = instance.Type;
			}
			int pos = this.token.pos;
			string identifier = this.GetIdentifier();
			this.NextToken();
			if (this.token.id == ExpressionParser.TokenId.OpenParen)
			{
				if (instance != null && type != typeof(string))
				{
					Type type2 = ExpressionParser.FindGenericType(typeof(IEnumerable<>), type);
					if (type2 != null)
					{
						Type elementType = type2.GetGenericArguments()[0];
						return this.ParseAggregate(instance, elementType, identifier, pos);
					}
				}
				Expression[] array = this.ParseArgumentList();
				MethodBase methodBase;
				switch (this.FindMethod(type, identifier, instance == null, array, out methodBase))
				{
				case 0:
					throw this.ParseError(pos, "No applicable method '{0}' exists in type '{1}'", new object[]
					{
						identifier,
						ExpressionParser.GetTypeName(type)
					});
				case 1:
				{
					MethodInfo methodInfo = (MethodInfo)methodBase;
					if (methodInfo.ReturnType == typeof(void))
					{
						throw this.ParseError(pos, "Method '{0}' in type '{1}' does not return a value", new object[]
						{
							identifier,
							ExpressionParser.GetTypeName(methodInfo.DeclaringType)
						});
					}
					return Expression.Call(instance, methodInfo, array);
				}
				default:
					throw this.ParseError(pos, "Ambiguous invocation of method '{0}' in type '{1}'", new object[]
					{
						identifier,
						ExpressionParser.GetTypeName(type)
					});
				}
			}
			else
			{
				MemberInfo memberInfo = this.FindPropertyOrField(type, identifier, instance == null);
				if (memberInfo == null)
				{
					throw this.ParseError(pos, "No property or field '{0}' exists in type '{1}'", new object[]
					{
						identifier,
						ExpressionParser.GetTypeName(type)
					});
				}
				if (!(memberInfo is PropertyInfo))
				{
					return Expression.Field(instance, (FieldInfo)memberInfo);
				}
				return Expression.Property(instance, (PropertyInfo)memberInfo);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001195C File Offset: 0x0000FB5C
		private static Type FindGenericType(Type generic, Type type)
		{
			while (type != null && type != typeof(object))
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == generic)
				{
					return type;
				}
				if (generic.IsInterface)
				{
					foreach (Type type2 in type.GetInterfaces())
					{
						Type type3 = ExpressionParser.FindGenericType(generic, type2);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000119E4 File Offset: 0x0000FBE4
		private Expression ParseAggregate(Expression instance, Type elementType, string methodName, int errorPos)
		{
			ParameterExpression parameterExpression = this.it;
			ParameterExpression parameterExpression2 = Expression.Parameter(elementType, "");
			this.it = parameterExpression2;
			Expression[] array = this.ParseArgumentList();
			this.it = parameterExpression;
			MethodBase methodBase;
			if (this.FindMethod(typeof(ExpressionParser.IEnumerableSignatures), methodName, false, array, out methodBase) != 1)
			{
				throw this.ParseError(errorPos, "No applicable aggregate method '{0}' exists", new object[]
				{
					methodName
				});
			}
			Type[] typeArguments;
			if (methodBase.Name == "Min" || methodBase.Name == "Max")
			{
				typeArguments = new Type[]
				{
					elementType,
					array[0].Type
				};
			}
			else
			{
				typeArguments = new Type[]
				{
					elementType
				};
			}
			if (array.Length == 0)
			{
				array = new Expression[]
				{
					instance
				};
			}
			else
			{
				array = new Expression[]
				{
					instance,
					Expression.Lambda(array[0], new ParameterExpression[]
					{
						parameterExpression2
					})
				};
			}
			return Expression.Call(typeof(Enumerable), methodBase.Name, typeArguments, array);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00011AFC File Offset: 0x0000FCFC
		private Expression[] ParseArgumentList()
		{
			this.ValidateToken(ExpressionParser.TokenId.OpenParen, "'(' expected");
			this.NextToken();
			Expression[] result = (this.token.id != ExpressionParser.TokenId.CloseParen) ? this.ParseArguments() : new Expression[0];
			this.ValidateToken(ExpressionParser.TokenId.CloseParen, "')' or ',' expected");
			this.NextToken();
			return result;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00011B50 File Offset: 0x0000FD50
		private Expression[] ParseArguments()
		{
			List<Expression> list = new List<Expression>();
			for (;;)
			{
				list.Add(this.ParseExpression());
				if (this.token.id != ExpressionParser.TokenId.Comma)
				{
					break;
				}
				this.NextToken();
			}
			return list.ToArray();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00011B8C File Offset: 0x0000FD8C
		private Expression ParseElementAccess(Expression expr)
		{
			int pos = this.token.pos;
			this.ValidateToken(ExpressionParser.TokenId.OpenBracket, "'(' expected");
			this.NextToken();
			Expression[] array = this.ParseArguments();
			this.ValidateToken(ExpressionParser.TokenId.CloseBracket, "']' or ',' expected");
			this.NextToken();
			if (expr.Type.IsArray)
			{
				if (expr.Type.GetArrayRank() != 1 || array.Length != 1)
				{
					throw this.ParseError(pos, "Indexing of multi-dimensional arrays is not supported", new object[0]);
				}
				Expression expression = this.PromoteExpression(array[0], typeof(int), true);
				if (expression == null)
				{
					throw this.ParseError(pos, "Array index must be an integer expression", new object[0]);
				}
				return Expression.ArrayIndex(expr, expression);
			}
			else
			{
				MethodBase methodBase;
				switch (this.FindIndexer(expr.Type, array, out methodBase))
				{
				case 0:
					throw this.ParseError(pos, "No applicable indexer exists in type '{0}'", new object[]
					{
						ExpressionParser.GetTypeName(expr.Type)
					});
				case 1:
					return Expression.Call(expr, (MethodInfo)methodBase, array);
				default:
					throw this.ParseError(pos, "Ambiguous invocation of indexer in type '{0}'", new object[]
					{
						ExpressionParser.GetTypeName(expr.Type)
					});
				}
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		private static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00011CD5 File Offset: 0x0000FED5
		private static Type GetNonNullableType(Type type)
		{
			if (!ExpressionParser.IsNullableType(type))
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00011CEC File Offset: 0x0000FEEC
		private static string GetTypeName(Type type)
		{
			Type nonNullableType = ExpressionParser.GetNonNullableType(type);
			string text = nonNullableType.Name;
			if (type != nonNullableType)
			{
				text += '?';
			}
			return text;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00011D1F File Offset: 0x0000FF1F
		private static bool IsNumericType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) != 0;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00011D2D File Offset: 0x0000FF2D
		private static bool IsSignedIntegralType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) == 2;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00011D38 File Offset: 0x0000FF38
		private static bool IsUnsignedIntegralType(Type type)
		{
			return ExpressionParser.GetNumericTypeKind(type) == 3;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00011D44 File Offset: 0x0000FF44
		private static int GetNumericTypeKind(Type type)
		{
			type = ExpressionParser.GetNonNullableType(type);
			if (type.IsEnum)
			{
				return 0;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Char:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return 1;
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				return 2;
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00011DAB File Offset: 0x0000FFAB
		private static bool IsEnumType(Type type)
		{
			return ExpressionParser.GetNonNullableType(type).IsEnum;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		private void CheckAndPromoteOperand(Type signatures, string opName, ref Expression expr, int errorPos)
		{
			Expression[] array = new Expression[]
			{
				expr
			};
			MethodBase methodBase;
			if (this.FindMethod(signatures, "F", false, array, out methodBase) != 1)
			{
				throw this.ParseError(errorPos, "Operator '{0}' incompatible with operand type '{1}'", new object[]
				{
					opName,
					ExpressionParser.GetTypeName(array[0].Type)
				});
			}
			expr = array[0];
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00011E18 File Offset: 0x00010018
		private void CheckAndPromoteOperands(Type signatures, string opName, ref Expression left, ref Expression right, int errorPos)
		{
			Expression[] array = new Expression[]
			{
				left,
				right
			};
			MethodBase methodBase;
			if (this.FindMethod(signatures, "F", false, array, out methodBase) != 1)
			{
				throw this.IncompatibleOperandsError(opName, left, right, errorPos);
			}
			left = array[0];
			right = array[1];
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00011E68 File Offset: 0x00010068
		private Exception IncompatibleOperandsError(string opName, Expression left, Expression right, int pos)
		{
			return this.ParseError(pos, "Operator '{0}' incompatible with operand types '{1}' and '{2}'", new object[]
			{
				opName,
				ExpressionParser.GetTypeName(left.Type),
				ExpressionParser.GetTypeName(right.Type)
			});
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00011EAC File Offset: 0x000100AC
		private MemberInfo FindPropertyOrField(Type type, string memberName, bool staticAccess)
		{
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
			foreach (Type type2 in ExpressionParser.SelfAndBaseTypes(type))
			{
				MemberInfo[] array = type2.FindMembers(MemberTypes.Field | MemberTypes.Property, bindingAttr, Type.FilterNameIgnoreCase, memberName);
				if (array.Length != 0)
				{
					return array[0];
				}
			}
			return null;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00011F20 File Offset: 0x00010120
		private int FindMethod(Type type, string methodName, bool staticAccess, Expression[] args, out MethodBase method)
		{
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | (staticAccess ? BindingFlags.Static : BindingFlags.Instance);
			foreach (Type type2 in ExpressionParser.SelfAndBaseTypes(type))
			{
				MemberInfo[] source = type2.FindMembers(MemberTypes.Method, bindingAttr, Type.FilterNameIgnoreCase, methodName);
				int num = this.FindBestMethod(source.Cast<MethodBase>(), args, out method);
				if (num != 0)
				{
					return num;
				}
			}
			method = null;
			return 0;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00011FBC File Offset: 0x000101BC
		private int FindIndexer(Type type, Expression[] args, out MethodBase method)
		{
			foreach (Type type2 in ExpressionParser.SelfAndBaseTypes(type))
			{
				MemberInfo[] defaultMembers = type2.GetDefaultMembers();
				if (defaultMembers.Length != 0)
				{
					IEnumerable<MethodBase> methods = from p in defaultMembers.OfType<PropertyInfo>()
					select p.GetGetMethod() into m
					where m != null
					select m;
					int num = this.FindBestMethod(methods, args, out method);
					if (num != 0)
					{
						return num;
					}
				}
			}
			method = null;
			return 0;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00012078 File Offset: 0x00010278
		private static IEnumerable<Type> SelfAndBaseTypes(Type type)
		{
			if (type.IsInterface)
			{
				List<Type> list = new List<Type>();
				ExpressionParser.AddInterface(list, type);
				return list;
			}
			return ExpressionParser.SelfAndBaseClasses(type);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001218C File Offset: 0x0001038C
		private static IEnumerable<Type> SelfAndBaseClasses(Type type)
		{
			while (type != null)
			{
				yield return type;
				type = type.BaseType;
			}
			yield break;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000121AC File Offset: 0x000103AC
		private static void AddInterface(List<Type> types, Type type)
		{
			if (!types.Contains(type))
			{
				types.Add(type);
				foreach (Type type2 in type.GetInterfaces())
				{
					ExpressionParser.AddInterface(types, type2);
				}
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00012294 File Offset: 0x00010494
		private int FindBestMethod(IEnumerable<MethodBase> methods, Expression[] args, out MethodBase method)
		{
			ExpressionParser.MethodData[] applicable = (from m in methods
			select new ExpressionParser.MethodData
			{
				MethodBase = m,
				Parameters = m.GetParameters()
			} into m
			where this.IsApplicable(m, args)
			select m).ToArray<ExpressionParser.MethodData>();
			if (applicable.Length > 1)
			{
				applicable = (from m in applicable
				where applicable.All((ExpressionParser.MethodData n) => m == n || ExpressionParser.IsBetterThan(args, m, n))
				select m).ToArray<ExpressionParser.MethodData>();
			}
			if (applicable.Length == 1)
			{
				ExpressionParser.MethodData methodData = applicable[0];
				for (int i = 0; i < args.Length; i++)
				{
					args[i] = methodData.Args[i];
				}
				method = methodData.MethodBase;
			}
			else
			{
				method = null;
			}
			return applicable.Length;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00012374 File Offset: 0x00010574
		private bool IsApplicable(ExpressionParser.MethodData method, Expression[] args)
		{
			if (method.Parameters.Length != args.Length)
			{
				return false;
			}
			Expression[] array = new Expression[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				ParameterInfo parameterInfo = method.Parameters[i];
				if (parameterInfo.IsOut)
				{
					return false;
				}
				Expression expression = this.PromoteExpression(args[i], parameterInfo.ParameterType, false);
				if (expression == null)
				{
					return false;
				}
				array[i] = expression;
			}
			method.Args = array;
			return true;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000123DC File Offset: 0x000105DC
		private Expression PromoteExpression(Expression expr, Type type, bool exact)
		{
			if (expr.Type == type)
			{
				return expr;
			}
			if (expr is ConstantExpression)
			{
				ConstantExpression constantExpression = (ConstantExpression)expr;
				string name;
				if (constantExpression == ExpressionParser.nullLiteral)
				{
					if (!type.IsValueType || ExpressionParser.IsNullableType(type))
					{
						return Expression.Constant(null, type);
					}
				}
				else if (this.literals.TryGetValue(constantExpression, out name))
				{
					Type nonNullableType = ExpressionParser.GetNonNullableType(type);
					object obj = null;
					switch (Type.GetTypeCode(constantExpression.Type))
					{
					case TypeCode.Int32:
					case TypeCode.UInt32:
					case TypeCode.Int64:
					case TypeCode.UInt64:
						obj = ExpressionParser.ParseNumber(name, nonNullableType);
						break;
					case TypeCode.Double:
						if (nonNullableType == typeof(decimal))
						{
							obj = ExpressionParser.ParseNumber(name, nonNullableType);
						}
						break;
					case TypeCode.String:
						obj = ExpressionParser.ParseEnum(name, nonNullableType);
						break;
					}
					if (obj != null)
					{
						return Expression.Constant(obj, type);
					}
				}
			}
			if (!ExpressionParser.IsCompatibleWith(expr.Type, type))
			{
				return null;
			}
			if (type.IsValueType || exact)
			{
				return Expression.Convert(expr, type);
			}
			return expr;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000124E8 File Offset: 0x000106E8
		private static object ParseNumber(string text, Type type)
		{
			switch (Type.GetTypeCode(ExpressionParser.GetNonNullableType(type)))
			{
			case TypeCode.SByte:
			{
				sbyte b;
				if (sbyte.TryParse(text, out b))
				{
					return b;
				}
				break;
			}
			case TypeCode.Byte:
			{
				byte b2;
				if (byte.TryParse(text, out b2))
				{
					return b2;
				}
				break;
			}
			case TypeCode.Int16:
			{
				short num;
				if (short.TryParse(text, out num))
				{
					return num;
				}
				break;
			}
			case TypeCode.UInt16:
			{
				ushort num2;
				if (ushort.TryParse(text, out num2))
				{
					return num2;
				}
				break;
			}
			case TypeCode.Int32:
			{
				int num3;
				if (int.TryParse(text, out num3))
				{
					return num3;
				}
				break;
			}
			case TypeCode.UInt32:
			{
				uint num4;
				if (uint.TryParse(text, out num4))
				{
					return num4;
				}
				break;
			}
			case TypeCode.Int64:
			{
				long num5;
				if (long.TryParse(text, out num5))
				{
					return num5;
				}
				break;
			}
			case TypeCode.UInt64:
			{
				ulong num6;
				if (ulong.TryParse(text, out num6))
				{
					return num6;
				}
				break;
			}
			case TypeCode.Single:
			{
				float num7;
				if (float.TryParse(text, out num7))
				{
					return num7;
				}
				break;
			}
			case TypeCode.Double:
			{
				double num8;
				if (double.TryParse(text, out num8))
				{
					return num8;
				}
				break;
			}
			case TypeCode.Decimal:
			{
				decimal num9;
				if (decimal.TryParse(text, out num9))
				{
					return num9;
				}
				break;
			}
			}
			return null;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001260C File Offset: 0x0001080C
		private static object ParseEnum(string name, Type type)
		{
			if (type.IsEnum)
			{
				MemberInfo[] array = type.FindMembers(MemberTypes.Field, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public, Type.FilterNameIgnoreCase, name);
				if (array.Length != 0)
				{
					return ((FieldInfo)array[0]).GetValue(null);
				}
			}
			return null;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00012648 File Offset: 0x00010848
		private static bool IsCompatibleWith(Type source, Type target)
		{
			if (source == target)
			{
				return true;
			}
			if (!target.IsValueType)
			{
				return target.IsAssignableFrom(source);
			}
			Type nonNullableType = ExpressionParser.GetNonNullableType(source);
			Type nonNullableType2 = ExpressionParser.GetNonNullableType(target);
			if (nonNullableType != source && nonNullableType2 == target)
			{
				return false;
			}
			TypeCode typeCode = nonNullableType.IsEnum ? TypeCode.Object : Type.GetTypeCode(nonNullableType);
			TypeCode typeCode2 = nonNullableType2.IsEnum ? TypeCode.Object : Type.GetTypeCode(nonNullableType2);
			switch (typeCode)
			{
			case TypeCode.SByte:
				switch (typeCode2)
				{
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Byte:
				switch (typeCode2)
				{
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt16:
				switch (typeCode2)
				{
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Int32:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt32:
				switch (typeCode2)
				{
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Int64:
				switch (typeCode2)
				{
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt64:
				switch (typeCode2)
				{
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Single:
				switch (typeCode2)
				{
				case TypeCode.Single:
				case TypeCode.Double:
					return true;
				}
				break;
			default:
				if (nonNullableType == nonNullableType2)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00012894 File Offset: 0x00010A94
		private static bool IsBetterThan(Expression[] args, ExpressionParser.MethodData m1, ExpressionParser.MethodData m2)
		{
			bool result = false;
			for (int i = 0; i < args.Length; i++)
			{
				int num = ExpressionParser.CompareConversions(args[i].Type, m1.Parameters[i].ParameterType, m2.Parameters[i].ParameterType);
				if (num < 0)
				{
					return false;
				}
				if (num > 0)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000128E8 File Offset: 0x00010AE8
		private static int CompareConversions(Type s, Type t1, Type t2)
		{
			if (t1 == t2)
			{
				return 0;
			}
			if (s == t1)
			{
				return 1;
			}
			if (s == t2)
			{
				return -1;
			}
			bool flag = ExpressionParser.IsCompatibleWith(t1, t2);
			bool flag2 = ExpressionParser.IsCompatibleWith(t2, t1);
			if (flag && !flag2)
			{
				return 1;
			}
			if (flag2 && !flag)
			{
				return -1;
			}
			if (ExpressionParser.IsSignedIntegralType(t1) && ExpressionParser.IsUnsignedIntegralType(t2))
			{
				return 1;
			}
			if (ExpressionParser.IsSignedIntegralType(t2) && ExpressionParser.IsUnsignedIntegralType(t1))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001295B File Offset: 0x00010B5B
		private Expression GenerateEqual(Expression left, Expression right)
		{
			return Expression.Equal(left, right);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00012964 File Offset: 0x00010B64
		private Expression GenerateNotEqual(Expression left, Expression right)
		{
			return Expression.NotEqual(left, right);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001296D File Offset: 0x00010B6D
		private Expression GenerateGreaterThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.GreaterThan(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.GreaterThan(left, right);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000129AB File Offset: 0x00010BAB
		private Expression GenerateGreaterThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.GreaterThanOrEqual(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.GreaterThanOrEqual(left, right);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000129E9 File Offset: 0x00010BE9
		private Expression GenerateLessThan(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.LessThan(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.LessThan(left, right);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00012A27 File Offset: 0x00010C27
		private Expression GenerateLessThanEqual(Expression left, Expression right)
		{
			if (left.Type == typeof(string))
			{
				return Expression.LessThanOrEqual(this.GenerateStaticMethodCall("Compare", left, right), Expression.Constant(0));
			}
			return Expression.LessThanOrEqual(left, right);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00012A68 File Offset: 0x00010C68
		private Expression GenerateAdd(Expression left, Expression right)
		{
			if (left.Type == typeof(string) && right.Type == typeof(string))
			{
				return this.GenerateStaticMethodCall("Concat", left, right);
			}
			return Expression.Add(left, right);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00012AB8 File Offset: 0x00010CB8
		private Expression GenerateSubtract(Expression left, Expression right)
		{
			return Expression.Subtract(left, right);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00012AC4 File Offset: 0x00010CC4
		private Expression GenerateStringConcat(Expression left, Expression right)
		{
			return Expression.Call(null, typeof(string).GetMethod("Concat", new Type[]
			{
				typeof(object),
				typeof(object)
			}), new Expression[]
			{
				left,
				right
			});
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00012B20 File Offset: 0x00010D20
		private MethodInfo GetStaticMethod(string methodName, Expression left, Expression right)
		{
			return left.Type.GetMethod(methodName, new Type[]
			{
				left.Type,
				right.Type
			});
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00012B54 File Offset: 0x00010D54
		private Expression GenerateStaticMethodCall(string methodName, Expression left, Expression right)
		{
			return Expression.Call(null, this.GetStaticMethod(methodName, left, right), new Expression[]
			{
				left,
				right
			});
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00012B80 File Offset: 0x00010D80
		private void SetTextPos(int pos)
		{
			this.textPos = pos;
			this.ch = ((this.textPos < this.textLen) ? this.text[this.textPos] : '\0');
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00012BB4 File Offset: 0x00010DB4
		private void NextChar()
		{
			if (this.textPos < this.textLen)
			{
				this.textPos++;
			}
			this.ch = ((this.textPos < this.textLen) ? this.text[this.textPos] : '\0');
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00012C08 File Offset: 0x00010E08
		private void NextToken()
		{
			while (char.IsWhiteSpace(this.ch))
			{
				this.NextChar();
			}
			int num = this.textPos;
			char c = this.ch;
			ExpressionParser.TokenId id;
			switch (c)
			{
			case '!':
				this.NextChar();
				if (this.ch == '=')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.ExclamationEqual;
					goto IL_41E;
				}
				id = ExpressionParser.TokenId.Exclamation;
				goto IL_41E;
			case '"':
			case '\'':
			{
				char c2 = this.ch;
				for (;;)
				{
					this.NextChar();
					while (this.textPos < this.textLen && this.ch != c2)
					{
						this.NextChar();
					}
					if (this.textPos == this.textLen)
					{
						break;
					}
					this.NextChar();
					if (this.ch != c2)
					{
						goto Block_14;
					}
				}
				throw this.ParseError(this.textPos, "Unterminated string literal", new object[0]);
				Block_14:
				id = ExpressionParser.TokenId.StringLiteral;
				goto IL_41E;
			}
			case '#':
			case '$':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case ';':
				break;
			case '%':
				this.NextChar();
				id = ExpressionParser.TokenId.Percent;
				goto IL_41E;
			case '&':
				this.NextChar();
				if (this.ch == '&')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.DoubleAmphersand;
					goto IL_41E;
				}
				id = ExpressionParser.TokenId.Amphersand;
				goto IL_41E;
			case '(':
				this.NextChar();
				id = ExpressionParser.TokenId.OpenParen;
				goto IL_41E;
			case ')':
				this.NextChar();
				id = ExpressionParser.TokenId.CloseParen;
				goto IL_41E;
			case '*':
				this.NextChar();
				id = ExpressionParser.TokenId.Asterisk;
				goto IL_41E;
			case '+':
				this.NextChar();
				id = ExpressionParser.TokenId.Plus;
				goto IL_41E;
			case ',':
				this.NextChar();
				id = ExpressionParser.TokenId.Comma;
				goto IL_41E;
			case '-':
				this.NextChar();
				id = ExpressionParser.TokenId.Minus;
				goto IL_41E;
			case '.':
				this.NextChar();
				id = ExpressionParser.TokenId.Dot;
				goto IL_41E;
			case '/':
				this.NextChar();
				id = ExpressionParser.TokenId.Slash;
				goto IL_41E;
			case ':':
				this.NextChar();
				id = ExpressionParser.TokenId.Colon;
				goto IL_41E;
			case '<':
				this.NextChar();
				if (this.ch == '=')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.LessThanEqual;
					goto IL_41E;
				}
				if (this.ch == '>')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.LessGreater;
					goto IL_41E;
				}
				id = ExpressionParser.TokenId.LessThan;
				goto IL_41E;
			case '=':
				this.NextChar();
				if (this.ch == '=')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.DoubleEqual;
					goto IL_41E;
				}
				id = ExpressionParser.TokenId.Equal;
				goto IL_41E;
			case '>':
				this.NextChar();
				if (this.ch == '=')
				{
					this.NextChar();
					id = ExpressionParser.TokenId.GreaterThanEqual;
					goto IL_41E;
				}
				id = ExpressionParser.TokenId.GreaterThan;
				goto IL_41E;
			case '?':
				this.NextChar();
				id = ExpressionParser.TokenId.Question;
				goto IL_41E;
			default:
				switch (c)
				{
				case '[':
					this.NextChar();
					id = ExpressionParser.TokenId.OpenBracket;
					goto IL_41E;
				case '\\':
					break;
				case ']':
					this.NextChar();
					id = ExpressionParser.TokenId.CloseBracket;
					goto IL_41E;
				default:
					if (c == '|')
					{
						this.NextChar();
						if (this.ch == '|')
						{
							this.NextChar();
							id = ExpressionParser.TokenId.DoubleBar;
							goto IL_41E;
						}
						id = ExpressionParser.TokenId.Bar;
						goto IL_41E;
					}
					break;
				}
				break;
			}
			if (char.IsLetter(this.ch) || this.ch == '@' || this.ch == '_')
			{
				do
				{
					this.NextChar();
				}
				while (char.IsLetterOrDigit(this.ch) || this.ch == '_');
				id = ExpressionParser.TokenId.Identifier;
			}
			else if (char.IsDigit(this.ch))
			{
				id = ExpressionParser.TokenId.IntegerLiteral;
				do
				{
					this.NextChar();
				}
				while (char.IsDigit(this.ch));
				if (this.ch == '.')
				{
					id = ExpressionParser.TokenId.RealLiteral;
					this.NextChar();
					this.ValidateDigit();
					do
					{
						this.NextChar();
					}
					while (char.IsDigit(this.ch));
				}
				if (this.ch == 'E' || this.ch == 'e')
				{
					id = ExpressionParser.TokenId.RealLiteral;
					this.NextChar();
					if (this.ch == '+' || this.ch == '-')
					{
						this.NextChar();
					}
					this.ValidateDigit();
					do
					{
						this.NextChar();
					}
					while (char.IsDigit(this.ch));
				}
				if (this.ch == 'F' || this.ch == 'f')
				{
					this.NextChar();
				}
			}
			else
			{
				if (this.textPos != this.textLen)
				{
					throw this.ParseError(this.textPos, "Syntax error '{0}'", new object[]
					{
						this.ch
					});
				}
				id = ExpressionParser.TokenId.End;
			}
			IL_41E:
			this.token.id = id;
			this.token.text = this.text.Substring(num, this.textPos - num);
			this.token.pos = num;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001306A File Offset: 0x0001126A
		private bool TokenIdentifierIs(string id)
		{
			return this.token.id == ExpressionParser.TokenId.Identifier && string.Equals(id, this.token.text, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00013090 File Offset: 0x00011290
		private string GetIdentifier()
		{
			this.ValidateToken(ExpressionParser.TokenId.Identifier, "Identifier expected");
			string text = this.token.text;
			if (text.Length > 1 && text[0] == '@')
			{
				text = text.Substring(1);
			}
			return text;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000130D2 File Offset: 0x000112D2
		private void ValidateDigit()
		{
			if (!char.IsDigit(this.ch))
			{
				throw this.ParseError(this.textPos, "Digit expected", new object[0]);
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000130F9 File Offset: 0x000112F9
		private void ValidateToken(ExpressionParser.TokenId t, string errorMessage)
		{
			if (this.token.id != t)
			{
				throw this.ParseError(errorMessage, new object[0]);
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00013117 File Offset: 0x00011317
		private void ValidateToken(ExpressionParser.TokenId t)
		{
			if (this.token.id != t)
			{
				throw this.ParseError("Syntax error", new object[0]);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00013139 File Offset: 0x00011339
		private Exception ParseError(string format, params object[] args)
		{
			return this.ParseError(this.token.pos, format, args);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001314E File Offset: 0x0001134E
		private Exception ParseError(int pos, string format, params object[] args)
		{
			return new ParseException(string.Format(CultureInfo.CurrentCulture, format, args), pos);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00013180 File Offset: 0x00011380
		private static Dictionary<string, object> CreateKeywords(Type[] servicePredefinedTypes)
		{
			Dictionary<string, object> d = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			d.Add("true", ExpressionParser.trueLiteral);
			d.Add("false", ExpressionParser.falseLiteral);
			d.Add("null", ExpressionParser.nullLiteral);
			d.Add(ExpressionParser.keywordIt, ExpressionParser.keywordIt);
			d.Add(ExpressionParser.keywordIif, ExpressionParser.keywordIif);
			d.Add(ExpressionParser.keywordNew, ExpressionParser.keywordNew);
			lock (ExpressionParser.syncRoot)
			{
				foreach (Type type3 in ExpressionParser.predefinedTypes)
				{
					d.Add(type3.Name, type3);
				}
			}
			if (servicePredefinedTypes != null)
			{
				foreach (Type type2 in from type in servicePredefinedTypes
				where !d.ContainsKey(type.Name)
				select type)
				{
					d.Add(type2.Name, type2);
				}
			}
			return d;
		}

		// Token: 0x04000123 RID: 291
		private static List<Type> predefinedTypes = new List<Type>(new Type[]
		{
			typeof(object),
			typeof(bool),
			typeof(char),
			typeof(string),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(EnhancedTimeSpan),
			typeof(Guid),
			typeof(Math),
			typeof(Convert),
			typeof(ADObjectId),
			typeof(RmsTemplateIdentity),
			typeof(DBNull),
			typeof(EnumObject),
			typeof(SecurityIdentifier),
			typeof(SecurityPrincipalIdParameter),
			typeof(Enum),
			typeof(AddressListType),
			typeof(ServerVersion),
			typeof(ServerEditionType),
			typeof(LocalizedString),
			typeof(ServerStatus),
			typeof(ServerRole),
			typeof(EmailAddressPolicyPriority),
			typeof(LocalizedDescriptionAttribute),
			typeof(StringComparison),
			typeof(MailEnabledRecipient),
			typeof(DagNetworkObjectId),
			typeof(SmtpAddress),
			typeof(ICustomFormatter),
			typeof(ICustomTextConverter),
			typeof(BooleanAsStatusCoverter),
			typeof(ObjectExtension),
			typeof(FilterControlHelper),
			typeof(StatusEnum),
			typeof(TextConverter),
			typeof(TypeDescriptor),
			typeof(PropertyDescriptor),
			typeof(X509Certificate2),
			typeof(SmtpDomainWithSubdomains),
			typeof(MultiValuedPropertyBase),
			typeof(WinformsHelper),
			typeof(PublicFolderClientPermissionHelper),
			typeof(SmtpDomain),
			typeof(IList),
			typeof(SharingPolicyAction),
			typeof(DataColumnCollection),
			typeof(DatabaseStatus),
			typeof(HygieneAgent),
			typeof(DataRow),
			typeof(Strings),
			typeof(RequestStatus),
			typeof(GatewayStatus),
			typeof(RecipientType),
			typeof(RecipientTypeDetails),
			typeof(ArchiveState),
			typeof(ExchangeObjectVersion),
			typeof(ArchiveType),
			typeof(MailboxMoveType),
			typeof(PublicFolderSettingsManageType),
			typeof(ClientPermissionUpdateType)
		});

		// Token: 0x04000124 RID: 292
		private static readonly object syncRoot = new object();

		// Token: 0x04000125 RID: 293
		private static readonly Expression trueLiteral = Expression.Constant(true);

		// Token: 0x04000126 RID: 294
		private static readonly Expression falseLiteral = Expression.Constant(false);

		// Token: 0x04000127 RID: 295
		private static readonly Expression nullLiteral = Expression.Constant(null);

		// Token: 0x04000128 RID: 296
		private static readonly string keywordIt = "it";

		// Token: 0x04000129 RID: 297
		private static readonly string keywordIif = "iif";

		// Token: 0x0400012A RID: 298
		private static readonly string keywordNew = "new";

		// Token: 0x0400012B RID: 299
		private Dictionary<string, object> keywords;

		// Token: 0x0400012C RID: 300
		private Dictionary<string, object> symbols;

		// Token: 0x0400012D RID: 301
		private IDictionary<string, object> externals;

		// Token: 0x0400012E RID: 302
		private Dictionary<Expression, string> literals;

		// Token: 0x0400012F RID: 303
		private ParameterExpression it;

		// Token: 0x04000130 RID: 304
		private string text;

		// Token: 0x04000131 RID: 305
		private int textPos;

		// Token: 0x04000132 RID: 306
		private int textLen;

		// Token: 0x04000133 RID: 307
		private char ch;

		// Token: 0x04000134 RID: 308
		private ExpressionParser.Token token;

		// Token: 0x02000083 RID: 131
		private struct Token
		{
			// Token: 0x04000138 RID: 312
			public ExpressionParser.TokenId id;

			// Token: 0x04000139 RID: 313
			public string text;

			// Token: 0x0400013A RID: 314
			public int pos;
		}

		// Token: 0x02000084 RID: 132
		private enum TokenId
		{
			// Token: 0x0400013C RID: 316
			Unknown,
			// Token: 0x0400013D RID: 317
			End,
			// Token: 0x0400013E RID: 318
			Identifier,
			// Token: 0x0400013F RID: 319
			StringLiteral,
			// Token: 0x04000140 RID: 320
			IntegerLiteral,
			// Token: 0x04000141 RID: 321
			RealLiteral,
			// Token: 0x04000142 RID: 322
			Exclamation,
			// Token: 0x04000143 RID: 323
			Percent,
			// Token: 0x04000144 RID: 324
			Amphersand,
			// Token: 0x04000145 RID: 325
			OpenParen,
			// Token: 0x04000146 RID: 326
			CloseParen,
			// Token: 0x04000147 RID: 327
			Asterisk,
			// Token: 0x04000148 RID: 328
			Plus,
			// Token: 0x04000149 RID: 329
			Comma,
			// Token: 0x0400014A RID: 330
			Minus,
			// Token: 0x0400014B RID: 331
			Dot,
			// Token: 0x0400014C RID: 332
			Slash,
			// Token: 0x0400014D RID: 333
			Colon,
			// Token: 0x0400014E RID: 334
			LessThan,
			// Token: 0x0400014F RID: 335
			Equal,
			// Token: 0x04000150 RID: 336
			GreaterThan,
			// Token: 0x04000151 RID: 337
			Question,
			// Token: 0x04000152 RID: 338
			OpenBracket,
			// Token: 0x04000153 RID: 339
			CloseBracket,
			// Token: 0x04000154 RID: 340
			Bar,
			// Token: 0x04000155 RID: 341
			ExclamationEqual,
			// Token: 0x04000156 RID: 342
			DoubleAmphersand,
			// Token: 0x04000157 RID: 343
			LessThanEqual,
			// Token: 0x04000158 RID: 344
			LessGreater,
			// Token: 0x04000159 RID: 345
			DoubleEqual,
			// Token: 0x0400015A RID: 346
			GreaterThanEqual,
			// Token: 0x0400015B RID: 347
			DoubleBar
		}

		// Token: 0x02000085 RID: 133
		private interface ILogicalSignatures
		{
			// Token: 0x060004B5 RID: 1205
			void F(bool x, bool y);

			// Token: 0x060004B6 RID: 1206
			void F(bool? x, bool? y);
		}

		// Token: 0x02000086 RID: 134
		private interface IArithmeticSignatures
		{
			// Token: 0x060004B7 RID: 1207
			void F(int x, int y);

			// Token: 0x060004B8 RID: 1208
			void F(uint x, uint y);

			// Token: 0x060004B9 RID: 1209
			void F(long x, long y);

			// Token: 0x060004BA RID: 1210
			void F(ulong x, ulong y);

			// Token: 0x060004BB RID: 1211
			void F(float x, float y);

			// Token: 0x060004BC RID: 1212
			void F(double x, double y);

			// Token: 0x060004BD RID: 1213
			void F(decimal x, decimal y);

			// Token: 0x060004BE RID: 1214
			void F(int? x, int? y);

			// Token: 0x060004BF RID: 1215
			void F(uint? x, uint? y);

			// Token: 0x060004C0 RID: 1216
			void F(long? x, long? y);

			// Token: 0x060004C1 RID: 1217
			void F(ulong? x, ulong? y);

			// Token: 0x060004C2 RID: 1218
			void F(float? x, float? y);

			// Token: 0x060004C3 RID: 1219
			void F(double? x, double? y);

			// Token: 0x060004C4 RID: 1220
			void F(decimal? x, decimal? y);
		}

		// Token: 0x02000087 RID: 135
		private interface IRelationalSignatures : ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x060004C5 RID: 1221
			void F(string x, string y);

			// Token: 0x060004C6 RID: 1222
			void F(char x, char y);

			// Token: 0x060004C7 RID: 1223
			void F(DateTime x, DateTime y);

			// Token: 0x060004C8 RID: 1224
			void F(TimeSpan x, TimeSpan y);

			// Token: 0x060004C9 RID: 1225
			void F(char? x, char? y);

			// Token: 0x060004CA RID: 1226
			void F(DateTime? x, DateTime? y);

			// Token: 0x060004CB RID: 1227
			void F(TimeSpan? x, TimeSpan? y);
		}

		// Token: 0x02000088 RID: 136
		private interface IEqualitySignatures : ExpressionParser.IRelationalSignatures, ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x060004CC RID: 1228
			void F(bool x, bool y);

			// Token: 0x060004CD RID: 1229
			void F(bool? x, bool? y);
		}

		// Token: 0x02000089 RID: 137
		private interface IAddSignatures : ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x060004CE RID: 1230
			void F(DateTime x, TimeSpan y);

			// Token: 0x060004CF RID: 1231
			void F(TimeSpan x, TimeSpan y);

			// Token: 0x060004D0 RID: 1232
			void F(DateTime? x, TimeSpan? y);

			// Token: 0x060004D1 RID: 1233
			void F(TimeSpan? x, TimeSpan? y);
		}

		// Token: 0x0200008A RID: 138
		private interface ISubtractSignatures : ExpressionParser.IAddSignatures, ExpressionParser.IArithmeticSignatures
		{
			// Token: 0x060004D2 RID: 1234
			void F(DateTime x, DateTime y);

			// Token: 0x060004D3 RID: 1235
			void F(DateTime? x, DateTime? y);
		}

		// Token: 0x0200008B RID: 139
		private interface INegationSignatures
		{
			// Token: 0x060004D4 RID: 1236
			void F(int x);

			// Token: 0x060004D5 RID: 1237
			void F(long x);

			// Token: 0x060004D6 RID: 1238
			void F(float x);

			// Token: 0x060004D7 RID: 1239
			void F(double x);

			// Token: 0x060004D8 RID: 1240
			void F(decimal x);

			// Token: 0x060004D9 RID: 1241
			void F(int? x);

			// Token: 0x060004DA RID: 1242
			void F(long? x);

			// Token: 0x060004DB RID: 1243
			void F(float? x);

			// Token: 0x060004DC RID: 1244
			void F(double? x);

			// Token: 0x060004DD RID: 1245
			void F(decimal? x);
		}

		// Token: 0x0200008C RID: 140
		private interface INotSignatures
		{
			// Token: 0x060004DE RID: 1246
			void F(bool x);

			// Token: 0x060004DF RID: 1247
			void F(bool? x);
		}

		// Token: 0x0200008D RID: 141
		private interface IEnumerableSignatures
		{
			// Token: 0x060004E0 RID: 1248
			void Where(bool predicate);

			// Token: 0x060004E1 RID: 1249
			void Any();

			// Token: 0x060004E2 RID: 1250
			void Any(bool predicate);

			// Token: 0x060004E3 RID: 1251
			void All(bool predicate);

			// Token: 0x060004E4 RID: 1252
			void Count();

			// Token: 0x060004E5 RID: 1253
			void Count(bool predicate);

			// Token: 0x060004E6 RID: 1254
			void Min(object selector);

			// Token: 0x060004E7 RID: 1255
			void Max(object selector);

			// Token: 0x060004E8 RID: 1256
			void Sum(int selector);

			// Token: 0x060004E9 RID: 1257
			void Sum(int? selector);

			// Token: 0x060004EA RID: 1258
			void Sum(long selector);

			// Token: 0x060004EB RID: 1259
			void Sum(long? selector);

			// Token: 0x060004EC RID: 1260
			void Sum(float selector);

			// Token: 0x060004ED RID: 1261
			void Sum(float? selector);

			// Token: 0x060004EE RID: 1262
			void Sum(double selector);

			// Token: 0x060004EF RID: 1263
			void Sum(double? selector);

			// Token: 0x060004F0 RID: 1264
			void Sum(decimal selector);

			// Token: 0x060004F1 RID: 1265
			void Sum(decimal? selector);

			// Token: 0x060004F2 RID: 1266
			void Average(int selector);

			// Token: 0x060004F3 RID: 1267
			void Average(int? selector);

			// Token: 0x060004F4 RID: 1268
			void Average(long selector);

			// Token: 0x060004F5 RID: 1269
			void Average(long? selector);

			// Token: 0x060004F6 RID: 1270
			void Average(float selector);

			// Token: 0x060004F7 RID: 1271
			void Average(float? selector);

			// Token: 0x060004F8 RID: 1272
			void Average(double selector);

			// Token: 0x060004F9 RID: 1273
			void Average(double? selector);

			// Token: 0x060004FA RID: 1274
			void Average(decimal selector);

			// Token: 0x060004FB RID: 1275
			void Average(decimal? selector);
		}

		// Token: 0x0200008E RID: 142
		private class MethodData
		{
			// Token: 0x0400015C RID: 348
			public MethodBase MethodBase;

			// Token: 0x0400015D RID: 349
			public ParameterInfo[] Parameters;

			// Token: 0x0400015E RID: 350
			public Expression[] Args;
		}
	}
}
