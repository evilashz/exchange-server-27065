﻿using System;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000B2 RID: 178
	internal class TraceFormatter<T>
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00012634 File Offset: 0x00010834
		public static TraceFormatter<T> Default
		{
			get
			{
				if (TraceFormatter<T>.defaultTraceFormatter != null)
				{
					return TraceFormatter<T>.defaultTraceFormatter;
				}
				return TraceFormatter<T>.CreateTraceFormatter();
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00012648 File Offset: 0x00010848
		public virtual void Format(ITraceBuilder builder, T value)
		{
			if (value == null)
			{
				builder.AddArgument(string.Empty);
				return;
			}
			ITraceable traceable = value as ITraceable;
			if (traceable != null)
			{
				traceable.TraceTo(builder);
				return;
			}
			string value2 = value.ToString();
			builder.AddArgument(value2);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00012698 File Offset: 0x00010898
		private static TraceFormatter<T> CreateTraceFormatter()
		{
			if (typeof(T) == typeof(int))
			{
				Int32TraceFormatter int32TraceFormatter = new Int32TraceFormatter();
				TraceFormatter<T>.defaultTraceFormatter = (int32TraceFormatter as TraceFormatter<T>);
			}
			else if (typeof(T) == typeof(long))
			{
				Int64TraceFormatter int64TraceFormatter = new Int64TraceFormatter();
				TraceFormatter<T>.defaultTraceFormatter = (int64TraceFormatter as TraceFormatter<T>);
			}
			else if (typeof(T) == typeof(Guid))
			{
				GuidTraceFormatter guidTraceFormatter = new GuidTraceFormatter();
				TraceFormatter<T>.defaultTraceFormatter = (guidTraceFormatter as TraceFormatter<T>);
			}
			else if (typeof(ITraceable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
			{
				TraceFormatter<T>.defaultTraceFormatter = (TraceFormatter<T>)Activator.CreateInstance(typeof(TraceableFormatter<>).MakeGenericType(new Type[]
				{
					typeof(T)
				}));
			}
			else
			{
				TraceFormatter<T>.defaultTraceFormatter = new TraceFormatter<T>();
			}
			return TraceFormatter<T>.defaultTraceFormatter;
		}

		// Token: 0x0400036E RID: 878
		private static TraceFormatter<T> defaultTraceFormatter;
	}
}
