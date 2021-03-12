using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003BE RID: 958
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DebuggableAttribute : Attribute
	{
		// Token: 0x060031EA RID: 12778 RVA: 0x000C013E File Offset: 0x000BE33E
		public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
		{
			this.m_debuggingModes = DebuggableAttribute.DebuggingModes.None;
			if (isJITTrackingEnabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.Default;
			}
			if (isJITOptimizerDisabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
			}
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000C0173 File Offset: 0x000BE373
		[__DynamicallyInvokable]
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.m_debuggingModes = modes;
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x000C0182 File Offset: 0x000BE382
		public bool IsJITTrackingEnabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.Default) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000C018F File Offset: 0x000BE38F
		public bool IsJITOptimizerDisabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000C01A0 File Offset: 0x000BE3A0
		public DebuggableAttribute.DebuggingModes DebuggingFlags
		{
			get
			{
				return this.m_debuggingModes;
			}
		}

		// Token: 0x040015E7 RID: 5607
		private DebuggableAttribute.DebuggingModes m_debuggingModes;

		// Token: 0x02000B4A RID: 2890
		[Flags]
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public enum DebuggingModes
		{
			// Token: 0x040033ED RID: 13293
			[__DynamicallyInvokable]
			None = 0,
			// Token: 0x040033EE RID: 13294
			[__DynamicallyInvokable]
			Default = 1,
			// Token: 0x040033EF RID: 13295
			[__DynamicallyInvokable]
			DisableOptimizations = 256,
			// Token: 0x040033F0 RID: 13296
			[__DynamicallyInvokable]
			IgnoreSymbolStoreSequencePoints = 2,
			// Token: 0x040033F1 RID: 13297
			[__DynamicallyInvokable]
			EnableEditAndContinue = 4
		}
	}
}
