using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200006F RID: 111
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ContentsManifestCallbackHelperBase<TCallback> : ManifestCallbackHelperBase<TCallback> where TCallback : class
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x0000C6C6 File Offset: 0x0000A8C6
		protected ContentsManifestCallbackHelperBase(bool conversations)
		{
			this.conversations = conversations;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		public ManifestCallbackQueue<TCallback> Reads
		{
			get
			{
				return this.readList;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		public bool Conversations
		{
			get
			{
				return this.conversations;
			}
		}

		// Token: 0x060002E9 RID: 745
		protected abstract ManifestCallbackStatus DoChangeCallback(TCallback callback, ManifestChangeType changeType, PropValue[] headerProps, PropValue[] messageProps);

		// Token: 0x060002EA RID: 746 RVA: 0x0000C718 File Offset: 0x0000A918
		protected unsafe int Change(ExchangeManifestCallbackChangeFlags flags, int cpvalHeader, SPropValue* ppvalHeader, int cpvalProps, SPropValue* ppvalProps)
		{
			PropValue[] pvaHeader = new PropValue[cpvalHeader];
			if ((!this.conversations && cpvalHeader != 9 && cpvalHeader != 10) || (this.conversations && cpvalHeader != 5))
			{
				return -2147221221;
			}
			for (int i = 0; i < cpvalHeader; i++)
			{
				pvaHeader[i] = new PropValue(ppvalHeader + i);
			}
			if (!this.conversations)
			{
				if (cpvalHeader == 9)
				{
					if (pvaHeader[0].PropTag != PropTag.SourceKey || pvaHeader[1].PropTag != PropTag.LastModificationTime || pvaHeader[2].PropTag != PropTag.ChangeKey || pvaHeader[3].PropTag != PropTag.PredecessorChangeList || pvaHeader[4].PropTag != PropTag.Associated || pvaHeader[5].PropTag != PropTag.Mid || pvaHeader[7].PropTag != PropTag.Cn || pvaHeader[8].PropTag != PropTag.EntryId)
					{
						return -2147221221;
					}
				}
				else if (pvaHeader[0].PropTag != PropTag.SourceKey || pvaHeader[1].PropTag != PropTag.LastModificationTime || pvaHeader[2].PropTag != PropTag.ChangeKey || pvaHeader[3].PropTag != PropTag.PredecessorChangeList || pvaHeader[4].PropTag != PropTag.Associated || pvaHeader[5].PropTag != PropTag.Mid || pvaHeader[7].PropTag != PropTag.Cn || pvaHeader[8].PropTag != PropTag.ReadCn || pvaHeader[9].PropTag != PropTag.EntryId)
				{
					return -2147221221;
				}
			}
			else if (pvaHeader[0].PropTag != PropTag.Mid || pvaHeader[1].PropTag != PropTag.Cn || pvaHeader[2].PropTag != PropTag.LastModificationTime || pvaHeader[3].PropTag != PropTag.ChangeType || pvaHeader[4].PropTag != PropTag.EntryId)
			{
				return -2147221221;
			}
			PropValue[] pvaProps = new PropValue[cpvalProps];
			for (int j = 0; j < cpvalProps; j++)
			{
				pvaProps[j] = new PropValue(ppvalProps + j);
			}
			ManifestChangeType changeType;
			if (!this.conversations)
			{
				changeType = (((flags & ExchangeManifestCallbackChangeFlags.NewMessage) == ExchangeManifestCallbackChangeFlags.NewMessage) ? ManifestChangeType.Add : ManifestChangeType.Change);
			}
			else
			{
				changeType = (ManifestChangeType)((short)pvaHeader[3].Value);
			}
			base.Changes.Enqueue((TCallback callback) => this.DoChangeCallback(callback, changeType, pvaHeader, pvaProps));
			return 0;
		}

		// Token: 0x040004A6 RID: 1190
		private readonly ManifestCallbackQueue<TCallback> readList = new ManifestCallbackQueue<TCallback>();

		// Token: 0x040004A7 RID: 1191
		private readonly bool conversations;

		// Token: 0x02000070 RID: 112
		protected enum ChangePropertyIndexOld
		{
			// Token: 0x040004A9 RID: 1193
			SourceKey,
			// Token: 0x040004AA RID: 1194
			LastModificationTime,
			// Token: 0x040004AB RID: 1195
			ChangeKey,
			// Token: 0x040004AC RID: 1196
			PredecessorChangeList,
			// Token: 0x040004AD RID: 1197
			Associated,
			// Token: 0x040004AE RID: 1198
			Mid,
			// Token: 0x040004AF RID: 1199
			MessageSize,
			// Token: 0x040004B0 RID: 1200
			Cn,
			// Token: 0x040004B1 RID: 1201
			EntryId
		}

		// Token: 0x02000071 RID: 113
		protected enum ChangePropertyIndex
		{
			// Token: 0x040004B3 RID: 1203
			SourceKey,
			// Token: 0x040004B4 RID: 1204
			LastModificationTime,
			// Token: 0x040004B5 RID: 1205
			ChangeKey,
			// Token: 0x040004B6 RID: 1206
			PredecessorChangeList,
			// Token: 0x040004B7 RID: 1207
			Associated,
			// Token: 0x040004B8 RID: 1208
			Mid,
			// Token: 0x040004B9 RID: 1209
			MessageSize,
			// Token: 0x040004BA RID: 1210
			Cn,
			// Token: 0x040004BB RID: 1211
			ReadCn,
			// Token: 0x040004BC RID: 1212
			EntryId
		}

		// Token: 0x02000072 RID: 114
		protected enum ConversationChangePropertyIndex
		{
			// Token: 0x040004BE RID: 1214
			Mid,
			// Token: 0x040004BF RID: 1215
			Cn,
			// Token: 0x040004C0 RID: 1216
			LastModificationTime,
			// Token: 0x040004C1 RID: 1217
			ChangeType,
			// Token: 0x040004C2 RID: 1218
			EntryId
		}
	}
}
