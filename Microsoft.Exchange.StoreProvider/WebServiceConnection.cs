using System;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.XropService;

namespace Microsoft.Mapi
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WebServiceConnection : MapiUnk
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00004DDC File Offset: 0x00002FDC
		internal WebServiceConnection(Client xropClient)
		{
			this.client = xropClient;
			this.connectDelegate = new WebServiceConnection.ConnectDelegate(this.ConnectCallback);
			this.executeDelegate = new WebServiceConnection.ExecuteDelegate(this.ExecuteCallback);
			this.disconnectDelegate = new WebServiceConnection.DisconnectDelegate(this.DisconnectCallback);
			this.intPtrConnectDelegate = Marshal.GetFunctionPointerForDelegate(this.connectDelegate);
			this.intPtrExecuteDelegate = Marshal.GetFunctionPointerForDelegate(this.executeDelegate);
			this.intPtrDisconnectDelegate = Marshal.GetFunctionPointerForDelegate(this.disconnectDelegate);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004E80 File Offset: 0x00003080
		internal IntPtr NativeConnectDelegate
		{
			get
			{
				return this.intPtrConnectDelegate;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004E88 File Offset: 0x00003088
		internal IntPtr NativeExecuteDelegate
		{
			get
			{
				return this.intPtrExecuteDelegate;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004E90 File Offset: 0x00003090
		internal IntPtr NativeDisconnectDelegate
		{
			get
			{
				return this.intPtrDisconnectDelegate;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004E98 File Offset: 0x00003098
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004EA0 File Offset: 0x000030A0
		internal Exception LastException
		{
			get
			{
				return this.lastException;
			}
			set
			{
				this.lastException = value;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004EAC File Offset: 0x000030AC
		private void TraceFailure(uint serviceCode, uint errorCode, string functionName)
		{
			if (serviceCode != 0U)
			{
				ComponentTrace<MapiNetTags>.Trace<string, uint>(60855, 580, (long)this.GetHashCode(), "WebServiceConnection.{0}: Failure ServiceCode from response: statusCode={1}", functionName, serviceCode);
				return;
			}
			if (errorCode != 0U)
			{
				ComponentTrace<MapiNetTags>.Trace<string, uint>(36279, 580, (long)this.GetHashCode(), "WebServiceConnection.{0}: Failure ErrorCode from response: errorCode={1}", functionName, errorCode);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004EFA File Offset: 0x000030FA
		private void TraceNullResponse(string functionName)
		{
			ComponentTrace<MapiNetTags>.Trace<string>(52663, 580, (long)this.GetHashCode(), "WebServiceConnection.{0}: Call returned null response object", functionName);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004F18 File Offset: 0x00003118
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WebServiceConnection>(this);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000052AC File Offset: 0x000034AC
		internal uint ConnectCallback(out uint errorCode, out uint context, IntPtr iUserDN, uint flags, uint connectionModulus, uint limit, uint cpid, uint lcidString, uint lcidSort, uint contextIdLink, ushort canConvertCodePages, out uint pollsMax, out uint retry, out uint retryDelay, out ushort contextId, out IntPtr iDNPrefix, out IntPtr iDisplayName, IntPtr iClientVersion, IntPtr iServerVersion, IntPtr iBestVersion, ref uint timeStamp, IntPtr iAuxIn, uint sizeAuxIn, IntPtr iAuxOut, ref uint sizeAuxOut)
		{
			bool isSuccessful = false;
			uint localErrorCode = 0U;
			uint localContext = 0U;
			uint localPollsMax = 0U;
			uint localRetry = 0U;
			uint localRetryDelay = 0U;
			ushort localContextId = 0;
			IntPtr localDNPrefix = IntPtr.Zero;
			IntPtr localDisplayName = IntPtr.Zero;
			uint localTimeStamp = timeStamp;
			uint localSizeAuxOut = sizeAuxOut;
			errorCode = 0U;
			context = 0U;
			pollsMax = 0U;
			retry = 0U;
			retryDelay = 0U;
			contextId = 0;
			iDNPrefix = IntPtr.Zero;
			iDisplayName = IntPtr.Zero;
			uint result = this.Execute(delegate
			{
				uint localSizeAuxOut = localSizeAuxOut;
				string userDN = null;
				if (iUserDN != IntPtr.Zero)
				{
					userDN = Marshal.PtrToStringAnsi(iUserDN);
				}
				byte[] array = new byte[6];
				Marshal.Copy(iClientVersion, array, 0, 6);
				byte[] array2 = new byte[sizeAuxIn];
				if (sizeAuxIn > 0U)
				{
					Marshal.Copy(iAuxIn, array2, 0, (int)sizeAuxIn);
				}
				ConnectRequest request = new ConnectRequest
				{
					UserDN = userDN,
					Flags = flags,
					ConMod = connectionModulus,
					Limit = limit,
					Cpid = cpid,
					LcidString = lcidString,
					LcidSort = lcidSort,
					IcxrLink = contextIdLink,
					FCanConvertCodePages = canConvertCodePages,
					ClientVersion = array,
					TimeStamp = localTimeStamp,
					AuxIn = array2,
					AuxOutMaxSize = localSizeAuxOut,
					Interactive = this.client.IsClientInteractive
				};
				ConnectResponse connectResponse = this.client.Connect(request);
				if (connectResponse == null)
				{
					this.TraceNullResponse("ConnectCallback");
					return 1726U;
				}
				localErrorCode = connectResponse.ErrorCode;
				localContext = connectResponse.Context;
				localPollsMax = connectResponse.PollsMax;
				localRetry = connectResponse.Retry;
				localRetryDelay = connectResponse.RetryDelay;
				localContextId = connectResponse.Icxr;
				if (connectResponse.DNPrefix != null)
				{
					localDNPrefix = Marshal.StringToCoTaskMemAnsi(connectResponse.DNPrefix);
				}
				if (connectResponse.DisplayName != null)
				{
					localDisplayName = Marshal.StringToCoTaskMemAnsi(connectResponse.DisplayName);
				}
				if (iServerVersion != IntPtr.Zero && connectResponse.ServerVersion != null && connectResponse.ServerVersion.Length == 6)
				{
					Marshal.Copy(connectResponse.ServerVersion, 0, iServerVersion, 6);
				}
				if (iBestVersion != IntPtr.Zero && connectResponse.BestVersion != null && connectResponse.BestVersion.Length == 6)
				{
					Marshal.Copy(connectResponse.BestVersion, 0, iBestVersion, 6);
				}
				localTimeStamp = connectResponse.TimeStamp;
				if (iAuxOut == IntPtr.Zero || connectResponse.AuxOut == null || (long)connectResponse.AuxOut.Length > (long)((ulong)localSizeAuxOut))
				{
					localSizeAuxOut = 0U;
				}
				else
				{
					localSizeAuxOut = (uint)connectResponse.AuxOut.Length;
					Marshal.Copy(connectResponse.AuxOut, 0, iAuxOut, connectResponse.AuxOut.Length);
				}
				this.TraceFailure(connectResponse.ServiceCode, connectResponse.ErrorCode, "ConnectCallback");
				isSuccessful = true;
				return connectResponse.ServiceCode;
			}, delegate
			{
				if (!isSuccessful)
				{
					if (localDNPrefix != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(localDNPrefix);
						localDNPrefix = IntPtr.Zero;
					}
					if (localDisplayName != IntPtr.Zero)
					{
						Marshal.FreeCoTaskMem(localDisplayName);
						localDisplayName = IntPtr.Zero;
					}
					localErrorCode = 0U;
					localContext = 0U;
					localPollsMax = 0U;
					localRetry = 0U;
					localRetryDelay = 0U;
					localContextId = 0;
					localSizeAuxOut = 0U;
				}
			}, "ConnectCallback");
			errorCode = localErrorCode;
			context = localContext;
			pollsMax = localPollsMax;
			retry = localRetry;
			retryDelay = localRetryDelay;
			contextId = localContextId;
			iDNPrefix = localDNPrefix;
			iDisplayName = localDisplayName;
			timeStamp = localTimeStamp;
			sizeAuxOut = localSizeAuxOut;
			return result;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005674 File Offset: 0x00003874
		internal uint ExecuteCallback(out uint errorCode, ref uint context, ref uint flags, IntPtr iRopIn, uint sizeRopIn, IntPtr iRopOut, ref uint sizeRopOut, IntPtr iAuxIn, uint sizeAuxIn, IntPtr iAuxOut, ref uint sizeAuxOut, out uint transferTime)
		{
			bool isSuccessful = false;
			uint localErrorCode = 0U;
			uint localTransferTime = 0U;
			uint localContext = context;
			uint localFlags = flags;
			uint localSizeRopOut = sizeRopOut;
			uint localSizeAuxOut = sizeAuxOut;
			errorCode = 0U;
			transferTime = 0U;
			uint result = this.Execute(delegate
			{
				uint localSizeRopOut = localSizeRopOut;
				uint localSizeAuxOut = localSizeAuxOut;
				byte[] array = new byte[sizeRopIn];
				if (sizeRopIn > 0U)
				{
					Marshal.Copy(iRopIn, array, 0, (int)sizeRopIn);
				}
				byte[] array2 = new byte[sizeAuxIn];
				if (sizeAuxIn > 0U)
				{
					Marshal.Copy(iAuxIn, array2, 0, (int)sizeAuxIn);
				}
				ExecuteRequest request = new ExecuteRequest
				{
					Context = localContext,
					Flags = localFlags,
					In = array,
					MaxSizeOut = localSizeRopOut,
					AuxIn = array2,
					MaxSizeAuxOut = localSizeAuxOut
				};
				ExecuteResponse executeResponse = this.client.Execute(request);
				if (executeResponse == null)
				{
					this.TraceNullResponse("ExecuteCallback");
					return 1726U;
				}
				localErrorCode = executeResponse.ErrorCode;
				localFlags = executeResponse.Flags;
				localContext = executeResponse.Context;
				if (iRopOut == IntPtr.Zero || executeResponse.Out == null || (long)executeResponse.Out.Length > (long)((ulong)localSizeRopOut))
				{
					localSizeRopOut = 0U;
				}
				else
				{
					localSizeRopOut = (uint)executeResponse.Out.Length;
					Marshal.Copy(executeResponse.Out, 0, iRopOut, executeResponse.Out.Length);
				}
				if (iAuxOut == IntPtr.Zero || executeResponse.AuxOut == null || (long)executeResponse.AuxOut.Length > (long)((ulong)localSizeAuxOut))
				{
					localSizeAuxOut = 0U;
				}
				else
				{
					localSizeAuxOut = (uint)executeResponse.AuxOut.Length;
					Marshal.Copy(executeResponse.AuxOut, 0, iAuxOut, executeResponse.AuxOut.Length);
				}
				localTransferTime = executeResponse.TransTime;
				this.TraceFailure(executeResponse.ServiceCode, executeResponse.ErrorCode, "ExecuteCallback");
				isSuccessful = true;
				return executeResponse.ServiceCode;
			}, delegate
			{
				if (!isSuccessful)
				{
					localErrorCode = 0U;
					localSizeRopOut = 0U;
					localSizeAuxOut = 0U;
					localTransferTime = 0U;
				}
			}, "ExecuteCallback");
			errorCode = localErrorCode;
			flags = localFlags;
			context = localContext;
			sizeRopOut = localSizeRopOut;
			sizeAuxOut = localSizeAuxOut;
			transferTime = localTransferTime;
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005804 File Offset: 0x00003A04
		internal uint DisconnectCallback(out uint errorCode, ref uint context)
		{
			bool isSuccessful = false;
			uint localErrorCode = 0U;
			uint localContext = context;
			errorCode = 0U;
			uint result = this.Execute(delegate
			{
				DisconnectRequest request = new DisconnectRequest
				{
					Context = localContext
				};
				DisconnectResponse disconnectResponse = this.client.Disconnect(request);
				if (disconnectResponse == null)
				{
					this.TraceNullResponse("DisconnectCallback");
					return 1726U;
				}
				localErrorCode = disconnectResponse.ErrorCode;
				localContext = disconnectResponse.Context;
				this.TraceFailure(disconnectResponse.ServiceCode, disconnectResponse.ErrorCode, "DisconnectCallback");
				isSuccessful = true;
				return disconnectResponse.ServiceCode;
			}, delegate
			{
				if (!isSuccessful)
				{
					localErrorCode = 0U;
					localContext = 0U;
				}
			}, "DisconnectCallback");
			errorCode = localErrorCode;
			context = localContext;
			return result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000586C File Offset: 0x00003A6C
		private uint Execute(Func<uint> executeDelegate, Action finallyDelegate, string functionName)
		{
			uint num = 0U;
			Exception ex = null;
			try
			{
				num = executeDelegate();
			}
			catch (EndpointNotFoundException ex2)
			{
				num = 1722U;
				ex = ex2;
			}
			catch (ServerTooBusyException ex3)
			{
				num = 1723U;
				ex = ex3;
			}
			catch (SecurityAccessDeniedException ex4)
			{
				num = 5U;
				ex = ex4;
			}
			catch (ActionNotSupportedException ex5)
			{
				num = 1745U;
				ex = ex5;
			}
			catch (ProtocolException ex6)
			{
				num = 1728U;
				ex = ex6;
			}
			catch (CommunicationException ex7)
			{
				num = 1726U;
				ex = ex7;
			}
			catch (TimeoutException ex8)
			{
				num = 1460U;
				ex = ex8;
			}
			catch (Exception ex9)
			{
				num = (uint)Marshal.GetHRForException(ex9);
				ex = ex9;
			}
			finally
			{
				finallyDelegate();
			}
			if (num != 0U && ComponentTrace<MapiNetTags>.CheckEnabled(580))
			{
				ComponentTrace<MapiNetTags>.Trace<string, uint, string>(44471, 580, (long)this.GetHashCode(), "WebServiceConnection.{0}: serviceCode={1}, serviceException=[{2}]", functionName, num, (ex != null) ? ex.ToString() : string.Empty);
			}
			this.lastException = ex;
			return num;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000059A8 File Offset: 0x00003BA8
		protected override void MapiInternalDispose()
		{
			if (this.client != null)
			{
				try
				{
					this.client.Dispose();
				}
				catch (CommunicationException ex)
				{
					ComponentTrace<MapiNetTags>.Trace<string>(46519, 580, (long)this.GetHashCode(), "WebServiceConnection.MapiInternalDispose: exception=[{0}]", ex.ToString());
				}
				catch (TimeoutException ex2)
				{
					ComponentTrace<MapiNetTags>.Trace<string>(58535, 580, (long)this.GetHashCode(), "WebServiceConnection.MapiInternalDispose: exception=[{0}]", ex2.ToString());
				}
			}
		}

		// Token: 0x040000F5 RID: 245
		private Client client;

		// Token: 0x040000F6 RID: 246
		private IntPtr intPtrConnectDelegate = IntPtr.Zero;

		// Token: 0x040000F7 RID: 247
		private IntPtr intPtrExecuteDelegate = IntPtr.Zero;

		// Token: 0x040000F8 RID: 248
		private IntPtr intPtrDisconnectDelegate = IntPtr.Zero;

		// Token: 0x040000F9 RID: 249
		private WebServiceConnection.ConnectDelegate connectDelegate;

		// Token: 0x040000FA RID: 250
		private WebServiceConnection.ExecuteDelegate executeDelegate;

		// Token: 0x040000FB RID: 251
		private WebServiceConnection.DisconnectDelegate disconnectDelegate;

		// Token: 0x040000FC RID: 252
		private Exception lastException;

		// Token: 0x02000022 RID: 34
		internal enum StatusCode : uint
		{
			// Token: 0x040000FE RID: 254
			AccessDenied = 5U,
			// Token: 0x040000FF RID: 255
			Timeout = 1460U,
			// Token: 0x04000100 RID: 256
			ServerUnavailable = 1722U,
			// Token: 0x04000101 RID: 257
			ServerTooBusy,
			// Token: 0x04000102 RID: 258
			CallFailed = 1726U,
			// Token: 0x04000103 RID: 259
			ProtocolError = 1728U,
			// Token: 0x04000104 RID: 260
			ProcNumOutOfRange = 1745U
		}

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x060000F8 RID: 248
		internal delegate uint ConnectDelegate(out uint errorCode, out uint context, IntPtr iUserDN, uint flags, uint connectionModulus, uint limit, uint cpid, uint lcidString, uint lcidSort, uint contextIdLink, ushort canConvertCodePages, out uint pollsMax, out uint retry, out uint retryDelay, out ushort contextId, out IntPtr iDNPrefix, out IntPtr iDisplayName, IntPtr iClientVersion, IntPtr iServerVersion, IntPtr iBestVersion, ref uint timeStamp, IntPtr iAuxIn, uint sizeAuxIn, IntPtr iAuxOut, ref uint sizeAuxOut);

		// Token: 0x02000024 RID: 36
		// (Invoke) Token: 0x060000FC RID: 252
		internal delegate uint ExecuteDelegate(out uint errorCode, ref uint context, ref uint flags, IntPtr iRopIn, uint sizeRopIn, IntPtr iRopOut, ref uint sizeRopOut, IntPtr iAuxIn, uint sizeAuxIn, IntPtr iAuxOut, ref uint sizeAuxOut, out uint transferTime);

		// Token: 0x02000025 RID: 37
		// (Invoke) Token: 0x06000100 RID: 256
		internal delegate uint DisconnectDelegate(out uint errorCode, ref uint context);
	}
}
