using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005E7 RID: 1511
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public class LocalVariableInfo
	{
		// Token: 0x060046EC RID: 18156 RVA: 0x0010167E File Offset: 0x000FF87E
		[__DynamicallyInvokable]
		protected LocalVariableInfo()
		{
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00101688 File Offset: 0x000FF888
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string text = string.Concat(new object[]
			{
				this.LocalType.ToString(),
				" (",
				this.LocalIndex,
				")"
			});
			if (this.IsPinned)
			{
				text += " (pinned)";
			}
			return text;
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x001016E2 File Offset: 0x000FF8E2
		[__DynamicallyInvokable]
		public virtual Type LocalType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x001016EA File Offset: 0x000FF8EA
		[__DynamicallyInvokable]
		public virtual bool IsPinned
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_isPinned != 0;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x060046F0 RID: 18160 RVA: 0x001016F5 File Offset: 0x000FF8F5
		[__DynamicallyInvokable]
		public virtual int LocalIndex
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_localIndex;
			}
		}

		// Token: 0x04001D22 RID: 7458
		private RuntimeType m_type;

		// Token: 0x04001D23 RID: 7459
		private int m_isPinned;

		// Token: 0x04001D24 RID: 7460
		private int m_localIndex;
	}
}
