using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000040 RID: 64
	internal class TaskStatisticsTracker<T> where T : struct
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00009590 File Offset: 0x00007790
		private static Dictionary<string, int> CreateDictionary()
		{
			string[] names = Enum.GetNames(typeof(T));
			Dictionary<string, int> dictionary = new Dictionary<string, int>(names.Length);
			for (int i = 0; i < names.Length; i++)
			{
				dictionary.Add(names[i], i);
			}
			return dictionary;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000095CF File Offset: 0x000077CF
		internal TaskDefinition<T> TaskDefinition
		{
			get
			{
				return this.taskDefinition;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000095D7 File Offset: 0x000077D7
		internal TaskStatisticsTracker(TaskDefinition<T> taskDefinition)
		{
			this.taskDefinition = taskDefinition;
			this.latencyDistribution = new LatencyDistribution(taskDefinition.LatencyDistributionBoundaries, taskDefinition.OperationCount);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00009618 File Offset: 0x00007818
		internal bool ProcessEvent(Event eventObject)
		{
			List<Event> list = null;
			if (!this.eventBuffer.TryGetValue(eventObject.Id, out list))
			{
				if (this.eventBuffer.Count > 1024)
				{
					return false;
				}
				if (!this.taskDefinition.TaskName.Equals(eventObject.TaskName) || !this.taskDefinition.StartOperationName.Equals(eventObject.Type))
				{
					return false;
				}
				list = new List<Event>(20);
				this.eventBuffer[eventObject.Id] = list;
			}
			if (list.Count > 1024)
			{
				this.eventBuffer.Remove(eventObject.Id);
				return false;
			}
			list.Add(eventObject);
			if (!this.taskDefinition.TaskName.Equals(eventObject.TaskName) || !this.taskDefinition.EndOperationName.Equals(eventObject.Type))
			{
				return true;
			}
			this.eventBuffer.Remove(eventObject.Id);
			this.ProcessRequest(list);
			this.FlushDataIfNeeded();
			return true;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00009718 File Offset: 0x00007918
		private void ProcessRequest(List<Event> events)
		{
			int count = events.Count;
			Stack<Event> stack = new Stack<Event>(20);
			if (events.Count < 2)
			{
				return;
			}
			Event @event = events[0];
			Event event2 = events[events.Count - 1];
			if (event2.RelativeTimestamp < @event.RelativeTimestamp)
			{
				return;
			}
			int totalLatency = event2.RelativeTimestamp - @event.RelativeTimestamp;
			int count2 = event2.Count;
			int[] array = new int[this.taskDefinition.OperationCount];
			int[] array2 = new int[this.taskDefinition.OperationCount];
			int[] array3 = new int[this.taskDefinition.OperationCount];
			for (int i = 1; i < events.Count - 1; i++)
			{
				Event event3 = events[i];
				if (string.Equals(event3.Type, this.taskDefinition.StartOperationName))
				{
					if (string.IsNullOrEmpty(event3.Operation))
					{
						return;
					}
					stack.Push(event3);
				}
				else if (string.Equals(event3.Type, this.taskDefinition.EndOperationName))
				{
					if (stack.Count == 0 || !string.Equals(stack.Peek().Operation, event3.Operation))
					{
						return;
					}
					Event event4 = stack.Pop();
					if (!string.Equals(event4.Operation, event3.Operation))
					{
						return;
					}
					if (event4.RelativeTimestamp > event3.RelativeTimestamp)
					{
						return;
					}
					int num = event3.RelativeTimestamp - event4.RelativeTimestamp;
					int count3 = event3.Count;
					int num2;
					if (!TaskStatisticsTracker<T>.operationsNameToValue.TryGetValue(event3.Operation, out num2))
					{
						return;
					}
					array[num2] += num;
					array3[num2] = count3;
					array2[num2]++;
					object obj;
					if (event3.Properties.TryGetValue(this.taskDefinition.ErrorPropertyName, out obj))
					{
						string text = obj as string;
						if (string.IsNullOrEmpty(text))
						{
							return;
						}
						this.AddError(text);
					}
				}
			}
			this.latencyDistribution.AddSample(totalLatency, count2, array, array2, array3);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000992A File Offset: 0x00007B2A
		private void FlushDataIfNeeded()
		{
			this.processedCount++;
			if (this.processedCount < 64)
			{
				return;
			}
			this.processedCount = 0;
			this.WriteLatencyDistribution();
			this.WriteErrors();
			this.ClearTables();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00009960 File Offset: 0x00007B60
		private void WriteLatencyDistribution()
		{
			foreach (LatencyDistributionRange latencyDistributionRange in this.latencyDistribution.LatencyDistributionRanges)
			{
				KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[2 * this.taskDefinition.OperationCount + 4];
				if (latencyDistributionRange.Frequency > 0)
				{
					int num = 0;
					array[num++] = new KeyValuePair<string, object>(Names<LatencyDistribution.Properties>.Map[0], latencyDistributionRange.Name);
					array[num++] = new KeyValuePair<string, object>(Names<LatencyDistribution.Properties>.Map[3], latencyDistributionRange.Frequency);
					array[num++] = new KeyValuePair<string, object>(Names<LatencyDistribution.Properties>.Map[4], latencyDistributionRange.AverageTotalLatency);
					array[num++] = new KeyValuePair<string, object>(Names<LatencyDistribution.Properties>.Map[5], latencyDistributionRange.AverageResultSize);
					for (int j = 0; j < this.taskDefinition.OperationCount; j++)
					{
						array[j + num] = new KeyValuePair<string, object>(this.taskDefinition.OperationNames[j], latencyDistributionRange.OperationTimings[j]);
						array[j + num + this.taskDefinition.OperationCount] = new KeyValuePair<string, object>(this.taskDefinition.OperationNamesWithCountSuffix[j], latencyDistributionRange.OperationCounts[j]);
					}
					LogSearchService.Instance.HealthLog.LogEvent(this.taskDefinition.LatencyEvent, array);
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00009AFC File Offset: 0x00007CFC
		private void WriteErrors()
		{
			ErrorAndCount[] array = new ErrorAndCount[this.errors.Count];
			int i = 0;
			foreach (KeyValuePair<string, int> keyValuePair in this.errors)
			{
				array[i++] = new ErrorAndCount
				{
					Error = keyValuePair.Key,
					Count = keyValuePair.Value
				};
			}
			Array.Sort<ErrorAndCount>(array);
			int num = Math.Min(array.Length, 5);
			for (i = 0; i < num; i++)
			{
				LogSearchService.Instance.HealthLog.LogEvent(this.taskDefinition.ErrorEvent, new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("Name", array[i].Error),
					new KeyValuePair<string, object>("Count", array[i].Count)
				});
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009C0C File Offset: 0x00007E0C
		private void ClearTables()
		{
			this.latencyDistribution = new LatencyDistribution(this.taskDefinition.LatencyDistributionBoundaries, TaskStatisticsTracker<T>.OperationsCount);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009C2C File Offset: 0x00007E2C
		private void AddError(string error)
		{
			int num = 1;
			if (this.errors.TryGetValue(error, out num))
			{
				num++;
			}
			this.errors[error] = num;
		}

		// Token: 0x04000100 RID: 256
		private const int ProcessCountBeforeDumping = 64;

		// Token: 0x04000101 RID: 257
		private const int MaxBufferSizeBeforeDroppingEvents = 1024;

		// Token: 0x04000102 RID: 258
		private static Dictionary<string, int> operationsNameToValue = TaskStatisticsTracker<T>.CreateDictionary();

		// Token: 0x04000103 RID: 259
		internal static int OperationsCount = Enum.GetNames(typeof(Operations)).Length;

		// Token: 0x04000104 RID: 260
		private TaskDefinition<T> taskDefinition;

		// Token: 0x04000105 RID: 261
		private Dictionary<int, List<Event>> eventBuffer = new Dictionary<int, List<Event>>(50);

		// Token: 0x04000106 RID: 262
		private LatencyDistribution latencyDistribution;

		// Token: 0x04000107 RID: 263
		private Dictionary<string, int> errors = new Dictionary<string, int>(5);

		// Token: 0x04000108 RID: 264
		private int processedCount;
	}
}
