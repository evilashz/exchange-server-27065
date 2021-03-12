using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000063 RID: 99
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class HierarchyManifestCallbackHelper : ManifestCallbackHelperBase<IMapiHierarchyManifestCallback>, IExchangeHierManifestCallback
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		internal HierarchyManifestCallbackHelper(HierarchyManifestCheckpoint checkpoint, bool isCallbackInfoExpected, bool isChangeNumberExpected)
		{
			if (ComponentTrace<MapiNetTags>.CheckEnabled(34))
			{
				ComponentTrace<MapiNetTags>.Trace<string>(51195, 34, (long)this.GetHashCode(), "HierarchyManifestCallbackHelper.HierarchyManifestCallbackHelper: this={0}", TraceUtils.MakeHash(this));
			}
			this.checkpoint = checkpoint;
			this.isCallbackInfoExpected = isCallbackInfoExpected;
			this.isChangeNumberExpected = isChangeNumberExpected;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000BE68 File Offset: 0x0000A068
		unsafe int IExchangeHierManifestCallback.Change(int cpvalChanges, SPropValue* ppvalChanges)
		{
			PropValue[] props = new PropValue[cpvalChanges];
			long? fid = null;
			long? cn = null;
			int num = 0;
			for (int i = 0; i < cpvalChanges; i++)
			{
				props[i] = new PropValue(ppvalChanges + i);
				if (props[i].PropTag == PropTag.Fid)
				{
					fid = new long?(props[i].GetLong());
				}
				else if (props[i].PropTag == PropTag.Cn)
				{
					cn = new long?(props[i].GetLong());
					num++;
				}
			}
			if (num > 0 && !this.isChangeNumberExpected)
			{
				PropValue[] array = new PropValue[props.Length - num];
				int j = 0;
				int num2 = 0;
				while (j < props.Length)
				{
					if (props[j].PropTag != PropTag.Cn)
					{
						array[num2++] = props[j];
					}
					j++;
				}
				props = array;
			}
			base.Changes.Enqueue(delegate(IMapiHierarchyManifestCallback callback)
			{
				ManifestCallbackStatus result = callback.Change(props);
				if (fid != null)
				{
					this.checkpoint.IdGiven(fid.Value);
				}
				if (cn != null)
				{
					this.checkpoint.CnSeen(cn.Value);
				}
				return result;
			});
			return 0;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000C044 File Offset: 0x0000A244
		unsafe int IExchangeHierManifestCallback.Delete(int cbIdsetDeleted, IntPtr pbIdsetDeleted)
		{
			if (cbIdsetDeleted > 0)
			{
				if (this.isCallbackInfoExpected)
				{
					_CallbackInfo* ptr = (_CallbackInfo*)pbIdsetDeleted.ToPointer();
					int i = 0;
					while (i < cbIdsetDeleted)
					{
						byte[] entryId = new byte[ptr->cb];
						Marshal.Copy((IntPtr)((void*)ptr->pb), entryId, 0, ptr->cb);
						long fid = ptr->id;
						base.Deletes.Enqueue(delegate(IMapiHierarchyManifestCallback callback)
						{
							ManifestCallbackStatus result = callback.Delete(entryId);
							this.checkpoint.IdDeleted(fid);
							return result;
						});
						i++;
						ptr++;
					}
				}
				else
				{
					byte[] idsetDeleted = new byte[cbIdsetDeleted];
					Marshal.Copy(pbIdsetDeleted, idsetDeleted, 0, cbIdsetDeleted);
					base.Deletes.Enqueue((IMapiHierarchyManifestCallback callback) => callback.Delete(idsetDeleted));
				}
			}
			return 0;
		}

		// Token: 0x04000470 RID: 1136
		private readonly HierarchyManifestCheckpoint checkpoint;

		// Token: 0x04000471 RID: 1137
		private readonly bool isCallbackInfoExpected;

		// Token: 0x04000472 RID: 1138
		private readonly bool isChangeNumberExpected;
	}
}
