using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000017 RID: 23
	internal class MessageEnvelope
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002358 File Offset: 0x00000558
		public MessageEnvelope(DeliveryPriority deliveryPriority, Guid organizationId, DateTime timeReceived, RoutingAddress fromAddress, MailDirectionality directionality, MimeDocument mimeDocument, long mimeSize, string subject, long msgId, IEnumerable<string> recipients)
		{
			this.deliveryPriority = deliveryPriority;
			this.externalOrganizationId = organizationId;
			this.timeReceived = timeReceived;
			this.fromAddress = fromAddress;
			this.directionality = directionality;
			this.mimeDocument = mimeDocument;
			this.mimeSize = mimeSize;
			this.subject = subject;
			this.msgId = msgId;
			this.recipients = recipients;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000023B8 File Offset: 0x000005B8
		public DeliveryPriority DeliveryPriority
		{
			get
			{
				return this.deliveryPriority;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000023C0 File Offset: 0x000005C0
		public Guid ExternalOrganizationId
		{
			get
			{
				return this.externalOrganizationId;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000023C8 File Offset: 0x000005C8
		public DateTime TimeReceived
		{
			get
			{
				return this.timeReceived;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000023D0 File Offset: 0x000005D0
		public RoutingAddress FromAddress
		{
			get
			{
				return this.fromAddress;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000023D8 File Offset: 0x000005D8
		public MailDirectionality Directionality
		{
			get
			{
				return this.directionality;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000023E0 File Offset: 0x000005E0
		public MimeDocument MimeDocument
		{
			get
			{
				return this.mimeDocument;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000023E8 File Offset: 0x000005E8
		public long MimeSize
		{
			get
			{
				return this.mimeSize;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000023F0 File Offset: 0x000005F0
		public string Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000023F8 File Offset: 0x000005F8
		public IEnumerable<string> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002400 File Offset: 0x00000600
		public long MsgId
		{
			get
			{
				return this.msgId;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002408 File Offset: 0x00000608
		public void AddProperty<T>(string name, T value)
		{
			if (this.properties == null)
			{
				this.properties = new Dictionary<string, object>();
			}
			this.properties[name] = value;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000242F File Offset: 0x0000062F
		public bool TryAddProperty<T>(string name, T value)
		{
			if (this.properties == null)
			{
				this.properties = new Dictionary<string, object>();
			}
			if (!this.properties.ContainsKey(name))
			{
				this.properties[name] = value;
				return true;
			}
			return false;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002468 File Offset: 0x00000668
		public bool TryGetProperty<T>(string name, out T value)
		{
			value = default(T);
			if (this.properties == null)
			{
				return false;
			}
			object obj;
			if (!this.properties.TryGetValue(name, out obj))
			{
				return false;
			}
			Type typeFromHandle = typeof(T);
			if (obj == null)
			{
				return !typeFromHandle.IsValueType || Nullable.GetUnderlyingType(typeFromHandle) != null;
			}
			Type type = obj.GetType();
			if (typeFromHandle.IsAssignableFrom(type))
			{
				value = (T)((object)obj);
				return true;
			}
			return false;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000024DC File Offset: 0x000006DC
		public IEnumerable<Tuple<string, Type>> GetPropertyNames(string prefix)
		{
			if (this.properties == null || prefix == null)
			{
				return null;
			}
			List<Tuple<string, Type>> list = new List<Tuple<string, Type>>();
			foreach (KeyValuePair<string, object> keyValuePair in this.properties)
			{
				if (keyValuePair.Key.StartsWith(prefix))
				{
					if (keyValuePair.Value == null)
					{
						list.Add(Tuple.Create<string, Type>(keyValuePair.Key, null));
					}
					else
					{
						list.Add(Tuple.Create<string, Type>(keyValuePair.Key, keyValuePair.Value.GetType()));
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x04000022 RID: 34
		internal static readonly string AttributionNamespace = "Transport.Attribution.";

		// Token: 0x04000023 RID: 35
		internal static readonly string AccountForestProperty = MessageEnvelope.AttributionNamespace + "AccountForest";

		// Token: 0x04000024 RID: 36
		private readonly DeliveryPriority deliveryPriority;

		// Token: 0x04000025 RID: 37
		private readonly Guid externalOrganizationId;

		// Token: 0x04000026 RID: 38
		private readonly DateTime timeReceived;

		// Token: 0x04000027 RID: 39
		private readonly RoutingAddress fromAddress;

		// Token: 0x04000028 RID: 40
		private readonly MailDirectionality directionality;

		// Token: 0x04000029 RID: 41
		private readonly MimeDocument mimeDocument;

		// Token: 0x0400002A RID: 42
		private readonly long mimeSize;

		// Token: 0x0400002B RID: 43
		private readonly string subject;

		// Token: 0x0400002C RID: 44
		private readonly long msgId;

		// Token: 0x0400002D RID: 45
		private IEnumerable<string> recipients;

		// Token: 0x0400002E RID: 46
		private Dictionary<string, object> properties;
	}
}
