using System;
using System.Collections.Generic;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000A2 RID: 162
	public class GenericBatchRequest : IDistributedStoreBatchRequest, IDisposable
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0001687C File Offset: 0x00014A7C
		public GenericBatchRequest(IDistributedStoreKey key)
		{
			this.key = key;
			this.Commands = new List<DxStoreBatchCommand>();
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00016896 File Offset: 0x00014A96
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0001689E File Offset: 0x00014A9E
		public List<DxStoreBatchCommand> Commands { get; private set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x000168A7 File Offset: 0x00014AA7
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x000168AF File Offset: 0x00014AAF
		public bool IsDisposed { get; private set; }

		// Token: 0x06000602 RID: 1538 RVA: 0x000168B8 File Offset: 0x00014AB8
		public void Dispose()
		{
			this.IsDisposed = true;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000168C4 File Offset: 0x00014AC4
		public void CreateKey(string keyName)
		{
			DxStoreBatchCommand.CreateKey item = new DxStoreBatchCommand.CreateKey
			{
				Name = keyName
			};
			this.Commands.Add(item);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000168EC File Offset: 0x00014AEC
		public void DeleteKey(string keyName)
		{
			DxStoreBatchCommand.DeleteKey item = new DxStoreBatchCommand.DeleteKey
			{
				Name = keyName
			};
			this.Commands.Add(item);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00016914 File Offset: 0x00014B14
		public void SetValue(string propertyName, object propertyValue, RegistryValueKind valueKind = RegistryValueKind.Unknown)
		{
			DxStoreBatchCommand.SetProperty item = new DxStoreBatchCommand.SetProperty
			{
				Name = propertyName,
				Value = new PropertyValue(propertyValue, valueKind)
			};
			this.Commands.Add(item);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001694C File Offset: 0x00014B4C
		public void DeleteValue(string propertyName)
		{
			DxStoreBatchCommand.DeleteProperty item = new DxStoreBatchCommand.DeleteProperty
			{
				Name = propertyName
			};
			this.Commands.Add(item);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00016974 File Offset: 0x00014B74
		public void Execute(ReadWriteConstraints constraints)
		{
			this.key.ExecuteBatchRequest(this.Commands, constraints);
		}

		// Token: 0x0400032D RID: 813
		private IDistributedStoreKey key;
	}
}
