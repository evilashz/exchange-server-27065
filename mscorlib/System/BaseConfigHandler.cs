using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	// Token: 0x020000BB RID: 187
	internal abstract class BaseConfigHandler
	{
		// Token: 0x06000ADE RID: 2782 RVA: 0x00022637 File Offset: 0x00020837
		public BaseConfigHandler()
		{
			this.InitializeCallbacks();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00022648 File Offset: 0x00020848
		private void InitializeCallbacks()
		{
			if (this.eventCallbacks == null)
			{
				this.eventCallbacks = new Delegate[6];
				this.eventCallbacks[0] = new BaseConfigHandler.NotifyEventCallback(this.NotifyEvent);
				this.eventCallbacks[1] = new BaseConfigHandler.BeginChildrenCallback(this.BeginChildren);
				this.eventCallbacks[2] = new BaseConfigHandler.EndChildrenCallback(this.EndChildren);
				this.eventCallbacks[3] = new BaseConfigHandler.ErrorCallback(this.Error);
				this.eventCallbacks[4] = new BaseConfigHandler.CreateNodeCallback(this.CreateNode);
				this.eventCallbacks[5] = new BaseConfigHandler.CreateAttributeCallback(this.CreateAttribute);
			}
		}

		// Token: 0x06000AE0 RID: 2784
		public abstract void NotifyEvent(ConfigEvents nEvent);

		// Token: 0x06000AE1 RID: 2785
		public abstract void BeginChildren(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE2 RID: 2786
		public abstract void EndChildren(int fEmpty, int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE3 RID: 2787
		public abstract void Error(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE4 RID: 2788
		public abstract void CreateNode(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE5 RID: 2789
		public abstract void CreateAttribute(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x06000AE6 RID: 2790
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RunParser(string fileName);

		// Token: 0x0400044C RID: 1100
		protected Delegate[] eventCallbacks;

		// Token: 0x02000AA8 RID: 2728
		// (Invoke) Token: 0x060068BA RID: 26810
		private delegate void NotifyEventCallback(ConfigEvents nEvent);

		// Token: 0x02000AA9 RID: 2729
		// (Invoke) Token: 0x060068BE RID: 26814
		private delegate void BeginChildrenCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x02000AAA RID: 2730
		// (Invoke) Token: 0x060068C2 RID: 26818
		private delegate void EndChildrenCallback(int fEmpty, int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x02000AAB RID: 2731
		// (Invoke) Token: 0x060068C6 RID: 26822
		private delegate void ErrorCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x02000AAC RID: 2732
		// (Invoke) Token: 0x060068CA RID: 26826
		private delegate void CreateNodeCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

		// Token: 0x02000AAD RID: 2733
		// (Invoke) Token: 0x060068CE RID: 26830
		private delegate void CreateAttributeCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);
	}
}
