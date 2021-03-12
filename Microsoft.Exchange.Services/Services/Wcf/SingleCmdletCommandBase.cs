using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200097C RID: 2428
	internal abstract class SingleCmdletCommandBase<TRequest, TResponse, TTask, TResult> : OptionServiceCommandBase<TRequest, TResponse> where TResponse : OptionsResponseBase, new() where TTask : Task
	{
		// Token: 0x06004598 RID: 17816 RVA: 0x000F460B File Offset: 0x000F280B
		public SingleCmdletCommandBase(CallContext callContext, TRequest request, string cmdletName, ScopeLocation rbacScope) : base(callContext, request)
		{
			this.cmdletName = cmdletName;
			this.rbacScope = rbacScope;
		}

		// Token: 0x06004599 RID: 17817 RVA: 0x000F4624 File Offset: 0x000F2824
		protected override TResponse CreateTaskAndExecute()
		{
			TResponse result;
			using (PSLocalTask<TTask, TResult> pslocalTask = this.InvokeCmdletFactory())
			{
				this.cmdletRunner = new CmdletRunner<TTask, TResult>(base.CallContext, this.cmdletName, this.rbacScope, pslocalTask);
				this.PopulateTaskParameters();
				this.cmdletRunner.Execute();
				TResponse tresponse = Activator.CreateInstance<TResponse>();
				tresponse.WasSuccessful = true;
				TResponse tresponse2 = tresponse;
				this.PopulateResponseData(tresponse2);
				result = tresponse2;
			}
			return result;
		}

		// Token: 0x0600459A RID: 17818
		protected abstract PSLocalTask<TTask, TResult> InvokeCmdletFactory();

		// Token: 0x0600459B RID: 17819 RVA: 0x000F46A4 File Offset: 0x000F28A4
		protected virtual void PopulateTaskParameters()
		{
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x000F46A6 File Offset: 0x000F28A6
		protected virtual void PopulateResponseData(TResponse response)
		{
		}

		// Token: 0x04002872 RID: 10354
		private readonly string cmdletName;

		// Token: 0x04002873 RID: 10355
		private readonly ScopeLocation rbacScope;

		// Token: 0x04002874 RID: 10356
		protected CmdletRunner<TTask, TResult> cmdletRunner;
	}
}
