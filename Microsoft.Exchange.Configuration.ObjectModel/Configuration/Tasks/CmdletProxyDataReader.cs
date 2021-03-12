using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000046 RID: 70
	internal class CmdletProxyDataReader : IEnumerable<PSObject>, IEnumerable, IDisposable
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000C4B3 File Offset: 0x0000A6B3
		private void AssertReaderIsOpen()
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("Reader is already closed.");
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		public CmdletProxyDataReader(RunspaceMediator runspaceMediator, PSCommand cmd, Task.TaskWarningLoggingDelegate writeWarning)
		{
			this.runspaceProxy = new RunspaceProxy(runspaceMediator, true);
			bool flag = false;
			try
			{
				this.shellProxy = new PowerShellProxy(this.runspaceProxy, cmd);
				this.writeWarning = writeWarning;
				this.asyncResult = (PowerShellAsyncResult<PSObject>)this.shellProxy.BeginInvoke(null, null);
				PSDataCollection<PSObject> output = this.asyncResult.Output;
				this.shellProxy.PowerShell.InvocationStateChanged += this.PipelineStateChanged;
				output.DataAdded += this.PipelineDataAdded;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Close();
				}
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C580 File Offset: 0x0000A780
		private bool Read()
		{
			this.AssertReaderIsOpen();
			this.currentRecord = null;
			PSDataCollection<PSObject> output = this.asyncResult.Output;
			if (this.WaitOne(output))
			{
				this.currentRecord = output[0];
				output.RemoveAt(0);
			}
			if (this.currentRecord == null && this.shellProxy.Errors != null && this.shellProxy.Errors.Count != 0 && this.shellProxy.Errors[0].Exception != null)
			{
				try
				{
					throw this.shellProxy.Errors[0].Exception;
				}
				finally
				{
					this.Close();
				}
			}
			if (this.shellProxy != null && this.shellProxy.Warnings != null && this.shellProxy.Warnings.Count != 0)
			{
				foreach (WarningRecord warningRecord in this.shellProxy.Warnings)
				{
					this.writeWarning(new LocalizedString(warningRecord.Message));
				}
			}
			if (this.currentRecord == null)
			{
				this.Close();
			}
			return null != this.currentRecord;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		public void Close()
		{
			if (!this.IsClosed)
			{
				try
				{
					if (this.shellProxy != null && this.asyncResult != null)
					{
						this.shellProxy.EndInvoke(this.asyncResult);
					}
				}
				finally
				{
					if (this.shellProxy != null)
					{
						this.shellProxy.PowerShell.InvocationStateChanged -= this.PipelineStateChanged;
					}
					if (this.asyncResult != null)
					{
						PSDataCollection<PSObject> output = this.asyncResult.Output;
						if (output != null)
						{
							output.DataAdded -= this.PipelineDataAdded;
						}
					}
					this.UnblockWaitOne();
					this.isClosed = true;
					if (this.runspaceProxy != null)
					{
						this.runspaceProxy.Dispose();
						this.runspaceProxy = null;
					}
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000C788 File Offset: 0x0000A988
		public bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C790 File Offset: 0x0000A990
		public PSObject CurrentRecord
		{
			get
			{
				return this.currentRecord;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000C81C File Offset: 0x0000AA1C
		public IEnumerator<PSObject> GetEnumerator()
		{
			while (this.Read())
			{
				yield return this.CurrentRecord;
			}
			yield break;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000C838 File Offset: 0x0000AA38
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C840 File Offset: 0x0000AA40
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000C84F File Offset: 0x0000AA4F
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000C85A File Offset: 0x0000AA5A
		protected virtual int GetMaxTimeoutMinutes()
		{
			return 15;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C860 File Offset: 0x0000AA60
		~CmdletProxyDataReader()
		{
			this.Dispose(false);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C890 File Offset: 0x0000AA90
		private void PipelineDataAdded(object sender, DataAddedEventArgs e)
		{
			this.UnblockWaitOne();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000C898 File Offset: 0x0000AA98
		private void PipelineStateChanged(object sender, PSInvocationStateChangedEventArgs e)
		{
			if (e.InvocationStateInfo.State == PSInvocationState.Failed || e.InvocationStateInfo.State == PSInvocationState.Stopped || e.InvocationStateInfo.State == PSInvocationState.Completed)
			{
				this.UnblockWaitOne();
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000C8CC File Offset: 0x0000AACC
		private bool WaitOne(PSDataCollection<PSObject> output)
		{
			lock (this.syncObject)
			{
				ExDateTime now = ExDateTime.Now;
				while (output.IsOpen && output.Count == 0 && (this.shellProxy.PowerShell.InvocationStateInfo.State == PSInvocationState.NotStarted || this.shellProxy.PowerShell.InvocationStateInfo.State == PSInvocationState.Running))
				{
					if (!Monitor.Wait(this.syncObject, TimeSpan.FromMinutes((double)this.GetMaxTimeoutMinutes())))
					{
						ExDateTime now2 = ExDateTime.Now;
						throw new TimeoutException(Strings.PowerShellTimeout((int)(now2 - now).TotalMinutes));
					}
				}
			}
			return output.Count > 0;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000C998 File Offset: 0x0000AB98
		private void UnblockWaitOne()
		{
			lock (this.syncObject)
			{
				Monitor.PulseAll(this.syncObject);
			}
		}

		// Token: 0x040000C7 RID: 199
		private const int MaxWaitMinutes = 15;

		// Token: 0x040000C8 RID: 200
		private PowerShellProxy shellProxy;

		// Token: 0x040000C9 RID: 201
		private PowerShellAsyncResult<PSObject> asyncResult;

		// Token: 0x040000CA RID: 202
		private RunspaceProxy runspaceProxy;

		// Token: 0x040000CB RID: 203
		private PSObject currentRecord;

		// Token: 0x040000CC RID: 204
		private bool isClosed;

		// Token: 0x040000CD RID: 205
		private object syncObject = new object();

		// Token: 0x040000CE RID: 206
		private readonly Task.TaskWarningLoggingDelegate writeWarning;
	}
}
