using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager;

namespace Microsoft.Exchange.Management.Edge.SystemManager
{
	// Token: 0x02000011 RID: 17
	internal class QueueViewerOptions : ExchangeDataObject
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000062A5 File Offset: 0x000044A5
		public QueueViewerOptions(bool refresh, EnhancedTimeSpan interval, uint size)
		{
			this.AutoRefreshEnabled = refresh;
			this.RefreshInterval = interval;
			this.PageSize = size;
			base.ResetChangeTracking();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000062C8 File Offset: 0x000044C8
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000062DA File Offset: 0x000044DA
		public bool AutoRefreshEnabled
		{
			get
			{
				return (bool)this[QueueViewerOptionsSchema.AutoRefreshEnabled];
			}
			set
			{
				this[QueueViewerOptionsSchema.AutoRefreshEnabled] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000062ED File Offset: 0x000044ED
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000062FF File Offset: 0x000044FF
		public EnhancedTimeSpan RefreshInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[QueueViewerOptionsSchema.RefreshInterval];
			}
			set
			{
				this[QueueViewerOptionsSchema.RefreshInterval] = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00006312 File Offset: 0x00004512
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00006324 File Offset: 0x00004524
		public uint PageSize
		{
			get
			{
				return (uint)this[QueueViewerOptionsSchema.PageSize];
			}
			set
			{
				this[QueueViewerOptionsSchema.PageSize] = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00006337 File Offset: 0x00004537
		internal override ObjectSchema Schema
		{
			get
			{
				return QueueViewerOptions.schema;
			}
		}

		// Token: 0x04000036 RID: 54
		private static QueueViewerOptionsSchema schema = ObjectSchema.GetInstance<QueueViewerOptionsSchema>();
	}
}
