using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200008B RID: 139
	public class ClusdbBatchRequest : IDisposable
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x00012E40 File Offset: 0x00011040
		public ClusdbBatchRequest(ClusterDbKey containerKey)
		{
			AmClusterBatchHandle amClusterBatchHandle = null;
			int num = ClusapiMethods.ClusterRegCreateBatch(containerKey.KeyHandle, out amClusterBatchHandle);
			if (num != 0 || amClusterBatchHandle.IsInvalid)
			{
				throw AmExceptionHelper.ConstructClusterApiException(num, "ClusterRegCreateBatch()", new object[0]);
			}
			this.batchHandle = amClusterBatchHandle;
			this.ContainerKey = containerKey;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00012E99 File Offset: 0x00011099
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00012EA1 File Offset: 0x000110A1
		public ClusterDbKey ContainerKey { get; private set; }

		// Token: 0x060004F9 RID: 1273 RVA: 0x00012EAC File Offset: 0x000110AC
		public void CreateKey(string keyName)
		{
			int num = ClusapiMethods.ClusterRegBatchAddCommand(this.batchHandle, CLUSTER_REG_COMMAND.CLUSREG_CREATE_KEY, keyName, RegistryValueKind.Unknown, IntPtr.Zero, 0);
			if (num != 0)
			{
				throw AmExceptionHelper.ConstructClusterApiException(num, "ClusterRegBatchAddCommand(CLUSREG_CREATE_KEY)", new object[0]);
			}
			Interlocked.Increment(ref this.totalCommands);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00012EF0 File Offset: 0x000110F0
		public void DeleteKey(string keyName)
		{
			int num = ClusapiMethods.ClusterRegBatchAddCommand(this.batchHandle, CLUSTER_REG_COMMAND.CLUSREG_DELETE_KEY, keyName, RegistryValueKind.Unknown, IntPtr.Zero, 0);
			if (num != 0)
			{
				throw AmExceptionHelper.ConstructClusterApiException(num, "ClusterRegBatchAddCommand(CLUSREG_DELETE_KEY)", new object[0]);
			}
			Interlocked.Increment(ref this.totalCommands);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00012F34 File Offset: 0x00011134
		public void SetValue(string propertyName, object propertyValue, RegistryValueKind valueKind)
		{
			ClusdbMarshalledProperty clusdbMarshalledProperty = ClusdbMarshalledProperty.Create(propertyName, propertyValue, valueKind);
			if (clusdbMarshalledProperty == null)
			{
				throw new ClusterApiException("WfcDataStoreBatch.SetValue - property value is null", new ClusterUnsupportedRegistryTypeException("null"));
			}
			this.properties.Add(clusdbMarshalledProperty);
			int num = ClusapiMethods.ClusterRegBatchAddCommand(this.batchHandle, CLUSTER_REG_COMMAND.CLUSREG_SET_VALUE, clusdbMarshalledProperty.PropertyName, clusdbMarshalledProperty.ValueKind, clusdbMarshalledProperty.PropertyValueIntPtr, clusdbMarshalledProperty.PropertyValueSize);
			if (num != 0)
			{
				throw AmExceptionHelper.ConstructClusterApiException(num, "ClusterRegBatchAddCommand(CLUSREG_SET_VALUE)", new object[0]);
			}
			Interlocked.Increment(ref this.totalCommands);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00012FB4 File Offset: 0x000111B4
		public void DeleteValue(string propertyName)
		{
			int num = ClusapiMethods.ClusterRegBatchAddCommand(this.batchHandle, CLUSTER_REG_COMMAND.CLUSREG_DELETE_VALUE, propertyName, RegistryValueKind.Unknown, IntPtr.Zero, 0);
			if (num != 0)
			{
				throw AmExceptionHelper.ConstructClusterApiException(num, "ClusterRegBatchAddCommand(CLUSREG_DELETE_VALUE)", new object[0]);
			}
			Interlocked.Increment(ref this.totalCommands);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00012FF8 File Offset: 0x000111F8
		public void Execute(ReadWriteConstraints constraints)
		{
			bool flag = this.totalCommands > 0;
			if (flag && this.batchHandle != null && !this.batchHandle.IsInvalid)
			{
				try
				{
					int num = this.batchHandle.CommitAndClose();
					if (num != 0)
					{
						throw AmExceptionHelper.ConstructClusterApiException(num, "CommitAndClose()", new object[0]);
					}
				}
				finally
				{
					try
					{
						this.batchHandle.Dispose();
					}
					finally
					{
						this.batchHandle = null;
					}
				}
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001307C File Offset: 0x0001127C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00013094 File Offset: 0x00011294
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing)
				{
					try
					{
						if (this.batchHandle != null)
						{
							this.batchHandle.Dispose();
						}
					}
					finally
					{
						this.properties.ForEach(delegate(ClusdbMarshalledProperty prop)
						{
							prop.Dispose();
						});
					}
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x040002BC RID: 700
		private AmClusterBatchHandle batchHandle;

		// Token: 0x040002BD RID: 701
		private int totalCommands;

		// Token: 0x040002BE RID: 702
		private List<ClusdbMarshalledProperty> properties = new List<ClusdbMarshalledProperty>();

		// Token: 0x040002BF RID: 703
		private bool isDisposed;
	}
}
