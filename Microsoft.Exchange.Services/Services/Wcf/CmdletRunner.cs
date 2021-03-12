using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000986 RID: 2438
	internal class CmdletRunner<TTask, TResult> where TTask : Task
	{
		// Token: 0x060045BD RID: 17853 RVA: 0x000F4DC4 File Offset: 0x000F2FC4
		internal CmdletRunner(CallContext callContext, string cmdletName, ScopeLocation rbacScope, PSLocalTask<TTask, TResult> taskWrapper)
		{
			this.callContext = callContext;
			this.cmdletName = cmdletName;
			this.rbacScope = rbacScope;
			this.taskWrapper = taskWrapper;
			this.instrumentationName = typeof(TTask).Name;
			this.taskParameterValues = new Dictionary<string, object>();
			OwsLogRegistry.Register(this.instrumentationName, typeof(DefaultOptionsActionMetadata), new Type[0]);
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x000F4E2F File Offset: 0x000F302F
		internal PSLocalTask<TTask, TResult> TaskWrapper
		{
			get
			{
				return this.taskWrapper;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x000F4E37 File Offset: 0x000F3037
		internal IList<TResult> TaskAllResults
		{
			get
			{
				return this.taskWrapper.AllResults;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x000F4E44 File Offset: 0x000F3044
		internal TResult TaskResult
		{
			get
			{
				return this.taskWrapper.Result;
			}
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x000F4E54 File Offset: 0x000F3054
		internal void SetTaskParameter(string propertyName, object taskParameters, object newPropertyValue)
		{
			if (!this.taskParameterValues.ContainsKey(propertyName))
			{
				PropertyInfo property = taskParameters.GetType().GetProperty(propertyName);
				if (property == null)
				{
					throw new ArgumentException(string.Format("Could not find property {0} on object.", propertyName));
				}
				object value = newPropertyValue;
				Type propertyType = property.PropertyType;
				if (newPropertyValue != null && !propertyType.IsInstanceOfType(newPropertyValue))
				{
					Type type = newPropertyValue.GetType();
					if (type.IsArray && propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(MultiValuedProperty<>) && propertyType.GetGenericArguments()[0].IsAssignableFrom(type.GetElementType()))
					{
						ConstructorInfo constructor = propertyType.GetConstructor(new Type[]
						{
							typeof(object)
						});
						if (constructor != null)
						{
							value = constructor.Invoke(new object[]
							{
								newPropertyValue
							});
						}
					}
				}
				this.taskParameterValues.Add(propertyName, value);
				property.SetValue(taskParameters, value);
			}
		}

		// Token: 0x060045C2 RID: 17858 RVA: 0x000F4F78 File Offset: 0x000F3178
		internal void SetRemainingModifiedTaskParameters(OptionsPropertyChangeTracker incomingOptions, object taskParameters)
		{
			HashSet<string> allPublicPropertyNames = new HashSet<string>(from propertyInfo in incomingOptions.GetType().GetProperties()
			select propertyInfo.Name);
			IEnumerable<string> enumerable = from e in taskParameters.GetType().GetProperties()
			where allPublicPropertyNames.Contains(e.Name)
			select e.Name;
			foreach (string propertyName in enumerable)
			{
				this.SetTaskParameterIfModified(propertyName, incomingOptions, taskParameters);
			}
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x000F5040 File Offset: 0x000F3240
		private void SetTaskParameterIfModified(string propertyName, OptionsPropertyChangeTracker incomingOptions, object taskParameters)
		{
			PropertyInfo property = incomingOptions.GetType().GetProperty(propertyName);
			if (property == null)
			{
				throw new ArgumentException(string.Format("Could not find property {0} on incoming data contract object.", propertyName));
			}
			object value = property.GetValue(incomingOptions);
			this.SetTaskParameterIfModified(propertyName, incomingOptions, taskParameters, value);
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x000F5086 File Offset: 0x000F3286
		internal void SetTaskParameterIfModified(string propertyName, OptionsPropertyChangeTracker incomingOptions, object taskParameters, object newPropertyValue)
		{
			this.SetTaskParameterIfModified(propertyName, propertyName, incomingOptions, taskParameters, newPropertyValue);
		}

		// Token: 0x060045C5 RID: 17861 RVA: 0x000F5094 File Offset: 0x000F3294
		internal void SetTaskParameterIfModified(string incomingOptionsPropertyName, string taskPropertyName, OptionsPropertyChangeTracker incomingOptions, object taskParameters, object newPropertyValue)
		{
			if (incomingOptions.HasPropertyChanged(incomingOptionsPropertyName))
			{
				this.SetTaskParameter(taskPropertyName, taskParameters, newPropertyValue);
			}
		}

		// Token: 0x060045C6 RID: 17862 RVA: 0x000F50AA File Offset: 0x000F32AA
		internal void Execute()
		{
			this.LogTaskParametersForDebug();
			this.CheckUserPermissions();
			this.ExecuteTask();
		}

		// Token: 0x060045C7 RID: 17863 RVA: 0x000F50E0 File Offset: 0x000F32E0
		private void LogTaskParametersForDebug()
		{
			string text = string.Join(",  ", (from p in this.taskParameterValues
			select p.Key + "=" + this.GetStringValue(p.Value)).ToArray<string>());
			ExTraceGlobals.OptionsTracer.TraceDebug((long)this.GetHashCode(), "[{0}] User = {1}, Calling cmdlet task {2} with parameters: {3}", new object[]
			{
				this.instrumentationName,
				this.callContext.AccessingPrincipal.Alias,
				this.cmdletName,
				text
			});
		}

		// Token: 0x060045C8 RID: 17864 RVA: 0x000F5164 File Offset: 0x000F3364
		private string GetStringValue(object value)
		{
			if (value == null)
			{
				return "<null>";
			}
			IEnumerable enumerable = value as IEnumerable;
			if (!(value is string) && enumerable != null)
			{
				IEnumerable<string> values = from object e in enumerable
				select this.GetStringValue(e);
				return "{" + string.Join(",", values) + "}";
			}
			return value.ToString();
		}

		// Token: 0x060045C9 RID: 17865 RVA: 0x000F51CC File Offset: 0x000F33CC
		private void CheckUserPermissions()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangeRunspaceConfigurationCache.Singleton.Get(this.callContext.EffectiveCaller, null, false);
			bool flag = exchangeRunspaceConfiguration.IsCmdletAllowedInScope(this.cmdletName, this.taskParameterValues.Keys.ToArray<string>(), exchangeRunspaceConfiguration.ExecutingUser, this.rbacScope);
			stopwatch.Stop();
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, DefaultOptionsActionMetadata.RbacTime, stopwatch.ElapsedMilliseconds);
			if (flag)
			{
				ExTraceGlobals.OptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "[{0}] User has sufficient permissions to execute task.", this.instrumentationName);
				return;
			}
			ExTraceGlobals.OptionsTracer.TraceError((long)this.GetHashCode(), "[{0}] User '{1}' does not have enough permissions to execute cmdlet '{2}' with parameters '{3}'", new object[]
			{
				this.instrumentationName,
				this.callContext.AccessingPrincipal.Alias,
				this.cmdletName,
				this.taskParameterValues.Keys.ToArray<string>()
			});
			throw new CmdletException(OptionsActionError.PermissionDenied, CoreResources.GetLocalizedString((CoreResources.IDs)3694049238U));
		}

		// Token: 0x060045CA RID: 17866 RVA: 0x000F52CC File Offset: 0x000F34CC
		private void ExecuteTask()
		{
			CmdletIOPipelineHandler cmdletIOPipelineHandler = new CmdletIOPipelineHandler();
			TTask task = this.taskWrapper.Task;
			task.PrependTaskIOPipelineHandler(cmdletIOPipelineHandler);
			Stopwatch stopwatch = Stopwatch.StartNew();
			TTask task2 = this.taskWrapper.Task;
			task2.Execute();
			stopwatch.Stop();
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, DefaultOptionsActionMetadata.CmdletTime, stopwatch.ElapsedMilliseconds);
			if (cmdletIOPipelineHandler.UserPrompt != null)
			{
				ExTraceGlobals.OptionsTracer.TraceError<string, string>((long)this.GetHashCode(), "[{0}] Task is requiring a user prompt: {1}", this.instrumentationName, cmdletIOPipelineHandler.UserPrompt);
				throw new CmdletException(OptionsActionError.PromptUser, cmdletIOPipelineHandler.UserPrompt);
			}
			if (this.taskWrapper.Error != null)
			{
				ExTraceGlobals.OptionsTracer.TraceError<string, ExchangeErrorCategory?, string>((long)this.GetHashCode(), "[{0}] Task execution completed with error. ExchangeErrorCategory = {1}, Message = {2}", this.instrumentationName, this.taskWrapper.Error.ExchangeErrorCategory, this.taskWrapper.ErrorMessage);
				throw new CmdletException(OptionsActionError.CmdletError, this.taskWrapper.ErrorMessage);
			}
			ExTraceGlobals.OptionsTracer.TraceDebug<string>((long)this.GetHashCode(), "[{0}] Task execution completed successfully.", this.instrumentationName);
		}

		// Token: 0x04002879 RID: 10361
		private readonly CallContext callContext;

		// Token: 0x0400287A RID: 10362
		private readonly string cmdletName;

		// Token: 0x0400287B RID: 10363
		private readonly ScopeLocation rbacScope;

		// Token: 0x0400287C RID: 10364
		private readonly string instrumentationName;

		// Token: 0x0400287D RID: 10365
		private readonly Dictionary<string, object> taskParameterValues;

		// Token: 0x0400287E RID: 10366
		private PSLocalTask<TTask, TResult> taskWrapper;
	}
}
