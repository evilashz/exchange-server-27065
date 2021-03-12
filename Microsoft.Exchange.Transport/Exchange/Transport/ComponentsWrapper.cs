using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000011 RID: 17
	internal class ComponentsWrapper : IComponentsWrapper
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002CBA File Offset: 0x00000EBA
		public bool IsPaused
		{
			get
			{
				return Components.IsPaused;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002CC1 File Offset: 0x00000EC1
		public bool IsActive
		{
			get
			{
				return Components.IsActive;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public bool IsShuttingDown
		{
			get
			{
				return Components.ShuttingDown;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002CCF File Offset: 0x00000ECF
		public bool IsBridgeHead
		{
			get
			{
				return Components.IsBridgehead;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002CD6 File Offset: 0x00000ED6
		public object SyncRoot
		{
			get
			{
				return Components.SyncRoot;
			}
		}
	}
}
