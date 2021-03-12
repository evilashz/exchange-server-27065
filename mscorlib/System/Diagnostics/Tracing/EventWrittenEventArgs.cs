using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003F8 RID: 1016
	[__DynamicallyInvokable]
	public class EventWrittenEventArgs : EventArgs
	{
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000C98CD File Offset: 0x000C7ACD
		// (set) Token: 0x060033F2 RID: 13298 RVA: 0x000C9904 File Offset: 0x000C7B04
		[__DynamicallyInvokable]
		public string EventName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_eventName != null || this.EventId < 0)
				{
					return this.m_eventName;
				}
				return this.m_eventSource.m_eventData[this.EventId].Name;
			}
			internal set
			{
				this.m_eventName = value;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x000C990D File Offset: 0x000C7B0D
		// (set) Token: 0x060033F4 RID: 13300 RVA: 0x000C9915 File Offset: 0x000C7B15
		[__DynamicallyInvokable]
		public int EventId { [__DynamicallyInvokable] get; internal set; }

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x000C9920 File Offset: 0x000C7B20
		// (set) Token: 0x060033F6 RID: 13302 RVA: 0x000C9948 File Offset: 0x000C7B48
		[__DynamicallyInvokable]
		public Guid ActivityId
		{
			[SecurityCritical]
			[__DynamicallyInvokable]
			get
			{
				Guid guid = this.m_activityId;
				if (guid == Guid.Empty)
				{
					guid = EventSource.CurrentThreadActivityId;
				}
				return guid;
			}
			internal set
			{
				this.m_activityId = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000C9951 File Offset: 0x000C7B51
		// (set) Token: 0x060033F8 RID: 13304 RVA: 0x000C9959 File Offset: 0x000C7B59
		[__DynamicallyInvokable]
		public Guid RelatedActivityId { [SecurityCritical] [__DynamicallyInvokable] get; internal set; }

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000C9962 File Offset: 0x000C7B62
		// (set) Token: 0x060033FA RID: 13306 RVA: 0x000C996A File Offset: 0x000C7B6A
		[__DynamicallyInvokable]
		public ReadOnlyCollection<object> Payload { [__DynamicallyInvokable] get; internal set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060033FB RID: 13307 RVA: 0x000C9974 File Offset: 0x000C7B74
		// (set) Token: 0x060033FC RID: 13308 RVA: 0x000C99DD File Offset: 0x000C7BDD
		[__DynamicallyInvokable]
		public ReadOnlyCollection<string> PayloadNames
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_payloadNames == null)
				{
					List<string> list = new List<string>();
					foreach (ParameterInfo parameterInfo in this.m_eventSource.m_eventData[this.EventId].Parameters)
					{
						list.Add(parameterInfo.Name);
					}
					this.m_payloadNames = new ReadOnlyCollection<string>(list);
				}
				return this.m_payloadNames;
			}
			internal set
			{
				this.m_payloadNames = value;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x000C99E6 File Offset: 0x000C7BE6
		[__DynamicallyInvokable]
		public EventSource EventSource
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_eventSource;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x000C99EE File Offset: 0x000C7BEE
		[__DynamicallyInvokable]
		public EventKeywords Keywords
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_keywords;
				}
				return (EventKeywords)this.m_eventSource.m_eventData[this.EventId].Descriptor.Keywords;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x000C9A22 File Offset: 0x000C7C22
		[__DynamicallyInvokable]
		public EventOpcode Opcode
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_opcode;
				}
				return (EventOpcode)this.m_eventSource.m_eventData[this.EventId].Descriptor.Opcode;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x000C9A56 File Offset: 0x000C7C56
		[__DynamicallyInvokable]
		public EventTask Task
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return EventTask.None;
				}
				return (EventTask)this.m_eventSource.m_eventData[this.EventId].Descriptor.Task;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000C9A85 File Offset: 0x000C7C85
		[__DynamicallyInvokable]
		public EventTags Tags
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_tags;
				}
				return this.m_eventSource.m_eventData[this.EventId].Tags;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x000C9AB4 File Offset: 0x000C7CB4
		// (set) Token: 0x06003403 RID: 13315 RVA: 0x000C9AE3 File Offset: 0x000C7CE3
		[__DynamicallyInvokable]
		public string Message
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_message;
				}
				return this.m_eventSource.m_eventData[this.EventId].Message;
			}
			internal set
			{
				this.m_message = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06003404 RID: 13316 RVA: 0x000C9AEC File Offset: 0x000C7CEC
		[__DynamicallyInvokable]
		public EventChannel Channel
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return EventChannel.None;
				}
				return (EventChannel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Channel;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000C9B1B File Offset: 0x000C7D1B
		[__DynamicallyInvokable]
		public byte Version
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return 0;
				}
				return this.m_eventSource.m_eventData[this.EventId].Descriptor.Version;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x000C9B4A File Offset: 0x000C7D4A
		[__DynamicallyInvokable]
		public EventLevel Level
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_level;
				}
				return (EventLevel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Level;
			}
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000C9B7E File Offset: 0x000C7D7E
		internal EventWrittenEventArgs(EventSource eventSource)
		{
			this.m_eventSource = eventSource;
		}

		// Token: 0x040016D2 RID: 5842
		private string m_message;

		// Token: 0x040016D3 RID: 5843
		private string m_eventName;

		// Token: 0x040016D4 RID: 5844
		private EventSource m_eventSource;

		// Token: 0x040016D5 RID: 5845
		private ReadOnlyCollection<string> m_payloadNames;

		// Token: 0x040016D6 RID: 5846
		private Guid m_activityId;

		// Token: 0x040016D7 RID: 5847
		internal EventTags m_tags;

		// Token: 0x040016D8 RID: 5848
		internal EventOpcode m_opcode;

		// Token: 0x040016D9 RID: 5849
		internal EventKeywords m_keywords;

		// Token: 0x040016DA RID: 5850
		internal EventLevel m_level;
	}
}
