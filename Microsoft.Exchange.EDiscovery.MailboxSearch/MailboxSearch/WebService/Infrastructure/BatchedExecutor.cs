using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure
{
	// Token: 0x02000031 RID: 49
	internal class BatchedExecutor : Executor
	{
		// Token: 0x06000240 RID: 576 RVA: 0x00012ECC File Offset: 0x000110CC
		public BatchedExecutor(ISearchPolicy policy, Type taskType) : base(policy, taskType)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "BatchedExecutor.ctor Task:", taskType);
			this.BatchSize = policy.ExecutionSettings.DiscoveryMaxAllowedExecutorItems;
			this.BatchKeyFactory = BatchedExecutor.BatchByCount;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00012F28 File Offset: 0x00011128
		public static Func<object, string> BatchByCount
		{
			get
			{
				return (object o) => string.Empty;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00012F47 File Offset: 0x00011147
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00012F4F File Offset: 0x0001114F
		public uint BatchSize { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00012F58 File Offset: 0x00011158
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00012F60 File Offset: 0x00011160
		public Func<object, string> BatchKeyFactory { get; set; }

		// Token: 0x06000246 RID: 582 RVA: 0x00012F6C File Offset: 0x0001116C
		protected override void Enqueue(object item)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "BatchedExecutor.Enqueue Item:", item);
			if (item != null)
			{
				string text = this.BatchKeyFactory(item);
				if (text != null)
				{
					lock (this.updateLock)
					{
						List<object> list;
						if (this.items.ContainsKey(text))
						{
							list = this.items[text];
						}
						else
						{
							list = new List<object>();
							this.items[text] = list;
						}
						list.Add(item);
						if ((long)list.Count >= (long)((ulong)this.BatchSize))
						{
							this.EnqueueBatch(text);
						}
						return;
					}
				}
				Recorder.Trace(2L, TraceType.WarningTrace, new object[]
				{
					"BatchedExecutor.Enqueue Null Key Task:",
					base.TaskType,
					"Item:",
					item
				});
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00013050 File Offset: 0x00011250
		protected override void SignalComplete()
		{
			lock (this.updateLock)
			{
				foreach (string key in this.items.Keys.ToList<string>())
				{
					this.EnqueueBatch(key);
				}
			}
			base.SignalComplete();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000130DC File Offset: 0x000112DC
		private void EnqueueBatch(string key)
		{
			if (this.items.ContainsKey(key))
			{
				List<object> item = this.items[key];
				this.items.Remove(key);
				base.Enqueue(item);
			}
		}

		// Token: 0x0400011D RID: 285
		private readonly object updateLock = new object();

		// Token: 0x0400011E RID: 286
		private Dictionary<string, List<object>> items = new Dictionary<string, List<object>>();
	}
}
