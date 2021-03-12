using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002F4 RID: 756
	[InstallerType(typeof(ServiceProcessInstaller))]
	internal abstract class ReplayServiceBase : Component
	{
		// Token: 0x06001E8B RID: 7819 RVA: 0x0008A9F4 File Offset: 0x00088BF4
		public ReplayServiceBase()
		{
			this.acceptedCommands = 1;
			this.AutoLog = true;
			this.ServiceName = "";
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0008AA40 File Offset: 0x00088C40
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x0008AA48 File Offset: 0x00088C48
		[DefaultValue(true)]
		public bool AutoLog
		{
			get
			{
				return this.autoLog;
			}
			set
			{
				this.autoLog = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0008AA51 File Offset: 0x00088C51
		// (set) Token: 0x06001E8F RID: 7823 RVA: 0x0008AA5E File Offset: 0x00088C5E
		[ComVisible(false)]
		public int ExitCode
		{
			get
			{
				return this.status.win32ExitCode;
			}
			set
			{
				this.status.win32ExitCode = value;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0008AA6C File Offset: 0x00088C6C
		// (set) Token: 0x06001E91 RID: 7825 RVA: 0x0008AA7D File Offset: 0x00088C7D
		[DefaultValue(false)]
		public bool CanHandlePowerEvent
		{
			get
			{
				return (this.acceptedCommands & 64) != 0;
			}
			set
			{
				if (this.commandPropsFrozen)
				{
					throw new InvalidOperationException(ReplayStrings.CannotChangeProperties);
				}
				if (value)
				{
					this.acceptedCommands |= 64;
					return;
				}
				this.acceptedCommands &= -65;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0008AAB9 File Offset: 0x00088CB9
		// (set) Token: 0x06001E93 RID: 7827 RVA: 0x0008AAD0 File Offset: 0x00088CD0
		[DefaultValue(false)]
		[ComVisible(false)]
		public bool CanHandleSessionChangeEvent
		{
			get
			{
				return (this.acceptedCommands & 128) != 0;
			}
			set
			{
				if (this.commandPropsFrozen)
				{
					throw new InvalidOperationException(ReplayStrings.CannotChangeProperties);
				}
				if (value)
				{
					this.acceptedCommands |= 128;
					return;
				}
				this.acceptedCommands &= -129;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0008AB1D File Offset: 0x00088D1D
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x0008AB2D File Offset: 0x00088D2D
		[DefaultValue(false)]
		public bool CanPauseAndContinue
		{
			get
			{
				return (this.acceptedCommands & 2) != 0;
			}
			set
			{
				if (this.commandPropsFrozen)
				{
					throw new InvalidOperationException(ReplayStrings.CannotChangeProperties);
				}
				if (value)
				{
					this.acceptedCommands |= 2;
					return;
				}
				this.acceptedCommands &= -3;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0008AB68 File Offset: 0x00088D68
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0008AB78 File Offset: 0x00088D78
		[DefaultValue(false)]
		public bool CanShutdown
		{
			get
			{
				return (this.acceptedCommands & 4) != 0;
			}
			set
			{
				if (this.commandPropsFrozen)
				{
					throw new InvalidOperationException(ReplayStrings.CannotChangeProperties);
				}
				if (value)
				{
					this.acceptedCommands |= 4;
					return;
				}
				this.acceptedCommands &= -5;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x0008ABB3 File Offset: 0x00088DB3
		// (set) Token: 0x06001E99 RID: 7833 RVA: 0x0008ABC3 File Offset: 0x00088DC3
		[DefaultValue(true)]
		public bool CanStop
		{
			get
			{
				return (this.acceptedCommands & 1) != 0;
			}
			set
			{
				if (this.commandPropsFrozen)
				{
					throw new InvalidOperationException(ReplayStrings.CannotChangeProperties);
				}
				if (value)
				{
					this.acceptedCommands |= 1;
					return;
				}
				this.acceptedCommands &= -2;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0008ABFE File Offset: 0x00088DFE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual EventLog EventLog
		{
			get
			{
				if (this.eventLog == null)
				{
					this.eventLog = new EventLog();
					this.eventLog.Source = this.ServiceName;
					this.eventLog.Log = "Application";
				}
				return this.eventLog;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0008AC3A File Offset: 0x00088E3A
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x0008AC44 File Offset: 0x00088E44
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design")]
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
			set
			{
				if (this.nameFrozen)
				{
					throw new InvalidOperationException(ReplayStrings.CannotChangeName);
				}
				if (value != "" && !ReplayServiceBase.ValidServiceName(value))
				{
					throw new ArgumentException(ReplayStrings.ServiceName(value, 80.ToString(CultureInfo.CurrentCulture)));
				}
				this.serviceName = value;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x0008ACA5 File Offset: 0x00088EA5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected IntPtr ServiceHandle
		{
			get
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				return this.statusHandle;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0008ACB8 File Offset: 0x00088EB8
		protected virtual TimeSpan StartTimeout
		{
			get
			{
				return this.DefaultTimeout;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x0008ACC0 File Offset: 0x00088EC0
		protected virtual TimeSpan StopTimeout
		{
			get
			{
				return this.DefaultTimeout;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06001EA0 RID: 7840 RVA: 0x0008ACC8 File Offset: 0x00088EC8
		protected virtual TimeSpan PauseTimeout
		{
			get
			{
				return this.DefaultTimeout;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x0008ACD0 File Offset: 0x00088ED0
		protected virtual TimeSpan ContinueTimeout
		{
			get
			{
				return this.DefaultTimeout;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0008ACD8 File Offset: 0x00088ED8
		protected virtual TimeSpan CustomCommandTimeout
		{
			get
			{
				return this.DefaultTimeout;
			}
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0008ACE0 File Offset: 0x00088EE0
		public static void Run(ReplayServiceBase service)
		{
			if (service == null)
			{
				throw new ArgumentException(ReplayStrings.NoServices);
			}
			ReplayServiceBase.Run(new ReplayServiceBase[]
			{
				service
			});
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x0008AD14 File Offset: 0x00088F14
		public static void Run(ReplayServiceBase[] services)
		{
			if (services == null || services.Length == 0)
			{
				throw new ArgumentException(ReplayStrings.NoServices);
			}
			IntPtr intPtr = Marshal.AllocHGlobal((IntPtr)((services.Length + 1) * Marshal.SizeOf(typeof(NativeMethods.SERVICE_TABLE_ENTRY))));
			NativeMethods.SERVICE_TABLE_ENTRY[] array = new NativeMethods.SERVICE_TABLE_ENTRY[services.Length];
			bool multipleServices = services.Length > 1;
			IntPtr ptr = (IntPtr)0;
			for (int i = 0; i < services.Length; i++)
			{
				services[i].Initialize(multipleServices);
				array[i] = services[i].GetEntry();
				ptr = (IntPtr)((long)intPtr + (long)(Marshal.SizeOf(typeof(NativeMethods.SERVICE_TABLE_ENTRY)) * i));
				Marshal.StructureToPtr(array[i], ptr, true);
			}
			NativeMethods.SERVICE_TABLE_ENTRY service_TABLE_ENTRY = new NativeMethods.SERVICE_TABLE_ENTRY();
			service_TABLE_ENTRY.callback = null;
			service_TABLE_ENTRY.name = (IntPtr)0;
			ptr = (IntPtr)((long)intPtr + (long)(Marshal.SizeOf(typeof(NativeMethods.SERVICE_TABLE_ENTRY)) * services.Length));
			Marshal.StructureToPtr(service_TABLE_ENTRY, ptr, true);
			bool flag = NativeMethods.StartServiceCtrlDispatcher(intPtr);
			string text = "";
			if (!flag)
			{
				text = new Win32Exception().Message;
				string text2 = ReplayStrings.CantStartFromCommandLine;
				if (Environment.UserInteractive)
				{
					string title = ReplayStrings.CantStartFromCommandLineTitle;
					ReplayServiceBase.LateBoundMessageBoxShow(text2, title);
				}
				else
				{
					Console.WriteLine(text2);
				}
			}
			foreach (ReplayServiceBase replayServiceBase in services)
			{
				replayServiceBase.Dispose();
				if (!flag)
				{
					ReplayEventLogConstants.Tuple_StartFailed.LogEvent(null, new object[]
					{
						text
					});
				}
			}
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0008AE9D File Offset: 0x0008909D
		public static void RunAsConsole(ReplayServiceBase service)
		{
			Console.WriteLine("Starting...");
			service.OnStartInternal(null);
			Console.WriteLine("Started. Type ENTER to stop.");
			Console.ReadLine();
			Console.WriteLine("Stopping...");
			service.OnStopInternal();
			Console.WriteLine("Stopped");
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0008AEDC File Offset: 0x000890DC
		[ComVisible(false)]
		public unsafe void RequestAdditionalTime(int milliseconds)
		{
			fixed (NativeMethods.SERVICE_STATUS* ptr = &this.status)
			{
				if (this.status.currentState != 5 && this.status.currentState != 2 && this.status.currentState != 3 && this.status.currentState != 6)
				{
					throw new InvalidOperationException(ReplayStrings.NotInPendingState);
				}
				this.status.waitHint = milliseconds;
				this.status.checkPoint = this.status.checkPoint + 1;
				NativeMethods.SetServiceStatus(this.statusHandle, ptr);
			}
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0008AF68 File Offset: 0x00089168
		public void Stop()
		{
			this.DeferredStop();
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x0008AF70 File Offset: 0x00089170
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ComVisible(false)]
		public unsafe void ServiceMainCallback(int argCount, IntPtr argPointer)
		{
			fixed (NativeMethods.SERVICE_STATUS* ptr = &this.status)
			{
				string[] array = null;
				if (argCount > 0)
				{
					char** ptr2 = (char**)argPointer.ToPointer();
					array = new string[argCount - 1];
					for (int i = 0; i < array.Length; i++)
					{
						ptr2 += sizeof(char*) / sizeof(char*);
						array[i] = Marshal.PtrToStringUni((IntPtr)(*(IntPtr*)ptr2));
					}
				}
				if (!this.initialized)
				{
					this.isServiceHosted = true;
					this.Initialize(true);
				}
				if (Environment.OSVersion.Version.Major >= 5)
				{
					this.statusHandle = NativeMethods.RegisterServiceCtrlHandlerEx(this.ServiceName, this.commandCallbackEx, (IntPtr)0);
				}
				else
				{
					this.statusHandle = NativeMethods.RegisterServiceCtrlHandler(this.ServiceName, this.commandCallback);
				}
				this.nameFrozen = true;
				if (this.statusHandle == (IntPtr)0)
				{
					string message = new Win32Exception().Message;
					ReplayEventLogConstants.Tuple_StartFailed.LogEvent(null, new object[]
					{
						message
					});
				}
				this.status.controlsAccepted = this.acceptedCommands;
				this.commandPropsFrozen = true;
				if ((this.status.controlsAccepted & 1) != 0)
				{
					this.status.controlsAccepted = (this.status.controlsAccepted | 256);
				}
				if (Environment.OSVersion.Version.Major < 5)
				{
					this.status.controlsAccepted = (this.status.controlsAccepted & -65);
				}
				this.status.currentState = 2;
				if (NativeMethods.SetServiceStatus(this.statusHandle, ptr))
				{
					this.startCompletedSignal = new ManualResetEvent(false);
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.ServiceQueuedMainCallback), array);
					this.startCompletedSignal.WaitOne();
					if (!NativeMethods.SetServiceStatus(this.statusHandle, ptr))
					{
						ReplayEventLogConstants.Tuple_StartFailed.LogEvent(null, new object[]
						{
							new Win32Exception().Message
						});
						this.status.currentState = 1;
						NativeMethods.SetServiceStatus(this.statusHandle, ptr);
					}
					ptr = null;
				}
			}
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0008B168 File Offset: 0x00089368
		public void ExRequestAdditionalTime(int milliseconds)
		{
			if (!Environment.UserInteractive)
			{
				this.RequestAdditionalTime(milliseconds);
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0008B178 File Offset: 0x00089378
		internal static bool ValidServiceName(string serviceName)
		{
			if (serviceName == null)
			{
				return false;
			}
			if (serviceName.Length > 80 || serviceName.Length == 0)
			{
				return false;
			}
			foreach (char c in serviceName.ToCharArray())
			{
				if (c == '\\' || c == '/')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0008B1C8 File Offset: 0x000893C8
		protected override void Dispose(bool disposing)
		{
			if (this.handleName != (IntPtr)0)
			{
				Marshal.FreeHGlobal(this.handleName);
				this.handleName = (IntPtr)0;
			}
			this.nameFrozen = false;
			this.commandPropsFrozen = false;
			this.disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0008B21B File Offset: 0x0008941B
		protected virtual bool OnPowerEvent(PowerBroadcastStatus powerStatus)
		{
			return true;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0008B21E File Offset: 0x0008941E
		protected virtual void OnSessionChange(SessionChangeDescription changeDescription)
		{
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0008B220 File Offset: 0x00089420
		protected virtual void OnShutdown()
		{
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0008B222 File Offset: 0x00089422
		protected virtual void OnPreShutdown()
		{
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x0008B258 File Offset: 0x00089458
		protected void OnStart(string[] args)
		{
			this.SendWatsonReportOnTimeout("OnStart", this.StartTimeout, new TimerCallback(this.OnStartTimeoutHandler), delegate
			{
				Dependencies.Watson.SendReportOnUnhandledException(delegate
				{
					this.OnStartInternal(args);
				});
			});
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0008B2C2 File Offset: 0x000894C2
		protected void OnStop()
		{
			this.SendWatsonReportOnTimeout("OnStop", this.StopTimeout, new TimerCallback(this.OnStopTimeoutHandler), delegate
			{
				Dependencies.Watson.SendReportOnUnhandledException(delegate
				{
					this.OnStopInternal();
				});
			});
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0008B30D File Offset: 0x0008950D
		protected void OnPause()
		{
			this.SendWatsonReportOnTimeout("OnPause", this.PauseTimeout, new TimerCallback(this.OnStartTimeoutHandler), delegate
			{
				Dependencies.Watson.SendReportOnUnhandledException(delegate
				{
					this.OnPauseInternal();
				});
			});
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x0008B358 File Offset: 0x00089558
		protected void OnContinue()
		{
			this.SendWatsonReportOnTimeout("OnContinue", this.ContinueTimeout, new TimerCallback(this.OnStartTimeoutHandler), delegate
			{
				Dependencies.Watson.SendReportOnUnhandledException(delegate
				{
					this.OnContinueInternal();
				});
			});
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x0008B3B8 File Offset: 0x000895B8
		protected void OnCustomCommand(int command)
		{
			this.SendWatsonReportOnTimeout("OnCustomCommand", this.CustomCommandTimeout, new TimerCallback(this.OnStartTimeoutHandler), delegate
			{
				Dependencies.Watson.SendReportOnUnhandledException(delegate
				{
					this.OnCustomCommandInternal(command);
				});
			});
		}

		// Token: 0x06001EB5 RID: 7861
		protected abstract void OnStartInternal(string[] args);

		// Token: 0x06001EB6 RID: 7862
		protected abstract void OnStopInternal();

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0008B402 File Offset: 0x00089602
		protected virtual void OnPauseInternal()
		{
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x0008B404 File Offset: 0x00089604
		protected virtual void OnContinueInternal()
		{
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0008B406 File Offset: 0x00089606
		protected virtual void OnCommandTimeout()
		{
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0008B408 File Offset: 0x00089608
		protected virtual void OnCustomCommandInternal(int command)
		{
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0008B40C File Offset: 0x0008960C
		private static void LateBoundMessageBoxShow(string message, string title)
		{
			int value = 1572864;
			Type type = Type.GetType("System.Windows.Forms.MessageBox, System.Windows.Forms");
			Type type2 = Type.GetType("System.Windows.Forms.MessageBoxButtons, System.Windows.Forms");
			Type type3 = Type.GetType("System.Windows.Forms.MessageBoxIcon, System.Windows.Forms");
			Type type4 = Type.GetType("System.Windows.Forms.MessageBoxDefaultButton, System.Windows.Forms");
			Type type5 = Type.GetType("System.Windows.Forms.MessageBoxOptions, System.Windows.Forms");
			type.InvokeMember("Show", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
			{
				message,
				title,
				Enum.ToObject(type2, 0),
				Enum.ToObject(type3, 0),
				Enum.ToObject(type4, 0),
				Enum.ToObject(type5, value)
			}, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x0008B4B4 File Offset: 0x000896B4
		private unsafe void DeferredContinue()
		{
			fixed (NativeMethods.SERVICE_STATUS* ptr = &this.status)
			{
				try
				{
					this.OnContinue();
					ReplayEventLogConstants.Tuple_ContinueSuccessful.LogEvent(null, new object[0]);
					this.status.currentState = 4;
				}
				catch (Exception ex)
				{
					this.status.currentState = 7;
					ReplayEventLogConstants.Tuple_ContinueFailed.LogEvent(string.Empty, new object[]
					{
						ex.ToString()
					});
					throw;
				}
				finally
				{
					NativeMethods.SetServiceStatus(this.statusHandle, ptr);
				}
			}
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x0008B54C File Offset: 0x0008974C
		private void DeferredCustomCommand(int command)
		{
			try
			{
				this.OnCustomCommand(command);
				ReplayEventLogConstants.Tuple_CommandSuccessful.LogEvent(null, new object[0]);
			}
			catch (Exception ex)
			{
				ReplayEventLogConstants.Tuple_CommandFailed.LogEvent(string.Empty, new object[]
				{
					ex.ToString()
				});
				throw;
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0008B5A8 File Offset: 0x000897A8
		private unsafe void DeferredPause()
		{
			fixed (NativeMethods.SERVICE_STATUS* ptr = &this.status)
			{
				try
				{
					this.OnPause();
					ReplayEventLogConstants.Tuple_PauseSuccessful.LogEvent(null, new object[0]);
					this.status.currentState = 7;
				}
				catch (Exception ex)
				{
					this.status.currentState = 4;
					ReplayEventLogConstants.Tuple_PauseFailed.LogEvent(string.Empty, new object[]
					{
						ex.ToString()
					});
					throw;
				}
				finally
				{
					NativeMethods.SetServiceStatus(this.statusHandle, ptr);
				}
			}
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0008B640 File Offset: 0x00089840
		private void DeferredPowerEvent(int eventType, IntPtr eventData)
		{
			try
			{
				this.OnPowerEvent((PowerBroadcastStatus)eventType);
				ReplayEventLogConstants.Tuple_PowerEventOK.LogEvent(null, new object[0]);
			}
			catch (Exception ex)
			{
				ReplayEventLogConstants.Tuple_PowerEventFailed.LogEvent(string.Empty, new object[]
				{
					ex.ToString()
				});
				throw;
			}
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x0008B6A0 File Offset: 0x000898A0
		private void DeferredSessionChange(int eventType, IntPtr eventData)
		{
			try
			{
				NativeMethods.WTSSESSION_NOTIFICATION structure = new NativeMethods.WTSSESSION_NOTIFICATION();
				Marshal.PtrToStructure(eventData, structure);
			}
			catch (Exception ex)
			{
				ReplayEventLogConstants.Tuple_SessionChangeFailed.LogEvent(ex.ToString(), new object[0]);
				throw;
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0008B6E8 File Offset: 0x000898E8
		private unsafe void DeferredStop()
		{
			fixed (NativeMethods.SERVICE_STATUS* ptr = &this.status)
			{
				int currentState = this.status.currentState;
				this.status.checkPoint = 0;
				this.status.waitHint = 0;
				this.status.currentState = 3;
				NativeMethods.SetServiceStatus(this.statusHandle, ptr);
				try
				{
					this.OnStop();
					this.status.currentState = 1;
					NativeMethods.SetServiceStatus(this.statusHandle, ptr);
				}
				catch (Exception ex)
				{
					this.status.currentState = currentState;
					NativeMethods.SetServiceStatus(this.statusHandle, ptr);
					ReplayEventLogConstants.Tuple_StopFailed.LogEvent(string.Empty, new object[]
					{
						ex.ToString()
					});
					throw;
				}
				if (this.isServiceHosted)
				{
					try
					{
						AppDomain.Unload(AppDomain.CurrentDomain);
					}
					catch (CannotUnloadAppDomainException ex2)
					{
						ReplayEventLogConstants.Tuple_FailedToUnloadAppDomain.LogEvent(string.Empty, new object[]
						{
							AppDomain.CurrentDomain.FriendlyName,
							ex2.Message
						});
					}
				}
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0008B804 File Offset: 0x00089A04
		private unsafe void DeferredShutdown()
		{
			fixed (NativeMethods.SERVICE_STATUS* ptr = &this.status)
			{
				int currentState = this.status.currentState;
				this.status.checkPoint = 0;
				this.status.waitHint = 0;
				this.status.currentState = 3;
				NativeMethods.SetServiceStatus(this.statusHandle, ptr);
				try
				{
					this.OnShutdown();
					ReplayEventLogConstants.Tuple_ShutdownOK.LogEvent(null, new object[0]);
					this.status.currentState = 1;
					NativeMethods.SetServiceStatus(this.statusHandle, ptr);
				}
				catch (Exception ex)
				{
					this.status.currentState = currentState;
					NativeMethods.SetServiceStatus(this.statusHandle, ptr);
					ReplayEventLogConstants.Tuple_ShutdownFailed.LogEvent(string.Empty, new object[]
					{
						ex.ToString()
					});
					throw;
				}
			}
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0008B8DC File Offset: 0x00089ADC
		private unsafe void DeferredPreShutdown()
		{
			fixed (NativeMethods.SERVICE_STATUS* ptr = &this.status)
			{
				this.status.checkPoint = 0;
				this.status.waitHint = 0;
				this.status.currentState = 3;
				NativeMethods.SetServiceStatus(this.statusHandle, ptr);
				try
				{
					this.OnPreShutdown();
					this.status.currentState = 1;
					NativeMethods.SetServiceStatus(this.statusHandle, ptr);
				}
				catch (Exception ex)
				{
					ReplayEventLogConstants.Tuple_PreShutdownFailed.LogEvent(string.Empty, new object[]
					{
						ex.ToString()
					});
					throw;
				}
			}
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0008B97C File Offset: 0x00089B7C
		private void Initialize(bool multipleServices)
		{
			if (!this.initialized)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (!multipleServices)
				{
					this.status.serviceType = 16;
				}
				else
				{
					this.status.serviceType = 32;
				}
				this.status.currentState = 2;
				this.status.controlsAccepted = 0;
				this.status.win32ExitCode = 0;
				this.status.serviceSpecificExitCode = 0;
				this.status.checkPoint = 0;
				this.status.waitHint = 0;
				this.mainCallback = new NativeMethods.ServiceMainCallback(this.ServiceMainCallback);
				this.commandCallback = new NativeMethods.ServiceControlCallback(this.ServiceCommandCallback);
				this.commandCallbackEx = new NativeMethods.ServiceControlCallbackEx(this.ServiceCommandCallbackEx);
				this.handleName = Marshal.StringToHGlobalUni(this.ServiceName);
				this.initialized = true;
			}
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x0008BA64 File Offset: 0x00089C64
		private NativeMethods.SERVICE_TABLE_ENTRY GetEntry()
		{
			NativeMethods.SERVICE_TABLE_ENTRY service_TABLE_ENTRY = new NativeMethods.SERVICE_TABLE_ENTRY();
			this.nameFrozen = true;
			service_TABLE_ENTRY.callback = this.mainCallback;
			service_TABLE_ENTRY.name = this.handleName;
			return service_TABLE_ENTRY;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0008BA98 File Offset: 0x00089C98
		private int ServiceCommandCallbackEx(int command, int eventType, IntPtr eventData, IntPtr eventContext)
		{
			int result = 0;
			switch (command)
			{
			case 13:
			{
				ReplayServiceBase.DeferredHandlerDelegateAdvanced deferredHandlerDelegateAdvanced = new ReplayServiceBase.DeferredHandlerDelegateAdvanced(this.DeferredPowerEvent);
				deferredHandlerDelegateAdvanced.BeginInvoke(eventType, eventData, null, null);
				break;
			}
			case 14:
			{
				ReplayServiceBase.DeferredHandlerDelegateAdvanced deferredHandlerDelegateAdvanced2 = new ReplayServiceBase.DeferredHandlerDelegateAdvanced(this.DeferredSessionChange);
				deferredHandlerDelegateAdvanced2.BeginInvoke(eventType, eventData, null, null);
				break;
			}
			default:
				this.ServiceCommandCallback(command);
				break;
			}
			return result;
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x0008BAF8 File Offset: 0x00089CF8
		private unsafe void ServiceCommandCallback(int command)
		{
			fixed (NativeMethods.SERVICE_STATUS* ptr = &this.status)
			{
				if (command == 4)
				{
					NativeMethods.SetServiceStatus(this.statusHandle, ptr);
				}
				else if (this.status.currentState != 5 && this.status.currentState != 2 && this.status.currentState != 3 && this.status.currentState != 6)
				{
					switch (command)
					{
					case 1:
					{
						int currentState = this.status.currentState;
						if (this.status.currentState == 7 || this.status.currentState == 4)
						{
							this.status.currentState = 3;
							NativeMethods.SetServiceStatus(this.statusHandle, ptr);
							this.status.currentState = currentState;
							ReplayServiceBase.DeferredHandlerDelegate deferredHandlerDelegate = new ReplayServiceBase.DeferredHandlerDelegate(this.DeferredStop);
							deferredHandlerDelegate.BeginInvoke(null, null);
							goto IL_1D4;
						}
						goto IL_1D4;
					}
					case 2:
						if (this.status.currentState == 4)
						{
							this.status.currentState = 6;
							NativeMethods.SetServiceStatus(this.statusHandle, ptr);
							ReplayServiceBase.DeferredHandlerDelegate deferredHandlerDelegate2 = new ReplayServiceBase.DeferredHandlerDelegate(this.DeferredPause);
							deferredHandlerDelegate2.BeginInvoke(null, null);
							goto IL_1D4;
						}
						goto IL_1D4;
					case 3:
						if (this.status.currentState == 7)
						{
							this.status.currentState = 5;
							NativeMethods.SetServiceStatus(this.statusHandle, ptr);
							ReplayServiceBase.DeferredHandlerDelegate deferredHandlerDelegate3 = new ReplayServiceBase.DeferredHandlerDelegate(this.DeferredContinue);
							deferredHandlerDelegate3.BeginInvoke(null, null);
							goto IL_1D4;
						}
						goto IL_1D4;
					case 4:
						break;
					case 5:
					{
						ReplayServiceBase.DeferredHandlerDelegate deferredHandlerDelegate4 = new ReplayServiceBase.DeferredHandlerDelegate(this.DeferredShutdown);
						deferredHandlerDelegate4.BeginInvoke(null, null);
						goto IL_1D4;
					}
					default:
						if (command == 15)
						{
							ReplayServiceBase.DeferredHandlerDelegate deferredHandlerDelegate5 = new ReplayServiceBase.DeferredHandlerDelegate(this.DeferredPreShutdown);
							deferredHandlerDelegate5.BeginInvoke(null, null);
							goto IL_1D4;
						}
						break;
					}
					ReplayServiceBase.DeferredHandlerDelegateCommand deferredHandlerDelegateCommand = new ReplayServiceBase.DeferredHandlerDelegateCommand(this.DeferredCustomCommand);
					deferredHandlerDelegateCommand.BeginInvoke(command, null, null);
				}
				IL_1D4:;
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x0008BCDC File Offset: 0x00089EDC
		private void ServiceQueuedMainCallback(object state)
		{
			string[] args = (string[])state;
			try
			{
				this.OnStart(args);
				this.status.checkPoint = 0;
				this.status.waitHint = 0;
				this.status.currentState = 4;
			}
			catch (Exception ex)
			{
				ReplayEventLogConstants.Tuple_StartFailed.LogEvent(null, new object[]
				{
					ex.ToString()
				});
				this.status.currentState = 1;
			}
			this.startCompletedSignal.Set();
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x0008BD64 File Offset: 0x00089F64
		private void SendWatsonReportOnTimeout(string caller, TimeSpan timeout, TimerCallback timeoutHandler, ReplayServiceBase.UnderTimeoutDelegate underTimeoutDelegate)
		{
			string state = string.Concat(new object[]
			{
				caller,
				" started on thread ",
				Environment.CurrentManagedThreadId,
				" at ",
				ExDateTime.Now.ToString(),
				" but did not complete in alloted time of ",
				timeout.ToString()
			});
			using (new Timer(timeoutHandler, state, timeout, TimeSpan.Zero))
			{
				underTimeoutDelegate();
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x0008BE00 File Offset: 0x0008A000
		private void OnStartTimeoutHandler(object state)
		{
			if (!Debugger.IsAttached)
			{
				this.OnCommandTimeout();
				ReplayServiceBase.ServiceTimeoutException ex = new ReplayServiceBase.ServiceTimeoutException(state as string);
				Dependencies.Watson.SendReport(ex);
				throw ex;
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0008BE54 File Offset: 0x0008A054
		private void OnStopTimeoutHandler(object state)
		{
			if (!Debugger.IsAttached)
			{
				this.OnCommandTimeout();
				Action action = delegate()
				{
					this.CauseWatson1(state as string);
				};
				this.HavePossibleHungComponentInvoke(action);
				action();
			}
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0008BEA4 File Offset: 0x0008A0A4
		private void CauseWatson1(string state)
		{
			ReplayServiceBase.ServiceTimeoutException ex = new ReplayServiceBase.ServiceTimeoutException(state);
			Dependencies.Watson.SendReport(ex);
			throw ex;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0008BEC4 File Offset: 0x0008A0C4
		protected virtual void HavePossibleHungComponentInvoke(Action toInvoke)
		{
		}

		// Token: 0x04000CD0 RID: 3280
		public const int MaxNameLength = 80;

		// Token: 0x04000CD1 RID: 3281
		private readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(10.0);

		// Token: 0x04000CD2 RID: 3282
		private NativeMethods.SERVICE_STATUS status = default(NativeMethods.SERVICE_STATUS);

		// Token: 0x04000CD3 RID: 3283
		private IntPtr statusHandle;

		// Token: 0x04000CD4 RID: 3284
		private NativeMethods.ServiceControlCallback commandCallback;

		// Token: 0x04000CD5 RID: 3285
		private NativeMethods.ServiceControlCallbackEx commandCallbackEx;

		// Token: 0x04000CD6 RID: 3286
		private NativeMethods.ServiceMainCallback mainCallback;

		// Token: 0x04000CD7 RID: 3287
		private IntPtr handleName;

		// Token: 0x04000CD8 RID: 3288
		private ManualResetEvent startCompletedSignal;

		// Token: 0x04000CD9 RID: 3289
		private int acceptedCommands;

		// Token: 0x04000CDA RID: 3290
		private bool autoLog;

		// Token: 0x04000CDB RID: 3291
		private string serviceName;

		// Token: 0x04000CDC RID: 3292
		private EventLog eventLog;

		// Token: 0x04000CDD RID: 3293
		private bool nameFrozen;

		// Token: 0x04000CDE RID: 3294
		private bool commandPropsFrozen;

		// Token: 0x04000CDF RID: 3295
		private bool disposed;

		// Token: 0x04000CE0 RID: 3296
		private bool initialized;

		// Token: 0x04000CE1 RID: 3297
		private bool isServiceHosted;

		// Token: 0x020002F5 RID: 757
		// (Invoke) Token: 0x06001ED5 RID: 7893
		private delegate void DeferredHandlerDelegate();

		// Token: 0x020002F6 RID: 758
		// (Invoke) Token: 0x06001ED9 RID: 7897
		private delegate void DeferredHandlerDelegateCommand(int command);

		// Token: 0x020002F7 RID: 759
		// (Invoke) Token: 0x06001EDD RID: 7901
		private delegate void DeferredHandlerDelegateAdvanced(int eventType, IntPtr eventData);

		// Token: 0x020002F8 RID: 760
		// (Invoke) Token: 0x06001EE1 RID: 7905
		private delegate void UnderTimeoutDelegate();

		// Token: 0x020002F9 RID: 761
		private class ServiceTimeoutException : Exception
		{
			// Token: 0x06001EE4 RID: 7908 RVA: 0x0008BEC6 File Offset: 0x0008A0C6
			public ServiceTimeoutException(string message) : base(message)
			{
			}
		}
	}
}
