using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class BaseTask : ITask, ITaskDescriptor
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00006994 File Offset: 0x00004B94
		public BaseTask(IContext context, LocalizedString title, LocalizedString description, TaskType type, params ContextProperty[] dependentProperties)
		{
			Util.ThrowOnNullArgument(context, "context");
			this.context = context;
			this.stateSpecificPropertyBag = this.context.Properties;
			this.title = title;
			this.description = description;
			this.type = type;
			this.result = TaskResult.Undefined;
			this.dependentProperties = Array.AsReadOnly<ContextProperty>(dependentProperties);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000069F4 File Offset: 0x00004BF4
		public IPropertyBag Properties
		{
			get
			{
				return this.stateSpecificPropertyBag;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000069FC File Offset: 0x00004BFC
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00006A04 File Offset: 0x00004C04
		public TaskResult Result
		{
			get
			{
				return this.result;
			}
			protected set
			{
				this.result = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00006A0D File Offset: 0x00004C0D
		public LocalizedString TaskTitle
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00006A15 File Offset: 0x00004C15
		public LocalizedString TaskDescription
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006A1D File Offset: 0x00004C1D
		public TaskType TaskType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00006A25 File Offset: 0x00004C25
		public IEnumerable<ContextProperty> DependentProperties
		{
			get
			{
				return TaskInfo.Get(base.GetType()).Dependencies.Concat(this.dependentProperties);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00006A42 File Offset: 0x00004C42
		protected ILogger Logger
		{
			get
			{
				return this.context.Logger;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006A4F File Offset: 0x00004C4F
		protected IEnvironment Environment
		{
			get
			{
				return this.context.Environment;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006A5C File Offset: 0x00004C5C
		void ITask.Initialize(Action resumeDelegate)
		{
			this.ChangeState(BaseTask.State.Uninitialized, BaseTask.State.Initialized);
			this.resumeDelegate = resumeDelegate;
			this.Logger.TaskStarted(this);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006A79 File Offset: 0x00004C79
		void ITask.OnCompleted()
		{
			this.Set<ExDateTime>(BaseTask.TaskFinished, ExDateTime.Now);
			this.stateSpecificPropertyBag = this.context.Properties;
			this.ChangeState(BaseTask.State.Executing, BaseTask.State.ResultObtained);
			this.Logger.TaskCompleted(this, this.Result);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006AB6 File Offset: 0x00004CB6
		IEnumerator<ITask> ITask.Process()
		{
			this.ChangeState(BaseTask.State.Initialized, BaseTask.State.Executing);
			this.stateSpecificPropertyBag = new BaseTask.DependencyCheckingPropertyBag(this.context.Properties, this.DependentProperties);
			this.Set<ExDateTime>(BaseTask.TaskStarted, ExDateTime.Now);
			return this.Process();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006AF2 File Offset: 0x00004CF2
		public IContext CreateDerivedContext()
		{
			return this.context;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006AFA File Offset: 0x00004CFA
		public IContext CreateContextCopy()
		{
			return this.context.CreateDerived();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00006B07 File Offset: 0x00004D07
		public TValue Get<TValue>(ContextProperty<TValue> property)
		{
			return this.Properties.Get(property);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006B15 File Offset: 0x00004D15
		public void Set<T>(ContextProperty property, T value)
		{
			this.Properties.Set(property, value);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006B29 File Offset: 0x00004D29
		public virtual BaseTask Copy()
		{
			throw new NotImplementedException("BaseTask.Copy()");
		}

		// Token: 0x060001E3 RID: 483
		protected abstract IEnumerator<ITask> Process();

		// Token: 0x060001E4 RID: 484 RVA: 0x00006B35 File Offset: 0x00004D35
		protected void Resume()
		{
			if (this is AsyncTask)
			{
				this.resumeDelegate();
				return;
			}
			throw new InvalidOperationException("Only AsyncTask derived classes can Resume");
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006B55 File Offset: 0x00004D55
		private void ChangeState(BaseTask.State oldState, BaseTask.State newState)
		{
			if (this.state == oldState)
			{
				this.state = newState;
				return;
			}
			throw new InvalidOperationException(string.Format("Only State={0} tasks can transition into {1}.", oldState, newState));
		}

		// Token: 0x040000D6 RID: 214
		public static readonly ContextProperty<ExDateTime> TaskStarted = ContextPropertySchema.TaskStarted.SetOnly();

		// Token: 0x040000D7 RID: 215
		public static readonly ContextProperty<ExDateTime> TaskFinished = ContextPropertySchema.TaskFinished.SetOnly();

		// Token: 0x040000D8 RID: 216
		public static readonly ContextProperty<Exception> Exception = ContextPropertySchema.Exception.SetOnly();

		// Token: 0x040000D9 RID: 217
		public static readonly ContextProperty<string> ErrorDetails = ContextPropertySchema.ErrorDetails.SetOnly();

		// Token: 0x040000DA RID: 218
		protected static readonly ContextProperty<TimeSpan> Timeout = ContextPropertySchema.Timeout.GetOnly();

		// Token: 0x040000DB RID: 219
		private readonly ReadOnlyCollection<ContextProperty> dependentProperties;

		// Token: 0x040000DC RID: 220
		private readonly IContext context;

		// Token: 0x040000DD RID: 221
		private readonly LocalizedString title;

		// Token: 0x040000DE RID: 222
		private readonly LocalizedString description;

		// Token: 0x040000DF RID: 223
		private readonly TaskType type;

		// Token: 0x040000E0 RID: 224
		private Action resumeDelegate;

		// Token: 0x040000E1 RID: 225
		private BaseTask.State state;

		// Token: 0x040000E2 RID: 226
		private IPropertyBag stateSpecificPropertyBag;

		// Token: 0x040000E3 RID: 227
		private TaskResult result;

		// Token: 0x0200004B RID: 75
		private enum State
		{
			// Token: 0x040000E5 RID: 229
			Uninitialized,
			// Token: 0x040000E6 RID: 230
			Initialized,
			// Token: 0x040000E7 RID: 231
			Executing,
			// Token: 0x040000E8 RID: 232
			ResultObtained
		}

		// Token: 0x0200004C RID: 76
		private class DependencyCheckingPropertyBag : IPropertyBag
		{
			// Token: 0x060001E7 RID: 487 RVA: 0x00006BDC File Offset: 0x00004DDC
			public DependencyCheckingPropertyBag(IPropertyBag inner, IEnumerable<ContextProperty> dependencies)
			{
				Util.ThrowOnNullArgument(inner, "inner");
				this.inner = inner;
				foreach (ContextProperty contextProperty in dependencies)
				{
					ContextProperty.AccessMode accessMode;
					this.propertyAccess.TryGetValue(contextProperty, out accessMode);
					this.propertyAccess[contextProperty] = (accessMode | contextProperty.AllowedAccessMode);
				}
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x00006C64 File Offset: 0x00004E64
			public bool TryGet(ContextProperty property, out object value)
			{
				this.EnsurePropertyDeclared(property, ContextProperty.AccessMode.Get);
				return this.inner.TryGet(property, out value);
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x00006C7B File Offset: 0x00004E7B
			public void Set(ContextProperty property, object value)
			{
				this.EnsurePropertyDeclared(property, ContextProperty.AccessMode.Set);
				this.inner.Set(property, value);
			}

			// Token: 0x060001EA RID: 490 RVA: 0x00006C94 File Offset: 0x00004E94
			private void EnsurePropertyDeclared(ContextProperty requestedProperty, ContextProperty.AccessMode requestedAccess)
			{
				ContextProperty.AccessMode accessMode;
				this.propertyAccess.TryGetValue(requestedProperty, out accessMode);
				if ((accessMode & requestedAccess) != requestedAccess)
				{
					throw new InvalidOperationException(string.Format("A task has declared at most {0} access mode on {1}, but is requesting {2}", accessMode, requestedProperty, requestedAccess));
				}
			}

			// Token: 0x040000E9 RID: 233
			private readonly IPropertyBag inner;

			// Token: 0x040000EA RID: 234
			private readonly Dictionary<ContextProperty, ContextProperty.AccessMode> propertyAccess = new Dictionary<ContextProperty, ContextProperty.AccessMode>();
		}
	}
}
