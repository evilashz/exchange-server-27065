using System;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200003F RID: 63
	internal sealed class ADNotificationRequest
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00012DDF File Offset: 0x00010FDF
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00012DE7 File Offset: 0x00010FE7
		internal string ObjectClass
		{
			get
			{
				return this.objectClass;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00012DEF File Offset: 0x00010FEF
		internal ADObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00012DF7 File Offset: 0x00010FF7
		internal ADNotificationCallback Callback
		{
			get
			{
				return this.callback;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00012DFF File Offset: 0x00010FFF
		internal object Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00012E07 File Offset: 0x00011007
		internal bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00012E0F File Offset: 0x0001100F
		internal void MakeInvalid()
		{
			ExTraceGlobals.ADNotificationsTracer.TraceDebug<int>((long)this.GetHashCode(), "Invalidating request {0}", this.GetHashCode());
			this.isValid = false;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00012E34 File Offset: 0x00011034
		internal ADNotificationRequest(Type type, string objectClass, ADObjectId rootId, ADNotificationCallback callback, object context)
		{
			if (rootId == null)
			{
				throw new ArgumentNullException("rootId");
			}
			this.type = type;
			this.objectClass = objectClass;
			this.rootId = rootId;
			this.callback = callback;
			this.context = context;
			this.isValid = true;
			this.RefCount = 0;
		}

		// Token: 0x04000100 RID: 256
		private Type type;

		// Token: 0x04000101 RID: 257
		private string objectClass;

		// Token: 0x04000102 RID: 258
		private ADObjectId rootId;

		// Token: 0x04000103 RID: 259
		private ADNotificationCallback callback;

		// Token: 0x04000104 RID: 260
		private object context;

		// Token: 0x04000105 RID: 261
		private bool isValid;

		// Token: 0x04000106 RID: 262
		internal ExDateTime LastCallbackTime;

		// Token: 0x04000107 RID: 263
		internal bool isDeletedContainer;

		// Token: 0x04000108 RID: 264
		internal int RefCount;
	}
}
