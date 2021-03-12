using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.QueueViewer;

namespace Microsoft.Exchange.Management.QueueViewerTasks
{
	// Token: 0x02000073 RID: 115
	public abstract class MessageAction : Task
	{
		// Token: 0x06000413 RID: 1043
		protected abstract void RunAction();

		// Token: 0x06000414 RID: 1044
		protected abstract LocalizedException GetLocalizedException(Exception ex);

		// Token: 0x06000415 RID: 1045 RVA: 0x00010110 File Offset: 0x0000E310
		protected override void InternalProcessRecord()
		{
			try
			{
				int num = 10;
				while (num-- > 0)
				{
					try
					{
						this.RunAction();
						break;
					}
					catch (RpcException ex)
					{
						if ((ex.ErrorCode != 1753 && ex.ErrorCode != 1727) || num == 0)
						{
							throw;
						}
					}
				}
			}
			catch (QueueViewerException ex2)
			{
				base.WriteError(this.GetLocalizedException(ex2) ?? this.GetDefaultException(ex2), ErrorCategory.InvalidOperation, null);
			}
			catch (RpcException ex3)
			{
				base.WriteError(this.GetLocalizedException(ex3) ?? this.GetDefaultException(ex3), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000101B8 File Offset: 0x0000E3B8
		private Exception GetDefaultException(QueueViewerException ex)
		{
			return ErrorMapper.GetLocalizedException(ex.ErrorCode, null, null);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000101C7 File Offset: 0x0000E3C7
		private Exception GetDefaultException(RpcException ex)
		{
			return ErrorMapper.GetLocalizedException(ex.ErrorCode, null, null);
		}

		// Token: 0x0400017D RID: 381
		private const int taskExecutionRetryCount = 10;
	}
}
