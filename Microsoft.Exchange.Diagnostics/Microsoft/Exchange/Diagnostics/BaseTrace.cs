using System;
using System.Collections;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000072 RID: 114
	public class BaseTrace
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public static ThreadTraceSettings CurrentThreadSettings
		{
			get
			{
				return PerThreadData.CurrentThreadData.ThreadTraceSettings;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006BBC File Offset: 0x00004DBC
		public BaseTrace(Guid guid, int traceTag)
		{
			this.category = guid;
			this.traceTag = traceTag;
			this.enabledTypes = ExTraceConfiguration.Instance.EnabledTypesArray();
			this.enabledTags = ExTraceConfiguration.Instance.EnabledTagArray(this.category);
			this.enabledInMemoryTags = ExTraceConfiguration.Instance.EnabledInMemoryTagArray(this.category);
			this.perThreadModeEnabledTags = ExTraceConfiguration.Instance.PerThreadModeTagArray(this.category);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00006C2F File Offset: 0x00004E2F
		public Guid Category
		{
			get
			{
				return this.category;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00006C37 File Offset: 0x00004E37
		public int TraceTag
		{
			get
			{
				return this.traceTag;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006C3F File Offset: 0x00004E3F
		public bool IsTraceEnabled(TraceType traceType)
		{
			if (this.TestHook != null)
			{
				return this.TestHook.IsTraceEnabled(traceType);
			}
			return this.enabledTypes[(int)traceType] && ExTraceInternal.AreAnyTraceProvidersEnabled && (this.IsOtherProviderTracesEnabled || this.IsInMemoryTraceEnabled);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00006C7D File Offset: 0x00004E7D
		protected bool IsInMemoryTraceEnabled
		{
			get
			{
				return this.enabledInMemoryTags[this.traceTag];
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00006C90 File Offset: 0x00004E90
		protected bool IsOtherProviderTracesEnabled
		{
			get
			{
				return this.enabledTags[this.traceTag] || (ExTraceConfiguration.Instance.PerThreadTracingConfigured && this.perThreadModeEnabledTags[this.traceTag] && BaseTrace.CurrentThreadSettings.IsEnabled);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00006CDD File Offset: 0x00004EDD
		internal ITraceTestHook TestHook
		{
			get
			{
				if (this.testHook == null)
				{
					return null;
				}
				return this.testHook.Value;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006CF4 File Offset: 0x00004EF4
		internal IDisposable SetTestHook(ITraceTestHook testHook)
		{
			if (this.testHook == null)
			{
				this.testHook = Hookable<ITraceTestHook>.Create(false, null);
			}
			return this.testHook.SetTestHook(testHook);
		}

		// Token: 0x04000247 RID: 583
		private Hookable<ITraceTestHook> testHook;

		// Token: 0x04000248 RID: 584
		private BitArray enabledTypes;

		// Token: 0x04000249 RID: 585
		private BitArray enabledTags;

		// Token: 0x0400024A RID: 586
		private BitArray enabledInMemoryTags;

		// Token: 0x0400024B RID: 587
		private BitArray perThreadModeEnabledTags;

		// Token: 0x0400024C RID: 588
		protected Guid category;

		// Token: 0x0400024D RID: 589
		protected int traceTag;
	}
}
