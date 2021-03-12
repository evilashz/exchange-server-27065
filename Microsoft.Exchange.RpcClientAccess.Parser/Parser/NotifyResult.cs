using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000272 RID: 626
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NotifyResult : Result
	{
		// Token: 0x06000D77 RID: 3447 RVA: 0x00028FB7 File Offset: 0x000271B7
		internal NotifyResult(ServerObjectHandle notificationHandle, byte logonId, Encoding string8Encoding, Notification notificationData) : base(RopId.Notify)
		{
			this.NotificationHandle = notificationHandle;
			this.LogonId = logonId;
			this.NotificationData = notificationData;
			base.String8Encoding = string8Encoding;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00028FDE File Offset: 0x000271DE
		internal NotifyResult(ServerObjectHandle notificationHandle, byte logonId, int codePage, Notification notificationData) : base(RopId.Notify)
		{
			this.NotificationHandle = notificationHandle;
			this.LogonId = logonId;
			this.NotificationData = notificationData;
			base.String8Encoding = CodePageMap.GetEncoding(codePage);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002900C File Offset: 0x0002720C
		internal static bool TryParse(Reader reader, IDictionary<ServerObjectHandle, PropertyTag[]> columnsDictionary, Func<ServerObjectHandle, Encoding> getEncoding, out NotifyResult notifyResult)
		{
			NotifyResult notifyResult2 = new NotifyResult(reader, columnsDictionary, getEncoding);
			if (notifyResult2.NotificationData == null)
			{
				notifyResult = null;
				return false;
			}
			notifyResult = notifyResult2;
			return true;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00029034 File Offset: 0x00027234
		private NotifyResult(Reader reader, IDictionary<ServerObjectHandle, PropertyTag[]> columnsDictionary, Func<ServerObjectHandle, Encoding> getEncoding) : base(reader)
		{
			this.NotificationHandle = ServerObjectHandle.Parse(reader);
			this.LogonId = reader.ReadByte();
			PropertyTag[] originalPropertyTags;
			if (!columnsDictionary.TryGetValue(this.NotificationHandle, out originalPropertyTags))
			{
				originalPropertyTags = null;
			}
			this.NotificationData = Notification.Parse(reader, originalPropertyTags, getEncoding(this.NotificationHandle));
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0002908B File Offset: 0x0002728B
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x00029093 File Offset: 0x00027293
		internal ServerObjectHandle NotificationHandle { get; private set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0002909C File Offset: 0x0002729C
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x000290A4 File Offset: 0x000272A4
		internal byte LogonId { get; private set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x000290AD File Offset: 0x000272AD
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x000290B5 File Offset: 0x000272B5
		internal Notification NotificationData { get; private set; }

		// Token: 0x06000D81 RID: 3457 RVA: 0x000290C0 File Offset: 0x000272C0
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.NotificationHandle.Serialize(writer);
			writer.WriteByte(this.LogonId);
			this.NotificationData.Serialize(writer, base.String8Encoding);
		}
	}
}
