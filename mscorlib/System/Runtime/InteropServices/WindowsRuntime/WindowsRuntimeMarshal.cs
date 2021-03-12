using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009C9 RID: 2505
	public static class WindowsRuntimeMarshal
	{
		// Token: 0x060063AD RID: 25517 RVA: 0x00152810 File Offset: 0x00150A10
		[SecurityCritical]
		public static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (addMethod == null)
			{
				throw new ArgumentNullException("addMethod");
			}
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x00152868 File Offset: 0x00150A68
		[SecurityCritical]
		public static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			if (handler == null)
			{
				return;
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x001528B0 File Offset: 0x00150AB0
		[SecurityCritical]
		public static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
		{
			if (removeMethod == null)
			{
				throw new ArgumentNullException("removeMethod");
			}
			object target = removeMethod.Target;
			if (target == null || Marshal.IsComObject(target))
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
				return;
			}
			WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x001528EC File Offset: 0x00150AEC
		internal static int GetRegistrationTokenCacheSize()
		{
			int num = 0;
			if (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations != null)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations)
				{
					num += WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Keys.Count;
				}
			}
			if (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations != null)
			{
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations2 = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				lock (s_eventRegistrations2)
				{
					num += WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Count;
				}
			}
			return num;
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x0015298C File Offset: 0x00150B8C
		internal static void CallRemoveMethods(Action<EventRegistrationToken> removeMethod, List<EventRegistrationToken> tokensToRemove)
		{
			List<Exception> list = new List<Exception>();
			foreach (EventRegistrationToken obj in tokensToRemove)
			{
				try
				{
					removeMethod(obj);
				}
				catch (Exception item)
				{
					list.Add(item);
				}
			}
			if (list.Count > 0)
			{
				throw new AggregateException(list.ToArray());
			}
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x00152A10 File Offset: 0x00150C10
		[SecurityCritical]
		internal unsafe static string HStringToString(IntPtr hstring)
		{
			if (hstring == IntPtr.Zero)
			{
				return string.Empty;
			}
			uint num;
			char* value = UnsafeNativeMethods.WindowsGetStringRawBuffer(hstring, &num);
			return new string(value, 0, checked((int)num));
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x00152A44 File Offset: 0x00150C44
		internal static Exception GetExceptionForHR(int hresult, Exception innerException, string messageResource)
		{
			Exception ex;
			if (innerException != null)
			{
				string text = innerException.Message;
				if (text == null && messageResource != null)
				{
					text = Environment.GetResourceString(messageResource);
				}
				ex = new Exception(text, innerException);
			}
			else
			{
				string message = (messageResource != null) ? Environment.GetResourceString(messageResource) : null;
				ex = new Exception(message);
			}
			ex.SetErrorCode(hresult);
			return ex;
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x00152A90 File Offset: 0x00150C90
		internal static Exception GetExceptionForHR(int hresult, Exception innerException)
		{
			return WindowsRuntimeMarshal.GetExceptionForHR(hresult, innerException, null);
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x00152A9C File Offset: 0x00150C9C
		[SecurityCritical]
		private static bool RoOriginateLanguageException(int error, string message, IntPtr languageException)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					return UnsafeNativeMethods.RoOriginateLanguageException(error, message, languageException);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x00152AD8 File Offset: 0x00150CD8
		[SecurityCritical]
		private static void RoReportUnhandledError(IRestrictedErrorInfo error)
		{
			if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				try
				{
					UnsafeNativeMethods.RoReportUnhandledError(error);
				}
				catch (EntryPointNotFoundException)
				{
					WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
				}
			}
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x00152B10 File Offset: 0x00150D10
		[FriendAccessAllowed]
		[SecuritySafeCritical]
		internal static bool ReportUnhandledError(Exception e)
		{
			if (!AppDomain.IsAppXModel())
			{
				return false;
			}
			if (!WindowsRuntimeMarshal.s_haveBlueErrorApis)
			{
				return false;
			}
			if (e != null)
			{
				IntPtr intPtr = IntPtr.Zero;
				IntPtr zero = IntPtr.Zero;
				try
				{
					intPtr = Marshal.GetIUnknownForObject(e);
					if (intPtr != IntPtr.Zero)
					{
						Marshal.QueryInterface(intPtr, ref WindowsRuntimeMarshal.s_iidIErrorInfo, out zero);
						if (zero != IntPtr.Zero && WindowsRuntimeMarshal.RoOriginateLanguageException(Marshal.GetHRForException_WinRT(e), e.Message, zero))
						{
							IRestrictedErrorInfo restrictedErrorInfo = UnsafeNativeMethods.GetRestrictedErrorInfo();
							if (restrictedErrorInfo != null)
							{
								WindowsRuntimeMarshal.RoReportUnhandledError(restrictedErrorInfo);
								return true;
							}
						}
					}
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						Marshal.Release(zero);
					}
					if (intPtr != IntPtr.Zero)
					{
						Marshal.Release(intPtr);
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x00152BD8 File Offset: 0x00150DD8
		[SecurityCritical]
		internal static IntPtr GetActivationFactoryForType(Type type)
		{
			ManagedActivationFactory managedActivationFactory = WindowsRuntimeMarshal.GetManagedActivationFactory(type);
			return Marshal.GetComInterfaceForObject(managedActivationFactory, typeof(IActivationFactory));
		}

		// Token: 0x060063B9 RID: 25529 RVA: 0x00152BFC File Offset: 0x00150DFC
		[SecurityCritical]
		internal static ManagedActivationFactory GetManagedActivationFactory(Type type)
		{
			ManagedActivationFactory managedActivationFactory = new ManagedActivationFactory(type);
			Marshal.InitializeManagedWinRTFactoryObject(managedActivationFactory, (RuntimeType)type);
			return managedActivationFactory;
		}

		// Token: 0x060063BA RID: 25530 RVA: 0x00152C20 File Offset: 0x00150E20
		[SecurityCritical]
		internal static IntPtr GetClassActivatorForApplication(string appBase)
		{
			if (WindowsRuntimeMarshal.s_pClassActivator == IntPtr.Zero)
			{
				AppDomainSetup info = new AppDomainSetup
				{
					ApplicationBase = appBase
				};
				AppDomain appDomain = AppDomain.CreateDomain(Environment.GetResourceString("WinRTHostDomainName", new object[]
				{
					appBase
				}), null, info);
				WinRTClassActivator winRTClassActivator = (WinRTClassActivator)appDomain.CreateInstanceAndUnwrap(typeof(WinRTClassActivator).Assembly.FullName, typeof(WinRTClassActivator).FullName);
				IntPtr iwinRTClassActivator = winRTClassActivator.GetIWinRTClassActivator();
				if (Interlocked.CompareExchange(ref WindowsRuntimeMarshal.s_pClassActivator, iwinRTClassActivator, IntPtr.Zero) != IntPtr.Zero)
				{
					Marshal.Release(iwinRTClassActivator);
					try
					{
						AppDomain.Unload(appDomain);
					}
					catch (CannotUnloadAppDomainException)
					{
					}
				}
			}
			Marshal.AddRef(WindowsRuntimeMarshal.s_pClassActivator);
			return WindowsRuntimeMarshal.s_pClassActivator;
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x00152CF0 File Offset: 0x00150EF0
		[SecurityCritical]
		public static IActivationFactory GetActivationFactory(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsWindowsRuntimeObject && type.IsImport)
			{
				return (IActivationFactory)Marshal.GetNativeActivationFactory(type);
			}
			return WindowsRuntimeMarshal.GetManagedActivationFactory(type);
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x00152D28 File Offset: 0x00150F28
		[SecurityCritical]
		public unsafe static IntPtr StringToHString(string s)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			IntPtr result;
			int errorCode = UnsafeNativeMethods.WindowsCreateString(s, s.Length, &result);
			Marshal.ThrowExceptionForHR(errorCode, new IntPtr(-1));
			return result;
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x00152D77 File Offset: 0x00150F77
		[SecurityCritical]
		public static string PtrToStringHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			return WindowsRuntimeMarshal.HStringToString(ptr);
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x00152D96 File Offset: 0x00150F96
		[SecurityCritical]
		public static void FreeHString(IntPtr ptr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (ptr != IntPtr.Zero)
			{
				UnsafeNativeMethods.WindowsDeleteString(ptr);
			}
		}

		// Token: 0x04002C6C RID: 11372
		private static bool s_haveBlueErrorApis = true;

		// Token: 0x04002C6D RID: 11373
		private static Guid s_iidIErrorInfo = new Guid(485667104, 21629, 4123, 142, 101, 8, 0, 43, 43, 209, 25);

		// Token: 0x04002C6E RID: 11374
		private static IntPtr s_pClassActivator = IntPtr.Zero;

		// Token: 0x02000C70 RID: 3184
		internal struct EventRegistrationTokenList
		{
			// Token: 0x06007010 RID: 28688 RVA: 0x00180C3A File Offset: 0x0017EE3A
			internal EventRegistrationTokenList(EventRegistrationToken token)
			{
				this.firstToken = token;
				this.restTokens = null;
			}

			// Token: 0x06007011 RID: 28689 RVA: 0x00180C4A File Offset: 0x0017EE4A
			internal EventRegistrationTokenList(WindowsRuntimeMarshal.EventRegistrationTokenList list)
			{
				this.firstToken = list.firstToken;
				this.restTokens = list.restTokens;
			}

			// Token: 0x06007012 RID: 28690 RVA: 0x00180C64 File Offset: 0x0017EE64
			public bool Push(EventRegistrationToken token)
			{
				bool result = false;
				if (this.restTokens == null)
				{
					this.restTokens = new List<EventRegistrationToken>();
					result = true;
				}
				this.restTokens.Add(token);
				return result;
			}

			// Token: 0x06007013 RID: 28691 RVA: 0x00180C98 File Offset: 0x0017EE98
			public bool Pop(out EventRegistrationToken token)
			{
				if (this.restTokens == null || this.restTokens.Count == 0)
				{
					token = this.firstToken;
					return false;
				}
				int index = this.restTokens.Count - 1;
				token = this.restTokens[index];
				this.restTokens.RemoveAt(index);
				return true;
			}

			// Token: 0x06007014 RID: 28692 RVA: 0x00180CF5 File Offset: 0x0017EEF5
			public void CopyTo(List<EventRegistrationToken> tokens)
			{
				tokens.Add(this.firstToken);
				if (this.restTokens != null)
				{
					tokens.AddRange(this.restTokens);
				}
			}

			// Token: 0x0400379F RID: 14239
			private EventRegistrationToken firstToken;

			// Token: 0x040037A0 RID: 14240
			private List<EventRegistrationToken> restTokens;
		}

		// Token: 0x02000C71 RID: 3185
		internal static class ManagedEventRegistrationImpl
		{
			// Token: 0x06007015 RID: 28693 RVA: 0x00180D18 File Offset: 0x0017EF18
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				object target = removeMethod.Target;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(target, removeMethod);
				EventRegistrationToken token = addMethod(handler);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> obj = eventRegistrationTokenTable;
				lock (obj)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList value;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out value))
					{
						value = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
						eventRegistrationTokenTable[handler] = value;
					}
					else
					{
						bool flag2 = value.Push(token);
						if (flag2)
						{
							eventRegistrationTokenTable[handler] = value;
						}
					}
				}
			}

			// Token: 0x06007016 RID: 28694 RVA: 0x00180DAC File Offset: 0x0017EFAC
			private static Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> GetEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod)
			{
				ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> obj = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> result;
				lock (obj)
				{
					Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>> dictionary = null;
					if (!WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.TryGetValue(instance, out dictionary))
					{
						dictionary = new Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>();
						WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Add(instance, dictionary);
					}
					Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary2 = null;
					if (!dictionary.TryGetValue(removeMethod.Method, out dictionary2))
					{
						dictionary2 = new Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>();
						dictionary.Add(removeMethod.Method, dictionary2);
					}
					result = dictionary2;
				}
				return result;
			}

			// Token: 0x06007017 RID: 28695 RVA: 0x00180E38 File Offset: 0x0017F038
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				object target = removeMethod.Target;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(target, removeMethod);
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> obj = eventRegistrationTokenTable;
				EventRegistrationToken obj2;
				lock (obj)
				{
					WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList;
					if (!eventRegistrationTokenTable.TryGetValue(handler, out eventRegistrationTokenList))
					{
						return;
					}
					if (!eventRegistrationTokenList.Pop(out obj2))
					{
						eventRegistrationTokenTable.Remove(handler);
					}
				}
				removeMethod(obj2);
			}

			// Token: 0x06007018 RID: 28696 RVA: 0x00180EB4 File Offset: 0x0017F0B4
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				object target = removeMethod.Target;
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> eventRegistrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(target, removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> obj = eventRegistrationTokenTable;
				lock (obj)
				{
					foreach (WindowsRuntimeMarshal.EventRegistrationTokenList eventRegistrationTokenList in eventRegistrationTokenTable.Values)
					{
						eventRegistrationTokenList.CopyTo(list);
					}
					eventRegistrationTokenTable.Clear();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x040037A1 RID: 14241
			internal static volatile ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = new ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>>();
		}

		// Token: 0x02000C72 RID: 3186
		internal static class NativeOrStaticEventRegistrationImpl
		{
			// Token: 0x0600701A RID: 28698 RVA: 0x00180F64 File Offset: 0x0017F164
			[SecuritySafeCritical]
			private static object GetInstanceKey(Action<EventRegistrationToken> removeMethod)
			{
				object target = removeMethod.Target;
				if (target == null)
				{
					return removeMethod.Method.DeclaringType;
				}
				return Marshal.GetRawIUnknownForComObjectNoAddRef(target);
			}

			// Token: 0x0600701B RID: 28699 RVA: 0x00180F94 File Offset: 0x0017F194
			[SecurityCritical]
			internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				EventRegistrationToken eventRegistrationToken = addMethod(handler);
				bool flag = false;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
					try
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> orCreateEventRegistrationTokenTable = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetOrCreateEventRegistrationTokenTable(instanceKey, removeMethod, out tokenListCount);
						ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> obj = orCreateEventRegistrationTokenTable;
						lock (obj)
						{
							WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
							if (orCreateEventRegistrationTokenTable.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount) == null)
							{
								eventRegistrationTokenListWithCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount(tokenListCount, eventRegistrationToken);
								orCreateEventRegistrationTokenTable.Add(handler, eventRegistrationTokenListWithCount);
							}
							else
							{
								eventRegistrationTokenListWithCount.Push(eventRegistrationToken);
							}
							flag = true;
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
					}
				}
				catch (Exception)
				{
					if (!flag)
					{
						removeMethod(eventRegistrationToken);
					}
					throw;
				}
			}

			// Token: 0x0600701C RID: 28700 RVA: 0x00181068 File Offset: 0x0017F268
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableNoCreate(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, false);
			}

			// Token: 0x0600701D RID: 28701 RVA: 0x00181073 File Offset: 0x0017F273
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetOrCreateEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
			{
				return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, true);
			}

			// Token: 0x0600701E RID: 28702 RVA: 0x00181080 File Offset: 0x0017F280
			private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableInternal(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, bool createIfNotFound)
			{
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key;
				key.target = instance;
				key.method = removeMethod.Method;
				Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> obj = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations;
				ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;
				lock (obj)
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry eventCacheEntry;
					if (!WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.TryGetValue(key, out eventCacheEntry))
					{
						if (!createIfNotFound)
						{
							tokenListCount = null;
							return null;
						}
						eventCacheEntry = default(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry);
						eventCacheEntry.registrationTable = new ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>();
						eventCacheEntry.tokenListCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount(key);
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Add(key, eventCacheEntry);
					}
					tokenListCount = eventCacheEntry.tokenListCount;
					registrationTable = eventCacheEntry.registrationTable;
				}
				return registrationTable;
			}

			// Token: 0x0600701F RID: 28703 RVA: 0x00181130 File Offset: 0x0017F330
			[SecurityCritical]
			internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				EventRegistrationToken obj2;
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> obj = eventRegistrationTokenTableNoCreate;
					lock (obj)
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount;
						object key = eventRegistrationTokenTableNoCreate.FindEquivalentKeyUnsafe(handler, out eventRegistrationTokenListWithCount);
						if (eventRegistrationTokenListWithCount == null)
						{
							return;
						}
						if (!eventRegistrationTokenListWithCount.Pop(out obj2))
						{
							eventRegistrationTokenTableNoCreate.Remove(key);
						}
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				removeMethod(obj2);
			}

			// Token: 0x06007020 RID: 28704 RVA: 0x001811DC File Offset: 0x0017F3DC
			[SecurityCritical]
			internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
			{
				object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
				List<EventRegistrationToken> list = new List<EventRegistrationToken>();
				WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
				try
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> eventRegistrationTokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
					if (eventRegistrationTokenTableNoCreate == null)
					{
						return;
					}
					ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> obj = eventRegistrationTokenTableNoCreate;
					lock (obj)
					{
						foreach (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount eventRegistrationTokenListWithCount in eventRegistrationTokenTableNoCreate.Values)
						{
							eventRegistrationTokenListWithCount.CopyTo(list);
						}
						eventRegistrationTokenTableNoCreate.Clear();
					}
				}
				finally
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
				}
				WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, list);
			}

			// Token: 0x040037A2 RID: 14242
			internal static volatile Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations = new Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry>(new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKeyEqualityComparer());

			// Token: 0x040037A3 RID: 14243
			private static volatile WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock s_eventCacheRWLock = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock();

			// Token: 0x02000CDC RID: 3292
			internal struct EventCacheKey
			{
				// Token: 0x060070FE RID: 28926 RVA: 0x00184A66 File Offset: 0x00182C66
				public override string ToString()
				{
					return string.Concat(new object[]
					{
						"(",
						this.target,
						", ",
						this.method,
						")"
					});
				}

				// Token: 0x04003888 RID: 14472
				internal object target;

				// Token: 0x04003889 RID: 14473
				internal MethodInfo method;
			}

			// Token: 0x02000CDD RID: 3293
			internal class EventCacheKeyEqualityComparer : IEqualityComparer<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey>
			{
				// Token: 0x060070FF RID: 28927 RVA: 0x00184A9D File Offset: 0x00182C9D
				public bool Equals(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey lhs, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey rhs)
				{
					return object.Equals(lhs.target, rhs.target) && object.Equals(lhs.method, rhs.method);
				}

				// Token: 0x06007100 RID: 28928 RVA: 0x00184AC5 File Offset: 0x00182CC5
				public int GetHashCode(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					return key.target.GetHashCode() ^ key.method.GetHashCode();
				}
			}

			// Token: 0x02000CDE RID: 3294
			internal class EventRegistrationTokenListWithCount
			{
				// Token: 0x06007102 RID: 28930 RVA: 0x00184AE6 File Offset: 0x00182CE6
				internal EventRegistrationTokenListWithCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, EventRegistrationToken token)
				{
					this._tokenListCount = tokenListCount;
					this._tokenListCount.Inc();
					this._tokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
				}

				// Token: 0x06007103 RID: 28931 RVA: 0x00184B0C File Offset: 0x00182D0C
				~EventRegistrationTokenListWithCount()
				{
					this._tokenListCount.Dec();
				}

				// Token: 0x06007104 RID: 28932 RVA: 0x00184B40 File Offset: 0x00182D40
				public void Push(EventRegistrationToken token)
				{
					this._tokenList.Push(token);
				}

				// Token: 0x06007105 RID: 28933 RVA: 0x00184B4F File Offset: 0x00182D4F
				public bool Pop(out EventRegistrationToken token)
				{
					return this._tokenList.Pop(out token);
				}

				// Token: 0x06007106 RID: 28934 RVA: 0x00184B5D File Offset: 0x00182D5D
				public void CopyTo(List<EventRegistrationToken> tokens)
				{
					this._tokenList.CopyTo(tokens);
				}

				// Token: 0x0400388A RID: 14474
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount _tokenListCount;

				// Token: 0x0400388B RID: 14475
				private WindowsRuntimeMarshal.EventRegistrationTokenList _tokenList;
			}

			// Token: 0x02000CDF RID: 3295
			internal class TokenListCount
			{
				// Token: 0x06007107 RID: 28935 RVA: 0x00184B6B File Offset: 0x00182D6B
				internal TokenListCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
				{
					this._key = key;
				}

				// Token: 0x17001378 RID: 4984
				// (get) Token: 0x06007108 RID: 28936 RVA: 0x00184B7A File Offset: 0x00182D7A
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey Key
				{
					get
					{
						return this._key;
					}
				}

				// Token: 0x06007109 RID: 28937 RVA: 0x00184B84 File Offset: 0x00182D84
				internal void Inc()
				{
					int num = Interlocked.Increment(ref this._count);
				}

				// Token: 0x0600710A RID: 28938 RVA: 0x00184BA0 File Offset: 0x00182DA0
				internal void Dec()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireWriterLock(-1);
					try
					{
						if (Interlocked.Decrement(ref this._count) == 0)
						{
							this.CleanupCache();
						}
					}
					finally
					{
						WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseWriterLock();
					}
				}

				// Token: 0x0600710B RID: 28939 RVA: 0x00184BF0 File Offset: 0x00182DF0
				private void CleanupCache()
				{
					WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Remove(this._key);
				}

				// Token: 0x0400388C RID: 14476
				private int _count;

				// Token: 0x0400388D RID: 14477
				private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey _key;
			}

			// Token: 0x02000CE0 RID: 3296
			internal struct EventCacheEntry
			{
				// Token: 0x0400388E RID: 14478
				internal ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;

				// Token: 0x0400388F RID: 14479
				internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
			}

			// Token: 0x02000CE1 RID: 3297
			internal class ReaderWriterLockTimedOutException : ApplicationException
			{
			}

			// Token: 0x02000CE2 RID: 3298
			internal class MyReaderWriterLock
			{
				// Token: 0x0600710D RID: 28941 RVA: 0x00184C0D File Offset: 0x00182E0D
				internal MyReaderWriterLock()
				{
				}

				// Token: 0x0600710E RID: 28942 RVA: 0x00184C18 File Offset: 0x00182E18
				internal void AcquireReaderLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners < 0 || this.numWriteWaiters != 0U)
					{
						if (this.readEvent == null)
						{
							this.LazyCreateEvent(ref this.readEvent, false);
						}
						else
						{
							this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
						}
					}
					this.owners++;
					this.ExitMyLock();
				}

				// Token: 0x0600710F RID: 28943 RVA: 0x00184C80 File Offset: 0x00182E80
				internal void AcquireWriterLock(int millisecondsTimeout)
				{
					this.EnterMyLock();
					while (this.owners != 0)
					{
						if (this.writeEvent == null)
						{
							this.LazyCreateEvent(ref this.writeEvent, true);
						}
						else
						{
							this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout);
						}
					}
					this.owners = -1;
					this.ExitMyLock();
				}

				// Token: 0x06007110 RID: 28944 RVA: 0x00184CD6 File Offset: 0x00182ED6
				internal void ReleaseReaderLock()
				{
					this.EnterMyLock();
					this.owners--;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x06007111 RID: 28945 RVA: 0x00184CF2 File Offset: 0x00182EF2
				internal void ReleaseWriterLock()
				{
					this.EnterMyLock();
					this.owners++;
					this.ExitAndWakeUpAppropriateWaiters();
				}

				// Token: 0x06007112 RID: 28946 RVA: 0x00184D10 File Offset: 0x00182F10
				private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
				{
					this.ExitMyLock();
					EventWaitHandle eventWaitHandle;
					if (makeAutoResetEvent)
					{
						eventWaitHandle = new AutoResetEvent(false);
					}
					else
					{
						eventWaitHandle = new ManualResetEvent(false);
					}
					this.EnterMyLock();
					if (waitEvent == null)
					{
						waitEvent = eventWaitHandle;
					}
				}

				// Token: 0x06007113 RID: 28947 RVA: 0x00184D44 File Offset: 0x00182F44
				private void WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
				{
					waitEvent.Reset();
					numWaiters += 1U;
					bool flag = false;
					this.ExitMyLock();
					try
					{
						if (!waitEvent.WaitOne(millisecondsTimeout, false))
						{
							throw new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.ReaderWriterLockTimedOutException();
						}
						flag = true;
					}
					finally
					{
						this.EnterMyLock();
						numWaiters -= 1U;
						if (!flag)
						{
							this.ExitMyLock();
						}
					}
				}

				// Token: 0x06007114 RID: 28948 RVA: 0x00184DA0 File Offset: 0x00182FA0
				private void ExitAndWakeUpAppropriateWaiters()
				{
					if (this.owners == 0 && this.numWriteWaiters > 0U)
					{
						this.ExitMyLock();
						this.writeEvent.Set();
						return;
					}
					if (this.owners >= 0 && this.numReadWaiters != 0U)
					{
						this.ExitMyLock();
						this.readEvent.Set();
						return;
					}
					this.ExitMyLock();
				}

				// Token: 0x06007115 RID: 28949 RVA: 0x00184DFB File Offset: 0x00182FFB
				private void EnterMyLock()
				{
					if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
					{
						this.EnterMyLockSpin();
					}
				}

				// Token: 0x06007116 RID: 28950 RVA: 0x00184E14 File Offset: 0x00183014
				private void EnterMyLockSpin()
				{
					int num = 0;
					for (;;)
					{
						if (num < 3 && Environment.ProcessorCount > 1)
						{
							Thread.SpinWait(20);
						}
						else
						{
							Thread.Sleep(0);
						}
						if (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
						{
							break;
						}
						num++;
					}
				}

				// Token: 0x06007117 RID: 28951 RVA: 0x00184E53 File Offset: 0x00183053
				private void ExitMyLock()
				{
					this.myLock = 0;
				}

				// Token: 0x04003890 RID: 14480
				private int myLock;

				// Token: 0x04003891 RID: 14481
				private int owners;

				// Token: 0x04003892 RID: 14482
				private uint numWriteWaiters;

				// Token: 0x04003893 RID: 14483
				private uint numReadWaiters;

				// Token: 0x04003894 RID: 14484
				private EventWaitHandle writeEvent;

				// Token: 0x04003895 RID: 14485
				private EventWaitHandle readEvent;
			}
		}
	}
}
