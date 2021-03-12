using System;
using System.Globalization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000BE RID: 190
	internal class MobileSpeechRecoRpcClient : MobileSpeechRecoRpcClientBase
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00019334 File Offset: 0x00017534
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x0001933C File Offset: 0x0001753C
		public object State { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x00019345 File Offset: 0x00017545
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0001934D File Offset: 0x0001754D
		private string Description { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x00019356 File Offset: 0x00017556
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0001935E File Offset: 0x0001755E
		private string ServerName { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00019367 File Offset: 0x00017567
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0001936F File Offset: 0x0001756F
		private MobileSpeechRecoRpcClient.MobileSpeechRecoAsyncResult AsyncResult { get; set; }

		// Token: 0x0600068B RID: 1675 RVA: 0x00019378 File Offset: 0x00017578
		public MobileSpeechRecoRpcClient(Guid requestId, string serverName, object state) : base(requestId, serverName)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "Entering MobileSpeechRecoRpcClient constructor", new object[0]);
			this.ServerName = serverName;
			this.State = state;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000193B0 File Offset: 0x000175B0
		public IAsyncResult BeginAddRecoRequest(MobileSpeechRecoRequestType requestType, Guid userObjectGuid, Guid tenantGuid, CultureInfo culture, ExTimeZone timeZone, AsyncCallback callback, object state)
		{
			ValidateArgument.NotNull(culture, "culture");
			ValidateArgument.NotNull(timeZone, "timeZone");
			CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcClient.BeginAddRecoRequest requestId='{0}', requestType='{1}', userObjectGuid='{2}', tenantGuid='{3}', culture='{4}', timeZone='{5}'", new object[]
			{
				base.RequestId,
				requestType,
				userObjectGuid,
				tenantGuid,
				culture,
				timeZone.Id
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientAddRecoRequestRPCParams, null, new object[]
			{
				base.RequestId,
				requestType,
				culture.Name,
				userObjectGuid,
				tenantGuid,
				timeZone.Id,
				this.ServerName
			});
			this.AsyncResult = new MobileSpeechRecoRpcClient.MobileSpeechRecoAsyncResult(base.RequestId, callback, state);
			this.Description = "Add Reco Request";
			byte[] bytes = new MdbefPropertyCollection
			{
				{
					2415919107U,
					0
				},
				{
					2415984712U,
					base.RequestId
				},
				{
					2416050179U,
					(int)requestType
				},
				{
					2416181320U,
					userObjectGuid
				},
				{
					2416377928U,
					tenantGuid
				},
				{
					2416115743U,
					culture.ToString()
				},
				{
					2416312351U,
					timeZone.Id
				}
			}.GetBytes();
			this.BeginExecuteStepWrapper(bytes);
			return this.AsyncResult;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00019548 File Offset: 0x00017748
		public MobileRecoRPCAsyncCompletedArgs EndAddRecoRequest(IAsyncResult asyncResult)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcClient.EndAddRecoRequest requestId='{0}'", new object[]
			{
				base.RequestId
			});
			return this.InternalEndOperation(asyncResult);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001958C File Offset: 0x0001778C
		public IAsyncResult BeginRecognize(byte[] audioBytes, AsyncCallback callback, object state)
		{
			ValidateArgument.NotNull(audioBytes, "audioBytes");
			CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcClient.BeginRecognize requestId='{0}', audioBytes.Length='{1}' bytes", new object[]
			{
				base.RequestId,
				audioBytes.Length
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientRecognizeRPCParams, null, new object[]
			{
				base.RequestId,
				audioBytes.Length,
				this.ServerName
			});
			this.AsyncResult = new MobileSpeechRecoRpcClient.MobileSpeechRecoAsyncResult(base.RequestId, callback, state);
			this.Description = "Recognize";
			byte[] bytes = new MdbefPropertyCollection
			{
				{
					2415919107U,
					1
				},
				{
					2415984712U,
					base.RequestId
				},
				{
					2416247042U,
					audioBytes
				}
			}.GetBytes();
			this.BeginExecuteStepWrapper(bytes);
			return this.AsyncResult;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00019688 File Offset: 0x00017888
		public MobileRecoRPCAsyncCompletedArgs EndRecognize(IAsyncResult asyncResult)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcClient.EndRecognize requestId='{0}'", new object[]
			{
				base.RequestId
			});
			return this.InternalEndOperation(asyncResult);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000196CC File Offset: 0x000178CC
		private MobileRecoRPCAsyncCompletedArgs InternalEndOperation(IAsyncResult asyncResult)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcClient.InternalEndOperation requestId='{0}'", new object[]
			{
				base.RequestId
			});
			MobileRecoRPCAsyncCompletedArgs completedArgs;
			using (MobileSpeechRecoRpcClient.MobileSpeechRecoAsyncResult mobileSpeechRecoAsyncResult = asyncResult as MobileSpeechRecoRpcClient.MobileSpeechRecoAsyncResult)
			{
				mobileSpeechRecoAsyncResult.AsyncWaitHandle.WaitOne();
				completedArgs = mobileSpeechRecoAsyncResult.CompletedArgs;
			}
			return completedArgs;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00019740 File Offset: 0x00017940
		private void BeginExecuteStepWrapper(byte[] inBlob)
		{
			try
			{
				base.BeginExecuteStep(inBlob, new AsyncCallback(this.InternalAsyncCallback));
			}
			catch (RpcException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcClient.BeginExecuteStepWrapper requestId='{0}', error='{1}'", new object[]
				{
					base.RequestId,
					ex
				});
				this.CompleteOperation(string.Empty, -2147466752, ex.ToString());
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000197C0 File Offset: 0x000179C0
		private void InternalAsyncCallback(IAsyncResult ar)
		{
			string text = string.Empty;
			int num = 0;
			string text2 = string.Empty;
			Exception ex = null;
			try
			{
				byte[] array;
				bool flag;
				base.EndExecuteStep(ar, out array, out flag);
				if (flag)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcClient.AsyncCallback requestId='{0}' timed out", new object[]
					{
						base.RequestId
					});
					text = string.Empty;
					num = -2147466742;
					text2 = string.Empty;
				}
				else
				{
					object obj = null;
					MdbefPropertyCollection args = MdbefPropertyCollection.Create(array, 0, array.Length);
					this.ExtractArg(args, 2499805215U, "Result not found", out obj);
					text = (obj as string);
					if (text == null)
					{
						throw new ArgumentOutOfRangeException("result", "Result is null");
					}
					this.ExtractArg(args, 2499870723U, "Error code not found", out obj);
					num = (int)obj;
					this.ExtractArg(args, 2499936287U, "Error message not found", out obj);
					text2 = (obj as string);
					if (text2 == null)
					{
						throw new ArgumentOutOfRangeException("errorMessage", "Error message is null");
					}
					CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcClient.AsyncCallback requestId='{0}', result='{1}', errorCode='{2}', errorMessage='{3}'", new object[]
					{
						base.RequestId,
						text,
						num,
						text2
					});
				}
			}
			catch (RpcException ex2)
			{
				ex = ex2;
			}
			catch (ArgumentException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					CallIdTracer.TraceError(ExTraceGlobals.RpcTracer, this.GetHashCode(), "MobileSpeechRecoRpcClient.AsyncCallback requestId='{0}', error='{1}'", new object[]
					{
						base.RequestId,
						ex
					});
					text = string.Empty;
					num = -2147466752;
					text2 = ex.ToString();
				}
			}
			this.CompleteOperation(text, num, text2);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000199C0 File Offset: 0x00017BC0
		private void ExtractArg(MdbefPropertyCollection args, uint argTag, string errorMessage, out object val)
		{
			val = null;
			if (!args.TryGetValue(argTag, out val))
			{
				throw new ArgumentException(errorMessage);
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000199D8 File Offset: 0x00017BD8
		private void CompleteOperation(string result, int errorCode, string errorMessage)
		{
			if (errorCode == 0)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientRPCSuccess, null, new object[]
				{
					base.RequestId,
					this.Description,
					CommonUtil.ToEventLogString(result),
					this.ServerName
				});
			}
			else if (UMErrorCode.IsUserInputError(errorCode))
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientRPCSuccess, null, new object[]
				{
					base.RequestId,
					this.Description,
					CommonUtil.ToEventLogString(errorMessage),
					this.ServerName
				});
			}
			else
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MobileSpeechRecoClientRPCFailure, null, new object[]
				{
					base.RequestId,
					this.Description,
					errorCode,
					CommonUtil.ToEventLogString(errorMessage),
					this.ServerName
				});
			}
			MobileRecoRPCAsyncCompletedArgs result2 = new MobileRecoRPCAsyncCompletedArgs(result, errorCode, errorMessage);
			this.AsyncResult.CompleteOperation(result2);
		}

		// Token: 0x040003BD RID: 957
		private const string AddRecoRequestDescriptionStr = "Add Reco Request";

		// Token: 0x040003BE RID: 958
		private const string RecognizeDescriptionStr = "Recognize";

		// Token: 0x020000BF RID: 191
		private class MobileSpeechRecoAsyncResult : AsyncResult
		{
			// Token: 0x06000695 RID: 1685 RVA: 0x00019ADB File Offset: 0x00017CDB
			public MobileSpeechRecoAsyncResult(Guid requestId, AsyncCallback callback, object state) : base(callback, state, false)
			{
				this.RequestId = requestId;
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x06000696 RID: 1686 RVA: 0x00019AED File Offset: 0x00017CED
			// (set) Token: 0x06000697 RID: 1687 RVA: 0x00019AF5 File Offset: 0x00017CF5
			public MobileRecoRPCAsyncCompletedArgs CompletedArgs { get; set; }

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000698 RID: 1688 RVA: 0x00019AFE File Offset: 0x00017CFE
			// (set) Token: 0x06000699 RID: 1689 RVA: 0x00019B06 File Offset: 0x00017D06
			private Guid RequestId { get; set; }

			// Token: 0x0600069A RID: 1690 RVA: 0x00019B10 File Offset: 0x00017D10
			internal void CompleteOperation(MobileRecoRPCAsyncCompletedArgs result)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.RpcTracer, this.GetHashCode(), "Entering MobileSpeechRecoRpcAsyncResult.CompleteOperation requestId='{0}'", new object[]
				{
					this.RequestId
				});
				this.CompletedArgs = result;
				base.IsCompleted = true;
			}
		}
	}
}
