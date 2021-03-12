using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000084 RID: 132
	internal class ETWTrace
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000AA94 File Offset: 0x00008C94
		private ETWTrace()
		{
			ETWTrace.StartTraceSession();
			AppDomain.CurrentDomain.DomainUnload += delegate(object source, EventArgs args)
			{
				ETWTrace.EndTraceSession();
			};
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002FB RID: 763 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		// (remove) Token: 0x060002FC RID: 764 RVA: 0x0000AAFC File Offset: 0x00008CFC
		public static event Action OnTraceStateChange;

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000AB2F File Offset: 0x00008D2F
		public static bool IsEnabled
		{
			get
			{
				return ETWTrace.ComponentSession.TracingEnabled;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000AB3B File Offset: 0x00008D3B
		public static bool IsCasEnabled
		{
			get
			{
				return ETWTrace.CasPerfSession.TracingEnabled;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000AB47 File Offset: 0x00008D47
		public static bool IsExmonRpcEnabled
		{
			get
			{
				return ETWTrace.ExmonMapiRpcSession.TracingEnabled || ETWTrace.ExmonAdminRpcSession.TracingEnabled || ETWTrace.ExmonTaskSession.TracingEnabled;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000AB6D File Offset: 0x00008D6D
		public static bool IsInternalTraceEnabled
		{
			get
			{
				return ETWTrace.InternalTraceSession.TracingEnabled;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000AB79 File Offset: 0x00008D79
		public static ETWTrace TraceSession
		{
			get
			{
				return ETWTrace.etwTraceSingletonInstance;
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000AB80 File Offset: 0x00008D80
		public static bool ShouldTraceCasStop(Guid serviceProviderRequestId)
		{
			return ETWTrace.CasPerfSession.TracingEnabled && serviceProviderRequestId != Guid.Empty;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public static bool WriteBinary(TraceType traceType, Guid component, int traceTag, byte[] message, out int maxBytes)
		{
			maxBytes = 0;
			int num = message.Length;
			if (traceTag > 255)
			{
				throw new ArgumentException("Maximum allowed tag-value is 255 for binary traces", "traceTag");
			}
			if (num > 8064)
			{
				maxBytes = 8064;
				return false;
			}
			GCHandle? gchandle = null;
			uint num2 = 0U;
			try
			{
				DiagnosticsNativeMethods.BinaryEventTrace binaryEventTrace = new DiagnosticsNativeMethods.BinaryEventTrace(component, (byte)traceTag, message, ref gchandle);
				num2 = DiagnosticsNativeMethods.TraceEvent(ETWTrace.ComponentSession.Session.DangerousGetHandle(), ref binaryEventTrace);
			}
			finally
			{
				if (gchandle != null && gchandle.Value.IsAllocated)
				{
					gchandle.Value.Free();
				}
			}
			return num2 == 0U;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000AC54 File Offset: 0x00008E54
		public static void Write(int lid, TraceType traceType, Guid component, int traceTag, long id, string message)
		{
			ETWTrace.Write(ETWTrace.ComponentSession, lid, traceType, component, traceTag, id, message);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000AC68 File Offset: 0x00008E68
		public static void InternalWrite(int lid, TraceType traceType, Guid component, int traceTag, long id, string message)
		{
			ETWTrace.Write(ETWTrace.InternalTraceSession, lid, traceType, component, traceTag, id, message);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000AC7C File Offset: 0x00008E7C
		public static void Write(EtwSessionInfo sessionInfo, int lid, TraceType traceType, Guid component, int traceTag, long id, string message)
		{
			int num = (message.Length + 1) * 2;
			int num2 = 16;
			int num3 = 8064;
			int val = (num3 - num2) / 2 - 1;
			if (num2 + num < num3)
			{
				uint num4 = DiagnosticsNativeMethods.TraceMessage(sessionInfo.Session.DangerousGetHandle(), 43U, ref component, (ushort)traceType | 32, ref traceTag, 4, message, num, ref id, 8, ref lid, 4, IntPtr.Zero, 0);
				if (num4 != 0U)
				{
					return;
				}
			}
			else
			{
				int num5 = 0;
				int i = message.Length;
				while (i > 0)
				{
					int num6 = Math.Min(i, val);
					uint num7 = DiagnosticsNativeMethods.TraceMessage(sessionInfo.Session.DangerousGetHandle(), 43U, ref component, (ushort)traceType | 32, ref traceTag, 4, message.Substring(num5, num6), (num6 + 1) * 2, ref id, 8, ref lid, 4, IntPtr.Zero, 0);
					i -= num6;
					num5 += num6;
				}
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000AD4C File Offset: 0x00008F4C
		internal static void WriteCas(CasTraceEventType eventType, CasTraceStartStop traceType, Guid serviceProviderRequestID, int bytesIn, int bytesOut, string serverAddress, string userContext, string spOperation, string spOperationData, string clientOperation)
		{
			int num = (serverAddress.Length + 1) * 2;
			int num2 = (userContext.Length + 1) * 2;
			int num3 = (spOperation.Length + 1) * 2;
			int num4 = (spOperationData.Length + 1) * 2;
			int num5 = (clientOperation.Length + 1) * 2;
			int num6 = 12 + 2 * ETWTrace.GuidByteLength + num + num2 + num3 + num4 + num5;
			int num7 = 8064;
			if (num6 < num7)
			{
				int num8 = (int)traceType;
				Guid activityId = ETWTrace.GetActivityId();
				Guid events = ETWTrace.casPerfGuids.Events;
				uint num9 = DiagnosticsNativeMethods.TraceMessage(ETWTrace.CasPerfSession.Session.DangerousGetHandle(), 43U, ref events, (ushort)eventType, ref num8, 4, activityId.ToByteArray(), ETWTrace.GuidByteLength, serviceProviderRequestID.ToByteArray(), ETWTrace.GuidByteLength, ref bytesIn, 4, ref bytesOut, 4, serverAddress, num, userContext, num2, spOperation, num3, spOperationData, num4, clientOperation, num5, IntPtr.Zero, 0);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000AE28 File Offset: 0x00009028
		private static void InvokeOnTraceStateChange()
		{
			Action onTraceStateChange = ETWTrace.OnTraceStateChange;
			if (onTraceStateChange != null)
			{
				onTraceStateChange();
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000AE44 File Offset: 0x00009044
		private static void StartTraceSession()
		{
			IntPtr intPtr;
			ETWTrace.RegisterGuid(ref ETWTrace.componentGuids, out ETWTrace.componentHandle, ETWTrace.ComponentSession.ControlCallback, out intPtr);
			ETWTrace.RegisterGuid(ref ETWTrace.casPerfGuids, out ETWTrace.casPerfHandle, ETWTrace.CasPerfSession.ControlCallback, out intPtr);
			ETWTrace.RegisterGuid(ref ETWTrace.exmonMapiRpcGuids, out ETWTrace.exmonMapiRpcHandle, ETWTrace.ExmonMapiRpcSession.ControlCallback, out ETWTrace.exmonMapiEventHandle);
			ETWTrace.RegisterGuid(ref ETWTrace.exmonAdminRpcGuids, out ETWTrace.exmonAdminRpcHandle, ETWTrace.ExmonAdminRpcSession.ControlCallback, out ETWTrace.exmonAdminEventHandle);
			ETWTrace.RegisterGuid(ref ETWTrace.exmonTaskGuids, out ETWTrace.exmonTaskHandle, ETWTrace.ExmonTaskSession.ControlCallback, out ETWTrace.exmonTaskEventHandle);
			ETWTrace.RegisterGuid(ref ETWTrace.internalTraceGuid, out ETWTrace.internalTraceHandle, ETWTrace.InternalTraceSession.ControlCallback, out intPtr);
			ETWTrace.ComponentSession.OnTraceStateChange += ETWTrace.InvokeOnTraceStateChange;
			ETWTrace.CasPerfSession.OnTraceStateChange += ETWTrace.InvokeOnTraceStateChange;
			ETWTrace.ExmonMapiRpcSession.OnTraceStateChange += ETWTrace.InvokeOnTraceStateChange;
			ETWTrace.ExmonAdminRpcSession.OnTraceStateChange += ETWTrace.InvokeOnTraceStateChange;
			ETWTrace.ExmonTaskSession.OnTraceStateChange += ETWTrace.InvokeOnTraceStateChange;
			ETWTrace.InternalTraceSession.OnTraceStateChange += ETWTrace.InvokeOnTraceStateChange;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000AF80 File Offset: 0x00009180
		private unsafe static void RegisterGuid(ref ETWTrace.EtwTraceGuids traceGuids, out DiagnosticsNativeMethods.CriticalTraceRegistrationHandle regHandle, DiagnosticsNativeMethods.ControlCallback callback, out IntPtr eventHandle)
		{
			Guid events = traceGuids.Events;
			DiagnosticsNativeMethods.TraceGuidRegistration traceGuidRegistration;
			traceGuidRegistration.guid = &events;
			traceGuidRegistration.handle = IntPtr.Zero;
			regHandle = DiagnosticsNativeMethods.CriticalTraceRegistrationHandle.RegisterTrace(traceGuids.Provider, ref traceGuidRegistration, callback);
			eventHandle = traceGuidRegistration.handle;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000AFC7 File Offset: 0x000091C7
		private static void EndTraceSession()
		{
			ETWTrace.componentHandle.Dispose();
			ETWTrace.casPerfHandle.Dispose();
			ETWTrace.exmonMapiRpcHandle.Dispose();
			ETWTrace.exmonAdminRpcHandle.Dispose();
			ETWTrace.exmonTaskHandle.Dispose();
			ETWTrace.internalTraceHandle.Dispose();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B008 File Offset: 0x00009208
		private static DiagnosticsNativeMethods.EventInstanceInfo CreateInstanceId(IntPtr eventHandle)
		{
			DiagnosticsNativeMethods.EventInstanceInfo result = default(DiagnosticsNativeMethods.EventInstanceInfo);
			DiagnosticsNativeMethods.CreateTraceInstanceId(eventHandle, ref result);
			return result;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B028 File Offset: 0x00009228
		internal static DiagnosticsNativeMethods.EventInstanceInfo CreateExmonMapiRpcInstanceId()
		{
			return ETWTrace.CreateInstanceId(ETWTrace.exmonMapiEventHandle);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B044 File Offset: 0x00009244
		internal static DiagnosticsNativeMethods.EventInstanceInfo CreateExmonAdminRpcInstanceId()
		{
			return ETWTrace.CreateInstanceId(ETWTrace.exmonAdminEventHandle);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B060 File Offset: 0x00009260
		internal static DiagnosticsNativeMethods.EventInstanceInfo CreateExmonTaskInstanceId()
		{
			return ETWTrace.CreateInstanceId(ETWTrace.exmonTaskEventHandle);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B07C File Offset: 0x0000927C
		public static uint ExmonMapiRpcTraceEventInstance(byte[] buffer, ref DiagnosticsNativeMethods.EventInstanceInfo instanceInfo, ref DiagnosticsNativeMethods.EventInstanceInfo parentInstanceInfo)
		{
			DiagnosticsNativeMethods.CriticalTraceHandle session = ETWTrace.ExmonMapiRpcSession.Session;
			if (session != null)
			{
				return DiagnosticsNativeMethods.TraceEventInstance(session.DangerousGetHandle(), buffer, ref instanceInfo, ref parentInstanceInfo);
			}
			return 87U;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B0A8 File Offset: 0x000092A8
		public static uint ExmonAdminRpcTraceEventInstance(byte[] buffer, ref DiagnosticsNativeMethods.EventInstanceInfo instanceInfo, ref DiagnosticsNativeMethods.EventInstanceInfo parentInstanceInfo)
		{
			DiagnosticsNativeMethods.CriticalTraceHandle session = ETWTrace.ExmonAdminRpcSession.Session;
			if (session != null)
			{
				return DiagnosticsNativeMethods.TraceEventInstance(session.DangerousGetHandle(), buffer, ref instanceInfo, ref parentInstanceInfo);
			}
			return 87U;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B0D4 File Offset: 0x000092D4
		public static uint ExmonTaskTraceEventInstance(byte[] buffer, ref DiagnosticsNativeMethods.EventInstanceInfo instanceInfo, ref DiagnosticsNativeMethods.EventInstanceInfo parentInstanceInfo)
		{
			DiagnosticsNativeMethods.CriticalTraceHandle session = ETWTrace.ExmonTaskSession.Session;
			if (session != null)
			{
				return DiagnosticsNativeMethods.TraceEventInstance(session.DangerousGetHandle(), buffer, ref instanceInfo, ref parentInstanceInfo);
			}
			return 87U;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B100 File Offset: 0x00009300
		private static Guid GetActivityId()
		{
			return Trace.CorrelationManager.ActivityId;
		}

		// Token: 0x040002B8 RID: 696
		internal const ushort EtlRecordVersion = 32;

		// Token: 0x040002B9 RID: 697
		private const int MaxTraceMessageSize = 8064;

		// Token: 0x040002BA RID: 698
		private const uint TraceFlags = 43U;

		// Token: 0x040002BB RID: 699
		internal static readonly Guid ExchangeProviderGuid = new Guid("{79BB49E6-2A2C-46e4-9167-FA122525D540}");

		// Token: 0x040002BC RID: 700
		internal static readonly Guid InternalProviderGuid = new Guid("{096D3B9E-AA4F-4204-B1DD-7DC258498AB6}");

		// Token: 0x040002BD RID: 701
		private static readonly object LockObject = new object();

		// Token: 0x040002BE RID: 702
		private static readonly int GuidByteLength = Guid.Empty.ToByteArray().Length;

		// Token: 0x040002BF RID: 703
		private static readonly EtwSessionInfo ComponentSession = new EtwSessionInfo();

		// Token: 0x040002C0 RID: 704
		private static readonly EtwSessionInfo CasPerfSession = new EtwSessionInfo();

		// Token: 0x040002C1 RID: 705
		private static readonly EtwSessionInfo ExmonMapiRpcSession = new EtwSessionInfo();

		// Token: 0x040002C2 RID: 706
		private static readonly EtwSessionInfo ExmonAdminRpcSession = new EtwSessionInfo();

		// Token: 0x040002C3 RID: 707
		private static readonly EtwSessionInfo ExmonTaskSession = new EtwSessionInfo();

		// Token: 0x040002C4 RID: 708
		private static readonly EtwSessionInfo InternalTraceSession = new EtwSessionInfo();

		// Token: 0x040002C5 RID: 709
		private static ETWTrace.EtwTraceGuids componentGuids = new ETWTrace.EtwTraceGuids(ETWTrace.ExchangeProviderGuid, new Guid("{FBA968C6-3276-4135-B16B-9D90D7151E61}"));

		// Token: 0x040002C6 RID: 710
		private static ETWTrace.EtwTraceGuids casPerfGuids = new ETWTrace.EtwTraceGuids(new Guid("{67C6E8E5-B62B-4f47-A04D-ECB487D00046}"), new Guid("{9BB29706-EC64-4345-8208-67B0C9E22283}"));

		// Token: 0x040002C7 RID: 711
		private static ETWTrace.EtwTraceGuids exmonMapiRpcGuids = new ETWTrace.EtwTraceGuids(new Guid("{2EACCEDF-8648-453e-9250-27F0069F71D2}"), new Guid("{31F5A811-6EA0-4321-93D9-CDB9A70D50A1}"));

		// Token: 0x040002C8 RID: 712
		private static ETWTrace.EtwTraceGuids exmonAdminRpcGuids = new ETWTrace.EtwTraceGuids(new Guid("{2EACCEDF-8648-453e-9250-27F0069F71D2}"), new Guid("{42EC5AC0-3D00-4DBF-A45C-EC569A40C512}"));

		// Token: 0x040002C9 RID: 713
		private static ETWTrace.EtwTraceGuids exmonTaskGuids = new ETWTrace.EtwTraceGuids(new Guid("{2EACCEDF-8648-453e-9250-27F0069F71D2}"), new Guid("{D77063BD-E0BE-488B-AD3A-B27B7C110FEC}"));

		// Token: 0x040002CA RID: 714
		private static ETWTrace.EtwTraceGuids internalTraceGuid = new ETWTrace.EtwTraceGuids(ETWTrace.InternalProviderGuid, new Guid("{9E0B578C-4634-4212-82DF-063DF01E3728}"));

		// Token: 0x040002CB RID: 715
		private static DiagnosticsNativeMethods.CriticalTraceRegistrationHandle componentHandle;

		// Token: 0x040002CC RID: 716
		private static DiagnosticsNativeMethods.CriticalTraceRegistrationHandle casPerfHandle;

		// Token: 0x040002CD RID: 717
		private static DiagnosticsNativeMethods.CriticalTraceRegistrationHandle exmonMapiRpcHandle;

		// Token: 0x040002CE RID: 718
		private static DiagnosticsNativeMethods.CriticalTraceRegistrationHandle exmonAdminRpcHandle;

		// Token: 0x040002CF RID: 719
		private static DiagnosticsNativeMethods.CriticalTraceRegistrationHandle exmonTaskHandle;

		// Token: 0x040002D0 RID: 720
		private static DiagnosticsNativeMethods.CriticalTraceRegistrationHandle internalTraceHandle;

		// Token: 0x040002D1 RID: 721
		private static IntPtr exmonMapiEventHandle;

		// Token: 0x040002D2 RID: 722
		private static IntPtr exmonAdminEventHandle;

		// Token: 0x040002D3 RID: 723
		private static IntPtr exmonTaskEventHandle;

		// Token: 0x040002D4 RID: 724
		private static ETWTrace etwTraceSingletonInstance = new ETWTrace();

		// Token: 0x02000085 RID: 133
		private struct EtwTraceGuids
		{
			// Token: 0x06000316 RID: 790 RVA: 0x0000B245 File Offset: 0x00009445
			public EtwTraceGuids(Guid provider, Guid eventClass)
			{
				this.Provider = provider;
				this.Events = eventClass;
			}

			// Token: 0x040002D7 RID: 727
			public readonly Guid Provider;

			// Token: 0x040002D8 RID: 728
			public readonly Guid Events;
		}
	}
}
