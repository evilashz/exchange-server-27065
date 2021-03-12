using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004E4 RID: 1252
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FormattedSentence
	{
		// Token: 0x060036C6 RID: 14022 RVA: 0x000DD318 File Offset: 0x000DB518
		public FormattedSentence(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			this.BuildInstructionQueue(this.instructionQueue, FormattedSentence.Sentence.Parse(format));
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000DD34C File Offset: 0x000DB54C
		public string Evaluate(FormattedSentence.Context context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			Stack<FormattedSentence.IEvaluationStackMember> stack = new Stack<FormattedSentence.IEvaluationStackMember>();
			foreach (FormattedSentence.IInstructionQueueMember instructionQueueMember in this.instructionQueue)
			{
				instructionQueueMember.Evaluate(context, stack);
			}
			FormattedSentence.IEvaluationStackMember evaluationStackMember = stack.Pop();
			if (stack.Count > 0)
			{
				throw new InvalidOperationException("FormattedSentence execution produced unexpected results: more than 1 result element");
			}
			StringBuilder stringBuilder = new StringBuilder();
			evaluationStackMember.WriteTo(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000DD3E4 File Offset: 0x000DB5E4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (FormattedSentence.IInstructionQueueMember instructionQueueMember in this.instructionQueue)
			{
				stringBuilder.Append(instructionQueueMember.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000DD44C File Offset: 0x000DB64C
		private void BuildInstructionsForOneToken(IList<FormattedSentence.IInstructionQueueMember> instructionQueue, FormattedSentence.Token token)
		{
			FormattedSentence.Sentence sentence = token as FormattedSentence.Sentence;
			FormattedSentence.SimpleToken simpleToken = token as FormattedSentence.SimpleToken;
			if (sentence != null)
			{
				this.BuildInstructionQueue(instructionQueue, sentence);
				return;
			}
			if (simpleToken == null)
			{
				return;
			}
			switch (simpleToken.Flavor)
			{
			case FormattedSentence.SimpleToken.TokenFlavor.None:
				instructionQueue.Add(new FormattedSentence.StringConstant(simpleToken.Value));
				return;
			case FormattedSentence.SimpleToken.TokenFlavor.InCurlyBraces:
				instructionQueue.Add(new FormattedSentence.ResolvePlaceholder(simpleToken.Value));
				return;
			default:
				throw new NotSupportedException("Token flavor is not supported");
			}
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000DD4C0 File Offset: 0x000DB6C0
		protected void BuildInstructionQueue(IList<FormattedSentence.IInstructionQueueMember> instructionQueue, FormattedSentence.Sentence sentence)
		{
			if (sentence.Tokens.Count == 0)
			{
				instructionQueue.Add(FormattedSentence.StringConstant.EmptySpacer);
				return;
			}
			if (sentence.Tokens.Count == 1)
			{
				this.BuildInstructionsForOneToken(instructionQueue, sentence.Tokens[0]);
				return;
			}
			bool flag = false;
			int num = 0;
			for (int i = 0; i < sentence.Tokens.Count; i++)
			{
				FormattedSentence.SimpleToken simpleToken = sentence.Tokens[i] as FormattedSentence.SimpleToken;
				bool flag2 = simpleToken != null && simpleToken.Flavor == FormattedSentence.SimpleToken.TokenFlavor.None;
				if (i == 0 && flag2)
				{
					instructionQueue.Add(FormattedSentence.StringConstant.Null);
					num++;
				}
				if (i > 0 && !flag && !flag2)
				{
					instructionQueue.Add(FormattedSentence.StringConstant.EmptySpacer);
					num++;
				}
				this.BuildInstructionsForOneToken(instructionQueue, sentence.Tokens[i]);
				num++;
				flag = flag2;
			}
			if (flag)
			{
				instructionQueue.Add(FormattedSentence.StringConstant.Null);
				num++;
			}
			int num2 = (num - 1) / 2;
			for (int j = 0; j < num2; j++)
			{
				instructionQueue.Add(new FormattedSentence.ConditionalDelimiter());
			}
		}

		// Token: 0x04001D42 RID: 7490
		private readonly List<FormattedSentence.IInstructionQueueMember> instructionQueue = new List<FormattedSentence.IInstructionQueueMember>();

		// Token: 0x020004E5 RID: 1253
		protected abstract class Token
		{
			// Token: 0x04001D43 RID: 7491
			protected const char EscapeChar = '\\';
		}

		// Token: 0x020004E6 RID: 1254
		protected sealed class Sentence : FormattedSentence.Token
		{
			// Token: 0x060036CC RID: 14028 RVA: 0x000DD5CC File Offset: 0x000DB7CC
			private Sentence(List<FormattedSentence.Token> tokens)
			{
				this.tokens = new ReadOnlyCollection<FormattedSentence.Token>(tokens);
			}

			// Token: 0x170010EE RID: 4334
			// (get) Token: 0x060036CD RID: 14029 RVA: 0x000DD5E0 File Offset: 0x000DB7E0
			public IList<FormattedSentence.Token> Tokens
			{
				get
				{
					return this.tokens;
				}
			}

			// Token: 0x060036CE RID: 14030 RVA: 0x000DD5E8 File Offset: 0x000DB7E8
			public static FormattedSentence.Sentence Parse(string input)
			{
				FormattedSentence.Sentence result;
				int num = FormattedSentence.Sentence.Parse(input, 0, out result);
				if (num != input.Length)
				{
					throw new FormatException("'>' is mismatched or not escaped");
				}
				return result;
			}

			// Token: 0x060036CF RID: 14031 RVA: 0x000DD614 File Offset: 0x000DB814
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (FormattedSentence.Token token in this.tokens)
				{
					stringBuilder.Append(token.ToString());
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060036D0 RID: 14032 RVA: 0x000DD674 File Offset: 0x000DB874
			private static int Parse(string input, int startAt, out FormattedSentence.Sentence sentence)
			{
				FormattedSentence.SimpleToken.Builder builder = new FormattedSentence.SimpleToken.Builder(null, FormattedSentence.SimpleToken.TokenFlavor.None, -1);
				List<FormattedSentence.Token> list = new List<FormattedSentence.Token>();
				int num = startAt - 1;
				bool flag = false;
				while (!flag)
				{
					num++;
					if (num > input.Length)
					{
						throw new FormatException("Unexpected end of escape sequence. If '' was intended for output, replace it with '\\'");
					}
					if (num == input.Length)
					{
						break;
					}
					char c = input[num];
					char c2 = c;
					switch (c2)
					{
					case '<':
					{
						FormattedSentence.Sentence.AddTokenFlavorNone(list, num - 1, ref builder);
						FormattedSentence.Sentence item;
						num = FormattedSentence.Sentence.Parse(input, num + 1, out item);
						if (num >= input.Length || input[num] != '>')
						{
							throw new FormatException("'<' is mismatched or not properly escaped");
						}
						list.Add(item);
						continue;
					}
					case '=':
						break;
					case '>':
						flag = true;
						continue;
					default:
						switch (c2)
						{
						case '{':
							FormattedSentence.Sentence.AddTokenFlavorNone(list, num - 1, ref builder);
							builder = new FormattedSentence.SimpleToken.Builder(input, FormattedSentence.SimpleToken.TokenFlavor.InCurlyBraces, num + 1);
							continue;
						case '}':
							if (!builder.IsValid || builder.Flavor != FormattedSentence.SimpleToken.TokenFlavor.InCurlyBraces)
							{
								throw new FormatException("'}' is mismatched or not escaped");
							}
							FormattedSentence.Sentence.AddToken(list, num - 1, ref builder);
							continue;
						}
						break;
					}
					if (!builder.IsValid)
					{
						builder = new FormattedSentence.SimpleToken.Builder(input, FormattedSentence.SimpleToken.TokenFlavor.None, num);
					}
					if (c == '\\')
					{
						num++;
					}
				}
				FormattedSentence.Sentence.AddTokenFlavorNone(list, num - 1, ref builder);
				sentence = new FormattedSentence.Sentence(list);
				return num;
			}

			// Token: 0x060036D1 RID: 14033 RVA: 0x000DD7CC File Offset: 0x000DB9CC
			private static void AddTokenFlavorNone(IList<FormattedSentence.Token> tokens, int endAt, ref FormattedSentence.SimpleToken.Builder tokenBuilder)
			{
				if (tokenBuilder.IsValid)
				{
					if (tokenBuilder.Flavor == FormattedSentence.SimpleToken.TokenFlavor.InCurlyBraces)
					{
						throw new FormatException("'{' is mismatched or not escaped");
					}
					FormattedSentence.Sentence.AddToken(tokens, endAt, ref tokenBuilder);
				}
			}

			// Token: 0x060036D2 RID: 14034 RVA: 0x000DD7F4 File Offset: 0x000DB9F4
			private static bool AddToken(IList<FormattedSentence.Token> tokens, int endAt, ref FormattedSentence.SimpleToken.Builder tokenBuilder)
			{
				if (tokenBuilder.IsValid)
				{
					FormattedSentence.SimpleToken simpleToken = tokenBuilder.Create(endAt);
					if (simpleToken != null)
					{
						tokens.Add(simpleToken);
					}
					tokenBuilder.Invalidate();
					return simpleToken != null;
				}
				return false;
			}

			// Token: 0x04001D44 RID: 7492
			private readonly IList<FormattedSentence.Token> tokens;
		}

		// Token: 0x020004E7 RID: 1255
		protected sealed class SimpleToken : FormattedSentence.Token
		{
			// Token: 0x060036D3 RID: 14035 RVA: 0x000DD82A File Offset: 0x000DBA2A
			public SimpleToken(FormattedSentence.SimpleToken.TokenFlavor flavor, string value)
			{
				this.flavor = flavor;
				this.value = value;
			}

			// Token: 0x170010EF RID: 4335
			// (get) Token: 0x060036D4 RID: 14036 RVA: 0x000DD840 File Offset: 0x000DBA40
			public FormattedSentence.SimpleToken.TokenFlavor Flavor
			{
				get
				{
					return this.flavor;
				}
			}

			// Token: 0x170010F0 RID: 4336
			// (get) Token: 0x060036D5 RID: 14037 RVA: 0x000DD848 File Offset: 0x000DBA48
			public string Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x060036D6 RID: 14038 RVA: 0x000DD850 File Offset: 0x000DBA50
			public override string ToString()
			{
				return this.value;
			}

			// Token: 0x04001D45 RID: 7493
			private readonly FormattedSentence.SimpleToken.TokenFlavor flavor;

			// Token: 0x04001D46 RID: 7494
			private readonly string value;

			// Token: 0x020004E8 RID: 1256
			public enum TokenFlavor
			{
				// Token: 0x04001D48 RID: 7496
				None = -1,
				// Token: 0x04001D49 RID: 7497
				InCurlyBraces
			}

			// Token: 0x020004E9 RID: 1257
			public struct Builder
			{
				// Token: 0x060036D7 RID: 14039 RVA: 0x000DD858 File Offset: 0x000DBA58
				public Builder(string input, FormattedSentence.SimpleToken.TokenFlavor flavor, int startAt)
				{
					this.input = input;
					this.flavor = flavor;
					this.startAt = startAt;
				}

				// Token: 0x170010F1 RID: 4337
				// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000DD86F File Offset: 0x000DBA6F
				public FormattedSentence.SimpleToken.TokenFlavor Flavor
				{
					get
					{
						return this.flavor;
					}
				}

				// Token: 0x170010F2 RID: 4338
				// (get) Token: 0x060036D9 RID: 14041 RVA: 0x000DD877 File Offset: 0x000DBA77
				public bool IsValid
				{
					get
					{
						return this.input != null;
					}
				}

				// Token: 0x060036DA RID: 14042 RVA: 0x000DD888 File Offset: 0x000DBA88
				public FormattedSentence.SimpleToken Create(int endAt)
				{
					if (endAt >= this.input.Length)
					{
						ExDiagnostics.FailFast("endAt is beyond the end of input string", false);
						return null;
					}
					if (endAt < this.startAt)
					{
						throw new FormatException("Empty tokens like \"{}\" are not allowed");
					}
					if (this.input.IndexOf('\\', this.startAt, endAt - this.startAt + 1) != -1)
					{
						StringBuilder stringBuilder = new StringBuilder(endAt - this.startAt);
						bool flag = false;
						for (int i = this.startAt; i <= endAt; i++)
						{
							if (this.input[i] != '\\' || flag)
							{
								stringBuilder.Append(this.input[i]);
							}
							flag = (!flag && this.input[i] == '\\');
						}
						return new FormattedSentence.SimpleToken(this.flavor, stringBuilder.ToString());
					}
					return new FormattedSentence.SimpleToken(this.flavor, this.input.Substring(this.startAt, endAt - this.startAt + 1));
				}

				// Token: 0x060036DB RID: 14043 RVA: 0x000DD97A File Offset: 0x000DBB7A
				public void Invalidate()
				{
					this.input = null;
				}

				// Token: 0x04001D4A RID: 7498
				private string input;

				// Token: 0x04001D4B RID: 7499
				private readonly int startAt;

				// Token: 0x04001D4C RID: 7500
				private readonly FormattedSentence.SimpleToken.TokenFlavor flavor;
			}
		}

		// Token: 0x020004EA RID: 1258
		protected interface IInstructionQueueMember
		{
			// Token: 0x060036DC RID: 14044
			void Evaluate(FormattedSentence.Context context, Stack<FormattedSentence.IEvaluationStackMember> evaluationStack);
		}

		// Token: 0x020004EB RID: 1259
		protected interface IEvaluationStackMember
		{
			// Token: 0x170010F3 RID: 4339
			// (get) Token: 0x060036DD RID: 14045
			bool IsEmpty { get; }

			// Token: 0x060036DE RID: 14046
			void WriteTo(StringBuilder outputBuilder);
		}

		// Token: 0x020004EC RID: 1260
		public abstract class Context
		{
			// Token: 0x060036DF RID: 14047
			public abstract string ResolvePlaceholder(string code);
		}

		// Token: 0x020004ED RID: 1261
		protected sealed class StringConstant : FormattedSentence.IInstructionQueueMember, FormattedSentence.IEvaluationStackMember
		{
			// Token: 0x060036E1 RID: 14049 RVA: 0x000DD98B File Offset: 0x000DBB8B
			public StringConstant(string value)
			{
				this.value = value;
			}

			// Token: 0x170010F4 RID: 4340
			// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000DD99A File Offset: 0x000DBB9A
			public bool IsEmpty
			{
				get
				{
					return string.IsNullOrEmpty(this.value);
				}
			}

			// Token: 0x060036E3 RID: 14051 RVA: 0x000DD9A7 File Offset: 0x000DBBA7
			public void Evaluate(FormattedSentence.Context context, Stack<FormattedSentence.IEvaluationStackMember> evaluationStack)
			{
				evaluationStack.Push(this);
			}

			// Token: 0x060036E4 RID: 14052 RVA: 0x000DD9B0 File Offset: 0x000DBBB0
			public override string ToString()
			{
				if (this == FormattedSentence.StringConstant.Null)
				{
					return "<Null>";
				}
				if (string.IsNullOrEmpty(this.value))
				{
					return "<Empty>";
				}
				return this.value;
			}

			// Token: 0x060036E5 RID: 14053 RVA: 0x000DD9D9 File Offset: 0x000DBBD9
			public void WriteTo(StringBuilder outputBuilder)
			{
				outputBuilder.Append(this.value);
			}

			// Token: 0x04001D4D RID: 7501
			public static readonly FormattedSentence.StringConstant EmptySpacer = new FormattedSentence.StringConstant(string.Empty);

			// Token: 0x04001D4E RID: 7502
			public static readonly FormattedSentence.StringConstant Null = new FormattedSentence.StringConstant(string.Empty);

			// Token: 0x04001D4F RID: 7503
			private readonly string value;
		}

		// Token: 0x020004EE RID: 1262
		protected sealed class ResolvePlaceholder : FormattedSentence.IInstructionQueueMember
		{
			// Token: 0x060036E7 RID: 14055 RVA: 0x000DDA08 File Offset: 0x000DBC08
			public ResolvePlaceholder(string code)
			{
				this.code = code;
			}

			// Token: 0x060036E8 RID: 14056 RVA: 0x000DDA17 File Offset: 0x000DBC17
			public void Evaluate(FormattedSentence.Context context, Stack<FormattedSentence.IEvaluationStackMember> evaluationStack)
			{
				evaluationStack.Push(new FormattedSentence.StringConstant(context.ResolvePlaceholder(this.code)));
			}

			// Token: 0x060036E9 RID: 14057 RVA: 0x000DDA30 File Offset: 0x000DBC30
			public override string ToString()
			{
				return "{" + this.code + "}";
			}

			// Token: 0x04001D50 RID: 7504
			private readonly string code;
		}

		// Token: 0x020004EF RID: 1263
		protected sealed class ConditionalDelimiter : FormattedSentence.IInstructionQueueMember
		{
			// Token: 0x060036EA RID: 14058 RVA: 0x000DDA48 File Offset: 0x000DBC48
			public void Evaluate(FormattedSentence.Context context, Stack<FormattedSentence.IEvaluationStackMember> evaluationStack)
			{
				FormattedSentence.IEvaluationStackMember evaluationStackMember = evaluationStack.Pop();
				FormattedSentence.IEvaluationStackMember evaluationStackMember2 = evaluationStack.Pop();
				FormattedSentence.IEvaluationStackMember evaluationStackMember3 = evaluationStack.Pop();
				bool flag = (!evaluationStackMember3.IsEmpty && !evaluationStackMember.IsEmpty) || (!evaluationStackMember3.IsEmpty && evaluationStackMember == FormattedSentence.StringConstant.Null) || (!evaluationStackMember.IsEmpty && evaluationStackMember3 == FormattedSentence.StringConstant.Null);
				evaluationStack.Push(FormattedSentence.CompositeString.Create(new FormattedSentence.IEvaluationStackMember[]
				{
					evaluationStackMember3,
					flag ? evaluationStackMember2 : FormattedSentence.StringConstant.Null,
					evaluationStackMember
				}));
			}

			// Token: 0x060036EB RID: 14059 RVA: 0x000DDACF File Offset: 0x000DBCCF
			public override string ToString()
			{
				return "!";
			}
		}

		// Token: 0x020004F0 RID: 1264
		protected sealed class CompositeString : FormattedSentence.IEvaluationStackMember
		{
			// Token: 0x060036ED RID: 14061 RVA: 0x000DDADE File Offset: 0x000DBCDE
			private CompositeString(FormattedSentence.IEvaluationStackMember[] members)
			{
				this.members = members;
			}

			// Token: 0x170010F5 RID: 4341
			// (get) Token: 0x060036EE RID: 14062 RVA: 0x000DDAF0 File Offset: 0x000DBCF0
			public bool IsEmpty
			{
				get
				{
					foreach (FormattedSentence.IEvaluationStackMember evaluationStackMember in this.members)
					{
						if (!evaluationStackMember.IsEmpty)
						{
							return false;
						}
					}
					return true;
				}
			}

			// Token: 0x060036EF RID: 14063 RVA: 0x000DDB28 File Offset: 0x000DBD28
			public static FormattedSentence.IEvaluationStackMember Create(params FormattedSentence.IEvaluationStackMember[] members)
			{
				FormattedSentence.IEvaluationStackMember evaluationStackMember = null;
				foreach (FormattedSentence.IEvaluationStackMember evaluationStackMember2 in members)
				{
					if (!evaluationStackMember2.IsEmpty)
					{
						if (evaluationStackMember != null)
						{
							return new FormattedSentence.CompositeString(members);
						}
						evaluationStackMember = evaluationStackMember2;
					}
				}
				if (evaluationStackMember == null)
				{
					return FormattedSentence.StringConstant.EmptySpacer;
				}
				return evaluationStackMember;
			}

			// Token: 0x060036F0 RID: 14064 RVA: 0x000DDB74 File Offset: 0x000DBD74
			public void WriteTo(StringBuilder outputBuilder)
			{
				foreach (FormattedSentence.IEvaluationStackMember evaluationStackMember in this.members)
				{
					evaluationStackMember.WriteTo(outputBuilder);
				}
			}

			// Token: 0x04001D51 RID: 7505
			private readonly FormattedSentence.IEvaluationStackMember[] members;
		}
	}
}
