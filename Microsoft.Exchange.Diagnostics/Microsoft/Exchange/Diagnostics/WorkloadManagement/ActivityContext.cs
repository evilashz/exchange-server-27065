using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001E5 RID: 485
	internal static class ActivityContext
	{
		// Token: 0x06000DD1 RID: 3537 RVA: 0x00039554 File Offset: 0x00037754
		static ActivityContext()
		{
			ActivityContext.RegisterMetadata(typeof(ActivityStandardMetadata));
			ActivityContext.ConfigureGlobalActivityFromRegistry();
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000DD2 RID: 3538 RVA: 0x00039600 File Offset: 0x00037800
		// (remove) Token: 0x06000DD3 RID: 3539 RVA: 0x00039634 File Offset: 0x00037834
		public static event EventHandler<ActivityEventArgs> OnActivityEvent;

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00039668 File Offset: 0x00037868
		public static Guid? ActivityId
		{
			get
			{
				Guid? result = null;
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null && currentActivityScope.ActivityId != Guid.Empty)
				{
					result = new Guid?(currentActivityScope.ActivityId);
				}
				return result;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x000396A8 File Offset: 0x000378A8
		public static bool IsStarted
		{
			get
			{
				bool flag = false;
				ActivityScopeImpl activityScopeImpl = null;
				Guid? localId = SingleContext.Singleton.LocalId;
				if (localId != null)
				{
					activityScopeImpl = ActivityScopeImpl.GetScopeImpl(localId.Value);
					if (activityScopeImpl != null)
					{
						flag = (activityScopeImpl.Status == ActivityContextStatus.ActivityStarted);
					}
				}
				ExTraceGlobals.ActivityContextTracer.TraceDebug((long)((localId != null) ? localId.Value.GetHashCode() : 0), "IsStarted = {0}, found ActivityScopeImpl object for Activity {1}, LocalId {2}, activityScopeImpl.Status = {3}", new object[]
				{
					flag,
					(activityScopeImpl != null) ? activityScopeImpl.ActivityId : Guid.Empty,
					(localId != null) ? localId.Value : Guid.Empty,
					(activityScopeImpl != null) ? activityScopeImpl.Status : ((ActivityContextStatus)(-1))
				});
				return flag;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0003977B File Offset: 0x0003797B
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x00039782 File Offset: 0x00037982
		internal static bool DisableFriendlyWatsonForTesting { get; set; } = false;

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0003978A File Offset: 0x0003798A
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x00039791 File Offset: 0x00037991
		private static bool IsGlobalScopeEnabled { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00039799 File Offset: 0x00037999
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x000397A0 File Offset: 0x000379A0
		public static int? InitialMetadataCapacity { get; set; }

		// Token: 0x06000DDC RID: 3548 RVA: 0x000397A8 File Offset: 0x000379A8
		public static void RegisterMetadata(Type customMetadataType)
		{
			ExTraceGlobals.ActivityContextTracer.TraceDebug<string>(0L, "ActivityContext.RegisterMetadata - adding to the list of supported ActivityContext keys, type {0}.", customMetadataType.Name);
			if (!customMetadataType.IsSubclassOf(typeof(Enum)))
			{
				try
				{
					throw new ArgumentException(DiagnosticsResources.ExceptionActivityContextEnumMetadataOnly, "customMetadataType");
				}
				catch (ArgumentException)
				{
				}
				return;
			}
			Array values = Enum.GetValues(customMetadataType);
			ListDictionary listDictionary = new ListDictionary();
			lock (ActivityContext.registeredEnumTypes)
			{
				if (!ActivityContext.registeredEnumTypes.Contains(customMetadataType) && !(customMetadataType == typeof(ActivityReadonlyMetadata)))
				{
					for (int i = 0; i < values.Length; i++)
					{
						Enum @enum = (Enum)values.GetValue(i);
						string text = DisplayNameAttribute.GetEnumName(@enum);
						if (ActivityContext.stringToEnum.ContainsKey(text))
						{
							text = @enum.GetType().FullName + "." + @enum.ToString();
							if (ActivityContext.stringToEnum.ContainsKey(text))
							{
								try
								{
									throw new ActivityContextException(DiagnosticsResources.ExceptionActivityContextKeyCollision);
								}
								catch (ActivityContextException)
								{
								}
								return;
							}
						}
						listDictionary.Add(@enum, text);
					}
					Dictionary<Enum, Enum> dictionary = new Dictionary<Enum, Enum>(ActivityContext.preBoxedEnumValues);
					Dictionary<Enum, string> dictionary2 = new Dictionary<Enum, string>(ActivityContext.enumToString);
					Dictionary<string, Enum> dictionary3 = new Dictionary<string, Enum>(ActivityContext.stringToEnum);
					foreach (object obj2 in listDictionary)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
						ExTraceGlobals.ActivityContextTracer.TraceDebug(0L, "ActivityContext.RegisterMetadata - ({0}, '{1}').", new object[]
						{
							dictionaryEntry.Key,
							dictionaryEntry.Value
						});
						Enum enum2 = (Enum)dictionaryEntry.Key;
						dictionary2[enum2] = (string)dictionaryEntry.Value;
						dictionary3[(string)dictionaryEntry.Value] = enum2;
						dictionary[enum2] = enum2;
					}
					ActivityContext.preBoxedEnumValues = dictionary;
					ActivityContext.stringToEnum = dictionary3;
					ActivityContext.enumToString = dictionary2;
					ActivityContext.registeredEnumTypes.Add(customMetadataType);
				}
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00039A20 File Offset: 0x00037C20
		public static ActivityScope Start(object userState, ActivityType activityType)
		{
			return ActivityContext.AddActivityScope(null, userState, activityType, ActivityContext.OnStartEventArgs, DiagnosticsResources.ExceptionStartInvokedTwice(DebugContext.GetDebugInfo()));
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00039A39 File Offset: 0x00037C39
		public static ActivityScope Start(object userState = null)
		{
			return ActivityContext.Start(userState, ActivityType.Request);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00039A44 File Offset: 0x00037C44
		public static ActivityScope Resume(ActivityContextState activityContextState, object userState = null)
		{
			if (activityContextState == null)
			{
				try
				{
					throw new ArgumentNullException("activityContextState was null");
				}
				catch (ArgumentException)
				{
				}
			}
			return ActivityContext.AddActivityScope(activityContextState, userState, (activityContextState != null) ? activityContextState.ActivityType : ActivityType.Request, ActivityContext.OnResumeEventArgs, DiagnosticsResources.ExceptionScopeAlreadyExists(DebugContext.GetDebugInfo()));
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00039A94 File Offset: 0x00037C94
		public static IActivityScope GetCurrentActivityScope()
		{
			Guid? localId = SingleContext.Singleton.LocalId;
			if (localId != null)
			{
				return ActivityScopeImpl.GetActivityScope(localId.Value);
			}
			return null;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00039AC3 File Offset: 0x00037CC3
		public static bool TryGetPreboxedEnum(Enum enumValue, out Enum result)
		{
			return ActivityContext.preBoxedEnumValues.TryGetValue(enumValue, out result);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00039AD4 File Offset: 0x00037CD4
		public static void AddOperation(ActivityOperationType operation, string instance, float value = 0f, int count = 1)
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			ActivityContext.AddOperation(currentActivityScope, operation, instance, value, count);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00039AF4 File Offset: 0x00037CF4
		public static void AddOperation(IActivityScope scope, ActivityOperationType operation, string instance, float value = 0f, int count = 1)
		{
			bool flag = false;
			IActivityScope activityScope = ActivityContext.globalScope;
			TimeInResourcePerfCounter.AddOperation(operation, value);
			if (scope != null && scope.Status == ActivityContextStatus.ActivityStarted)
			{
				flag = scope.AddOperation(operation, instance, value, count);
			}
			if (!ActivityContext.IsGlobalScopeEnabled)
			{
				return;
			}
			if (flag)
			{
				activityScope.AddOperation(operation, "INSTR", value, count);
				return;
			}
			if (scope != null || SingleContext.Singleton.LocalId != null)
			{
				activityScope.AddOperation(operation, "MISSED", value, count);
				return;
			}
			if (DebugContext.GetDebugProperty(DebugProperties.ActivityId) == null)
			{
				activityScope.AddOperation(operation, "UNINSTR", value, count);
				return;
			}
			activityScope.AddOperation(operation, "SUPPR", value, count);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00039B94 File Offset: 0x00037D94
		public static void SetThreadScope(IActivityScope activityScope)
		{
			ActivityContext.ClearThreadScope();
			if (activityScope != null && activityScope.Status == ActivityContextStatus.ActivityStarted)
			{
				SingleContext.Singleton.LocalId = new Guid?(activityScope.LocalId);
				DebugContext.UpdateFrom(activityScope);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00039BC4 File Offset: 0x00037DC4
		public static void ClearThreadScope()
		{
			Guid? localId = SingleContext.Singleton.LocalId;
			ExTraceGlobals.ActivityContextTracer.TraceDebug<Guid?>((long)((localId != null) ? localId.GetHashCode() : 0), "ActivityContext.ClearThreadScope - localId: {0}", (localId != null) ? localId : new Guid?(Guid.Empty));
			SingleContext.Singleton.Clear();
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00039C25 File Offset: 0x00037E25
		public static NullScope SuppressThreadScope()
		{
			return new NullScope();
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00039C2C File Offset: 0x00037E2C
		internal static ActivityScope DeserializeFrom(HttpRequestMessageProperty wcfMessage, object userState = null)
		{
			ActivityContextState activityContextState = ActivityContextState.DeserializeFrom(wcfMessage);
			return ActivityContext.AddActivityScope(activityContextState, userState, (activityContextState != null) ? activityContextState.ActivityType : ActivityType.Request, ActivityContext.OnStartEventArgs, DiagnosticsResources.ExceptionScopeAlreadyExists(DebugContext.GetDebugInfo()));
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00039C64 File Offset: 0x00037E64
		internal static ActivityScope DeserializeFrom(HttpRequest httpRequest, object userState = null)
		{
			ActivityContextState activityContextState = ActivityContextState.DeserializeFrom(httpRequest);
			return ActivityContext.AddActivityScope(activityContextState, userState, (activityContextState != null) ? activityContextState.ActivityType : ActivityType.Request, ActivityContext.OnStartEventArgs, DiagnosticsResources.ExceptionScopeAlreadyExists(DebugContext.GetDebugInfo()));
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00039C9C File Offset: 0x00037E9C
		internal static void RaiseEvent(IActivityScope activityScope, ActivityEventArgs args)
		{
			EventHandler<ActivityEventArgs> onActivityEvent = ActivityContext.OnActivityEvent;
			if (onActivityEvent != null && activityScope != null)
			{
				Guid activityId = activityScope.ActivityId;
				ExTraceGlobals.ActivityContextTracer.TraceDebug<ActivityEventType, Guid, int>(0L, "ActivityContext.RaiseEvent - raising event {0} for ActivityId {1}, and callback {2}.", args.ActivityEventType, activityId, onActivityEvent.GetHashCode());
				onActivityEvent(activityScope, args);
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00039CE4 File Offset: 0x00037EE4
		internal static string LookupEnumName(Enum value)
		{
			string result;
			if (ActivityContext.enumToString.TryGetValue(value, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00039D04 File Offset: 0x00037F04
		internal static Enum LookupEnum(string enumName)
		{
			Enum result;
			if (ActivityContext.stringToEnum.TryGetValue(enumName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00039D23 File Offset: 0x00037F23
		[Conditional("DEBUG")]
		internal static void FriendlyWatson(Exception exception)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
			ActivityContext.RaiseEvent(ActivityContext.GetCurrentActivityScope(), new ActivityEventArgs(ActivityEventType.WatsonActivity, exception.Message));
			if (!ActivityContext.DisableFriendlyWatsonForTesting)
			{
				ExWatson.SendReport(exception, ReportOptions.DoNotCollectDumps | ReportOptions.DeepStackTraceHash, null);
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00039D58 File Offset: 0x00037F58
		internal static void TestHook_UpdateTimer(int globalActivityLifetime, int rollupActivityCycleCount)
		{
			lock (ActivityContext.timerLock)
			{
				ActivityContext.ConfigureGlobalActivity(globalActivityLifetime, rollupActivityCycleCount);
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00039D98 File Offset: 0x00037F98
		internal static void TestHook_ResetTimer()
		{
			lock (ActivityContext.timerLock)
			{
				ActivityContext.ConfigureGlobalActivityFromRegistry();
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00039DD8 File Offset: 0x00037FD8
		internal static void TestHook_BlockRollover(ref bool lockTaken)
		{
			Monitor.Enter(ActivityContext.timerLock, ref lockTaken);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00039DE5 File Offset: 0x00037FE5
		internal static void TestHook_AllowRollover(ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.Exit(ActivityContext.timerLock);
				lockTaken = false;
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00039DF8 File Offset: 0x00037FF8
		internal static T ReadDiagnosticsSettingFromRegistry<T>(string keyName, T defaultValue)
		{
			T result = defaultValue;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics"))
				{
					if (registryKey != null)
					{
						result = (T)((object)registryKey.GetValue(keyName, defaultValue));
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ActivityContextTracer.TraceDebug<string, string, string>(0L, "Exception '{0}' while reading registry key '{1}', value '{2}' most likely key has incorrect value or value type.", ex.ToString(), "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics", keyName);
			}
			return result;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00039E78 File Offset: 0x00038078
		private static ActivityScope AddActivityScope(ActivityContextState activityContextState, object userState, ActivityType activityType, ActivityEventArgs eventArgs, LocalizedString exceptionString)
		{
			bool flag = false;
			bool flag2 = false;
			ActivityScope activityScope = null;
			if (ActivityContext.IsStarted)
			{
				try
				{
					throw new ActivityContextException(exceptionString);
				}
				catch (ActivityContextException)
				{
					flag2 = true;
				}
				if (SingleContext.Singleton.CheckId())
				{
					ActivityScopeImpl activityScopeImpl = (ActivityScopeImpl)ActivityContext.GetCurrentActivityScope();
					if (activityScopeImpl != null)
					{
						return new ActivityScope(activityScopeImpl);
					}
				}
			}
			if (SingleContext.Singleton.LocalId != null && !flag2)
			{
				try
				{
					throw new ActivityContextException(DiagnosticsResources.ExceptionActivityContextMustBeCleared(DebugContext.GetDebugInfo()));
				}
				catch (ActivityContextException)
				{
				}
			}
			ActivityScope result;
			try
			{
				activityScope = ActivityScopeImpl.AddActivityScope(activityContextState);
				activityScope.UserState = userState;
				activityScope.ActivityType = activityType;
				ActivityContext.RaiseEvent(activityScope.ActivityScopeImpl, eventArgs);
				ExTraceGlobals.ActivityContextTracer.TraceDebug<Guid, bool>((long)activityScope.LocalId.GetHashCode(), "ActivityContext.AddActivityScope - ActivityId {0}, (activityContextState != null) = {1}", activityScope.ActivityId, activityContextState != null);
				flag = true;
				result = activityScope;
			}
			finally
			{
				if (!flag)
				{
					if (activityScope != null)
					{
						activityScope.Dispose();
					}
					ActivityContext.ClearThreadScope();
				}
			}
			return result;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00039F84 File Offset: 0x00038184
		private static void OnGlobalActivityTimer(object state)
		{
			bool flag = false;
			try
			{
				Monitor.TryEnter(ActivityContext.timerLock, ref flag);
				if (flag)
				{
					ActivityContext.LogGlobalInactive();
					ActivityContext.RolloverGlobalScope();
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ActivityContext.timerLock);
				}
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00039FD0 File Offset: 0x000381D0
		private static void LogGlobalInactive()
		{
			try
			{
				ActivityScope activityScope = ActivityContext.inactiveGlobalScope;
				if (activityScope != null && activityScope.Status == ActivityContextStatus.ActivityStarted)
				{
					ActivityContext.SetThreadScope(activityScope);
					ActivityCoverageReport.OnGlobalActivityEnded(activityScope);
					activityScope.ActivityScopeImpl.RemoveInstrInstances();
					activityScope.End();
					ActivityContext.inactiveGlobalScope = null;
				}
			}
			finally
			{
				ActivityContext.ClearThreadScope();
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003A02C File Offset: 0x0003822C
		private static void RolloverGlobalScope()
		{
			try
			{
				ActivityScope activityScope = ActivityContext.Start(null, ActivityType.Global);
				activityScope.Action = "GlobalActivity";
				ActivityContext.inactiveGlobalScope = ActivityContext.globalScope;
				ActivityContext.globalScope = activityScope;
			}
			finally
			{
				ActivityContext.ClearThreadScope();
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003A074 File Offset: 0x00038274
		private static void ConfigureGlobalActivityFromRegistry()
		{
			int num = ActivityContext.ReadDiagnosticsSettingFromRegistry<int>("GlobalActivityLifetimeMS", 300000);
			int num2 = ActivityContext.ReadDiagnosticsSettingFromRegistry<int>("RollupActivityCycleCount", 72);
			if (num != -1 && num < 60000)
			{
				num = 60000;
			}
			if (num2 != -1 && num2 <= 0)
			{
				num2 = 72;
			}
			ActivityContext.ConfigureGlobalActivity(num, num2);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003A0C4 File Offset: 0x000382C4
		private static void ConfigureGlobalActivity(int globalActivityLifetimeMS, int rollupActivityCycleCount)
		{
			Timer timer = ActivityContext.globalScopeTimer;
			ActivityContext.globalActivityLifetime = globalActivityLifetimeMS;
			ActivityContext.rollupActivityCycle = rollupActivityCycleCount;
			ActivityContext.IsGlobalScopeEnabled = (globalActivityLifetimeMS != -1);
			if (ActivityContext.IsGlobalScopeEnabled)
			{
				ActivityContext.OnGlobalActivityTimer(null);
				ActivityContext.globalScopeTimer = new Timer(new TimerCallback(ActivityContext.OnGlobalActivityTimer), null, ActivityContext.globalActivityLifetime, ActivityContext.globalActivityLifetime);
			}
			ActivityCoverageReport.Configure(globalActivityLifetimeMS, rollupActivityCycleCount);
			if (timer != null)
			{
				timer.Dispose();
			}
		}

		// Token: 0x040009FF RID: 2559
		internal const string LocalIdPropertyName = "MSExchangeLocalId";

		// Token: 0x04000A00 RID: 2560
		internal const string ContextIdPropertyName = "SingleContextIdKey";

		// Token: 0x04000A01 RID: 2561
		internal const string GlobalUninstrInstance = "UNINSTR";

		// Token: 0x04000A02 RID: 2562
		internal const string GlobalMissedInstance = "MISSED";

		// Token: 0x04000A03 RID: 2563
		internal const string GlobalSupprInstance = "SUPPR";

		// Token: 0x04000A04 RID: 2564
		internal const string ActiveSyncComponentName = "ActiveSync";

		// Token: 0x04000A05 RID: 2565
		internal const string GlobalInstrInstance = "INSTR";

		// Token: 0x04000A06 RID: 2566
		internal const string GlobalActivity = "GlobalActivity";

		// Token: 0x04000A07 RID: 2567
		private const int NullMagicNumber = -1;

		// Token: 0x04000A08 RID: 2568
		private const string DiagnosticsRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics";

		// Token: 0x04000A09 RID: 2569
		private const string GlobalActivityLifetimeRegValue = "GlobalActivityLifetimeMS";

		// Token: 0x04000A0A RID: 2570
		private const string RollupActivityCycleRegValue = "RollupActivityCycleCount";

		// Token: 0x04000A0B RID: 2571
		private const int DefaultGlobalActivityLifetime = 300000;

		// Token: 0x04000A0C RID: 2572
		private const int DefaultRollupActivityCycle = 72;

		// Token: 0x04000A0D RID: 2573
		internal static readonly ActivityEventArgs OnSuspendEventArgs = new ActivityEventArgs(ActivityEventType.SuspendActivity, null);

		// Token: 0x04000A0E RID: 2574
		internal static readonly ActivityEventArgs OnEndEventArgs = new ActivityEventArgs(ActivityEventType.EndActivity, null);

		// Token: 0x04000A0F RID: 2575
		private static readonly ActivityEventArgs OnStartEventArgs = new ActivityEventArgs(ActivityEventType.StartActivity, null);

		// Token: 0x04000A10 RID: 2576
		private static readonly ActivityEventArgs OnResumeEventArgs = new ActivityEventArgs(ActivityEventType.ResumeActivity, null);

		// Token: 0x04000A11 RID: 2577
		private static int globalActivityLifetime = 300000;

		// Token: 0x04000A12 RID: 2578
		private static int rollupActivityCycle = 72;

		// Token: 0x04000A13 RID: 2579
		private static Dictionary<Enum, string> enumToString = new Dictionary<Enum, string>();

		// Token: 0x04000A14 RID: 2580
		private static Dictionary<string, Enum> stringToEnum = new Dictionary<string, Enum>();

		// Token: 0x04000A15 RID: 2581
		private static Dictionary<Enum, Enum> preBoxedEnumValues = new Dictionary<Enum, Enum>();

		// Token: 0x04000A16 RID: 2582
		private static HashSet<Type> registeredEnumTypes = new HashSet<Type>();

		// Token: 0x04000A17 RID: 2583
		private static ActivityScope globalScope = null;

		// Token: 0x04000A18 RID: 2584
		private static ActivityScope inactiveGlobalScope = null;

		// Token: 0x04000A19 RID: 2585
		private static object timerLock = new object();

		// Token: 0x04000A1A RID: 2586
		private static Timer globalScopeTimer = null;
	}
}
