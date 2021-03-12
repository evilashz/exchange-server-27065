using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.Diagnostics;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x0200001B RID: 27
	[DataContract]
	public abstract class Command<TResult>
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00003DB3 File Offset: 0x00001FB3
		static Command()
		{
			ActivityContext.RegisterMetadata(typeof(EntitiesMetadata));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003DC4 File Offset: 0x00001FC4
		protected Command()
		{
			this.Id = Guid.NewGuid();
			this.OnDeserialized();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003DDD File Offset: 0x00001FDD
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003DE5 File Offset: 0x00001FE5
		[DataMember]
		public Guid Id { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003DEE File Offset: 0x00001FEE
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003DF6 File Offset: 0x00001FF6
		protected virtual CommandContext Context { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A3 RID: 163
		protected abstract ITracer Trace { get; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00003E00 File Offset: 0x00002000
		public TResult Execute(CommandContext context)
		{
			this.Context = context;
			Stopwatch stopwatch = Stopwatch.StartNew();
			TResult result;
			try
			{
				this.onBeforeExecute();
				result = this.OnExecute();
			}
			catch (Exception obj)
			{
				this.SetCustomLoggingData("Exception", obj);
				throw;
			}
			finally
			{
				stopwatch.Stop();
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null)
				{
					currentActivityScope.SetProperty(EntitiesMetadata.CommandName, base.GetType().Name);
					currentActivityScope.SetProperty(EntitiesMetadata.CoreExecutionLatency, stopwatch.ElapsedMilliseconds.ToString());
					string customLoggingData = this.GetCustomLoggingData();
					currentActivityScope.SetProperty(EntitiesMetadata.CustomData, customLoggingData);
				}
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003EB4 File Offset: 0x000020B4
		protected virtual void SetCustomLoggingData(string key, object obj)
		{
			if (obj != null)
			{
				IPropertyChangeTracker<PropertyDefinition> propertyChangeTracker = obj as IPropertyChangeTracker<PropertyDefinition>;
				try
				{
					string value = (propertyChangeTracker == null) ? obj.ToString() : EntityLogger.GetLoggingDetails(propertyChangeTracker);
					this.SetCustomLoggingData(key, value);
				}
				catch (Exception exception)
				{
					ExWatson.SendReport(exception, ReportOptions.DoNotCollectDumps | ReportOptions.DoNotLogProcessAndThreadIds | ReportOptions.DoNotFreezeThreads, string.Empty);
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003F08 File Offset: 0x00002108
		protected void RegisterOnBeforeExecute(Action action)
		{
			this.onBeforeExecute = (Action)Delegate.Combine(this.onBeforeExecute, action);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003F21 File Offset: 0x00002121
		protected virtual string GetCommandTraceDetails()
		{
			return string.Empty;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003F28 File Offset: 0x00002128
		protected virtual void UpdateCustomLoggingData()
		{
			this.SetCustomLoggingData("CommandContext", this.Context);
		}

		// Token: 0x060000A9 RID: 169
		protected abstract TResult OnExecute();

		// Token: 0x060000AA RID: 170 RVA: 0x00003F3B File Offset: 0x0000213B
		private void TraceExecution()
		{
			if (this.Trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.Trace.TraceDebug<string, string, CommandContext>((long)this.GetHashCode(), "{0}::Execute({1}){2}", base.GetType().Name, this.GetCommandTraceDetails(), this.Context);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003F79 File Offset: 0x00002179
		private void OnDeserialized()
		{
			this.RegisterOnBeforeExecute(new Action(this.TraceExecution));
			this.RegisterOnBeforeExecute(new Action(this.LogInputData));
			this.customLogData = new Dictionary<string, string>();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003FAA File Offset: 0x000021AA
		[OnDeserialized]
		private void OnDeserialized(StreamingContext streamingContext)
		{
			this.OnDeserialized();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003FB4 File Offset: 0x000021B4
		private void LogInputData()
		{
			try
			{
				this.UpdateCustomLoggingData();
			}
			catch (Exception exception)
			{
				ExWatson.SendReport(exception, ReportOptions.DoNotCollectDumps | ReportOptions.DoNotLogProcessAndThreadIds | ReportOptions.DoNotFreezeThreads, string.Empty);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003FEC File Offset: 0x000021EC
		private void SetCustomLoggingData(string key, string value)
		{
			this.customLogData[key] = value;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003FFC File Offset: 0x000021FC
		private string GetCustomLoggingData()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in this.customLogData)
			{
				stringBuilder.Append(string.Format("[{0}-{1}]", keyValuePair.Key, keyValuePair.Value));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000039 RID: 57
		private Action onBeforeExecute;

		// Token: 0x0400003A RID: 58
		private Dictionary<string, string> customLogData;
	}
}
