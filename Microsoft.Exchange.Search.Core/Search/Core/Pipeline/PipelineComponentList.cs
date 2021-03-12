using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000AD RID: 173
	internal sealed class PipelineComponentList : Disposable
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x00010FC6 File Offset: 0x0000F1C6
		internal PipelineComponentList(int count, int poisonComponentThreshold)
		{
			this.components = new SortedList<int, PipelineComponentList.PipelineComponentElement>(count);
			this.count = count;
			this.hasStartStopComponent = false;
			this.poisonComponentThreshold = poisonComponentThreshold;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00010FFA File Offset: 0x0000F1FA
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00011002 File Offset: 0x0000F202
		internal bool HasStartStopComponent
		{
			get
			{
				return this.hasStartStopComponent;
			}
		}

		// Token: 0x17000136 RID: 310
		public IPipelineComponent this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				IPipelineComponent component;
				lock (this.locker)
				{
					component = this.components[index].Component;
				}
				return component;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00011074 File Offset: 0x0000F274
		internal bool TrackPoisonComponent(int index)
		{
			bool result = false;
			lock (this.locker)
			{
				result = (++this.components[index].PoisonCount == this.poisonComponentThreshold);
			}
			return result;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000110D8 File Offset: 0x0000F2D8
		internal bool IsPoisonComponent(int index)
		{
			bool result = false;
			lock (this.locker)
			{
				result = (this.components[index].PoisonCount >= this.poisonComponentThreshold);
			}
			return result;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00011134 File Offset: 0x0000F334
		internal int IndexOf(IPipelineComponent component)
		{
			Util.ThrowOnNullArgument(component, "component");
			int num = 0;
			while (num < this.count && component != this[num])
			{
				num++;
			}
			if (num >= this.count)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00011174 File Offset: 0x0000F374
		internal IPipelineComponent Insert(int index, PipelineComponentCreator componentCreator)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this.components.ContainsKey(index))
			{
				throw new ArgumentException("Creator with index already exists", "index");
			}
			Util.ThrowOnNullArgument(componentCreator, "componentCreator");
			this.components.Add(index, new PipelineComponentList.PipelineComponentElement(componentCreator));
			IPipelineComponent pipelineComponent = this[index];
			if (pipelineComponent is IStartStopPipelineComponent)
			{
				this.hasStartStopComponent = true;
			}
			return pipelineComponent;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000111EC File Offset: 0x0000F3EC
		internal void Recreate(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (!this.components.ContainsKey(index))
			{
				throw new ArgumentException("Creator with index doesn't exist", "index");
			}
			IPipelineComponent component = this[index];
			lock (this.locker)
			{
				this.components[index].Create();
			}
			this.TryDisposeComponent(component);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00011280 File Offset: 0x0000F480
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PipelineComponentList>(this);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00011288 File Offset: 0x0000F488
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				for (int i = 0; i < this.Count; i++)
				{
					IPipelineComponent component = this[i];
					this.TryDisposeComponent(component);
				}
				this.components.Clear();
				this.components = null;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000112CC File Offset: 0x0000F4CC
		private bool TryDisposeComponent(IPipelineComponent component)
		{
			IDisposable disposable = component as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x0400026B RID: 619
		private readonly int count;

		// Token: 0x0400026C RID: 620
		private readonly int poisonComponentThreshold;

		// Token: 0x0400026D RID: 621
		private SortedList<int, PipelineComponentList.PipelineComponentElement> components;

		// Token: 0x0400026E RID: 622
		private object locker = new object();

		// Token: 0x0400026F RID: 623
		private bool hasStartStopComponent;

		// Token: 0x020000AE RID: 174
		internal class PipelineComponentElement
		{
			// Token: 0x0600054D RID: 1357 RVA: 0x000112EC File Offset: 0x0000F4EC
			public PipelineComponentElement(PipelineComponentCreator componentCreator)
			{
				Util.ThrowOnNullArgument(componentCreator, "componentCreator");
				this.creator = componentCreator;
				this.PoisonCount = 0;
				this.Create();
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x0600054E RID: 1358 RVA: 0x00011313 File Offset: 0x0000F513
			// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001131B File Offset: 0x0000F51B
			public IPipelineComponent Component { get; private set; }

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000550 RID: 1360 RVA: 0x00011324 File Offset: 0x0000F524
			// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001132C File Offset: 0x0000F52C
			public int PoisonCount { get; set; }

			// Token: 0x06000552 RID: 1362 RVA: 0x00011335 File Offset: 0x0000F535
			public void Create()
			{
				this.Component = this.creator();
			}

			// Token: 0x04000270 RID: 624
			private readonly PipelineComponentCreator creator;
		}
	}
}
