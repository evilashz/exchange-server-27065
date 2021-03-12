using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000201 RID: 513
	internal class TaskStatusConverter : EnumConverter
	{
		// Token: 0x06000D5D RID: 3421 RVA: 0x000435B8 File Offset: 0x000417B8
		public static string ToString(TaskStatus taskStatus)
		{
			string result = null;
			switch (taskStatus)
			{
			case TaskStatus.NotStarted:
				result = "NotStarted";
				break;
			case TaskStatus.InProgress:
				result = "InProgress";
				break;
			case TaskStatus.Completed:
				result = "Completed";
				break;
			case TaskStatus.WaitingOnOthers:
				result = "WaitingOnOthers";
				break;
			case TaskStatus.Deferred:
				result = "Deferred";
				break;
			}
			return result;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0004360C File Offset: 0x0004180C
		public static TaskStatus Parse(string taskStatusString)
		{
			if (taskStatusString != null)
			{
				TaskStatus result;
				if (!(taskStatusString == "NotStarted"))
				{
					if (!(taskStatusString == "InProgress"))
					{
						if (!(taskStatusString == "Completed"))
						{
							if (!(taskStatusString == "WaitingOnOthers"))
							{
								if (!(taskStatusString == "Deferred"))
								{
									goto IL_5E;
								}
								result = TaskStatus.Deferred;
							}
							else
							{
								result = TaskStatus.WaitingOnOthers;
							}
						}
						else
						{
							result = TaskStatus.Completed;
						}
					}
					else
					{
						result = TaskStatus.InProgress;
					}
				}
				else
				{
					result = TaskStatus.NotStarted;
				}
				return result;
			}
			IL_5E:
			throw new FormatException("Invalid taskStatus string: " + taskStatusString);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00043689 File Offset: 0x00041889
		public override object ConvertToObject(string propertyString)
		{
			return TaskStatusConverter.Parse(propertyString);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00043696 File Offset: 0x00041896
		public override string ConvertToString(object propertyValue)
		{
			return TaskStatusConverter.ToString((TaskStatus)propertyValue);
		}

		// Token: 0x04000A90 RID: 2704
		private const string NotStartedString = "NotStarted";

		// Token: 0x04000A91 RID: 2705
		private const string InProgressString = "InProgress";

		// Token: 0x04000A92 RID: 2706
		private const string CompletedString = "Completed";

		// Token: 0x04000A93 RID: 2707
		private const string WaitingOnOthersString = "WaitingOnOthers";

		// Token: 0x04000A94 RID: 2708
		private const string DeferredString = "Deferred";
	}
}
