using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000628 RID: 1576
	[Serializable]
	public sealed class ForwardSyncCookie : ForwardSyncCookieHeader
	{
		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x00113212 File Offset: 0x00111412
		internal override ADObjectSchema Schema
		{
			get
			{
				return ForwardSyncCookie.SchemaInstance;
			}
		}

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x06004A72 RID: 19058 RVA: 0x00113219 File Offset: 0x00111419
		// (set) Token: 0x06004A73 RID: 19059 RVA: 0x0011322B File Offset: 0x0011142B
		public int Version
		{
			get
			{
				return (int)this[ForwardSyncCookieSchema.Version];
			}
			set
			{
				this[ForwardSyncCookieSchema.Version] = value;
			}
		}

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x06004A74 RID: 19060 RVA: 0x0011323E File Offset: 0x0011143E
		// (set) Token: 0x06004A75 RID: 19061 RVA: 0x00113250 File Offset: 0x00111450
		public bool IsUpgradingSyncPropertySet
		{
			get
			{
				return (bool)this[ForwardSyncCookieSchema.IsUpgradingSyncPropertySet];
			}
			set
			{
				this[ForwardSyncCookieSchema.IsUpgradingSyncPropertySet] = value;
			}
		}

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x00113263 File Offset: 0x00111463
		// (set) Token: 0x06004A77 RID: 19063 RVA: 0x00113275 File Offset: 0x00111475
		public int SyncPropertySetVersion
		{
			get
			{
				return (int)this[ForwardSyncCookieSchema.SyncPropertySetVersion];
			}
			set
			{
				this[ForwardSyncCookieSchema.SyncPropertySetVersion] = value;
			}
		}

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x00113288 File Offset: 0x00111488
		// (set) Token: 0x06004A79 RID: 19065 RVA: 0x0011329A File Offset: 0x0011149A
		public byte[] Data
		{
			get
			{
				return (byte[])this[ForwardSyncCookieSchema.Data];
			}
			set
			{
				this[ForwardSyncCookieSchema.Data] = value;
			}
		}

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x001132A8 File Offset: 0x001114A8
		// (set) Token: 0x06004A7B RID: 19067 RVA: 0x001132BA File Offset: 0x001114BA
		public MultiValuedProperty<string> FilteredContextIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[ForwardSyncCookieSchema.FilteredContextIds];
			}
			set
			{
				this[ForwardSyncCookieSchema.FilteredContextIds] = value;
			}
		}

		// Token: 0x04003384 RID: 13188
		private static readonly ForwardSyncCookieSchema SchemaInstance = ObjectSchema.GetInstance<ForwardSyncCookieSchema>();

		// Token: 0x04003385 RID: 13189
		internal new static readonly string MostDerivedClass = ForwardSyncCookieHeader.MostDerivedClass;
	}
}
