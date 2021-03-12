using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200000B RID: 11
	internal sealed class NspiBindDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000045E1 File Offset: 0x000027E1
		public NspiBindDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, NspiContext context, NspiBindFlags flags, NspiState state, Guid? serverGuid) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.clientBinding = clientBinding;
			this.flags = flags;
			this.serverGuid = serverGuid;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004614 File Offset: 0x00002814
		public NspiStatus End(out Guid? guid, out int contextHandle)
		{
			base.CheckDisposed();
			bool flag = false;
			NspiStatus status;
			try
			{
				base.CheckCompletion();
				guid = this.serverGuid;
				if (base.Status == NspiStatus.Success)
				{
					contextHandle = base.ContextHandle;
				}
				else
				{
					contextHandle = 0;
				}
				flag = true;
				status = base.Status;
			}
			finally
			{
				if (!flag || base.Status != NspiStatus.Success)
				{
					base.IsContextRundown = true;
				}
			}
			return status;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004680 File Offset: 0x00002880
		protected override string TaskName
		{
			get
			{
				return "NspiBind";
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004688 File Offset: 0x00002888
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiBindFlags, string>((long)base.ContextHandle, "{0} params: flags={1}, guid={2}", this.TaskName, this.flags, (this.serverGuid != null) ? this.serverGuid.Value.ToString() : "<null>");
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004700 File Offset: 0x00002900
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			this.serverGuid = new Guid?(base.Context.Guid);
			base.Context.ProtocolLogSession[ProtocolLog.Field.Authentication] = this.clientBinding.AuthenticationType.ToString();
			if (!base.Context.IsAnonymous && Configuration.EncryptionRequired && !this.clientBinding.IsEncrypted)
			{
				NspiDispatchTask.NspiTracer.TraceError((long)base.ContextHandle, "Encrypted connection is required.");
				throw new NspiException(NspiStatus.NotSupported, "EncryptionRequired");
			}
			base.NspiContextCallWrapper("Bind", () => base.Context.Bind(this.flags, base.NspiState));
			if (base.Status == NspiStatus.Success)
			{
				try
				{
					base.Context.Budget.StartConnection("NspiBindDispatchTask");
				}
				catch (OverBudgetException)
				{
					NspiDispatchTask.NspiTracer.TraceDebug<string>((long)base.ContextHandle, "Too many connections for user {0}", base.Context.UserIdentity);
					throw new NspiException(NspiStatus.GeneralFailure, "TooManyConnections");
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004810 File Offset: 0x00002A10
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiBindDispatchTask>(this);
		}

		// Token: 0x0400003B RID: 59
		private const string Name = "NspiBind";

		// Token: 0x0400003C RID: 60
		private readonly ClientBinding clientBinding;

		// Token: 0x0400003D RID: 61
		private readonly NspiBindFlags flags;

		// Token: 0x0400003E RID: 62
		private Guid? serverGuid = null;
	}
}
