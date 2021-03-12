using System;
using System.Collections.Generic;
using System.Management.Automation;
using mppg;

namespace Microsoft.Exchange.Data.Generated
{
	// Token: 0x02000249 RID: 585
	public class Parser : ShiftReduceParser<LexValue, LexLocation>
	{
		// Token: 0x060013FF RID: 5119 RVA: 0x0003E154 File Offset: 0x0003C354
		protected override void Initialize()
		{
			this.errToken = 1;
			this.eofToken = 2;
			this.states = new State[44];
			base.AddState(0, new State(new int[]
			{
				2,
				3,
				3,
				38,
				9,
				39,
				19,
				42
			}, new int[]
			{
				-1,
				1,
				-3,
				4,
				-4,
				13,
				-6,
				14
			}));
			base.AddState(1, new State(new int[]
			{
				2,
				2
			}));
			base.AddState(2, new State(-1));
			base.AddState(3, new State(-2));
			base.AddState(4, new State(new int[]
			{
				2,
				5,
				12,
				6,
				11,
				8,
				22,
				12
			}, new int[]
			{
				-5,
				10
			}));
			base.AddState(5, new State(-3));
			base.AddState(6, new State(new int[]
			{
				3,
				38,
				9,
				39,
				19,
				42
			}, new int[]
			{
				-3,
				7,
				-4,
				13,
				-6,
				14
			}));
			base.AddState(7, new State(new int[]
			{
				12,
				-5,
				11,
				-5,
				22,
				12,
				2,
				-5,
				10,
				-5
			}, new int[]
			{
				-5,
				10
			}));
			base.AddState(8, new State(new int[]
			{
				3,
				38,
				9,
				39,
				19,
				42
			}, new int[]
			{
				-3,
				9,
				-4,
				13,
				-6,
				14
			}));
			base.AddState(9, new State(new int[]
			{
				12,
				6,
				11,
				-6,
				22,
				12,
				2,
				-6,
				10,
				-6
			}, new int[]
			{
				-5,
				10
			}));
			base.AddState(10, new State(new int[]
			{
				3,
				38,
				9,
				39,
				19,
				42
			}, new int[]
			{
				-3,
				11,
				-4,
				13,
				-6,
				14
			}));
			base.AddState(11, new State(new int[]
			{
				12,
				6,
				11,
				8,
				22,
				12,
				2,
				-9,
				10,
				-9
			}, new int[]
			{
				-5,
				10
			}));
			base.AddState(12, new State(-19));
			base.AddState(13, new State(-4));
			base.AddState(14, new State(new int[]
			{
				13,
				15,
				14,
				22,
				15,
				24,
				16,
				26,
				17,
				28,
				18,
				30,
				20,
				32,
				21,
				34,
				22,
				12
			}, new int[]
			{
				-5,
				36
			}));
			base.AddState(15, new State(new int[]
			{
				7,
				17,
				8,
				18,
				4,
				19,
				5,
				20,
				6,
				21
			}, new int[]
			{
				-7,
				16
			}));
			base.AddState(16, new State(-10));
			base.AddState(17, new State(-20));
			base.AddState(18, new State(-21));
			base.AddState(19, new State(-22));
			base.AddState(20, new State(-23));
			base.AddState(21, new State(-24));
			base.AddState(22, new State(new int[]
			{
				7,
				17,
				8,
				18,
				4,
				19,
				5,
				20,
				6,
				21
			}, new int[]
			{
				-7,
				23
			}));
			base.AddState(23, new State(-11));
			base.AddState(24, new State(new int[]
			{
				7,
				17,
				8,
				18,
				4,
				19,
				5,
				20,
				6,
				21
			}, new int[]
			{
				-7,
				25
			}));
			base.AddState(25, new State(-12));
			base.AddState(26, new State(new int[]
			{
				7,
				17,
				8,
				18,
				4,
				19,
				5,
				20,
				6,
				21
			}, new int[]
			{
				-7,
				27
			}));
			base.AddState(27, new State(-13));
			base.AddState(28, new State(new int[]
			{
				7,
				17,
				8,
				18,
				4,
				19,
				5,
				20,
				6,
				21
			}, new int[]
			{
				-7,
				29
			}));
			base.AddState(29, new State(-14));
			base.AddState(30, new State(new int[]
			{
				7,
				17,
				8,
				18,
				4,
				19,
				5,
				20,
				6,
				21
			}, new int[]
			{
				-7,
				31
			}));
			base.AddState(31, new State(-15));
			base.AddState(32, new State(new int[]
			{
				7,
				17,
				8,
				18,
				4,
				19,
				5,
				20,
				6,
				21
			}, new int[]
			{
				-7,
				33
			}));
			base.AddState(33, new State(-16));
			base.AddState(34, new State(new int[]
			{
				7,
				17,
				8,
				18,
				4,
				19,
				5,
				20,
				6,
				21
			}, new int[]
			{
				-7,
				35
			}));
			base.AddState(35, new State(-17));
			base.AddState(36, new State(new int[]
			{
				7,
				17,
				8,
				18,
				4,
				19,
				5,
				20,
				6,
				21
			}, new int[]
			{
				-7,
				37
			}));
			base.AddState(37, new State(-18));
			base.AddState(38, new State(-25));
			base.AddState(39, new State(new int[]
			{
				3,
				38,
				9,
				39,
				19,
				42
			}, new int[]
			{
				-3,
				40,
				-4,
				13,
				-6,
				14
			}));
			base.AddState(40, new State(new int[]
			{
				10,
				41,
				12,
				6,
				11,
				8,
				22,
				12
			}, new int[]
			{
				-5,
				10
			}));
			base.AddState(41, new State(-7));
			base.AddState(42, new State(new int[]
			{
				3,
				38,
				9,
				39,
				19,
				42
			}, new int[]
			{
				-3,
				43,
				-4,
				13,
				-6,
				14
			}));
			base.AddState(43, new State(new int[]
			{
				12,
				-8,
				11,
				-8,
				22,
				12,
				2,
				-8,
				10,
				-8
			}, new int[]
			{
				-5,
				10
			}));
			this.rules = new Rule[26];
			this.rules[1] = new Rule(-2, new int[]
			{
				-1,
				2
			});
			this.rules[2] = new Rule(-1, new int[]
			{
				2
			});
			this.rules[3] = new Rule(-1, new int[]
			{
				-3,
				2
			});
			this.rules[4] = new Rule(-3, new int[]
			{
				-4
			});
			this.rules[5] = new Rule(-3, new int[]
			{
				-3,
				12,
				-3
			});
			this.rules[6] = new Rule(-3, new int[]
			{
				-3,
				11,
				-3
			});
			this.rules[7] = new Rule(-3, new int[]
			{
				9,
				-3,
				10
			});
			this.rules[8] = new Rule(-3, new int[]
			{
				19,
				-3
			});
			this.rules[9] = new Rule(-3, new int[]
			{
				-3,
				-5,
				-3
			});
			this.rules[10] = new Rule(-4, new int[]
			{
				-6,
				13,
				-7
			});
			this.rules[11] = new Rule(-4, new int[]
			{
				-6,
				14,
				-7
			});
			this.rules[12] = new Rule(-4, new int[]
			{
				-6,
				15,
				-7
			});
			this.rules[13] = new Rule(-4, new int[]
			{
				-6,
				16,
				-7
			});
			this.rules[14] = new Rule(-4, new int[]
			{
				-6,
				17,
				-7
			});
			this.rules[15] = new Rule(-4, new int[]
			{
				-6,
				18,
				-7
			});
			this.rules[16] = new Rule(-4, new int[]
			{
				-6,
				20,
				-7
			});
			this.rules[17] = new Rule(-4, new int[]
			{
				-6,
				21,
				-7
			});
			this.rules[18] = new Rule(-4, new int[]
			{
				-6,
				-5,
				-7
			});
			this.rules[19] = new Rule(-5, new int[]
			{
				22
			});
			this.rules[20] = new Rule(-7, new int[]
			{
				7
			});
			this.rules[21] = new Rule(-7, new int[]
			{
				8
			});
			this.rules[22] = new Rule(-7, new int[]
			{
				4
			});
			this.rules[23] = new Rule(-7, new int[]
			{
				5
			});
			this.rules[24] = new Rule(-7, new int[]
			{
				6
			});
			this.rules[25] = new Rule(-6, new int[]
			{
				3
			});
			this.nonTerminals = new string[]
			{
				"",
				"query",
				"$accept",
				"exp",
				"relexp",
				"opnotsupported",
				"property",
				"value"
			};
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0003EAB8 File Offset: 0x0003CCB8
		protected override void DoAction(int action)
		{
			switch (action)
			{
			case 2:
				this.parseTree = null;
				return;
			case 3:
				this.parseTree = this.value_stack.array[this.value_stack.top - 2].QueryFilter;
				return;
			case 4:
				this.parseTree = this.value_stack.array[this.value_stack.top - 1].QueryFilter;
				return;
			case 5:
				this.yyval.QueryFilter = new AndFilter(new QueryFilter[]
				{
					this.value_stack.array[this.value_stack.top - 3].QueryFilter,
					this.value_stack.array[this.value_stack.top - 1].QueryFilter
				});
				return;
			case 6:
				this.CheckCapability(QueryParser.Capabilities.Or);
				this.yyval.QueryFilter = new OrFilter(new QueryFilter[]
				{
					this.value_stack.array[this.value_stack.top - 3].QueryFilter,
					this.value_stack.array[this.value_stack.top - 1].QueryFilter
				});
				return;
			case 7:
				this.yyval = this.value_stack.array[this.value_stack.top - 2];
				return;
			case 8:
				this.yyval.QueryFilter = new NotFilter(this.value_stack.array[this.value_stack.top - 1].QueryFilter);
				return;
			case 9:
				this.yyval = this.value_stack.array[this.value_stack.top - 2];
				return;
			case 10:
				this.yyval.QueryFilter = this.CreateRelationalExpression(ComparisonOperator.Equal, (string)this.value_stack.array[this.value_stack.top - 3].Value, this.value_stack.array[this.value_stack.top - 1].Value, false);
				return;
			case 11:
				this.yyval.QueryFilter = this.CreateRelationalExpression(ComparisonOperator.NotEqual, (string)this.value_stack.array[this.value_stack.top - 3].Value, this.value_stack.array[this.value_stack.top - 1].Value, false);
				return;
			case 12:
				this.yyval.QueryFilter = this.CreateRelationalExpression(ComparisonOperator.GreaterThan, (string)this.value_stack.array[this.value_stack.top - 3].Value, this.value_stack.array[this.value_stack.top - 1].Value, false);
				return;
			case 13:
				this.yyval.QueryFilter = this.CreateRelationalExpression(ComparisonOperator.LessThan, (string)this.value_stack.array[this.value_stack.top - 3].Value, this.value_stack.array[this.value_stack.top - 1].Value, false);
				return;
			case 14:
				this.yyval.QueryFilter = this.CreateRelationalExpression(ComparisonOperator.GreaterThanOrEqual, (string)this.value_stack.array[this.value_stack.top - 3].Value, this.value_stack.array[this.value_stack.top - 1].Value, false);
				return;
			case 15:
				this.yyval.QueryFilter = this.CreateRelationalExpression(ComparisonOperator.LessThanOrEqual, (string)this.value_stack.array[this.value_stack.top - 3].Value, this.value_stack.array[this.value_stack.top - 1].Value, false);
				return;
			case 16:
				this.CheckCapability(QueryParser.Capabilities.Like);
				this.yyval.QueryFilter = this.CreateRelationalExpression(ComparisonOperator.Like, (string)this.value_stack.array[this.value_stack.top - 3].Value, this.value_stack.array[this.value_stack.top - 1].Value, true);
				return;
			case 17:
				this.CheckCapability(QueryParser.Capabilities.NotLike);
				this.yyval.QueryFilter = new NotFilter(this.CreateRelationalExpression(ComparisonOperator.Equal, (string)this.value_stack.array[this.value_stack.top - 3].Value, this.value_stack.array[this.value_stack.top - 1].Value, true));
				return;
			case 18:
				this.yyval = this.value_stack.array[this.value_stack.top - 2];
				return;
			case 19:
				if (this.value_stack.array[this.value_stack.top - 1].Value != null)
				{
					throw new ParsingInvalidFilterOperator((string)this.value_stack.array[this.value_stack.top - 1].Value, this.query.ToString(), this.yyloc.sCol + 1);
				}
				break;
			case 20:
			{
				string text = (string)this.value_stack.array[this.value_stack.top - 1].Value;
				text = text.Substring(1);
				if (!this.Variables.Contains(text))
				{
					this.Variables.Add(text);
				}
				this.yyval.Value = new Parser.Variable(text);
				return;
			}
			case 21:
			{
				string text2 = (string)this.value_stack.array[this.value_stack.top - 1].Value;
				text2 = text2.Substring(2, text2.Length - 3);
				if (!this.Variables.Contains(text2))
				{
					this.Variables.Add(text2);
				}
				this.yyval.Value = new Parser.Variable(text2);
				return;
			}
			case 22:
			{
				string text3 = ((string)this.value_stack.array[this.value_stack.top - 1].Value).Substring(1, ((string)this.value_stack.array[this.value_stack.top - 1].Value).Length - 2);
				text3 = text3.Replace("''", "'");
				this.yyval.Value = text3;
				return;
			}
			case 23:
			{
				string text4 = ((string)this.value_stack.array[this.value_stack.top - 1].Value).Substring(1, ((string)this.value_stack.array[this.value_stack.top - 1].Value).Length - 2);
				text4 = text4.Replace("`'", "'");
				text4 = text4.Replace("`\"", "\"");
				this.yyval.Value = text4;
				return;
			}
			case 24:
				this.yyval = this.value_stack.array[this.value_stack.top - 1];
				return;
			case 25:
				this.CheckFilterableProperty((string)this.value_stack.array[this.value_stack.top - 1].Value);
				this.yyval = this.value_stack.array[this.value_stack.top - 1];
				break;
			default:
				return;
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0003F2B9 File Offset: 0x0003D4B9
		protected override string TerminalToString(int terminal)
		{
			if (((Tokens)terminal).ToString() != terminal.ToString())
			{
				return ((Tokens)terminal).ToString();
			}
			return base.CharToString((char)terminal);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0003F2E8 File Offset: 0x0003D4E8
		internal Parser(string query, QueryParser.Capabilities capabilities, QueryParser.LookupPropertyDelegate schemaLookupDelegate, QueryParser.ListKnownPropertiesDelegate listKnownPropertiesDelegate, QueryParser.EvaluateVariableDelegate evalDelegate, QueryParser.ConvertValueFromStringDelegate convertDelegate)
		{
			if (query == null)
			{
				throw new ArgumentNullException("query");
			}
			if (convertDelegate == null)
			{
				throw new ArgumentNullException("convertDelegate", DataStrings.StringConversionDelegateNotSet);
			}
			if (schemaLookupDelegate == null)
			{
				throw new ArgumentNullException("schemaLookupDelegate", DataStrings.StringConversionDelegateNotSet);
			}
			this.query = query;
			this.Capabilities = capabilities;
			this.Variables = new HashSet<string>();
			this.schemaLookupDelegate = schemaLookupDelegate;
			this.listKnownPropertiesDelegate = listKnownPropertiesDelegate;
			this.evalDelegate = evalDelegate;
			this.convertDelegate = convertDelegate;
			this.scanner = new Scanner(query);
			base.Parse();
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0003F385 File Offset: 0x0003D585
		public QueryFilter ParseTree
		{
			get
			{
				return this.parseTree;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0003F38D File Offset: 0x0003D58D
		private int CurPos
		{
			get
			{
				return this.yyloc.eCol;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0003F39A File Offset: 0x0003D59A
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x0003F3A2 File Offset: 0x0003D5A2
		private QueryParser.Capabilities Capabilities { get; set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0003F3AB File Offset: 0x0003D5AB
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x0003F3B3 File Offset: 0x0003D5B3
		private ICollection<string> Variables { get; set; }

		// Token: 0x06001409 RID: 5129 RVA: 0x0003F3BC File Offset: 0x0003D5BC
		private void CheckFilterableProperty(string prop)
		{
			if (this.schemaLookupDelegate(prop) != null)
			{
				return;
			}
			if (this.listKnownPropertiesDelegate != null)
			{
				List<string> list = new List<string>();
				foreach (PropertyDefinition propertyDefinition in this.listKnownPropertiesDelegate())
				{
					list.Add(propertyDefinition.Name);
				}
				list.Sort();
				throw new ParsingNonFilterablePropertyWithListException(prop, string.Join(", ", list), this.query, this.CurPos + 1 - (string.IsNullOrEmpty(prop) ? 0 : prop.Length));
			}
			throw new ParsingNonFilterablePropertyException(prop, this.query, this.CurPos + 1 - (string.IsNullOrEmpty(prop) ? 0 : prop.Length));
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0003F494 File Offset: 0x0003D694
		private QueryFilter CreateRelationalExpression(ComparisonOperator op, string prop, object val, bool useWildchars)
		{
			PropertyDefinition propertyDefinition = this.schemaLookupDelegate(prop);
			if (val is Parser.Variable)
			{
				if (this.evalDelegate == null)
				{
					if (!((Parser.Variable)val).Name.Equals("null", StringComparison.OrdinalIgnoreCase))
					{
						throw new ParsingVariablesNotSupported(this.query, this.CurPos + 1);
					}
					val = null;
				}
				else
				{
					val = this.evalDelegate(((Parser.Variable)val).Name);
				}
			}
			if (val == null)
			{
				if (op == ComparisonOperator.Equal)
				{
					return new NotFilter(new ExistsFilter(propertyDefinition));
				}
				return new ExistsFilter(propertyDefinition);
			}
			else
			{
				string text = val as string;
				if (text == "*" || text == "**")
				{
					if (op == ComparisonOperator.Like)
					{
						return new ExistsFilter(propertyDefinition);
					}
					if (op == ComparisonOperator.Equal)
					{
						return new TextFilter(propertyDefinition, text, MatchOptions.FullString, MatchFlags.IgnoreCase);
					}
					return new NotFilter(new ExistsFilter(propertyDefinition));
				}
				else
				{
					if (useWildchars)
					{
						if (text == null)
						{
							text = val.ToString();
						}
						MatchOptions matchOptions = MatchOptions.FullString;
						if (text.StartsWith("*"))
						{
							matchOptions = MatchOptions.Suffix;
							text = text.Substring(1, text.Length - 1);
						}
						if (text.EndsWith("*"))
						{
							if (matchOptions == MatchOptions.Suffix)
							{
								matchOptions = MatchOptions.SubString;
							}
							else
							{
								matchOptions = MatchOptions.Prefix;
							}
							text = text.Substring(0, text.Length - 1);
						}
						return new TextFilter(propertyDefinition, text, matchOptions, MatchFlags.IgnoreCase);
					}
					bool flag = propertyDefinition.Type.IsGenericType && propertyDefinition.Type.GetGenericTypeDefinition() == typeof(Nullable<>);
					if (flag)
					{
						Type[] genericArguments = propertyDefinition.Type.GetGenericArguments();
						flag = genericArguments[0].Equals(val.GetType());
					}
					if (val.GetType() != propertyDefinition.Type && !flag)
					{
						if (val is Parser.Variable)
						{
							val = val.ToString();
						}
						try
						{
							val = this.convertDelegate((string)val, propertyDefinition.Type);
						}
						catch (OverflowException)
						{
							throw new ParsingInvalidFormat((string)val, propertyDefinition.Type, this.query, this.CurPos + 1);
						}
						catch (FormatException)
						{
							throw new ParsingInvalidFormat((string)val, propertyDefinition.Type, this.query, this.CurPos + 1);
						}
						catch (PSInvalidCastException)
						{
							throw new ParsingInvalidFormat((string)val, propertyDefinition.Type, this.query, this.CurPos + 1);
						}
					}
					return new ComparisonFilter(op, propertyDefinition, val);
				}
			}
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		private void CheckCapability(QueryParser.Capabilities capability)
		{
			if ((this.Capabilities & capability) == (QueryParser.Capabilities)0)
			{
				switch (capability)
				{
				case QueryParser.Capabilities.Or:
					throw new ParsingInvalidFilterOperator("-or", this.query, this.CurPos + 1);
				case QueryParser.Capabilities.Like:
					throw new ParsingInvalidFilterOperator("-like", this.query, this.CurPos + 1);
				case QueryParser.Capabilities.NotLike:
					throw new ParsingInvalidFilterOperator("-notlike", this.query, this.CurPos + 1);
				}
				throw new ParsingInvalidFilterOperator("Unknown", this.query, this.CurPos + 1);
			}
		}

		// Token: 0x04000BD4 RID: 3028
		private readonly string query;

		// Token: 0x04000BD5 RID: 3029
		private QueryParser.LookupPropertyDelegate schemaLookupDelegate;

		// Token: 0x04000BD6 RID: 3030
		private QueryParser.ListKnownPropertiesDelegate listKnownPropertiesDelegate;

		// Token: 0x04000BD7 RID: 3031
		private QueryParser.EvaluateVariableDelegate evalDelegate;

		// Token: 0x04000BD8 RID: 3032
		private QueryParser.ConvertValueFromStringDelegate convertDelegate;

		// Token: 0x04000BD9 RID: 3033
		private QueryFilter parseTree;

		// Token: 0x0200024A RID: 586
		private class Variable
		{
			// Token: 0x0600140C RID: 5132 RVA: 0x0003F786 File Offset: 0x0003D986
			public Variable(string name)
			{
				this.Name = name;
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x0600140D RID: 5133 RVA: 0x0003F795 File Offset: 0x0003D995
			// (set) Token: 0x0600140E RID: 5134 RVA: 0x0003F79D File Offset: 0x0003D99D
			public string Name { get; set; }
		}
	}
}
