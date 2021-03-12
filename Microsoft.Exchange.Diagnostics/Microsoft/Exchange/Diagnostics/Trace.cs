using System;
using System.IO;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000AD RID: 173
	public class Trace : BaseTrace, ITracer
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000FB89 File Offset: 0x0000DD89
		public Trace(Guid guid, int traceTag) : base(guid, traceTag)
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000FB94 File Offset: 0x0000DD94
		public static Guid TraceCasStart(CasTraceEventType type)
		{
			Guid guid = Guid.Empty;
			if (ETWTrace.IsCasEnabled)
			{
				guid = Guid.NewGuid();
				ETWTrace.WriteCas(type, CasTraceStartStop.Start, guid, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
			}
			return guid;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		public static void TraceCasStop(CasTraceEventType type, Guid serviceProviderRequestId, int bytesIn, int bytesOut, object serverAddress, object userContext, object spOperation, object spOperationData, object clientOperation)
		{
			if (ETWTrace.ShouldTraceCasStop(serviceProviderRequestId))
			{
				ETWTrace.WriteCas(type, CasTraceStartStop.Stop, serviceProviderRequestId, bytesIn, bytesOut, Trace.ConvertReferenceToString(serverAddress), Trace.ConvertReferenceToString(userContext), Trace.ConvertReferenceToString(spOperation), Trace.ConvertReferenceToString(spOperationData), Trace.ConvertReferenceToString(clientOperation));
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		public void Information(long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(0, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.InfoTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.InfoTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000FC8C File Offset: 0x0000DE8C
		public void Information<T0>(long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(0, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(0, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000FD10 File Offset: 0x0000DF10
		public void Information<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(0, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
		public void Information<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(0, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000FE44 File Offset: 0x0000E044
		public void Information(long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(0, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		public void TraceDebug(long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(0, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.DebugTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.DebugTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000FF28 File Offset: 0x0000E128
		public void TraceDebug<T0>(long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(0, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(0, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000FFAC File Offset: 0x0000E1AC
		public void TraceDebug<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(0, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001003C File Offset: 0x0000E23C
		public void TraceDebug<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(0, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000100E0 File Offset: 0x0000E2E0
		public void TraceDebug(long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(0, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00010154 File Offset: 0x0000E354
		public void TraceError(long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(0, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.ErrorTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.ErrorTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000101C4 File Offset: 0x0000E3C4
		public void TraceError<T0>(long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(0, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(0, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010248 File Offset: 0x0000E448
		public void TraceError<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(0, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public void TraceError<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(0, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001037C File Offset: 0x0000E57C
		public void TraceError(long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(0, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000103F0 File Offset: 0x0000E5F0
		public void TraceWarning(long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(0, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.WarningTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.WarningTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00010460 File Offset: 0x0000E660
		public void TraceWarning<T0>(long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(0, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(0, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000104E4 File Offset: 0x0000E6E4
		public void TraceWarning<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(0, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00010574 File Offset: 0x0000E774
		public void TraceWarning<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(0, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00010618 File Offset: 0x0000E818
		public void TraceWarning(long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(0, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001068C File Offset: 0x0000E88C
		public void TracePfd(long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(0, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.PfdTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.PfdTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000106FC File Offset: 0x0000E8FC
		public void TracePfd<T0>(long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(0, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(0, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00010780 File Offset: 0x0000E980
		public void TracePfd<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(0, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00010810 File Offset: 0x0000EA10
		public void TracePfd<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(0, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000108B4 File Offset: 0x0000EAB4
		public void TracePfd(long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(0, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00010928 File Offset: 0x0000EB28
		public void TracePerformance(long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(0, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00010998 File Offset: 0x0000EB98
		public void TracePerformance<T0>(long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(0, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00010A1C File Offset: 0x0000EC1C
		public void TracePerformance<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(0, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00010AAC File Offset: 0x0000ECAC
		public void TracePerformance<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(0, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00010B50 File Offset: 0x0000ED50
		public void TracePerformance(long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(0, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		public void TraceFunction(long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(0, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.FunctionTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.FunctionTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00010C34 File Offset: 0x0000EE34
		public void TraceFunction<T0>(long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(0, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(0, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00010CB8 File Offset: 0x0000EEB8
		public void TraceFunction<T0, T1>(long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(0, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00010D48 File Offset: 0x0000EF48
		public void TraceFunction<T0, T1, T2>(long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(0, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00010DEC File Offset: 0x0000EFEC
		public void TraceFunction(long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(0, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00010E60 File Offset: 0x0000F060
		public void TraceInformation(int lid, long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(lid, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.InfoTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.InfoTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		public void TraceInformation<T0>(int lid, long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(lid, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00010F58 File Offset: 0x0000F158
		public void TraceInformation<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(lid, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00010FEC File Offset: 0x0000F1EC
		public void TraceInformation<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(lid, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00011094 File Offset: 0x0000F294
		public void TraceInformation(int lid, long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.InfoTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceInformation(lid, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.InfoTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001110C File Offset: 0x0000F30C
		public void TraceDebug(int lid, long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(lid, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.DebugTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.DebugTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001117C File Offset: 0x0000F37C
		public void TraceDebug<T0>(int lid, long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(lid, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00011204 File Offset: 0x0000F404
		public void TraceDebug<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(lid, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00011298 File Offset: 0x0000F498
		public void TraceDebug<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(lid, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00011340 File Offset: 0x0000F540
		public void TraceDebug(int lid, long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceDebug(lid, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.DebugTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public void TraceError(int lid, long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(lid, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00011428 File Offset: 0x0000F628
		public void TraceError<T0>(int lid, long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(lid, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000114B0 File Offset: 0x0000F6B0
		public void TraceError<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(lid, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00011544 File Offset: 0x0000F744
		public void TraceError<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(lid, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000115EC File Offset: 0x0000F7EC
		public void TraceError(int lid, long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.ErrorTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceError(lid, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.ErrorTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00011664 File Offset: 0x0000F864
		public void TraceWarning(int lid, long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(lid, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.WarningTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.WarningTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000116D4 File Offset: 0x0000F8D4
		public void TraceWarning<T0>(int lid, long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(lid, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001175C File Offset: 0x0000F95C
		public void TraceWarning<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(lid, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000117F0 File Offset: 0x0000F9F0
		public void TraceWarning<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(lid, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00011898 File Offset: 0x0000FA98
		public void TraceWarning(int lid, long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceWarning(lid, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.WarningTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00011910 File Offset: 0x0000FB10
		public void TracePfd(int lid, long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(lid, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.PfdTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.PfdTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00011980 File Offset: 0x0000FB80
		public void TracePfd<T0>(int lid, long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(lid, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00011A08 File Offset: 0x0000FC08
		public void TracePfd<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(lid, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00011A9C File Offset: 0x0000FC9C
		public void TracePfd<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(lid, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00011B44 File Offset: 0x0000FD44
		public void TracePfd(int lid, long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PfdTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePfd(lid, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.PfdTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00011BBC File Offset: 0x0000FDBC
		public void TracePerformance(int lid, long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(lid, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public void TracePerformance<T0>(int lid, long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(lid, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		public void TracePerformance<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(lid, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00011D48 File Offset: 0x0000FF48
		public void TracePerformance<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(lid, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00011DF0 File Offset: 0x0000FFF0
		public void TracePerformance(int lid, long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TracePerformance(lid, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.PerformanceTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00011E68 File Offset: 0x00010068
		public void TraceFunction(int lid, long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(lid, id, message);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00011ED8 File Offset: 0x000100D8
		public void TraceFunction<T0>(int lid, long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(lid, id, formatString, new object[]
					{
						arg0
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00011F60 File Offset: 0x00010160
		public void TraceFunction<T0, T1>(int lid, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(lid, id, formatString, new object[]
					{
						arg0,
						arg1
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00011FF4 File Offset: 0x000101F4
		public void TraceFunction<T0, T1, T2>(int lid, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(lid, id, formatString, new object[]
					{
						arg0,
						arg1,
						arg2
					});
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001209C File Offset: 0x0001029C
		public void TraceFunction(int lid, long id, string formatString, params object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(TraceType.FunctionTrace))
			{
				if (base.TestHook != null)
				{
					base.TestHook.TraceFunction(lid, id, formatString, args);
					return;
				}
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(lid, TraceType.FunctionTrace, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00012111 File Offset: 0x00010311
		public ITracer Compose(ITracer other)
		{
			if (other == null || NullTracer.Instance.Equals(other))
			{
				return this;
			}
			return new CompositeTracer(this, other);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001212C File Offset: 0x0001032C
		public void Dump(TextWriter writer, bool addHeader, bool verbose)
		{
			if (base.IsInMemoryTraceEnabled)
			{
				ExTraceInternal.DumpMemoryTrace(writer);
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001213C File Offset: 0x0001033C
		protected void Log(TraceType traceType, long id, string message)
		{
			if (!string.IsNullOrEmpty(message) && base.IsTraceEnabled(traceType))
			{
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, traceType, this.category, this.traceTag, id, message);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, traceType, this.category, this.traceTag, id, message);
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00012194 File Offset: 0x00010394
		protected void Log<T0>(TraceType traceType, long id, string formatString, T0 arg0)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(traceType))
			{
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0>(0, traceType, this.category, this.traceTag, id, formatString, arg0);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0>(0, traceType, this.category, this.traceTag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000121F0 File Offset: 0x000103F0
		protected void Log<T0, T1>(TraceType traceType, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(traceType))
			{
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, traceType, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1>(0, traceType, this.category, this.traceTag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00012250 File Offset: 0x00010450
		protected void Log<T0, T1, T2>(TraceType traceType, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(traceType))
			{
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, traceType, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, traceType, this.category, this.traceTag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000122B4 File Offset: 0x000104B4
		protected void Log(TraceType traceType, long id, string formatString, object[] args)
		{
			if (!string.IsNullOrEmpty(formatString) && base.IsTraceEnabled(traceType))
			{
				if (base.IsInMemoryTraceEnabled)
				{
					ExTraceInternal.TraceInMemory(0, traceType, this.category, this.traceTag, id, formatString, args);
				}
				if (base.IsOtherProviderTracesEnabled)
				{
					ExTraceInternal.Trace(0, traceType, this.category, this.traceTag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00012310 File Offset: 0x00010510
		private static string ConvertReferenceToString(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			return value.ToString();
		}
	}
}
