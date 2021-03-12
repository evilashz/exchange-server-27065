using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.StoreTasks;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000981 RID: 2433
	internal sealed class SetCalendarProcessingCommand : SingleCmdletCommandBase<SetCalendarProcessingRequest, OptionsResponseBase, SetCalendarProcessing, CalendarConfiguration>
	{
		// Token: 0x060045AC RID: 17836 RVA: 0x000F4AF3 File Offset: 0x000F2CF3
		public SetCalendarProcessingCommand(CallContext callContext, SetCalendarProcessingRequest request) : base(callContext, request, "Set-CalendarProcessing", ScopeLocation.RecipientWrite)
		{
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x000F4B04 File Offset: 0x000F2D04
		protected override void PopulateTaskParameters()
		{
			PSLocalTask<SetCalendarProcessing, CalendarConfiguration> taskWrapper = this.cmdletRunner.TaskWrapper;
			this.cmdletRunner.SetTaskParameter("Identity", taskWrapper.Task, new MailboxIdParameter(base.CallContext.AccessingPrincipal.ObjectId));
			CalendarConfiguration taskParameters = (CalendarConfiguration)taskWrapper.Task.GetDynamicParameters();
			this.cmdletRunner.SetRemainingModifiedTaskParameters(this.request.Options, taskParameters);
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x000F4B70 File Offset: 0x000F2D70
		protected override PSLocalTask<SetCalendarProcessing, CalendarConfiguration> InvokeCmdletFactory()
		{
			return CmdletTaskFactory.Instance.CreateSetCalendarProcessingTask(base.CallContext.AccessingPrincipal);
		}
	}
}
