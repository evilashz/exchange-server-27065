using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000685 RID: 1669
	internal class StoreTransaction : IDisposable
	{
		// Token: 0x06004EED RID: 20205 RVA: 0x00118736 File Offset: 0x00116936
		public void Add(StoreOperationInstallDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x0011874A File Offset: 0x0011694A
		public void Add(StoreOperationPinDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0011875E File Offset: 0x0011695E
		public void Add(StoreOperationSetCanonicalizationContext o)
		{
			this._list.Add(o);
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x00118772 File Offset: 0x00116972
		public void Add(StoreOperationSetDeploymentMetadata o)
		{
			this._list.Add(o);
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x00118786 File Offset: 0x00116986
		public void Add(StoreOperationStageComponent o)
		{
			this._list.Add(o);
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x0011879A File Offset: 0x0011699A
		public void Add(StoreOperationStageComponentFile o)
		{
			this._list.Add(o);
		}

		// Token: 0x06004EF3 RID: 20211 RVA: 0x001187AE File Offset: 0x001169AE
		public void Add(StoreOperationUninstallDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x06004EF4 RID: 20212 RVA: 0x001187C2 File Offset: 0x001169C2
		public void Add(StoreOperationUnpinDeployment o)
		{
			this._list.Add(o);
		}

		// Token: 0x06004EF5 RID: 20213 RVA: 0x001187D6 File Offset: 0x001169D6
		public void Add(StoreOperationScavenge o)
		{
			this._list.Add(o);
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x001187EC File Offset: 0x001169EC
		~StoreTransaction()
		{
			this.Dispose(false);
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x0011881C File Offset: 0x00116A1C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x00118828 File Offset: 0x00116A28
		[SecuritySafeCritical]
		private void Dispose(bool fDisposing)
		{
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
			StoreTransactionOperation[] storeOps = this._storeOps;
			this._storeOps = null;
			if (storeOps != null)
			{
				for (int num = 0; num != storeOps.Length; num++)
				{
					StoreTransactionOperation storeTransactionOperation = storeOps[num];
					if (storeTransactionOperation.Data.DataPtr != IntPtr.Zero)
					{
						switch (storeTransactionOperation.Operation)
						{
						case StoreTransactionOperationType.SetCanonicalizationContext:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationSetCanonicalizationContext));
							break;
						case StoreTransactionOperationType.StageComponent:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationStageComponent));
							break;
						case StoreTransactionOperationType.PinDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationPinDeployment));
							break;
						case StoreTransactionOperationType.UnpinDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationUnpinDeployment));
							break;
						case StoreTransactionOperationType.StageComponentFile:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationStageComponentFile));
							break;
						case StoreTransactionOperationType.InstallDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationInstallDeployment));
							break;
						case StoreTransactionOperationType.UninstallDeployment:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationUninstallDeployment));
							break;
						case StoreTransactionOperationType.SetDeploymentMetadata:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationSetDeploymentMetadata));
							break;
						case StoreTransactionOperationType.Scavenge:
							Marshal.DestroyStructure(storeTransactionOperation.Data.DataPtr, typeof(StoreOperationScavenge));
							break;
						}
						Marshal.FreeCoTaskMem(storeTransactionOperation.Data.DataPtr);
					}
				}
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x001189EB File Offset: 0x00116BEB
		public StoreTransactionOperation[] Operations
		{
			get
			{
				if (this._storeOps == null)
				{
					this._storeOps = this.GenerateStoreOpsList();
				}
				return this._storeOps;
			}
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x00118A08 File Offset: 0x00116C08
		[SecuritySafeCritical]
		private StoreTransactionOperation[] GenerateStoreOpsList()
		{
			StoreTransactionOperation[] array = new StoreTransactionOperation[this._list.Count];
			for (int num = 0; num != this._list.Count; num++)
			{
				object obj = this._list[num];
				Type type = obj.GetType();
				array[num].Data.DataPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(obj));
				Marshal.StructureToPtr(obj, array[num].Data.DataPtr, false);
				if (type == typeof(StoreOperationSetCanonicalizationContext))
				{
					array[num].Operation = StoreTransactionOperationType.SetCanonicalizationContext;
				}
				else if (type == typeof(StoreOperationStageComponent))
				{
					array[num].Operation = StoreTransactionOperationType.StageComponent;
				}
				else if (type == typeof(StoreOperationPinDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.PinDeployment;
				}
				else if (type == typeof(StoreOperationUnpinDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.UnpinDeployment;
				}
				else if (type == typeof(StoreOperationStageComponentFile))
				{
					array[num].Operation = StoreTransactionOperationType.StageComponentFile;
				}
				else if (type == typeof(StoreOperationInstallDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.InstallDeployment;
				}
				else if (type == typeof(StoreOperationUninstallDeployment))
				{
					array[num].Operation = StoreTransactionOperationType.UninstallDeployment;
				}
				else if (type == typeof(StoreOperationSetDeploymentMetadata))
				{
					array[num].Operation = StoreTransactionOperationType.SetDeploymentMetadata;
				}
				else
				{
					if (!(type == typeof(StoreOperationScavenge)))
					{
						throw new Exception("How did you get here?");
					}
					array[num].Operation = StoreTransactionOperationType.Scavenge;
				}
			}
			return array;
		}

		// Token: 0x040021B6 RID: 8630
		private ArrayList _list = new ArrayList();

		// Token: 0x040021B7 RID: 8631
		private StoreTransactionOperation[] _storeOps;
	}
}
