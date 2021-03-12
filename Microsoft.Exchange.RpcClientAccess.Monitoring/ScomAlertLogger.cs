using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000044 RID: 68
	internal class ScomAlertLogger : BaseLogger
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000613D File Offset: 0x0000433D
		public ScomAlertLogger(Action<LocalizedString> logOutputAction = null)
		{
			if (logOutputAction != null)
			{
				base.LogOutput += logOutputAction;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000615A File Offset: 0x0000435A
		public override void TaskStarted(ITaskDescriptor task)
		{
			this.taskStack.Push(task);
			this.LogTaskCaption(task);
			this.LogInputProperties(task);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006178 File Offset: 0x00004378
		public override void TaskCompleted(ITaskDescriptor task, TaskResult result)
		{
			this.LogOutputProperties(task);
			if (this.taskStack.Count == 0 || this.taskStack.Peek() != task)
			{
				string message = string.Format("Task structure violated; Expected to complete task \"{0}\", requested to complete task \"{1}\"", (this.taskStack.Count > 0) ? this.taskStack.Peek().TaskTitle : "< empty stack >", task.TaskTitle);
				throw new InvalidOperationException(message);
			}
			if (result == TaskResult.Success)
			{
				this.LogHierarchicalOutput(-1, Strings.ScomAlertLoggerTaskSucceeded(task.TaskTitle));
			}
			else
			{
				this.LogHierarchicalOutput(-1, Strings.ScomAlertLoggerTaskFailed(task.TaskTitle));
			}
			this.taskStack.Pop();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006224 File Offset: 0x00004424
		protected ITaskDescriptor GetCurrentTask()
		{
			return this.taskStack.Peek();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006231 File Offset: 0x00004431
		protected virtual void LogTaskCaption(ITaskDescriptor task)
		{
			this.LogHierarchicalOutput(-1, Strings.ScomAlertLoggerTaskStarted(task.TaskTitle));
			this.LogHierarchicalOutput(Strings.ScomAlertLoggerTaskDescription(task.TaskDescription));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006256 File Offset: 0x00004456
		protected virtual void LogInputProperties(ITaskDescriptor task)
		{
			this.LogProperties(this.GetPropertyFeed(task, ContextProperty.AccessMode.Get), Strings.ScomAlertLoggerTaskStartProperties);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000626B File Offset: 0x0000446B
		protected virtual void LogOutputProperties(ITaskDescriptor task)
		{
			this.LogProperties(this.GetPropertyFeed(task, ContextProperty.AccessMode.Set), Strings.ScomAlertLoggerTaskCompletedProperties);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006280 File Offset: 0x00004480
		protected void LogProperties(IEnumerable<KeyValuePair<ContextProperty, string>> feed, LocalizedString caption)
		{
			bool flag = true;
			foreach (KeyValuePair<ContextProperty, string> keyValuePair in feed)
			{
				if (flag)
				{
					this.LogHierarchicalOutput(caption);
					flag = false;
				}
				this.LogHierarchicalOutput(Strings.ScomAlertLoggerTaskProperty(keyValuePair.Key.ToString(), keyValuePair.Value));
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006528 File Offset: 0x00004728
		protected virtual IEnumerable<KeyValuePair<ContextProperty, string>> GetPropertyFeed(ITaskDescriptor task, ContextProperty.AccessMode forAccessMode)
		{
			foreach (ContextProperty property in (from prop in task.DependentProperties
			where (prop.AllowedAccessMode & forAccessMode) == forAccessMode
			select prop).Distinct<ContextProperty>())
			{
				object value;
				if (task.Properties.TryGet(property, out value))
				{
					yield return new KeyValuePair<ContextProperty, string>(property, this.StringizePropertyValue(property, value));
				}
			}
			yield break;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006590 File Offset: 0x00004790
		protected string StringizePropertyValue(ContextProperty property, object value)
		{
			if (value == null)
			{
				return Strings.ScomAlertLoggerTaskPropertyNullValue;
			}
			if (value is NetworkCredential)
			{
				NetworkCredential networkCredential = (NetworkCredential)value;
				return Strings.NetworkCredentialString(networkCredential.Domain, networkCredential.UserName);
			}
			if (value is Array)
			{
				return (from object entry in (IEnumerable)value
				select this.StringizePropertyValue(property, entry)).Aggregate(default(LocalizedString), delegate(LocalizedString list, string entry)
				{
					if (!list.IsEmpty)
					{
						return Strings.ListOfItems(list, entry);
					}
					return new LocalizedString(entry);
				});
			}
			return value.ToString();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006647 File Offset: 0x00004847
		protected void LogHierarchicalOutput(LocalizedString message)
		{
			this.LogHierarchicalOutput(0, message);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000665C File Offset: 0x0000485C
		protected void LogHierarchicalOutput(int indentShift, LocalizedString message)
		{
			if (this.ShouldLogTask(this.GetCurrentTask()))
			{
				int num = indentShift + this.taskStack.Select(new Func<ITaskDescriptor, bool>(this.ShouldLogTask)).Aggregate(0, delegate(int level, bool shouldLog)
				{
					if (!shouldLog)
					{
						return level;
					}
					return level + 1;
				});
				LocalizedString localizedString = message;
				for (int i = 0; i < num; i++)
				{
					localizedString = Strings.ScomAlertLoggerIndent(localizedString);
				}
				this.OnLogOutput(localizedString);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000066D1 File Offset: 0x000048D1
		protected virtual bool ShouldLogTask(ITaskDescriptor task)
		{
			return true;
		}

		// Token: 0x040000CD RID: 205
		private readonly Stack<ITaskDescriptor> taskStack = new Stack<ITaskDescriptor>();
	}
}
