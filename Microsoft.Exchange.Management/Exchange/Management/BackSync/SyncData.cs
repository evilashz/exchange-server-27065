using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync
{
	// Token: 0x020000C2 RID: 194
	[Serializable]
	public class SyncData : ConfigurableObject
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x0001A714 File Offset: 0x00018914
		internal SyncData(byte[] cookie, object response) : this()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New SyncData");
			this.response = response;
			this.Data = SyncObject.SerializeResponse(response, !SyncConfiguration.SkipSchemaValidation);
			this.Identity = ((cookie != null) ? Convert.ToBase64String(cookie) : null);
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncData cookie {0}", this.Identity);
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "SyncData data {0}", this.Data);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001A79F File Offset: 0x0001899F
		internal SyncData() : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001A7C8 File Offset: 0x000189C8
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0001A7DF File Offset: 0x000189DF
		public new string Identity
		{
			get
			{
				return (string)this.propertyBag[SyncDataSchema.Identity];
			}
			private set
			{
				this.propertyBag[SyncDataSchema.Identity] = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001A7F2 File Offset: 0x000189F2
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0001A809 File Offset: 0x00018A09
		public string Data
		{
			get
			{
				return (string)this.propertyBag[SyncDataSchema.Data];
			}
			private set
			{
				this.propertyBag[SyncDataSchema.Data] = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001A81C File Offset: 0x00018A1C
		internal object Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001A824 File Offset: 0x00018A24
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<SyncDataSchema>();
			}
		}

		// Token: 0x040002F4 RID: 756
		[NonSerialized]
		private readonly object response;
	}
}
