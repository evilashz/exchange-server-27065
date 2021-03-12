using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Data;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000275 RID: 629
	[Serializable]
	public class ComponentLatencyInfo
	{
		// Token: 0x060014FD RID: 5373 RVA: 0x00042F18 File Offset: 0x00041118
		public ComponentLatencyInfo(LocalizedString componentName, EnhancedTimeSpan componentLatency, int componentSequenceNumber, bool isPending)
		{
			this.componentName = componentName;
			this.componentLatency = componentLatency;
			this.isPending = isPending;
			this.componentSequenceNumber = componentSequenceNumber;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00042F40 File Offset: 0x00041140
		private ComponentLatencyInfo(PropertyStreamReader reader)
		{
			KeyValuePair<string, object> item;
			reader.Read(out item);
			if (!string.Equals("NumProperties", item.Key, StringComparison.OrdinalIgnoreCase))
			{
				throw new SerializationException(string.Format("Cannot deserialize ComponentLatencyInfo. Expected property NumProperties, but found property '{0}'", item.Key));
			}
			int value = PropertyStreamReader.GetValue<int>(item);
			for (int i = 0; i < value; i++)
			{
				reader.Read(out item);
				if (string.Equals("Name", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.componentName = new LocalizedString(PropertyStreamReader.GetValue<string>(item));
				}
				else if (string.Equals("Latency", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.componentLatency = EnhancedTimeSpan.Parse(PropertyStreamReader.GetValue<string>(item));
				}
				else if (string.Equals("SequenceNumber", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.componentSequenceNumber = PropertyStreamReader.GetValue<int>(item);
				}
				else if (string.Equals("Pending", item.Key, StringComparison.OrdinalIgnoreCase))
				{
					this.isPending = PropertyStreamReader.GetValue<bool>(item);
				}
				else
				{
					ExTraceGlobals.SerializationTracer.TraceWarning<string>(0L, "Ignoring unknown property '{0} in ComponentLatencyInfo", item.Key);
				}
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00043056 File Offset: 0x00041256
		public int ComponentSequenceNumber
		{
			get
			{
				return this.componentSequenceNumber;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0004305E File Offset: 0x0004125E
		public LocalizedString ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00043066 File Offset: 0x00041266
		public bool IsPending
		{
			get
			{
				return this.isPending;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0004306E File Offset: 0x0004126E
		public EnhancedTimeSpan ComponentLatency
		{
			get
			{
				return this.componentLatency;
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00043078 File Offset: 0x00041278
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.componentSequenceNumber,
				";",
				this.componentName,
				";",
				this.isPending,
				";",
				this.componentLatency
			});
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000430E2 File Offset: 0x000412E2
		internal static ComponentLatencyInfo Create(PropertyStreamReader reader)
		{
			return new ComponentLatencyInfo(reader);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000430EC File Offset: 0x000412EC
		internal void ToByteArray(ref byte[] bytes, ref int offset)
		{
			int num = 0;
			PropertyStreamWriter.WritePropertyKeyValue("NumProperties", StreamPropertyType.Int32, 4, ref bytes, ref offset);
			PropertyStreamWriter.WritePropertyKeyValue("Name", StreamPropertyType.String, this.componentName.ToString(), ref bytes, ref offset);
			num++;
			PropertyStreamWriter.WritePropertyKeyValue("Latency", StreamPropertyType.String, this.componentLatency.ToString(), ref bytes, ref offset);
			num++;
			PropertyStreamWriter.WritePropertyKeyValue("SequenceNumber", StreamPropertyType.Int32, this.componentSequenceNumber, ref bytes, ref offset);
			num++;
			PropertyStreamWriter.WritePropertyKeyValue("Pending", StreamPropertyType.Bool, this.isPending, ref bytes, ref offset);
			num++;
		}

		// Token: 0x04000C3B RID: 3131
		private const string NumPropertiesKey = "NumProperties";

		// Token: 0x04000C3C RID: 3132
		private const string ComponentSequenceNumberKey = "SequenceNumber";

		// Token: 0x04000C3D RID: 3133
		private const string ComponentNameKey = "Name";

		// Token: 0x04000C3E RID: 3134
		private const string ComponentLatencyKey = "Latency";

		// Token: 0x04000C3F RID: 3135
		private const string PendingKey = "Pending";

		// Token: 0x04000C40 RID: 3136
		private int componentSequenceNumber;

		// Token: 0x04000C41 RID: 3137
		private LocalizedString componentName;

		// Token: 0x04000C42 RID: 3138
		private EnhancedTimeSpan componentLatency;

		// Token: 0x04000C43 RID: 3139
		private bool isPending;
	}
}
