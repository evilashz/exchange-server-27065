using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000A2 RID: 162
	public class TaskContext
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00017E64 File Offset: 0x00016064
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00017E6C File Offset: 0x0001606C
		internal Guid UniqueId { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00017E75 File Offset: 0x00016075
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00017E7D File Offset: 0x0001607D
		internal TaskInvocationInfo InvocationInfo { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00017E86 File Offset: 0x00016086
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00017E8E File Offset: 0x0001608E
		internal ExchangeRunspaceConfiguration ExchangeRunspaceConfig { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00017E97 File Offset: 0x00016097
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00017E9F File Offset: 0x0001609F
		internal ICommandShell CommandShell { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00017EA8 File Offset: 0x000160A8
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00017EB0 File Offset: 0x000160B0
		internal TaskUserInfo UserInfo { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00017EB9 File Offset: 0x000160B9
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00017EC1 File Offset: 0x000160C1
		internal ScopeSet ScopeSet { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x00017ECA File Offset: 0x000160CA
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x00017ED2 File Offset: 0x000160D2
		internal ISessionState SessionState { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00017EDB File Offset: 0x000160DB
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x00017EE3 File Offset: 0x000160E3
		internal TaskStage Stage { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00017EEC File Offset: 0x000160EC
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x00017EF4 File Offset: 0x000160F4
		internal int CurrentObjectIndex { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00017EFD File Offset: 0x000160FD
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00017F05 File Offset: 0x00016105
		internal TaskErrorInfo ErrorInfo { get; private set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00017F0E File Offset: 0x0001610E
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x00017F16 File Offset: 0x00016116
		internal bool ShouldTerminateCmdletExecution { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00017F1F File Offset: 0x0001611F
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x00017F27 File Offset: 0x00016127
		internal IDictionary<string, object> Items { get; private set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00017F30 File Offset: 0x00016130
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x00017F38 File Offset: 0x00016138
		internal bool WasCancelled { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00017F41 File Offset: 0x00016141
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00017F49 File Offset: 0x00016149
		internal bool WasStopped { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00017F52 File Offset: 0x00016152
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x00017F5A File Offset: 0x0001615A
		internal bool ObjectWrittenToPipeline { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00017F63 File Offset: 0x00016163
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x00017F6B File Offset: 0x0001616B
		internal ADServerSettings ServerSettingsAfterFailOver { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00017F74 File Offset: 0x00016174
		internal bool CanBypassRBACScope
		{
			get
			{
				return this.ExchangeRunspaceConfig == null || (this.InvocationInfo.IsInternalOrigin && this.IsScriptInUserRole);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00017F95 File Offset: 0x00016195
		private bool IsScriptInUserRole
		{
			get
			{
				return this.ExchangeRunspaceConfig.IsScriptInUserRole(this.InvocationInfo.ScriptName) || this.ExchangeRunspaceConfig.IsScriptInUserRole(this.InvocationInfo.RootScriptName);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00017FC7 File Offset: 0x000161C7
		internal TaskContext(ICommandShell commandShell)
		{
			this.CommandShell = commandShell;
			this.UniqueId = Guid.NewGuid();
			this.CurrentObjectIndex = -1;
			this.Items = new Dictionary<string, object>();
			this.ErrorInfo = new TaskErrorInfo();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00018000 File Offset: 0x00016200
		internal bool TryGetItem<T>(string key, ref T value)
		{
			object obj;
			if (this.Items.TryGetValue(key, out obj))
			{
				value = (T)((object)obj);
				return true;
			}
			return false;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001802C File Offset: 0x0001622C
		public void Reset()
		{
			if (this.ErrorInfo != null)
			{
				this.ErrorInfo.ResetErrorInfo();
			}
			this.WasCancelled = false;
			this.ServerSettingsAfterFailOver = null;
		}
	}
}
