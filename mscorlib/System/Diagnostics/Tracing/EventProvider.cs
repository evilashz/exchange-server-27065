using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003F2 RID: 1010
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal class EventProvider : IDisposable
	{
		// Token: 0x06003331 RID: 13105 RVA: 0x000C2925 File Offset: 0x000C0B25
		[SecurityCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		protected EventProvider(Guid providerGuid)
		{
			this.m_providerId = providerGuid;
			this.Register(providerGuid);
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000C293B File Offset: 0x000C0B3B
		internal EventProvider()
		{
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000C2944 File Offset: 0x000C0B44
		[SecurityCritical]
		internal void Register(Guid providerGuid)
		{
			this.m_providerId = providerGuid;
			this.m_etwCallback = new UnsafeNativeMethods.ManifestEtw.EtwEnableCallback(this.EtwEnableCallBack);
			uint num = this.EventRegister(ref this.m_providerId, this.m_etwCallback);
			if (num != 0U)
			{
				throw new ArgumentException(Win32Native.GetMessage((int)num));
			}
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000C298C File Offset: 0x000C0B8C
		[SecurityCritical]
		internal unsafe int SetInformation(UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS eventInfoClass, void* data, int dataSize)
		{
			int result = 50;
			if (!EventProvider.m_setInformationMissing)
			{
				try
				{
					result = UnsafeNativeMethods.ManifestEtw.EventSetInformation(this.m_regHandle, eventInfoClass, data, dataSize);
				}
				catch (TypeLoadException)
				{
					EventProvider.m_setInformationMissing = true;
				}
			}
			return result;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000C29D0 File Offset: 0x000C0BD0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000C29E0 File Offset: 0x000C0BE0
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			this.m_enabled = false;
			long num = 0L;
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (this.m_disposed)
				{
					return;
				}
				num = this.m_regHandle;
				this.m_regHandle = 0L;
				this.m_disposed = true;
			}
			if (num != 0L)
			{
				this.EventUnregister(num);
			}
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000C2A58 File Offset: 0x000C0C58
		public virtual void Close()
		{
			this.Dispose();
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000C2A60 File Offset: 0x000C0C60
		~EventProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000C2A90 File Offset: 0x000C0C90
		[SecurityCritical]
		private unsafe void EtwEnableCallBack([In] ref Guid sourceId, [In] int controlCode, [In] byte setLevel, [In] long anyKeyword, [In] long allKeyword, [In] UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, [In] void* callbackContext)
		{
			try
			{
				ControllerCommand command = ControllerCommand.Update;
				IDictionary<string, string> dictionary = null;
				bool flag = false;
				if (controlCode == 1)
				{
					this.m_enabled = true;
					this.m_level = setLevel;
					this.m_anyKeywordMask = anyKeyword;
					this.m_allKeywordMask = allKeyword;
					List<Tuple<EventProvider.SessionInfo, bool>> sessions = this.GetSessions();
					using (List<Tuple<EventProvider.SessionInfo, bool>>.Enumerator enumerator = sessions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Tuple<EventProvider.SessionInfo, bool> tuple = enumerator.Current;
							int sessionIdBit = tuple.Item1.sessionIdBit;
							int etwSessionId = tuple.Item1.etwSessionId;
							bool item = tuple.Item2;
							flag = true;
							dictionary = null;
							if (sessions.Count > 1)
							{
								filterData = null;
							}
							byte[] array;
							int i;
							if (item && this.GetDataFromController(etwSessionId, filterData, out command, out array, out i))
							{
								dictionary = new Dictionary<string, string>(4);
								while (i < array.Length)
								{
									int num = EventProvider.FindNull(array, i);
									int num2 = num + 1;
									int num3 = EventProvider.FindNull(array, num2);
									if (num3 < array.Length)
									{
										string @string = Encoding.UTF8.GetString(array, i, num - i);
										string string2 = Encoding.UTF8.GetString(array, num2, num3 - num2);
										dictionary[@string] = string2;
									}
									i = num3 + 1;
								}
							}
							this.OnControllerCommand(command, dictionary, item ? sessionIdBit : (-sessionIdBit), etwSessionId);
						}
						goto IL_162;
					}
				}
				if (controlCode == 0)
				{
					this.m_enabled = false;
					this.m_level = 0;
					this.m_anyKeywordMask = 0L;
					this.m_allKeywordMask = 0L;
					this.m_liveSessions = null;
				}
				else
				{
					if (controlCode != 2)
					{
						return;
					}
					command = ControllerCommand.SendManifest;
				}
				IL_162:
				if (!flag)
				{
					this.OnControllerCommand(command, dictionary, 0, 0);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000C2C48 File Offset: 0x000C0E48
		protected virtual void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int sessionId, int etwSessionId)
		{
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000C2C4A File Offset: 0x000C0E4A
		// (set) Token: 0x0600333C RID: 13116 RVA: 0x000C2C52 File Offset: 0x000C0E52
		protected EventLevel Level
		{
			get
			{
				return (EventLevel)this.m_level;
			}
			set
			{
				this.m_level = (byte)value;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000C2C5C File Offset: 0x000C0E5C
		// (set) Token: 0x0600333E RID: 13118 RVA: 0x000C2C64 File Offset: 0x000C0E64
		protected EventKeywords MatchAnyKeyword
		{
			get
			{
				return (EventKeywords)this.m_anyKeywordMask;
			}
			set
			{
				this.m_anyKeywordMask = (long)value;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000C2C6D File Offset: 0x000C0E6D
		// (set) Token: 0x06003340 RID: 13120 RVA: 0x000C2C75 File Offset: 0x000C0E75
		protected EventKeywords MatchAllKeyword
		{
			get
			{
				return (EventKeywords)this.m_allKeywordMask;
			}
			set
			{
				this.m_allKeywordMask = (long)value;
			}
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000C2C7E File Offset: 0x000C0E7E
		private static int FindNull(byte[] buffer, int idx)
		{
			while (idx < buffer.Length && buffer[idx] != 0)
			{
				idx++;
			}
			return idx;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000C2C94 File Offset: 0x000C0E94
		[SecuritySafeCritical]
		private List<Tuple<EventProvider.SessionInfo, bool>> GetSessions()
		{
			List<EventProvider.SessionInfo> liveSessionList = null;
			this.GetSessionInfo(delegate(int etwSessionId, long matchAllKeywords)
			{
				EventProvider.GetSessionInfoCallback(etwSessionId, matchAllKeywords, ref liveSessionList);
			});
			List<Tuple<EventProvider.SessionInfo, bool>> list = new List<Tuple<EventProvider.SessionInfo, bool>>();
			if (this.m_liveSessions != null)
			{
				foreach (EventProvider.SessionInfo sessionInfo in this.m_liveSessions)
				{
					int index;
					if ((index = EventProvider.IndexOfSessionInList(liveSessionList, sessionInfo.etwSessionId)) < 0 || liveSessionList[index].sessionIdBit != sessionInfo.sessionIdBit)
					{
						list.Add(Tuple.Create<EventProvider.SessionInfo, bool>(sessionInfo, false));
					}
				}
			}
			if (liveSessionList != null)
			{
				foreach (EventProvider.SessionInfo sessionInfo2 in liveSessionList)
				{
					int index2;
					if ((index2 = EventProvider.IndexOfSessionInList(this.m_liveSessions, sessionInfo2.etwSessionId)) < 0 || this.m_liveSessions[index2].sessionIdBit != sessionInfo2.sessionIdBit)
					{
						list.Add(Tuple.Create<EventProvider.SessionInfo, bool>(sessionInfo2, true));
					}
				}
			}
			this.m_liveSessions = liveSessionList;
			return list;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000C2DE0 File Offset: 0x000C0FE0
		private static void GetSessionInfoCallback(int etwSessionId, long matchAllKeywords, ref List<EventProvider.SessionInfo> sessionList)
		{
			uint n = (uint)SessionMask.FromEventKeywords((ulong)matchAllKeywords);
			if (EventProvider.bitcount(n) > 1)
			{
				return;
			}
			if (sessionList == null)
			{
				sessionList = new List<EventProvider.SessionInfo>(8);
			}
			if (EventProvider.bitcount(n) == 1)
			{
				sessionList.Add(new EventProvider.SessionInfo(EventProvider.bitindex(n) + 1, etwSessionId));
				return;
			}
			sessionList.Add(new EventProvider.SessionInfo(EventProvider.bitcount((uint)SessionMask.All) + 1, etwSessionId));
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000C2E4C File Offset: 0x000C104C
		[SecurityCritical]
		private unsafe void GetSessionInfo(Action<int, long> action)
		{
			int num = 256;
			byte* ptr2;
			int num2;
			do
			{
				byte* ptr = stackalloc byte[checked(unchecked((UIntPtr)num) * 1)];
				ptr2 = ptr;
				fixed (Guid* ptr3 = &this.m_providerId)
				{
					num2 = UnsafeNativeMethods.ManifestEtw.EnumerateTraceGuidsEx(UnsafeNativeMethods.ManifestEtw.TRACE_QUERY_INFO_CLASS.TraceGuidQueryInfo, (void*)ptr3, sizeof(Guid), (void*)ptr2, num, ref num);
				}
				if (num2 == 0)
				{
					goto IL_40;
				}
			}
			while (num2 == 122);
			return;
			IL_40:
			UnsafeNativeMethods.ManifestEtw.TRACE_GUID_INFO* ptr4 = (UnsafeNativeMethods.ManifestEtw.TRACE_GUID_INFO*)ptr2;
			UnsafeNativeMethods.ManifestEtw.TRACE_PROVIDER_INSTANCE_INFO* ptr5 = (UnsafeNativeMethods.ManifestEtw.TRACE_PROVIDER_INSTANCE_INFO*)(ptr4 + 1);
			int currentProcessId = (int)Win32Native.GetCurrentProcessId();
			for (int i = 0; i < ptr4->InstanceCount; i++)
			{
				if (ptr5->Pid == currentProcessId)
				{
					UnsafeNativeMethods.ManifestEtw.TRACE_ENABLE_INFO* ptr6 = (UnsafeNativeMethods.ManifestEtw.TRACE_ENABLE_INFO*)(ptr5 + 1);
					for (int j = 0; j < ptr5->EnableCount; j++)
					{
						action((int)ptr6[j].LoggerId, ptr6[j].MatchAllKeyword);
					}
				}
				if (ptr5->NextOffset == 0)
				{
					break;
				}
				byte* ptr7 = (byte*)ptr5;
				ptr5 = (UnsafeNativeMethods.ManifestEtw.TRACE_PROVIDER_INSTANCE_INFO*)(ptr7 + ptr5->NextOffset);
			}
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000C2F2C File Offset: 0x000C112C
		private static int IndexOfSessionInList(List<EventProvider.SessionInfo> sessions, int etwSessionId)
		{
			if (sessions == null)
			{
				return -1;
			}
			for (int i = 0; i < sessions.Count; i++)
			{
				if (sessions[i].etwSessionId == etwSessionId)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000C2F64 File Offset: 0x000C1164
		[SecurityCritical]
		private unsafe bool GetDataFromController(int etwSessionId, UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, out ControllerCommand command, out byte[] data, out int dataStart)
		{
			data = null;
			dataStart = 0;
			if (filterData != null)
			{
				if (filterData->Ptr != 0L && 0 < filterData->Size && filterData->Size <= 1024)
				{
					data = new byte[filterData->Size];
					Marshal.Copy((IntPtr)filterData->Ptr, data, 0, data.Length);
				}
				command = (ControllerCommand)filterData->Type;
				return true;
			}
			string text = "\\Microsoft\\Windows\\CurrentVersion\\Winevt\\Publishers\\{" + this.m_providerId + "}";
			if (Marshal.SizeOf(typeof(IntPtr)) == 8)
			{
				text = "HKEY_LOCAL_MACHINE\\Software\\Wow6432Node" + text;
			}
			else
			{
				text = "HKEY_LOCAL_MACHINE\\Software" + text;
			}
			string valueName = "ControllerData_Session_" + etwSessionId.ToString(CultureInfo.InvariantCulture);
			new RegistryPermission(RegistryPermissionAccess.Read, text).Assert();
			data = (Registry.GetValue(text, valueName, null) as byte[]);
			if (data != null)
			{
				command = ControllerCommand.Update;
				return true;
			}
			command = ControllerCommand.Update;
			return false;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000C3058 File Offset: 0x000C1258
		public bool IsEnabled()
		{
			return this.m_enabled;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000C3060 File Offset: 0x000C1260
		public bool IsEnabled(byte level, long keywords)
		{
			return this.m_enabled && ((level <= this.m_level || this.m_level == 0) && (keywords == 0L || ((keywords & this.m_anyKeywordMask) != 0L && (keywords & this.m_allKeywordMask) == this.m_allKeywordMask)));
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000C309D File Offset: 0x000C129D
		internal bool IsValid()
		{
			return this.m_regHandle != 0L;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000C30A9 File Offset: 0x000C12A9
		public static EventProvider.WriteEventErrorCode GetLastWriteEventError()
		{
			return EventProvider.s_returnCode;
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000C30B0 File Offset: 0x000C12B0
		private static void SetLastError(int error)
		{
			if (error != 8)
			{
				if (error == 234 || error == 534)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return;
				}
			}
			else
			{
				EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.NoFreeBuffers;
			}
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000C30D4 File Offset: 0x000C12D4
		[SecurityCritical]
		private unsafe static object EncodeObject(ref object data, ref EventProvider.EventData* dataDescriptor, ref byte* dataBuffer, ref uint totalEventSize)
		{
			string text;
			byte[] array;
			for (;;)
			{
				dataDescriptor.Reserved = 0U;
				text = (data as string);
				array = null;
				if (text != null)
				{
					break;
				}
				if ((array = (data as byte[])) != null)
				{
					goto Block_1;
				}
				if (data is IntPtr)
				{
					goto Block_2;
				}
				if (data is int)
				{
					goto Block_3;
				}
				if (data is long)
				{
					goto Block_4;
				}
				if (data is uint)
				{
					goto Block_5;
				}
				if (data is ulong)
				{
					goto Block_6;
				}
				if (data is char)
				{
					goto Block_7;
				}
				if (data is byte)
				{
					goto Block_8;
				}
				if (data is short)
				{
					goto Block_9;
				}
				if (data is sbyte)
				{
					goto Block_10;
				}
				if (data is ushort)
				{
					goto Block_11;
				}
				if (data is float)
				{
					goto Block_12;
				}
				if (data is double)
				{
					goto Block_13;
				}
				if (data is bool)
				{
					goto Block_14;
				}
				if (data is Guid)
				{
					goto Block_16;
				}
				if (data is decimal)
				{
					goto Block_17;
				}
				if (data is DateTime)
				{
					goto Block_18;
				}
				if (!(data is Enum))
				{
					goto IL_40C;
				}
				Type underlyingType = Enum.GetUnderlyingType(data.GetType());
				if (underlyingType == typeof(int))
				{
					data = ((IConvertible)data).ToInt32(null);
				}
				else
				{
					if (!(underlyingType == typeof(long)))
					{
						goto IL_40C;
					}
					data = ((IConvertible)data).ToInt64(null);
				}
			}
			dataDescriptor.Size = (uint)((text.Length + 1) * 2);
			goto IL_431;
			Block_1:
			*dataBuffer = array.Length;
			dataDescriptor.Ptr = (ulong)dataBuffer;
			dataDescriptor.Size = 4U;
			totalEventSize += dataDescriptor.Size;
			dataDescriptor += (IntPtr)sizeof(EventProvider.EventData);
			dataBuffer += 16;
			dataDescriptor.Size = (uint)array.Length;
			goto IL_431;
			Block_2:
			dataDescriptor.Size = (uint)sizeof(IntPtr);
			IntPtr* ptr = dataBuffer;
			*ptr = (IntPtr)data;
			dataDescriptor.Ptr = ptr;
			goto IL_431;
			Block_3:
			dataDescriptor.Size = 4U;
			int* ptr2 = dataBuffer;
			*ptr2 = (int)data;
			dataDescriptor.Ptr = ptr2;
			goto IL_431;
			Block_4:
			dataDescriptor.Size = 8U;
			long* ptr3 = dataBuffer;
			*ptr3 = (long)data;
			dataDescriptor.Ptr = ptr3;
			goto IL_431;
			Block_5:
			dataDescriptor.Size = 4U;
			uint* ptr4 = dataBuffer;
			*ptr4 = (uint)data;
			dataDescriptor.Ptr = ptr4;
			goto IL_431;
			Block_6:
			dataDescriptor.Size = 8U;
			ulong* ptr5 = dataBuffer;
			*ptr5 = (ulong)data;
			dataDescriptor.Ptr = ptr5;
			goto IL_431;
			Block_7:
			dataDescriptor.Size = 2U;
			char* ptr6 = dataBuffer;
			*ptr6 = (char)data;
			dataDescriptor.Ptr = ptr6;
			goto IL_431;
			Block_8:
			dataDescriptor.Size = 1U;
			byte* ptr7 = dataBuffer;
			*ptr7 = (byte)data;
			dataDescriptor.Ptr = ptr7;
			goto IL_431;
			Block_9:
			dataDescriptor.Size = 2U;
			short* ptr8 = dataBuffer;
			*ptr8 = (short)data;
			dataDescriptor.Ptr = ptr8;
			goto IL_431;
			Block_10:
			dataDescriptor.Size = 1U;
			sbyte* ptr9 = dataBuffer;
			*ptr9 = (sbyte)data;
			dataDescriptor.Ptr = ptr9;
			goto IL_431;
			Block_11:
			dataDescriptor.Size = 2U;
			ushort* ptr10 = dataBuffer;
			*ptr10 = (ushort)data;
			dataDescriptor.Ptr = ptr10;
			goto IL_431;
			Block_12:
			dataDescriptor.Size = 4U;
			float* ptr11 = dataBuffer;
			*ptr11 = (float)data;
			dataDescriptor.Ptr = ptr11;
			goto IL_431;
			Block_13:
			dataDescriptor.Size = 8U;
			double* ptr12 = dataBuffer;
			*ptr12 = (double)data;
			dataDescriptor.Ptr = ptr12;
			goto IL_431;
			Block_14:
			dataDescriptor.Size = 4U;
			int* ptr13 = dataBuffer;
			if ((bool)data)
			{
				*ptr13 = 1;
			}
			else
			{
				*ptr13 = 0;
			}
			dataDescriptor.Ptr = ptr13;
			goto IL_431;
			Block_16:
			dataDescriptor.Size = (uint)sizeof(Guid);
			Guid* ptr14 = dataBuffer;
			*ptr14 = (Guid)data;
			dataDescriptor.Ptr = ptr14;
			goto IL_431;
			Block_17:
			dataDescriptor.Size = 16U;
			decimal* ptr15 = dataBuffer;
			*ptr15 = (decimal)data;
			dataDescriptor.Ptr = ptr15;
			goto IL_431;
			Block_18:
			long num = 0L;
			if (((DateTime)data).Ticks > 504911232000000000L)
			{
				num = ((DateTime)data).ToFileTimeUtc();
			}
			dataDescriptor.Size = 8U;
			long* ptr16 = dataBuffer;
			*ptr16 = num;
			dataDescriptor.Ptr = ptr16;
			goto IL_431;
			IL_40C:
			if (data == null)
			{
				text = "";
			}
			else
			{
				text = data.ToString();
			}
			dataDescriptor.Size = (uint)((text.Length + 1) * 2);
			IL_431:
			totalEventSize += dataDescriptor.Size;
			dataDescriptor += (IntPtr)sizeof(EventProvider.EventData);
			dataBuffer += 16;
			return text ?? array;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000C3538 File Offset: 0x000C1738
		[SecurityCritical]
		internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* childActivityID, params object[] eventPayload)
		{
			int num = 0;
			if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
			{
				int num2 = eventPayload.Length;
				if (num2 > 128)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
					return false;
				}
				uint num3 = 0U;
				int i = 0;
				List<int> list = new List<int>(8);
				List<object> list2 = new List<object>(8);
				EventProvider.EventData* ptr;
				EventProvider.EventData* ptr2;
				byte* ptr4;
				bool flag;
				checked
				{
					ptr = stackalloc EventProvider.EventData[unchecked((UIntPtr)(2 * num2)) * (UIntPtr)sizeof(EventProvider.EventData)];
					ptr2 = ptr;
					byte* ptr3 = stackalloc byte[unchecked((UIntPtr)(32 * num2)) * 1];
					ptr4 = ptr3;
					flag = false;
				}
				for (int j = 0; j < eventPayload.Length; j++)
				{
					if (eventPayload[j] == null)
					{
						EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.NullInput;
						return false;
					}
					object obj = EventProvider.EncodeObject(ref eventPayload[j], ref ptr2, ref ptr4, ref num3);
					if (obj != null)
					{
						int num4 = (int)((long)(ptr2 - ptr) - 1L);
						if (!(obj is string))
						{
							if (eventPayload.Length + num4 + 1 - j > 128)
							{
								EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
								return false;
							}
							flag = true;
						}
						list2.Add(obj);
						list.Add(num4);
						i++;
					}
				}
				num2 = (int)((long)(ptr2 - ptr));
				if (num3 > 65482U)
				{
					EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
					return false;
				}
				if (!flag && i < 8)
				{
					while (i < 8)
					{
						list2.Add(null);
						i++;
					}
					fixed (string text = (string)list2[0])
					{
						char* ptr5 = text;
						if (ptr5 != null)
						{
							ptr5 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text2 = (string)list2[1])
						{
							char* ptr6 = text2;
							if (ptr6 != null)
							{
								ptr6 += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text3 = (string)list2[2])
							{
								char* ptr7 = text3;
								if (ptr7 != null)
								{
									ptr7 += RuntimeHelpers.OffsetToStringData / 2;
								}
								fixed (string text4 = (string)list2[3])
								{
									char* ptr8 = text4;
									if (ptr8 != null)
									{
										ptr8 += RuntimeHelpers.OffsetToStringData / 2;
									}
									fixed (string text5 = (string)list2[4])
									{
										char* ptr9 = text5;
										if (ptr9 != null)
										{
											ptr9 += RuntimeHelpers.OffsetToStringData / 2;
										}
										fixed (string text6 = (string)list2[5])
										{
											char* ptr10 = text6;
											if (ptr10 != null)
											{
												ptr10 += RuntimeHelpers.OffsetToStringData / 2;
											}
											fixed (string text7 = (string)list2[6])
											{
												char* ptr11 = text7;
												if (ptr11 != null)
												{
													ptr11 += RuntimeHelpers.OffsetToStringData / 2;
												}
												fixed (string text8 = (string)list2[7])
												{
													char* ptr12 = text8;
													if (ptr12 != null)
													{
														ptr12 += RuntimeHelpers.OffsetToStringData / 2;
													}
													ptr2 = ptr;
													if (list2[0] != null)
													{
														ptr2[list[0]].Ptr = ptr5;
													}
													if (list2[1] != null)
													{
														ptr2[list[1]].Ptr = ptr6;
													}
													if (list2[2] != null)
													{
														ptr2[list[2]].Ptr = ptr7;
													}
													if (list2[3] != null)
													{
														ptr2[list[3]].Ptr = ptr8;
													}
													if (list2[4] != null)
													{
														ptr2[list[4]].Ptr = ptr9;
													}
													if (list2[5] != null)
													{
														ptr2[list[5]].Ptr = ptr10;
													}
													if (list2[6] != null)
													{
														ptr2[list[6]].Ptr = ptr11;
													}
													if (list2[7] != null)
													{
														ptr2[list[7]].Ptr = ptr12;
													}
													num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, num2, ptr);
													text = null;
													text2 = null;
													text3 = null;
													text4 = null;
													text5 = null;
													text6 = null;
													text7 = null;
												}
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					ptr2 = ptr;
					GCHandle[] array = new GCHandle[i];
					for (int k = 0; k < i; k++)
					{
						array[k] = GCHandle.Alloc(list2[k], GCHandleType.Pinned);
						if (list2[k] is string)
						{
							fixed (string text9 = (string)list2[k])
							{
								char* ptr13 = text9;
								if (ptr13 != null)
								{
									ptr13 += RuntimeHelpers.OffsetToStringData / 2;
								}
								ptr2[list[k]].Ptr = ptr13;
							}
						}
						else
						{
							fixed (byte* ptr14 = (byte[])list2[k])
							{
								ptr2[list[k]].Ptr = ptr14;
							}
						}
					}
					num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, num2, ptr);
					for (int l = 0; l < i; l++)
					{
						array[l].Free();
					}
				}
			}
			if (num != 0)
			{
				EventProvider.SetLastError(num);
				return false;
			}
			return true;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000C3A00 File Offset: 0x000C1C00
		[SecurityCritical]
		protected internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* childActivityID, int dataCount, IntPtr data)
		{
			UIntPtr uintPtr = (UIntPtr)0;
			int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, dataCount, (EventProvider.EventData*)((void*)data));
			if (num != 0)
			{
				EventProvider.SetLastError(num);
				return false;
			}
			return true;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000C3A38 File Offset: 0x000C1C38
		[SecurityCritical]
		internal unsafe bool WriteEventRaw(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
		{
			int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, relatedActivityID, dataCount, (EventProvider.EventData*)((void*)data));
			if (num != 0)
			{
				EventProvider.SetLastError(num);
				return false;
			}
			return true;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000C3A69 File Offset: 0x000C1C69
		[SecurityCritical]
		private uint EventRegister(ref Guid providerId, UnsafeNativeMethods.ManifestEtw.EtwEnableCallback enableCallback)
		{
			this.m_providerId = providerId;
			this.m_etwCallback = enableCallback;
			return UnsafeNativeMethods.ManifestEtw.EventRegister(ref providerId, enableCallback, null, ref this.m_regHandle);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000C3A8D File Offset: 0x000C1C8D
		[SecurityCritical]
		private uint EventUnregister(long registrationHandle)
		{
			return UnsafeNativeMethods.ManifestEtw.EventUnregister(registrationHandle);
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000C3A98 File Offset: 0x000C1C98
		private static int bitcount(uint n)
		{
			int num = 0;
			while (n != 0U)
			{
				num += EventProvider.nibblebits[(int)(n & 15U)];
				n >>= 4;
			}
			return num;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000C3AC0 File Offset: 0x000C1CC0
		private static int bitindex(uint n)
		{
			int num = 0;
			while (((ulong)n & (ulong)(1L << (num & 31))) == 0UL)
			{
				num++;
			}
			return num;
		}

		// Token: 0x04001683 RID: 5763
		private static bool m_setInformationMissing;

		// Token: 0x04001684 RID: 5764
		[SecurityCritical]
		private UnsafeNativeMethods.ManifestEtw.EtwEnableCallback m_etwCallback;

		// Token: 0x04001685 RID: 5765
		private long m_regHandle;

		// Token: 0x04001686 RID: 5766
		private byte m_level;

		// Token: 0x04001687 RID: 5767
		private long m_anyKeywordMask;

		// Token: 0x04001688 RID: 5768
		private long m_allKeywordMask;

		// Token: 0x04001689 RID: 5769
		private List<EventProvider.SessionInfo> m_liveSessions;

		// Token: 0x0400168A RID: 5770
		private bool m_enabled;

		// Token: 0x0400168B RID: 5771
		private Guid m_providerId;

		// Token: 0x0400168C RID: 5772
		internal bool m_disposed;

		// Token: 0x0400168D RID: 5773
		[ThreadStatic]
		private static EventProvider.WriteEventErrorCode s_returnCode;

		// Token: 0x0400168E RID: 5774
		private const int s_basicTypeAllocationBufferSize = 16;

		// Token: 0x0400168F RID: 5775
		private const int s_etwMaxNumberArguments = 128;

		// Token: 0x04001690 RID: 5776
		private const int s_etwAPIMaxRefObjCount = 8;

		// Token: 0x04001691 RID: 5777
		private const int s_maxEventDataDescriptors = 128;

		// Token: 0x04001692 RID: 5778
		private const int s_traceEventMaximumSize = 65482;

		// Token: 0x04001693 RID: 5779
		private const int s_traceEventMaximumStringSize = 32724;

		// Token: 0x04001694 RID: 5780
		private static int[] nibblebits = new int[]
		{
			0,
			1,
			1,
			2,
			1,
			2,
			2,
			3,
			1,
			2,
			2,
			3,
			2,
			3,
			3,
			4
		};

		// Token: 0x02000B4E RID: 2894
		public struct EventData
		{
			// Token: 0x04003400 RID: 13312
			internal ulong Ptr;

			// Token: 0x04003401 RID: 13313
			internal uint Size;

			// Token: 0x04003402 RID: 13314
			internal uint Reserved;
		}

		// Token: 0x02000B4F RID: 2895
		public struct SessionInfo
		{
			// Token: 0x06006B3D RID: 27453 RVA: 0x00172AFE File Offset: 0x00170CFE
			internal SessionInfo(int sessionIdBit_, int etwSessionId_)
			{
				this.sessionIdBit = sessionIdBit_;
				this.etwSessionId = etwSessionId_;
			}

			// Token: 0x04003403 RID: 13315
			internal int sessionIdBit;

			// Token: 0x04003404 RID: 13316
			internal int etwSessionId;
		}

		// Token: 0x02000B50 RID: 2896
		public enum WriteEventErrorCode
		{
			// Token: 0x04003406 RID: 13318
			NoError,
			// Token: 0x04003407 RID: 13319
			NoFreeBuffers,
			// Token: 0x04003408 RID: 13320
			EventTooBig,
			// Token: 0x04003409 RID: 13321
			NullInput,
			// Token: 0x0400340A RID: 13322
			TooManyArgs,
			// Token: 0x0400340B RID: 13323
			Other
		}
	}
}
