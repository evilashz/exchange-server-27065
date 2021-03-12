using System;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x0200004F RID: 79
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	public class DxStoreAccess : IDxStoreAccess
	{
		// Token: 0x0600028F RID: 655 RVA: 0x000049A8 File Offset: 0x00002BA8
		public DxStoreAccess(DxStoreInstance instance)
		{
			this.instance = instance;
			this.UpdateTimeout = instance.GroupConfig.Settings.PaxosUpdateTimeout;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000049CD File Offset: 0x00002BCD
		// (set) Token: 0x06000291 RID: 657 RVA: 0x000049D5 File Offset: 0x00002BD5
		public TimeSpan UpdateTimeout { get; set; }

		// Token: 0x06000292 RID: 658 RVA: 0x000049E0 File Offset: 0x00002BE0
		public WriteResult CreateKey(string baseKeyName, string subKeyName, WriteOptions writeOptions)
		{
			DxStoreCommand.CreateKey command = new DxStoreCommand.CreateKey
			{
				KeyName = baseKeyName,
				SubkeyName = subKeyName
			};
			return this.ExecuteCommand(command, writeOptions, this.UpdateTimeout);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00004A1A File Offset: 0x00002C1A
		public DxStoreAccessReply.CheckKey CheckKey(DxStoreAccessRequest.CheckKey request)
		{
			return this.RunServerOperationAndConvertToFaultException<DxStoreAccessRequest.CheckKey, DxStoreAccessReply.CheckKey>(request, (DxStoreAccessRequest.CheckKey req) => this.CheckKeyInternal(req));
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00004A38 File Offset: 0x00002C38
		public DxStoreAccessReply.DeleteKey DeleteKey(DxStoreAccessRequest.DeleteKey request)
		{
			return this.RunServerOperationAndConvertToFaultException<DxStoreAccessRequest.DeleteKey, DxStoreAccessReply.DeleteKey>(request, (DxStoreAccessRequest.DeleteKey req) => this.DeleteKeyInternal(req));
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00004A56 File Offset: 0x00002C56
		public DxStoreAccessReply.SetProperty SetProperty(DxStoreAccessRequest.SetProperty request)
		{
			return this.RunServerOperationAndConvertToFaultException<DxStoreAccessRequest.SetProperty, DxStoreAccessReply.SetProperty>(request, (DxStoreAccessRequest.SetProperty req) => this.SetPropertyInternal(req));
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00004A74 File Offset: 0x00002C74
		public DxStoreAccessReply.DeleteProperty DeleteProperty(DxStoreAccessRequest.DeleteProperty request)
		{
			return this.RunServerOperationAndConvertToFaultException<DxStoreAccessRequest.DeleteProperty, DxStoreAccessReply.DeleteProperty>(request, (DxStoreAccessRequest.DeleteProperty req) => this.DeletePropertyInternal(req));
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00004A92 File Offset: 0x00002C92
		public DxStoreAccessReply.ExecuteBatch ExecuteBatch(DxStoreAccessRequest.ExecuteBatch request)
		{
			return this.RunServerOperationAndConvertToFaultException<DxStoreAccessRequest.ExecuteBatch, DxStoreAccessReply.ExecuteBatch>(request, (DxStoreAccessRequest.ExecuteBatch req) => this.ExecuteBatchInternal(req));
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00004AB0 File Offset: 0x00002CB0
		public DxStoreAccessReply.GetProperty GetProperty(DxStoreAccessRequest.GetProperty request)
		{
			return this.RunServerOperationAndConvertToFaultException<DxStoreAccessRequest.GetProperty, DxStoreAccessReply.GetProperty>(request, (DxStoreAccessRequest.GetProperty req) => this.GetPropertyInternal(req));
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00004ACE File Offset: 0x00002CCE
		public DxStoreAccessReply.GetAllProperties GetAllProperties(DxStoreAccessRequest.GetAllProperties request)
		{
			return this.RunServerOperationAndConvertToFaultException<DxStoreAccessRequest.GetAllProperties, DxStoreAccessReply.GetAllProperties>(request, (DxStoreAccessRequest.GetAllProperties req) => this.GetAllPropertiesInternal(req));
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00004AEC File Offset: 0x00002CEC
		public DxStoreAccessReply.GetPropertyNames GetPropertyNames(DxStoreAccessRequest.GetPropertyNames request)
		{
			return this.RunServerOperationAndConvertToFaultException<DxStoreAccessRequest.GetPropertyNames, DxStoreAccessReply.GetPropertyNames>(request, (DxStoreAccessRequest.GetPropertyNames req) => this.GetPropertyNamesInternal(req));
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00004B0A File Offset: 0x00002D0A
		public DxStoreAccessReply.GetSubkeyNames GetSubkeyNames(DxStoreAccessRequest.GetSubkeyNames request)
		{
			return this.RunServerOperationAndConvertToFaultException<DxStoreAccessRequest.GetSubkeyNames, DxStoreAccessReply.GetSubkeyNames>(request, (DxStoreAccessRequest.GetSubkeyNames req) => this.GetSubkeyNamesInternal(req));
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00004B20 File Offset: 0x00002D20
		public TRep RunServerOperationAndConvertToFaultException<TReq, TRep>(TReq request, Func<TReq, TRep> action) where TReq : DxStoreAccessRequest where TRep : DxStoreAccessReply
		{
			TRep trep = default(TRep);
			try
			{
				if (ExTraceGlobals.AccessEntryPointTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					string arg = Utils.SerializeObjectToJsonString<TReq>(request, false, true) ?? "<serialization error>";
					ExTraceGlobals.AccessEntryPointTracer.TraceDebug<string, string, string>((long)this.instance.IdentityHash, "{0}: Entering Request: {1} {2}", this.instance.Identity, typeof(TReq).Name, arg);
				}
				trep = action(request);
			}
			catch (Exception ex)
			{
				if (!this.instance.GroupConfig.Settings.IsUseHttpTransportForClientCommunication)
				{
					DxStoreServerFault dxStoreServerFault = WcfUtils.ConvertExceptionToDxStoreFault(ex);
					if (ExTraceGlobals.AccessEntryPointTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						string text = Utils.SerializeObjectToJsonString<DxStoreServerFault>(dxStoreServerFault, false, true) ?? "<serialization error>";
						ExTraceGlobals.AccessEntryPointTracer.TraceError((long)this.instance.IdentityHash, "{0}: Leaving (Failed) - Request: {1} - Fault: {2} - Exception: {3}", new object[]
						{
							this.instance.Identity,
							typeof(TReq).Name,
							text,
							ex
						});
					}
					throw new FaultException<DxStoreServerFault>(dxStoreServerFault);
				}
				ExTraceGlobals.AccessEntryPointTracer.TraceError<string, string, Exception>((long)this.instance.IdentityHash, "{0}: Leaving (Failed) - Request: {1} - Exception: {2}", this.instance.Identity, typeof(TReq).Name, ex);
				throw;
			}
			if (ExTraceGlobals.AccessEntryPointTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string text2 = (trep != null) ? (Utils.SerializeObjectToJsonString<TRep>(trep, false, true) ?? "<serialization error>") : "<null>";
				ExTraceGlobals.AccessEntryPointTracer.TraceDebug((long)this.instance.IdentityHash, "{0}: Leaving (Success) - Request: {1} Reply: {2} {3}", new object[]
				{
					this.instance.Identity,
					typeof(TReq).Name,
					typeof(TRep).Name,
					text2
				});
			}
			return trep;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00004D10 File Offset: 0x00002F10
		private DxStoreAccessReply.CheckKey CheckKeyInternal(DxStoreAccessRequest.CheckKey request)
		{
			bool isStale = !this.EnsureInstanceReadyAndNotStale(request.ReadOptions);
			DxStoreAccessReply.CheckKey checkKey = this.CreateReply<DxStoreAccessReply.CheckKey>();
			string keyName = Utils.CombinePathNullSafe(request.FullKeyName, request.SubkeyName);
			checkKey.ReadResult = new ReadResult
			{
				IsStale = isStale
			};
			checkKey.IsExist = this.instance.LocalDataStore.IsKeyExist(keyName);
			if (!checkKey.IsExist && request.IsCreateIfNotExist)
			{
				checkKey.WriteResult = this.CreateKey(request.FullKeyName, request.SubkeyName, request.WriteOptions);
				checkKey.IsCreated = true;
				checkKey.IsExist = true;
			}
			return this.FinishRequest<DxStoreAccessReply.CheckKey>(checkKey);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00004DB4 File Offset: 0x00002FB4
		private DxStoreAccessReply.DeleteKey DeleteKeyInternal(DxStoreAccessRequest.DeleteKey request)
		{
			bool isStale = !this.EnsureInstanceReadyAndNotStale(request.ReadOptions);
			DxStoreAccessReply.DeleteKey deleteKey = this.CreateReply<DxStoreAccessReply.DeleteKey>();
			string keyName = Utils.CombinePathNullSafe(request.FullKeyName, request.SubkeyName);
			deleteKey.ReadResult = new ReadResult
			{
				IsStale = isStale
			};
			deleteKey.IsExist = this.instance.LocalDataStore.IsKeyExist(keyName);
			if (deleteKey.IsExist)
			{
				DxStoreCommand.DeleteKey command = new DxStoreCommand.DeleteKey
				{
					KeyName = request.FullKeyName,
					SubkeyName = request.SubkeyName
				};
				deleteKey.WriteResult = this.ExecuteCommand(command, request.WriteOptions, this.UpdateTimeout);
			}
			return this.FinishRequest<DxStoreAccessReply.DeleteKey>(deleteKey);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00004E64 File Offset: 0x00003064
		private DxStoreAccessReply.SetProperty SetPropertyInternal(DxStoreAccessRequest.SetProperty request)
		{
			this.VerifyInstanceReady();
			DxStoreAccessReply.SetProperty setProperty = this.CreateReply<DxStoreAccessReply.SetProperty>();
			DxStoreCommand.SetProperty command = new DxStoreCommand.SetProperty
			{
				KeyName = request.FullKeyName,
				Name = request.Name,
				Value = request.Value
			};
			setProperty.WriteResult = this.ExecuteCommand(command, request.WriteOptions, this.UpdateTimeout);
			return this.FinishRequest<DxStoreAccessReply.SetProperty>(setProperty);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00004ECC File Offset: 0x000030CC
		private DxStoreAccessReply.DeleteProperty DeletePropertyInternal(DxStoreAccessRequest.DeleteProperty request)
		{
			bool isStale = !this.EnsureInstanceReadyAndNotStale(request.ReadOptions);
			DxStoreAccessReply.DeleteProperty deleteProperty = this.CreateReply<DxStoreAccessReply.DeleteProperty>();
			deleteProperty.ReadResult = new ReadResult
			{
				IsStale = isStale
			};
			deleteProperty.IsExist = this.instance.LocalDataStore.IsPropertyExist(request.FullKeyName, request.Name);
			if (deleteProperty.IsExist)
			{
				DxStoreCommand.DeleteProperty command = new DxStoreCommand.DeleteProperty
				{
					KeyName = request.FullKeyName,
					Name = request.Name
				};
				deleteProperty.WriteResult = this.ExecuteCommand(command, request.WriteOptions, this.UpdateTimeout);
			}
			return this.FinishRequest<DxStoreAccessReply.DeleteProperty>(deleteProperty);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00004F74 File Offset: 0x00003174
		private DxStoreAccessReply.ExecuteBatch ExecuteBatchInternal(DxStoreAccessRequest.ExecuteBatch request)
		{
			this.VerifyInstanceReady();
			DxStoreAccessReply.ExecuteBatch executeBatch = this.CreateReply<DxStoreAccessReply.ExecuteBatch>();
			DxStoreCommand.ExecuteBatch command = new DxStoreCommand.ExecuteBatch
			{
				KeyName = request.FullKeyName,
				Commands = request.Commands
			};
			executeBatch.WriteResult = this.ExecuteCommand(command, request.WriteOptions, this.UpdateTimeout);
			return this.FinishRequest<DxStoreAccessReply.ExecuteBatch>(executeBatch);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00004FD0 File Offset: 0x000031D0
		private DxStoreAccessReply.GetProperty GetPropertyInternal(DxStoreAccessRequest.GetProperty request)
		{
			bool isStale = !this.EnsureInstanceReadyAndNotStale(request.ReadOptions);
			DxStoreAccessReply.GetProperty getProperty = this.CreateReply<DxStoreAccessReply.GetProperty>();
			getProperty.ReadResult = new ReadResult
			{
				IsStale = isStale
			};
			getProperty.Value = this.instance.LocalDataStore.GetProperty(request.FullKeyName, request.Name);
			return this.FinishRequest<DxStoreAccessReply.GetProperty>(getProperty);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00005034 File Offset: 0x00003234
		private DxStoreAccessReply.GetAllProperties GetAllPropertiesInternal(DxStoreAccessRequest.GetAllProperties request)
		{
			bool isStale = !this.EnsureInstanceReadyAndNotStale(request.ReadOptions);
			DxStoreAccessReply.GetAllProperties getAllProperties = this.CreateReply<DxStoreAccessReply.GetAllProperties>();
			getAllProperties.ReadResult = new ReadResult
			{
				IsStale = isStale
			};
			getAllProperties.Values = this.instance.LocalDataStore.GetAllProperties(request.FullKeyName);
			return this.FinishRequest<DxStoreAccessReply.GetAllProperties>(getAllProperties);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00005090 File Offset: 0x00003290
		private DxStoreAccessReply.GetPropertyNames GetPropertyNamesInternal(DxStoreAccessRequest.GetPropertyNames request)
		{
			bool isStale = !this.EnsureInstanceReadyAndNotStale(request.ReadOptions);
			DxStoreAccessReply.GetPropertyNames getPropertyNames = this.CreateReply<DxStoreAccessReply.GetPropertyNames>();
			getPropertyNames.ReadResult = new ReadResult
			{
				IsStale = isStale
			};
			getPropertyNames.Infos = this.instance.LocalDataStore.EnumPropertyNames(request.FullKeyName);
			return this.FinishRequest<DxStoreAccessReply.GetPropertyNames>(getPropertyNames);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000050EC File Offset: 0x000032EC
		private DxStoreAccessReply.GetSubkeyNames GetSubkeyNamesInternal(DxStoreAccessRequest.GetSubkeyNames request)
		{
			bool isStale = !this.EnsureInstanceReadyAndNotStale(request.ReadOptions);
			DxStoreAccessReply.GetSubkeyNames getSubkeyNames = this.CreateReply<DxStoreAccessReply.GetSubkeyNames>();
			getSubkeyNames.ReadResult = new ReadResult
			{
				IsStale = isStale
			};
			getSubkeyNames.Keys = this.instance.LocalDataStore.EnumSubkeyNames(request.FullKeyName);
			return this.FinishRequest<DxStoreAccessReply.GetSubkeyNames>(getSubkeyNames);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00005148 File Offset: 0x00003348
		private T CreateReply<T>() where T : DxStoreAccessReply, new()
		{
			T result = Activator.CreateInstance<T>();
			result.Initialize(this.instance.GroupConfig.Self);
			return result;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000517C File Offset: 0x0000337C
		private WriteResult EnsureTestUpdateIsSuccessful(DxStoreCommand command, WriteOptions options, TimeSpan timeout)
		{
			DxStoreCommand.DummyCommand dummyCommand = new DxStoreCommand.DummyCommand
			{
				OriginalDbCommandId = command.CommandId
			};
			dummyCommand.Initialize(this.instance.GroupConfig.Self, options);
			WriteResult writeResult = this.ExecuteCommandWithAck(dummyCommand, options, true, timeout);
			if (!writeResult.IsConstraintPassed)
			{
				throw new DxStoreCommandConstraintFailedException("TestUpdate");
			}
			return writeResult;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000051D4 File Offset: 0x000033D4
		private WriteResult ExecuteCommand(DxStoreCommand command, WriteOptions options, TimeSpan timeout)
		{
			WriteResult writeResult = null;
			command.Initialize(this.instance.GroupConfig.Self, options);
			if (options != null && (options.IsPerformTestUpdate || options.IsWaitRequired()))
			{
				if (options.IsPerformTestUpdate)
				{
					this.EnsureTestUpdateIsSuccessful(command, options, timeout);
				}
				writeResult = this.ExecuteCommandWithAck(command, options, true, timeout);
				if (!writeResult.IsConstraintPassed)
				{
					this.instance.EventLogger.Log(DxEventSeverity.Warning, 0, "{0}: Failed to satisfy constraint in the second attempt. Updates will eventually catch up", new object[]
					{
						this.instance.GroupConfig.Identity
					});
				}
			}
			else
			{
				this.instance.StateMachine.ReplicateCommand(command, new TimeSpan?(timeout));
			}
			return writeResult;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00005280 File Offset: 0x00003480
		private WriteResult ExecuteCommandWithAck(DxStoreCommand command, WriteOptions options, bool isThrowOnException, TimeSpan timeout)
		{
			LocalCommitAcknowledger commitAcknowledger = this.instance.CommitAcknowledger;
			WriteResult writeResult = new WriteResult();
			Guid commandId = command.CommandId;
			try
			{
				commitAcknowledger.AddCommand(command, options);
				this.instance.StateMachine.ReplicateCommand(command, new TimeSpan?(timeout));
				if (commitAcknowledger.WaitForExecution(commandId, timeout))
				{
					writeResult.IsConstraintPassed = true;
				}
			}
			finally
			{
				writeResult.Responses = commitAcknowledger.RemoveCommand(commandId);
			}
			return writeResult;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000052FC File Offset: 0x000034FC
		private bool EnsureInstanceReadyAndNotStale(ReadOptions readOptions)
		{
			this.VerifyInstanceReady();
			return this.EnsureDatabaseNotStale(readOptions);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000530B File Offset: 0x0000350B
		private void VerifyInstanceReady()
		{
			this.instance.EnsureInstanceIsReady();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00005318 File Offset: 0x00003518
		private bool EnsureDatabaseNotStale(ReadOptions readOptions)
		{
			if (this.instance.HealthChecker.IsStoreReady())
			{
				return true;
			}
			if (readOptions != null && readOptions.IsAllowStale)
			{
				return false;
			}
			throw new DxStoreInstanceStaleStoreException();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00005340 File Offset: 0x00003540
		private void InitializeCommand(DxStoreCommand command, WriteOptions options)
		{
			command.TimeInitiated = DateTimeOffset.Now;
			command.Initiator = this.instance.GroupConfig.Self;
			if (options != null)
			{
				command.IsNotifyInitiator = options.IsWaitRequired();
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00005374 File Offset: 0x00003574
		private T FinishRequest<T>(T reply) where T : DxStoreAccessReply
		{
			reply.MostRecentUpdateNumber = this.instance.LocalDataStore.LastInstanceExecuted;
			reply.Duration = DateTimeOffset.Now - reply.TimeReceived;
			return reply;
		}

		// Token: 0x04000175 RID: 373
		private readonly DxStoreInstance instance;
	}
}
