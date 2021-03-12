using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000064 RID: 100
	public static class PerformanceCounterFactory
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x000063C7 File Offset: 0x000045C7
		public static IPerformanceCounter CreatePerformanceCounter()
		{
			return PerformanceCounterFactory.hookableFactory.Value.CreatePerformanceCounter();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000063D8 File Offset: 0x000045D8
		public static IPerformanceCounterCategory CreatePerformanceCounterCategory(string categoryName)
		{
			return PerformanceCounterFactory.hookableFactory.Value.CreatePerformanceCounterCategory(categoryName);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000063EA File Offset: 0x000045EA
		internal static IDisposable SetTestHook(PerformanceCounterFactory.IFactory factory)
		{
			return PerformanceCounterFactory.hookableFactory.SetTestHook(factory);
		}

		// Token: 0x04000212 RID: 530
		private static Hookable<PerformanceCounterFactory.IFactory> hookableFactory = Hookable<PerformanceCounterFactory.IFactory>.Create(true, new PerformanceCounterFactory.Factory());

		// Token: 0x02000065 RID: 101
		internal interface IFactory
		{
			// Token: 0x060001C5 RID: 453
			IPerformanceCounter CreatePerformanceCounter();

			// Token: 0x060001C6 RID: 454
			IPerformanceCounterCategory CreatePerformanceCounterCategory(string categoryName);
		}

		// Token: 0x02000066 RID: 102
		private class Factory : PerformanceCounterFactory.IFactory
		{
			// Token: 0x060001C7 RID: 455 RVA: 0x00006409 File Offset: 0x00004609
			public IPerformanceCounter CreatePerformanceCounter()
			{
				return new PerformanceCounterFactory.PerformanceCounterWrapper();
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x00006410 File Offset: 0x00004610
			public IPerformanceCounterCategory CreatePerformanceCounterCategory(string categoryName)
			{
				return new PerformanceCounterFactory.PerformanceCounterCategoryWrapper(categoryName);
			}
		}

		// Token: 0x02000067 RID: 103
		private class PerformanceCounterWrapper : IPerformanceCounter, IDisposable
		{
			// Token: 0x060001CA RID: 458 RVA: 0x00006420 File Offset: 0x00004620
			public PerformanceCounterWrapper()
			{
				this.counter = new PerformanceCounter();
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060001CB RID: 459 RVA: 0x00006433 File Offset: 0x00004633
			// (set) Token: 0x060001CC RID: 460 RVA: 0x00006440 File Offset: 0x00004640
			string IPerformanceCounter.CategoryName
			{
				get
				{
					return this.counter.CategoryName;
				}
				set
				{
					this.counter.CategoryName = value;
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060001CD RID: 461 RVA: 0x0000644E File Offset: 0x0000464E
			// (set) Token: 0x060001CE RID: 462 RVA: 0x0000645B File Offset: 0x0000465B
			string IPerformanceCounter.CounterName
			{
				get
				{
					return this.counter.CounterName;
				}
				set
				{
					this.counter.CounterName = value;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060001CF RID: 463 RVA: 0x00006469 File Offset: 0x00004669
			string IPerformanceCounter.CounterHelp
			{
				get
				{
					return this.counter.CounterHelp;
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060001D0 RID: 464 RVA: 0x00006476 File Offset: 0x00004676
			// (set) Token: 0x060001D1 RID: 465 RVA: 0x00006483 File Offset: 0x00004683
			string IPerformanceCounter.InstanceName
			{
				get
				{
					return this.counter.InstanceName;
				}
				set
				{
					this.counter.InstanceName = value;
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x060001D2 RID: 466 RVA: 0x00006491 File Offset: 0x00004691
			// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000649E File Offset: 0x0000469E
			bool IPerformanceCounter.ReadOnly
			{
				get
				{
					return this.counter.ReadOnly;
				}
				set
				{
					this.counter.ReadOnly = value;
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x060001D4 RID: 468 RVA: 0x000064AC File Offset: 0x000046AC
			// (set) Token: 0x060001D5 RID: 469 RVA: 0x000064B9 File Offset: 0x000046B9
			PerformanceCounterInstanceLifetime IPerformanceCounter.InstanceLifetime
			{
				get
				{
					return this.counter.InstanceLifetime;
				}
				set
				{
					this.counter.InstanceLifetime = value;
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060001D6 RID: 470 RVA: 0x000064C7 File Offset: 0x000046C7
			PerformanceCounterType IPerformanceCounter.CounterType
			{
				get
				{
					return this.counter.CounterType;
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x060001D7 RID: 471 RVA: 0x000064D4 File Offset: 0x000046D4
			// (set) Token: 0x060001D8 RID: 472 RVA: 0x000064E1 File Offset: 0x000046E1
			long IPerformanceCounter.RawValue
			{
				get
				{
					return this.counter.RawValue;
				}
				set
				{
					this.counter.RawValue = value;
				}
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x000064EF File Offset: 0x000046EF
			long IPerformanceCounter.IncrementBy(long incrementValue)
			{
				return this.counter.IncrementBy(incrementValue);
			}

			// Token: 0x060001DA RID: 474 RVA: 0x000064FD File Offset: 0x000046FD
			float IPerformanceCounter.NextValue()
			{
				return this.counter.NextValue();
			}

			// Token: 0x060001DB RID: 475 RVA: 0x0000650A File Offset: 0x0000470A
			void IPerformanceCounter.RemoveInstance()
			{
				this.counter.RemoveInstance();
			}

			// Token: 0x060001DC RID: 476 RVA: 0x00006517 File Offset: 0x00004717
			void IPerformanceCounter.Close()
			{
				this.counter.Close();
			}

			// Token: 0x060001DD RID: 477 RVA: 0x00006524 File Offset: 0x00004724
			void IDisposable.Dispose()
			{
				this.counter.Dispose();
			}

			// Token: 0x04000213 RID: 531
			private PerformanceCounter counter;
		}

		// Token: 0x02000068 RID: 104
		private class PerformanceCounterCategoryWrapper : IPerformanceCounterCategory
		{
			// Token: 0x060001DE RID: 478 RVA: 0x00006531 File Offset: 0x00004731
			public PerformanceCounterCategoryWrapper(string categoryName)
			{
				this.category = new PerformanceCounterCategory(categoryName);
			}

			// Token: 0x060001DF RID: 479 RVA: 0x00006545 File Offset: 0x00004745
			bool IPerformanceCounterCategory.InstanceExists(string instanceName)
			{
				return this.category.InstanceExists(instanceName);
			}

			// Token: 0x060001E0 RID: 480 RVA: 0x00006553 File Offset: 0x00004753
			string[] IPerformanceCounterCategory.GetInstanceNames()
			{
				return this.category.GetInstanceNames();
			}

			// Token: 0x04000214 RID: 532
			private PerformanceCounterCategory category;
		}
	}
}
