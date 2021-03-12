﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks;
using Microsoft.Exchange.Management.CompliancePolicy.DarTasks.Protocol;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Dar;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Execution;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.LocStrings;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Utility;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Service
{
	// Token: 0x02000010 RID: 16
	internal class HostRpcServer : ExDarHostRpcServer
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003AAC File Offset: 0x00001CAC
		public static bool Start()
		{
			if (HostRpcServer.registered == 1)
			{
				return true;
			}
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.Read, AccessControlType.Allow);
			FileSecurity fileSecurity = new FileSecurity();
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.SetAccessRule(accessRule);
			bool result;
			try
			{
				RpcServerBase.RegisterServer(typeof(HostRpcServer), fileSecurity, 131209);
				Interlocked.CompareExchange(ref HostRpcServer.registered, 1, 0);
				InstanceManager.Current.Start();
				result = true;
			}
			catch
			{
				Interlocked.CompareExchange(ref HostRpcServer.registered, 0, 1);
				result = false;
			}
			return result;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003B44 File Offset: 0x00001D44
		public static void Stop()
		{
			int num = Interlocked.CompareExchange(ref HostRpcServer.registered, 0, 1);
			if (num == 1)
			{
				InstanceManager.Current.Stop();
				RpcServerBase.StopServer(ExDarHostRpcServer.RpcIntfHandle);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003B78 File Offset: 0x00001D78
		public override byte[] SendHostRequest(int version, int type, byte[] inputParameterBytes)
		{
			Guid guid = Guid.NewGuid();
			DarTaskResult darTaskResult;
			try
			{
				try
				{
					HostRpcServer.Log(guid.ToString(), "HandleRpcRequest", "Serving DAR Runtime Request of type: " + ((RpcRequestType)type).ToString(), ResultSeverityLevel.Informational);
					darTaskResult = HostRpcServer.GetSendHostRequestResult(version, (RpcRequestType)type, guid.ToString(), inputParameterBytes);
				}
				catch (AggregateException ex)
				{
					if (ex.InnerExceptions.Count == 1)
					{
						throw ex.InnerException;
					}
					throw;
				}
			}
			catch (ApplicationException ex2)
			{
				HostRpcServer.Log(guid.ToString(), "HandleRpcRequestFailure", ex2.ToString(), ResultSeverityLevel.Warning);
				darTaskResult = new DarTaskResult
				{
					LocalizedError = ex2.Message
				};
			}
			catch (DataSourceOperationException ex3)
			{
				HostRpcServer.Log(guid.ToString(), "HandleRpcRequestFailure", ex3.ToString(), ResultSeverityLevel.Warning);
				darTaskResult = new DarTaskResult
				{
					LocalizedError = ex3.LocalizedString
				};
			}
			catch (Exception ex4)
			{
				HostRpcServer.Log(guid.ToString(), "HandleRpcRequestFailure", ex4.ToString(), ResultSeverityLevel.Error);
				darTaskResult = new DarTaskResult
				{
					LocalizedError = Strings.ErrorDuringDarCall(guid.ToString())
				};
			}
			if (darTaskResult != null)
			{
				try
				{
					return darTaskResult.ToBytes();
				}
				catch (Exception ex5)
				{
					HostRpcServer.Log(guid.ToString(), "HandleRpcRequestResultSerializationFailure", ex5.ToString(), ResultSeverityLevel.Error);
					throw;
				}
			}
			return HostRpcServer.ok;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003D28 File Offset: 0x00001F28
		private static DarTaskResult GetSendHostRequestResult(int version, RpcRequestType type, string correlationId, byte[] inputParameterBytes)
		{
			switch (type)
			{
			case RpcRequestType.NotifyTaskStoreChange:
			case RpcRequestType.EnsureTenantMonitoring:
				InstanceManager.Current.NotifyTaskStoreChange(HostRpcServer.GetTenantId(inputParameterBytes), correlationId);
				return null;
			case RpcRequestType.GetDarTask:
				return HostRpcServer.GetDarTask(inputParameterBytes, correlationId);
			case RpcRequestType.SetDarTask:
				return HostRpcServer.SetDarTask(inputParameterBytes, correlationId);
			case RpcRequestType.GetDarTaskAggregate:
				return HostRpcServer.GetDarTaskAggregate(inputParameterBytes, correlationId);
			case RpcRequestType.SetDarTaskAggregate:
				return HostRpcServer.SetDarTaskAggregate(inputParameterBytes, correlationId);
			case RpcRequestType.RemoveCompletedDarTasks:
				return HostRpcServer.RemoveCompletedDarTasks(inputParameterBytes, correlationId);
			case RpcRequestType.RemoveDarTaskAggregate:
				return HostRpcServer.RemoveDarTaskAggregate(inputParameterBytes, correlationId);
			case RpcRequestType.GetDarInfo:
				return HostRpcServer.GetDarInfo();
			default:
				throw new InvalidOperationException("Unknown request RpcRequestType");
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003DB8 File Offset: 0x00001FB8
		private static string GetTenantId(byte[] inputParameterBytes)
		{
			if (inputParameterBytes == null)
			{
				throw new ApplicationException(Strings.TenantMustBeSpecified);
			}
			return Convert.ToBase64String(inputParameterBytes);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003DD0 File Offset: 0x00001FD0
		private static DarTaskResult GetDarInfo()
		{
			string localizedInformation = string.Join("\n", Helper.DumpObject(InstanceManager.Current, "DARRuntime", 3).ToArray<string>());
			return new DarTaskResult
			{
				LocalizedInformation = localizedInformation
			};
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003E0C File Offset: 0x0000200C
		private static DarTaskResult GetDarTask(byte[] inputParameterBytes, string correlationId)
		{
			DarTaskParams darTaskParams = DarTaskParamsBase.FromBytes<DarTaskParams>(inputParameterBytes);
			string tenantId = HostRpcServer.GetTenantId(darTaskParams.TenantId);
			HostRpcServer.Log(correlationId, "GetDarTask", Helper.DumpObject(darTaskParams), ResultSeverityLevel.Informational);
			if (darTaskParams.ActiveInRuntime)
			{
				IEnumerable<DarTask> activeTaskList = InstanceManager.Current.GetActiveTaskList(tenantId);
				return new DarTaskResult
				{
					DarTasks = activeTaskList.Select(new Func<DarTask, TaskStoreObject>(HostRpcServer.GetTaskStoreObject)).ToArray<TaskStoreObject>()
				};
			}
			SearchFilter taskFilter = TaskHelper.GetTaskFilter(darTaskParams);
			IEnumerable<TaskStoreObject> source = TenantStore.Find<TaskStoreObject>(tenantId, taskFilter, false, correlationId);
			return new DarTaskResult
			{
				DarTasks = source.ToArray<TaskStoreObject>()
			};
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003EA4 File Offset: 0x000020A4
		private static TaskStoreObject GetTaskStoreObject(DarTask task)
		{
			TaskStoreObject taskStoreObject = task.WorkloadContext as TaskStoreObject;
			if (taskStoreObject != null)
			{
				return taskStoreObject;
			}
			taskStoreObject = new TaskStoreObject();
			taskStoreObject.UpdateFromDarTask(task);
			return taskStoreObject;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003ED0 File Offset: 0x000020D0
		private static TaskAggregateStoreObject GetTaskAggregateStoreObject(DarTaskAggregate task)
		{
			TaskAggregateStoreObject taskAggregateStoreObject = task.WorkloadContext as TaskAggregateStoreObject;
			if (taskAggregateStoreObject != null)
			{
				return taskAggregateStoreObject;
			}
			taskAggregateStoreObject = new TaskAggregateStoreObject();
			taskAggregateStoreObject.UpdateFromDarTaskAggregate(task);
			return taskAggregateStoreObject;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003EFC File Offset: 0x000020FC
		private static DarTaskResult RemoveCompletedDarTasks(byte[] inputParameterBytes, string correlationId)
		{
			DarTaskParams darTaskParams = DarTaskParamsBase.FromBytes<DarTaskParams>(inputParameterBytes);
			DarServiceProvider darServiceProvider = new ExDarServiceProvider();
			HostRpcServer.Log(correlationId, "RemoveCompletedDarTasks", Helper.DumpObject(darTaskParams), ResultSeverityLevel.Informational);
			darServiceProvider.DarTaskQueue.DeleteCompletedTask(darTaskParams.MaxCompletionTime, darTaskParams.TaskType, HostRpcServer.GetTenantId(darTaskParams.TenantId));
			return DarTaskResult.Nothing();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003F64 File Offset: 0x00002164
		private static DarTaskResult SetDarTask(byte[] inputParameterBytes, string correlationId)
		{
			TaskStoreObject taskStoreObject = DarTaskResult.ObjectFromBytes<TaskStoreObject>(inputParameterBytes);
			SearchFilter filter = new SearchFilter.IsEqualTo(TaskStoreObjectSchema.Id.StorePropertyDefinition, taskStoreObject.Id);
			string tenantId = HostRpcServer.GetTenantId(taskStoreObject.TenantId);
			DarTask darTask = (from t in TenantStore.Find<TaskStoreObject>(tenantId, filter, false, correlationId)
			select t.ToDarTask(InstanceManager.Current.Provider)).FirstOrDefault<DarTask>();
			DarTaskManager darTaskManager = new DarTaskManager(InstanceManager.Current.Provider);
			if (taskStoreObject.ObjectState == ObjectState.New)
			{
				if (darTask != null)
				{
					throw new DataSourceOperationException(new LocalizedString(Strings.TaskAlreadyExists));
				}
				darTask = InstanceManager.Current.Provider.DarTaskFactory.CreateTask(taskStoreObject.TaskType);
				darTask.Id = taskStoreObject.Id;
				darTask.Priority = taskStoreObject.Priority;
				darTask.TenantId = tenantId;
				darTask.SerializedTaskData = taskStoreObject.SerializedTaskData;
				if (!darTask.RestoreStateFromSerializedData(darTaskManager))
				{
					throw new DataSourceOperationException(new LocalizedString(Strings.TaskCannotBeRestored));
				}
				HostRpcServer.Log(correlationId, "NewDarTask", Helper.DumpObject(darTask), ResultSeverityLevel.Informational);
				darTaskManager.Enqueue(darTask);
			}
			else
			{
				if (darTask == null)
				{
					throw new DataSourceOperationException(new LocalizedString(Strings.TaskNotFound));
				}
				darTask.TaskState = taskStoreObject.TaskState;
				darTask.Priority = taskStoreObject.Priority;
				HostRpcServer.Log(correlationId, "SetDarTask", Helper.DumpObject(darTask), ResultSeverityLevel.Informational);
				darTaskManager.UpdateTaskState(darTask);
			}
			return DarTaskResult.Nothing();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000040C0 File Offset: 0x000022C0
		private static DarTaskResult GetDarTaskAggregate(byte[] inputParameterBytes, string correlationId)
		{
			DarTaskAggregateParams darTaskAggregateParams = DarTaskParamsBase.FromBytes<DarTaskAggregateParams>(inputParameterBytes);
			SearchFilter taskAggregateFilter = TaskHelper.GetTaskAggregateFilter(darTaskAggregateParams);
			HostRpcServer.Log(correlationId, "GetDarTaskAggregate", Helper.DumpObject(darTaskAggregateParams), ResultSeverityLevel.Informational);
			HostRpcServer.GetTenantId(darTaskAggregateParams.TenantId);
			IEnumerable<TaskAggregateStoreObject> source = TenantStore.Find<TaskAggregateStoreObject>(HostRpcServer.GetTenantId(darTaskAggregateParams.TenantId), taskAggregateFilter, false, correlationId);
			return new DarTaskResult
			{
				DarTaskAggregates = source.ToArray<TaskAggregateStoreObject>()
			};
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004120 File Offset: 0x00002320
		private static DarTaskResult SetDarTaskAggregate(byte[] inputParameterBytes, string correlationId)
		{
			TaskAggregateStoreObject taskAggregateStoreObject = DarTaskResult.ObjectFromBytes<TaskAggregateStoreObject>(inputParameterBytes);
			string tenantId = HostRpcServer.GetTenantId(taskAggregateStoreObject.ScopeId);
			if (taskAggregateStoreObject.ObjectState == ObjectState.New)
			{
				InstanceManager.Current.TaskAggregates.Remove(tenantId, taskAggregateStoreObject.TaskType, correlationId);
			}
			DarTaskAggregate darTaskAggregate = InstanceManager.Current.TaskAggregates.Get(tenantId, taskAggregateStoreObject.TaskType, correlationId);
			darTaskAggregate.MaxRunningTasks = taskAggregateStoreObject.MaxRunningTasks;
			darTaskAggregate.Enabled = taskAggregateStoreObject.Enabled;
			HostRpcServer.Log(correlationId, "SetDarTaskAggregate", Helper.DumpObject(darTaskAggregate), ResultSeverityLevel.Informational);
			InstanceManager.Current.TaskAggregates.Set(darTaskAggregate, correlationId);
			return DarTaskResult.Nothing();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000041B8 File Offset: 0x000023B8
		private static DarTaskResult RemoveDarTaskAggregate(byte[] inputParameterBytes, string correlationId)
		{
			DarTaskAggregateParams darTaskAggregateParams = DarTaskParamsBase.FromBytes<DarTaskAggregateParams>(inputParameterBytes);
			string tenantId = HostRpcServer.GetTenantId(darTaskAggregateParams.TenantId);
			HostRpcServer.Log(correlationId, "RemoveDarTaskAggregate", "Type: " + darTaskAggregateParams.TaskType + ", tenantId: " + tenantId, ResultSeverityLevel.Informational);
			if (!InstanceManager.Current.TaskAggregates.Remove(tenantId, darTaskAggregateParams.TaskType, correlationId))
			{
				throw new DataSourceOperationException(new LocalizedString(Strings.TaskNotFound));
			}
			return DarTaskResult.Nothing();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004228 File Offset: 0x00002428
		private static void Log(string correlationId, string tag, string message, ResultSeverityLevel severity = ResultSeverityLevel.Informational)
		{
			LogItem.Publish("HostRpcServer", tag, message, correlationId, severity);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004238 File Offset: 0x00002438
		// Note: this type is marked as 'beforefieldinit'.
		static HostRpcServer()
		{
			byte[] array = new byte[1];
			HostRpcServer.ok = array;
		}

		// Token: 0x04000030 RID: 48
		private const string LogComponent = "HostRpcServer";

		// Token: 0x04000031 RID: 49
		private static byte[] ok;

		// Token: 0x04000032 RID: 50
		private static int registered;
	}
}
