﻿using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200003C RID: 60
	internal static class AppContextSwitches
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00005A3F File Offset: 0x00003C3F
		public static bool NoAsyncCurrentCulture
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchNoAsyncCurrentCulture, ref AppContextSwitches._noAsyncCurrentCulture);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00005A50 File Offset: 0x00003C50
		public static bool EnforceJapaneseEraYearRanges
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchEnforceJapaneseEraYearRanges, ref AppContextSwitches._enforceJapaneseEraYearRanges);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00005A61 File Offset: 0x00003C61
		public static bool FormatJapaneseFirstYearAsANumber
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchFormatJapaneseFirstYearAsANumber, ref AppContextSwitches._formatJapaneseFirstYearAsANumber);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00005A72 File Offset: 0x00003C72
		public static bool EnforceLegacyJapaneseDateParsing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchEnforceLegacyJapaneseDateParsing, ref AppContextSwitches._enforceLegacyJapaneseDateParsing);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00005A83 File Offset: 0x00003C83
		public static bool ThrowExceptionIfDisposedCancellationTokenSource
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchThrowExceptionIfDisposedCancellationTokenSource, ref AppContextSwitches._throwExceptionIfDisposedCancellationTokenSource);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00005A94 File Offset: 0x00003C94
		public static bool UseConcurrentFormatterTypeCache
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchUseConcurrentFormatterTypeCache, ref AppContextSwitches._useConcurrentFormatterTypeCache);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public static bool PreserveEventListnerObjectIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchPreserveEventListnerObjectIdentity, ref AppContextSwitches._preserveEventListnerObjectIdentity);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00005AB6 File Offset: 0x00003CB6
		public static bool UseLegacyPathHandling
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchUseLegacyPathHandling, ref AppContextSwitches._useLegacyPathHandling);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00005AC7 File Offset: 0x00003CC7
		public static bool BlockLongPaths
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchBlockLongPaths, ref AppContextSwitches._blockLongPaths);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public static bool SetActorAsReferenceWhenCopyingClaimsIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchSetActorAsReferenceWhenCopyingClaimsIdentity, ref AppContextSwitches._cloneActor);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00005AE9 File Offset: 0x00003CE9
		public static bool DoNotAddrOfCspParentWindowHandle
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchDoNotAddrOfCspParentWindowHandle, ref AppContextSwitches._doNotAddrOfCspParentWindowHandle);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00005AFA File Offset: 0x00003CFA
		public static bool IgnorePortablePDBsInStackTraces
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchIgnorePortablePDBsInStackTraces, ref AppContextSwitches._ignorePortablePDBsInStackTraces);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00005B0B File Offset: 0x00003D0B
		public static bool UseNewMaxArraySize
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchUseNewMaxArraySize, ref AppContextSwitches._useNewMaxArraySize);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00005B1C File Offset: 0x00003D1C
		public static bool UseLegacyExecutionContextBehaviorUponUndoFailure
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchUseLegacyExecutionContextBehaviorUponUndoFailure, ref AppContextSwitches._useLegacyExecutionContextBehaviorUponUndoFailure);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00005B2D File Offset: 0x00003D2D
		public static bool UseLegacyFipsThrow
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchCryptographyUseLegacyFipsThrow, ref AppContextSwitches._useLegacyFipsThrow);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00005B3E File Offset: 0x00003D3E
		public static bool DoNotMarshalOutByrefSafeArrayOnInvoke
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchDoNotMarshalOutByrefSafeArrayOnInvoke, ref AppContextSwitches._doNotMarshalOutByrefSafeArrayOnInvoke);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00005B4F File Offset: 0x00003D4F
		public static bool UseNetCoreTimer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchUseNetCoreTimer, ref AppContextSwitches._useNetCoreTimer);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00005B60 File Offset: 0x00003D60
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00005B67 File Offset: 0x00003D67
		private static bool DisableCaching { get; set; }

		// Token: 0x06000226 RID: 550 RVA: 0x00005B70 File Offset: 0x00003D70
		static AppContextSwitches()
		{
			bool disableCaching;
			if (AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out disableCaching))
			{
				AppContextSwitches.DisableCaching = disableCaching;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00005B91 File Offset: 0x00003D91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || AppContextSwitches.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00005BA8 File Offset: 0x00003DA8
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			bool flag;
			AppContext.TryGetSwitch(switchName, out flag);
			if (AppContextSwitches.DisableCaching)
			{
				return flag;
			}
			switchValue = (flag ? 1 : -1);
			return flag;
		}

		// Token: 0x040001DC RID: 476
		private static int _noAsyncCurrentCulture;

		// Token: 0x040001DD RID: 477
		private static int _enforceJapaneseEraYearRanges;

		// Token: 0x040001DE RID: 478
		private static int _formatJapaneseFirstYearAsANumber;

		// Token: 0x040001DF RID: 479
		private static int _enforceLegacyJapaneseDateParsing;

		// Token: 0x040001E0 RID: 480
		private static int _throwExceptionIfDisposedCancellationTokenSource;

		// Token: 0x040001E1 RID: 481
		private static int _useConcurrentFormatterTypeCache;

		// Token: 0x040001E2 RID: 482
		private static int _preserveEventListnerObjectIdentity;

		// Token: 0x040001E3 RID: 483
		private static int _useLegacyPathHandling;

		// Token: 0x040001E4 RID: 484
		private static int _blockLongPaths;

		// Token: 0x040001E5 RID: 485
		private static int _cloneActor;

		// Token: 0x040001E6 RID: 486
		private static int _doNotAddrOfCspParentWindowHandle;

		// Token: 0x040001E7 RID: 487
		private static int _ignorePortablePDBsInStackTraces;

		// Token: 0x040001E8 RID: 488
		private static int _useNewMaxArraySize;

		// Token: 0x040001E9 RID: 489
		private static int _useLegacyExecutionContextBehaviorUponUndoFailure;

		// Token: 0x040001EA RID: 490
		private static int _useLegacyFipsThrow;

		// Token: 0x040001EB RID: 491
		private static int _doNotMarshalOutByrefSafeArrayOnInvoke;

		// Token: 0x040001EC RID: 492
		private static int _useNetCoreTimer;
	}
}
