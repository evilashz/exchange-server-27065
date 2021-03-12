using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x0200000B RID: 11
	public abstract class Servicelet : MarshalByRefObject
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003790 File Offset: 0x00001990
		public Servicelet()
		{
			this.thread = new Thread(new ThreadStart(this.Work));
			this.eventWaitHandles = new EventWaitHandle[2];
			this.eventWaitHandles[0] = new ManualResetEvent(false);
			this.eventWaitHandles[1] = new AutoResetEvent(false);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000037E3 File Offset: 0x000019E3
		public ServerRole InstalledServerRoles
		{
			get
			{
				return this.serverRole;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000037EB File Offset: 0x000019EB
		public bool IsRunningAsService
		{
			get
			{
				return this.runningAsService;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000037F3 File Offset: 0x000019F3
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000037FB File Offset: 0x000019FB
		public string ModuleName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003804 File Offset: 0x00001A04
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000380C File Offset: 0x00001A0C
		public string TypeName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003815 File Offset: 0x00001A15
		protected WaitHandle StopEvent
		{
			get
			{
				return this.eventWaitHandles[0];
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000381F File Offset: 0x00001A1F
		protected AutoResetEvent CustomCommandEvent
		{
			get
			{
				return (AutoResetEvent)this.eventWaitHandles[1];
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000382E File Offset: 0x00001A2E
		internal void SetRoleInfo(ServerRole role, bool isRunningAsService)
		{
			this.serverRole = role;
			this.runningAsService = isRunningAsService;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000383E File Offset: 0x00001A3E
		internal void AddCustomCommand(int customCommand)
		{
			if (this.supportedCustomCommands == null)
			{
				this.supportedCustomCommands = new List<int>();
			}
			this.supportedCustomCommands.Add(customCommand);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000385F File Offset: 0x00001A5F
		internal void Join()
		{
			this.thread.Join();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000386C File Offset: 0x00001A6C
		internal void OnStart()
		{
			this.thread.Start();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003879 File Offset: 0x00001A79
		internal void OnStop()
		{
			this.OnStopInternal();
			this.eventWaitHandles[0].Set();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000388F File Offset: 0x00001A8F
		protected virtual void OnStopInternal()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003894 File Offset: 0x00001A94
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

		// Token: 0x06000056 RID: 86 RVA: 0x000038F8 File Offset: 0x00001AF8
		protected virtual void OnCustomCommandInternal(int command)
		{
		}

		// Token: 0x06000057 RID: 87
		public abstract void Work();

		// Token: 0x0400002A RID: 42
		private Thread thread;

		// Token: 0x0400002B RID: 43
		private EventWaitHandle[] eventWaitHandles;

		// Token: 0x0400002C RID: 44
		private ServerRole serverRole;

		// Token: 0x0400002D RID: 45
		private bool runningAsService;

		// Token: 0x0400002E RID: 46
		private List<int> supportedCustomCommands;

		// Token: 0x0200000C RID: 12
		private enum HandleIndex
		{
			// Token: 0x04000032 RID: 50
			Stop,
			// Token: 0x04000033 RID: 51
			CustomCommand
		}
	}
}
