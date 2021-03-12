using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x02000005 RID: 5
	public abstract class Servicelet : MarshalByRefObject
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002B90 File Offset: 0x00000D90
		public Servicelet()
		{
			this.thread = new Thread(new ThreadStart(this.Work));
			this.eventWaitHandles = new EventWaitHandle[2];
			this.eventWaitHandles[0] = new ManualResetEvent(false);
			this.eventWaitHandles[1] = new AutoResetEvent(false);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002BE3 File Offset: 0x00000DE3
		public ServerRole InstalledServerRoles
		{
			get
			{
				return this.serverRole;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002BEB File Offset: 0x00000DEB
		public bool IsRunningAsService
		{
			get
			{
				return this.runningAsService;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002BF3 File Offset: 0x00000DF3
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002BFB File Offset: 0x00000DFB
		public string ModuleName { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002C04 File Offset: 0x00000E04
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002C0C File Offset: 0x00000E0C
		public string TypeName { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002C15 File Offset: 0x00000E15
		protected WaitHandle StopEvent
		{
			get
			{
				return this.eventWaitHandles[0];
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002C1F File Offset: 0x00000E1F
		protected AutoResetEvent CustomCommandEvent
		{
			get
			{
				return (AutoResetEvent)this.eventWaitHandles[1];
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C2E File Offset: 0x00000E2E
		internal void SetRoleInfo(ServerRole role, bool isRunningAsService)
		{
			this.serverRole = role;
			this.runningAsService = isRunningAsService;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C3E File Offset: 0x00000E3E
		internal void AddCustomCommand(int customCommand)
		{
			if (this.supportedCustomCommands == null)
			{
				this.supportedCustomCommands = new List<int>();
			}
			this.supportedCustomCommands.Add(customCommand);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C5F File Offset: 0x00000E5F
		internal void Join()
		{
			this.thread.Join();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C6C File Offset: 0x00000E6C
		internal void OnStart()
		{
			this.thread.Start();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C79 File Offset: 0x00000E79
		internal void OnStop()
		{
			this.OnStopInternal();
			this.eventWaitHandles[0].Set();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C8F File Offset: 0x00000E8F
		protected virtual void OnStopInternal()
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C94 File Offset: 0x00000E94
		internal void OnCustomCommand(int command)
		{
			if (this.supportedCustomCommands != null && this.supportedCustomCommands.Contains(command))
			{
				lock (this.supportedCustomCommands)
				{
					this.OnCustomCommandInternal(command);
					this.CustomCommandEvent.Set();
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002CF8 File Offset: 0x00000EF8
		protected virtual void OnCustomCommandInternal(int command)
		{
		}

		// Token: 0x06000026 RID: 38
		public abstract void Work();

		// Token: 0x0400000D RID: 13
		private Thread thread;

		// Token: 0x0400000E RID: 14
		private EventWaitHandle[] eventWaitHandles;

		// Token: 0x0400000F RID: 15
		private ServerRole serverRole;

		// Token: 0x04000010 RID: 16
		private bool runningAsService;

		// Token: 0x04000011 RID: 17
		private List<int> supportedCustomCommands;

		// Token: 0x02000006 RID: 6
		private enum HandleIndex
		{
			// Token: 0x04000015 RID: 21
			Stop,
			// Token: 0x04000016 RID: 22
			CustomCommand
		}
	}
}
