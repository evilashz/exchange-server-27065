using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000003 RID: 3
	public abstract class ConclusionImplementation<TConclusion, TSettingConclusion, TRuleConclusion> : Conclusion where TConclusion : ConclusionImplementation<TConclusion, TSettingConclusion, TRuleConclusion> where TSettingConclusion : TConclusion where TRuleConclusion : TConclusion, IRuleConclusion
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000021E6 File Offset: 0x000003E6
		protected ConclusionImplementation()
		{
			this.children = new List<TConclusion>();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F9 File Offset: 0x000003F9
		protected ConclusionImplementation(Result result) : base(result)
		{
			this.children = new List<TConclusion>();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000220D File Offset: 0x0000040D
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002215 File Offset: 0x00000415
		public TConclusion Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				base.ThrowIfReadOnly();
				this.parent = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002224 File Offset: 0x00000424
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000222C File Offset: 0x0000042C
		public IList<TConclusion> Children
		{
			get
			{
				return this.children;
			}
			set
			{
				base.ThrowIfReadOnly();
				this.children = new ReadOnlyCollection<TConclusion>(this.children);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002268 File Offset: 0x00000468
		public IEnumerable<TConclusion> ChildrenWithName(string name)
		{
			return from x in this.children
			where string.Equals(name, x.Name, StringComparison.Ordinal)
			select x;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022AB File Offset: 0x000004AB
		public IEnumerable<TConclusion> ChildrenWithoutExceptions()
		{
			return from x in this.children
			where !x.HasException
			select x;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022E7 File Offset: 0x000004E7
		public IEnumerable<TConclusion> ChildrenWithoutExceptions(string name)
		{
			return from x in this.ChildrenWithName(name)
			where !x.HasException
			select x;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002418 File Offset: 0x00000618
		public IEnumerable<TConclusion> AncestorsAndSelf()
		{
			for (TConclusion current = (TConclusion)((object)this); current != null; current = current.Parent)
			{
				yield return current;
			}
			yield break;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002458 File Offset: 0x00000658
		public IEnumerable<TConclusion> AncestorsAndSelf(string name)
		{
			return from x in this.AncestorsAndSelf()
			where string.Equals(name, x.Name, StringComparison.Ordinal)
			select x;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002489 File Offset: 0x00000689
		public IEnumerable<TConclusion> Ancestors()
		{
			return this.AncestorsAndSelf().Skip(1);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002497 File Offset: 0x00000697
		public IEnumerable<TConclusion> Ancestors(string name)
		{
			return this.AncestorsAndSelf(name).Skip(1);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000260C File Offset: 0x0000080C
		public IEnumerable<TConclusion> DescendantsAndSelf()
		{
			Stack<TConclusion> stack = new Stack<TConclusion>();
			stack.Push((TConclusion)((object)this));
			while (stack.Count > 0)
			{
				TConclusion current = stack.Pop();
				yield return current;
				for (int i = current.Children.Count - 1; i >= 0; i--)
				{
					stack.Push(current.Children[i]);
				}
			}
			yield break;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000264C File Offset: 0x0000084C
		public IEnumerable<TConclusion> DescendantsAndSelf(string name)
		{
			return from x in this.DescendantsAndSelf()
			where string.Equals(name, x.Name, StringComparison.Ordinal)
			select x;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000268F File Offset: 0x0000088F
		public IEnumerable<TConclusion> DescendantsAndSelfWithoutExceptions()
		{
			return from x in this.DescendantsAndSelf()
			where !x.HasException
			select x;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026CB File Offset: 0x000008CB
		public IEnumerable<TConclusion> DescendantsAndSelfWithoutExceptions(string name)
		{
			return from x in this.DescendantsAndSelf(name)
			where !x.HasException
			select x;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026F6 File Offset: 0x000008F6
		public IEnumerable<TConclusion> Descendants()
		{
			return this.DescendantsAndSelf().Skip(1);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002704 File Offset: 0x00000904
		public IEnumerable<TConclusion> Descendants(string name)
		{
			return this.DescendantsAndSelf(name).Skip(1);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002725 File Offset: 0x00000925
		public IEnumerable<TConclusion> DescendantsWithoutExceptions()
		{
			return from x in this.Descendants()
			where !x.HasException
			select x;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002761 File Offset: 0x00000961
		public IEnumerable<TConclusion> DescendantsWithoutExceptions(string name)
		{
			return from x in this.Descendants(name)
			where !x.HasException
			select x;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000279B File Offset: 0x0000099B
		public IEnumerable<TConclusion> Exceptions()
		{
			return from x in this.DescendantsAndSelf()
			where x.HasException
			select x;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027D4 File Offset: 0x000009D4
		public IEnumerable<TConclusion> Exceptions(string name)
		{
			return from x in this.DescendantsAndSelf(name)
			where x.HasException
			select x;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027FF File Offset: 0x000009FF
		public IEnumerable<TSettingConclusion> Settings()
		{
			return this.DescendantsAndSelfWithoutExceptions().OfType<TSettingConclusion>();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000280C File Offset: 0x00000A0C
		public IEnumerable<TSettingConclusion> Settings(string name)
		{
			return this.DescendantsAndSelfWithoutExceptions(name).OfType<TSettingConclusion>();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000281A File Offset: 0x00000A1A
		public IEnumerable<TRuleConclusion> Rules()
		{
			return this.DescendantsAndSelfWithoutExceptions().OfType<TRuleConclusion>();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002827 File Offset: 0x00000A27
		public IEnumerable<TRuleConclusion> Rules(string name)
		{
			return this.DescendantsAndSelfWithoutExceptions(name).OfType<TRuleConclusion>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002858 File Offset: 0x00000A58
		public IEnumerable<TRuleConclusion> Errors()
		{
			return from x in this.Rules()
			where x.IsConditionMet && x.Severity == Severity.Error
			select x;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028A5 File Offset: 0x00000AA5
		public IEnumerable<TRuleConclusion> Errors(string name)
		{
			return from x in this.Rules(name)
			where x.IsConditionMet && x.Severity == Severity.Error
			select x;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028F3 File Offset: 0x00000AF3
		public IEnumerable<TRuleConclusion> Warnings()
		{
			return from x in this.Rules()
			where x.IsConditionMet && x.Severity == Severity.Warning
			select x;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002940 File Offset: 0x00000B40
		public IEnumerable<TRuleConclusion> Warnings(string name)
		{
			return from x in this.Rules(name)
			where x.IsConditionMet && x.Severity == Severity.Warning
			select x;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000298E File Offset: 0x00000B8E
		public IEnumerable<TRuleConclusion> Info()
		{
			return from x in this.Rules()
			where x.IsConditionMet && x.Severity == Severity.Info
			select x;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000029DB File Offset: 0x00000BDB
		public IEnumerable<TRuleConclusion> Info(string name)
		{
			return from x in this.Rules(name)
			where x.IsConditionMet && x.Severity == Severity.Info
			select x;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A08 File Offset: 0x00000C08
		public sealed override void MakeReadOnly()
		{
			if (base.IsReadOnly)
			{
				return;
			}
			foreach (TConclusion tconclusion in this.Children)
			{
				tconclusion.MakeReadOnly();
			}
			base.MakeReadOnly();
			this.OnMakeReadonly();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A70 File Offset: 0x00000C70
		protected virtual void OnMakeReadonly()
		{
		}

		// Token: 0x04000006 RID: 6
		private TConclusion parent;

		// Token: 0x04000007 RID: 7
		private IList<TConclusion> children;
	}
}
