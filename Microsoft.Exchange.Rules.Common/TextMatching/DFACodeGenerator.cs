using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x0200003F RID: 63
	internal sealed class DFACodeGenerator
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000718C File Offset: 0x0000538C
		public DFACodeGenerator(string name, int size)
		{
			this.matcherMethod = new DynamicMethod(name, typeof(bool), new Type[]
			{
				typeof(ITextInputBuffer)
			}, typeof(DFACodeGenerator).Module);
			this.stateLabels = new Label[size];
			this.stateLabelDefinedFlags = new BitArray(size);
			this.stateLabelMarkedFlags = new BitArray(size);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007212 File Offset: 0x00005412
		private ILGenerator Generator
		{
			get
			{
				return this.matcherMethod.GetILGenerator();
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000721F File Offset: 0x0000541F
		public void BeginCompile()
		{
			this.Generator.DeclareLocal(typeof(int));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007237 File Offset: 0x00005437
		public void Add(StateNode from, StateNode to, int ch)
		{
			this.AddTransition(from, to, ch);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007244 File Offset: 0x00005444
		public void Add(StateNode from, StateNode to, RegexCharacterClass cl)
		{
			this.EmitLoadInput(from, to);
			if (cl.Type == RegexCharacterClass.ValueType.Character)
			{
				this.EmitCharComparision(from, to, cl.GetHashCode());
			}
			else
			{
				this.EmitCharacterClassComparision(from, to, cl.Type);
			}
			if (from.State == 0 && cl.Type == RegexCharacterClass.ValueType.NonWordCharacterClass)
			{
				this.beginningNonWordToStateid = to.State;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000729C File Offset: 0x0000549C
		public void AddTransition(StateNode from, StateNode to, int ch)
		{
			this.EmitLoadInput(from, to);
			this.EmitCharComparision(from, to, ch);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000072AF File Offset: 0x000054AF
		public PatternMatcher EndCompile()
		{
			this.EmitLastCheck();
			this.CheckPendingLabels();
			return (PatternMatcher)this.matcherMethod.CreateDelegate(typeof(PatternMatcher));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000072D8 File Offset: 0x000054D8
		private void CheckPendingLabels()
		{
			for (int i = 0; i <= this.pendingLabelIndex; i++)
			{
				if (this.stateLabelDefinedFlags[i] && !this.stateLabelMarkedFlags[i])
				{
					this.EmitLabel(this.stateLabels[i], true);
					this.stateLabelMarkedFlags[i] = true;
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007337 File Offset: 0x00005537
		private void EmitLoadInput()
		{
			this.Generator.Emit(OpCodes.Ldarg_0);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000734C File Offset: 0x0000554C
		private void EmitLabel(Label label, bool finalState)
		{
			this.Generator.MarkLabel(label);
			if (finalState)
			{
				this.Generator.Emit(OpCodes.Ldc_I4_1);
				this.Generator.Emit(OpCodes.Ret);
				return;
			}
			this.EmitLoadInput();
			this.Generator.EmitCall(OpCodes.Callvirt, DFACodeGenerator.getNextChar, null);
			this.Generator.Emit(OpCodes.Stloc_0);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000073B8 File Offset: 0x000055B8
		private void EmitLoadInput(StateNode from, StateNode to)
		{
			if (this.lastStateid != -1 && this.lastStateid != from.State)
			{
				this.EmitLastCheck();
			}
			if (!this.stateLabelDefinedFlags[from.State])
			{
				this.stateLabels[from.State] = this.Generator.DefineLabel();
				this.EmitLabel(this.stateLabels[from.State], from.IsFinal);
				this.stateLabelDefinedFlags[from.State] = true;
				this.stateLabelMarkedFlags[from.State] = true;
			}
			else if (!this.stateLabelMarkedFlags[from.State])
			{
				this.EmitLabel(this.stateLabels[from.State], from.IsFinal);
				this.stateLabelMarkedFlags[from.State] = true;
			}
			if (!this.stateLabelDefinedFlags[to.State])
			{
				this.stateLabels[to.State] = this.Generator.DefineLabel();
				this.stateLabelDefinedFlags[to.State] = true;
				this.pendingLabelIndex = to.State;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000074F8 File Offset: 0x000056F8
		private void EmitLastCheck()
		{
			Label label = this.Generator.DefineLabel();
			this.Generator.Emit(OpCodes.Ldloc_0);
			this.Generator.Emit(OpCodes.Ldc_I4_M1);
			this.Generator.Emit(OpCodes.Beq, label);
			if (this.beginningNonWordToStateid > 0 && this.lastStateid > 0)
			{
				this.Generator.Emit(OpCodes.Ldloc_0);
				this.Generator.EmitCall(OpCodes.Call, DFACodeGenerator.compareNonWord, null);
				this.Generator.Emit(OpCodes.Brtrue, this.stateLabels[this.beginningNonWordToStateid]);
			}
			this.Generator.Emit(OpCodes.Br, this.stateLabels[0]);
			this.Generator.MarkLabel(label);
			this.Generator.Emit(OpCodes.Ldc_I4_0);
			this.Generator.Emit(OpCodes.Ret);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000075F0 File Offset: 0x000057F0
		private void EmitCharComparision(StateNode from, StateNode to, int ch)
		{
			Label label = this.stateLabels[to.State];
			this.Generator.Emit(OpCodes.Ldloc_0);
			this.Generator.Emit(OpCodes.Ldc_I4, ch);
			this.Generator.Emit(OpCodes.Beq, label);
			this.lastStateid = from.State;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007654 File Offset: 0x00005854
		private void EmitCharacterClassComparision(StateNode from, StateNode to, RegexCharacterClass.ValueType charType)
		{
			Label label = this.stateLabels[to.State];
			this.Generator.Emit(OpCodes.Ldloc_0);
			switch (charType)
			{
			case RegexCharacterClass.ValueType.BeginCharacterClass:
				this.Generator.EmitCall(OpCodes.Call, DFACodeGenerator.compareBegin, null);
				break;
			case RegexCharacterClass.ValueType.EndCharacterClass:
				this.Generator.EmitCall(OpCodes.Call, DFACodeGenerator.compareEnd, null);
				break;
			case RegexCharacterClass.ValueType.SpaceCharacterClass:
				this.Generator.EmitCall(OpCodes.Call, DFACodeGenerator.compareSpace, null);
				break;
			case RegexCharacterClass.ValueType.NonSpaceCharacterClass:
				this.Generator.EmitCall(OpCodes.Call, DFACodeGenerator.compareNonSpace, null);
				break;
			case RegexCharacterClass.ValueType.NonDigitCharacterClass:
				this.Generator.EmitCall(OpCodes.Call, DFACodeGenerator.compareNonDigit, null);
				break;
			case RegexCharacterClass.ValueType.DigitCharacterClass:
				this.Generator.EmitCall(OpCodes.Call, DFACodeGenerator.compareDigit, null);
				break;
			case RegexCharacterClass.ValueType.WordCharacterClass:
				this.Generator.EmitCall(OpCodes.Call, DFACodeGenerator.compareWord, null);
				break;
			case RegexCharacterClass.ValueType.NonWordCharacterClass:
				this.Generator.EmitCall(OpCodes.Call, DFACodeGenerator.compareNonWord, null);
				break;
			default:
				throw new TextMatchingParsingException(TextMatchingStrings.RegexUnSupportedMetaCharacter);
			}
			this.Generator.Emit(OpCodes.Brtrue, label);
			this.lastStateid = from.State;
		}

		// Token: 0x040000B6 RID: 182
		private static MethodInfo getNextChar = typeof(ITextInputBuffer).GetMethod("get_NextChar");

		// Token: 0x040000B7 RID: 183
		private static MethodInfo compareSpace = typeof(RegexCharacterClassRuntime).GetMethod("IsSpace", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x040000B8 RID: 184
		private static MethodInfo compareNonSpace = typeof(RegexCharacterClassRuntime).GetMethod("IsNonSpace", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x040000B9 RID: 185
		private static MethodInfo compareDigit = typeof(RegexCharacterClassRuntime).GetMethod("IsDigit", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x040000BA RID: 186
		private static MethodInfo compareNonDigit = typeof(RegexCharacterClassRuntime).GetMethod("IsNonDigit", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x040000BB RID: 187
		private static MethodInfo compareNonWord = typeof(RegexCharacterClassRuntime).GetMethod("IsNonWord", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x040000BC RID: 188
		private static MethodInfo compareWord = typeof(RegexCharacterClassRuntime).GetMethod("IsWord", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x040000BD RID: 189
		private static MethodInfo compareBegin = typeof(RegexCharacterClassRuntime).GetMethod("IsBegin", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x040000BE RID: 190
		private static MethodInfo compareEnd = typeof(RegexCharacterClassRuntime).GetMethod("IsEnd", BindingFlags.Static | BindingFlags.Public);

		// Token: 0x040000BF RID: 191
		private DynamicMethod matcherMethod;

		// Token: 0x040000C0 RID: 192
		private Label[] stateLabels;

		// Token: 0x040000C1 RID: 193
		private BitArray stateLabelDefinedFlags;

		// Token: 0x040000C2 RID: 194
		private BitArray stateLabelMarkedFlags;

		// Token: 0x040000C3 RID: 195
		private int lastStateid = -1;

		// Token: 0x040000C4 RID: 196
		private int pendingLabelIndex = -1;

		// Token: 0x040000C5 RID: 197
		private int beginningNonWordToStateid = -1;
	}
}
