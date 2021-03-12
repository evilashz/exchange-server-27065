using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200023B RID: 571
	[Serializable]
	internal class XsoTaskStateProperty : XsoProperty, ITaskState, IProperty
	{
		// Token: 0x06001518 RID: 5400 RVA: 0x0007BA61 File Offset: 0x00079C61
		public XsoTaskStateProperty() : base(null)
		{
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x0007BA6C File Offset: 0x00079C6C
		public bool Complete
		{
			get
			{
				Task task = (Task)base.XsoItem;
				return task.IsComplete;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x0007BA8C File Offset: 0x00079C8C
		public ExDateTime? DateCompleted
		{
			get
			{
				Task task = (Task)base.XsoItem;
				ExDateTime? result = task.CompleteDate;
				if (result == null)
				{
					task.Load(new PropertyDefinition[]
					{
						ItemSchema.FlagCompleteTime
					});
					result = task.GetValueAsNullable<ExDateTime>(ItemSchema.FlagCompleteTime);
				}
				return result;
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0007BAD8 File Offset: 0x00079CD8
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			Task task = (Task)base.XsoItem;
			task.SetStatusNotStarted();
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0007BAF8 File Offset: 0x00079CF8
		protected override void InternalCopyFromModified(IProperty srcProperty)
		{
			Task task = (Task)base.XsoItem;
			ITaskState taskState = (ITaskState)srcProperty;
			if (taskState.Complete)
			{
				if (taskState.DateCompleted != null)
				{
					if (task.IsRecurring)
					{
						task.SuppressCreateOneOff = false;
					}
					task.SetCompleteTimesForUtcSession(taskState.DateCompleted.Value, taskState.DateCompleted);
					return;
				}
				throw new ConversionException("DateCompleted must be specified if Complete = 1");
			}
			else
			{
				if (taskState.DateCompleted != null)
				{
					throw new ConversionException("DateCompleted must not be specified if Complete = 0");
				}
				task.SetStatusNotStarted();
				return;
			}
		}
	}
}
