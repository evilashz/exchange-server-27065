using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Protocols.FastTransfer;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x02000002 RID: 2
	internal abstract class RopHandlerBase : DisposableBase, IRopHandlerWithContext, IRopHandler, IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public RopResult Abort(IServerObject serverObject, AbortResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.Abort"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002134 File Offset: 0x00000334
		public RopResult AbortSubmit(IServerObject serverObject, StoreId folderId, StoreId messageId, AbortSubmitResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.AbortSubmit"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.AbortSubmit, RopHandlerBase.AbortSubmitClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.AbortSubmit, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.AbortSubmit, mapiBase);
								ropResult = this.AbortSubmit(this.mapiContext, mapiBase, RcaTypeHelpers.StoreIdToExchangeId(folderId, logon.StoreMailbox), RcaTypeHelpers.StoreIdToExchangeId(messageId, logon.StoreMailbox), resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnMid(messageId);
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(folderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 52U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 52U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 52U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.AbortSubmit, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000003 RID: 3
		protected abstract RopResult AbortSubmit(MapiContext context, MapiBase serverObject, ExchangeId folderId, ExchangeId messageId, AbortSubmitResultFactory resultFactory);

		// Token: 0x06000004 RID: 4 RVA: 0x00002604 File Offset: 0x00000804
		public RopResult AddressTypes(IServerObject serverObject, AddressTypesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.AddressTypes"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.AddressTypes, RopHandlerBase.AddressTypesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.AddressTypes, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.AddressTypes, mapiBase);
								ropResult = this.AddressTypes(this.mapiContext, mapiBase, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 73U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 73U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 73U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.AddressTypes, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000005 RID: 5
		protected abstract RopResult AddressTypes(MapiContext context, MapiBase serverObject, AddressTypesResultFactory resultFactory);

		// Token: 0x06000006 RID: 6 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public RopResult CloneStream(IServerObject serverObject, CloneStreamResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CloneStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = 2 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Stream).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CloneStream, RopHandlerBase.CloneStreamClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CloneStream, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CloneStream, mapiStream);
								ropResult = this.CloneStream(this.mapiContext, mapiStream, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Stream);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 59U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 59U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 59U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CloneStream, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7
		protected abstract RopResult CloneStream(MapiContext context, MapiStream serverObject, CloneStreamResultFactory resultFactory);

		// Token: 0x06000008 RID: 8 RVA: 0x00002FCC File Offset: 0x000011CC
		public RopResult CollapseRow(IServerObject serverObject, StoreId categoryId, CollapseRowResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CollapseRow"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CollapseRow, RopHandlerBase.CollapseRowClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CollapseRow, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CollapseRow, mapiViewTableBase);
								ropResult = this.CollapseRow(this.mapiContext, mapiViewTableBase, RcaTypeHelpers.StoreIdToExchangeId(categoryId, logon.StoreMailbox), resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 90U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 90U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 90U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CollapseRow, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9
		protected abstract RopResult CollapseRow(MapiContext context, MapiViewTableBase serverObject, ExchangeId categoryId, CollapseRowResultFactory resultFactory);

		// Token: 0x0600000A RID: 10 RVA: 0x000034A0 File Offset: 0x000016A0
		public RopResult CommitStream(IServerObject serverObject, CommitStreamResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CommitStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = 2 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsCommitStreamSharedMailboxOperation(mapiStream);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CommitStream, RopHandlerBase.CommitStreamClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CommitStream, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CommitStream, mapiStream);
								ropResult = this.CommitStream(this.mapiContext, mapiStream, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 93U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 93U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 93U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CommitStream, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600000B RID: 11
		protected abstract RopResult CommitStream(MapiContext context, MapiStream serverObject, CommitStreamResultFactory resultFactory);

		// Token: 0x0600000C RID: 12 RVA: 0x00003950 File Offset: 0x00001B50
		public RopResult CopyFolder(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, bool recurse, StoreId sourceSubFolderId, string destinationSubFolderName, CopyFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CopyFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (sourceServerObject == null)
				{
					throw new ArgumentNullException("sourceServerObject");
				}
				if (destinationServerObject == null)
				{
					throw new ArgumentNullException("destinationServerObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = sourceServerObject as MapiFolder;
				MapiFolder mapiFolder2 = destinationServerObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null || mapiFolder2 == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CopyFolder, RopHandlerBase.CopyFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CopyFolder, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CopyFolder, mapiFolder);
								ropResult = this.CopyFolder(this.mapiContext, mapiFolder, mapiFolder2, reportProgress, recurse, RcaTypeHelpers.StoreIdToExchangeId(sourceSubFolderId, logon.StoreMailbox), destinationSubFolderName, out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(sourceSubFolderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 54U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 54U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 54U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CopyFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600000D RID: 13
		protected abstract RopResult CopyFolder(MapiContext context, MapiFolder sourceServerObject, MapiFolder destinationServerObject, bool reportProgress, bool recurse, ExchangeId sourceSubFolderId, string destinationSubFolderName, out bool outputIsPartiallyCompleted, CopyFolderResultFactory resultFactory);

		// Token: 0x0600000E RID: 14 RVA: 0x00003E30 File Offset: 0x00002030
		public RopResult CopyProperties(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] propertyTags, CopyPropertiesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CopyProperties"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (sourceServerObject == null)
				{
					throw new ArgumentNullException("sourceServerObject");
				}
				if (destinationServerObject == null)
				{
					throw new ArgumentNullException("destinationServerObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = sourceServerObject as MapiPropBagBase;
				MapiPropBagBase mapiPropBagBase2 = destinationServerObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null || mapiPropBagBase2 == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = 4 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsCopyPropertiesSharedMailboxOperation(mapiPropBagBase);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CopyProperties, RopHandlerBase.CopyPropertiesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CopyProperties, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CopyProperties, mapiPropBagBase);
								ropResult = this.CopyProperties(this.mapiContext, mapiPropBagBase, mapiPropBagBase2, reportProgress, copyPropertiesFlags, propertyTags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 103U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 103U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 103U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CopyProperties, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600000F RID: 15
		protected abstract RopResult CopyProperties(MapiContext context, MapiPropBagBase sourceServerObject, MapiPropBagBase destinationServerObject, bool reportProgress, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] propertyTags, CopyPropertiesResultFactory resultFactory);

		// Token: 0x06000010 RID: 16 RVA: 0x00004304 File Offset: 0x00002504
		public RopResult CopyTo(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, bool copySubObjects, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] excludePropertyTags, CopyToResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CopyTo"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (sourceServerObject == null)
				{
					throw new ArgumentNullException("sourceServerObject");
				}
				if (destinationServerObject == null)
				{
					throw new ArgumentNullException("destinationServerObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = sourceServerObject as MapiPropBagBase;
				MapiPropBagBase mapiPropBagBase2 = destinationServerObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null || mapiPropBagBase2 == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = 4 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsCopyToSharedMailboxOperation(mapiPropBagBase);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CopyTo, RopHandlerBase.CopyToClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CopyTo, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CopyTo, mapiPropBagBase);
								ropResult = this.CopyTo(this.mapiContext, mapiPropBagBase, mapiPropBagBase2, reportProgress, copySubObjects, copyPropertiesFlags, excludePropertyTags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 57U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 57U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 57U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CopyTo, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000011 RID: 17
		protected abstract RopResult CopyTo(MapiContext context, MapiPropBagBase sourceServerObject, MapiPropBagBase destinationServerObject, bool reportProgress, bool copySubObjects, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] excludePropertyTags, CopyToResultFactory resultFactory);

		// Token: 0x06000012 RID: 18 RVA: 0x000047DC File Offset: 0x000029DC
		public RopResult CopyToExtended(IServerObject sourceServerObject, IServerObject destinationServerObject, bool copySubObjects, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] excludePropertyTags, CopyToExtendedResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.CopyToExtended"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (sourceServerObject == null)
				{
					throw new ArgumentNullException("sourceServerObject");
				}
				if (destinationServerObject == null)
				{
					throw new ArgumentNullException("destinationServerObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00004850 File Offset: 0x00002A50
		public RopResult CopyToStream(IServerObject sourceServerObject, IServerObject destinationServerObject, ulong bytesToCopy, CopyToStreamResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.CopyToStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (sourceServerObject == null)
				{
					throw new ArgumentNullException("sourceServerObject");
				}
				if (destinationServerObject == null)
				{
					throw new ArgumentNullException("destinationServerObject");
				}
				ulong bytesRead = 0UL;
				ulong bytesWritten = 0UL;
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U, bytesRead, bytesWritten);
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000048E4 File Offset: 0x00002AE4
		public RopResult CreateAttachment(IServerObject serverObject, CreateAttachmentResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CreateAttachment"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Attachment).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CreateAttachment, RopHandlerBase.CreateAttachmentClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CreateAttachment, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CreateAttachment, mapiMessage);
								ropResult = this.CreateAttachment(this.mapiContext, mapiMessage, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Attachment);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 35U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 35U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 35U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CreateAttachment, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21
		protected abstract RopResult CreateAttachment(MapiContext context, MapiMessage serverObject, CreateAttachmentResultFactory resultFactory);

		// Token: 0x06000016 RID: 22 RVA: 0x00004DEC File Offset: 0x00002FEC
		public RopResult CreateBookmark(IServerObject serverObject, CreateBookmarkResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CreateBookmark"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CreateBookmark, RopHandlerBase.CreateBookmarkClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CreateBookmark, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CreateBookmark, mapiViewTableBase);
								ropResult = this.CreateBookmark(this.mapiContext, mapiViewTableBase, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 27U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 27U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 27U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CreateBookmark, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000017 RID: 23
		protected abstract RopResult CreateBookmark(MapiContext context, MapiViewTableBase serverObject, CreateBookmarkResultFactory resultFactory);

		// Token: 0x06000018 RID: 24 RVA: 0x000052CC File Offset: 0x000034CC
		public RopResult CreateFolder(IServerObject serverObject, FolderType folderType, CreateFolderFlags flags, string displayName, string folderComment, StoreLongTermId? longTermId, CreateFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CreateFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Folder).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CreateFolder, RopHandlerBase.CreateFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CreateFolder, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CreateFolder, mapiFolder);
								ropResult = this.CreateFolder(this.mapiContext, mapiFolder, folderType, flags, displayName, folderComment, longTermId, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Folder);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 28U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 28U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 28U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CreateFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25
		protected abstract RopResult CreateFolder(MapiContext context, MapiFolder serverObject, FolderType folderType, CreateFolderFlags flags, string displayName, string folderComment, StoreLongTermId? longTermId, CreateFolderResultFactory resultFactory);

		// Token: 0x0600001A RID: 26 RVA: 0x000057E8 File Offset: 0x000039E8
		public RopResult CreateMessage(IServerObject serverObject, ushort codePageId, StoreId folderId, bool createAssociated, CreateMessageResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CreateMessage"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Message).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CreateMessage, RopHandlerBase.CreateMessageClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CreateMessage, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CreateMessage, mapiBase);
								ropResult = this.CreateMessage(this.mapiContext, mapiBase, codePageId, RcaTypeHelpers.StoreIdToExchangeId(folderId, logon.StoreMailbox), createAssociated, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Message);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(folderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 6U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 6U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 6U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CreateMessage, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600001B RID: 27
		protected abstract RopResult CreateMessage(MapiContext context, MapiBase serverObject, ushort codePageId, ExchangeId folderId, bool createAssociated, CreateMessageResultFactory resultFactory);

		// Token: 0x0600001C RID: 28 RVA: 0x00005D20 File Offset: 0x00003F20
		public RopResult CreateMessageExtended(IServerObject serverObject, ushort codePageId, StoreId folderId, CreateMessageExtendedFlags createFlags, CreateMessageExtendedResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.CreateMessageExtended"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Message).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.CreateMessageExtended, RopHandlerBase.CreateMessageExtendedClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.CreateMessageExtended, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.CreateMessageExtended, mapiBase);
								ropResult = this.CreateMessageExtended(this.mapiContext, mapiBase, codePageId, RcaTypeHelpers.StoreIdToExchangeId(folderId, logon.StoreMailbox), createFlags, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Message);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(folderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 159U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 159U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 159U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.CreateMessageExtended, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600001D RID: 29
		protected abstract RopResult CreateMessageExtended(MapiContext context, MapiBase serverObject, ushort codePageId, ExchangeId folderId, CreateMessageExtendedFlags createFlags, CreateMessageExtendedResultFactory resultFactory);

		// Token: 0x0600001E RID: 30 RVA: 0x0000625C File Offset: 0x0000445C
		public RopResult DeleteAttachment(IServerObject serverObject, uint attachmentNumber, DeleteAttachmentResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.DeleteAttachment"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = 4 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.DeleteAttachment, RopHandlerBase.DeleteAttachmentClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.DeleteAttachment, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.DeleteAttachment, mapiMessage);
								ropResult = this.DeleteAttachment(this.mapiContext, mapiMessage, attachmentNumber, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 36U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 36U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 36U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.DeleteAttachment, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600001F RID: 31
		protected abstract RopResult DeleteAttachment(MapiContext context, MapiMessage serverObject, uint attachmentNumber, DeleteAttachmentResultFactory resultFactory);

		// Token: 0x06000020 RID: 32 RVA: 0x00006708 File Offset: 0x00004908
		public RopResult DeleteFolder(IServerObject serverObject, DeleteFolderFlags deleteFolderFlags, StoreId folderId, DeleteFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.DeleteFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.DeleteFolder, RopHandlerBase.DeleteFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.DeleteFolder, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.DeleteFolder, mapiFolder);
								ropResult = this.DeleteFolder(this.mapiContext, mapiFolder, deleteFolderFlags, RcaTypeHelpers.StoreIdToExchangeId(folderId, logon.StoreMailbox), resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(folderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 29U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 29U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 29U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.DeleteFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33
		protected abstract RopResult DeleteFolder(MapiContext context, MapiFolder serverObject, DeleteFolderFlags deleteFolderFlags, ExchangeId folderId, DeleteFolderResultFactory resultFactory);

		// Token: 0x06000022 RID: 34 RVA: 0x00006BC0 File Offset: 0x00004DC0
		public RopResult DeleteMessages(IServerObject serverObject, bool reportProgress, bool isOkToSendNonReadNotification, StoreId[] messageIds, DeleteMessagesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.DeleteMessages"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.DeleteMessages, RopHandlerBase.DeleteMessagesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.DeleteMessages, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.DeleteMessages, mapiFolder);
								ropResult = this.DeleteMessages(this.mapiContext, mapiFolder, reportProgress, isOkToSendNonReadNotification, RcaTypeHelpers.StoreIdsToExchangeIds(messageIds, logon.StoreMailbox), out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 30U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 30U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 30U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.DeleteMessages, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000023 RID: 35
		protected abstract RopResult DeleteMessages(MapiContext context, MapiFolder serverObject, bool reportProgress, bool isOkToSendNonReadNotification, ExchangeId[] messageIds, out bool outputIsPartiallyCompleted, DeleteMessagesResultFactory resultFactory);

		// Token: 0x06000024 RID: 36 RVA: 0x00007074 File Offset: 0x00005274
		public RopResult DeleteProperties(IServerObject serverObject, PropertyTag[] propertyTags, DeletePropertiesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.DeleteProperties"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsDeletePropertiesSharedMailboxOperation(mapiPropBagBase);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.DeleteProperties, RopHandlerBase.DeletePropertiesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.DeleteProperties, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.DeleteProperties, mapiPropBagBase);
								ropResult = this.DeleteProperties(this.mapiContext, mapiPropBagBase, propertyTags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 11U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 11U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 11U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.DeleteProperties, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000025 RID: 37
		protected abstract RopResult DeleteProperties(MapiContext context, MapiPropBagBase serverObject, PropertyTag[] propertyTags, DeletePropertiesResultFactory resultFactory);

		// Token: 0x06000026 RID: 38 RVA: 0x00007524 File Offset: 0x00005724
		public RopResult DeletePropertiesNoReplicate(IServerObject serverObject, PropertyTag[] propertyTags, DeletePropertiesNoReplicateResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.DeletePropertiesNoReplicate"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsDeletePropertiesNoReplicateSharedMailboxOperation(mapiPropBagBase);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.DeletePropertiesNoReplicate, RopHandlerBase.DeletePropertiesNoReplicateClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.DeletePropertiesNoReplicate, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.DeletePropertiesNoReplicate, mapiPropBagBase);
								ropResult = this.DeletePropertiesNoReplicate(this.mapiContext, mapiPropBagBase, propertyTags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 122U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 122U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 122U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.DeletePropertiesNoReplicate, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000027 RID: 39
		protected abstract RopResult DeletePropertiesNoReplicate(MapiContext context, MapiPropBagBase serverObject, PropertyTag[] propertyTags, DeletePropertiesNoReplicateResultFactory resultFactory);

		// Token: 0x06000028 RID: 40 RVA: 0x000079D4 File Offset: 0x00005BD4
		public RopResult EchoBinary(byte[] inputParameter, EchoBinaryResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.EchoBinary"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				result = resultFactory.CreateSuccessfulResult(0, inputParameter);
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00007A28 File Offset: 0x00005C28
		public RopResult EchoInt(int inputParameter, EchoIntResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.EchoInt"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				result = resultFactory.CreateSuccessfulResult(0, inputParameter);
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00007A7C File Offset: 0x00005C7C
		public RopResult EchoString(string inputParameter, EchoStringResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.EchoString"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				result = resultFactory.CreateSuccessfulResult(string.Empty, inputParameter);
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public RopResult EmptyFolder(IServerObject serverObject, bool reportProgress, EmptyFolderFlags emptyFolderFlags, EmptyFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.EmptyFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.EmptyFolder, RopHandlerBase.EmptyFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.EmptyFolder, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.EmptyFolder, mapiFolder);
								ropResult = this.EmptyFolder(this.mapiContext, mapiFolder, reportProgress, emptyFolderFlags, out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 88U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 88U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 88U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.EmptyFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600002C RID: 44
		protected abstract RopResult EmptyFolder(MapiContext context, MapiFolder serverObject, bool reportProgress, EmptyFolderFlags emptyFolderFlags, out bool outputIsPartiallyCompleted, EmptyFolderResultFactory resultFactory);

		// Token: 0x0600002D RID: 45 RVA: 0x00007F7C File Offset: 0x0000617C
		public RopResult ExpandRow(IServerObject serverObject, short maxRows, StoreId categoryId, ExpandRowResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ExpandRow"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ExpandRow, RopHandlerBase.ExpandRowClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ExpandRow, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ExpandRow, mapiViewTableBase);
								ropResult = this.ExpandRow(this.mapiContext, mapiViewTableBase, maxRows, RcaTypeHelpers.StoreIdToExchangeId(categoryId, logon.StoreMailbox), resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 89U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 89U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 89U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ExpandRow, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600002E RID: 46
		protected abstract RopResult ExpandRow(MapiContext context, MapiViewTableBase serverObject, short maxRows, ExchangeId categoryId, ExpandRowResultFactory resultFactory);

		// Token: 0x0600002F RID: 47 RVA: 0x0000846C File Offset: 0x0000666C
		public RopResult FastTransferDestinationCopyOperationConfigure(IServerObject serverObject, FastTransferCopyOperation copyOperation, FastTransferCopyPropertiesFlag flags, FastTransferDestinationCopyOperationConfigureResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferDestinationCopyOperationConfigure"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.FastTransferDestination).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferDestinationCopyOperationConfigure, RopHandlerBase.FastTransferDestinationCopyOperationConfigureClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferDestinationCopyOperationConfigure, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferDestinationCopyOperationConfigure, mapiPropBagBase);
								ropResult = this.FastTransferDestinationCopyOperationConfigure(this.mapiContext, mapiPropBagBase, copyOperation, flags, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.FastTransferDestination);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 83U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 83U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 83U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferDestinationCopyOperationConfigure, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000030 RID: 48
		protected abstract RopResult FastTransferDestinationCopyOperationConfigure(MapiContext context, MapiPropBagBase serverObject, FastTransferCopyOperation copyOperation, FastTransferCopyPropertiesFlag flags, FastTransferDestinationCopyOperationConfigureResultFactory resultFactory);

		// Token: 0x06000031 RID: 49 RVA: 0x0000896C File Offset: 0x00006B6C
		public RopResult FastTransferDestinationPutBuffer(IServerObject serverObject, ArraySegment<byte>[] dataChunks, FastTransferDestinationPutBufferResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferDestinationPutBuffer"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				ushort progressCount = 0;
				ushort totalStepCount = 0;
				bool moveUserOperation = false;
				ushort usedBufferSize = 0;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferDestinationPutBuffer, RopHandlerBase.FastTransferDestinationPutBufferClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferDestinationPutBuffer, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferDestinationPutBuffer, mapiBase);
								ropResult = this.FastTransferDestinationPutBuffer(this.mapiContext, mapiBase, dataChunks, out progressCount, out totalStepCount, out moveUserOperation, out usedBufferSize, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 84U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 84U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 84U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferDestinationPutBuffer, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, progressCount, totalStepCount, moveUserOperation, usedBufferSize);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000032 RID: 50
		protected abstract RopResult FastTransferDestinationPutBuffer(MapiContext context, MapiBase serverObject, ArraySegment<byte>[] dataChunks, out ushort outputProgress, out ushort outputSteps, out bool outputIsMoveUser, out ushort outputUsedBufferSize, FastTransferDestinationPutBufferResultFactory resultFactory);

		// Token: 0x06000033 RID: 51 RVA: 0x00008E24 File Offset: 0x00007024
		public RopResult FastTransferDestinationPutBufferExtended(IServerObject serverObject, ArraySegment<byte>[] dataChunks, FastTransferDestinationPutBufferExtendedResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferDestinationPutBufferExtended"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				uint progressCount = 0U;
				uint totalStepCount = 0U;
				bool moveUserOperation = false;
				ushort usedBufferSize = 0;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferDestinationPutBufferExtended, RopHandlerBase.FastTransferDestinationPutBufferExtendedClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferDestinationPutBufferExtended, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferDestinationPutBufferExtended, mapiBase);
								ropResult = this.FastTransferDestinationPutBufferExtended(this.mapiContext, mapiBase, dataChunks, out progressCount, out totalStepCount, out moveUserOperation, out usedBufferSize, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 157U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 157U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 157U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferDestinationPutBufferExtended, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, progressCount, totalStepCount, moveUserOperation, usedBufferSize);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000034 RID: 52
		protected abstract RopResult FastTransferDestinationPutBufferExtended(MapiContext context, MapiBase serverObject, ArraySegment<byte>[] dataChunks, out uint outputProgress, out uint outputSteps, out bool outputIsMoveUser, out ushort outputUsedBufferSize, FastTransferDestinationPutBufferExtendedResultFactory resultFactory);

		// Token: 0x06000035 RID: 53 RVA: 0x00009308 File Offset: 0x00007508
		public RopResult FastTransferGetIncrementalState(IServerObject serverObject, FastTransferGetIncrementalStateResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferGetIncrementalState"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.FastTransferSource).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferGetIncrementalState, RopHandlerBase.FastTransferGetIncrementalStateClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferGetIncrementalState, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferGetIncrementalState, mapiBase);
								ropResult = this.FastTransferGetIncrementalState(this.mapiContext, mapiBase, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.FastTransferSource);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 130U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 130U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 130U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferGetIncrementalState, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000036 RID: 54
		protected abstract RopResult FastTransferGetIncrementalState(MapiContext context, MapiBase serverObject, FastTransferGetIncrementalStateResultFactory resultFactory);

		// Token: 0x06000037 RID: 55 RVA: 0x00009830 File Offset: 0x00007A30
		public RopResult FastTransferSourceCopyFolder(IServerObject serverObject, FastTransferCopyFolderFlag flags, FastTransferSendOption sendOptions, FastTransferSourceCopyFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferSourceCopyFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.FastTransferSource).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferSourceCopyFolder, RopHandlerBase.FastTransferSourceCopyFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferSourceCopyFolder, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferSourceCopyFolder, mapiFolder);
								ropResult = this.FastTransferSourceCopyFolder(this.mapiContext, mapiFolder, flags, sendOptions, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.FastTransferSource);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 76U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 76U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 76U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferSourceCopyFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000038 RID: 56
		protected abstract RopResult FastTransferSourceCopyFolder(MapiContext context, MapiFolder serverObject, FastTransferCopyFolderFlag flags, FastTransferSendOption sendOptions, FastTransferSourceCopyFolderResultFactory resultFactory);

		// Token: 0x06000039 RID: 57 RVA: 0x00009D48 File Offset: 0x00007F48
		public RopResult FastTransferSourceCopyMessages(IServerObject serverObject, StoreId[] messageIds, FastTransferCopyMessagesFlag flags, FastTransferSendOption sendOptions, FastTransferSourceCopyMessagesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferSourceCopyMessages"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.FastTransferSource).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferSourceCopyMessages, RopHandlerBase.FastTransferSourceCopyMessagesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferSourceCopyMessages, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferSourceCopyMessages, mapiFolder);
								ropResult = this.FastTransferSourceCopyMessages(this.mapiContext, mapiFolder, RcaTypeHelpers.StoreIdsToExchangeIds(messageIds, logon.StoreMailbox), flags, sendOptions, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.FastTransferSource);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 75U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 75U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 75U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferSourceCopyMessages, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600003A RID: 58
		protected abstract RopResult FastTransferSourceCopyMessages(MapiContext context, MapiFolder serverObject, ExchangeId[] messageIds, FastTransferCopyMessagesFlag flags, FastTransferSendOption sendOptions, FastTransferSourceCopyMessagesResultFactory resultFactory);

		// Token: 0x0600003B RID: 59 RVA: 0x0000A270 File Offset: 0x00008470
		public RopResult FastTransferSourceCopyProperties(IServerObject serverObject, byte level, FastTransferCopyPropertiesFlag flags, FastTransferSendOption sendOptions, PropertyTag[] propertyTags, FastTransferSourceCopyPropertiesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferSourceCopyProperties"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.FastTransferSource).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferSourceCopyProperties, RopHandlerBase.FastTransferSourceCopyPropertiesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferSourceCopyProperties, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferSourceCopyProperties, mapiPropBagBase);
								ropResult = this.FastTransferSourceCopyProperties(this.mapiContext, mapiPropBagBase, level, flags, sendOptions, propertyTags, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.FastTransferSource);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 105U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 105U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 105U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferSourceCopyProperties, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600003C RID: 60
		protected abstract RopResult FastTransferSourceCopyProperties(MapiContext context, MapiPropBagBase serverObject, byte level, FastTransferCopyPropertiesFlag flags, FastTransferSendOption sendOptions, PropertyTag[] propertyTags, FastTransferSourceCopyPropertiesResultFactory resultFactory);

		// Token: 0x0600003D RID: 61 RVA: 0x0000A78C File Offset: 0x0000898C
		public RopResult FastTransferSourceCopyTo(IServerObject serverObject, byte level, FastTransferCopyFlag flags, FastTransferSendOption sendOptions, PropertyTag[] excludedPropertyTags, FastTransferSourceCopyToResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferSourceCopyTo"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.FastTransferSource).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferSourceCopyTo, RopHandlerBase.FastTransferSourceCopyToClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferSourceCopyTo, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferSourceCopyTo, mapiPropBagBase);
								ropResult = this.FastTransferSourceCopyTo(this.mapiContext, mapiPropBagBase, level, flags, sendOptions, excludedPropertyTags, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.FastTransferSource);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 77U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 77U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 77U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferSourceCopyTo, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600003E RID: 62
		protected abstract RopResult FastTransferSourceCopyTo(MapiContext context, MapiPropBagBase serverObject, byte level, FastTransferCopyFlag flags, FastTransferSendOption sendOptions, PropertyTag[] excludedPropertyTags, FastTransferSourceCopyToResultFactory resultFactory);

		// Token: 0x0600003F RID: 63 RVA: 0x0000AC90 File Offset: 0x00008E90
		public RopResult FastTransferSourceGetBuffer(IServerObject serverObject, ushort bufferSize, FastTransferSourceGetBufferResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferSourceGetBuffer"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferSourceGetBuffer, RopHandlerBase.FastTransferSourceGetBufferClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferSourceGetBuffer, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferSourceGetBuffer, mapiBase);
								ropResult = this.FastTransferSourceGetBuffer(this.mapiContext, mapiBase, bufferSize, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 78U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 78U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 78U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferSourceGetBuffer, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000040 RID: 64
		protected abstract RopResult FastTransferSourceGetBuffer(MapiContext context, MapiBase serverObject, ushort bufferSize, FastTransferSourceGetBufferResultFactory resultFactory);

		// Token: 0x06000041 RID: 65 RVA: 0x0000B12C File Offset: 0x0000932C
		public RopResult FastTransferSourceGetBufferExtended(IServerObject serverObject, ushort bufferSize, FastTransferSourceGetBufferExtendedResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FastTransferSourceGetBufferExtended"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FastTransferSourceGetBufferExtended, RopHandlerBase.FastTransferSourceGetBufferExtendedClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FastTransferSourceGetBufferExtended, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FastTransferSourceGetBufferExtended, mapiBase);
								ropResult = this.FastTransferSourceGetBufferExtended(this.mapiContext, mapiBase, bufferSize, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 156U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 156U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 156U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FastTransferSourceGetBufferExtended, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000042 RID: 66
		protected abstract RopResult FastTransferSourceGetBufferExtended(MapiContext context, MapiBase serverObject, ushort bufferSize, FastTransferSourceGetBufferExtendedResultFactory resultFactory);

		// Token: 0x06000043 RID: 67 RVA: 0x0000B5DC File Offset: 0x000097DC
		public RopResult FindRow(IServerObject serverObject, FindRowFlags flags, Restriction restriction, BookmarkOrigin bookmarkOrigin, byte[] bookmark, FindRowResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FindRow"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FindRow, RopHandlerBase.FindRowClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FindRow, RopHandlerBase.FindRowClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FindRow, mapiViewTableBase);
								ropResult = this.FindRow(this.mapiContext, mapiViewTableBase, flags, restriction, bookmarkOrigin, bookmark, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 79U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 79U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 79U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FindRow, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000044 RID: 68
		protected abstract RopResult FindRow(MapiContext context, MapiViewTableBase serverObject, FindRowFlags flags, Restriction restriction, BookmarkOrigin bookmarkOrigin, byte[] bookmark, FindRowResultFactory resultFactory);

		// Token: 0x06000045 RID: 69 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		public RopResult FlushRecipients(IServerObject serverObject, PropertyTag[] extraPropertyTags, RecipientRow[] recipientRows, FlushRecipientsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FlushRecipients"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FlushRecipients, RopHandlerBase.FlushRecipientsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FlushRecipients, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FlushRecipients, mapiMessage);
								ropResult = this.FlushRecipients(this.mapiContext, mapiMessage, extraPropertyTags, recipientRows, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 14U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 14U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 14U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FlushRecipients, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000046 RID: 70
		protected abstract RopResult FlushRecipients(MapiContext context, MapiMessage serverObject, PropertyTag[] extraPropertyTags, RecipientRow[] recipientRows, FlushRecipientsResultFactory resultFactory);

		// Token: 0x06000047 RID: 71 RVA: 0x0000BF60 File Offset: 0x0000A160
		public RopResult FreeBookmark(IServerObject serverObject, byte[] bookmark, FreeBookmarkResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.FreeBookmark"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.FreeBookmark, RopHandlerBase.FreeBookmarkClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.FreeBookmark, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.FreeBookmark, mapiViewTableBase);
								ropResult = this.FreeBookmark(this.mapiContext, mapiViewTableBase, bookmark, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 137U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 137U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 137U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.FreeBookmark, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000048 RID: 72
		protected abstract RopResult FreeBookmark(MapiContext context, MapiViewTableBase serverObject, byte[] bookmark, FreeBookmarkResultFactory resultFactory);

		// Token: 0x06000049 RID: 73 RVA: 0x0000C440 File Offset: 0x0000A640
		public RopResult GetAllPerUserLongTermIds(IServerObject serverObject, StoreLongTermId startId, GetAllPerUserLongTermIdsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetAllPerUserLongTermIds"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = 3 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetAllPerUserLongTermIds, RopHandlerBase.GetAllPerUserLongTermIdsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetAllPerUserLongTermIds, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetAllPerUserLongTermIds, mapiLogon);
								ropResult = this.GetAllPerUserLongTermIds(this.mapiContext, mapiLogon, startId, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 125U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 125U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 125U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetAllPerUserLongTermIds, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600004A RID: 74
		protected abstract RopResult GetAllPerUserLongTermIds(MapiContext context, MapiLogon serverObject, StoreLongTermId startId, GetAllPerUserLongTermIdsResultFactory resultFactory);

		// Token: 0x0600004B RID: 75 RVA: 0x0000C904 File Offset: 0x0000AB04
		public RopResult GetAttachmentTable(IServerObject serverObject, TableFlags tableFlags, GetAttachmentTableResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetAttachmentTable"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.AttachmentView).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetAttachmentTable, RopHandlerBase.GetAttachmentTableClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetAttachmentTable, RopHandlerBase.GetAttachmentTableClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetAttachmentTable, mapiMessage);
								ropResult = this.GetAttachmentTable(this.mapiContext, mapiMessage, tableFlags, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.AttachmentView);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 33U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 33U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 33U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetAttachmentTable, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600004C RID: 76
		protected abstract RopResult GetAttachmentTable(MapiContext context, MapiMessage serverObject, TableFlags tableFlags, GetAttachmentTableResultFactory resultFactory);

		// Token: 0x0600004D RID: 77 RVA: 0x0000CE04 File Offset: 0x0000B004
		public RopResult GetCollapseState(IServerObject serverObject, StoreId rowId, uint rowInstanceNumber, GetCollapseStateResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetCollapseState"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetCollapseState, RopHandlerBase.GetCollapseStateClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetCollapseState, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetCollapseState, mapiViewTableBase);
								ropResult = this.GetCollapseState(this.mapiContext, mapiViewTableBase, RcaTypeHelpers.StoreIdToExchangeId(rowId, logon.StoreMailbox), rowInstanceNumber, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 107U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 107U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 107U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetCollapseState, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600004E RID: 78
		protected abstract RopResult GetCollapseState(MapiContext context, MapiViewTableBase serverObject, ExchangeId rowId, uint rowInstanceNumber, GetCollapseStateResultFactory resultFactory);

		// Token: 0x0600004F RID: 79 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		public RopResult GetContentsTable(IServerObject serverObject, TableFlags tableFlags, GetContentsTableResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetContentsTable"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.MessageView).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetContentsTable, RopHandlerBase.GetContentsTableClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetContentsTable, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetContentsTable, mapiFolder);
								ropResult = this.GetContentsTable(this.mapiContext, mapiFolder, tableFlags, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.MessageView);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 5U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 5U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 5U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetContentsTable, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000050 RID: 80
		protected abstract RopResult GetContentsTable(MapiContext context, MapiFolder serverObject, TableFlags tableFlags, GetContentsTableResultFactory resultFactory);

		// Token: 0x06000051 RID: 81 RVA: 0x0000D800 File Offset: 0x0000BA00
		public RopResult GetContentsTableExtended(IServerObject serverObject, ExtendedTableFlags extendedTableFlags, GetContentsTableExtendedResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetContentsTableExtended"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.MessageView).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetContentsTableExtended, RopHandlerBase.GetContentsTableExtendedClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetContentsTableExtended, RopHandlerBase.GetContentsTableExtendedClientTypesAllowedOnReadOnlyDatabase);
									RopHandlerBase.CheckGetContentsTableExtendedConditionsForReadOnlyDatabase(this.mapiContext, mapiFolder, extendedTableFlags);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetContentsTableExtended, mapiFolder);
								ropResult = this.GetContentsTableExtended(this.mapiContext, mapiFolder, extendedTableFlags, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.MessageView);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 164U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 164U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 164U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetContentsTableExtended, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000052 RID: 82
		protected abstract RopResult GetContentsTableExtended(MapiContext context, MapiFolder serverObject, ExtendedTableFlags extendedTableFlags, GetContentsTableExtendedResultFactory resultFactory);

		// Token: 0x06000053 RID: 83 RVA: 0x0000DD20 File Offset: 0x0000BF20
		public RopResult GetEffectiveRights(IServerObject serverObject, byte[] addressBookId, StoreId folderId, GetEffectiveRightsResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.GetEffectiveRights"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000DD9C File Offset: 0x0000BF9C
		public RopResult GetHierarchyTable(IServerObject serverObject, TableFlags tableFlags, GetHierarchyTableResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetHierarchyTable"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.FolderView).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetHierarchyTable, RopHandlerBase.GetHierarchyTableClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetHierarchyTable, RopHandlerBase.GetHierarchyTableClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetHierarchyTable, mapiFolder);
								ropResult = this.GetHierarchyTable(this.mapiContext, mapiFolder, tableFlags, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.FolderView);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 4U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 4U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 4U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetHierarchyTable, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000055 RID: 85
		protected abstract RopResult GetHierarchyTable(MapiContext context, MapiFolder serverObject, TableFlags tableFlags, GetHierarchyTableResultFactory resultFactory);

		// Token: 0x06000056 RID: 86 RVA: 0x0000E294 File Offset: 0x0000C494
		public RopResult GetIdsFromNames(IServerObject serverObject, GetIdsFromNamesFlags flags, NamedProperty[] namedProperties, GetIdsFromNamesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetIdsFromNames"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetIdsFromNames, RopHandlerBase.GetIdsFromNamesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetIdsFromNames, RopHandlerBase.GetIdsFromNamesClientTypesAllowedOnReadOnlyDatabase);
									RopHandlerBase.CheckGetIdsFromNamesConditionsForReadOnlyDatabase(this.mapiContext, mapiBase, flags, namedProperties);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetIdsFromNames, mapiBase);
								ropResult = this.GetIdsFromNames(this.mapiContext, mapiBase, flags, namedProperties, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 86U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 86U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 86U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetIdsFromNames, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000057 RID: 87
		protected abstract RopResult GetIdsFromNames(MapiContext context, MapiBase serverObject, GetIdsFromNamesFlags flags, NamedProperty[] namedProperties, GetIdsFromNamesResultFactory resultFactory);

		// Token: 0x06000058 RID: 88 RVA: 0x0000E744 File Offset: 0x0000C944
		public RopResult GetLocalReplicationIds(IServerObject serverObject, uint idCount, GetLocalReplicationIdsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetLocalReplicationIds"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = 3 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetLocalReplicationIds, RopHandlerBase.GetLocalReplicationIdsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetLocalReplicationIds, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetLocalReplicationIds, mapiLogon);
								ropResult = this.GetLocalReplicationIds(this.mapiContext, mapiLogon, idCount, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 127U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 127U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 127U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetLocalReplicationIds, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000059 RID: 89
		protected abstract RopResult GetLocalReplicationIds(MapiContext context, MapiLogon serverObject, uint idCount, GetLocalReplicationIdsResultFactory resultFactory);

		// Token: 0x0600005A RID: 90 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public RopResult GetMessageStatus(IServerObject serverObject, StoreId messageId, GetMessageStatusResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetMessageStatus"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetMessageStatus, RopHandlerBase.GetMessageStatusClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetMessageStatus, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetMessageStatus, mapiFolder);
								ropResult = this.GetMessageStatus(this.mapiContext, mapiFolder, RcaTypeHelpers.StoreIdToExchangeId(messageId, logon.StoreMailbox), resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnMid(messageId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 31U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 31U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 31U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetMessageStatus, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600005B RID: 91
		protected abstract RopResult GetMessageStatus(MapiContext context, MapiFolder serverObject, ExchangeId messageId, GetMessageStatusResultFactory resultFactory);

		// Token: 0x0600005C RID: 92 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		public RopResult GetNamesFromIDs(IServerObject serverObject, PropertyId[] propertyIds, GetNamesFromIDsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetNamesFromIDs"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetNamesFromIDs, RopHandlerBase.GetNamesFromIDsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetNamesFromIDs, RopHandlerBase.GetNamesFromIDsClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetNamesFromIDs, mapiBase);
								ropResult = this.GetNamesFromIDs(this.mapiContext, mapiBase, propertyIds, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 85U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 85U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 85U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetNamesFromIDs, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600005D RID: 93
		protected abstract RopResult GetNamesFromIDs(MapiContext context, MapiBase serverObject, PropertyId[] propertyIds, GetNamesFromIDsResultFactory resultFactory);

		// Token: 0x0600005E RID: 94 RVA: 0x0000F554 File Offset: 0x0000D754
		public RopResult GetOptionsData(IServerObject serverObject, string addressType, bool wantWin32, GetOptionsDataResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.GetOptionsData"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		public RopResult GetOwningServers(IServerObject serverObject, StoreId folderId, GetOwningServersResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.GetOwningServers"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000F61C File Offset: 0x0000D81C
		public RopResult GetPermissionsTable(IServerObject serverObject, TableFlags tableFlags, GetPermissionsTableResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.GetPermissionsTable"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000F680 File Offset: 0x0000D880
		public RopResult GetPerUserGuid(IServerObject serverObject, StoreLongTermId publicFolderLongTermId, GetPerUserGuidResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetPerUserGuid"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = 3 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetPerUserGuid, RopHandlerBase.GetPerUserGuidClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetPerUserGuid, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetPerUserGuid, mapiLogon);
								ropResult = this.GetPerUserGuid(this.mapiContext, mapiLogon, publicFolderLongTermId, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 97U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 97U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 97U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetPerUserGuid, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000062 RID: 98
		protected abstract RopResult GetPerUserGuid(MapiContext context, MapiLogon serverObject, StoreLongTermId publicFolderLongTermId, GetPerUserGuidResultFactory resultFactory);

		// Token: 0x06000063 RID: 99 RVA: 0x0000FB2C File Offset: 0x0000DD2C
		public RopResult GetPerUserLongTermIds(IServerObject serverObject, Guid databaseGuid, GetPerUserLongTermIdsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetPerUserLongTermIds"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = 3 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetPerUserLongTermIds, RopHandlerBase.GetPerUserLongTermIdsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetPerUserLongTermIds, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetPerUserLongTermIds, mapiLogon);
								ropResult = this.GetPerUserLongTermIds(this.mapiContext, mapiLogon, databaseGuid, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 96U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 96U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 96U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetPerUserLongTermIds, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000064 RID: 100
		protected abstract RopResult GetPerUserLongTermIds(MapiContext context, MapiLogon serverObject, Guid databaseGuid, GetPerUserLongTermIdsResultFactory resultFactory);

		// Token: 0x06000065 RID: 101 RVA: 0x0000FFD8 File Offset: 0x0000E1D8
		public RopResult GetPropertiesAll(IServerObject serverObject, ushort streamLimit, GetPropertiesFlags flags, GetPropertiesAllResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetPropertiesAll"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = this.IsGetPropertiesAllSharedMailboxOperation(mapiPropBagBase);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetPropertiesAll, RopHandlerBase.GetPropertiesAllClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetPropertiesAll, RopHandlerBase.GetPropertiesAllClientTypesAllowedOnReadOnlyDatabase);
									RopHandlerBase.CheckGetPropertiesAllConditionsForReadOnlyDatabase(this.mapiContext, mapiPropBagBase, streamLimit, flags);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetPropertiesAll, mapiPropBagBase);
								ropResult = this.GetPropertiesAll(this.mapiContext, mapiPropBagBase, streamLimit, flags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 8U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 8U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 8U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetPropertiesAll, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000066 RID: 102
		protected abstract RopResult GetPropertiesAll(MapiContext context, MapiPropBagBase serverObject, ushort streamLimit, GetPropertiesFlags flags, GetPropertiesAllResultFactory resultFactory);

		// Token: 0x06000067 RID: 103 RVA: 0x00010488 File Offset: 0x0000E688
		public RopResult GetPropertiesSpecific(IServerObject serverObject, ushort streamLimit, GetPropertiesFlags flags, PropertyTag[] propertyTags, GetPropertiesSpecificResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetPropertiesSpecific"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = this.IsGetPropertiesSpecificSharedMailboxOperation(mapiPropBagBase, streamLimit, flags, propertyTags);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetPropertiesSpecific, RopHandlerBase.GetPropertiesSpecificClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetPropertiesSpecific, RopHandlerBase.GetPropertiesSpecificClientTypesAllowedOnReadOnlyDatabase);
									RopHandlerBase.CheckGetPropertiesSpecificConditionsForReadOnlyDatabase(this.mapiContext, mapiPropBagBase, streamLimit, flags, propertyTags);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetPropertiesSpecific, mapiPropBagBase);
								ropResult = this.GetPropertiesSpecific(this.mapiContext, mapiPropBagBase, streamLimit, flags, propertyTags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 7U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 7U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 7U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetPropertiesSpecific, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000068 RID: 104
		protected abstract RopResult GetPropertiesSpecific(MapiContext context, MapiPropBagBase serverObject, ushort streamLimit, GetPropertiesFlags flags, PropertyTag[] propertyTags, GetPropertiesSpecificResultFactory resultFactory);

		// Token: 0x06000069 RID: 105 RVA: 0x00010940 File Offset: 0x0000EB40
		public RopResult GetPropertyList(IServerObject serverObject, GetPropertyListResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetPropertyList"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetPropertyList, RopHandlerBase.GetPropertyListClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetPropertyList, RopHandlerBase.GetPropertyListClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetPropertyList, mapiPropBagBase);
								ropResult = this.GetPropertyList(this.mapiContext, mapiPropBagBase, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 9U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 9U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 9U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetPropertyList, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600006A RID: 106
		protected abstract RopResult GetPropertyList(MapiContext context, MapiPropBagBase serverObject, GetPropertyListResultFactory resultFactory);

		// Token: 0x0600006B RID: 107 RVA: 0x00010DE0 File Offset: 0x0000EFE0
		public RopResult GetReceiveFolder(IServerObject serverObject, string messageClass, GetReceiveFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetReceiveFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetReceiveFolder, RopHandlerBase.GetReceiveFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetReceiveFolder, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetReceiveFolder, mapiLogon);
								ropResult = this.GetReceiveFolder(this.mapiContext, mapiLogon, messageClass, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 39U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 39U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 39U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetReceiveFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600006C RID: 108
		protected abstract RopResult GetReceiveFolder(MapiContext context, MapiLogon serverObject, string messageClass, GetReceiveFolderResultFactory resultFactory);

		// Token: 0x0600006D RID: 109 RVA: 0x0001128C File Offset: 0x0000F48C
		public RopResult GetReceiveFolderTable(IServerObject serverObject, GetReceiveFolderTableResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetReceiveFolderTable"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetReceiveFolderTable, RopHandlerBase.GetReceiveFolderTableClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetReceiveFolderTable, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetReceiveFolderTable, mapiBase);
								ropResult = this.GetReceiveFolderTable(this.mapiContext, mapiBase, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 104U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 104U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 104U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetReceiveFolderTable, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600006E RID: 110
		protected abstract RopResult GetReceiveFolderTable(MapiContext context, MapiBase serverObject, GetReceiveFolderTableResultFactory resultFactory);

		// Token: 0x0600006F RID: 111 RVA: 0x00011734 File Offset: 0x0000F934
		public RopResult GetRulesTable(IServerObject serverObject, TableFlags tableFlags, GetRulesTableResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.GetRulesTable"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00011798 File Offset: 0x0000F998
		public RopResult GetSearchCriteria(IServerObject serverObject, GetSearchCriteriaFlags flags, GetSearchCriteriaResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetSearchCriteria"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetSearchCriteria, RopHandlerBase.GetSearchCriteriaClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetSearchCriteria, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetSearchCriteria, mapiFolder);
								ropResult = this.GetSearchCriteria(this.mapiContext, mapiFolder, flags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 49U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 49U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 49U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetSearchCriteria, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000071 RID: 113
		protected abstract RopResult GetSearchCriteria(MapiContext context, MapiFolder serverObject, GetSearchCriteriaFlags flags, GetSearchCriteriaResultFactory resultFactory);

		// Token: 0x06000072 RID: 114 RVA: 0x00011C44 File Offset: 0x0000FE44
		public RopResult GetStatus(IServerObject serverObject, GetStatusResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.GetStatus"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		public RopResult GetStoreState(IServerObject serverObject, GetStoreStateResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.GetStoreState"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00011D0C File Offset: 0x0000FF0C
		public RopResult GetStreamSize(IServerObject serverObject, GetStreamSizeResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.GetStreamSize"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = 2 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.GetStreamSize, RopHandlerBase.GetStreamSizeClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.GetStreamSize, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.GetStreamSize, mapiStream);
								ropResult = this.GetStreamSize(this.mapiContext, mapiStream, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 94U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 94U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 94U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.GetStreamSize, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000075 RID: 117
		protected abstract RopResult GetStreamSize(MapiContext context, MapiStream serverObject, GetStreamSizeResultFactory resultFactory);

		// Token: 0x06000076 RID: 118 RVA: 0x000121B4 File Offset: 0x000103B4
		public RopResult HardDeleteMessages(IServerObject serverObject, bool reportProgress, bool isOkToSendNonReadNotification, StoreId[] messageIds, HardDeleteMessagesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.HardDeleteMessages"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.HardDeleteMessages, RopHandlerBase.HardDeleteMessagesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.HardDeleteMessages, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.HardDeleteMessages, mapiFolder);
								ropResult = this.HardDeleteMessages(this.mapiContext, mapiFolder, reportProgress, isOkToSendNonReadNotification, RcaTypeHelpers.StoreIdsToExchangeIds(messageIds, logon.StoreMailbox), out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 145U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 145U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 145U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.HardDeleteMessages, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000077 RID: 119
		protected abstract RopResult HardDeleteMessages(MapiContext context, MapiFolder serverObject, bool reportProgress, bool isOkToSendNonReadNotification, ExchangeId[] messageIds, out bool outputIsPartiallyCompleted, HardDeleteMessagesResultFactory resultFactory);

		// Token: 0x06000078 RID: 120 RVA: 0x0001267C File Offset: 0x0001087C
		public RopResult HardEmptyFolder(IServerObject serverObject, bool reportProgress, EmptyFolderFlags emptyFolderFlags, HardEmptyFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.HardEmptyFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.HardEmptyFolder, RopHandlerBase.HardEmptyFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.HardEmptyFolder, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.HardEmptyFolder, mapiFolder);
								ropResult = this.HardEmptyFolder(this.mapiContext, mapiFolder, reportProgress, emptyFolderFlags, out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 146U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 146U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 146U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.HardEmptyFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000079 RID: 121
		protected abstract RopResult HardEmptyFolder(MapiContext context, MapiFolder serverObject, bool reportProgress, EmptyFolderFlags emptyFolderFlags, out bool outputIsPartiallyCompleted, HardEmptyFolderResultFactory resultFactory);

		// Token: 0x0600007A RID: 122 RVA: 0x00012B38 File Offset: 0x00010D38
		public RopResult IdFromLongTermId(IServerObject serverObject, StoreLongTermId longTermId, IdFromLongTermIdResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.IdFromLongTermId"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.IdFromLongTermId, RopHandlerBase.IdFromLongTermIdClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.IdFromLongTermId, RopHandlerBase.IdFromLongTermIdClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.IdFromLongTermId, mapiBase);
								ropResult = this.IdFromLongTermId(this.mapiContext, mapiBase, longTermId, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 68U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 68U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 68U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.IdFromLongTermId, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600007B RID: 123
		protected abstract RopResult IdFromLongTermId(MapiContext context, MapiBase serverObject, StoreLongTermId longTermId, IdFromLongTermIdResultFactory resultFactory);

		// Token: 0x0600007C RID: 124 RVA: 0x00012FD8 File Offset: 0x000111D8
		public RopResult ImportDelete(IServerObject serverObject, ImportDeleteFlags importDeleteFlags, PropertyValue[] deleteChanges, ImportDeleteResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ImportDelete"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				IcsUploadContext icsUploadContext = serverObject as IcsUploadContext;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (icsUploadContext == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = icsUploadContext;
					MapiLogon logon = icsUploadContext.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ImportDelete, RopHandlerBase.ImportDeleteClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ImportDelete, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ImportDelete, icsUploadContext);
								ropResult = this.ImportDelete(this.mapiContext, icsUploadContext, importDeleteFlags, deleteChanges, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 116U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 116U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 116U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ImportDelete, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600007D RID: 125
		protected abstract RopResult ImportDelete(MapiContext context, IcsUploadContext serverObject, ImportDeleteFlags importDeleteFlags, PropertyValue[] deleteChanges, ImportDeleteResultFactory resultFactory);

		// Token: 0x0600007E RID: 126 RVA: 0x00013478 File Offset: 0x00011678
		public RopResult ImportHierarchyChange(IServerObject serverObject, PropertyValue[] hierarchyPropertyValues, PropertyValue[] folderPropertyValues, ImportHierarchyChangeResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ImportHierarchyChange"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				IcsHierarchyUploadContext icsHierarchyUploadContext = serverObject as IcsHierarchyUploadContext;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (icsHierarchyUploadContext == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = icsHierarchyUploadContext;
					MapiLogon logon = icsHierarchyUploadContext.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ImportHierarchyChange, RopHandlerBase.ImportHierarchyChangeClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ImportHierarchyChange, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ImportHierarchyChange, icsHierarchyUploadContext);
								ropResult = this.ImportHierarchyChange(this.mapiContext, icsHierarchyUploadContext, hierarchyPropertyValues, folderPropertyValues, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 115U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 115U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 115U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ImportHierarchyChange, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600007F RID: 127
		protected abstract RopResult ImportHierarchyChange(MapiContext context, IcsHierarchyUploadContext serverObject, PropertyValue[] hierarchyPropertyValues, PropertyValue[] folderPropertyValues, ImportHierarchyChangeResultFactory resultFactory);

		// Token: 0x06000080 RID: 128 RVA: 0x00013930 File Offset: 0x00011B30
		public RopResult ImportMessageChange(IServerObject serverObject, ImportMessageChangeFlags importMessageChangeFlags, PropertyValue[] propertyValues, ImportMessageChangeResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ImportMessageChange"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				IcsContentUploadContext icsContentUploadContext = serverObject as IcsContentUploadContext;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (icsContentUploadContext == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = icsContentUploadContext;
					MapiLogon logon = icsContentUploadContext.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Message).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ImportMessageChange, RopHandlerBase.ImportMessageChangeClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ImportMessageChange, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ImportMessageChange, icsContentUploadContext);
								ropResult = this.ImportMessageChange(this.mapiContext, icsContentUploadContext, importMessageChangeFlags, propertyValues, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Message);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 114U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 114U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 114U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ImportMessageChange, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000081 RID: 129
		protected abstract RopResult ImportMessageChange(MapiContext context, IcsContentUploadContext serverObject, ImportMessageChangeFlags importMessageChangeFlags, PropertyValue[] propertyValues, ImportMessageChangeResultFactory resultFactory);

		// Token: 0x06000082 RID: 130 RVA: 0x00013E54 File Offset: 0x00012054
		public RopResult ImportMessageChangePartial(IServerObject serverObject, ImportMessageChangeFlags importMessageChangeFlags, PropertyValue[] propertyValues, ImportMessageChangePartialResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ImportMessageChangePartial"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				IcsContentUploadContext icsContentUploadContext = serverObject as IcsContentUploadContext;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (icsContentUploadContext == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = icsContentUploadContext;
					MapiLogon logon = icsContentUploadContext.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Message).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ImportMessageChangePartial, RopHandlerBase.ImportMessageChangePartialClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ImportMessageChangePartial, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ImportMessageChangePartial, icsContentUploadContext);
								ropResult = this.ImportMessageChangePartial(this.mapiContext, icsContentUploadContext, importMessageChangeFlags, propertyValues, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Message);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 153U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 153U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 153U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ImportMessageChangePartial, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000083 RID: 131
		protected abstract RopResult ImportMessageChangePartial(MapiContext context, IcsContentUploadContext serverObject, ImportMessageChangeFlags importMessageChangeFlags, PropertyValue[] propertyValues, ImportMessageChangePartialResultFactory resultFactory);

		// Token: 0x06000084 RID: 132 RVA: 0x00014378 File Offset: 0x00012578
		public RopResult ImportMessageMove(IServerObject serverObject, byte[] sourceFolder, byte[] sourceMessage, byte[] predecessorChangeList, byte[] destinationMessage, byte[] destinationChangeNumber, ImportMessageMoveResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ImportMessageMove"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				IcsContentUploadContext icsContentUploadContext = serverObject as IcsContentUploadContext;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (icsContentUploadContext == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = icsContentUploadContext;
					MapiLogon logon = icsContentUploadContext.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ImportMessageMove, RopHandlerBase.ImportMessageMoveClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ImportMessageMove, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ImportMessageMove, icsContentUploadContext);
								ropResult = this.ImportMessageMove(this.mapiContext, icsContentUploadContext, sourceFolder, sourceMessage, predecessorChangeList, destinationMessage, destinationChangeNumber, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 120U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 120U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 120U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ImportMessageMove, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000085 RID: 133
		protected abstract RopResult ImportMessageMove(MapiContext context, IcsContentUploadContext serverObject, byte[] sourceFolder, byte[] sourceMessage, byte[] predecessorChangeList, byte[] destinationMessage, byte[] destinationChangeNumber, ImportMessageMoveResultFactory resultFactory);

		// Token: 0x06000086 RID: 134 RVA: 0x0001481C File Offset: 0x00012A1C
		public RopResult ImportReads(IServerObject serverObject, MessageReadState[] messageReadStates, ImportReadsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ImportReads"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				IcsContentUploadContext icsContentUploadContext = serverObject as IcsContentUploadContext;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (icsContentUploadContext == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = icsContentUploadContext;
					MapiLogon logon = icsContentUploadContext.Logon;
					bool flag = 5 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsImportReadsSharedMailboxOperation(icsContentUploadContext);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ImportReads, RopHandlerBase.ImportReadsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ImportReads, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ImportReads, icsContentUploadContext);
								ropResult = this.ImportReads(this.mapiContext, icsContentUploadContext, messageReadStates, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 128U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 128U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 128U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ImportReads, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000087 RID: 135
		protected abstract RopResult ImportReads(MapiContext context, IcsContentUploadContext serverObject, MessageReadState[] messageReadStates, ImportReadsResultFactory resultFactory);

		// Token: 0x06000088 RID: 136 RVA: 0x00014CFC File Offset: 0x00012EFC
		public RopResult IncrementalConfig(IServerObject serverObject, IncrementalConfigOption configOptions, FastTransferSendOption sendOptions, SyncFlag syncFlags, Restriction restriction, SyncExtraFlag extraFlags, PropertyTag[] propertyTags, StoreId[] messageIds, IncrementalConfigResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.IncrementalConfig"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.FastTransferSource).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.IncrementalConfig, RopHandlerBase.IncrementalConfigClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.IncrementalConfig, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.IncrementalConfig, mapiFolder);
								ropResult = this.IncrementalConfig(this.mapiContext, mapiFolder, configOptions, sendOptions, syncFlags, restriction, extraFlags, propertyTags, RcaTypeHelpers.StoreIdsToExchangeIds(messageIds, logon.StoreMailbox), resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.FastTransferSource);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 112U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 112U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 112U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.IncrementalConfig, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000089 RID: 137
		protected abstract RopResult IncrementalConfig(MapiContext context, MapiFolder serverObject, IncrementalConfigOption configOptions, FastTransferSendOption sendOptions, SyncFlag syncFlags, Restriction restriction, SyncExtraFlag extraFlags, PropertyTag[] propertyTags, ExchangeId[] messageIds, IncrementalConfigResultFactory resultFactory);

		// Token: 0x0600008A RID: 138 RVA: 0x00015214 File Offset: 0x00013414
		public RopResult LockRegionStream(IServerObject serverObject, ulong offset, ulong regionLength, LockTypeFlag lockType, LockRegionStreamResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.LockRegionStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = 2 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.LockRegionStream, RopHandlerBase.LockRegionStreamClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.LockRegionStream, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.LockRegionStream, mapiStream);
								ropResult = this.LockRegionStream(this.mapiContext, mapiStream, offset, regionLength, lockType, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 91U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 91U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 91U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.LockRegionStream, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139
		protected abstract RopResult LockRegionStream(MapiContext context, MapiStream serverObject, ulong offset, ulong regionLength, LockTypeFlag lockType, LockRegionStreamResultFactory resultFactory);

		// Token: 0x0600008C RID: 140 RVA: 0x000156C4 File Offset: 0x000138C4
		public RopResult Logon(LogonFlags logonFlags, OpenFlags openFlags, StoreState storeState, LogonExtendedRequestFlags extendedFlags, MailboxId? mailboxId, LocaleInfo? localeInfo, string applicationId, AuthenticationContext authenticationContext, byte[] tenantHint, LogonResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.Logon"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				ErrorCode errorCode = (ErrorCode)2147746065U;
				RopResult ropResult = null;
				try
				{
					if (mailboxId != null)
					{
						methodFrame.CurrentThreadInfo.MailboxGuid = mailboxId.Value.MailboxGuid;
					}
					ropResult = this.Logon(this.mapiContext, logonFlags, openFlags, storeState, extendedFlags, mailboxId, localeInfo, applicationId, authenticationContext, tenantHint, resultFactory);
				}
				catch (StoreException ex)
				{
					this.mapiContext.OnExceptionCatch(ex);
					ropResult = null;
					DiagnosticContext.TraceDword((LID)56872U, 254U);
					ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)42712U, ex);
					if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
					{
						throw;
					}
					errorCode = (ErrorCode)ex.Error;
				}
				catch (RopExecutionException ex2)
				{
					this.mapiContext.OnExceptionCatch(ex2);
					ropResult = null;
					DiagnosticContext.TraceDword((LID)44584U, 254U);
					ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)59096U, ex2);
					if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
					{
						throw;
					}
					errorCode = ex2.ErrorCode;
				}
				catch (BufferParseException exception)
				{
					this.mapiContext.OnExceptionCatch(exception);
					ropResult = null;
					DiagnosticContext.TraceDword((LID)60968U, 254U);
					ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)34520U, exception);
					if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
					{
						throw;
					}
					errorCode = ErrorCode.RpcFormat;
				}
				this.AssertSessionIsNotTerminating(RopId.Logon, errorCode, ropResult);
				if (ropResult == null)
				{
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider((MapiExecutionDiagnostics)this.MapiContext.Diagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600008D RID: 141
		protected abstract RopResult Logon(MapiContext context, LogonFlags logonFlags, OpenFlags openFlags, StoreState storeState, LogonExtendedRequestFlags extendedFlags, MailboxId? mailboxId, LocaleInfo? localeInfo, string applicationId, AuthenticationContext authenticationContext, byte[] tenantHint, LogonResultFactory resultFactory);

		// Token: 0x0600008E RID: 142 RVA: 0x000158E8 File Offset: 0x00013AE8
		public RopResult LongTermIdFromId(IServerObject serverObject, StoreId storeId, LongTermIdFromIdResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.LongTermIdFromId"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.LongTermIdFromId, RopHandlerBase.LongTermIdFromIdClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.LongTermIdFromId, RopHandlerBase.LongTermIdFromIdClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.LongTermIdFromId, mapiBase);
								ropResult = this.LongTermIdFromId(this.mapiContext, mapiBase, RcaTypeHelpers.StoreIdToExchangeId(storeId, logon.StoreMailbox), resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 67U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 67U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 67U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.LongTermIdFromId, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600008F RID: 143
		protected abstract RopResult LongTermIdFromId(MapiContext context, MapiBase serverObject, ExchangeId storeId, LongTermIdFromIdResultFactory resultFactory);

		// Token: 0x06000090 RID: 144 RVA: 0x00015D94 File Offset: 0x00013F94
		public RopResult ModifyPermissions(IServerObject serverObject, ModifyPermissionsFlags modifyPermissionsFlags, ModifyTableRow[] permissions, ModifyPermissionsResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.ModifyPermissions"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00015DF8 File Offset: 0x00013FF8
		public RopResult ModifyRules(IServerObject serverObject, ModifyRulesFlags modifyRulesFlags, ModifyTableRow[] rulesData, ModifyRulesResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.ModifyRules"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00015E5C File Offset: 0x0001405C
		public RopResult MoveCopyMessages(IServerObject sourceServerObject, IServerObject destinationServerObject, StoreId[] messageIds, bool reportProgress, bool isCopy, MoveCopyMessagesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.MoveCopyMessages"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (sourceServerObject == null)
				{
					throw new ArgumentNullException("sourceServerObject");
				}
				if (destinationServerObject == null)
				{
					throw new ArgumentNullException("destinationServerObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = sourceServerObject as MapiFolder;
				MapiFolder mapiFolder2 = destinationServerObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null || mapiFolder2 == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.MoveCopyMessages, RopHandlerBase.MoveCopyMessagesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.MoveCopyMessages, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.MoveCopyMessages, mapiFolder);
								ropResult = this.MoveCopyMessages(this.mapiContext, mapiFolder, mapiFolder2, RcaTypeHelpers.StoreIdsToExchangeIds(messageIds, logon.StoreMailbox), reportProgress, isCopy, out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 51U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 51U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 51U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.MoveCopyMessages, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000093 RID: 147
		protected abstract RopResult MoveCopyMessages(MapiContext context, MapiFolder sourceServerObject, MapiFolder destinationServerObject, ExchangeId[] messageIds, bool reportProgress, bool isCopy, out bool outputIsPartiallyCompleted, MoveCopyMessagesResultFactory resultFactory);

		// Token: 0x06000094 RID: 148 RVA: 0x0001632C File Offset: 0x0001452C
		public RopResult MoveCopyMessagesExtended(IServerObject sourceServerObject, IServerObject destinationServerObject, StoreId[] messageIds, bool reportProgress, bool isCopy, PropertyValue[] propertyValues, MoveCopyMessagesExtendedResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.MoveCopyMessagesExtended"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (sourceServerObject == null)
				{
					throw new ArgumentNullException("sourceServerObject");
				}
				if (destinationServerObject == null)
				{
					throw new ArgumentNullException("destinationServerObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = sourceServerObject as MapiFolder;
				MapiFolder mapiFolder2 = destinationServerObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null || mapiFolder2 == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.MoveCopyMessagesExtended, RopHandlerBase.MoveCopyMessagesExtendedClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.MoveCopyMessagesExtended, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.MoveCopyMessagesExtended, mapiFolder);
								ropResult = this.MoveCopyMessagesExtended(this.mapiContext, mapiFolder, mapiFolder2, RcaTypeHelpers.StoreIdsToExchangeIds(messageIds, logon.StoreMailbox), reportProgress, isCopy, propertyValues, out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 155U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 155U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 155U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.MoveCopyMessagesExtended, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000095 RID: 149
		protected abstract RopResult MoveCopyMessagesExtended(MapiContext context, MapiFolder sourceServerObject, MapiFolder destinationServerObject, ExchangeId[] messageIds, bool reportProgress, bool isCopy, PropertyValue[] propertyValues, out bool outputIsPartiallyCompleted, MoveCopyMessagesExtendedResultFactory resultFactory);

		// Token: 0x06000096 RID: 150 RVA: 0x00016814 File Offset: 0x00014A14
		public RopResult MoveCopyMessagesExtendedWithEntryIds(IServerObject sourceServerObject, IServerObject destinationServerObject, StoreId[] messageIds, bool reportProgress, bool isCopy, PropertyValue[] propertyValues, MoveCopyMessagesExtendedWithEntryIdsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.MoveCopyMessagesExtendedWithEntryIds"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (sourceServerObject == null)
				{
					throw new ArgumentNullException("sourceServerObject");
				}
				if (destinationServerObject == null)
				{
					throw new ArgumentNullException("destinationServerObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = sourceServerObject as MapiFolder;
				MapiFolder mapiFolder2 = destinationServerObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null || mapiFolder2 == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.MoveCopyMessagesExtendedWithEntryIds, RopHandlerBase.MoveCopyMessagesExtendedWithEntryIdsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.MoveCopyMessagesExtendedWithEntryIds, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.MoveCopyMessagesExtendedWithEntryIds, mapiFolder);
								ropResult = this.MoveCopyMessagesExtendedWithEntryIds(this.mapiContext, mapiFolder, mapiFolder2, RcaTypeHelpers.StoreIdsToExchangeIds(messageIds, logon.StoreMailbox), reportProgress, isCopy, propertyValues, out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 160U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 160U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 160U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.MoveCopyMessagesExtendedWithEntryIds, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000097 RID: 151
		protected abstract RopResult MoveCopyMessagesExtendedWithEntryIds(MapiContext context, MapiFolder sourceServerObject, MapiFolder destinationServerObject, ExchangeId[] messageIds, bool reportProgress, bool isCopy, PropertyValue[] propertyValues, out bool outputIsPartiallyCompleted, MoveCopyMessagesExtendedWithEntryIdsResultFactory resultFactory);

		// Token: 0x06000098 RID: 152 RVA: 0x00016CFC File Offset: 0x00014EFC
		public RopResult MoveFolder(IServerObject sourceServerObject, IServerObject destinationServerObject, bool reportProgress, StoreId sourceSubFolderId, string destinationSubFolderName, MoveFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.MoveFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (sourceServerObject == null)
				{
					throw new ArgumentNullException("sourceServerObject");
				}
				if (destinationServerObject == null)
				{
					throw new ArgumentNullException("destinationServerObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = sourceServerObject as MapiFolder;
				MapiFolder mapiFolder2 = destinationServerObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null || mapiFolder2 == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.MoveFolder, RopHandlerBase.MoveFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.MoveFolder, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.MoveFolder, mapiFolder);
								ropResult = this.MoveFolder(this.mapiContext, mapiFolder, mapiFolder2, reportProgress, RcaTypeHelpers.StoreIdToExchangeId(sourceSubFolderId, logon.StoreMailbox), destinationSubFolderName, out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(sourceSubFolderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 53U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 53U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 53U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.MoveFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000099 RID: 153
		protected abstract RopResult MoveFolder(MapiContext context, MapiFolder sourceServerObject, MapiFolder destinationServerObject, bool reportProgress, ExchangeId sourceSubFolderId, string destinationSubFolderName, out bool outputIsPartiallyCompleted, MoveFolderResultFactory resultFactory);

		// Token: 0x0600009A RID: 154 RVA: 0x000171F4 File Offset: 0x000153F4
		public RopResult OpenAttachment(IServerObject serverObject, OpenMode openMode, uint attachmentNumber, OpenAttachmentResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.OpenAttachment"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Attachment).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.OpenAttachment, RopHandlerBase.OpenAttachmentClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.OpenAttachment, RopHandlerBase.OpenAttachmentClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.OpenAttachment, mapiMessage);
								ropResult = this.OpenAttachment(this.mapiContext, mapiMessage, openMode, attachmentNumber, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Attachment);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 34U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 34U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 34U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.OpenAttachment, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600009B RID: 155
		protected abstract RopResult OpenAttachment(MapiContext context, MapiMessage serverObject, OpenMode openMode, uint attachmentNumber, OpenAttachmentResultFactory resultFactory);

		// Token: 0x0600009C RID: 156 RVA: 0x00017710 File Offset: 0x00015910
		public RopResult OpenCollector(IServerObject serverObject, bool wantMessageCollector, OpenCollectorResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.OpenCollector"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.UntrackedObject).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.OpenCollector, RopHandlerBase.OpenCollectorClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.OpenCollector, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.OpenCollector, mapiFolder);
								ropResult = this.OpenCollector(this.mapiContext, mapiFolder, wantMessageCollector, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.UntrackedObject);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 126U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 126U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 126U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.OpenCollector, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600009D RID: 157
		protected abstract RopResult OpenCollector(MapiContext context, MapiFolder serverObject, bool wantMessageCollector, OpenCollectorResultFactory resultFactory);

		// Token: 0x0600009E RID: 158 RVA: 0x00017C24 File Offset: 0x00015E24
		public RopResult OpenEmbeddedMessage(IServerObject serverObject, ushort codePageId, OpenMode openMode, OpenEmbeddedMessageResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.OpenEmbeddedMessage"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiAttachment mapiAttachment = serverObject as MapiAttachment;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiAttachment == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiAttachment;
					MapiLogon logon = mapiAttachment.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Message).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.OpenEmbeddedMessage, RopHandlerBase.OpenEmbeddedMessageClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.OpenEmbeddedMessage, RopHandlerBase.OpenEmbeddedMessageClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.OpenEmbeddedMessage, mapiAttachment);
								ropResult = this.OpenEmbeddedMessage(this.mapiContext, mapiAttachment, codePageId, openMode, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Message);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 70U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 70U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 70U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.OpenEmbeddedMessage, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600009F RID: 159
		protected abstract RopResult OpenEmbeddedMessage(MapiContext context, MapiAttachment serverObject, ushort codePageId, OpenMode openMode, OpenEmbeddedMessageResultFactory resultFactory);

		// Token: 0x060000A0 RID: 160 RVA: 0x0001814C File Offset: 0x0001634C
		public RopResult OpenFolder(IServerObject serverObject, StoreId folderId, OpenMode openMode, OpenFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.OpenFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Folder).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.OpenFolder, RopHandlerBase.OpenFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.OpenFolder, RopHandlerBase.OpenFolderClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.OpenFolder, mapiBase);
								ropResult = this.OpenFolder(this.mapiContext, mapiBase, RcaTypeHelpers.StoreIdToExchangeId(folderId, logon.StoreMailbox), openMode, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Folder);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(folderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 2U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 2U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 2U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.OpenFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000A1 RID: 161
		protected abstract RopResult OpenFolder(MapiContext context, MapiBase serverObject, ExchangeId folderId, OpenMode openMode, OpenFolderResultFactory resultFactory);

		// Token: 0x060000A2 RID: 162 RVA: 0x00018678 File Offset: 0x00016878
		public RopResult OpenMessage(IServerObject serverObject, ushort codePageId, StoreId folderId, OpenMode openMode, StoreId messageId, OpenMessageResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.OpenMessage"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Message).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.OpenMessage, RopHandlerBase.OpenMessageClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.OpenMessage, RopHandlerBase.OpenMessageClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.OpenMessage, mapiBase);
								ropResult = this.OpenMessage(this.mapiContext, mapiBase, codePageId, RcaTypeHelpers.StoreIdToExchangeId(folderId, logon.StoreMailbox), openMode, RcaTypeHelpers.StoreIdToExchangeId(messageId, logon.StoreMailbox), resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Message);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnMid(messageId);
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(folderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 3U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 3U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 3U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.OpenMessage, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000A3 RID: 163
		protected abstract RopResult OpenMessage(MapiContext context, MapiBase serverObject, ushort codePageId, ExchangeId folderId, OpenMode openMode, ExchangeId messageId, OpenMessageResultFactory resultFactory);

		// Token: 0x060000A4 RID: 164 RVA: 0x00018BC4 File Offset: 0x00016DC4
		public RopResult OpenStream(IServerObject serverObject, PropertyTag propertyTag, OpenMode openMode, OpenStreamResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.OpenStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Stream).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.OpenStream, RopHandlerBase.OpenStreamClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.OpenStream, RopHandlerBase.OpenStreamClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.OpenStream, mapiPropBagBase);
								ropResult = this.OpenStream(this.mapiContext, mapiPropBagBase, propertyTag, openMode, resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Stream);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 43U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 43U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 43U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.OpenStream, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165
		protected abstract RopResult OpenStream(MapiContext context, MapiPropBagBase serverObject, PropertyTag propertyTag, OpenMode openMode, OpenStreamResultFactory resultFactory);

		// Token: 0x060000A6 RID: 166 RVA: 0x000190C8 File Offset: 0x000172C8
		public RopResult PrereadMessages(IServerObject serverObject, StoreIdPair[] messages, PrereadMessagesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.PrereadMessages"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.PrereadMessages, RopHandlerBase.PrereadMessagesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.PrereadMessages, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.PrereadMessages, mapiLogon);
								ropResult = this.PrereadMessages(this.mapiContext, mapiLogon, messages, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 162U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 162U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 162U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.PrereadMessages, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000A7 RID: 167
		protected abstract RopResult PrereadMessages(MapiContext context, MapiLogon serverObject, StoreIdPair[] messages, PrereadMessagesResultFactory resultFactory);

		// Token: 0x060000A8 RID: 168 RVA: 0x00019578 File Offset: 0x00017778
		public RopResult Progress(IServerObject serverObject, bool wantCancel, ProgressResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.Progress"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000195DC File Offset: 0x000177DC
		public RopResult PublicFolderIsGhosted(IServerObject serverObject, StoreId folderId, PublicFolderIsGhostedResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.PublicFolderIsGhosted"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00019640 File Offset: 0x00017840
		public RopResult QueryColumnsAll(IServerObject serverObject, QueryColumnsAllResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.QueryColumnsAll"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.QueryColumnsAll, RopHandlerBase.QueryColumnsAllClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.QueryColumnsAll, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.QueryColumnsAll, mapiViewTableBase);
								ropResult = this.QueryColumnsAll(this.mapiContext, mapiViewTableBase, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 55U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 55U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 55U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.QueryColumnsAll, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000AB RID: 171
		protected abstract RopResult QueryColumnsAll(MapiContext context, MapiViewTableBase serverObject, QueryColumnsAllResultFactory resultFactory);

		// Token: 0x060000AC RID: 172 RVA: 0x00019B08 File Offset: 0x00017D08
		public RopResult QueryNamedProperties(IServerObject serverObject, QueryNamedPropertyFlags queryFlags, Guid? propertyGuid, QueryNamedPropertiesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.QueryNamedProperties"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = 3 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.QueryNamedProperties, RopHandlerBase.QueryNamedPropertiesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.QueryNamedProperties, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.QueryNamedProperties, mapiPropBagBase);
								ropResult = this.QueryNamedProperties(this.mapiContext, mapiPropBagBase, queryFlags, propertyGuid, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 95U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 95U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 95U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.QueryNamedProperties, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000AD RID: 173
		protected abstract RopResult QueryNamedProperties(MapiContext context, MapiPropBagBase serverObject, QueryNamedPropertyFlags queryFlags, Guid? propertyGuid, QueryNamedPropertiesResultFactory resultFactory);

		// Token: 0x060000AE RID: 174 RVA: 0x00019FB8 File Offset: 0x000181B8
		public RopResult QueryPosition(IServerObject serverObject, QueryPositionResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.QueryPosition"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.QueryPosition, RopHandlerBase.QueryPositionClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.QueryPosition, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.QueryPosition, mapiViewTableBase);
								ropResult = this.QueryPosition(this.mapiContext, mapiViewTableBase, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 23U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 23U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 23U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.QueryPosition, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000AF RID: 175
		protected abstract RopResult QueryPosition(MapiContext context, MapiViewTableBase serverObject, QueryPositionResultFactory resultFactory);

		// Token: 0x060000B0 RID: 176 RVA: 0x0001A480 File Offset: 0x00018680
		public RopResult QueryRows(IServerObject serverObject, QueryRowsFlags flags, bool useForwardDirection, ushort rowCount, QueryRowsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.QueryRows"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.QueryRows, RopHandlerBase.QueryRowsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.QueryRows, RopHandlerBase.QueryRowsClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.QueryRows, mapiViewTableBase);
								ropResult = this.QueryRows(this.mapiContext, mapiViewTableBase, flags, useForwardDirection, rowCount, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 21U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 21U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 21U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.QueryRows, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177
		protected abstract RopResult QueryRows(MapiContext context, MapiViewTableBase serverObject, QueryRowsFlags flags, bool useForwardDirection, ushort rowCount, QueryRowsResultFactory resultFactory);

		// Token: 0x060000B2 RID: 178 RVA: 0x0001A954 File Offset: 0x00018B54
		public RopResult ReadPerUserInformation(IServerObject serverObject, StoreLongTermId longTermId, bool wantIfChanged, uint dataOffset, ushort maxDataSize, ReadPerUserInformationResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ReadPerUserInformation"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = 5 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ReadPerUserInformation, RopHandlerBase.ReadPerUserInformationClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ReadPerUserInformation, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ReadPerUserInformation, mapiLogon);
								ropResult = this.ReadPerUserInformation(this.mapiContext, mapiLogon, longTermId, wantIfChanged, dataOffset, maxDataSize, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 99U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 99U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 99U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ReadPerUserInformation, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000B3 RID: 179
		protected abstract RopResult ReadPerUserInformation(MapiContext context, MapiLogon serverObject, StoreLongTermId longTermId, bool wantIfChanged, uint dataOffset, ushort maxDataSize, ReadPerUserInformationResultFactory resultFactory);

		// Token: 0x060000B4 RID: 180 RVA: 0x0001AE08 File Offset: 0x00019008
		public RopResult ReadRecipients(IServerObject serverObject, uint recipientRowId, PropertyTag[] extraUnicodePropertyTags, ReadRecipientsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ReadRecipients"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ReadRecipients, RopHandlerBase.ReadRecipientsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ReadRecipients, RopHandlerBase.ReadRecipientsClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ReadRecipients, mapiMessage);
								ropResult = this.ReadRecipients(this.mapiContext, mapiMessage, recipientRowId, extraUnicodePropertyTags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 15U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 15U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 15U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ReadRecipients, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000B5 RID: 181
		protected abstract RopResult ReadRecipients(MapiContext context, MapiMessage serverObject, uint recipientRowId, PropertyTag[] extraUnicodePropertyTags, ReadRecipientsResultFactory resultFactory);

		// Token: 0x060000B6 RID: 182 RVA: 0x0001B2AC File Offset: 0x000194AC
		public RopResult ReadStream(IServerObject serverObject, ushort byteCount, ReadStreamResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ReadStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ReadStream, RopHandlerBase.ReadStreamClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ReadStream, RopHandlerBase.ReadStreamClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ReadStream, mapiStream);
								ropResult = this.ReadStream(this.mapiContext, mapiStream, byteCount, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 44U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 44U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 44U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ReadStream, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000B7 RID: 183
		protected abstract RopResult ReadStream(MapiContext context, MapiStream serverObject, ushort byteCount, ReadStreamResultFactory resultFactory);

		// Token: 0x060000B8 RID: 184 RVA: 0x0001B764 File Offset: 0x00019964
		public RopResult RegisterNotification(IServerObject serverObject, NotificationFlags flags, NotificationEventFlags eventFlags, bool wantGlobalScope, StoreId folderId, StoreId messageId, RegisterNotificationResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.RegisterNotification"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.Session.GetPerSessionObjectCounter(MapiObjectTrackedType.Notify).CheckObjectQuota(true);
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.RegisterNotification, RopHandlerBase.RegisterNotificationClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.RegisterNotification, RopHandlerBase.RegisterNotificationClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.RegisterNotification, mapiLogon);
								ropResult = this.RegisterNotification(this.mapiContext, mapiLogon, flags, eventFlags, wantGlobalScope, RcaTypeHelpers.StoreIdToExchangeId(folderId, logon.StoreMailbox), RcaTypeHelpers.StoreIdToExchangeId(messageId, logon.StoreMailbox), resultFactory);
								if (ropResult.ReturnObject != null)
								{
									IMapiObject mapiObject = (IMapiObject)ropResult.ReturnObject;
									mapiObject.IncrementObjectCounter(MapiObjectTrackingScope.Session, MapiObjectTrackedType.Notify);
									this.mapiContext.RegisterStateAction(null, delegate(Context ctx)
									{
										mapiObject.Dispose();
									});
								}
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnMid(messageId);
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(folderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 41U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 41U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 41U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.RegisterNotification, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000B9 RID: 185
		protected abstract RopResult RegisterNotification(MapiContext context, MapiLogon serverObject, NotificationFlags flags, NotificationEventFlags eventFlags, bool wantGlobalScope, ExchangeId folderId, ExchangeId messageId, RegisterNotificationResultFactory resultFactory);

		// Token: 0x060000BA RID: 186 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		public RopResult RegisterSynchronizationNotifications(IServerObject serverObject, StoreId[] folderIds, uint[] changeNumbers, RegisterSynchronizationNotificationsResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.RegisterSynchronizationNotifications"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0001BD04 File Offset: 0x00019F04
		public void Release(IServerObject serverObject)
		{
			using (Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi.ExTraceGlobals.FaultInjectionTracer.DisableAllTraces())
			{
				using (Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices.ExTraceGlobals.FaultInjectionTracer.DisableAllTraces())
				{
					using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.Release"))
					{
						if (serverObject == null)
						{
							throw new ArgumentNullException("serverObject");
						}
						MapiBase mapiBase = serverObject as MapiBase;
						if (mapiBase != null && !mapiBase.IsDisposed)
						{
							mapiBase.DecrementObjectCounter(MapiObjectTrackingScope.All);
							MapiLogon logon = mapiBase.Logon;
							if (logon.IsValid)
							{
								bool flag = this.IsReleaseSharedMailboxOperation(mapiBase);
								bool flag2 = logon.IsDeferedReleaseSharedOperation();
								this.mapiContext.Initialize(logon, flag && flag2, true);
								methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
								MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
								bool flag3 = false;
								try
								{
									mapiExecutionDiagnostics.ClearExceptionHistory();
									ErrorCode errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, true, true, true);
									if (errorCode == ErrorCode.None)
									{
										flag3 = true;
										bool commit = false;
										try
										{
											mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
											logon.ProcessDeferedReleaseROPs(this.mapiContext);
											this.Release(this.mapiContext, mapiBase);
											if (this.mapiContext.IsMailboxOperationStarted)
											{
												commit = (!this.mapiContext.LockedMailboxState.Quarantined || ((this.mapiContext.ClientType == ClientType.Migration || this.mapiContext.ClientType == ClientType.PublicFolderSystem) && MailboxQuarantineProvider.Instance.IsMigrationAccessAllowed(this.mapiContext.LockedMailboxState.DatabaseGuid, this.mapiContext.LockedMailboxState.MailboxGuid)));
											}
											goto IL_1CB;
										}
										finally
										{
											try
											{
												mapiBase.Dispose();
												if (mapiBase is MapiLogon)
												{
													this.mapiContext.SetMapiLogon(null);
												}
											}
											finally
											{
												if (this.mapiContext.IsMailboxOperationStarted)
												{
													this.mapiContext.EndMailboxOperation(commit, true);
												}
											}
										}
										goto IL_1B9;
										IL_1CB:
										goto IL_343;
									}
									IL_1B9:
									throw new StoreException((LID)42272U, (ErrorCodeValue)errorCode);
								}
								catch (StoreException exception)
								{
									this.mapiContext.OnExceptionCatch(exception);
									DiagnosticContext.TraceDword((LID)36392U, 1U);
									ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)50904U, exception);
									if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
									{
										throw;
									}
									if (mapiBase is MapiLogon)
									{
										using (this.mapiContext.CriticalBlock((LID)43852U, CriticalBlockScope.MailboxSession))
										{
											throw;
										}
									}
									if (!flag3)
									{
										logon.DeferReleaseROP(mapiBase);
									}
								}
								catch (RopExecutionException exception2)
								{
									this.mapiContext.OnExceptionCatch(exception2);
									DiagnosticContext.TraceDword((LID)52776U, 1U);
									ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)47832U, exception2);
									if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
									{
										throw;
									}
									if (mapiBase is MapiLogon)
									{
										using (this.mapiContext.CriticalBlock((LID)37708U, CriticalBlockScope.MailboxSession))
										{
											throw;
										}
									}
								}
								catch (BufferParseException exception3)
								{
									this.mapiContext.OnExceptionCatch(exception3);
									DiagnosticContext.TraceDword((LID)46632U, 1U);
									ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)64216U, exception3);
									if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
									{
										throw;
									}
									if (mapiBase is MapiLogon)
									{
										using (this.mapiContext.CriticalBlock((LID)45900U, CriticalBlockScope.MailboxSession))
										{
											throw;
										}
									}
								}
							}
							IL_343:
							this.AssertSessionIsNotTerminating(RopId.Release, (ErrorCode)2147746069U, null);
						}
					}
				}
			}
		}

		// Token: 0x060000BC RID: 188
		protected abstract void Release(MapiContext context, MapiBase serverObject);

		// Token: 0x060000BD RID: 189 RVA: 0x0001C19C File Offset: 0x0001A39C
		public RopResult ReloadCachedInformation(IServerObject serverObject, PropertyTag[] extraUnicodePropertyTags, ReloadCachedInformationResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ReloadCachedInformation"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ReloadCachedInformation, RopHandlerBase.ReloadCachedInformationClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ReloadCachedInformation, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ReloadCachedInformation, mapiMessage);
								ropResult = this.ReloadCachedInformation(this.mapiContext, mapiMessage, extraUnicodePropertyTags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 16U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 16U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 16U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ReloadCachedInformation, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000BE RID: 190
		protected abstract RopResult ReloadCachedInformation(MapiContext context, MapiMessage serverObject, PropertyTag[] extraUnicodePropertyTags, ReloadCachedInformationResultFactory resultFactory);

		// Token: 0x060000BF RID: 191 RVA: 0x0001C648 File Offset: 0x0001A848
		public RopResult RemoveAllRecipients(IServerObject serverObject, RemoveAllRecipientsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.RemoveAllRecipients"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.RemoveAllRecipients, RopHandlerBase.RemoveAllRecipientsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.RemoveAllRecipients, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.RemoveAllRecipients, mapiMessage);
								ropResult = this.RemoveAllRecipients(this.mapiContext, mapiMessage, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 13U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 13U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 13U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.RemoveAllRecipients, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192
		protected abstract RopResult RemoveAllRecipients(MapiContext context, MapiMessage serverObject, RemoveAllRecipientsResultFactory resultFactory);

		// Token: 0x060000C1 RID: 193 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		public RopResult ResetTable(IServerObject serverObject, ResetTableResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.ResetTable"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.ResetTable, RopHandlerBase.ResetTableClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.ResetTable, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.ResetTable, mapiViewTableBase);
								ropResult = this.ResetTable(this.mapiContext, mapiViewTableBase, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 129U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 129U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 129U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.ResetTable, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000C2 RID: 194
		protected abstract RopResult ResetTable(MapiContext context, MapiViewTableBase serverObject, ResetTableResultFactory resultFactory);

		// Token: 0x060000C3 RID: 195 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		public RopResult Restrict(IServerObject serverObject, RestrictFlags flags, Restriction restriction, RestrictResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.Restrict"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.Restrict, RopHandlerBase.RestrictClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.Restrict, RopHandlerBase.RestrictClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.Restrict, mapiViewTableBase);
								ropResult = this.Restrict(this.mapiContext, mapiViewTableBase, flags, restriction, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 20U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 20U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 20U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.Restrict, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				if (mapiViewTableBase != null)
				{
					mapiViewTableBase.ConfigurationError.Restrict = ropResult.ErrorCode;
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196
		protected abstract RopResult Restrict(MapiContext context, MapiViewTableBase serverObject, RestrictFlags flags, Restriction restriction, RestrictResultFactory resultFactory);

		// Token: 0x060000C5 RID: 197 RVA: 0x0001D458 File Offset: 0x0001B658
		public RopResult SaveChangesAttachment(IServerObject serverObject, SaveChangesMode saveChangesMode, SaveChangesAttachmentResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SaveChangesAttachment"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiAttachment mapiAttachment = serverObject as MapiAttachment;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiAttachment == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiAttachment;
					MapiLogon logon = mapiAttachment.Logon;
					bool flag = 4 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsSaveChangesAttachmentSharedMailboxOperation(mapiAttachment);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SaveChangesAttachment, RopHandlerBase.SaveChangesAttachmentClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SaveChangesAttachment, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SaveChangesAttachment, mapiAttachment);
								ropResult = this.SaveChangesAttachment(this.mapiContext, mapiAttachment, saveChangesMode, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 37U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 37U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 37U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SaveChangesAttachment, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198
		protected abstract RopResult SaveChangesAttachment(MapiContext context, MapiAttachment serverObject, SaveChangesMode saveChangesMode, SaveChangesAttachmentResultFactory resultFactory);

		// Token: 0x060000C7 RID: 199 RVA: 0x0001D908 File Offset: 0x0001BB08
		public RopResult SaveChangesMessage(IServerObject serverObject, SaveChangesMode saveChangesMode, SaveChangesMessageResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SaveChangesMessage"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = 4 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsSaveChangesMessageSharedMailboxOperation(mapiMessage);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SaveChangesMessage, RopHandlerBase.SaveChangesMessageClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SaveChangesMessage, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SaveChangesMessage, mapiMessage);
								ropResult = this.SaveChangesMessage(this.mapiContext, mapiMessage, saveChangesMode, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 12U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 12U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 12U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SaveChangesMessage, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000C8 RID: 200
		protected abstract RopResult SaveChangesMessage(MapiContext context, MapiMessage serverObject, SaveChangesMode saveChangesMode, SaveChangesMessageResultFactory resultFactory);

		// Token: 0x060000C9 RID: 201 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		public RopResult SeekRow(IServerObject serverObject, BookmarkOrigin bookmarkOrigin, int rowCount, bool wantMoveCount, SeekRowResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SeekRow"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SeekRow, RopHandlerBase.SeekRowClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SeekRow, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SeekRow, mapiViewTableBase);
								ropResult = this.SeekRow(this.mapiContext, mapiViewTableBase, bookmarkOrigin, rowCount, wantMoveCount, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 24U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 24U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 24U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SeekRow, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000CA RID: 202
		protected abstract RopResult SeekRow(MapiContext context, MapiViewTableBase serverObject, BookmarkOrigin bookmarkOrigin, int rowCount, bool wantMoveCount, SeekRowResultFactory resultFactory);

		// Token: 0x060000CB RID: 203 RVA: 0x0001E288 File Offset: 0x0001C488
		public RopResult SeekRowApproximate(IServerObject serverObject, uint numerator, uint denominator, SeekRowApproximateResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SeekRowApproximate"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SeekRowApproximate, RopHandlerBase.SeekRowApproximateClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SeekRowApproximate, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SeekRowApproximate, mapiViewTableBase);
								ropResult = this.SeekRowApproximate(this.mapiContext, mapiViewTableBase, numerator, denominator, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 26U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 26U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 26U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SeekRowApproximate, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000CC RID: 204
		protected abstract RopResult SeekRowApproximate(MapiContext context, MapiViewTableBase serverObject, uint numerator, uint denominator, SeekRowApproximateResultFactory resultFactory);

		// Token: 0x060000CD RID: 205 RVA: 0x0001E754 File Offset: 0x0001C954
		public RopResult SeekRowBookmark(IServerObject serverObject, byte[] bookmark, int rowCount, bool wantMoveCount, SeekRowBookmarkResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SeekRowBookmark"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SeekRowBookmark, RopHandlerBase.SeekRowBookmarkClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SeekRowBookmark, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SeekRowBookmark, mapiViewTableBase);
								ropResult = this.SeekRowBookmark(this.mapiContext, mapiViewTableBase, bookmark, rowCount, wantMoveCount, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 25U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 25U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 25U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SeekRowBookmark, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000CE RID: 206
		protected abstract RopResult SeekRowBookmark(MapiContext context, MapiViewTableBase serverObject, byte[] bookmark, int rowCount, bool wantMoveCount, SeekRowBookmarkResultFactory resultFactory);

		// Token: 0x060000CF RID: 207 RVA: 0x0001EC24 File Offset: 0x0001CE24
		public RopResult SeekStream(IServerObject serverObject, StreamSeekOrigin streamSeekOrigin, long offset, SeekStreamResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SeekStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = 2 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SeekStream, RopHandlerBase.SeekStreamClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SeekStream, RopHandlerBase.SeekStreamClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SeekStream, mapiStream);
								ropResult = this.SeekStream(this.mapiContext, mapiStream, streamSeekOrigin, offset, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 46U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 46U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 46U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SeekStream, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208
		protected abstract RopResult SeekStream(MapiContext context, MapiStream serverObject, StreamSeekOrigin streamSeekOrigin, long offset, SeekStreamResultFactory resultFactory);

		// Token: 0x060000D1 RID: 209 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		public RopResult SetCollapseState(IServerObject serverObject, byte[] collapseState, SetCollapseStateResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetCollapseState"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else if (mapiViewTableBase.ConfigurationError.HasConfigurationError)
				{
					errorCode = mapiViewTableBase.ConfigurationError.ErrorCode;
					DiagnosticContext.TraceDword((LID)60464U, (uint)errorCode);
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetCollapseState, RopHandlerBase.SetCollapseStateClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetCollapseState, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetCollapseState, mapiViewTableBase);
								ropResult = this.SetCollapseState(this.mapiContext, mapiViewTableBase, collapseState, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 108U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 108U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 108U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetCollapseState, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210
		protected abstract RopResult SetCollapseState(MapiContext context, MapiViewTableBase serverObject, byte[] collapseState, SetCollapseStateResultFactory resultFactory);

		// Token: 0x060000D3 RID: 211 RVA: 0x0001F5A0 File Offset: 0x0001D7A0
		public RopResult SetColumns(IServerObject serverObject, SetColumnsFlags flags, PropertyTag[] propertyTags, SetColumnsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetColumns"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetColumns, RopHandlerBase.SetColumnsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetColumns, RopHandlerBase.SetColumnsClientTypesAllowedOnReadOnlyDatabase);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetColumns, mapiViewTableBase);
								ropResult = this.SetColumns(this.mapiContext, mapiViewTableBase, flags, propertyTags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 18U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 18U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 18U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetColumns, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				if (mapiViewTableBase != null)
				{
					mapiViewTableBase.ConfigurationError.SetColumns = ropResult.ErrorCode;
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000D4 RID: 212
		protected abstract RopResult SetColumns(MapiContext context, MapiViewTableBase serverObject, SetColumnsFlags flags, PropertyTag[] propertyTags, SetColumnsResultFactory resultFactory);

		// Token: 0x060000D5 RID: 213 RVA: 0x0001FA58 File Offset: 0x0001DC58
		public RopResult SetLocalReplicaMidsetDeleted(IServerObject serverObject, LongTermIdRange[] longTermIdRanges, SetLocalReplicaMidsetDeletedResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.SetLocalReplicaMidsetDeleted"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0001FABC File Offset: 0x0001DCBC
		public RopResult SetMessageFlags(IServerObject serverObject, StoreId messageId, MessageFlags flags, MessageFlags flagsMask, SetMessageFlagsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetMessageFlags"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetMessageFlags, RopHandlerBase.SetMessageFlagsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetMessageFlags, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetMessageFlags, mapiFolder);
								ropResult = this.SetMessageFlags(this.mapiContext, mapiFolder, RcaTypeHelpers.StoreIdToExchangeId(messageId, logon.StoreMailbox), flags, flagsMask, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnMid(messageId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 154U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 154U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 154U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetMessageFlags, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000D7 RID: 215
		protected abstract RopResult SetMessageFlags(MapiContext context, MapiFolder serverObject, ExchangeId messageId, MessageFlags flags, MessageFlags flagsMask, SetMessageFlagsResultFactory resultFactory);

		// Token: 0x060000D8 RID: 216 RVA: 0x0001FF8C File Offset: 0x0001E18C
		public RopResult SetMessageStatus(IServerObject serverObject, StoreId messageId, MessageStatusFlags status, MessageStatusFlags statusMask, SetMessageStatusResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetMessageStatus"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetMessageStatus, RopHandlerBase.SetMessageStatusClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetMessageStatus, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetMessageStatus, mapiFolder);
								ropResult = this.SetMessageStatus(this.mapiContext, mapiFolder, RcaTypeHelpers.StoreIdToExchangeId(messageId, logon.StoreMailbox), status, statusMask, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnMid(messageId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 32U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 32U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 32U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetMessageStatus, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000D9 RID: 217
		protected abstract RopResult SetMessageStatus(MapiContext context, MapiFolder serverObject, ExchangeId messageId, MessageStatusFlags status, MessageStatusFlags statusMask, SetMessageStatusResultFactory resultFactory);

		// Token: 0x060000DA RID: 218 RVA: 0x00020448 File Offset: 0x0001E648
		public RopResult SetProperties(IServerObject serverObject, PropertyValue[] propertyValues, SetPropertiesResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetProperties"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsSetPropertiesSharedMailboxOperation(mapiPropBagBase);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetProperties, RopHandlerBase.SetPropertiesClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetProperties, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetProperties, mapiPropBagBase);
								ropResult = this.SetProperties(this.mapiContext, mapiPropBagBase, propertyValues, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 10U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 10U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 10U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetProperties, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000DB RID: 219
		protected abstract RopResult SetProperties(MapiContext context, MapiPropBagBase serverObject, PropertyValue[] propertyValues, SetPropertiesResultFactory resultFactory);

		// Token: 0x060000DC RID: 220 RVA: 0x000208F8 File Offset: 0x0001EAF8
		public RopResult SetPropertiesNoReplicate(IServerObject serverObject, PropertyValue[] propertyValues, SetPropertiesNoReplicateResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetPropertiesNoReplicate"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiPropBagBase mapiPropBagBase = serverObject as MapiPropBagBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiPropBagBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiPropBagBase;
					MapiLogon logon = mapiPropBagBase.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsSetPropertiesNoReplicateSharedMailboxOperation(mapiPropBagBase);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetPropertiesNoReplicate, RopHandlerBase.SetPropertiesNoReplicateClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetPropertiesNoReplicate, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetPropertiesNoReplicate, mapiPropBagBase);
								ropResult = this.SetPropertiesNoReplicate(this.mapiContext, mapiPropBagBase, propertyValues, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 121U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 121U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 121U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetPropertiesNoReplicate, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000DD RID: 221
		protected abstract RopResult SetPropertiesNoReplicate(MapiContext context, MapiPropBagBase serverObject, PropertyValue[] propertyValues, SetPropertiesNoReplicateResultFactory resultFactory);

		// Token: 0x060000DE RID: 222 RVA: 0x00020DA8 File Offset: 0x0001EFA8
		public RopResult SetReadFlag(IServerObject serverObject, SetReadFlagFlags flags, SetReadFlagResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetReadFlag"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = 5 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsSetReadFlagSharedMailboxOperation(mapiMessage);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetReadFlag, RopHandlerBase.SetReadFlagClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetReadFlag, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetReadFlag, mapiMessage);
								ropResult = this.SetReadFlag(this.mapiContext, mapiMessage, flags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 17U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 17U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 17U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetReadFlag, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000DF RID: 223
		protected abstract RopResult SetReadFlag(MapiContext context, MapiMessage serverObject, SetReadFlagFlags flags, SetReadFlagResultFactory resultFactory);

		// Token: 0x060000E0 RID: 224 RVA: 0x00021258 File Offset: 0x0001F458
		public RopResult SetReadFlags(IServerObject serverObject, bool reportProgress, SetReadFlagFlags flags, StoreId[] messageIds, SetReadFlagsResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetReadFlags"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				bool isPartiallyCompleted = false;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = 5 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsSetReadFlagsSharedMailboxOperation(mapiFolder);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetReadFlags, RopHandlerBase.SetReadFlagsClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetReadFlags, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetReadFlags, mapiFolder);
								ropResult = this.SetReadFlags(this.mapiContext, mapiFolder, reportProgress, flags, RcaTypeHelpers.StoreIdsToExchangeIds(messageIds, logon.StoreMailbox), out isPartiallyCompleted, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 102U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 102U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 102U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetReadFlags, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000E1 RID: 225
		protected abstract RopResult SetReadFlags(MapiContext context, MapiFolder serverObject, bool reportProgress, SetReadFlagFlags flags, ExchangeId[] messageIds, out bool outputIsPartiallyCompleted, SetReadFlagsResultFactory resultFactory);

		// Token: 0x060000E2 RID: 226 RVA: 0x00021724 File Offset: 0x0001F924
		public RopResult SetReceiveFolder(IServerObject serverObject, StoreId folderId, string messageClass, SetReceiveFolderResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetReceiveFolder"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetReceiveFolder, RopHandlerBase.SetReceiveFolderClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetReceiveFolder, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetReceiveFolder, mapiBase);
								ropResult = this.SetReceiveFolder(this.mapiContext, mapiBase, RcaTypeHelpers.StoreIdToExchangeId(folderId, logon.StoreMailbox), messageClass, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(folderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 38U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 38U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 38U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetReceiveFolder, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227
		protected abstract RopResult SetReceiveFolder(MapiContext context, MapiBase serverObject, ExchangeId folderId, string messageClass, SetReceiveFolderResultFactory resultFactory);

		// Token: 0x060000E4 RID: 228 RVA: 0x00021BDC File Offset: 0x0001FDDC
		public RopResult SetSearchCriteria(IServerObject serverObject, Restriction restriction, StoreId[] folderIds, SetSearchCriteriaFlags setSearchCriteriaFlags, SetSearchCriteriaResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetSearchCriteria"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiFolder mapiFolder = serverObject as MapiFolder;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiFolder == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiFolder;
					MapiLogon logon = mapiFolder.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetSearchCriteria, RopHandlerBase.SetSearchCriteriaClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetSearchCriteria, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetSearchCriteria, mapiFolder);
								ropResult = this.SetSearchCriteria(this.mapiContext, mapiFolder, restriction, RcaTypeHelpers.StoreIdsToExchangeIds(folderIds, logon.StoreMailbox), setSearchCriteriaFlags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 48U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 48U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 48U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetSearchCriteria, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229
		protected abstract RopResult SetSearchCriteria(MapiContext context, MapiFolder serverObject, Restriction restriction, ExchangeId[] folderIds, SetSearchCriteriaFlags setSearchCriteriaFlags, SetSearchCriteriaResultFactory resultFactory);

		// Token: 0x060000E6 RID: 230 RVA: 0x00022088 File Offset: 0x00020288
		public RopResult SetSizeStream(IServerObject serverObject, ulong streamSize, SetSizeStreamResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetSizeStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = 2 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetSizeStream, RopHandlerBase.SetSizeStreamClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetSizeStream, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetSizeStream, mapiStream);
								ropResult = this.SetSizeStream(this.mapiContext, mapiStream, streamSize, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 47U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 47U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 47U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetSizeStream, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000E7 RID: 231
		protected abstract RopResult SetSizeStream(MapiContext context, MapiStream serverObject, ulong streamSize, SetSizeStreamResultFactory resultFactory);

		// Token: 0x060000E8 RID: 232 RVA: 0x00022534 File Offset: 0x00020734
		public RopResult SetSpooler(IServerObject serverObject, SetSpoolerResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetSpooler"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetSpooler, RopHandlerBase.SetSpoolerClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetSpooler, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetSpooler, mapiLogon);
								ropResult = this.SetSpooler(this.mapiContext, mapiLogon, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 71U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 71U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 71U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetSpooler, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000E9 RID: 233
		protected abstract RopResult SetSpooler(MapiContext context, MapiLogon serverObject, SetSpoolerResultFactory resultFactory);

		// Token: 0x060000EA RID: 234 RVA: 0x000229DC File Offset: 0x00020BDC
		public RopResult SetSynchronizationNotificationGuid(IServerObject serverObject, Guid notificationGuid, SetSynchronizationNotificationGuidResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.SetSynchronizationNotificationGuid"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00022A40 File Offset: 0x00020C40
		public RopResult SetTransport(IServerObject serverObject, SetTransportResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SetTransport"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = 1 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SetTransport, RopHandlerBase.SetTransportClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SetTransport, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SetTransport, mapiLogon);
								ropResult = this.SetTransport(this.mapiContext, mapiLogon, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 109U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 109U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 109U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SetTransport, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000EC RID: 236
		protected abstract RopResult SetTransport(MapiContext context, MapiLogon serverObject, SetTransportResultFactory resultFactory);

		// Token: 0x060000ED RID: 237 RVA: 0x00022EE8 File Offset: 0x000210E8
		public RopResult SortTable(IServerObject serverObject, SortTableFlags flags, ushort categoryCount, ushort expandedCount, SortOrder[] sortOrders, SortTableResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SortTable"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiViewTableBase mapiViewTableBase = serverObject as MapiViewTableBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiViewTableBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiViewTableBase;
					MapiLogon logon = mapiViewTableBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SortTable, RopHandlerBase.SortTableClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SortTable, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SortTable, mapiViewTableBase);
								ropResult = this.SortTable(this.mapiContext, mapiViewTableBase, flags, categoryCount, expandedCount, sortOrders, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 19U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 19U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 19U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SortTable, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				if (mapiViewTableBase != null)
				{
					mapiViewTableBase.ConfigurationError.SortTable = ropResult.ErrorCode;
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000EE RID: 238
		protected abstract RopResult SortTable(MapiContext context, MapiViewTableBase serverObject, SortTableFlags flags, ushort categoryCount, ushort expandedCount, SortOrder[] sortOrders, SortTableResultFactory resultFactory);

		// Token: 0x060000EF RID: 239 RVA: 0x000233A0 File Offset: 0x000215A0
		public RopResult SpoolerLockMessage(IServerObject serverObject, StoreId messageId, LockState lockState, SpoolerLockMessageResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SpoolerLockMessage"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SpoolerLockMessage, RopHandlerBase.SpoolerLockMessageClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SpoolerLockMessage, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SpoolerLockMessage, mapiLogon);
								ropResult = this.SpoolerLockMessage(this.mapiContext, mapiLogon, RcaTypeHelpers.StoreIdToExchangeId(messageId, logon.StoreMailbox), lockState, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnMid(messageId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 72U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 72U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 72U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SpoolerLockMessage, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000F0 RID: 240
		protected abstract RopResult SpoolerLockMessage(MapiContext context, MapiLogon serverObject, ExchangeId messageId, LockState lockState, SpoolerLockMessageResultFactory resultFactory);

		// Token: 0x060000F1 RID: 241 RVA: 0x00023858 File Offset: 0x00021A58
		public RopResult SpoolerRules(IServerObject serverObject, StoreId folderId, SpoolerRulesResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.SpoolerRules"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000238BC File Offset: 0x00021ABC
		public RopResult SubmitMessage(IServerObject serverObject, SubmitMessageFlags submitFlags, SubmitMessageResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.SubmitMessage"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.SubmitMessage, RopHandlerBase.SubmitMessageClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.SubmitMessage, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.SubmitMessage, mapiMessage);
								ropResult = this.SubmitMessage(this.mapiContext, mapiMessage, submitFlags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 50U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 50U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 50U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.SubmitMessage, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000F3 RID: 243
		protected abstract RopResult SubmitMessage(MapiContext context, MapiMessage serverObject, SubmitMessageFlags submitFlags, SubmitMessageResultFactory resultFactory);

		// Token: 0x060000F4 RID: 244 RVA: 0x00023D58 File Offset: 0x00021F58
		public RopResult SynchronizationOpenAdvisor(IServerObject serverObject, SynchronizationOpenAdvisorResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.SynchronizationOpenAdvisor"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00023DBC File Offset: 0x00021FBC
		public RopResult TellVersion(IServerObject serverObject, ushort productVersion, ushort buildMajorVersion, ushort buildMinorVersion, TellVersionResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.TellVersion"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				FastTransferContext fastTransferContext = serverObject as FastTransferContext;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (fastTransferContext == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = fastTransferContext;
					MapiLogon logon = fastTransferContext.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.TellVersion, RopHandlerBase.TellVersionClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.TellVersion, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.TellVersion, fastTransferContext);
								ropResult = this.TellVersion(this.mapiContext, fastTransferContext, productVersion, buildMajorVersion, buildMinorVersion, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 134U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 134U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 134U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.TellVersion, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000F6 RID: 246
		protected abstract RopResult TellVersion(MapiContext context, FastTransferContext serverObject, ushort productVersion, ushort buildMajorVersion, ushort buildMinorVersion, TellVersionResultFactory resultFactory);

		// Token: 0x060000F7 RID: 247 RVA: 0x00024274 File Offset: 0x00022474
		public RopResult TransportDeliverMessage(IServerObject serverObject, TransportRecipientType recipientType, TransportDeliverMessageResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.TransportDeliverMessage"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.TransportDeliverMessage, RopHandlerBase.TransportDeliverMessageClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.TransportDeliverMessage, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.TransportDeliverMessage, mapiMessage);
								ropResult = this.TransportDeliverMessage(this.mapiContext, mapiMessage, recipientType, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 148U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 148U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 148U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.TransportDeliverMessage, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248
		protected abstract RopResult TransportDeliverMessage(MapiContext context, MapiMessage serverObject, TransportRecipientType recipientType, TransportDeliverMessageResultFactory resultFactory);

		// Token: 0x060000F9 RID: 249 RVA: 0x00024724 File Offset: 0x00022924
		public RopResult TransportDeliverMessage2(IServerObject serverObject, TransportRecipientType recipientType, TransportDeliverMessage2ResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.TransportDeliverMessage2"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.TransportDeliverMessage2, RopHandlerBase.TransportDeliverMessage2ClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.TransportDeliverMessage2, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.TransportDeliverMessage2, mapiMessage);
								ropResult = this.TransportDeliverMessage2(this.mapiContext, mapiMessage, recipientType, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 158U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 158U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 158U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.TransportDeliverMessage2, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000FA RID: 250
		protected abstract RopResult TransportDeliverMessage2(MapiContext context, MapiMessage serverObject, TransportRecipientType recipientType, TransportDeliverMessage2ResultFactory resultFactory);

		// Token: 0x060000FB RID: 251 RVA: 0x00024BD4 File Offset: 0x00022DD4
		public RopResult TransportDoneWithMessage(IServerObject serverObject, TransportDoneWithMessageResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.TransportDoneWithMessage"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.TransportDoneWithMessage, RopHandlerBase.TransportDoneWithMessageClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.TransportDoneWithMessage, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.TransportDoneWithMessage, mapiMessage);
								ropResult = this.TransportDoneWithMessage(this.mapiContext, mapiMessage, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 149U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 149U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 149U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.TransportDoneWithMessage, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000FC RID: 252
		protected abstract RopResult TransportDoneWithMessage(MapiContext context, MapiMessage serverObject, TransportDoneWithMessageResultFactory resultFactory);

		// Token: 0x060000FD RID: 253 RVA: 0x00025084 File Offset: 0x00023284
		public RopResult TransportDuplicateDeliveryCheck(IServerObject serverObject, byte flags, ExDateTime submitTime, string internetMessageId, TransportDuplicateDeliveryCheckResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.TransportDuplicateDeliveryCheck"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = 3 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.TransportDuplicateDeliveryCheck, RopHandlerBase.TransportDuplicateDeliveryCheckClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.TransportDuplicateDeliveryCheck, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.TransportDuplicateDeliveryCheck, mapiMessage);
								ropResult = this.TransportDuplicateDeliveryCheck(this.mapiContext, mapiMessage, flags, submitTime, internetMessageId, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 161U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 161U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 161U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.TransportDuplicateDeliveryCheck, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x060000FE RID: 254
		protected abstract RopResult TransportDuplicateDeliveryCheck(MapiContext context, MapiMessage serverObject, byte flags, ExDateTime submitTime, string internetMessageId, TransportDuplicateDeliveryCheckResultFactory resultFactory);

		// Token: 0x060000FF RID: 255 RVA: 0x00025548 File Offset: 0x00023748
		public RopResult TransportNewMail(IServerObject serverObject, StoreId folderId, StoreId messageId, string messageClass, MessageFlags messageFlags, TransportNewMailResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.TransportNewMail"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.TransportNewMail, RopHandlerBase.TransportNewMailClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.TransportNewMail, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.TransportNewMail, mapiLogon);
								ropResult = this.TransportNewMail(this.mapiContext, mapiLogon, RcaTypeHelpers.StoreIdToExchangeId(folderId, logon.StoreMailbox), RcaTypeHelpers.StoreIdToExchangeId(messageId, logon.StoreMailbox), messageClass, messageFlags, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.OnMid(messageId);
								mapiExecutionDiagnostics.MapiExMonLogger.OnFid(folderId);
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 81U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 81U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 81U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.TransportNewMail, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000100 RID: 256
		protected abstract RopResult TransportNewMail(MapiContext context, MapiLogon serverObject, ExchangeId folderId, ExchangeId messageId, string messageClass, MessageFlags messageFlags, TransportNewMailResultFactory resultFactory);

		// Token: 0x06000101 RID: 257 RVA: 0x00025A1C File Offset: 0x00023C1C
		public RopResult TransportSend(IServerObject serverObject, TransportSendResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.TransportSend"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiMessage mapiMessage = serverObject as MapiMessage;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiMessage == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiMessage;
					MapiLogon logon = mapiMessage.Logon;
					bool flag = false;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.TransportSend, RopHandlerBase.TransportSendClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.TransportSend, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.TransportSend, mapiMessage);
								ropResult = this.TransportSend(this.mapiContext, mapiMessage, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 74U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 74U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 74U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.TransportSend, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000102 RID: 258
		protected abstract RopResult TransportSend(MapiContext context, MapiMessage serverObject, TransportSendResultFactory resultFactory);

		// Token: 0x06000103 RID: 259 RVA: 0x00025EB8 File Offset: 0x000240B8
		public RopResult UnlockRegionStream(IServerObject serverObject, ulong offset, ulong regionLength, LockTypeFlag lockType, UnlockRegionStreamResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.UnlockRegionStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = 2 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.UnlockRegionStream, RopHandlerBase.UnlockRegionStreamClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.UnlockRegionStream, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.UnlockRegionStream, mapiStream);
								ropResult = this.UnlockRegionStream(this.mapiContext, mapiStream, offset, regionLength, lockType, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 92U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 92U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 92U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.UnlockRegionStream, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000104 RID: 260
		protected abstract RopResult UnlockRegionStream(MapiContext context, MapiStream serverObject, ulong offset, ulong regionLength, LockTypeFlag lockType, UnlockRegionStreamResultFactory resultFactory);

		// Token: 0x06000105 RID: 261 RVA: 0x00026368 File Offset: 0x00024568
		public RopResult UpdateDeferredActionMessages(IServerObject serverObject, byte[] serverEntryId, byte[] clientEntryId, UpdateDeferredActionMessagesResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.UpdateDeferredActionMessages"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U);
			}
			return result;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000263CC File Offset: 0x000245CC
		public RopResult UploadStateStreamBegin(IServerObject serverObject, PropertyTag propertyTag, uint size, UploadStateStreamBeginResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.UploadStateStreamBegin"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.UploadStateStreamBegin, RopHandlerBase.UploadStateStreamBeginClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.UploadStateStreamBegin, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.UploadStateStreamBegin, mapiBase);
								ropResult = this.UploadStateStreamBegin(this.mapiContext, mapiBase, propertyTag, size, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 117U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 117U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 117U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.UploadStateStreamBegin, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000107 RID: 263
		protected abstract RopResult UploadStateStreamBegin(MapiContext context, MapiBase serverObject, PropertyTag propertyTag, uint size, UploadStateStreamBeginResultFactory resultFactory);

		// Token: 0x06000108 RID: 264 RVA: 0x0002686C File Offset: 0x00024A6C
		public RopResult UploadStateStreamContinue(IServerObject serverObject, ArraySegment<byte> data, UploadStateStreamContinueResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.UploadStateStreamContinue"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.UploadStateStreamContinue, RopHandlerBase.UploadStateStreamContinueClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.UploadStateStreamContinue, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.UploadStateStreamContinue, mapiBase);
								ropResult = this.UploadStateStreamContinue(this.mapiContext, mapiBase, data, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 118U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 118U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 118U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.UploadStateStreamContinue, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000109 RID: 265
		protected abstract RopResult UploadStateStreamContinue(MapiContext context, MapiBase serverObject, ArraySegment<byte> data, UploadStateStreamContinueResultFactory resultFactory);

		// Token: 0x0600010A RID: 266 RVA: 0x00026D08 File Offset: 0x00024F08
		public RopResult UploadStateStreamEnd(IServerObject serverObject, UploadStateStreamEndResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.UploadStateStreamEnd"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiBase mapiBase = serverObject as MapiBase;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiBase == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiBase;
					MapiLogon logon = mapiBase.Logon;
					bool flag = true;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.UploadStateStreamEnd, RopHandlerBase.UploadStateStreamEndClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.UploadStateStreamEnd, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.UploadStateStreamEnd, mapiBase);
								ropResult = this.UploadStateStreamEnd(this.mapiContext, mapiBase, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 119U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 119U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 119U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.UploadStateStreamEnd, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600010B RID: 267
		protected abstract RopResult UploadStateStreamEnd(MapiContext context, MapiBase serverObject, UploadStateStreamEndResultFactory resultFactory);

		// Token: 0x0600010C RID: 268 RVA: 0x000271A4 File Offset: 0x000253A4
		public RopResult WriteCommitStream(IServerObject serverObject, byte[] data, WriteCommitStreamResultFactory resultFactory)
		{
			RopResult result;
			using (this.CreateThreadManagerMethodFrame("MapiRop.WriteCommitStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ushort byteCount = 0;
				result = resultFactory.CreateFailedResult((ErrorCode)2147746050U, byteCount);
			}
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0002720C File Offset: 0x0002540C
		public RopResult WritePerUserInformation(IServerObject serverObject, StoreLongTermId longTermId, bool hasFinished, uint dataOffset, byte[] data, Guid? replicaGuid, WritePerUserInformationResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.WritePerUserInformation"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiLogon mapiLogon = serverObject as MapiLogon;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				if (mapiLogon == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiLogon;
					MapiLogon logon = mapiLogon.Logon;
					bool flag = 5 <= ConfigurationSchema.ConfigurableSharedLockStage.Value && this.IsWritePerUserInformationSharedMailboxOperation(hasFinished);
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = false;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.WritePerUserInformation, RopHandlerBase.WritePerUserInformationClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.WritePerUserInformation, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.WritePerUserInformation, mapiLogon);
								ropResult = this.WritePerUserInformation(this.mapiContext, mapiLogon, longTermId, hasFinished, dataOffset, data, replicaGuid, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 100U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 100U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 100U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.WritePerUserInformation, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x0600010E RID: 270
		protected abstract RopResult WritePerUserInformation(MapiContext context, MapiLogon serverObject, StoreLongTermId longTermId, bool hasFinished, uint dataOffset, byte[] data, Guid? replicaGuid, WritePerUserInformationResultFactory resultFactory);

		// Token: 0x0600010F RID: 271 RVA: 0x000276C8 File Offset: 0x000258C8
		public RopResult WriteStream(IServerObject serverObject, ArraySegment<byte> data, WriteStreamResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.WriteStream"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				ushort byteCount = 0;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = 2 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.WriteStream, RopHandlerBase.WriteStreamClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.WriteStream, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.WriteStream, mapiStream);
								ropResult = this.WriteStream(this.mapiContext, mapiStream, data, out byteCount, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 45U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 45U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 45U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.WriteStream, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, byteCount);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000110 RID: 272
		protected abstract RopResult WriteStream(MapiContext context, MapiStream serverObject, ArraySegment<byte> data, out ushort outputByteCount, WriteStreamResultFactory resultFactory);

		// Token: 0x06000111 RID: 273 RVA: 0x00027B78 File Offset: 0x00025D78
		public RopResult WriteStreamExtended(IServerObject serverObject, ArraySegment<byte>[] dataChunks, WriteStreamExtendedResultFactory resultFactory)
		{
			RopResult result;
			using (ThreadManager.MethodFrame methodFrame = this.CreateThreadManagerMethodFrame("MapiRop.WriteStreamExtended"))
			{
				if (resultFactory == null)
				{
					throw new ArgumentNullException("resultFactory");
				}
				if (serverObject == null)
				{
					throw new ArgumentNullException("serverObject");
				}
				ErrorCode errorCode = (ErrorCode)2147500037U;
				RopResult ropResult = null;
				MapiStream mapiStream = serverObject as MapiStream;
				MapiExecutionDiagnostics mapiExecutionDiagnostics = (MapiExecutionDiagnostics)this.mapiContext.Diagnostics;
				uint byteCount = 0U;
				if (mapiStream == null)
				{
					errorCode = (ErrorCode)2147746050U;
				}
				else
				{
					mapiExecutionDiagnostics.MapiObject = mapiStream;
					MapiLogon logon = mapiStream.Logon;
					bool flag = 2 <= ConfigurationSchema.ConfigurableSharedLockStage.Value;
					bool flag2 = logon.IsDeferedReleaseSharedOperation();
					bool flag3 = true;
					this.mapiContext.Initialize(logon, flag && flag2, flag3);
					methodFrame.CurrentThreadInfo.MailboxGuid = logon.MailboxGuid;
					logon.DatabaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(this.mapiContext, logon.DatabaseInfo.MdbGuid);
					logon.LoggedOnUserAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.LoggedOnUserAddressInfo.ObjectId);
					logon.MailboxOwnerAddressInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetAddressInfoByObjectId(this.mapiContext, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxOwnerAddressInfo.ObjectId);
					if (!logon.MailboxInfo.IsDisconnected)
					{
						logon.MailboxInfo = RopHandlerBase.GetMailboxInfo(this.mapiContext, logon);
						if (!RopHandlerBase.ValidateMailboxType(logon.MapiMailbox.SharedState, logon.MailboxInfo))
						{
							throw new StoreException((LID)62412U, ErrorCodeValue.UnexpectedMailboxState);
						}
					}
					try
					{
						errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
						if (errorCode == ErrorCode.None)
						{
							bool commit = false;
							try
							{
								logon.ProcessDeferedReleaseROPs(this.mapiContext);
								if (!flag2 && flag3)
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(true, true);
									}
									this.mapiContext.Initialize(logon, flag, flag3);
									errorCode = (ErrorCode)this.mapiContext.StartMailboxOperation(MailboxCreation.DontAllow, false, false, true);
								}
								if (errorCode != ErrorCode.None)
								{
									throw new StoreException((LID)60236U, (ErrorCodeValue)errorCode);
								}
								this.CheckClientTypeIsAllowedOnMoveTarget(logon, RopId.WriteStreamExtended, RopHandlerBase.WriteStreamExtendedClientTypesAllowedOnMoveTarget);
								if (this.mapiContext.Database.IsReadOnly)
								{
									RopHandlerBase.CheckClientTypeIsAllowedOnReadOnlyDatabase(this.mapiContext, RopId.WriteStreamExtended, null);
								}
								this.mapiContext.SkipDatabaseLogsFlush = RopHandlerBase.SkipDatabaseLogFlush(RopId.WriteStreamExtended, mapiStream);
								ropResult = this.WriteStreamExtended(this.mapiContext, mapiStream, dataChunks, out byteCount, resultFactory);
								commit = true;
							}
							finally
							{
								mapiExecutionDiagnostics.MapiExMonLogger.AccessedMailboxLegacyDn = logon.MailboxOwnerAddressInfo.LegacyExchangeDN;
								try
								{
									if (this.mapiContext.IsMailboxOperationStarted)
									{
										this.mapiContext.EndMailboxOperation(commit, true);
									}
								}
								finally
								{
									this.mapiContext.SkipDatabaseLogsFlush = false;
								}
							}
						}
					}
					catch (StoreException ex)
					{
						this.mapiContext.OnExceptionCatch(ex);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)63016U, 163U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)39640U, ex);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = (ErrorCode)ex.Error;
					}
					catch (RopExecutionException ex2)
					{
						this.mapiContext.OnExceptionCatch(ex2);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)38440U, 163U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)56024U, ex2);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ex2.ErrorCode;
					}
					catch (BufferParseException exception)
					{
						this.mapiContext.OnExceptionCatch(exception);
						ropResult = null;
						DiagnosticContext.TraceDword((LID)54824U, 163U);
						ErrorHelper.TraceException(Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp.ExTraceGlobals.RpcOperationTracer, (LID)43736U, exception);
						if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
						{
							throw;
						}
						errorCode = ErrorCode.RpcFormat;
					}
				}
				this.AssertSessionIsNotTerminating(RopId.WriteStreamExtended, errorCode, ropResult);
				if (ropResult == null)
				{
					if (errorCode == (ErrorCode)2147746817U)
					{
						throw new StoreException((LID)58656U, (ErrorCodeValue)errorCode);
					}
					ropResult = resultFactory.CreateFailedResult(errorCode, byteCount);
				}
				if (ropResult.ErrorCode != ErrorCode.None)
				{
					ropResult.SetDiagnosticInfoProvider(mapiExecutionDiagnostics);
				}
				result = ropResult;
			}
			return result;
		}

		// Token: 0x06000112 RID: 274
		protected abstract RopResult WriteStreamExtended(MapiContext context, MapiStream serverObject, ArraySegment<byte>[] dataChunks, out uint outputByteCount, WriteStreamExtendedResultFactory resultFactory);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00028040 File Offset: 0x00026240
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00028048 File Offset: 0x00026248
		public MapiContext MapiContext
		{
			get
			{
				return this.mapiContext;
			}
			set
			{
				this.mapiContext = value;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00028054 File Offset: 0x00026254
		internal static bool SkipDatabaseLogFlush(RopId ropId, MapiBase mapiObject)
		{
			if (ropId <= RopId.FindRow)
			{
				if (ropId <= RopId.SaveChangesAttachment)
				{
					if (ropId == RopId.Release)
					{
						return !(mapiObject is MapiStream);
					}
					switch (ropId)
					{
					case RopId.SaveChangesMessage:
						return ((MapiMessage)mapiObject).StoreMessage.IsEmbedded;
					case RopId.RemoveAllRecipients:
					case RopId.FlushRecipients:
					case RopId.ReadRecipients:
					case RopId.ReloadCachedInformation:
					case RopId.GetStatus:
					case RopId.CreateFolder:
					case RopId.DeleteFolder:
					case RopId.DeleteMessages:
					case RopId.SetMessageStatus:
					case RopId.GetAttachmentTable:
					case RopId.OpenAttachment:
						return false;
					case RopId.SetReadFlag:
					case RopId.SetColumns:
					case RopId.SortTable:
					case RopId.Restrict:
					case RopId.QueryRows:
					case RopId.QueryPosition:
					case RopId.SeekRow:
					case RopId.SeekRowBookmark:
					case RopId.SeekRowApproximate:
					case RopId.CreateBookmark:
					case RopId.GetMessageStatus:
					case RopId.CreateAttachment:
					case RopId.DeleteAttachment:
					case RopId.SaveChangesAttachment:
						break;
					default:
						return false;
					}
				}
				else if (ropId != RopId.OpenEmbeddedMessage)
				{
					switch (ropId)
					{
					case RopId.FastTransferSourceGetBuffer:
					case RopId.FindRow:
						break;
					default:
						return false;
					}
				}
			}
			else if (ropId <= RopId.FastTransferGetIncrementalState)
			{
				switch (ropId)
				{
				case RopId.ExpandRow:
				case RopId.CollapseRow:
					break;
				default:
					if (ropId != RopId.FastTransferGetIncrementalState)
					{
						return false;
					}
					break;
				}
			}
			else if (ropId != RopId.FastTransferSourceGetBufferExtended && ropId != RopId.TransportDuplicateDeliveryCheck && ropId != RopId.Logon)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0002815E File Offset: 0x0002635E
		internal static bool SkipHomeMdbValidation(MapiContext context, OpenStoreFlags openStoreFlags)
		{
			return (context.ClientType == ClientType.Migration || ClientTypeHelper.IsContentIndexing(context.ClientType)) && OpenStoreFlags.None != (openStoreFlags & OpenStoreFlags.OverrideHomeMdb);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00028184 File Offset: 0x00026384
		internal static MailboxInfo GetMailboxInfo(MapiContext context, MapiLogon logon)
		{
			GetMailboxInfoFlags flags = GetMailboxInfoFlags.None;
			if (RopHandlerBase.SkipHomeMdbValidation(context, logon.OpenStoreFlags))
			{
				flags = GetMailboxInfoFlags.IgnoreHomeMdb;
			}
			return Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetMailboxInfo(context, logon.MapiMailbox.SharedState.TenantHint, logon.MailboxInfo.MailboxGuid, flags);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000281CC File Offset: 0x000263CC
		protected static void CheckClientTypeIsAllowedOnReadOnlyDatabase(MapiContext context, RopId ropId, IList<ClientType> clientTypesAllowedOnReadOnlyDatabase)
		{
			if (clientTypesAllowedOnReadOnlyDatabase != null && clientTypesAllowedOnReadOnlyDatabase.Contains(context.ClientType))
			{
				return;
			}
			string message = string.Format("Client {0} is not allowed to execute Rop {1} on read-only databases.", context.ClientType, ropId);
			DiagnosticContext.TraceDword((LID)45180U, (uint)context.ClientType);
			DiagnosticContext.TraceDword((LID)61564U, (uint)ropId);
			if (context.Database.IsOnlinePassiveAttachedReadOnly)
			{
				throw new StoreException((LID)36988U, ErrorCodeValue.MdbNotInitialized, message);
			}
			throw new ExExceptionAccessDenied((LID)38652U, message);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00028260 File Offset: 0x00026460
		protected static bool ValidateMailboxType(MailboxState mailboxState, MailboxInfo mailboxInfo)
		{
			return (mailboxState.MailboxType != MailboxInfo.MailboxType.PublicFolderPrimary && mailboxState.MailboxType != MailboxInfo.MailboxType.PublicFolderSecondary && mailboxInfo.Type != MailboxInfo.MailboxType.PublicFolderPrimary && mailboxInfo.Type != MailboxInfo.MailboxType.PublicFolderSecondary) || !ConfigurationSchema.ValidatePublicFolderMailboxTypeMatch.Value || mailboxState.MailboxType == mailboxInfo.Type;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000282B0 File Offset: 0x000264B0
		private static void CheckGetContentsTableExtendedConditionsForReadOnlyDatabase(MapiContext context, MapiFolder mapiFolder, ExtendedTableFlags extendedTableFlags)
		{
			if ((extendedTableFlags & ExtendedTableFlags.DocumentIdView) == ExtendedTableFlags.None)
			{
				DiagnosticContext.TraceDword((LID)47612U, (uint)context.ClientType);
				DiagnosticContext.TraceDword((LID)63996U, (uint)extendedTableFlags);
				throw new ExExceptionNoSupport((LID)55804U, string.Format("Only DocumentId view is supported when calling GetContentsTableExtended on read-only databases (ClientType={0}).", context.ClientType));
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00028310 File Offset: 0x00026510
		private static void CheckGetIdsFromNamesConditionsForReadOnlyDatabase(MapiContext context, MapiBase mapiBase, GetIdsFromNamesFlags flags, NamedProperty[] namedProperties)
		{
			if ((byte)(flags & GetIdsFromNamesFlags.Create) != 0)
			{
				DiagnosticContext.TraceDword((LID)43516U, (uint)context.ClientType);
				DiagnosticContext.TraceDword((LID)59900U, (uint)flags);
				throw new ExExceptionNoSupport((LID)35324U, string.Format("The Create flag is not supported when calling GetIdsFromNames on read-only databases (ClientType={0}).", context.ClientType));
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00028370 File Offset: 0x00026570
		private static void CheckGetPropertiesAllConditionsForReadOnlyDatabase(MapiContext context, MapiPropBagBase propertyBag, ushort streamLimit, GetPropertiesFlags flags)
		{
			if (propertyBag.MapiObjectType == MapiObjectType.Logon)
			{
				DiagnosticContext.TraceDword((LID)51708U, (uint)context.ClientType);
				throw new ExExceptionNoSupport((LID)45564U, string.Format("GetPropertiesAll for MapiLogon is not currently supported on read-only databases (ClientType={0}).", context.ClientType));
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000283C0 File Offset: 0x000265C0
		private static void CheckGetPropertiesSpecificConditionsForReadOnlyDatabase(MapiContext context, MapiPropBagBase propertyBag, ushort streamLimit, GetPropertiesFlags flags, PropertyTag[] propertyTags)
		{
			if (RopHandlerBase.IsRetrievingLocalDirectoryEntryIdFromMapiLogon(propertyBag, propertyTags))
			{
				DiagnosticContext.TraceDword((LID)61948U, (uint)context.ClientType);
				throw new ExExceptionNoSupport((LID)37372U, string.Format("GetPropertiesSpecific for LocalDirectoryEntryId from MapiLogon is not currently supported on read-only databases (ClientType={0}).", context.ClientType));
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00028414 File Offset: 0x00026614
		private static bool IsRetrievingLocalDirectoryEntryIdFromMapiLogon(MapiPropBagBase propertyBag, IList<PropertyTag> propertyTags)
		{
			if (propertyBag.MapiObjectType == MapiObjectType.Logon && propertyTags != null)
			{
				foreach (PropertyTag propertyTag in propertyTags)
				{
					if (propertyTag.PropertyId == (PropertyId)13334)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00028478 File Offset: 0x00026678
		private static ErrorCode ErrorToThrowForSessionTermination(ErrorCode error, RopResult result)
		{
			if (error == ErrorCode.None || error == (ErrorCode)2147500037U)
			{
				error = (ErrorCode)2147746069U;
				if (result != null && result.ErrorCode != ErrorCode.None)
				{
					error = result.ErrorCode;
				}
			}
			return ErrorCode.CreateWithLid((LID)61848U, (ErrorCodeValue)error);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000284AF File Offset: 0x000266AF
		private static bool IsSetReadStatusSharedLock(Folder folder)
		{
			return folder.IsPerUserReadUnreadTrackingEnabled;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000284B8 File Offset: 0x000266B8
		internal void CheckClientTypeIsAllowedOnMoveTarget(MapiLogon logon, RopId ropId, ClientType[] clientTypesAllowedOnMoveTarget)
		{
			if (PropertyBagHelpers.TestPropertyFlags(this.mapiContext, logon.StoreMailbox, PropTag.Mailbox.MailboxFlags, 16, 16))
			{
				bool flag = false;
				foreach (ClientType clientType in clientTypesAllowedOnMoveTarget)
				{
					if (this.mapiContext.ClientType == clientType)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					DiagnosticContext.TraceDword((LID)40104U, (uint)this.mapiContext.ClientType);
					DiagnosticContext.TraceDword((LID)56488U, (uint)ropId);
					throw new ExExceptionNoSupport((LID)44200U, string.Format("Client {0} is not allowed to execute Rop {1} on the target of a mailbox move.", this.mapiContext.ClientType, ropId));
				}
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00028569 File Offset: 0x00026769
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RopHandlerBase>(this);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00028571 File Offset: 0x00026771
		protected override void InternalDispose(bool calledFromDispose)
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00028573 File Offset: 0x00026773
		private void AssertSessionIsNotTerminating(RopId ropId, ErrorCode error, RopResult result)
		{
			if (this.mapiContext.HighestFailedCriticalBlockScope >= CriticalBlockScope.MailboxSession)
			{
				DiagnosticContext.TraceDword((LID)35256U, (uint)ropId);
				throw new StoreException((LID)59448U, RopHandlerBase.ErrorToThrowForSessionTermination(error, result));
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000285AF File Offset: 0x000267AF
		private bool IsGetPropertiesAllSharedMailboxOperation(MapiPropBagBase propertyBag)
		{
			return propertyBag.MapiObjectType != MapiObjectType.Logon;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000285BD File Offset: 0x000267BD
		private bool IsGetPropertiesSpecificSharedMailboxOperation(MapiPropBagBase propertyBag, ushort streamLimit, GetPropertiesFlags flags, PropertyTag[] propertyTags)
		{
			return !RopHandlerBase.IsRetrievingLocalDirectoryEntryIdFromMapiLogon(propertyBag, propertyTags);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000285CA File Offset: 0x000267CA
		private bool IsImportReadsSharedMailboxOperation(IcsContentUploadContext uploadContext)
		{
			return this.IsSetReadFlagsSharedMailboxOperation(uploadContext.ParentObject as MapiFolder);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000285DD File Offset: 0x000267DD
		private bool IsCopyPropertiesSharedMailboxOperation(MapiPropBagBase mapiPropBag)
		{
			return mapiPropBag.CanUseSharedMailboxLockForCopy;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000285E5 File Offset: 0x000267E5
		private bool IsCopyToSharedMailboxOperation(MapiPropBagBase mapiPropBag)
		{
			return mapiPropBag.CanUseSharedMailboxLockForCopy;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000285ED File Offset: 0x000267ED
		private bool IsSaveChangesAttachmentSharedMailboxOperation(MapiAttachment mapiAttachment)
		{
			return mapiAttachment.CanUseSharedMailboxLockForSave;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000285F5 File Offset: 0x000267F5
		private bool IsSaveChangesMessageSharedMailboxOperation(MapiMessage mapiMessage)
		{
			return mapiMessage.CanUseSharedMailboxLockForSave;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00028600 File Offset: 0x00026800
		private bool IsSetReadFlagSharedMailboxOperation(MapiMessage mapiMessage)
		{
			if (mapiMessage.StoreMessage.IsEmbedded)
			{
				return true;
			}
			Folder parentFolder = ((TopMessage)mapiMessage.StoreMessage).ParentFolder;
			return RopHandlerBase.IsSetReadStatusSharedLock(parentFolder);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00028633 File Offset: 0x00026833
		private bool IsSetReadFlagsSharedMailboxOperation(MapiFolder mapiFolder)
		{
			return RopHandlerBase.IsSetReadStatusSharedLock(mapiFolder.StoreFolder);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00028640 File Offset: 0x00026840
		private bool IsWritePerUserInformationSharedMailboxOperation(bool hasFinished)
		{
			return !hasFinished;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00028648 File Offset: 0x00026848
		private bool IsReleaseSharedMailboxOperation(MapiBase serverObject)
		{
			MapiStream mapiStream = serverObject as MapiStream;
			return mapiStream == null || !mapiStream.ReleaseMayNeedExclusiveLock;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0002866A File Offset: 0x0002686A
		private bool IsPropertyChangeSharedMailboxOperation(MapiPropBagBase propertyBag)
		{
			return propertyBag is MapiAttachment || (propertyBag is MapiMessage && ConfigurationSchema.ConfigurableSharedLockStage.Value >= 5 && this.IsSetReadFlagSharedMailboxOperation((MapiMessage)propertyBag));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00028699 File Offset: 0x00026899
		private bool IsDeletePropertiesSharedMailboxOperation(MapiPropBagBase propertyBag)
		{
			return this.IsPropertyChangeSharedMailboxOperation(propertyBag);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000286A2 File Offset: 0x000268A2
		private bool IsDeletePropertiesNoReplicateSharedMailboxOperation(MapiPropBagBase propertyBag)
		{
			return this.IsPropertyChangeSharedMailboxOperation(propertyBag);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000286AB File Offset: 0x000268AB
		private bool IsSetPropertiesSharedMailboxOperation(MapiPropBagBase propertyBag)
		{
			return this.IsPropertyChangeSharedMailboxOperation(propertyBag) || (ConfigurationSchema.ConfigurableSharedLockStage.Value >= 6 && propertyBag is FastTransferStream);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000286D0 File Offset: 0x000268D0
		private bool IsSetPropertiesNoReplicateSharedMailboxOperation(MapiPropBagBase propertyBag)
		{
			return this.IsSetPropertiesSharedMailboxOperation(propertyBag);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000286D9 File Offset: 0x000268D9
		private bool IsCommitStreamSharedMailboxOperation(MapiStream stream)
		{
			return stream.ParentObject is MapiMessage || stream.ParentObject is MapiAttachment;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000286F8 File Offset: 0x000268F8
		private ThreadManager.MethodFrame CreateThreadManagerMethodFrame(string currentMethodName)
		{
			ThreadManager.ThreadInfo threadInfo;
			ThreadManager.MethodFrame result = ThreadManager.NewMethodFrame(currentMethodName, out threadInfo);
			bool flag = false;
			try
			{
				if (this.mapiContext != null)
				{
					if (this.mapiContext.Session != null)
					{
						threadInfo.Client = this.mapiContext.Session.ApplicationId;
						threadInfo.UserGuid = this.mapiContext.Session.UserGuid;
						threadInfo.User = this.mapiContext.Session.UserDN;
					}
					else
					{
						threadInfo.UserGuid = this.mapiContext.UserIdentity;
					}
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					result.Dispose();
				}
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private const ClientType[] AbortSubmitClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000002 RID: 2
		private const ClientType[] AddressTypesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000003 RID: 3
		private const ClientType[] CloneStreamClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000004 RID: 4
		private const ClientType[] CollapseRowClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000005 RID: 5
		private const ClientType[] CommitStreamClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000006 RID: 6
		private const ClientType[] CopyFolderClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000007 RID: 7
		private const ClientType[] CopyPropertiesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000008 RID: 8
		private const ClientType[] CopyToClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000009 RID: 9
		private const ClientType[] CreateAttachmentClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400000A RID: 10
		private const ClientType[] CreateBookmarkClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400000B RID: 11
		private const ClientType[] CreateFolderClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400000C RID: 12
		private const ClientType[] CreateMessageClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400000D RID: 13
		private const ClientType[] CreateMessageExtendedClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400000E RID: 14
		private const ClientType[] DeleteAttachmentClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400000F RID: 15
		private const ClientType[] DeleteFolderClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000010 RID: 16
		private const ClientType[] DeleteMessagesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000011 RID: 17
		private const ClientType[] DeletePropertiesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000012 RID: 18
		private const ClientType[] DeletePropertiesNoReplicateClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000013 RID: 19
		private const ClientType[] EmptyFolderClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000014 RID: 20
		private const ClientType[] ExpandRowClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000015 RID: 21
		private const ClientType[] FastTransferDestinationCopyOperationConfigureClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000016 RID: 22
		private const ClientType[] FastTransferDestinationPutBufferClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000017 RID: 23
		private const ClientType[] FastTransferDestinationPutBufferExtendedClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000018 RID: 24
		private const ClientType[] FastTransferGetIncrementalStateClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000019 RID: 25
		private const ClientType[] FastTransferSourceCopyFolderClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400001A RID: 26
		private const ClientType[] FastTransferSourceCopyMessagesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400001B RID: 27
		private const ClientType[] FastTransferSourceCopyPropertiesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400001C RID: 28
		private const ClientType[] FastTransferSourceCopyToClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400001D RID: 29
		private const ClientType[] FastTransferSourceGetBufferClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400001E RID: 30
		private const ClientType[] FastTransferSourceGetBufferExtendedClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400001F RID: 31
		private const ClientType[] FlushRecipientsClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000020 RID: 32
		private const ClientType[] FreeBookmarkClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000021 RID: 33
		private const ClientType[] GetAllPerUserLongTermIdsClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000022 RID: 34
		private const ClientType[] GetCollapseStateClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000023 RID: 35
		private const ClientType[] GetContentsTableClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000024 RID: 36
		private const ClientType[] GetLocalReplicationIdsClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000025 RID: 37
		private const ClientType[] GetMessageStatusClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000026 RID: 38
		private const ClientType[] GetPerUserGuidClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000027 RID: 39
		private const ClientType[] GetPerUserLongTermIdsClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000028 RID: 40
		private const ClientType[] GetReceiveFolderClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000029 RID: 41
		private const ClientType[] GetReceiveFolderTableClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400002A RID: 42
		private const ClientType[] GetSearchCriteriaClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400002B RID: 43
		private const ClientType[] GetStreamSizeClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400002C RID: 44
		private const ClientType[] HardDeleteMessagesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400002D RID: 45
		private const ClientType[] HardEmptyFolderClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400002E RID: 46
		private const ClientType[] ImportDeleteClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400002F RID: 47
		private const ClientType[] ImportHierarchyChangeClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000030 RID: 48
		private const ClientType[] ImportMessageChangeClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000031 RID: 49
		private const ClientType[] ImportMessageChangePartialClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000032 RID: 50
		private const ClientType[] ImportMessageMoveClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000033 RID: 51
		private const ClientType[] ImportReadsClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000034 RID: 52
		private const ClientType[] IncrementalConfigClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000035 RID: 53
		private const ClientType[] LockRegionStreamClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000036 RID: 54
		private const ClientType[] LogonClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000037 RID: 55
		private const ClientType[] MoveCopyMessagesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000038 RID: 56
		private const ClientType[] MoveCopyMessagesExtendedClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000039 RID: 57
		private const ClientType[] MoveCopyMessagesExtendedWithEntryIdsClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400003A RID: 58
		private const ClientType[] MoveFolderClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400003B RID: 59
		private const ClientType[] OpenCollectorClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400003C RID: 60
		private const ClientType[] PrereadMessagesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400003D RID: 61
		private const ClientType[] QueryColumnsAllClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400003E RID: 62
		private const ClientType[] QueryNamedPropertiesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400003F RID: 63
		private const ClientType[] QueryPositionClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000040 RID: 64
		private const ClientType[] ReadPerUserInformationClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000041 RID: 65
		private const ClientType[] ReleaseClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000042 RID: 66
		private const ClientType[] ReloadCachedInformationClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000043 RID: 67
		private const ClientType[] RemoveAllRecipientsClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000044 RID: 68
		private const ClientType[] ResetTableClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000045 RID: 69
		private const ClientType[] SaveChangesAttachmentClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000046 RID: 70
		private const ClientType[] SaveChangesMessageClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000047 RID: 71
		private const ClientType[] SeekRowClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000048 RID: 72
		private const ClientType[] SeekRowApproximateClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000049 RID: 73
		private const ClientType[] SeekRowBookmarkClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400004A RID: 74
		private const ClientType[] SetCollapseStateClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400004B RID: 75
		private const ClientType[] SetMessageFlagsClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400004C RID: 76
		private const ClientType[] SetMessageStatusClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400004D RID: 77
		private const ClientType[] SetPropertiesClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400004E RID: 78
		private const ClientType[] SetPropertiesNoReplicateClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400004F RID: 79
		private const ClientType[] SetReadFlagClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000050 RID: 80
		private const ClientType[] SetReadFlagsClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000051 RID: 81
		private const ClientType[] SetReceiveFolderClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000052 RID: 82
		private const ClientType[] SetSearchCriteriaClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000053 RID: 83
		private const ClientType[] SetSizeStreamClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000054 RID: 84
		private const ClientType[] SetSpoolerClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000055 RID: 85
		private const ClientType[] SetTransportClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000056 RID: 86
		private const ClientType[] SortTableClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000057 RID: 87
		private const ClientType[] SpoolerLockMessageClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000058 RID: 88
		private const ClientType[] SubmitMessageClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000059 RID: 89
		private const ClientType[] TellVersionClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400005A RID: 90
		private const ClientType[] TransportDeliverMessageClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400005B RID: 91
		private const ClientType[] TransportDeliverMessage2ClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400005C RID: 92
		private const ClientType[] TransportDoneWithMessageClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400005D RID: 93
		private const ClientType[] TransportDuplicateDeliveryCheckClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400005E RID: 94
		private const ClientType[] TransportNewMailClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x0400005F RID: 95
		private const ClientType[] TransportSendClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000060 RID: 96
		private const ClientType[] UnlockRegionStreamClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000061 RID: 97
		private const ClientType[] UploadStateStreamBeginClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000062 RID: 98
		private const ClientType[] UploadStateStreamContinueClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000063 RID: 99
		private const ClientType[] UploadStateStreamEndClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000064 RID: 100
		private const ClientType[] WritePerUserInformationClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000065 RID: 101
		private const ClientType[] WriteStreamClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000066 RID: 102
		private const ClientType[] WriteStreamExtendedClientTypesAllowedOnReadOnlyDatabase = null;

		// Token: 0x04000067 RID: 103
		private static readonly ClientType[] AbortSubmitClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000068 RID: 104
		private static readonly ClientType[] AddressTypesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000069 RID: 105
		private static readonly ClientType[] CloneStreamClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400006A RID: 106
		private static readonly ClientType[] CollapseRowClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400006B RID: 107
		private static readonly ClientType[] CommitStreamClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400006C RID: 108
		private static readonly ClientType[] CopyFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400006D RID: 109
		private static readonly ClientType[] CopyPropertiesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400006E RID: 110
		private static readonly ClientType[] CopyToClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400006F RID: 111
		private static readonly ClientType[] CreateAttachmentClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000070 RID: 112
		private static readonly ClientType[] CreateBookmarkClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000071 RID: 113
		private static readonly ClientType[] CreateFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000072 RID: 114
		private static readonly ClientType[] CreateMessageClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000073 RID: 115
		private static readonly ClientType[] CreateMessageExtendedClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000074 RID: 116
		private static readonly ClientType[] DeleteAttachmentClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000075 RID: 117
		private static readonly ClientType[] DeleteFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000076 RID: 118
		private static readonly ClientType[] DeleteMessagesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000077 RID: 119
		private static readonly ClientType[] DeletePropertiesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000078 RID: 120
		private static readonly ClientType[] DeletePropertiesNoReplicateClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000079 RID: 121
		private static readonly ClientType[] EmptyFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400007A RID: 122
		private static readonly ClientType[] ExpandRowClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400007B RID: 123
		private static readonly ClientType[] FastTransferDestinationCopyOperationConfigureClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400007C RID: 124
		private static readonly ClientType[] FastTransferDestinationPutBufferClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400007D RID: 125
		private static readonly ClientType[] FastTransferDestinationPutBufferExtendedClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400007E RID: 126
		private static readonly ClientType[] FastTransferGetIncrementalStateClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400007F RID: 127
		private static readonly ClientType[] FastTransferSourceCopyFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000080 RID: 128
		private static readonly ClientType[] FastTransferSourceCopyMessagesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000081 RID: 129
		private static readonly ClientType[] FastTransferSourceCopyPropertiesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000082 RID: 130
		private static readonly ClientType[] FastTransferSourceCopyToClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000083 RID: 131
		private static readonly ClientType[] FastTransferSourceGetBufferClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000084 RID: 132
		private static readonly ClientType[] FastTransferSourceGetBufferExtendedClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000085 RID: 133
		private static readonly ClientType[] FindRowClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x04000086 RID: 134
		private static readonly ClientType[] FindRowClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x04000087 RID: 135
		private static readonly ClientType[] FlushRecipientsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000088 RID: 136
		private static readonly ClientType[] FreeBookmarkClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000089 RID: 137
		private static readonly ClientType[] GetAllPerUserLongTermIdsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400008A RID: 138
		private static readonly ClientType[] GetAttachmentTableClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x0400008B RID: 139
		private static readonly ClientType[] GetAttachmentTableClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x0400008C RID: 140
		private static readonly ClientType[] GetCollapseStateClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400008D RID: 141
		private static readonly ClientType[] GetContentsTableClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400008E RID: 142
		private static readonly ClientType[] GetContentsTableExtendedClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x0400008F RID: 143
		private static readonly ClientType[] GetContentsTableExtendedClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x04000090 RID: 144
		private static readonly ClientType[] GetHierarchyTableClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x04000091 RID: 145
		private static readonly ClientType[] GetHierarchyTableClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination,
			ClientType.StoreActiveMonitoring
		};

		// Token: 0x04000092 RID: 146
		private static readonly ClientType[] GetIdsFromNamesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x04000093 RID: 147
		private static readonly ClientType[] GetIdsFromNamesClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination,
			ClientType.StoreActiveMonitoring
		};

		// Token: 0x04000094 RID: 148
		private static readonly ClientType[] GetLocalReplicationIdsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000095 RID: 149
		private static readonly ClientType[] GetMessageStatusClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000096 RID: 150
		private static readonly ClientType[] GetNamesFromIDsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x04000097 RID: 151
		private static readonly ClientType[] GetNamesFromIDsClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x04000098 RID: 152
		private static readonly ClientType[] GetPerUserGuidClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x04000099 RID: 153
		private static readonly ClientType[] GetPerUserLongTermIdsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x0400009A RID: 154
		private static readonly ClientType[] GetPropertiesAllClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x0400009B RID: 155
		private static readonly ClientType[] GetPropertiesAllClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x0400009C RID: 156
		private static readonly ClientType[] GetPropertiesSpecificClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x0400009D RID: 157
		private static readonly ClientType[] GetPropertiesSpecificClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination,
			ClientType.StoreActiveMonitoring
		};

		// Token: 0x0400009E RID: 158
		private static readonly ClientType[] GetPropertyListClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x0400009F RID: 159
		private static readonly ClientType[] GetPropertyListClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000A0 RID: 160
		private static readonly ClientType[] GetReceiveFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000A1 RID: 161
		private static readonly ClientType[] GetReceiveFolderTableClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000A2 RID: 162
		private static readonly ClientType[] GetSearchCriteriaClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000A3 RID: 163
		private static readonly ClientType[] GetStreamSizeClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000A4 RID: 164
		private static readonly ClientType[] HardDeleteMessagesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000A5 RID: 165
		private static readonly ClientType[] HardEmptyFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000A6 RID: 166
		private static readonly ClientType[] IdFromLongTermIdClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000A7 RID: 167
		private static readonly ClientType[] IdFromLongTermIdClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000A8 RID: 168
		private static readonly ClientType[] ImportDeleteClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000A9 RID: 169
		private static readonly ClientType[] ImportHierarchyChangeClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000AA RID: 170
		private static readonly ClientType[] ImportMessageChangeClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000AB RID: 171
		private static readonly ClientType[] ImportMessageChangePartialClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000AC RID: 172
		private static readonly ClientType[] ImportMessageMoveClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000AD RID: 173
		private static readonly ClientType[] ImportReadsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000AE RID: 174
		private static readonly ClientType[] IncrementalConfigClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000AF RID: 175
		private static readonly ClientType[] LockRegionStreamClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000B0 RID: 176
		private static readonly ClientType[] LogonClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000B1 RID: 177
		private static readonly ClientType[] LongTermIdFromIdClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000B2 RID: 178
		private static readonly ClientType[] LongTermIdFromIdClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000B3 RID: 179
		private static readonly ClientType[] MoveCopyMessagesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000B4 RID: 180
		private static readonly ClientType[] MoveCopyMessagesExtendedClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000B5 RID: 181
		private static readonly ClientType[] MoveCopyMessagesExtendedWithEntryIdsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000B6 RID: 182
		private static readonly ClientType[] MoveFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000B7 RID: 183
		private static readonly ClientType[] OpenAttachmentClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000B8 RID: 184
		private static readonly ClientType[] OpenAttachmentClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000B9 RID: 185
		private static readonly ClientType[] OpenCollectorClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000BA RID: 186
		private static readonly ClientType[] OpenEmbeddedMessageClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000BB RID: 187
		private static readonly ClientType[] OpenEmbeddedMessageClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000BC RID: 188
		private static readonly ClientType[] OpenFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000BD RID: 189
		private static readonly ClientType[] OpenFolderClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination,
			ClientType.StoreActiveMonitoring
		};

		// Token: 0x040000BE RID: 190
		private static readonly ClientType[] OpenMessageClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000BF RID: 191
		private static readonly ClientType[] OpenMessageClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000C0 RID: 192
		private static readonly ClientType[] OpenStreamClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000C1 RID: 193
		private static readonly ClientType[] OpenStreamClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000C2 RID: 194
		private static readonly ClientType[] PrereadMessagesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000C3 RID: 195
		private static readonly ClientType[] QueryColumnsAllClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000C4 RID: 196
		private static readonly ClientType[] QueryNamedPropertiesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000C5 RID: 197
		private static readonly ClientType[] QueryPositionClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000C6 RID: 198
		private static readonly ClientType[] QueryRowsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000C7 RID: 199
		private static readonly ClientType[] QueryRowsClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination,
			ClientType.StoreActiveMonitoring
		};

		// Token: 0x040000C8 RID: 200
		private static readonly ClientType[] ReadPerUserInformationClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000C9 RID: 201
		private static readonly ClientType[] ReadRecipientsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000CA RID: 202
		private static readonly ClientType[] ReadRecipientsClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000CB RID: 203
		private static readonly ClientType[] ReadStreamClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000CC RID: 204
		private static readonly ClientType[] ReadStreamClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000CD RID: 205
		private static readonly ClientType[] RegisterNotificationClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000CE RID: 206
		private static readonly ClientType[] RegisterNotificationClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination,
			ClientType.StoreActiveMonitoring
		};

		// Token: 0x040000CF RID: 207
		private static readonly ClientType[] ReleaseClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000D0 RID: 208
		private static readonly ClientType[] ReloadCachedInformationClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000D1 RID: 209
		private static readonly ClientType[] RemoveAllRecipientsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000D2 RID: 210
		private static readonly ClientType[] ResetTableClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000D3 RID: 211
		private static readonly ClientType[] RestrictClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000D4 RID: 212
		private static readonly ClientType[] RestrictClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000D5 RID: 213
		private static readonly ClientType[] SaveChangesAttachmentClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000D6 RID: 214
		private static readonly ClientType[] SaveChangesMessageClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000D7 RID: 215
		private static readonly ClientType[] SeekRowClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000D8 RID: 216
		private static readonly ClientType[] SeekRowApproximateClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000D9 RID: 217
		private static readonly ClientType[] SeekRowBookmarkClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000DA RID: 218
		private static readonly ClientType[] SeekStreamClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000DB RID: 219
		private static readonly ClientType[] SeekStreamClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000DC RID: 220
		private static readonly ClientType[] SetCollapseStateClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000DD RID: 221
		private static readonly ClientType[] SetColumnsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration,
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination
		};

		// Token: 0x040000DE RID: 222
		private static readonly ClientType[] SetColumnsClientTypesAllowedOnReadOnlyDatabase = new ClientType[]
		{
			ClientType.ContentIndexing,
			ClientType.ContentIndexingMoveDestination,
			ClientType.StoreActiveMonitoring
		};

		// Token: 0x040000DF RID: 223
		private static readonly ClientType[] SetMessageFlagsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E0 RID: 224
		private static readonly ClientType[] SetMessageStatusClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E1 RID: 225
		private static readonly ClientType[] SetPropertiesClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E2 RID: 226
		private static readonly ClientType[] SetPropertiesNoReplicateClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E3 RID: 227
		private static readonly ClientType[] SetReadFlagClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E4 RID: 228
		private static readonly ClientType[] SetReadFlagsClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E5 RID: 229
		private static readonly ClientType[] SetReceiveFolderClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E6 RID: 230
		private static readonly ClientType[] SetSearchCriteriaClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E7 RID: 231
		private static readonly ClientType[] SetSizeStreamClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E8 RID: 232
		private static readonly ClientType[] SetSpoolerClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000E9 RID: 233
		private static readonly ClientType[] SetTransportClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000EA RID: 234
		private static readonly ClientType[] SortTableClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000EB RID: 235
		private static readonly ClientType[] SpoolerLockMessageClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000EC RID: 236
		private static readonly ClientType[] SubmitMessageClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000ED RID: 237
		private static readonly ClientType[] TellVersionClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000EE RID: 238
		private static readonly ClientType[] TransportDeliverMessageClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000EF RID: 239
		private static readonly ClientType[] TransportDeliverMessage2ClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F0 RID: 240
		private static readonly ClientType[] TransportDoneWithMessageClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F1 RID: 241
		private static readonly ClientType[] TransportDuplicateDeliveryCheckClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F2 RID: 242
		private static readonly ClientType[] TransportNewMailClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F3 RID: 243
		private static readonly ClientType[] TransportSendClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F4 RID: 244
		private static readonly ClientType[] UnlockRegionStreamClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F5 RID: 245
		private static readonly ClientType[] UploadStateStreamBeginClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F6 RID: 246
		private static readonly ClientType[] UploadStateStreamContinueClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F7 RID: 247
		private static readonly ClientType[] UploadStateStreamEndClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F8 RID: 248
		private static readonly ClientType[] WritePerUserInformationClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000F9 RID: 249
		private static readonly ClientType[] WriteStreamClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000FA RID: 250
		private static readonly ClientType[] WriteStreamExtendedClientTypesAllowedOnMoveTarget = new ClientType[]
		{
			ClientType.Migration
		};

		// Token: 0x040000FB RID: 251
		private MapiContext mapiContext;
	}
}
