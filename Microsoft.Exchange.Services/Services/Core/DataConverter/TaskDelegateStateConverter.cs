using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000200 RID: 512
	internal class TaskDelegateStateConverter : EnumConverter
	{
		// Token: 0x06000D58 RID: 3416 RVA: 0x00043464 File Offset: 0x00041664
		public static string ToString(TaskDelegateState taskDelegateState)
		{
			string result = null;
			switch (taskDelegateState)
			{
			case TaskDelegateState.NoMatch:
				result = "NoMatch";
				break;
			case TaskDelegateState.OwnNew:
				result = "OwnNew";
				break;
			case TaskDelegateState.Owned:
				result = "Owned";
				break;
			case TaskDelegateState.Accepted:
				result = "Accepted";
				break;
			case TaskDelegateState.Declined:
				result = "Declined";
				break;
			case TaskDelegateState.Max:
				result = "Max";
				break;
			}
			return result;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000434C4 File Offset: 0x000416C4
		public static TaskDelegateState Parse(string taskDelegateStateString)
		{
			if (taskDelegateStateString != null)
			{
				if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6000cb1-1 == null)
				{
					<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6000cb1-1 = new Dictionary<string, int>(6)
					{
						{
							"NoMatch",
							0
						},
						{
							"OwnNew",
							1
						},
						{
							"Owned",
							2
						},
						{
							"Accepted",
							3
						},
						{
							"Declined",
							4
						},
						{
							"Max",
							5
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{3E8D983F-1A9C-416D-80C3-9D441BA4C28E}.$$method0x6000cb1-1.TryGetValue(taskDelegateStateString, out num))
				{
					TaskDelegateState result;
					switch (num)
					{
					case 0:
						result = TaskDelegateState.NoMatch;
						break;
					case 1:
						result = TaskDelegateState.OwnNew;
						break;
					case 2:
						result = TaskDelegateState.Owned;
						break;
					case 3:
						result = TaskDelegateState.Accepted;
						break;
					case 4:
						result = TaskDelegateState.Declined;
						break;
					case 5:
						result = TaskDelegateState.Max;
						break;
					default:
						goto IL_B1;
					}
					return result;
				}
			}
			IL_B1:
			throw new FormatException("Invalid taskDelegateState string: " + taskDelegateStateString);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00043594 File Offset: 0x00041794
		public override object ConvertToObject(string propertyString)
		{
			return TaskDelegateStateConverter.Parse(propertyString);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000435A1 File Offset: 0x000417A1
		public override string ConvertToString(object propertyValue)
		{
			return TaskDelegateStateConverter.ToString((TaskDelegateState)propertyValue);
		}

		// Token: 0x04000A8A RID: 2698
		private const string NoMatchString = "NoMatch";

		// Token: 0x04000A8B RID: 2699
		private const string OwnNewString = "OwnNew";

		// Token: 0x04000A8C RID: 2700
		private const string OwnedString = "Owned";

		// Token: 0x04000A8D RID: 2701
		private const string AcceptedString = "Accepted";

		// Token: 0x04000A8E RID: 2702
		private const string DeclinedString = "Declined";

		// Token: 0x04000A8F RID: 2703
		private const string MaxString = "Max";
	}
}
