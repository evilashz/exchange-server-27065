using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000079 RID: 121
	public abstract class ObjectCreatedModifiedNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x0004BE38 File Offset: 0x0004A038
		public ObjectCreatedModifiedNotificationEvent(StoreDatabase database, int mailboxNumber, EventType eventType, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int? documentId, int? conversationDocumentId, StorePropTag[] changedPropTags, string objectClass, Guid? userIdentityContext) : base(database, mailboxNumber, eventType, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, parentFid, documentId, conversationDocumentId, objectClass, userIdentityContext)
		{
			this.changedPropTags = changedPropTags;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0004BE6C File Offset: 0x0004A06C
		public StorePropTag[] ChangedPropTags
		{
			get
			{
				return this.changedPropTags;
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0004BE74 File Offset: 0x0004A074
		protected static bool PropTagArraysEqual(StorePropTag[] array1, StorePropTag[] array2)
		{
			if (array1 == array2)
			{
				return true;
			}
			if (array1 == null || array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0004BEC4 File Offset: 0x0004A0C4
		protected static StorePropTag[] MergeChangedPropTagArrays(StorePropTag[] ptags1, StorePropTag[] ptags2)
		{
			if (ptags1 == null || ptags1.Length == 0)
			{
				return ptags2;
			}
			if (ptags2 == null || ptags2.Length == 0)
			{
				return ptags1;
			}
			List<StorePropTag> list = new List<StorePropTag>(ptags1.Length + ptags2.Length);
			foreach (StorePropTag item in ptags1)
			{
				list.Add(item);
			}
			foreach (StorePropTag item2 in ptags2)
			{
				if (!list.Contains(item2))
				{
					list.Add(item2);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0004BF56 File Offset: 0x0004A156
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" ChangedPropTags:[");
			sb.AppendAsString(this.changedPropTags);
			sb.Append("]");
		}

		// Token: 0x04000473 RID: 1139
		private StorePropTag[] changedPropTags;
	}
}
