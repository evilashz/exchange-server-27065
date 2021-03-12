using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200041A RID: 1050
	internal sealed class EventSourceActivity : IDisposable
	{
		// Token: 0x060034FF RID: 13567 RVA: 0x000CDC40 File Offset: 0x000CBE40
		public EventSourceActivity(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			this.eventSource = eventSource;
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000CDC5D File Offset: 0x000CBE5D
		public static implicit operator EventSourceActivity(EventSource eventSource)
		{
			return new EventSourceActivity(eventSource);
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06003501 RID: 13569 RVA: 0x000CDC65 File Offset: 0x000CBE65
		public EventSource EventSource
		{
			get
			{
				return this.eventSource;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x000CDC6D File Offset: 0x000CBE6D
		public Guid Id
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000CDC75 File Offset: 0x000CBE75
		public EventSourceActivity Start<T>(string eventName, EventSourceOptions options, T data)
		{
			return this.Start<T>(eventName, ref options, ref data);
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000CDC84 File Offset: 0x000CBE84
		public EventSourceActivity Start(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000CDCAC File Offset: 0x000CBEAC
		public EventSourceActivity Start(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			return this.Start<EmptyStruct>(eventName, ref options, ref emptyStruct);
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000CDCCC File Offset: 0x000CBECC
		public EventSourceActivity Start<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			return this.Start<T>(eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000CDCEC File Offset: 0x000CBEEC
		public void Stop<T>(T data)
		{
			this.Stop<T>(null, ref data);
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000CDCF8 File Offset: 0x000CBEF8
		public void Stop<T>(string eventName)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Stop<EmptyStruct>(eventName, ref emptyStruct);
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000CDD16 File Offset: 0x000CBF16
		public void Stop<T>(string eventName, T data)
		{
			this.Stop<T>(eventName, ref data);
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000CDD21 File Offset: 0x000CBF21
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(this.eventSource, eventName, ref options, ref data);
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000CDD34 File Offset: 0x000CBF34
		public void Write<T>(string eventName, T data)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.Write<T>(this.eventSource, eventName, ref eventSourceOptions, ref data);
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000CDD5C File Offset: 0x000CBF5C
		public void Write(string eventName, EventSourceOptions options)
		{
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref options, ref emptyStruct);
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000CDD84 File Offset: 0x000CBF84
		public void Write(string eventName)
		{
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.Write<EmptyStruct>(this.eventSource, eventName, ref eventSourceOptions, ref emptyStruct);
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000CDDB2 File Offset: 0x000CBFB2
		public void Write<T>(EventSource source, string eventName, EventSourceOptions options, T data)
		{
			this.Write<T>(source, eventName, ref options, ref data);
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000CDDC0 File Offset: 0x000CBFC0
		public void Dispose()
		{
			if (this.state == EventSourceActivity.State.Started)
			{
				EmptyStruct emptyStruct = default(EmptyStruct);
				this.Stop<EmptyStruct>(null, ref emptyStruct);
			}
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000CDDE8 File Offset: 0x000CBFE8
		private EventSourceActivity Start<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.eventSource.IsEnabled())
			{
				return this;
			}
			EventSourceActivity eventSourceActivity = new EventSourceActivity(this.eventSource);
			if (!this.eventSource.IsEnabled(options.Level, options.Keywords))
			{
				Guid id = this.Id;
				eventSourceActivity.activityId = Guid.NewGuid();
				eventSourceActivity.startStopOptions = options;
				eventSourceActivity.eventName = eventName;
				eventSourceActivity.startStopOptions.Opcode = EventOpcode.Start;
				this.eventSource.Write<T>(eventName, ref eventSourceActivity.startStopOptions, ref eventSourceActivity.activityId, ref id, ref data);
			}
			else
			{
				eventSourceActivity.activityId = this.Id;
			}
			return eventSourceActivity;
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000CDE92 File Offset: 0x000CC092
		private void Write<T>(EventSource eventSource, string eventName, ref EventSourceOptions options, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (eventName == null)
			{
				throw new ArgumentNullException();
			}
			eventSource.Write<T>(eventName, ref options, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000CDEC0 File Offset: 0x000CC0C0
		private void Stop<T>(string eventName, ref T data)
		{
			if (this.state != EventSourceActivity.State.Started)
			{
				throw new InvalidOperationException();
			}
			if (!this.StartEventWasFired)
			{
				return;
			}
			this.state = EventSourceActivity.State.Stopped;
			if (eventName == null)
			{
				eventName = this.eventName;
				if (eventName.EndsWith("Start"))
				{
					eventName = eventName.Substring(0, eventName.Length - 5);
				}
				eventName += "Stop";
			}
			this.startStopOptions.Opcode = EventOpcode.Stop;
			this.eventSource.Write<T>(eventName, ref this.startStopOptions, ref this.activityId, ref EventSourceActivity.s_empty, ref data);
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06003513 RID: 13587 RVA: 0x000CDF4B File Offset: 0x000CC14B
		private bool StartEventWasFired
		{
			get
			{
				return this.eventName != null;
			}
		}

		// Token: 0x04001776 RID: 6006
		private readonly EventSource eventSource;

		// Token: 0x04001777 RID: 6007
		private EventSourceOptions startStopOptions;

		// Token: 0x04001778 RID: 6008
		internal Guid activityId;

		// Token: 0x04001779 RID: 6009
		private EventSourceActivity.State state;

		// Token: 0x0400177A RID: 6010
		private string eventName;

		// Token: 0x0400177B RID: 6011
		internal static Guid s_empty;

		// Token: 0x02000B66 RID: 2918
		private enum State
		{
			// Token: 0x04003446 RID: 13382
			Started,
			// Token: 0x04003447 RID: 13383
			Stopped
		}
	}
}
