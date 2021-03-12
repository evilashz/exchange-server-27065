using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x02000005 RID: 5
	public abstract class ConclusionSetImplementation<TConclusion, TSettingConclusion, TRuleConclusion> where TConclusion : ConclusionImplementation<TConclusion, TSettingConclusion, TRuleConclusion> where TSettingConclusion : TConclusion where TRuleConclusion : TConclusion, IRuleConclusion
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002CA6 File Offset: 0x00000EA6
		protected ConclusionSetImplementation()
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CAE File Offset: 0x00000EAE
		protected ConclusionSetImplementation(TConclusion root)
		{
			this.root = root;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002CBD File Offset: 0x00000EBD
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002CC5 File Offset: 0x00000EC5
		public TConclusion Root
		{
			get
			{
				return this.root;
			}
			set
			{
				this.ThrowIfReadOnly();
				this.root = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002CDC File Offset: 0x00000EDC
		public IEnumerable<TConclusion> Conclusions()
		{
			return this.root.DescendantsAndSelfWithoutExceptions();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002CEF File Offset: 0x00000EEF
		public IEnumerable<TConclusion> Conclusions(string name)
		{
			return this.root.DescendantsAndSelfWithoutExceptions(name);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D03 File Offset: 0x00000F03
		public IEnumerable<TConclusion> Exceptions()
		{
			return this.root.Exceptions();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D16 File Offset: 0x00000F16
		public IEnumerable<TConclusion> Exceptions(string name)
		{
			return this.root.Exceptions(name);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D2A File Offset: 0x00000F2A
		public IEnumerable<TSettingConclusion> Settings()
		{
			return this.root.Settings();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D3D File Offset: 0x00000F3D
		public IEnumerable<TSettingConclusion> Settings(string name)
		{
			return this.root.Settings(name);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D51 File Offset: 0x00000F51
		public IEnumerable<TRuleConclusion> Rules()
		{
			return this.root.Rules();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002D64 File Offset: 0x00000F64
		public IEnumerable<TRuleConclusion> Rules(string name)
		{
			return this.root.Rules(name);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D78 File Offset: 0x00000F78
		public IEnumerable<TRuleConclusion> Errors()
		{
			return this.root.Errors();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002D8B File Offset: 0x00000F8B
		public IEnumerable<TRuleConclusion> Errors(string name)
		{
			return this.root.Errors(name);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D9F File Offset: 0x00000F9F
		public IEnumerable<TRuleConclusion> Warnings()
		{
			return this.root.Warnings();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DB2 File Offset: 0x00000FB2
		public IEnumerable<TRuleConclusion> Warnings(string name)
		{
			return this.root.Warnings(name);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DC6 File Offset: 0x00000FC6
		public IEnumerable<TRuleConclusion> Info()
		{
			return this.root.Info();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002DD9 File Offset: 0x00000FD9
		public IEnumerable<TRuleConclusion> Info(string name)
		{
			return this.root.Info(name);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DED File Offset: 0x00000FED
		public void MakeReadOnly()
		{
			if (this.IsReadOnly)
			{
				return;
			}
			this.root.MakeReadOnly();
			this.isReadOnly = true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E10 File Offset: 0x00001010
		protected void ThrowIfReadOnly()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(Strings.CannotModifyReadOnlyProperty);
			}
		}

		// Token: 0x04000016 RID: 22
		private TConclusion root;

		// Token: 0x04000017 RID: 23
		private bool isReadOnly;
	}
}
