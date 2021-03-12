using System;
using System.Collections;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000073 RID: 115
	public class ComponentTrace<TComponentTagsClass>
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00006D18 File Offset: 0x00004F18
		private static Guid ComponentGuid()
		{
			Type typeFromHandle = typeof(TComponentTagsClass);
			FieldInfo declaredField = typeFromHandle.GetTypeInfo().GetDeclaredField("guid");
			return (Guid)declaredField.GetValue(typeFromHandle);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00006D4D File Offset: 0x00004F4D
		public static Guid Category
		{
			get
			{
				return ComponentTrace<TComponentTagsClass>.componentGuid;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006D54 File Offset: 0x00004F54
		public static bool IsTraceEnabled(TraceType traceType)
		{
			return ComponentTrace<TComponentTagsClass>.enabledTypes[(int)traceType];
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006D61 File Offset: 0x00004F61
		public static bool IsTraceEnabled(int tag)
		{
			return ComponentTrace<TComponentTagsClass>.enabledTags[tag];
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006D6E File Offset: 0x00004F6E
		public static bool CheckEnabled(int tag)
		{
			return ETWTrace.IsEnabled && ComponentTrace<TComponentTagsClass>.enabledTags[tag];
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006D84 File Offset: 0x00004F84
		public static void TraceInformation(int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public static void TraceInformation<T0>(int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006E2C File Offset: 0x0000502C
		public static void TraceInformation<T0, T1>(int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006E84 File Offset: 0x00005084
		public static void TraceInformation<T0, T1, T2>(int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006EE0 File Offset: 0x000050E0
		public static void TraceInformation(int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006F34 File Offset: 0x00005134
		public static void TraceDebug(int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006F88 File Offset: 0x00005188
		public static void TraceDebug<T0>(int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006FDC File Offset: 0x000051DC
		public static void TraceDebug<T0, T1>(int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007034 File Offset: 0x00005234
		public static void TraceDebug<T0, T1, T2>(int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007090 File Offset: 0x00005290
		public static void TraceDebug(int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000070E4 File Offset: 0x000052E4
		public static void TraceWarning(int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007138 File Offset: 0x00005338
		public static void TraceWarning<T0>(int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000718C File Offset: 0x0000538C
		public static void TraceWarning<T0, T1>(int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000071E4 File Offset: 0x000053E4
		public static void TraceWarning<T0, T1, T2>(int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007240 File Offset: 0x00005440
		public static void TraceWarning(int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007294 File Offset: 0x00005494
		public static void TraceError(int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000072E8 File Offset: 0x000054E8
		public static void TraceError<T0>(int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000733C File Offset: 0x0000553C
		public static void TraceError<T0, T1>(int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007394 File Offset: 0x00005594
		public static void TraceError<T0, T1, T2>(int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000073F0 File Offset: 0x000055F0
		public static void TraceError(int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007444 File Offset: 0x00005644
		public static void TracePfd(int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007498 File Offset: 0x00005698
		public static void TracePfd<T0>(int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000074EC File Offset: 0x000056EC
		public static void TracePfd<T0, T1>(int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007544 File Offset: 0x00005744
		public static void TracePfd<T0, T1, T2>(int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000075A0 File Offset: 0x000057A0
		public static void TracePfd(int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000075F4 File Offset: 0x000057F4
		public static void TracePerformance(int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007648 File Offset: 0x00005848
		public static void TracePerformance<T0>(int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000769C File Offset: 0x0000589C
		public static void TracePerformance<T0, T1>(int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000076F4 File Offset: 0x000058F4
		public static void TracePerformance<T0, T1, T2>(int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007750 File Offset: 0x00005950
		public static void TracePerformance(int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000077A4 File Offset: 0x000059A4
		public static void TraceFunction(int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000077F8 File Offset: 0x000059F8
		public static void TraceFunction<T0>(int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000784C File Offset: 0x00005A4C
		public static void TraceFunction<T0, T1>(int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000078A4 File Offset: 0x00005AA4
		public static void TraceFunction<T0, T1, T2>(int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007900 File Offset: 0x00005B00
		public static void TraceFunction(int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(0, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007954 File Offset: 0x00005B54
		public static void TraceInformation(int lid, int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000079A8 File Offset: 0x00005BA8
		public static void TraceInformation<T0>(int lid, int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007A00 File Offset: 0x00005C00
		public static void TraceInformation<T0, T1>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007A5C File Offset: 0x00005C5C
		public static void TraceInformation<T0, T1, T2>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007ABC File Offset: 0x00005CBC
		public static void TraceInformation(int lid, int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[5])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.InfoTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007B14 File Offset: 0x00005D14
		public static void TraceDebug(int lid, int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007B68 File Offset: 0x00005D68
		public static void TraceDebug<T0>(int lid, int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public static void TraceDebug<T0, T1>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007C1C File Offset: 0x00005E1C
		public static void TraceDebug<T0, T1, T2>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007C7C File Offset: 0x00005E7C
		public static void TraceDebug(int lid, int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[1])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public static void TraceWarning(int lid, int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007D28 File Offset: 0x00005F28
		public static void TraceWarning<T0>(int lid, int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007D80 File Offset: 0x00005F80
		public static void TraceWarning<T0, T1>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007DDC File Offset: 0x00005FDC
		public static void TraceWarning<T0, T1, T2>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007E3C File Offset: 0x0000603C
		public static void TraceWarning(int lid, int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[2])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.WarningTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007E94 File Offset: 0x00006094
		public static void TraceError(int lid, int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007EE8 File Offset: 0x000060E8
		public static void TraceError<T0>(int lid, int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007F40 File Offset: 0x00006140
		public static void TraceError<T0, T1>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007F9C File Offset: 0x0000619C
		public static void TraceError<T0, T1, T2>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007FFC File Offset: 0x000061FC
		public static void TraceError(int lid, int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[3])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.ErrorTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008054 File Offset: 0x00006254
		public static void TracePfd(int lid, int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000080A8 File Offset: 0x000062A8
		public static void TracePfd<T0>(int lid, int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8] && ComponentTrace<TComponentTagsClass>.enabledTags[tag])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000810C File Offset: 0x0000630C
		public static void TracePfd<T0, T1>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00008168 File Offset: 0x00006368
		public static void TracePfd<T0, T1, T2>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000081C8 File Offset: 0x000063C8
		public static void TracePfd(int lid, int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[8])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.PfdTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008220 File Offset: 0x00006420
		public static void TracePerformance(int lid, int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008274 File Offset: 0x00006474
		public static void TracePerformance<T0>(int lid, int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000082CC File Offset: 0x000064CC
		public static void TracePerformance<T0, T1>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008328 File Offset: 0x00006528
		public static void TracePerformance<T0, T1, T2>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008388 File Offset: 0x00006588
		public static void TracePerformance(int lid, int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[6])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.PerformanceTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000083E0 File Offset: 0x000065E0
		public static void TraceFunction(int lid, int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
				}
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008434 File Offset: 0x00006634
		public static void TraceFunction<T0>(int lid, int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0>(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0>(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000848C File Offset: 0x0000668C
		public static void TraceFunction<T0, T1>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1>(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
				}
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000084E8 File Offset: 0x000066E8
		public static void TraceFunction<T0, T1, T2>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
				}
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008548 File Offset: 0x00006748
		public static void TraceFunction(int lid, int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledTypes[7])
			{
				if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
				{
					ExTraceInternal.TraceInMemory(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
				if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
				{
					ExTraceInternal.Trace(lid, TraceType.FunctionTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
				}
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000859E File Offset: 0x0000679E
		public static void Trace(int lid, int tag, long id, string message)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
			{
				ExTraceInternal.TraceInMemory(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
			}
			if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
			{
				ExTraceInternal.Trace(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, message);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000085D8 File Offset: 0x000067D8
		public static void Trace<T0>(int lid, int tag, long id, string formatString, T0 arg0)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
			{
				ExTraceInternal.TraceInMemory<T0>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
			}
			if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
			{
				ExTraceInternal.Trace<T0>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008618 File Offset: 0x00006818
		public static void Trace<T0, T1>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
			{
				ExTraceInternal.TraceInMemory<T0, T1>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
			}
			if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
			{
				ExTraceInternal.Trace<T0, T1>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008668 File Offset: 0x00006868
		public static void Trace<T0, T1, T2>(int lid, int tag, long id, string formatString, T0 arg0, T1 arg1, T2 arg2)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
			{
				ExTraceInternal.TraceInMemory<T0, T1, T2>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
			}
			if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
			{
				ExTraceInternal.Trace<T0, T1, T2>(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, arg0, arg1, arg2);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000086B9 File Offset: 0x000068B9
		public static void Trace(int lid, int tag, long id, string formatString, params object[] args)
		{
			if (ComponentTrace<TComponentTagsClass>.enabledInMemoryTags[tag])
			{
				ExTraceInternal.TraceInMemory(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
			}
			if (ComponentTrace<TComponentTagsClass>.enabledTags[tag])
			{
				ExTraceInternal.Trace(lid, TraceType.DebugTrace, ComponentTrace<TComponentTagsClass>.componentGuid, tag, id, formatString, args);
			}
		}

		// Token: 0x0400024E RID: 590
		private static BitArray enabledTypes = ExTraceConfiguration.Instance.EnabledTypesArray();

		// Token: 0x0400024F RID: 591
		private static BitArray enabledTags = ExTraceConfiguration.Instance.EnabledTagArray(ComponentTrace<TComponentTagsClass>.ComponentGuid());

		// Token: 0x04000250 RID: 592
		private static BitArray enabledInMemoryTags = ExTraceConfiguration.Instance.EnabledInMemoryTagArray(ComponentTrace<TComponentTagsClass>.ComponentGuid());

		// Token: 0x04000251 RID: 593
		private static Guid componentGuid = ComponentTrace<TComponentTagsClass>.ComponentGuid();
	}
}
